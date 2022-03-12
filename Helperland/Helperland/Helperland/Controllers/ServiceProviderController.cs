using Helperland.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Helperland.Models;
using Microsoft.EntityFrameworkCore;
using cloudscribe.Pagination.Models;
using System.Text;
using System.Net;
using MimeKit;
using MailKit.Net.Smtp;

namespace Helperland.Controllers
{
    public class ServiceProviderController : Controller
    {
        private readonly HelperlandContext _context;

        public ServiceProviderController(HelperlandContext context)
        {
            _context = context;

        }

        public IActionResult Index()
        {
            return View();
        }

        [ActionName("IndexWithCode")]
        public IActionResult Index(int id)
        {
            TempData["Mysetting"] = id;
            return View("Index");
        }



      






        public PagedResult<ServiceRequest> GetNewServicesDetails(int pageNumber = 1, int pageSize = 5, string sortBy = "ServiceRequestId", string sortOrder = "Asc", bool hasPet = false)
        {
            HttpContext.Session.SetInt32("pageNumber", pageNumber);
            HttpContext.Session.SetInt32("pageSize", pageSize);
            HttpContext.Session.SetString("sortBy", sortBy);
            HttpContext.Session.SetString("sortOrder", sortOrder);

            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            int id = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            var ZipCode = HttpContext.Session.GetString("code");
            HttpContext.Session.SetString("hasPet", hasPet.ToString());

            List<ServiceRequest> serviceRequests = new List<ServiceRequest>();
            PagedResult<ServiceRequest> result = new PagedResult<ServiceRequest>();

            if (ZipCode != "0")
            {

                switch (sortBy)
                {
                    case "ServiceRequestId":


                        if (sortOrder == "Asc")
                        {
                            serviceRequests = _context.ServiceRequests.Where(x => x.ZipCode.Equals(ZipCode) &&  x.HasPets.Equals(hasPet) && x.Status.Equals(1) && x.ServiceStartDate >= DateTime.Now)
                                                    .OrderBy(x => x.ServiceRequestId)
                                                    .Skip(ExcludeRecords)
                                                    .Take(pageSize)
                                                    .ToList();
                        }
                        else
                        {
                            serviceRequests = _context.ServiceRequests.Where(x => x.ZipCode.Equals(ZipCode) &&  x.HasPets.Equals(hasPet) && x.Status.Equals(1) && x.ServiceStartDate >= DateTime.Now)
                                                .OrderByDescending(x => x.ServiceRequestId)
                                                .Skip(ExcludeRecords)
                                                .Take(pageSize)
                                                .ToList();
                        }


                        break;

                    case "ServiceStartDate":


                        if (sortOrder == "Asc")
                        {
                            serviceRequests = _context.ServiceRequests.Where(x => x.ZipCode.Equals(ZipCode) && x.HasPets.Equals(hasPet) && x.Status.Equals(1) && x.ServiceStartDate >= DateTime.Now)
                                               .OrderBy(x => x.ServiceStartDate)
                                               .Skip(ExcludeRecords)
                                               .Take(pageSize)
                                               .ToList();
                        }
                        else
                        {
                            serviceRequests = _context.ServiceRequests.Where(x => x.ZipCode.Equals(ZipCode) && x.HasPets.Equals(hasPet) && x.Status.Equals(1) && x.ServiceStartDate >= DateTime.Now)
                                               .OrderByDescending(x => x.ServiceStartDate)
                                               .Skip(ExcludeRecords)
                                               .Take(pageSize)
                                               .ToList();
                        }


                        break;

                    case "UserId":



                        if (sortOrder == "Asc")
                        {
                            serviceRequests = _context.ServiceRequests.Where(x => x.ZipCode.Equals(ZipCode) && x.HasPets.Equals(hasPet) && x.Status.Equals(1) && x.ServiceStartDate >= DateTime.Now)
                                               .OrderBy(x => x.UserId)
                                               .Skip(ExcludeRecords)
                                               .Take(pageSize)
                                               .ToList();
                        }
                        else
                        {
                            serviceRequests = _context.ServiceRequests.Where(x => x.ZipCode.Equals(ZipCode) && x.HasPets.Equals(hasPet) && x.Status.Equals(1) && x.ServiceStartDate >= DateTime.Now)
                                               .OrderByDescending(x => x.UserId)
                                               .Skip(ExcludeRecords)
                                               .Take(pageSize)
                                               .ToList();
                        }
                        break;

                    case "Payment":



                        if (sortOrder == "Asc")
                        {
                            serviceRequests = _context.ServiceRequests.Where(x => x.ZipCode.Equals(ZipCode) && x.HasPets.Equals(hasPet) && x.Status.Equals(1) && x.ServiceStartDate >= DateTime.Now)
                                               .OrderBy(x => x.TotalCost)
                                               .Skip(ExcludeRecords)
                                               .Take(pageSize)
                                               .ToList();
                        }
                        else
                        {
                            serviceRequests = _context.ServiceRequests.Where(x => x.ZipCode.Equals(ZipCode) && x.HasPets.Equals(hasPet) && x.Status.Equals(1) && x.ServiceStartDate >= DateTime.Now)
                                               .OrderByDescending(x => x.TotalCost)
                                               .Skip(ExcludeRecords)
                                               .Take(pageSize)
                                               .ToList();
                        }
                        break;

                    default:

                        serviceRequests = _context.ServiceRequests.Where(x => x.ZipCode.Equals(ZipCode) && x.HasPets.Equals(hasPet) && x.Status.Equals(1) && x.ServiceStartDate >= DateTime.Now)
                                    .OrderBy(x => x.ServiceRequestId)
                                    .Skip(ExcludeRecords)
                                    .Take(pageSize)
                                    .ToList();
                        break;
                }

                result.Data = serviceRequests;
                result.TotalItems = _context.ServiceRequests.Where(x => x.ZipCode.Equals(ZipCode) && x.HasPets.Equals(hasPet) && x.Status.Equals(1) && x.ServiceStartDate >= DateTime.Now)
                    .Count();
                result.PageNumber = pageNumber;
                result.PageSize = pageSize;

            }
            else
            {
                switch (sortBy)
                {
                    case "ServiceRequestId":


                        if (sortOrder == "Asc")
                        {
                            serviceRequests = _context.ServiceRequests.Where(x => x.HasPets.Equals(hasPet) && x.Status.Equals(1) && x.ServiceStartDate >= DateTime.Now)
                                                    .OrderBy(x => x.ServiceRequestId)
                                                    .Skip(ExcludeRecords)
                                                    .Take(pageSize)
                                                    .ToList();
                        }
                        else
                        {
                            serviceRequests = _context.ServiceRequests.Where(x => x.HasPets.Equals(hasPet) && x.Status.Equals(1) && x.ServiceStartDate >= DateTime.Now)
                                                .OrderByDescending(x => x.ServiceRequestId)
                                                .Skip(ExcludeRecords)
                                                .Take(pageSize)
                                                .ToList();
                        }


                        break;

                    case "ServiceStartDate":


                        if (sortOrder == "Asc")
                        {
                            serviceRequests = _context.ServiceRequests.Where(x => x.HasPets.Equals(hasPet) && x.Status.Equals(1) && x.ServiceStartDate >= DateTime.Now).OrderBy(x => x.ServiceStartDate)
                                               .Skip(ExcludeRecords)
                                               .Take(pageSize)
                                               .ToList();
                        }
                        else
                        {
                            serviceRequests = _context.ServiceRequests.Where(x => x.HasPets.Equals(hasPet) && x.Status.Equals(1) && x.ServiceStartDate >= DateTime.Now).OrderByDescending(x => x.ServiceStartDate)
                                               .Skip(ExcludeRecords)
                                               .Take(pageSize)
                                               .ToList();
                        }


                        break;

                    case "UserId":



                        if (sortOrder == "Asc")
                        {
                            serviceRequests = _context.ServiceRequests.Where(x => x.HasPets.Equals(hasPet) && x.Status.Equals(1) && x.ServiceStartDate >= DateTime.Now).OrderBy(x => x.UserId)
                                               .Skip(ExcludeRecords)
                                               .Take(pageSize)
                                               .ToList();
                        }
                        else
                        {
                            serviceRequests = _context.ServiceRequests.Where(x => x.HasPets.Equals(hasPet) && x.Status.Equals(1) && x.ServiceStartDate >= DateTime.Now).OrderByDescending(x => x.UserId)
                                               .Skip(ExcludeRecords)
                                               .Take(pageSize)
                                               .ToList();
                        }
                        break;

                    case "Payment":



                        if (sortOrder == "Asc")
                        {
                            serviceRequests = _context.ServiceRequests.Where(x => x.HasPets.Equals(hasPet) && x.Status.Equals(1) && x.ServiceStartDate >= DateTime.Now).OrderBy(x => x.TotalCost)
                                               .Skip(ExcludeRecords)
                                               .Take(pageSize)
                                               .ToList();
                        }
                        else
                        {
                            serviceRequests = _context.ServiceRequests.Where(x => x.HasPets.Equals(hasPet) && x.Status.Equals(1) && x.ServiceStartDate >= DateTime.Now).OrderByDescending(x => x.TotalCost)
                                               .Skip(ExcludeRecords)
                                               .Take(pageSize)
                                               .ToList();
                        }
                        break;

                    default:

                        serviceRequests = _context.ServiceRequests.Where(x => x.HasPets.Equals(hasPet) && x.Status.Equals(1) && x.ServiceStartDate >= DateTime.Now)
                                    .OrderBy(x => x.ServiceRequestId)
                                    .Skip(ExcludeRecords)
                                    .Take(pageSize)
                                    .ToList();
                        break;
                }

                result.Data = serviceRequests;
                result.TotalItems = _context.ServiceRequests.Where(x => x.HasPets.Equals(hasPet) && x.Status.Equals(1) && x.ServiceStartDate >= DateTime.Now)
                                    .Count();
                result.PageNumber = pageNumber;
                result.PageSize = pageSize;

            }


            List<FavoriteAndBlocked> blockedCustomerList = _context.FavoriteAndBlockeds.Where(x => x.UserId.Equals(id) && x.IsBlocked == true).ToList();

            foreach (FavoriteAndBlocked cust in blockedCustomerList)
            {
                serviceRequests.RemoveAll(x => x.UserId.Equals(cust.TargetUserId));
            }

            List<User> users = new List<User>();
            List<ServiceRequestAddress> addresses = new List<ServiceRequestAddress>();

            foreach (ServiceRequest service in serviceRequests)
            {
                User temp = _context.Users.Find(service.UserId);
                users.Add(temp);

                ServiceRequestAddress add = _context.ServiceRequestAddresses.Where(x => x.ServiceRequestId.Equals(service.ServiceRequestId)).FirstOrDefault();
                addresses.Add(add);
            }

            TempData["users"] = users;
            TempData["addresses"] = addresses;

            return result;
        }

        public IActionResult GetNewServices(int pageNumber = 1, int pageSize = 5, string sortBy = "ServiceRequestId", string sortOrder = "Asc", bool hasPet=false)
        {

            PagedResult<ServiceRequest> result = GetNewServicesDetails(pageNumber, pageSize, sortBy, sortOrder, hasPet);
            if (hasPet == true)
                hasPet = false;
            else hasPet = true;
            ViewBag.hasPet = hasPet;
            ViewBag.sortBy = sortBy;
            switch (sortBy)
            {
                case "ServiceRequestId":
                    ViewBag.ServiceRequestIdsortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "ServiceStartDate":
                    ViewBag.ServiceRequestDatesortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "UserId":
                    ViewBag.UserIdsortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Payment":
                    ViewBag.PaymentsortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                default:
                    ViewBag.ServiceRequestIdsortOrder = "Desc";
                    break;
            }

            ViewBag.users = TempData["users"];
            ViewBag.addresses = TempData["addresses"];

            return PartialView("_NewServicePartial", result);
        }
        public void SendMailstoSp(string zipcode, int serviceRequestId)
        {
            int id = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            List<User> users = _context.Users.Where(x => x.UserTypeId.Equals(2) && x.UserId != id && x.ZipCode.Equals(zipcode)).ToList();

            foreach(User u in users)
            {
                var name = u.FirstName + " " + u.LastName;

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Helperland", "helperlandindia@gmail.com"));
                message.To.Add(new MailboxAddress(name, u.Email));
                message.Subject =" Regarding Service Request Id = "+ serviceRequestId;
                message.Body = new TextPart("plain")
                {
                    Text = "\n\nService Request id:" + serviceRequestId + " has already been accepted by someone and is no more available."
                };
                using var client = new SmtpClient();
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("helperlandindia@gmail.com", "Helperland@123");
                client.Send(message);
                client.Disconnect(true);
            }

        }
        public async Task<IActionResult> Accept(int serviceRequestId)
        {
            int id = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            ServiceRequest service = await _context.ServiceRequests.FindAsync(serviceRequestId);

            if(service!=null)
            {
                if(service.ServiceProviderId==null)
                {
                    var AST = service.ServiceStartDate;
                    double totalhour = Convert.ToDouble(service.ServiceHours + service.ExtraHours);
                    var AET = AST.AddHours(totalhour);

                    List<ServiceRequest> serviceRequestss = await _context.ServiceRequests.Where(x => x.ServiceProviderId.Equals(id) && x.Status.Equals(2) && x.ServiceStartDate > DateTime.Now).ToListAsync();


                    foreach(ServiceRequest s in serviceRequestss)
                    {
                        //if(s.ServiceStartDate.Date>startDateTime.Date || startDateTime.Date>s.ServiceStartDate.Date)
                        if(s.ServiceStartDate.Date.Subtract(AST.Date)>=TimeSpan.FromHours(24) || AST.Date.Subtract(s.ServiceStartDate.Date)>=TimeSpan.FromHours(24))
                        {
                            continue;
                        }
                        else // same date
                        {
                            var BST = s.ServiceStartDate;
                            var BET = BST.AddHours((double)(s.ServiceHours + s.ExtraHours));

                            if (AST >= BST)
                            {
                                if (AET < BET || (BET.AddHours(1) > AST))
                                {
                                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                    return Json(new { message = "Another service request with Id =" + s.ServiceRequestId + " has already been assigned which has time overlap with this service request. You can’t pick this one!" });

                                }
                                else
                                    continue;
                            }
                            else
                            {
                                if(BET<AET||(AET.AddHours(1)>BST))
                                {
                                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                return Json(new { message = "Another service request with Id ="+ s.ServiceRequestId+" has already been assigned which has time overlap with this service request. You can’t pick this one!" });
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                    }



                    service.ServiceProviderId = id;
                    service.SpacceptedDate = DateTime.Now;
                    service.Status = 2;
                    service.ModifiedDate = DateTime.Now;

                    this._context.ServiceRequests.Update(service);
                    await this._context.SaveChangesAsync();

                    SendMailstoSp(service.ZipCode, service.ServiceRequestId);

                    int pageNumber = (int)HttpContext.Session.GetInt32("pageNumber");
                    int pageSize = (int)HttpContext.Session.GetInt32("pageSize");
                    string sortBy = HttpContext.Session.GetString("sortBy");
                    string sortOrder =HttpContext.Session.GetString("sortOrder");
                    bool hasPet = Convert.ToBoolean(HttpContext.Session.GetString("hasPet"));
                    PagedResult<ServiceRequest> result = GetNewServicesDetails(pageNumber, pageSize, sortBy, sortOrder, hasPet);
                    
                    if (hasPet == true)
                        hasPet = false;
                    else hasPet = true;
                    ViewBag.hasPet = hasPet;


                    ViewBag.sortBy = sortBy;
                    switch (sortBy)
                    {
                        case "ServiceRequestId":
                            ViewBag.ServiceRequestIdsortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                            break;
                        case "ServiceStartDate":
                            ViewBag.ServiceRequestDatesortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                            break;
                        case "UserId":
                            ViewBag.UserIdsortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                            break;
                        case "Payment":
                            ViewBag.PaymentsortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                            break;
                        default:
                            ViewBag.ServiceRequestIdsortOrder = "Desc";
                            break;
                    }

                    ViewBag.users = TempData["users"];
                    ViewBag.addresses = TempData["addresses"];

                    return PartialView("_NewServicePartial", result);

                }
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return Json(new { message = "This service request is no more available. It has been assigned to another provider." });

                }
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { message = "This service request is no more available. It has been assigned to another provider." });
            }
        }
        
        


        public async Task<IActionResult> GetServiceDetails(int serviceRequestId, int tab=0)
        {
            ViewBag.tab = tab;
            ServiceRequest serviceRequest = await _context.ServiceRequests.Where(x => x.ServiceRequestId.Equals(serviceRequestId)).FirstOrDefaultAsync();

            var startDateTime = serviceRequest.ServiceStartDate;
            var date = startDateTime.ToString("dd/MM/yyyy").Replace("-", "/");
            var time = startDateTime.ToString("HH:mm");
            double totalhour = Convert.ToDouble(serviceRequest.ServiceHours + serviceRequest.ExtraHours);
            var endTime = startDateTime.AddHours(totalhour).ToString("HH:mm");

            ViewBag.serviceDetails = serviceRequest;
            ViewBag.date = date;
            ViewBag.time = time;
            ViewBag.totalhour = totalhour;
            ViewBag.endtime = endTime;

            ServiceRequestExtra serviceRequestExtra = await _context.ServiceRequestExtras.Where(x =>
                x.ServiceRequestId.Equals(serviceRequestId)).FirstOrDefaultAsync();

            var extras = serviceRequestExtra.ServiceExtraId.ToString("D5");
            var etc = extras.ToCharArray();
            var str = "";
            if (extras != "0")
            {

                if (etc[0] == '1')
                {
                    str += "Inside cabinets";
                }
                if (etc[1] == '1')
                {
                    str += ", Inside fridge";
                }
                if (etc[2] == '1')
                {
                    str += ", Inside oven";
                }
                if (etc[3] == '1')
                {
                    str += ", Laundry wash & dry";
                }
                if (etc[4] == '1')
                {
                    str += ", Interior windows";
                }

                if (str != "")
                {
                    if (str.StartsWith(","))
                    {
                        str = str.Remove(0, 1);
                    }
                }

            }
            ViewBag.extras = str;

            ServiceRequestAddress serviceRequestAddress = await _context.ServiceRequestAddresses.Where(x =>
            x.ServiceRequestId.Equals(serviceRequestId)).FirstOrDefaultAsync();

            ViewBag.serviceAddress = serviceRequestAddress;

            User user = await _context.Users.FindAsync(serviceRequest.UserId);

            ViewBag.user = user;

            return PartialView("_DetailsModalPartial",serviceRequest);
        }

        
        
        
       public PagedResult<ServiceRequest> GetUpcomingServiceDetails(int pageNumber = 1, int pageSize = 5, string sortBy = "ServiceRequestId", string sortOrder = "Asc")
        {
            HttpContext.Session.SetInt32("pageNumber", pageNumber);
            HttpContext.Session.SetInt32("pageSize", pageSize);
            HttpContext.Session.SetString("sortBy", sortBy);
            HttpContext.Session.SetString("sortOrder", sortOrder);

            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            int id = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            var ZipCode = HttpContext.Session.GetString("code");

            List<ServiceRequest> serviceRequests = new List<ServiceRequest>();
            switch (sortBy)
            {
                case "ServiceRequestId":


                    if (sortOrder == "Asc")
                    {
                        serviceRequests = _context.ServiceRequests.Where(x => x.Status.Equals(2) && x.ServiceProviderId.Equals(id))
                                                .OrderBy(x => x.ServiceRequestId)
                                                .Skip(ExcludeRecords)
                                                .Take(pageSize)
                                                .ToList();
                    }
                    else
                    {
                        serviceRequests = _context.ServiceRequests.Where(x => x.Status.Equals(2) && x.ServiceProviderId.Equals(id))
                                            .OrderByDescending(x => x.ServiceRequestId)
                                            .Skip(ExcludeRecords)
                                            .Take(pageSize)
                                            .ToList();
                    }


                    break;
                case "ServiceStartDate":


                    if (sortOrder == "Asc")
                    {
                        serviceRequests = _context.ServiceRequests.Where(x => x.Status.Equals(2) && x.ServiceProviderId.Equals(id))
                                           .OrderBy(x => x.ServiceStartDate)
                                           .Skip(ExcludeRecords)
                                           .Take(pageSize)
                                           .ToList();
                    }
                    else
                    {
                        serviceRequests = _context.ServiceRequests.Where(x => x.Status.Equals(2) && x.ServiceProviderId.Equals(id))
                                           .OrderByDescending(x => x.ServiceStartDate)
                                           .Skip(ExcludeRecords)
                                           .Take(pageSize)
                                           .ToList();
                    }


                    break;
                case "UserId":



                    if (sortOrder == "Asc")
                    {
                        serviceRequests = _context.ServiceRequests.Where(x => x.Status.Equals(2) && x.ServiceProviderId.Equals(id))
                                           .OrderBy(x => x.UserId)
                                           .Skip(ExcludeRecords)
                                           .Take(pageSize)
                                           .ToList();
                    }
                    else
                    {
                        serviceRequests = _context.ServiceRequests.Where(x => x.Status.Equals(2) && x.ServiceProviderId.Equals(id))
                                           .OrderByDescending(x => x.UserId)
                                           .Skip(ExcludeRecords)
                                           .Take(pageSize)
                                           .ToList();
                    }
                    break;

                case "Payment":



                    if (sortOrder == "Asc")
                    {
                        serviceRequests = _context.ServiceRequests.Where(x => x.Status.Equals(2) && x.ServiceProviderId.Equals(id))
                                           .OrderBy(x => x.TotalCost)
                                           .Skip(ExcludeRecords)
                                           .Take(pageSize)
                                           .ToList();
                    }
                    else
                    {
                        serviceRequests = _context.ServiceRequests.Where(x => x.Status.Equals(2) && x.ServiceProviderId.Equals(id))
                                           .OrderByDescending(x => x.TotalCost)
                                           .Skip(ExcludeRecords)
                                           .Take(pageSize)
                                           .ToList();
                    }
                    break;

                case "Distance":



                    if (sortOrder == "Asc")
                    {
                        serviceRequests = _context.ServiceRequests.Where(x => x.Status.Equals(2) && x.ServiceProviderId.Equals(id))
                                           .OrderBy(x => x.Distance)
                                           .Skip(ExcludeRecords)
                                           .Take(pageSize)
                                           .ToList();
                    }
                    else
                    {
                        serviceRequests = _context.ServiceRequests.Where(x => x.Status.Equals(2) && x.ServiceProviderId.Equals(id))
                                           .OrderByDescending(x => x.Distance)
                                           .Skip(ExcludeRecords)
                                           .Take(pageSize)
                                           .ToList();
                    }
                    break;

                default:

                    serviceRequests = _context.ServiceRequests.Where(x => x.Status.Equals(2) && x.ServiceProviderId.Equals(id))
                                .OrderBy(x => x.ServiceRequestId)
                                .Skip(ExcludeRecords)
                                .Take(pageSize)
                                .ToList();
                    break;
            }

            var result = new PagedResult<ServiceRequest>
            {
                Data = serviceRequests,
                TotalItems = _context.ServiceRequests.Where(x => x.Status.Equals(2) && x.ServiceProviderId.Equals(id))
                              .Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            List<User> users = new List<User>();
            List<ServiceRequestAddress> addresses = new List<ServiceRequestAddress>();

            foreach (ServiceRequest service in serviceRequests)
            {
                User temp = _context.Users.Find(service.UserId);
                users.Add(temp);

                ServiceRequestAddress add = _context.ServiceRequestAddresses.Where(x => x.ServiceRequestId.Equals(service.ServiceRequestId)).FirstOrDefault();
                addresses.Add(add);
            }

            TempData["users"] = users;
            TempData["addresses"] = addresses;

            return result;

        }

        public IActionResult GetUpcomingService(int pageNumber = 1, int pageSize = 5, string sortBy = "ServiceRequestId", string sortOrder = "Asc")
        {

            PagedResult<ServiceRequest> result = GetUpcomingServiceDetails(pageNumber, pageSize, sortBy, sortOrder);


            ViewBag.sortBy = sortBy;
            switch (sortBy)
            {
                case "ServiceRequestId":
                    ViewBag.ServiceRequestIdsortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "ServiceStartDate":
                    ViewBag.ServiceRequestDatesortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "UserId":
                    ViewBag.UserIdsortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Payment":
                    ViewBag.PaymentsortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Distance":
                    ViewBag.ServiceRequestDistancesortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;

                default:
                    ViewBag.ServiceRequestIdsortOrder = "Desc";
                    break;
            }

            ViewBag.users = TempData["users"];
            ViewBag.addresses = TempData["addresses"];

            return PartialView("_UpcomingServicePartial", result);
        }

        public IActionResult Complete(int serviceRequestId)
        {
            ServiceRequest service = _context.ServiceRequests.Find(serviceRequestId);
            service.Status = 3;
            service.ModifiedDate = DateTime.Now;
            this._context.ServiceRequests.Update(service);
            this._context.SaveChanges();


            int pageNumber = (int)HttpContext.Session.GetInt32("pageNumber");
            int pageSize = (int)HttpContext.Session.GetInt32("pageSize");
            string sortBy = HttpContext.Session.GetString("sortBy");
            string sortOrder = HttpContext.Session.GetString("sortOrder");


            PagedResult<ServiceRequest> result = GetUpcomingServiceDetails(pageNumber, pageSize, sortBy, sortOrder);

            ViewBag.sortBy = sortBy;
            switch (sortBy)
            {
                case "ServiceRequestId":
                    ViewBag.ServiceRequestIdsortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "ServiceStartDate":
                    ViewBag.ServiceRequestDatesortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "UserId":
                    ViewBag.UserIdsortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Payment":
                    ViewBag.PaymentsortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Distance":
                    ViewBag.ServiceRequestDistancesortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;

                default:
                    ViewBag.ServiceRequestIdsortOrder = "Desc";
                    break;
            }

            ViewBag.users = TempData["users"];
            ViewBag.addresses = TempData["addresses"];

            return PartialView("_UpcomingServicePartial", result);
        }

        public IActionResult Cancel(int serviceRequestId)
        {
            ServiceRequest service = _context.ServiceRequests.Find(serviceRequestId);
            service.Status = 4;
            service.ModifiedDate = DateTime.Now;
            this._context.ServiceRequests.Update(service);
            this._context.SaveChanges();


            int pageNumber = (int)HttpContext.Session.GetInt32("pageNumber");
            int pageSize = (int)HttpContext.Session.GetInt32("pageSize");
            string sortBy = HttpContext.Session.GetString("sortBy");
            string sortOrder = HttpContext.Session.GetString("sortOrder");


            PagedResult<ServiceRequest> result = GetUpcomingServiceDetails(pageNumber, pageSize, sortBy, sortOrder);

            ViewBag.sortBy = sortBy;
            switch (sortBy)
            {
                case "ServiceRequestId":
                    ViewBag.ServiceRequestIdsortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "ServiceStartDate":
                    ViewBag.ServiceRequestDatesortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "UserId":
                    ViewBag.UserIdsortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Payment":
                    ViewBag.PaymentsortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Distance":
                    ViewBag.ServiceRequestDistancesortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;

                default:
                    ViewBag.ServiceRequestIdsortOrder = "Desc";
                    break;
            }

            ViewBag.users = TempData["users"];
            ViewBag.addresses = TempData["addresses"];

            return PartialView("_UpcomingServicePartial", result);
        }




        public IActionResult GetServiceSchedule()
        {
            return PartialView("_ServiceSchedulePartial");
        }




        public PagedResult<ServiceRequest> GetServiceHistoryDetails(int pageNumber = 1, int pageSize = 5,
            string sortBy = "ServiceRequestId", string sortOrder = "Asc")
        {
            HttpContext.Session.SetInt32("pageNumber", pageNumber);
            HttpContext.Session.SetInt32("pageSize", pageSize);
            HttpContext.Session.SetString("sortBy", sortBy);
            HttpContext.Session.SetString("sortOrder", sortOrder);

            int ExcludeRecords = (pageSize * pageNumber) - pageSize;

            int id = Convert.ToInt32(HttpContext.Session.GetString("UserId"));

            List<ServiceRequest> serviceRequests = new List<ServiceRequest>();

            switch (sortBy)
            {

                case "ServiceRequestId":


                    if (sortOrder == "Asc")
                    {
                        serviceRequests = _context.ServiceRequests.Where(x =>
                                                x.Status.Equals(3) && x.ServiceProviderId.Equals(id))
                                                .OrderBy(x => x.ServiceRequestId)
                                                .Skip(ExcludeRecords)
                                                .Take(pageSize)
                                                .ToList();
                    }
                    else
                    {
                        serviceRequests = _context.ServiceRequests.Where(x =>
                                             x.Status.Equals(3) && x.ServiceProviderId.Equals(id))
                                            .OrderByDescending(x => x.ServiceRequestId)
                                            .Skip(ExcludeRecords)
                                            .Take(pageSize)
                                            .ToList();
                    }
                    break;

                case "ServiceStartDate":


                    if (sortOrder == "Asc")
                    {
                        serviceRequests = _context.ServiceRequests.Where(x =>
                                             x.Status.Equals(3) && x.ServiceProviderId.Equals(id))
                                            .OrderBy(x => x.ServiceStartDate)
                                           .Skip(ExcludeRecords)
                                           .Take(pageSize)
                                           .ToList();
                    }
                    else
                    {
                        serviceRequests = _context.ServiceRequests.Where(x =>
                                              x.Status.Equals(3) && x.ServiceProviderId.Equals(id))
                                            .OrderByDescending(x => x.ServiceStartDate)
                                           .Skip(ExcludeRecords)
                                           .Take(pageSize)
                                           .ToList();
                    }
                    break;

                case "Distance":


                    if (sortOrder == "Asc")
                    {
                        serviceRequests = _context.ServiceRequests.Where(x =>
                                             x.Status.Equals(3) && x.ServiceProviderId.Equals(id))
                                            .OrderBy(x => x.Distance)
                                           .Skip(ExcludeRecords)
                                           .Take(pageSize)
                                           .ToList();
                    }
                    else
                    {
                        serviceRequests = _context.ServiceRequests.Where(x =>
                                              x.Status.Equals(3) && x.ServiceProviderId.Equals(id))
                                            .OrderByDescending(x => x.Distance)
                                           .Skip(ExcludeRecords)
                                           .Take(pageSize)
                                           .ToList();
                    }
                    break;

                default:
                    serviceRequests = _context.ServiceRequests.Where(x =>
                                                x.Status.Equals(3) && x.ServiceProviderId.Equals(id))
                                                .OrderBy(x => x.ServiceRequestId)
                                                .Skip(ExcludeRecords)
                                                .Take(pageSize)
                                                .ToList();
                    break;
            }


            List<User> users = new List<User>();
            List<ServiceRequestAddress> addresses = new List<ServiceRequestAddress>();

            foreach (ServiceRequest service in serviceRequests)
            {
                User temp = _context.Users.Find(service.UserId);
                users.Add(temp);

                ServiceRequestAddress add = _context.ServiceRequestAddresses.Where(x => x.ServiceRequestId.Equals(service.ServiceRequestId)).FirstOrDefault();
                addresses.Add(add);
            }

            var result = new PagedResult<ServiceRequest>
            {
                Data = serviceRequests,
                TotalItems = _context.ServiceRequests.Where(x =>
                            x.Status.Equals(3) && x.ServiceProviderId.Equals(id))
                                .Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            TempData["users"] = users;
            TempData["addresses"] = addresses;

            return result;

        }
        public IActionResult GetServiceHistory(int pageNumber = 1, int pageSize = 5, string sortBy = "ServiceRequestId", string sortOrder = "Asc")
        {
            ViewBag.sortBy = sortBy;

            PagedResult<ServiceRequest> result = GetServiceHistoryDetails(pageNumber, pageSize,sortBy,sortOrder);

            switch (sortBy)
            {
                case "ServiceRequestId":
                    ViewBag.ServiceRequestIdsortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "ServiceStartDate":
                    ViewBag.ServiceRequestDatesortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Distance":
                    ViewBag.ServiceRequestDistancesortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;

                default:
                    ViewBag.ServiceRequestIdsortOrder = "Desc";
                    break;
            }

            ViewBag.users = TempData["users"];
            ViewBag.addresses = TempData["addresses"];

            return PartialView("_ServiceHistoryPartial", result);
        }

        public IActionResult Export()
        {
            string sortBy = HttpContext.Session.GetString("sortBy");
            string sortOrder = HttpContext.Session.GetString("sortOrder");
            int pageNumber = (int)HttpContext.Session.GetInt32("pageNumber");
            int pageSize = (int)HttpContext.Session.GetInt32("pageSize");

            PagedResult<ServiceRequest> result = GetServiceHistoryDetails(pageNumber, pageSize, sortBy, sortOrder);

            List<User> users = (List<User>)TempData["users"];
            List<ServiceRequestAddress> addresses = (List<ServiceRequestAddress>)TempData["addresses"];

            List<ServiceRequest> serviceRequests = result.Data;

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Service Id,Service Date,Customer Name,Customer Address");
            var i = 0;
            foreach (var service in serviceRequests)
            {
                
                  var name = users[i].FirstName + " " + users[i].LastName;
                var add = addresses[i].AddressLine1 + " " + addresses[i].AddressLine2 + "," + addresses[i].City + " " + addresses[i].PostalCode;

                var startDateTime = service.ServiceStartDate;
                var date = startDateTime.ToString("dd/MM/yyyy").Replace("-", "/");
                var time = startDateTime.ToString("HH:mm");
                double totalhour = Convert.ToDouble(service.ServiceHours + service.ExtraHours);
                var endTime = startDateTime.AddHours(totalhour).ToString("HH:mm");

                var finalDate = date + " " + time + "-" + endTime;
                stringBuilder.AppendLine($"{service.ServiceRequestId},{finalDate},{name},{add}");
                i++;
            }

            return File(Encoding.UTF8.GetBytes(stringBuilder.ToString()), "text/csv", "history.csv");

        }
        
        
        
        public PagedResult<ServiceRequest> GetMyRattingsDetails(int rateBy = 0, int pageNumber = 1, int pageSize = 5)
        {
            HttpContext.Session.SetInt32("pageNumber", pageNumber);
            HttpContext.Session.SetInt32("pageSize", pageSize);
            HttpContext.Session.SetInt32("rateBy", rateBy);

            TempData["rateBy"] = rateBy;
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            int id = Convert.ToInt32(HttpContext.Session.GetString("UserId"));

            List<Rating> ratings = new List<Rating>();
            int count = 0;
            if (rateBy != 0)
            {
                ratings = _context.Ratings.Where(x => x.RatingTo.Equals(id) && x.Ratings >= rateBy && x.Ratings < (rateBy + 1))
                                 .Skip(ExcludeRecords)
                                 .Take(pageSize)
                                 .ToList();
                count = _context.Ratings.Where(x => x.RatingTo.Equals(id) && x.Ratings >= rateBy && x.Ratings < (rateBy + 1))
                                .Count();
            }
            else
            {
                ratings = _context.Ratings.Where(x => x.RatingTo.Equals(id))
                                    .Skip(ExcludeRecords)
                                    .Take(pageSize)
                                    .ToList();

                count = _context.Ratings.Where(x => x.RatingTo.Equals(id))
                              .Count();
            }

            List<ServiceRequest> serviceRequests = new List<ServiceRequest>();

            List<User> users = new List<User>();


            foreach (Rating rate in ratings)
            {
                ServiceRequest service = _context.ServiceRequests.Find(rate.ServiceRequestId);

                serviceRequests.Add(service);

            }

            foreach (ServiceRequest service in serviceRequests)
            {
                User temp = _context.Users.Find(service.UserId);

                users.Add(temp);

            }

            var result = new PagedResult<ServiceRequest>
            {
                Data = serviceRequests,
                TotalItems = count,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            TempData["ratings"] = ratings;
            TempData["users"] = users;

            return result;

        }

        public IActionResult GetMyRattings(int rateBy = 0, int pageNumber = 1, int pageSize = 5)
        {

            PagedResult<ServiceRequest> result = GetMyRattingsDetails(rateBy, pageNumber,pageSize);

            ViewBag.ratings =TempData["ratings"];
            ViewBag.users =TempData["users"];
            ViewBag.rateBy = TempData["rateBy"];

            return PartialView("_MyRattingsPartial", result);
        }

        
        
        
        
        
        public PagedResult<User> GetBlockCustomerDetails(int pageNumber = 1, int pageSize = 5)
        {
            HttpContext.Session.SetInt32("pageNumber", pageNumber);
            HttpContext.Session.SetInt32("pageSize", pageSize);

            int ExcludeRecords = (pageSize * pageNumber) - pageSize;

            int id = Convert.ToInt32(HttpContext.Session.GetString("UserId"));

            var ids = _context.ServiceRequests.Where(x => x.Status.Equals(3) && x.ServiceProviderId.Equals(id))
                                                            .Skip(ExcludeRecords)
                                                            .Take(pageSize)
                                                            .AsEnumerable()
                                                            .GroupBy(x => x.UserId)
                                                            .ToList();
            List<User> users = new List<User>();

            foreach (var i in ids)
            {
                User temp = _context.Users.Find(i.Key);
                users.Add(temp);
            }

            var result = new PagedResult<User>
            {
                Data = users,
                TotalItems = _context.ServiceRequests.Where(x => x.Status.Equals(3) && x.ServiceProviderId.Equals(id))
                                                            .AsEnumerable()
                                                            .GroupBy(x => x.UserId)
                                                            .Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            List<FavoriteAndBlocked> favoriteAndBlockeds = new List<FavoriteAndBlocked>();
            List<bool> hasEntry = new List<bool>();

            foreach (var i in users)
            {
                FavoriteAndBlocked temp = _context.FavoriteAndBlockeds.Where(x =>
                       x.UserId.Equals(id) && x.TargetUserId.Equals(i.UserId)).FirstOrDefault();
                if (temp != null)
                {
                    favoriteAndBlockeds.Add(temp);
                    hasEntry.Add(true);
                }
                else
                {
                    favoriteAndBlockeds.Add(temp);
                    hasEntry.Add(false);
                }
            }

            TempData["favs"] = favoriteAndBlockeds;
            TempData["hasEntry"] = hasEntry;

            return result;

        }
        
        public IActionResult GetBlockCustomer(int pageNumber = 1, int pageSize = 5)
        {

            PagedResult<User> result = GetBlockCustomerDetails(pageNumber, pageSize);

            ViewBag.favs = TempData["favs"];
            ViewBag.hasEntry = TempData["hasEntry"];

            return PartialView("_BlockCustomerPartial", result);
        }

        public async Task<IActionResult> Block(int id)
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));

            FavoriteAndBlocked block = await _context.FavoriteAndBlockeds.Where(x =>
                x.UserId.Equals(userId) && x.TargetUserId.Equals(id)).FirstOrDefaultAsync();

            if (block == null)
            {
                FavoriteAndBlocked blk = new FavoriteAndBlocked
                {
                    UserId = userId,
                    TargetUserId = id,
                    IsFavorite = false,
                    IsBlocked = true
                };

                await this._context.FavoriteAndBlockeds.AddAsync(blk);
                await this._context.SaveChangesAsync();

            }
            else
            {
                block.IsBlocked = true;
                await this._context.SaveChangesAsync();
            }

            int pageNumber = (int)HttpContext.Session.GetInt32("pageNumber");
            int pageSize = (int)HttpContext.Session.GetInt32("pageSize");

            PagedResult<User> result = GetBlockCustomerDetails(pageNumber, pageSize);

            ViewBag.favs = TempData["favs"];
            ViewBag.hasEntry = TempData["hasEntry"];

            return PartialView("_BlockCustomerPartial", result);

        }

        public async Task<IActionResult> UnBlock(int id)
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));

            FavoriteAndBlocked block = await _context.FavoriteAndBlockeds.Where(x =>
                x.UserId.Equals(userId) && x.TargetUserId.Equals(id)).FirstOrDefaultAsync();

                block.IsBlocked = false;
                await this._context.SaveChangesAsync();

            int pageNumber = (int)HttpContext.Session.GetInt32("pageNumber");
            int pageSize = (int)HttpContext.Session.GetInt32("pageSize");

            PagedResult<User> result = GetBlockCustomerDetails(pageNumber, pageSize);

            ViewBag.favs = TempData["favs"];
            ViewBag.hasEntry = TempData["hasEntry"];

            return PartialView("_BlockCustomerPartial", result);
        }



        public IActionResult GetSpDashboardDetails()
        {
            //PagedResult<ServiceRequest> result = GetNewServicesDetails(1, 5, "ServiceRequestId", "Asc", false);

            //ViewBag.NewServiceCount = result.TotalItems;

            //PagedResult<ServiceRequest> result1 = GetUpcomingServiceDetails(1, 5, "ServiceRequestId", "Asc");

            //ViewBag.UpComingService = result1.TotalItems;

            //PagedResult<ServiceRequest> result2 = GetServiceHistoryDetails(1, 5, "ServiceRequestId", "Asc");

            //ViewBag.ServiceHistory = result2.TotalItems;

            return PartialView("_DashBoardPartial");
        }


        public IActionResult GetMySetting()
        {
            return PartialView("_MySettingPartial");
        }


        public async Task<IActionResult> LoadMyDetails()
        {

            SpViewModel sp = new SpViewModel();


            int id = Convert.ToInt32(HttpContext.Session.GetString("UserId"));

            User user = await _context.Users.Where(x => x.UserId.Equals(id)).FirstOrDefaultAsync();

            if (user.DateOfBirth != null)
            {
                DateTime dob = (DateTime)user.DateOfBirth;
                user.Day = dob.Day;
                user.Month = dob.Month;
                user.Year = dob.Year;
            }
            else
            {
                user.Day = 00;
                user.Month = 00;
                user.Year = 0000;

            }

            if (user.NationalityId == null)
                user.NationalityId = 0;

            if (user.UserProfilePicture == null)
                user.UserProfilePicture = "0";

            if (user.Gender == null)
                user.Gender = 0;

            string city = "";
            if(user.ZipCode!=null)
            {
                string zip = user.ZipCode;
                var cityId = await _context.Zipcodes.Where(x => x.ZipcodeValue==zip).Select(x=>x.CityId).FirstOrDefaultAsync();

                city = await _context.Cities.Where(x => x.Id.Equals(cityId)).Select(x => x.CityName).FirstOrDefaultAsync();

            }
            sp.ServieProvider = user;

            UserAddress address = await _context.UserAddresses.Where(x => x.UserId.Equals(id)).FirstOrDefaultAsync();

            if(address==null)
            {
                UserAddress add = new UserAddress{ 
                
                    City = city
                };

                sp.SpAddress = add;
            }
            else
            {
            sp.SpAddress = address;

            }
            return PartialView("_DetailsPartial", sp);
        }

        public IActionResult LoadChangePassword()
        {
            return PartialView("_ChangePasswordPartial");
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(User user)
        {
            int id = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            User olduser = await _context.Users.Where(x => x.UserId.Equals(id)).FirstOrDefaultAsync();

            if (olduser.Password != user.Password)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                return Json(new
                {
                    success = false,
                    messege = "Wrong Old Password"
                });
            }
            else
            {
                olduser.Password = user.NewPassword;
                olduser.ModifiedDate = DateTime.Now;
                await _context.SaveChangesAsync();

                Response.StatusCode = (int)HttpStatusCode.OK;

                return PartialView("_ChangePasswordPartial");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDetails(SpViewModel sp)
        {
            int id = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            User olduser = await _context.Users.Where(x => x.UserId.Equals(id)).FirstOrDefaultAsync();

            olduser.FirstName = sp.ServieProvider.FirstName;

            olduser.LastName = sp.ServieProvider.LastName;

            olduser.Mobile = sp.ServieProvider.Mobile;

            int dd = Convert.ToInt32(sp.ServieProvider.Day);
            int mm = Convert.ToInt32(sp.ServieProvider.Month);
            int yy = Convert.ToInt32(sp.ServieProvider.Year);
            if (dd != 0 && mm != 0 && yy != 0)
            {
                DateTime dob = new DateTime(yy, mm, dd);
                olduser.DateOfBirth = dob;

            }
            else if (dd == 0 || mm == 0 || yy == 0)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                return Json(new
                {
                    success = false,
                    errorFor = "dates",
                    messege = "Invalid Date Format",

                });
            }

            if (sp.ServieProvider.Gender !=0)
                olduser.Gender = sp.ServieProvider.Gender;

            if (sp.ServieProvider.UserProfilePicture !="0")
                olduser.UserProfilePicture = sp.ServieProvider.UserProfilePicture;

            if (sp.ServieProvider.NationalityId != 0)
                olduser.NationalityId = sp.ServieProvider.NationalityId;

            olduser.ZipCode = sp.ServieProvider.ZipCode;

            olduser.ModifiedDate = DateTime.Now;

            this._context.Users.Update(olduser);

            UserAddress add = new UserAddress();

            add = await _context.UserAddresses.Where(x => x.UserId.Equals(id)).FirstOrDefaultAsync();

            if(add==null)
            {
                int stateId = await _context.Cities.Where(x => x.CityName.Equals(sp.SpAddress.City)).Select(x => x.StateId).FirstOrDefaultAsync();

                string state = await _context.States.Where(x => x.Id.Equals(stateId)).Select(x => x.StateName).FirstOrDefaultAsync();


                UserAddress userAddress = new UserAddress { 
                    UserId=id,
                    AddressLine1=sp.SpAddress.AddressLine1,
                    AddressLine2 = sp.SpAddress.AddressLine2,
                    City = sp.SpAddress.City,
                    State = state,
                    PostalCode = sp.ServieProvider.ZipCode,
                    Mobile= olduser.Mobile,
                    Email = olduser.Email
                };

                this._context.UserAddresses.Update(userAddress);

                await this._context.SaveChangesAsync();

                Response.StatusCode = (int)HttpStatusCode.OK;


                return PartialView("_DetailsPartial", sp);


            }
            else
            {
                add.AddressLine1 = sp.SpAddress.AddressLine1;
                add.AddressLine2 = sp.SpAddress.AddressLine2;
                add.PostalCode = sp.ServieProvider.ZipCode;
                add.City = sp.SpAddress.City;

                int stateId = await _context.Cities.Where(x => x.CityName.Equals(sp.SpAddress.City)).Select(x=>x.StateId).FirstOrDefaultAsync();

                string state = await _context.States.Where(x => x.Id.Equals(stateId)).Select(x => x.StateName).FirstOrDefaultAsync();

                add.State = state;

                this._context.UserAddresses.Update(add);
                await this._context.SaveChangesAsync();

                Response.StatusCode = (int)HttpStatusCode.OK;


                return PartialView("_DetailsPartial", sp);

            }

        }
    }
}
