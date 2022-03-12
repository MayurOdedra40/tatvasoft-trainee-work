using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Helperland.Models;
using Helperland.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;
using cloudscribe.Pagination.Models;
using System.Text;

namespace Helperland.Controllers
{
    public class CustomerController : Controller
    {
        private readonly HelperlandContext _context;

        public CustomerController(HelperlandContext context)
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

        public PagedResult<ServiceRequest> GetDashboardDetails(int pageNumber = 1, int pageSize = 5, 
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
                            serviceRequests =   _context.ServiceRequests.Where(x =>
                                                      x.UserId.Equals(id) && (x.Status == 1 || x.Status == 2 || x.Status == 5 || x.Status == 7) && x.ServiceStartDate >= DateTime.Now)
                                                    .OrderBy(x => x.ServiceRequestId)
                                                    .Skip(ExcludeRecords)
                                                    .Take(pageSize)
                                                    .ToList();
                        }
                        else
                        {
                            serviceRequests = _context.ServiceRequests.Where(x =>
                                                  x.UserId.Equals(id) && (x.Status == 1 || x.Status == 2 || x.Status == 5 || x.Status == 7) && x.ServiceStartDate >= DateTime.Now)
                                                .OrderByDescending(x => x.ServiceRequestId)
                                                .Skip(ExcludeRecords)
                                                .Take(pageSize)
                                                .ToList();
                        }


                        break;
                    case "ServiceStartDate":

                   
                        if (sortOrder == "Asc")
                        {
                            serviceRequests =  _context.ServiceRequests.Where(x =>
                                               x.UserId.Equals(id) && (x.Status == 1 || x.Status == 2 || x.Status == 5 || x.Status == 7) && x.ServiceStartDate >= DateTime.Now)
                                               .OrderBy(x => x.ServiceStartDate)
                                               .Skip(ExcludeRecords)
                                               .Take(pageSize)
                                               .ToList();
                        }
                        else
                        {
                            serviceRequests =  _context.ServiceRequests.Where(x =>
                                               x.UserId.Equals(id) && (x.Status == 1 || x.Status == 2 || x.Status == 5 || x.Status == 7) && x.ServiceStartDate >= DateTime.Now)
                                               .OrderByDescending(x => x.ServiceStartDate)
                                               .Skip(ExcludeRecords)
                                               .Take(pageSize)
                                               .ToList();
                        }


                        break;
                    case "ServiceProviderId":

                    

                        if (sortOrder == "Asc")
                        {
                            serviceRequests =  _context.ServiceRequests.Where(x =>
                                               x.UserId.Equals(id) && (x.Status == 1 || x.Status == 2 || x.Status == 5 || x.Status == 7) && x.ServiceStartDate >= DateTime.Now)
                                               .OrderByDescending(x => x.ServiceProviderId)
                                               .Skip(ExcludeRecords)
                                               .Take(pageSize)
                                               .ToList();
                        }
                        else
                        {
                            serviceRequests =  _context.ServiceRequests.Where(x =>
                                               x.UserId.Equals(id) && (x.Status == 1 || x.Status == 2 || x.Status == 5 || x.Status == 7) && x.ServiceStartDate >= DateTime.Now)
                                               .OrderBy(x => x.ServiceProviderId)
                                               .Skip(ExcludeRecords)
                                               .Take(pageSize)
                                               .ToList();
                        }
                        break;

                    case "Payment":

                   

                        if (sortOrder == "Asc")
                        {
                            serviceRequests =  _context.ServiceRequests.Where(x =>
                                               x.UserId.Equals(id) && (x.Status == 1 || x.Status == 2 || x.Status == 5 || x.Status == 7) && x.ServiceStartDate >= DateTime.Now)
                                               .OrderBy(x => x.TotalCost)
                                               .Skip(ExcludeRecords)
                                               .Take(pageSize)
                                               .ToList();
                        }
                        else
                        {
                            serviceRequests = _context.ServiceRequests.Where(x =>
                                               x.UserId.Equals(id) && (x.Status == 1 || x.Status == 2 || x.Status == 5 || x.Status == 7) && x.ServiceStartDate >= DateTime.Now)
                                               .OrderByDescending(x => x.TotalCost)
                                               .Skip(ExcludeRecords)
                                               .Take(pageSize)
                                               .ToList();
                        }
                        break;

                    default:
                    
                        serviceRequests = _context.ServiceRequests.Where(x =>
                                      x.UserId.Equals(id) && (x.Status == 1 || x.Status == 2 || x.Status == 5 || x.Status == 7) && x.ServiceStartDate >= DateTime.Now)
                                    .OrderBy(x => x.ServiceRequestId)
                                    .Skip(ExcludeRecords)
                                    .Take(pageSize)
                                    .ToList();
                        break;
                }


                var result = new PagedResult<ServiceRequest>
                {
                    Data = serviceRequests,
                    TotalItems = _context.ServiceRequests.Where(x =>
                           x.UserId.Equals(id) && (x.Status == 1 || x.Status == 2 || x.Status == 5 || x.Status == 7) && x.ServiceStartDate >= DateTime.Now)
                            .Count(),

                    PageNumber = pageNumber,
                    PageSize = pageSize

                };

                List<User> sp = new List<User>();

                foreach (ServiceRequest service in serviceRequests)
                {
                    if (service.ServiceProviderId != null)
                    {
                        User user =  _context.Users.Where(x => x.UserId.Equals(service.ServiceProviderId)).FirstOrDefault();
                        sp.Add(user);

                    }
                    else
                    {
                        sp.Add(null);
                    }
                }

                List<decimal> ratings = new List<decimal>();

                foreach (User u in sp)
                {
                    if (u == null)
                    {
                        ratings.Add(0);

                    }
                    else
                    {

                        List<Rating> temp =  _context.Ratings.Where(x => x.RatingTo.Equals(u.UserId)).ToList();
                        if (temp.Count == 0)
                        {
                            ratings.Add(0);
                        }
                        else
                        {

                            decimal total = 0;
                            foreach (Rating r in temp)
                            {
                                total += r.Ratings;
                            }

                            ratings.Add(total / temp.Count);
                        }
                    }
                }

                TempData["sp"] = sp;
                TempData["ratings"] = ratings;

                return result;
        
        }
        public  IActionResult GetDashboard(int pageNumber = 1, int pageSize = 5, string sortBy = "ServiceRequestId", string sortOrder = "Asc")
        {
            ViewBag.sortBy = sortBy;
            HttpContext.Session.SetInt32("pageNumber", pageNumber);
            HttpContext.Session.SetInt32("pageSize", pageSize);
            HttpContext.Session.SetString("sortBy", sortBy);
            HttpContext.Session.SetString("sortOrder", sortOrder);

            PagedResult<ServiceRequest> result = GetDashboardDetails(pageNumber, pageSize, sortBy, sortOrder);

            ViewBag.sortBy = sortBy;


            switch (sortBy)
            {
                case "ServiceRequestId":
                    ViewBag.ServiceRequestIdsortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "ServiceStartDate":
                    ViewBag.ServiceRequestDatesortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "ServiceProviderId":
                    ViewBag.ServiceProviderIdsortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Payment":
                    ViewBag.PaymentsortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;

                default:
                    ViewBag.ServiceRequestIdsortOrder = "Desc";
                    break;
            }


            ViewBag.sp = TempData["sp"];
            ViewBag.ratings = TempData["ratings"];

            return PartialView("_DashboardPartial", result);
        }

        public  PagedResult<ServiceRequest> GetCustomerServiceHistoryDetails(int pageNumber = 1, int pageSize = 5, 
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
                        serviceRequests =  _context.ServiceRequests.Where(x =>
                                                  x.UserId.Equals(id) && (x.Status == 3 || x.Status == 4 || x.Status == 6 || x.Status == 8))
                                                .OrderBy(x => x.ServiceRequestId)
                                                .Skip(ExcludeRecords)
                                                .Take(pageSize)
                                                .ToList();
                    }
                    else
                    {
                        serviceRequests =  _context.ServiceRequests.Where(x =>
                                              x.UserId.Equals(id) && (x.Status == 3 || x.Status == 4 || x.Status == 6 || x.Status == 8))
                                            .OrderByDescending(x => x.ServiceRequestId)
                                            .Skip(ExcludeRecords)
                                            .Take(pageSize)
                                            .ToList();
                    }


                    break;
                case "ServiceStartDate":

             
                    if (sortOrder == "Asc")
                    {
                        serviceRequests =  _context.ServiceRequests.Where(x =>
                                           x.UserId.Equals(id) && (x.Status == 3 || x.Status == 4 || x.Status == 6 || x.Status == 8))
                                           .OrderBy(x => x.ServiceStartDate)
                                           .Skip(ExcludeRecords)
                                           .Take(pageSize)
                                           .ToList();
                    }
                    else
                    {
                        serviceRequests =  _context.ServiceRequests.Where(x =>
                                           x.UserId.Equals(id) && (x.Status == 3 || x.Status == 4 || x.Status == 6 || x.Status == 8))
                                           .OrderByDescending(x => x.ServiceStartDate)
                                           .Skip(ExcludeRecords)
                                           .Take(pageSize)
                                           .ToList();
                    }


                    break;
                case "ServiceProviderId":

           

                    if (sortOrder == "Asc")
                    {
                        serviceRequests =  _context.ServiceRequests.Where(x =>
                                           x.UserId.Equals(id) && (x.Status == 3 || x.Status == 4 || x.Status == 6 || x.Status == 8))
                                           .OrderByDescending(x => x.ServiceProviderId)
                                           .Skip(ExcludeRecords)
                                           .Take(pageSize)
                                           .ToList();
                    }
                    else
                    {
                        serviceRequests =  _context.ServiceRequests.Where(x =>
                                           x.UserId.Equals(id) && (x.Status == 3 || x.Status == 4 || x.Status == 6 || x.Status == 8))
                                           .OrderBy(x => x.ServiceProviderId)
                                           .Skip(ExcludeRecords)
                                           .Take(pageSize)
                                           .ToList();
                    }
                    break;

                case "Payment":

     

                    if (sortOrder == "Asc")
                    {
                        serviceRequests =  _context.ServiceRequests.Where(x =>
                                           x.UserId.Equals(id) && (x.Status == 3 || x.Status == 4 || x.Status == 6 || x.Status == 8))
                                           .OrderBy(x => x.TotalCost)
                                           .Skip(ExcludeRecords)
                                           .Take(pageSize)
                                           .ToList();
                    }
                    else
                    {
                        serviceRequests =  _context.ServiceRequests.Where(x =>
                                           x.UserId.Equals(id) && (x.Status == 3 || x.Status == 4 || x.Status == 6 || x.Status == 8))
                                           .OrderByDescending(x => x.TotalCost)
                                           .Skip(ExcludeRecords)
                                           .Take(pageSize)
                                           .ToList();
                    }
                    break;

                default:
     
                    serviceRequests = _context.ServiceRequests.Where(x =>
                                  x.UserId.Equals(id) && (x.Status == 3 || x.Status == 4 || x.Status == 6 || x.Status == 8))
                                .OrderBy(x => x.ServiceRequestId)
                                .Skip(ExcludeRecords)
                                .Take(pageSize)
                                .ToList();
                    break;
            }




            var result = new PagedResult<ServiceRequest>
            {
                Data = serviceRequests,
                TotalItems = _context.ServiceRequests.Where(x =>
                       x.UserId.Equals(id) && (x.Status == 3 || x.Status == 4 || x.Status == 6 || x.Status == 8))
                        .Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            List<User> sp = new List<User>();

            foreach (ServiceRequest service in serviceRequests)
            {
                if (service.ServiceProviderId != null)
                {
                    User user =  _context.Users.Where(x => x.UserId.Equals(service.ServiceProviderId)).FirstOrDefault();
                    sp.Add(user);

                }
                else
                {
                    sp.Add(null);
                }
            }

            List<decimal> ratings = new List<decimal>();

            foreach (User u in sp)
            {
                if (u == null)
                {
                    ratings.Add(0);

                }
                else
                {

                    List<Rating> temp = _context.Ratings.Where(x => x.RatingTo.Equals(u.UserId)).ToList();
                    if (temp.Count == 0)
                    {
                        ratings.Add(0);
                    }
                    else
                    {

                        decimal total = 0;
                        foreach (Rating r in temp)
                        {
                            total += r.Ratings;
                        }

                        ratings.Add(total / temp.Count);
                    }
                }
            }

            TempData["sp"] = sp;
            TempData["ratings"] = ratings;

            return  result;
        }

        public IActionResult GetCustomerServiceHistory( int pageNumber=1, int pageSize=5, string sortBy="ServiceRequestId", string sortOrder="Asc")
        {

            PagedResult<ServiceRequest> result = GetCustomerServiceHistoryDetails(pageNumber, pageSize, sortBy, sortOrder);

            ViewBag.sortBy = sortBy;
            switch (sortBy)
            {
                case "ServiceRequestId":
                    ViewBag.ServiceRequestIdsortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "ServiceStartDate":
                    ViewBag.ServiceRequestDatesortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "ServiceProviderId":
                    ViewBag.ServiceProviderIdsortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Payment":
                    ViewBag.PaymentsortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;

                default:
                    ViewBag.ServiceRequestIdsortOrder = "Desc";
                    break;
            }


            ViewBag.sp = TempData["sp"];
            ViewBag.ratings = TempData["ratings"];

            return PartialView("_ServiceHistoryPartial", result);
        }

        public IActionResult GetServiceSchedule()
        {
            return PartialView("_ServiceSchedulePartial");
        }

        public async Task<IActionResult> GetFavPro()
        {
            int id = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            var serviceRequestsIds = _context.ServiceRequests
                 .Where(x => x.UserId.Equals(id) && x.Status.Equals(3) && x.ServiceProviderId!=null)
                 .AsEnumerable()
                 .GroupBy(x => x.ServiceProviderId).ToList();

            List<int> ids = new List<int>();
            List<int> counts = new List<int>();

            foreach (var i in serviceRequestsIds)
            {
                counts.Add(i.Count());
                ids.Add(Convert.ToInt32(i.Key));
            }

            List<FavoriteAndBlocked> favoriteAndBlockeds = new List<FavoriteAndBlocked>();
            List<bool> hasEntry = new List<bool>();

            foreach(var i in ids)
            {
                FavoriteAndBlocked temp = await _context.FavoriteAndBlockeds.Where(x => x.UserId.Equals(id) && x.TargetUserId.Equals(i)).FirstOrDefaultAsync();
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

            ViewBag.favs = favoriteAndBlockeds;
            ViewBag.hasEntry = hasEntry;
            ViewBag.counts = counts;



            List<User> SPs = new List<User>();

            foreach (var i in ids)
            {
                var sp = _context.Users.Find(i);
                SPs.Add(sp);
            }

            ViewBag.serviceProviders = SPs;




            return PartialView("_FavProPartial");
        }

        public IActionResult GetMySetting()
        {
            return PartialView("_MySettingPartial");
        }

        
        public async Task<IActionResult> GetServiceDetails(int serviceRequestId)
        {
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

            return PartialView("_Modal");
        }

        [HttpPost]
        public async Task<IActionResult> Reschedule(ServiceRequest serviceRequest)
        {
            ServiceRequest sr = await _context.ServiceRequests.Where(x =>
                    x.ServiceRequestId.Equals(serviceRequest.ServiceRequestId)).FirstOrDefaultAsync();

            string date = serviceRequest.StartDate.ToString("yyyy-MM-dd");
            string time = serviceRequest.StartTime.ToString("HH:mm:ss");
            DateTime startDateTime = Convert.ToDateTime(date).Add(TimeSpan.Parse(time));
            sr.ServiceStartDate = startDateTime;
            sr.Status = 5;
            sr.ModifiedDate = DateTime.Now;

            this._context.SaveChanges();

            TempData["Message"] = "Booking has been Successfully rescheduled";
            TempData["ModalName"] = "#logout-Modal";

            Response.StatusCode = (int)HttpStatusCode.OK;

            string sortBy = HttpContext.Session.GetString("sortBy");
            string sortOrder = HttpContext.Session.GetString("sortOrder");
            int pageNumber = (int)HttpContext.Session.GetInt32("pageNumber");
            int pageSize = (int)HttpContext.Session.GetInt32("pageSize");

            PagedResult<ServiceRequest> result = GetDashboardDetails(pageNumber, pageSize, sortBy, sortOrder);

            ViewBag.sortBy = sortBy;


            switch (sortBy)
            {
                case "ServiceRequestId":
                                        ViewBag.ServiceRequestIdsortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                                        break;
                case "ServiceStartDate":
                                        ViewBag.ServiceRequestDatesortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                                        break;
                case "ServiceProviderId":
                                        ViewBag.ServiceProviderIdsortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                                        break;
                case "Payment":
                                        ViewBag.PaymentsortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                                        break;

                default:
                                        ViewBag.ServiceRequestIdsortOrder = "Desc";
                                        break;
            }


            ViewBag.sp = TempData["sp"];
            ViewBag.ratings =TempData["ratings"];

            return PartialView("_DashboardPartial", result);
        }

        [HttpPost]
        public async Task<IActionResult> CancelRequest(ServiceRequest serviceRequest)
        {
            ServiceRequest sr = await _context.ServiceRequests.Where(x =>
                    x.ServiceRequestId.Equals(serviceRequest.ServiceRequestId)).FirstOrDefaultAsync();

            sr.Comments = serviceRequest.Comments;
            sr.Status = 4;
            sr.ModifiedDate = DateTime.Now;
            sr.HasIssue = true;

            await this._context.SaveChangesAsync();

            TempData["Message"] = "Booking has be Cancelled";
            TempData["ModalName"] = "#logout-Modal";

            Response.StatusCode = (int)HttpStatusCode.OK;

            string sortBy = HttpContext.Session.GetString("sortBy");
            string sortOrder = HttpContext.Session.GetString("sortOrder");
            int pageNumber = (int)HttpContext.Session.GetInt32("pageNumber");
            int pageSize = (int)HttpContext.Session.GetInt32("pageSize");

            PagedResult<ServiceRequest> result = GetDashboardDetails(pageNumber, pageSize, sortBy, sortOrder);

            ViewBag.sortBy = sortBy;


            switch (sortBy)
            {
                case "ServiceRequestId":
                    ViewBag.ServiceRequestIdsortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "ServiceStartDate":
                    ViewBag.ServiceRequestDatesortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "ServiceProviderId":
                    ViewBag.ServiceProviderIdsortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Payment":
                    ViewBag.PaymentsortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;

                default:
                    ViewBag.ServiceRequestIdsortOrder = "Desc";
                    break;
            }


            ViewBag.sp = TempData["sp"];
            ViewBag.ratings = TempData["ratings"];

            return PartialView("_DashboardPartial", result);

        }

        public async Task<IActionResult> Fav(int id)
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));

            FavoriteAndBlocked favorite = await _context.FavoriteAndBlockeds.Where(x => x.UserId.Equals(userId) && x.TargetUserId.Equals(id)).FirstOrDefaultAsync();

            if(favorite==null)
            {
                FavoriteAndBlocked fav = new FavoriteAndBlocked
                {
                    UserId = userId,
                    TargetUserId = id,
                    IsFavorite = true,
                    IsBlocked = false
                };

                await this._context.FavoriteAndBlockeds.AddAsync(fav);
                await this._context.SaveChangesAsync();

            }
            else
            {
                favorite.IsFavorite = true;
                await this._context.SaveChangesAsync();
            }

            var serviceRequestsIds = _context.ServiceRequests
               .Where(x => x.UserId.Equals(userId) && x.Status.Equals(3) && x.ServiceProviderId != null)
               .AsEnumerable()
               .GroupBy(x => x.ServiceProviderId).ToList();

            List<int> ids = new List<int>();
            List<int> counts = new List<int>();

            foreach (var i in serviceRequestsIds)
            {
                counts.Add(i.Count());
                ids.Add(Convert.ToInt32(i.Key));
            }

            List<FavoriteAndBlocked> favoriteAndBlockeds = new List<FavoriteAndBlocked>();
            List<bool> hasEntry = new List<bool>();

            foreach (var i in ids)
            {
                FavoriteAndBlocked temp = await _context.FavoriteAndBlockeds.Where(x => x.UserId.Equals(userId) && x.TargetUserId.Equals(i)).FirstOrDefaultAsync();
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

            ViewBag.favs = favoriteAndBlockeds;
            ViewBag.hasEntry = hasEntry;
            ViewBag.counts = counts;



            List<User> SPs = new List<User>();

            foreach (var i in ids)
            {
                var sp = _context.Users.Find(i);
                SPs.Add(sp);
            }

            ViewBag.serviceProviders = SPs;




            return PartialView("_FavProPartial");
        }


        public async Task<IActionResult> UnFav(int id)
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));

            FavoriteAndBlocked favorite = await _context.FavoriteAndBlockeds.Where(x => x.UserId.Equals(userId) && x.TargetUserId.Equals(id)).FirstOrDefaultAsync();

            favorite.IsFavorite = false;
            await this._context.SaveChangesAsync();
    
            var serviceRequestsIds = _context.ServiceRequests
               .Where(x => x.UserId.Equals(userId) && x.Status.Equals(3) && x.ServiceProviderId != null)
               .AsEnumerable()
               .GroupBy(x => x.ServiceProviderId).ToList();

            List<int> ids = new List<int>();
            List<int> counts = new List<int>();

            foreach (var i in serviceRequestsIds)
            {
                counts.Add(i.Count());
                ids.Add(Convert.ToInt32(i.Key));
            }

            List<FavoriteAndBlocked> favoriteAndBlockeds = new List<FavoriteAndBlocked>();
            List<bool> hasEntry = new List<bool>();

            foreach (var i in ids)
            {
                FavoriteAndBlocked temp = await _context.FavoriteAndBlockeds.Where(x => x.UserId.Equals(userId) && x.TargetUserId.Equals(i)).FirstOrDefaultAsync();
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

            ViewBag.favs = favoriteAndBlockeds;
            ViewBag.hasEntry = hasEntry;
            ViewBag.counts = counts;



            List<User> SPs = new List<User>();

            foreach (var i in ids)
            {
                var sp = _context.Users.Find(i);
                SPs.Add(sp);
            }

            ViewBag.serviceProviders = SPs;

            return PartialView("_FavProPartial");
        }

        public async Task<IActionResult> Block(int id)
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));

            FavoriteAndBlocked block = await _context.FavoriteAndBlockeds.Where(x => x.UserId.Equals(userId) && x.TargetUserId.Equals(id)).FirstOrDefaultAsync();

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

            var serviceRequestsIds = _context.ServiceRequests
               .Where(x => x.UserId.Equals(userId) && x.Status.Equals(3) && x.ServiceProviderId != null)
               .AsEnumerable()
               .GroupBy(x => x.ServiceProviderId).ToList();

            List<int> ids = new List<int>();
            List<int> counts = new List<int>();

            foreach (var i in serviceRequestsIds)
            {
                counts.Add(i.Count());
                ids.Add(Convert.ToInt32(i.Key));
            }

            List<FavoriteAndBlocked> favoriteAndBlockeds = new List<FavoriteAndBlocked>();
            List<bool> hasEntry = new List<bool>();

            foreach (var i in ids)
            {
                FavoriteAndBlocked temp = await _context.FavoriteAndBlockeds.Where(x => x.UserId.Equals(userId) && x.TargetUserId.Equals(i)).FirstOrDefaultAsync();
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

            ViewBag.favs = favoriteAndBlockeds;
            ViewBag.hasEntry = hasEntry;
            ViewBag.counts = counts;



            List<User> SPs = new List<User>();

            foreach (var i in ids)
            {
                var sp = _context.Users.Find(i);
                SPs.Add(sp);
            }

            ViewBag.serviceProviders = SPs;




            return PartialView("_FavProPartial");
        }


        public async Task<IActionResult> UnBlock(int id)
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));

            FavoriteAndBlocked block = await _context.FavoriteAndBlockeds.Where(x => x.UserId.Equals(userId) && x.TargetUserId.Equals(id)).FirstOrDefaultAsync();

            block.IsBlocked = false;
            await this._context.SaveChangesAsync();

            var serviceRequestsIds = _context.ServiceRequests
               .Where(x => x.UserId.Equals(userId) && x.Status.Equals(3) && x.ServiceProviderId != null)
               .AsEnumerable()
               .GroupBy(x => x.ServiceProviderId).ToList();

            List<int> ids = new List<int>();
            List<int> counts = new List<int>();

            foreach (var i in serviceRequestsIds)
            {
                counts.Add(i.Count());
                ids.Add(Convert.ToInt32(i.Key));
            }

            List<FavoriteAndBlocked> favoriteAndBlockeds = new List<FavoriteAndBlocked>();
            List<bool> hasEntry = new List<bool>();

            foreach (var i in ids)
            {
                FavoriteAndBlocked temp = await _context.FavoriteAndBlockeds.Where(x => x.UserId.Equals(userId) && x.TargetUserId.Equals(i)).FirstOrDefaultAsync();
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

            ViewBag.favs = favoriteAndBlockeds;
            ViewBag.hasEntry = hasEntry;
            ViewBag.counts = counts;



            List<User> SPs = new List<User>();

            foreach (var i in ids)
            {
                var sp = _context.Users.Find(i);
                SPs.Add(sp);
            }

            ViewBag.serviceProviders = SPs;

            return PartialView("_FavProPartial");
        }

        public async Task<IActionResult> GetRattingDetails(int serviceRequestId)
        {
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

            int id = Convert.ToInt32(HttpContext.Session.GetString("UserId"));

            decimal ratings = 0;
            User user = new User();
            if (serviceRequest.ServiceProviderId != null)
            {

                user = await _context.Users.Where(x => x.UserId.Equals(serviceRequest.ServiceProviderId)).FirstOrDefaultAsync();
                ViewBag.user = user;

                List<Rating> temp = _context.Ratings.Where(x => x.RatingTo.Equals(serviceRequest.ServiceProviderId)).ToList();
                if (temp.Count == 0)
                {
                    ratings = 0;
                }
                else
                {

                    decimal total = 0;
                    foreach (Rating r in temp)
                    {
                        total += r.Ratings;
                    }

                    ratings =total / temp.Count;
                }


            }

            ViewBag.ratings = ratings;

            

            Rating rating = await _context.Ratings.Where(x => x.ServiceRequestId.Equals(serviceRequestId)).FirstOrDefaultAsync();

            ViewBag.rating = rating;

            return PartialView("_ModalRattingPartial");
        }

        [HttpPost]
        public async Task<IActionResult> RateSp(Rating rating) {

            int id = Convert.ToInt32(HttpContext.Session.GetString("UserId"));

            Rating tempory = await _context.Ratings.Where(x => x.ServiceRequestId.Equals(rating.ServiceRequestId)).FirstOrDefaultAsync();

            if (tempory == null)
            {
                rating.RatingFrom = id;
                rating.Ratings = (rating.OnTimeArrival + rating.Friendly + rating.QualityOfService) / 3;
                rating.RatingDate = DateTime.Now;

                await this._context.Ratings.AddAsync(rating);
                await this._context.SaveChangesAsync();

            }
            else
            {
                tempory.OnTimeArrival = rating.OnTimeArrival;
                tempory.Friendly = rating.Friendly;
                tempory.QualityOfService = rating.QualityOfService;
                tempory.Ratings = (tempory.OnTimeArrival + tempory.Friendly + tempory.QualityOfService) / 3;
                tempory.Comments = rating.Comments;
                tempory.RatingDate = DateTime.Now;

                await this._context.SaveChangesAsync();

            }

            Response.StatusCode = (int)HttpStatusCode.OK;

            string sortBy = HttpContext.Session.GetString("sortBy");
            string sortOrder = HttpContext.Session.GetString("sortOrder");
            int pageNumber = (int)HttpContext.Session.GetInt32("pageNumber");
            int pageSize = (int)HttpContext.Session.GetInt32("pageSize");

            PagedResult<ServiceRequest> result = GetCustomerServiceHistoryDetails(pageNumber, pageSize, sortBy, sortOrder);

            ViewBag.sortBy = sortBy;
            switch (sortBy)
            {
                case "ServiceRequestId":
                    ViewBag.ServiceRequestIdsortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "ServiceStartDate":
                    ViewBag.ServiceRequestDatesortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "ServiceProviderId":
                    ViewBag.ServiceProviderIdsortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Payment":
                    ViewBag.PaymentsortOrder = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;

                default:
                    ViewBag.ServiceRequestIdsortOrder = "Desc";
                    break;
            }


            ViewBag.sp = TempData["sp"];
            ViewBag.ratings = TempData["ratings"];

            return PartialView("_ServiceHistoryPartial", result);
        }

        public async Task<IActionResult> GetMyDetails()
        {
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
            if (user.LanguageId == null)
                user.LanguageId = 0;
            return PartialView("_DetailsPartial", user);
        }

        public async Task<IActionResult> GetMyAddress()
        {
            int id = Convert.ToInt32(HttpContext.Session.GetString("UserId"));

           List<UserAddress> addressess = await _context.UserAddresses.Where(x => x.UserId.Equals(id)).ToListAsync();
            return PartialView("_AddressPartial", addressess);
        }

        public IActionResult GetChangePassword()
        {
            return PartialView("_ChangePasswordPartial");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDetails(User user)
        {
            int id = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            User olduser = await _context.Users.Where(x => x.UserId.Equals(id)).FirstOrDefaultAsync();
            olduser.FirstName = user.FirstName;
            olduser.LastName = user.LastName;
            olduser.Mobile = user.Mobile;
            int dd = Convert.ToInt32(user.Day);
            int mm = Convert.ToInt32(user.Month);
            int yy = Convert.ToInt32(user.Year);
            if(dd!=0 && mm!=0 && yy != 0)
            {
            DateTime dob = new DateTime(yy,mm,dd);
            olduser.DateOfBirth = dob;

            }
            else if(dd==0 || mm==0 || yy==0)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                
                return Json(new
                {
                    success = false,
                    errorFor = "dates",
                    messege = "Invalid Date Format",
                    
                });
            }
            olduser.LanguageId = user.LanguageId;
            olduser.ModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();

            Response.StatusCode = (int)HttpStatusCode.OK;

            return PartialView("_DetailsPartial", olduser);

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

        public async Task<IActionResult> LoadAddressPartial(int addressId)
        {
            UserAddress address = await _context.UserAddresses.Where(x => x.AddressId.Equals(addressId)).FirstOrDefaultAsync();

            return PartialView("_EditAddressPartials", address);
        }

        public async Task<string> GetCity(string postalCode)
        {
            Zipcode zipcode = await _context.Zipcodes.Where(x => x.ZipcodeValue.Equals(postalCode)).FirstOrDefaultAsync();
            if (zipcode == null)
            {
                return "empty";
            }
            else
            {
                City city = await _context.Cities.Where(x => x.Id.Equals(zipcode.CityId)).FirstOrDefaultAsync();
                return city.CityName;
            }
        }

        public IActionResult LoadAddressPartialNew()
        {
            ViewBag.New = true;
            return PartialView("_EditAddressPartials");
        }

        [HttpPost]
        public async Task<IActionResult> AddNewAddress(UserAddress address)
        {
            int id = Convert.ToInt32(HttpContext.Session.GetString("UserId"));

            address.UserId = id;

            City city = await _context.Cities.Where(x => x.CityName.Equals(address.City)).FirstOrDefaultAsync();

            State state = await _context.States.Where(x => x.Id.Equals(city.StateId)).FirstOrDefaultAsync();

            address.State = state.StateName;
            address.IsDefault = false;
            address.IsDeleted = false;

            User user = await _context.Users.Where(x => x.UserId.Equals(id)).FirstOrDefaultAsync();

            address.Email = user.Email;

            await _context.UserAddresses.AddAsync(address);
            await _context.SaveChangesAsync();

            List<UserAddress> addressess = await _context.UserAddresses.Where(x => x.UserId.Equals(id)).ToListAsync();
            return PartialView("_AddressPartial", addressess);

        }

        [HttpPost]
        public async Task<IActionResult> UpdateAddress(UserAddress address)
        {
            int id = Convert.ToInt32(HttpContext.Session.GetString("UserId"));

            UserAddress oldAddress = await _context.UserAddresses.Where(x => x.AddressId.Equals(address.AddressId)).FirstOrDefaultAsync();

            City city = await _context.Cities.Where(x => x.CityName.Equals(address.City)).FirstOrDefaultAsync();

            State state = await _context.States.Where(x => x.Id.Equals(city.StateId)).FirstOrDefaultAsync();

            address.State = state.StateName;

            oldAddress.AddressLine1 = address.AddressLine1;
            oldAddress.AddressLine2 = address.AddressLine2;
            oldAddress.PostalCode = address.PostalCode;
            oldAddress.City = address.City;
            oldAddress.State = state.StateName;
            oldAddress.Mobile = address.Mobile;

            this._context.UserAddresses.Update(oldAddress);
            await this._context.SaveChangesAsync();

            List<UserAddress> addressess = await _context.UserAddresses.Where(x => x.UserId.Equals(id)).ToListAsync();
            return PartialView("_AddressPartial", addressess);


        }
        [HttpPost]
        public async Task<IActionResult> DeleteAddress(UserAddress deleteAddress)
        {
            int id = Convert.ToInt32(HttpContext.Session.GetString("UserId")); 

            UserAddress address = await _context.UserAddresses.Where(x => x.AddressId.Equals(deleteAddress.AddressId)).FirstOrDefaultAsync();

             this._context.UserAddresses.Remove(address);
            await this._context.SaveChangesAsync();

            List<UserAddress> addressess = await _context.UserAddresses.Where(x => x.UserId.Equals(id)).ToListAsync();
            return PartialView("_AddressPartial", addressess);
        }

        public IActionResult Export()
        {
            string sortBy = HttpContext.Session.GetString("sortBy");
            string sortOrder = HttpContext.Session.GetString("sortOrder");
            int pageNumber = (int)HttpContext.Session.GetInt32("pageNumber");
            int pageSize = (int)HttpContext.Session.GetInt32("pageSize");

            PagedResult<ServiceRequest> result = GetCustomerServiceHistoryDetails(pageNumber, pageSize, sortBy, sortOrder);
            
            List<User> sp = (List<User>)TempData["sp"];

            List<decimal> ratings = (List<decimal>)TempData["ratings"];

            List<ServiceRequest> serviceRequests = result.Data;

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Service Id,Service Date,Service Provider,Payment,Status");
            var i = 0;
            foreach(var service in serviceRequests)
            {
                var name = "";
                if(sp[i]!=null)
                {
                    name = sp[i].FirstName + " " + sp[i].LastName; 

                }
                var status = "";

                status = service.Status switch
                {
                    1 => "New",
                    2 => "Pending",
                    3 => "Completed",
                    4 => "Cancelled",
                    5 => "Reschedulled",
                    6 => "Refunded",
                    7 => "Changed SP",
                    8 => "Expired",
                    _ => " ",
                };

                var startDateTime = service.ServiceStartDate;
                var date = startDateTime.ToString("dd/MM/yyyy").Replace("-", "/");
                var time = startDateTime.ToString("HH:mm");
                double totalhour = Convert.ToDouble(service.ServiceHours + service.ExtraHours);
                var endTime = startDateTime.AddHours(totalhour).ToString("HH:mm");

                var finalDate = date + " " + time + "-" + endTime;
                stringBuilder.AppendLine($"{service.ServiceRequestId},{finalDate},{name},{service.TotalCost},{status}");
                status = ratings[i].ToString();
                i++;
            }

            return File(Encoding.UTF8.GetBytes(stringBuilder.ToString()), "text/csv", "history.csv");

        }
    }
}
