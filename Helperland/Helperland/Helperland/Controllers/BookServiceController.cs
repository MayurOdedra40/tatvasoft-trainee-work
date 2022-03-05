using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Helperland.Data;
using Helperland.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System;
using System.Globalization;
using Newtonsoft.Json;

namespace Helperland.Controllers
{
    public class BookServiceController : Controller
    {
        private readonly HelperlandContext _context;

        public BookServiceController(HelperlandContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("isLoggedIn") == null)
            {
                TempData["ModalName"] = "#loginModal";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CheckZipcode(User user)
        {
            List<User> Identity = await _context.Users.Where(x => x.ZipCode.Equals(user.ZipCode)).ToListAsync();
            if (Identity.Count > 0)
            {
                
                Response.StatusCode = (int)HttpStatusCode.OK;
                ViewBag.ZipCode = user.ZipCode;
                return PartialView("_BookService2Partial");
       
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { success = false, message = "We are not providing service in this area. We’ll notify you if any helper would start working near your area." });
            }
        }

        public IActionResult AddAddressForm()
        {
            City city=null;
            State state = null;
            User user=null;
            Zipcode zipcode = _context.Zipcodes.Where(x => x.ZipcodeValue.Equals(HttpContext.Session.GetString("Zipcode"))).FirstOrDefault();
            if(zipcode!=null)
            {
             city =  _context.Cities.Where(x => x.Id.Equals(zipcode.CityId)).FirstOrDefault();
                
                if (city != null)
                {
                    state =  _context.States.Where(x => x.Id.Equals(city.StateId)).FirstOrDefault();
                }
            }
            int userid = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            user = _context.Users.Where(x => x.UserId.Equals(userid)).FirstOrDefault();


            ViewBag.zipcode = zipcode;
            ViewBag.city = city;
            ViewBag.state = state;
            ViewBag.user = user;

            return PartialView("_AddNewAddressPartial");
        }

        [HttpPost]
        public IActionResult TakeServiceDetails(ServiceRequest sr)
        {

            char[] extra = { '0', '0', '0', '0', '0' };
            sr.UserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            sr.ServiceId = 0;
            string date = sr.StartDate.ToString("yyyy-MM-dd");
            string time = sr.StartTime.ToString("HH:mm:ss");
            DateTime startDateTime = Convert.ToDateTime(date).Add(TimeSpan.Parse(time));
            sr.ServiceStartDate = startDateTime;
            sr.ExtraHours = 0;
            if (sr.ExtraService1 == "true")
            {
                sr.ExtraHours += 0.5;
                extra[0] = '1';

            }
            if (sr.ExtraService2 == "true")
            {
                sr.ExtraHours += 0.5;
                extra[1] = '1';

            }
            if (sr.ExtraService3 == "true")
            {
                sr.ExtraHours += 0.5;
                extra[2] = '1';

            }
            if (sr.ExtraService4 == "true")
            {
                sr.ExtraHours += 0.5;
                extra[3] = '1';

            }
            if (sr.ExtraService5 == "true")
            {
                sr.ExtraHours += 0.5;
                extra[4] = '1';

            }

            string etc = new string(extra);
            sr.Extra = Convert.ToInt32(etc);



            decimal totalHour = (decimal)(sr.ServiceHours + sr.ExtraHours);
            sr.SubTotal = (decimal)(totalHour * sr.ServiceHourlyRate);
            sr.TotalCost = sr.SubTotal;
            sr.PaymentDue = true;
            sr.CreatedDate = DateTime.Now;
            sr.ModifiedDate = DateTime.Now;
            sr.Distance = 0;


            List<UserAddress> userAddress = _context.UserAddresses.Where(x => x.UserId.Equals(sr.UserId) && x.PostalCode.Equals(HttpContext.Session.GetString("Zipcode"))).ToList();
            HttpContext.Session.SetString("Zipcode", sr.ZipCode);
            ViewBag.addresses = userAddress;

            HttpContext.Session.SetString("sr", JsonConvert.SerializeObject(sr)); 
            return PartialView("_BookService3Partial");
        }

        [HttpPost]
        public IActionResult AddNewAddress(UserAddress userAddress)
        {
            userAddress.IsDefault = false;
            userAddress.IsDeleted = false;

             this._context.UserAddresses.Add(userAddress);
             this._context.SaveChanges();
            List<UserAddress> addressess = _context.UserAddresses.Where(x => x.UserId.Equals(userAddress.UserId) && x.PostalCode.Equals(HttpContext.Session.GetString("Zipcode"))).ToList();

            ViewBag.addresses = addressess;
            return PartialView("_BookService3Partial");
        }

        [HttpPost]
        public async Task<IActionResult> TakeAddressDetials(ServiceRequestAddress serviceRequestAddress)
        {
            UserAddress address = await _context.UserAddresses.Where(x => x.AddressId.Equals(serviceRequestAddress.UserAddressId)).FirstOrDefaultAsync();
            if (address != null)
            {
                serviceRequestAddress.AddressLine1 = address.AddressLine1;
                serviceRequestAddress.AddressLine2 = address.AddressLine2;
                serviceRequestAddress.City = address.City;
                serviceRequestAddress.State = address.State;
                serviceRequestAddress.PostalCode = address.PostalCode;
                serviceRequestAddress.Mobile = address.Mobile;
                serviceRequestAddress.Email = address.Email;
            }
            HttpContext.Session.SetString("sra", JsonConvert.SerializeObject(serviceRequestAddress));

            return PartialView("_BookService4Partial");
        }

        [HttpPost]
        public async Task<IActionResult> CompleteService(ServiceRequest serviceRequest)
        {
            var value = HttpContext.Session.GetString("sr");
            ServiceRequest newService = JsonConvert.DeserializeObject<ServiceRequest>(value);
            newService.Terms = serviceRequest.Terms;
            newService.PaymentDue = false;
            newService.Status = 1;
            newService.CreatedDate = DateTime.Now;
            newService.ModifiedDate = DateTime.Now;
            newService.PaymentDone = true;

            await this._context.ServiceRequests.AddAsync(newService);
            await this._context.SaveChangesAsync();

            HttpContext.Session.Remove("sr");

            int id = newService.ServiceRequestId;

            value = HttpContext.Session.GetString("sra");
            ServiceRequestAddress newServiceRequestAddress = JsonConvert.DeserializeObject<ServiceRequestAddress>(value);
            newServiceRequestAddress.ServiceRequestId = id;
            await this._context.ServiceRequestAddresses.AddAsync(newServiceRequestAddress);
            await this._context.SaveChangesAsync();

            HttpContext.Session.Remove("sra");

            ServiceRequestExtra serviceRequestExtra = new ServiceRequestExtra
            {
                ServiceRequestId = id,
                ServiceExtraId = newService.Extra
            };
            await this._context.ServiceRequestExtras.AddAsync(serviceRequestExtra);
            await this._context.SaveChangesAsync();

            TempData["Message"] = "Booking has been Successfully submitted";
            TempData["Message2"] = "Service Request ID: "+id.ToString();
            TempData["ModalName"] = "#logout-Modal";
            return RedirectToAction("Index","Customer");

        }
    }
}
