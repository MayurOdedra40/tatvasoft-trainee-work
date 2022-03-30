using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Helperland.Models;
using Helperland.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text;
using MimeKit;
using MailKit.Net.Smtp;
using System.Net;

namespace Helperland.Controllers
{
    public class AdminController : Controller
    {
        private readonly HelperlandContext _context;

        public AdminController(HelperlandContext context)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetServiceManage()
        {
            List<SelectListItem> customerSelect = new List<SelectListItem>
            {
                new SelectListItem { Text = "Select Customer", Value = "0" }
            };

            List<SelectListItem> serviceProviderSelect = new List<SelectListItem>
            {
                new SelectListItem { Text = "Select Service Provider", Value = "0" }
            };

            List<User> allUsers = _context.Users.ToList();

            foreach (User a in allUsers)
            {
                if (a.UserTypeId == 1)
                {
                    customerSelect.Add(new SelectListItem { Text = a.FirstName + " " + a.LastName, Value = a.UserId.ToString() });
                }
                else if (a.UserTypeId == 2)
                {
                    serviceProviderSelect.Add(new SelectListItem { Text = a.FirstName + " " + a.LastName, Value = a.UserId.ToString() });

                }
            }

            FilterServices GetService = new FilterServices();
            HttpContext.Session.SetString("GetService", JsonConvert.SerializeObject(GetService));

            ViewBag.ServiceProviderSelect = serviceProviderSelect;
            ViewBag.customerSelect = customerSelect;

            return PartialView("_ServiceManagmentPartial");

        }

        public PagedResult<ServiceRequest> GetDefaultServiceManage(int pageNumber = 1, int pageSize = 5, string sortBy = "ServiceId", string sortOrder = "Asc")
        {
            HttpContext.Session.SetInt32("pageNumber", pageNumber);
            HttpContext.Session.SetInt32("pageSize", pageSize);
            HttpContext.Session.SetString("sortBy", sortBy);
            HttpContext.Session.SetString("sortOrder", sortOrder);


            var value = HttpContext.Session.GetString("GetService");
            FilterServices GetService = JsonConvert.DeserializeObject<FilterServices>(value);

            int ExcludeRecords = (pageSize * pageNumber) - pageSize;

            List<ServiceRequest> services = new List<ServiceRequest>();

            services = _context.ServiceRequests.ToList();

            switch (sortBy)
            {
                case "ServiceId":


                    if (sortOrder == "Asc")
                    {
                        services = services.OrderBy(x => x.ServiceRequestId).ToList();
                    }
                    else
                    {
                        services = services.OrderByDescending(x => x.ServiceRequestId).ToList();
                    }

                    break;

                case "Date":


                    if (sortOrder == "Asc")
                    {
                        services = services.OrderBy(x => x.ServiceStartDate).ToList();
                    }
                    else
                    {
                        services = services.OrderByDescending(x => x.ServiceStartDate).ToList();
                    }

                    break;

                case "UserId":

                    if (sortOrder == "Asc")
                    {
                        List<int> ids = _context.Users.OrderBy(x => x.FirstName)
                                                .ThenBy(x => x.LastName)
                                                .Select(x => x.UserId)
                                                .ToList();
                        
                        List<ServiceRequest> temp = new List<ServiceRequest>();
                        
                        for(int i = 0; i < ids.Count(); i++)
                        {
                            foreach (ServiceRequest s in services)
                            {
                                if (s.UserId == ids[i])
                                {
                                    temp.Add(s);
                                }
                            }
                        }
                        services = temp;
                        
                    }
                    else
                    {
                        List<int> ids = _context.Users.OrderByDescending(x => x.FirstName)
                                                .ThenByDescending(x => x.LastName)
                                                .Select(x => x.UserId)
                                                .ToList();

                        List<ServiceRequest> temp = new List<ServiceRequest>();

                        for (int i = 0; i < ids.Count(); i++)
                        {
                            foreach (ServiceRequest s in services)
                            {
                                if (s.UserId == ids[i])
                                {
                                    temp.Add(s);
                                }
                            }
                        }
                        services = temp;
                    
                    }
                    break;

                case "Sps":



                    if (sortOrder == "Asc")
                    {
                        services = services.OrderBy(x => x.ServiceProviderId)
                                                    .ToList();

                        List<ServiceRequest> temp = new List<ServiceRequest>();

                        foreach (ServiceRequest s in services)
                        {
                            if (s.ServiceProviderId==null)
                            {
                                temp.Add(s);
                            }
                        }


                        List<int> ids = _context.Users.OrderBy(x => x.FirstName)
                                              .ThenBy(x => x.LastName)
                                              .Select(x => x.UserId)
                                              .ToList();


                        for (int i = 0; i < ids.Count(); i++)
                        {
                            foreach (ServiceRequest s in services)
                            {
                                if(s.ServiceProviderId!=null)
                                {
                                if (s.ServiceProviderId == ids[i])
                                {
                                    temp.Add(s);
                                }
                                }
                            }
                        }

                        services = temp;

                    }
                    else
                    {
                        services = services.
                            OrderBy(x => x.ServiceProviderId)
                                                    .ToList();

                        List<ServiceRequest> temp = new List<ServiceRequest>();

                        List<int> ids = _context.Users.OrderByDescending(x => x.FirstName)
                                              .OrderByDescending(x => x.LastName)
                                              .Select(x => x.UserId)
                                              .ToList();


                        for (int i = 0; i < ids.Count(); i++)
                        {
                            foreach (ServiceRequest s in services)
                            {
                                if (s.ServiceProviderId != null)
                                {
                                    if (s.ServiceProviderId == ids[i])
                                    {
                                        temp.Add(s);
                                    }
                                }
                            }
                        }

                        foreach (ServiceRequest s in services)
                        {
                            if (s.ServiceProviderId == null)
                            {
                                temp.Add(s);
                            }
                        }

                        services = temp;
                    }
                    break;

                case "Amount":

                    if (sortOrder == "Asc")
                    {
                        services = services.OrderBy(x => x.TotalCost).ToList();
                    }
                    else
                    {
                        services = services.OrderByDescending(x => x.TotalCost).ToList();
                    }
                    break;

                case "Status":

                    if (sortOrder == "Asc")
                    {
                        //services = services.OrderBy(x => x.Status).ToList();
                        List<ServiceRequest> temp = new List<ServiceRequest>();

                        for (int i = 1; i <= 8; i++)
                        {
                            foreach (ServiceRequest s in services)
                            {
                                if (i == 1 && s.Status == 1 && s.ServiceStartDate > DateTime.Now)
                                {
                                    if (s.Status == 1 && s.ServiceStartDate > DateTime.Now)
                                    {
                                        temp.Add(s);
                                    }
                                }
                                else if (i == 2 && s.Status == 2 && s.ServiceStartDate > DateTime.Now)
                                {
                                    if (s.Status == 2 && s.ServiceStartDate > DateTime.Now)
                                    {
                                        temp.Add(s);
                                    }
                                }
                                else if (i == 5 && s.Status == 5 && s.ServiceStartDate > DateTime.Now)
                                {
                                    if (s.Status == 5 && s.ServiceStartDate > DateTime.Now)
                                    {
                                        temp.Add(s);
                                    }
                                }
                                else if (i == 3 && s.Status == 3)
                                {
                                    temp.Add(s);
                                }
                                else if (i == 4 && s.Status == 4)
                                {
                                    temp.Add(s);
                                }
                                else if (i == 6 && s.Status == 6)
                                {
                                    temp.Add(s);
                                }
                                else if (i == 7 && s.Status == 7)
                                {
                                    temp.Add(s);

                                }
                                else if (i == 8)
                                {
                                    if ((s.Status == 1 || s.Status == 2 || s.Status == 5) && s.ServiceStartDate < DateTime.Now)
                                    {
                                        temp.Add(s);
                                    }
                                }
                            }
                        }

                        services = temp;


                    }
                    else
                    {
                        //services = services.OrderByDescending(x => x.Status).ToList();

                        List<ServiceRequest> temp = new List<ServiceRequest>();

                        for (int i = 8; i >= 1; i--)
                        {
                            foreach (ServiceRequest s in services)
                            {
                                if (i == 1)
                                {
                                    if (s.Status == 1 && s.ServiceStartDate > DateTime.Now)
                                    {
                                        temp.Add(s);
                                    }
                                }
                                else if (i == 2)
                                {
                                    if (s.Status == 2 && s.ServiceStartDate > DateTime.Now)
                                    {
                                        temp.Add(s);
                                    }
                                }
                                else if (i == 5)
                                {
                                    if (s.Status == 5 && s.ServiceStartDate > DateTime.Now)
                                    {
                                        temp.Add(s);
                                    }
                                }
                                else if (i == 3 && s.Status == 3)
                                {
                                    temp.Add(s);
                                }
                                else if (i == 4 && s.Status == 4)
                                {
                                    temp.Add(s);
                                }
                                else if (i == 6 && s.Status == 6)
                                {
                                    temp.Add(s);
                                }
                                else if (i == 7 && s.Status == 7)
                                {
                                    temp.Add(s);

                                }
                                else if (i == 8)
                                {
                                    if ((s.Status == 1 || s.Status == 2 || s.Status == 5) && s.ServiceStartDate < DateTime.Now)
                                    {
                                        temp.Add(s);
                                    }
                                }
                            }
                        }

                        services = temp;

                    }
                    break;


                default:
                    services = services.OrderBy(x => x.ServiceRequestId).ToList();
                    break;
            }

            
            List<ServiceRequest> finalServices = new List<ServiceRequest>();

            if (GetService.ServiceRequestId != null)
            {
                services = services.Where(x => x.ServiceRequestId.Equals(GetService.ServiceRequestId)).ToList();
            }

            if (GetService.PostalCode != null)
            {
                services = services.Where(x => x.ZipCode.Contains(GetService.PostalCode)).ToList();
            }

            if (GetService.CustomerId != 0)
            {
                services = services.Where(x => x.UserId.Equals(GetService.CustomerId)).ToList();
            }

            if (GetService.ServiceProviderId != 0)
            {
                services = services.Where(x => x.ServiceProviderId.Equals(GetService.ServiceProviderId)).ToList();
            }

            if (GetService.Status != 0)
            {
                if (GetService.Status == 8)
                {
                    services = services.Where(x => (x.Status.Equals(1) || x.Status.Equals(2) || x.Status.Equals(5)) && x.ServiceStartDate < DateTime.Now).ToList();
                }
                else if (GetService.Status == 1 || GetService.Status == 2 || GetService.Status == 5)
                {
                    services = services.Where(x => x.Status.Equals(GetService.Status) && x.ServiceStartDate > DateTime.Now).ToList();
                }
                else
                {
                    services = services.Where(x => x.Status.Equals(GetService.Status)).ToList();

                }
            }

            if (GetService.HasIssue == true)
            {
                services = services.Where(x => x.HasIssue.Equals(GetService.HasIssue)).ToList();
            }

            if (GetService.FromDate != null)
            {
                services = services.Where(x => x.ServiceStartDate >= GetService.FromDate).ToList();
            }

            if (GetService.ToDate != null)
            {
                services = services.Where(x => x.ServiceStartDate <= GetService.ToDate).ToList();
            }

            if (GetService.Email != null)
            {
                List<string> emails = new List<string>();

                foreach (ServiceRequest s in services)
                {
                    var email = _context.Users.Where(x => x.UserId.Equals(s.UserId)).Select(x => x.Email).FirstOrDefault();
                    emails.Add(email);
                }

                int i = 0;
                foreach (string e in emails)
                {
                    if (e.Contains(GetService.Email))
                    {
                        finalServices.Add(services[i]);
                    }
                    i++;
                }

                services = finalServices;
            }

            PagedResult<ServiceRequest> result = new PagedResult<ServiceRequest>
            {
                Data = services.Skip(ExcludeRecords).Take(pageSize).ToList(),
                TotalItems = services.ToList().Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            services = services.Skip(ExcludeRecords).Take(pageSize).ToList();

            List<User> users = new List<User>();
            List<User> sp = new List<User>();
            List<ServiceRequestAddress> addresses = new List<ServiceRequestAddress>();

            foreach (ServiceRequest service in services)
            {
                User temp = _context.Users.Find(service.UserId);
                users.Add(temp);

                ServiceRequestAddress add = _context.ServiceRequestAddresses.Where(x => x.ServiceRequestId.Equals(service.ServiceRequestId)).FirstOrDefault();
                addresses.Add(add);

                if (service.ServiceProviderId != null)
                {
                    User user = _context.Users.Where(x => x.UserId.Equals(service.ServiceProviderId)).FirstOrDefault();
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

            TempData["users"]  = users;
            TempData["addresses"] = addresses;
            TempData["sp"] = sp;
            TempData["ratings"] = ratings;

            return result;
        }

        public PagedResult<ServiceRequest> GetDefaultServiceManage(List<ServiceRequest> services, int pageNumber = 1, int pageSize = 5, string sortBy = "ServiceId", string sortOrder = "Asc")
        {
            HttpContext.Session.SetInt32("pageNumber", pageNumber);
            HttpContext.Session.SetInt32("pageSize", pageSize);
            HttpContext.Session.SetString("sortBy", sortBy);
            HttpContext.Session.SetString("sortOrder", sortOrder);

            switch (sortBy)
            {
                case "ServiceId":


                    if (sortOrder == "Asc")
                    {
                        services = services.OrderBy(x => x.ServiceRequestId).ToList();
                    }
                    else
                    {
                        services = services.OrderByDescending(x => x.ServiceRequestId).ToList();
                    }

                    break;

                case "Date":


                    if (sortOrder == "Asc")
                    {
                        services = services.OrderBy(x => x.ServiceStartDate).ToList();
                    }
                    else
                    {
                        services = services.OrderByDescending(x => x.ServiceStartDate).ToList();
                    }

                    break;

                case "UserId":

                    if (sortOrder == "Asc")
                    {
                        List<int> ids = _context.Users.OrderBy(x => x.FirstName)
                                                .ThenBy(x => x.LastName)
                                                .Select(x => x.UserId)
                                                .ToList();

                        List<ServiceRequest> temp = new List<ServiceRequest>();

                        for (int i = 0; i < ids.Count(); i++)
                        {
                            foreach (ServiceRequest s in services)
                            {
                                if (s.UserId == ids[i])
                                {
                                    temp.Add(s);
                                }
                            }
                        }
                        services = temp;
                    }
                    else
                    {
                        List<int> ids = _context.Users.OrderByDescending(x => x.FirstName)
                                                .ThenByDescending(x => x.LastName)
                                                .Select(x => x.UserId)
                                                .ToList();

                        List<ServiceRequest> temp = new List<ServiceRequest>();

                        for (int i = 0; i < ids.Count(); i++)
                        {
                            foreach (ServiceRequest s in services)
                            {
                                if (s.UserId == ids[i])
                                {
                                    temp.Add(s);
                                }
                            }
                        }
                        services = temp;
                    
                    }
                    break;

                case "Sps":
                    if (sortOrder == "Asc")
                    {
                        services = services.OrderBy(x => x.ServiceProviderId)
                                                    .ToList();

                        List<ServiceRequest> temp = new List<ServiceRequest>();

                        foreach (ServiceRequest s in services)
                        {
                            if (s.ServiceProviderId == null)
                            {
                                temp.Add(s);
                            }
                        }


                        List<int> ids = _context.Users.OrderBy(x => x.FirstName)
                                              .ThenBy(x => x.LastName)
                                              .Select(x => x.UserId)
                                              .ToList();


                        for (int i = 0; i < ids.Count(); i++)
                        {
                            foreach (ServiceRequest s in services)
                            {
                                if (s.ServiceProviderId != null)
                                {
                                    if (s.ServiceProviderId == ids[i])
                                    {
                                        temp.Add(s);
                                    }
                                }
                            }
                        }

                        services = temp;

                    }
                    else
                    {
                        services = services.
                            OrderBy(x => x.ServiceProviderId)
                                                    .ToList();

                        List<ServiceRequest> temp = new List<ServiceRequest>();

                        List<int> ids = _context.Users.OrderByDescending(x => x.FirstName)
                                              .OrderByDescending(x => x.LastName)
                                              .Select(x => x.UserId)
                                              .ToList();


                        for (int i = 0; i < ids.Count(); i++)
                        {
                            foreach (ServiceRequest s in services)
                            {
                                if (s.ServiceProviderId != null)
                                {
                                    if (s.ServiceProviderId == ids[i])
                                    {
                                        temp.Add(s);
                                    }
                                }
                            }
                        }

                        foreach (ServiceRequest s in services)
                        {
                            if (s.ServiceProviderId == null)
                            {
                                temp.Add(s);
                            }
                        }

                        services = temp;
                    }
                    break;

                case "Amount":

                    if (sortOrder == "Asc")
                    {
                        services = services.OrderBy(x => x.TotalCost).ToList();
                    }
                    else
                    {
                        services = services.OrderByDescending(x => x.TotalCost).ToList();
                    }
                    break;

                case "Status":

                    if (sortOrder == "Asc")
                    {
                        //services = services.OrderBy(x => x.Status).ToList();
                        List<ServiceRequest> temp = new List<ServiceRequest>();

                        for(int i=1;i<=8;i++)
                        {
                            foreach (ServiceRequest s in services)
                            {
                                if (i == 1 && s.Status == 1 && s.ServiceStartDate > DateTime.Now)
                                {
                                    if (s.Status == 1 && s.ServiceStartDate > DateTime.Now)
                                    {
                                        temp.Add(s);
                                    }
                                }
                                else if (i == 2 && s.Status == 2 && s.ServiceStartDate > DateTime.Now)
                                {
                                    if (s.Status == 2 && s.ServiceStartDate > DateTime.Now)
                                    {
                                        temp.Add(s);
                                    }
                                }
                                else if (i == 5 && s.Status == 5 && s.ServiceStartDate > DateTime.Now)
                                {
                                    if (s.Status == 5 && s.ServiceStartDate > DateTime.Now)
                                    {
                                        temp.Add(s);
                                    }
                                }
                                else if (i == 3 && s.Status==3)
                                {
                                   temp.Add(s);
                                }
                                else if (i == 4 && s.Status == 4)
                                {
                                    temp.Add(s);
                                }
                                else if (i == 6 && s.Status == 6)
                                {
                                    temp.Add(s);
                                }
                                else if (i == 7 && s.Status == 7)
                                {
                                    temp.Add(s);

                                }
                                else if (i == 8)
                                {
                                    if ((s.Status == 1 || s.Status==2 || s.Status==5) && s.ServiceStartDate < DateTime.Now)
                                    {
                                        temp.Add(s);
                                    }
                                }
                            }
                        }

                        services = temp;


                    }
                    else
                    {
                        //services = services.OrderByDescending(x => x.Status).ToList();

                        List<ServiceRequest> temp = new List<ServiceRequest>();

                        for (int i = 8; i >= 1; i--)
                        {
                            foreach (ServiceRequest s in services)
                            {
                                if (i == 1)
                                {
                                    if (s.Status == 1 && s.ServiceStartDate > DateTime.Now)
                                    {
                                        temp.Add(s);
                                    }
                                }
                                else if (i == 2)
                                {
                                    if (s.Status == 2 && s.ServiceStartDate > DateTime.Now)
                                    {
                                        temp.Add(s);
                                    }
                                }
                                else if (i == 5)
                                {
                                    if (s.Status == 5 && s.ServiceStartDate > DateTime.Now)
                                    {
                                        temp.Add(s);
                                    }
                                }
                                else if (i == 3 && s.Status == 3)
                                {
                                    temp.Add(s);
                                }
                                else if (i == 4 && s.Status == 4)
                                {
                                    temp.Add(s);
                                }
                                else if (i == 6 && s.Status == 6)
                                {
                                    temp.Add(s);
                                }
                                else if (i == 7 && s.Status == 7)
                                {
                                    temp.Add(s);

                                }
                                else if (i == 8)
                                {
                                    if ((s.Status == 1 || s.Status == 2 || s.Status == 5) && s.ServiceStartDate < DateTime.Now)
                                    {
                                        temp.Add(s);
                                    }
                                }
                            }
                        }

                        services = temp;

                    }
                    break;

                default:
                    services = services.OrderBy(x => x.ServiceRequestId).ToList();
                    break;
            }


            int ExcludeRecords = (pageSize * pageNumber) - pageSize;


            PagedResult<ServiceRequest> result = new PagedResult<ServiceRequest>
            {
                TotalItems = services.Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };

             services = services.Skip(ExcludeRecords)
                                             .Take(pageSize)
                                              .ToList();

            result.Data = services;

            List<User> users = new List<User>();
            List<User> sp = new List<User>();
            List<ServiceRequestAddress> addresses = new List<ServiceRequestAddress>();

            foreach (ServiceRequest service in services)
            {
                User temp = _context.Users.Find(service.UserId);
                users.Add(temp);

                ServiceRequestAddress add = _context.ServiceRequestAddresses.Where(x => x.ServiceRequestId.Equals(service.ServiceRequestId)).FirstOrDefault();
                addresses.Add(add);

                if (service.ServiceProviderId != null)
                {
                    User user = _context.Users.Where(x => x.UserId.Equals(service.ServiceProviderId)).FirstOrDefault();
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

            TempData["users"] = users;
            TempData["addresses"] = addresses;
            TempData["sp"] = sp;
            TempData["ratings"] = ratings;

            return result;
        }

        public IActionResult DefaultServiceManage(int pageNumber = 1, int pageSize = 5, string sortBy = "ServiceId", string sortOrder = "Asc")
        {

            PagedResult<ServiceRequest> result = GetDefaultServiceManage(pageNumber, pageSize, sortBy, sortOrder);
            
            ViewBag.sortBy = sortBy;
            switch (sortBy)
            {
                case "ServiceId":
                    ViewBag.ServiceId = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Date":
                    ViewBag.Date = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "UserId":
                    ViewBag.UserId = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Sps":
                    ViewBag.Sps = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Amount":
                    ViewBag.Amount = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Status":
                    ViewBag.Status = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;

                default:
                    ViewBag.ServiceRequestIdsortOrder = "Desc";
                    break;
            }

            ViewBag.users = TempData["users"];
            ViewBag.addresses = TempData["addresses"];
            ViewBag.sp = TempData["sp"];
            ViewBag.ratings = TempData["ratings"];

            return PartialView("_ServiceResultPartial", result);
        }
        
        [HttpPost]
        public IActionResult FilterService(FilterServices GetService)
        {
            List<ServiceRequest> services = _context.ServiceRequests.ToList();
            List<ServiceRequest> finalServices = new List<ServiceRequest>();

            if (GetService.ServiceRequestId!=null)
            {
             services = services.Where(x => x.ServiceRequestId.Equals(GetService.ServiceRequestId)).ToList();
            }

            if(GetService.PostalCode!=null)
            {
                services = services.Where(x => x.ZipCode.Contains(GetService.PostalCode)).ToList();
            }

            if (GetService.CustomerId!=0)
            {
                services = services.Where(x => x.UserId.Equals(GetService.CustomerId)).ToList();
            }

            if (GetService.ServiceProviderId != 0)
            {
                services = services.Where(x => x.ServiceProviderId.Equals(GetService.ServiceProviderId)).ToList();
            }

            if (GetService.Status != 0)
            {
                if(GetService.Status==8)
                {
                    services = services.Where(x => (x.Status.Equals(1)||x.Status.Equals(2) || x.Status.Equals(5)) && x.ServiceStartDate<DateTime.Now).ToList();
                }
                else if (GetService.Status == 1|| GetService.Status == 2 || GetService.Status == 5)
                {
                    services = services.Where(x => x.Status.Equals(GetService.Status) && x.ServiceStartDate > DateTime.Now).ToList();
                }
                else
                {
                services = services.Where(x => x.Status.Equals(GetService.Status)).ToList();

                }
            }

            if (GetService.HasIssue == true)
            {
                services = services.Where(x => x.HasIssue.Equals(GetService.HasIssue)).ToList();
            }

            if (GetService.FromDate != null)
            {
                services = services.Where(x => x.ServiceStartDate>=GetService.FromDate).ToList();
            }

            if (GetService.ToDate != null)
            {
                if (GetService.ToDate < GetService.FromDate)
                {
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return Json(new { message = "To Date must be Greater than From Date" });
                }

                services = services.Where(x => x.ServiceStartDate <= GetService.ToDate).ToList();
            }

            if (GetService.Email != null)
            {
                List<string> emails = new List<string>();

                foreach(ServiceRequest s in services)
                {
                    var email = _context.Users.Where(x => x.UserId.Equals(s.UserId)).Select(x => x.Email).FirstOrDefault();
                    emails.Add(email);
                }

                int i = 0;
                foreach(string e in emails)
                {
                    if (e.Contains(GetService.Email))
                    {
                        finalServices.Add(services[i]);
                    }
                    i++;
                }

                services = finalServices;
            }

            int pageNumber = (int)HttpContext.Session.GetInt32("pageNumber");
            int pageSize = (int)HttpContext.Session.GetInt32("pageSize");
            string sortBy = HttpContext.Session.GetString("sortBy");
            string sortOrder = HttpContext.Session.GetString("sortOrder");

            PagedResult<ServiceRequest> result = GetDefaultServiceManage(services,pageNumber,pageSize,sortBy,sortOrder);

            HttpContext.Session.SetString("GetService", JsonConvert.SerializeObject(GetService));

            ViewBag.users = TempData["users"];
            ViewBag.addresses = TempData["addresses"];
            ViewBag.sp = TempData["sp"];
            ViewBag.ratings = TempData["ratings"];


            ViewBag.sortBy = sortBy;
            switch (sortBy)
            {
                case "ServiceId":
                    ViewBag.ServiceId = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Date":
                    ViewBag.Date = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "UserId":
                    ViewBag.UserId = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Sps":
                    ViewBag.Sps = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Amount":
                    ViewBag.Amount = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Status":
                    ViewBag.Status = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;

                default:
                    ViewBag.ServiceRequestIdsortOrder = "Desc";
                    break;
            }

            return PartialView("_ServiceResultPartial", result);
        }

        public IActionResult GetServiceDetails(int serviceRequestId)
        {
            ServiceRequest service = _context.ServiceRequests.Find(serviceRequestId);

            var startDateTime = service.ServiceStartDate;
            var date = startDateTime.ToString("dd/MM/yyyy").Replace("-", "/");
            service.StartDate = Convert.ToDateTime(date);
            service.TimeStart = startDateTime.ToString("HH:mm");
            
            double totalhour = Convert.ToDouble(service.ServiceHours + service.ExtraHours);

           
            ViewBag.totalhour = totalhour;

            ServiceRequestAddress serviceAddress = _context.ServiceRequestAddresses.Where(x => x.ServiceRequestId.Equals(serviceRequestId)).FirstOrDefault();

            ServiceRequestExtra serviceExtra = _context.ServiceRequestExtras.Where(x => x.ServiceRequestId.Equals(serviceRequestId)).FirstOrDefault();

            ServiceAllDetails serviceRequest = new ServiceAllDetails {              
                
                Service = service,
                ServiceAddress =serviceAddress,
                ServiceExtra = serviceExtra
                };

            return PartialView("_ModalPartial", serviceRequest);
        }

        public void SendMail(ServiceRequest sr, ServiceRequestAddress srAddress,User customer,User sp=null)
        {
            
                var name = customer.FirstName + " " + customer.LastName;
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Helperland", "helperlandindia@gmail.com"));
                message.To.Add(new MailboxAddress(name, customer.Email));
                message.Subject = " Regarding Service Request Id = " + sr.ServiceRequestId;
                message.Body = new TextPart("plain")
                {
                    Text = "\nHello "+name+
                    "\nService Request id:" +sr.ServiceRequestId + " has been Rescheduled by Admin for you." +
                    "\nNew Date Time:"+ sr.ServiceStartDate.ToString("dd/MM/yyyy HH:mm:ss")+
                    "\nAddress: "+ srAddress.AddressLine1+" "+ srAddress.AddressLine2+", "+ srAddress.PostalCode+" "+ srAddress.City+" "+srAddress.State+
                    "\n\nThankyou"
                };
                using var client = new SmtpClient();
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("helperlandindia@gmail.com", "Helperland@123");
                client.Send(message);
                client.Disconnect(true);

            if (sp != null)
            {
                var  spname = sp.FirstName + " " + sp.LastName;
                message = new MimeMessage();
                message.From.Add(new MailboxAddress("Helperland", "helperlandindia@gmail.com"));
                message.To.Add(new MailboxAddress(name, sp.Email));
                message.Subject = " Regarding Service Request Id = " + sr.ServiceRequestId;
                message.Body = new TextPart("plain")
                {
                    Text = "\nHello " + spname +
                    "\nService Request id:" + sr.ServiceRequestId + " has been Rescheduled by Admin for Customer." +
                    "\nNew Date Time:" + sr.ServiceStartDate.ToString("dd/MM/yyyy HH:mm:ss") +
                    "\nCustomer: "+ name +
                    "\nAddress: " + srAddress.AddressLine1 + " " + srAddress.AddressLine2 + ", " + srAddress.PostalCode + " " + srAddress.City + " " + srAddress.State +
                    "\nContact Details: +49" + srAddress.Mobile + " " + srAddress.Email+
                    "\n\nThankyou"
                };
                using var clients = new SmtpClient();
                clients.Connect("smtp.gmail.com", 587, false);
                clients.Authenticate("helperlandindia@gmail.com", "Helperland@123");
                clients.Send(message);
                clients.Disconnect(true);
            }
            
        }

        public void SendCancelMail(ServiceRequest sr, User customer)
        {
            var name = customer.FirstName + " " + customer.LastName;
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Helperland", "helperlandindia@gmail.com"));
            message.To.Add(new MailboxAddress(name, customer.Email));
            message.Subject = " Regarding Service Request Id = " + sr.ServiceRequestId;
            message.Body = new TextPart("plain")
            {
                Text = "\nHello " + name +
                "\nService Request id:" + sr.ServiceRequestId + " has been Cancel by Admin for you." +
                "\n\nThankyou"
            };
            using var client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("helperlandindia@gmail.com", "Helperland@123");
            client.Send(message);
            client.Disconnect(true);

        }
        
        [HttpPost]
        public IActionResult Reschdule(ServiceAllDetails serviceRequest)
        {

            ServiceRequest sr = _context.ServiceRequests.Where(x =>
                    x.ServiceRequestId.Equals(serviceRequest.Service.ServiceRequestId)).FirstOrDefault();

            string date = serviceRequest.Service.StartDate.ToString("yyyy-MM-dd");
            string time = serviceRequest.Service.TimeStart;
            DateTime startDateTime = Convert.ToDateTime(date).Add(TimeSpan.Parse(time));
            sr.ZipCode = serviceRequest.ServiceAddress.PostalCode;
            sr.Comments = serviceRequest.Service.Comments;
            sr.ServiceStartDate = startDateTime;
            sr.Status = 5;
            sr.ModifiedDate = DateTime.Now;

            this._context.ServiceRequests.Update(sr);

            ServiceRequestAddress srAddress = _context.ServiceRequestAddresses.Where(x =>
                    x.ServiceRequestId.Equals(serviceRequest.Service.ServiceRequestId)).FirstOrDefault();

            srAddress.AddressLine1 = serviceRequest.ServiceAddress.AddressLine1;
            srAddress.AddressLine2 = serviceRequest.ServiceAddress.AddressLine2;
            srAddress.PostalCode = serviceRequest.ServiceAddress.PostalCode;
            srAddress.City = serviceRequest.ServiceAddress.City;

            this._context.ServiceRequestAddresses.Update(srAddress);

            this._context.SaveChanges();

            User Customer = _context.Users.Find(sr.UserId);

            User Sp = new User();
            if (sr.ServiceProviderId != null)
            {
                Sp = _context.Users.Find(sr.ServiceProviderId);
                SendMail(sr, srAddress, Customer, Sp);
            }
            else
            {
                SendMail(sr, srAddress, Customer);
            }



            int pageNumber = (int)HttpContext.Session.GetInt32("pageNumber");
            int pageSize = (int)HttpContext.Session.GetInt32("pageSize");
            string sortBy = HttpContext.Session.GetString("sortBy");
            string sortOrder = HttpContext.Session.GetString("sortOrder");

            PagedResult<ServiceRequest> result = GetDefaultServiceManage(pageNumber, pageSize, sortBy, sortOrder);

            ViewBag.users = TempData["users"];
            ViewBag.addresses = TempData["addresses"];
            ViewBag.sp = TempData["sp"];
            ViewBag.ratings = TempData["ratings"];


            ViewBag.sortBy = sortBy;
            switch (sortBy)
            {
                case "ServiceId":
                    ViewBag.ServiceId = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Date":
                    ViewBag.Date = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "UserId":
                    ViewBag.UserId = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Sps":
                    ViewBag.Sps = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Amount":
                    ViewBag.Amount = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Status":
                    ViewBag.Status = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;

                default:
                    ViewBag.ServiceRequestIdsortOrder = "Desc";
                    break;
            }


            return PartialView("_ServiceResultPartial", result);

        }


        [HttpPost]
        public IActionResult Cancel(int id, int pageNumber = 1, int pageSize = 5)
        {

            ServiceRequest sr = _context.ServiceRequests.Find(id);
            sr.Status = 4;
            sr.ModifiedDate = DateTime.Now;

            this._context.ServiceRequests.Update(sr);
            this._context.SaveChanges();

            User Customer = _context.Users.Find(sr.UserId);

            SendCancelMail(sr, Customer);

          
            string sortBy = HttpContext.Session.GetString("sortBy");
            string sortOrder = HttpContext.Session.GetString("sortOrder");

            PagedResult<ServiceRequest> result = GetDefaultServiceManage(pageNumber, pageSize, sortBy, sortOrder);

            ViewBag.users = TempData["users"];
            ViewBag.addresses = TempData["addresses"];
            ViewBag.sp = TempData["sp"];
            ViewBag.ratings = TempData["ratings"];


            ViewBag.sortBy = sortBy;
            switch (sortBy)
            {
                case "ServiceId":
                    ViewBag.ServiceId = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Date":
                    ViewBag.Date = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "UserId":
                    ViewBag.UserId = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Sps":
                    ViewBag.Sps = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Amount":
                    ViewBag.Amount = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Status":
                    ViewBag.Status = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;

                default:
                    ViewBag.ServiceRequestIdsortOrder = "Desc";
                    break;
            }

            return PartialView("_ServiceResultPartial", result);

        }

        public IActionResult Refund(ServiceAllDetails serviceRequest)
        {
            ServiceRequest service = _context.ServiceRequests.Find(serviceRequest.Service.ServiceRequestId);
            service.Comments = serviceRequest.Service.Comments;
            if (service.RefundedAmount == null)
            {
                service.RefundedAmount = serviceRequest.Service.RefundedAmount;

            }
            else
            {
                service.RefundedAmount += serviceRequest.Service.RefundedAmount;
            }
            service.Status = 6;
            service.ModifiedDate = DateTime.Now;

            this._context.ServiceRequests.Update(service);
            this._context.SaveChanges();

            int pageNumber = (int)HttpContext.Session.GetInt32("pageNumber");
            int pageSize = (int)HttpContext.Session.GetInt32("pageSize");
            string sortBy = HttpContext.Session.GetString("sortBy");
            string sortOrder = HttpContext.Session.GetString("sortOrder");

            PagedResult<ServiceRequest> result = GetDefaultServiceManage(pageNumber, pageSize, sortBy, sortOrder);

            ViewBag.users = TempData["users"];
            ViewBag.addresses = TempData["addresses"];
            ViewBag.sp = TempData["sp"];
            ViewBag.ratings = TempData["ratings"];


            ViewBag.sortBy = sortBy;
            switch (sortBy)
            {
                case "ServiceId":
                    ViewBag.ServiceId = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Date":
                    ViewBag.Date = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "UserId":
                    ViewBag.UserId = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Sps":
                    ViewBag.Sps = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Amount":
                    ViewBag.Amount = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Status":
                    ViewBag.Status = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;

                default:
                    ViewBag.ServiceRequestIdsortOrder = "Desc";
                    break;
            }

            return PartialView("_ServiceResultPartial", result);
        }







        public IActionResult GetUserManage()
        {
            int id = Convert.ToInt32(HttpContext.Session.GetString("UserId"));

            List<SelectListItem> userName = new List<SelectListItem>
            {
                new SelectListItem { Text = "User Name", Value = "0" }
            };

            List<User> allUsers = _context.Users.Where(x => x.UserId != id).ToList();

            foreach (User a in allUsers)
            { 
                userName.Add(new SelectListItem { Text = a.FirstName + " " + a.LastName, Value = a.UserId.ToString() });   
            }

            ViewBag.userName = userName;

            FilterServices GetService = new FilterServices();
            HttpContext.Session.SetString("GetService", JsonConvert.SerializeObject(GetService));

            return PartialView("_UserManagmentPartial");
        }


        public PagedResult<User> GetDefaultUserManage(int pageNumber = 1, int pageSize = 5, string sortBy = "Name", string sortOrder = "Asc")
        {
            int id = Convert.ToInt32(HttpContext.Session.GetString("UserId"));

            HttpContext.Session.SetInt32("pageNumber", pageNumber);
            HttpContext.Session.SetInt32("pageSize", pageSize);
            HttpContext.Session.SetString("sortBy", sortBy);
            HttpContext.Session.SetString("sortOrder", sortOrder);

            var value = HttpContext.Session.GetString("GetService");
            FilterServices GetUser = JsonConvert.DeserializeObject<FilterServices>(value);

            int ExcludeRecords = (pageSize * pageNumber) - pageSize;

            List<User> users = new List<User>();
             users = _context.Users.Where(x=>x.UserId!=id).ToList();

            foreach (User user in users)
            {
                if (user.UserTypeId == 1)
                {
                    UserAddress address = _context.UserAddresses.Where(x => x.UserId.Equals(user.UserId))
                        .OrderBy(x => x.AddressId)
                        .LastOrDefault();

                    if (address != null)
                    {
                        user.ZipCode = address.PostalCode;
                    }
                }
            }


            switch (sortBy)
            {
                case "Name":


                    if (sortOrder == "Asc")
                    {
                        users = users.OrderBy(x=>x.FirstName)
                                                .ThenBy(x=>x.LastName)
                                                    .ToList();
                    }
                    else
                    {
                        users = users.OrderByDescending(x => x.FirstName)
                                               .ThenByDescending(x => x.LastName)
                                                   .ToList();
                    }


                    break;

                case "UserTypeId":


                    if (sortOrder == "Asc")
                    {
                        users = users.OrderBy(x =>x.UserTypeId)
                                                    .ToList();
                    }
                    else
                    {
                        users = users.OrderByDescending(x => x.UserTypeId)
                                                     .ToList();
                    }


                    break;

                case "DOR":

                    if (sortOrder == "Asc")
                    {
                        users = users.OrderBy(x => x.CreatedDate)
                                                    .ToList();
                    }
                    else
                    {
                        users = users.OrderByDescending(x => x.CreatedDate)
                                                     .ToList();
                    }
                    break;

                case "Phone":



                    if (sortOrder == "Asc")
                    {
                        users = users.OrderBy(x => x.Mobile)
                                                    .ToList();
                    }
                    else
                    {
                        users = users.OrderByDescending(x => x.Mobile)
                                                   .ToList();
                    }
                    break;

                case "Postal":

                    if (sortOrder == "Asc")
                    {
                        users = users.OrderBy(x => x.ZipCode).ToList();
                    }
                    else
                    {
                        users = users.OrderByDescending(x => x.ZipCode).ToList();
                    }
                    break;

                case "Status":

                    if (sortOrder == "Asc")
                    {
                        users = users.OrderBy(x => x.IsActive).ToList();
                    }
                    else
                    {
                        users = users.OrderByDescending(x => x.IsActive).ToList();
                    }
                    break;

                default:
                    users = users.OrderBy(x => x.FirstName)
                                   .ThenBy(x => x.LastName)
                                    .ToList();
                    break;
            }


            if (GetUser.UserId != 0)
            {
                users = users.Where(x => x.UserId.Equals(GetUser.UserId)).ToList();
            }

            if (GetUser.UserTypeId != 0)
            {
                users = users.Where(x => x.UserTypeId.Equals(GetUser.UserTypeId)).ToList();
            }

            if (GetUser.Mobile != null)
            {
                users = users.Where(x => x.Mobile.Contains(GetUser.Mobile)).ToList();
            }

            if (GetUser.PostalCode != null)
            {
                List<User> newUsers = new List<User>();

                foreach (User u in users)
                {
                    if (u.UserTypeId == 2 && u.ZipCode != null && u.ZipCode.Contains(GetUser.PostalCode))
                    {
                        newUsers.Add(u);
                    }
                    else if (u.UserTypeId == 1)
                    {
                        UserAddress address = _context.UserAddresses.Where(x => x.UserId.Equals(u.UserId))
                        .OrderBy(x => x.AddressId)
                        .LastOrDefault();

                        if (address != null && address.PostalCode.Contains(GetUser.PostalCode))
                        {
                            newUsers.Add(u);
                        }

                    }
                }
                //users = users.Where(x => x.ZipCode.Equals(GetUser.PostalCode)).ToList();
                users = newUsers;
            }

            if (GetUser.Email != null)
            {
                users = users.Where(x => x.Email.Contains(GetUser.Email)).ToList();
            }

            if (GetUser.FromDate != null)
            {
                users = users.Where(x => x.CreatedDate >= GetUser.FromDate).ToList();
            }

            if (GetUser.ToDate != null)
            {
                

                users = users.Where(x => x.CreatedDate <= GetUser.ToDate).ToList();
            }

            PagedResult<User> result = new PagedResult<User>
            {
                Data = users.Skip(ExcludeRecords).Take(pageSize).ToList(),
                TotalItems = users.ToList().Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            users = users.Skip(ExcludeRecords).Take(pageSize).ToList();

            List<string> cities = new List<string>();

            foreach (User user in users)
            {
                if (user.ZipCode != null)
                {
                    string zip = user.ZipCode;
                    var cityId = _context.Zipcodes.Where(x => x.ZipcodeValue == zip).Select(x => x.CityId).FirstOrDefault();

                    string city = _context.Cities.Where(x => x.Id.Equals(cityId)).Select(x => x.CityName).FirstOrDefault();

                    cities.Add(city);

                }
                //else if (user.UserTypeId == 1)
                //{
                //    UserAddress address = _context.UserAddresses.Where(x => x.UserId.Equals(user.UserId))
                //        .OrderBy(x => x.AddressId)
                //        .LastOrDefault();

                //    if (address == null)
                //    {

                //        cities.Add(null);
                //    }
                //    else
                //    {
                //        user.ZipCode = address.PostalCode;
                //        cities.Add(address.City);
                //    }
                //}
                else
                {
                    cities.Add(null);
                }
            }

            TempData["cities"] = cities;

            return result;
        }

        public PagedResult<User> GetDefaultUserManage(List<User> users, int pageNumber = 1, int pageSize = 5)
        {
            HttpContext.Session.SetInt32("pageNumber", pageNumber);
            HttpContext.Session.SetInt32("pageSize", pageSize);

            int ExcludeRecords = (pageSize * pageNumber) - pageSize;

            PagedResult<User> result = new PagedResult<User>
            {
                TotalItems = users.Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            users = users.Skip(ExcludeRecords).Take(pageSize).ToList();

            result.Data = users;

            List<string> cities = new List<string>();

            foreach (User user in users)
            {
                if (user.ZipCode != null)
                {
                    string zip = user.ZipCode;
                    var cityId = _context.Zipcodes.Where(x => x.ZipcodeValue == zip).Select(x => x.CityId).FirstOrDefault();

                    string city = _context.Cities.Where(x => x.Id.Equals(cityId)).Select(x => x.CityName).FirstOrDefault();

                    cities.Add(city);

                }
                //else if (user.UserTypeId == 1)
                //{
                //    UserAddress address = _context.UserAddresses.Where(x => x.UserId.Equals(user.UserId))
                //        .OrderBy(x => x.AddressId)
                //        .LastOrDefault();

                //    if (address == null)
                //    {

                //        cities.Add(null);
                //    }
                //    else
                //    {
                //        user.ZipCode = address.PostalCode;
                //        cities.Add(address.City);
                //    }
                //}
                else
                {
                    cities.Add(null);
                }
            }

            TempData["cities"] = cities;

            return result;
        }

        public IActionResult DefaultUserManage(int pageNumber = 1, int pageSize = 5, string sortBy = "Name", string sortOrder = "Asc")
        {

            PagedResult<User> result = GetDefaultUserManage(pageNumber, pageSize,sortBy,sortOrder);

            ViewBag.sortBy = sortBy;

            switch (sortBy)
            {
                case "Name":
                    ViewBag.Name = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "UserTypeId":
                    ViewBag.UserTypeId = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "DOR":
                    ViewBag.DOR = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Phone":
                    ViewBag.Phone = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Postal":
                    ViewBag.Postal = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Status":
                    ViewBag.Status = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                default:
                    ViewBag.Name = "Desc";
                    break;
            }

            ViewBag.cities = TempData["cities"];
            return PartialView("_UserResultPartial", result);
        }

        [HttpPost]
        public IActionResult FilterUser(FilterServices GetUser)
        {
            int id = Convert.ToInt32(HttpContext.Session.GetString("UserId"));

            List<User> users = _context.Users.Where(x => x.UserId != id).ToList();

            string sortBy = HttpContext.Session.GetString("sortBy");
            string sortOrder = HttpContext.Session.GetString("sortOrder");

            foreach (User user in users)
            {
                if (user.UserTypeId == 1)
                {
                    UserAddress address = _context.UserAddresses.Where(x => x.UserId.Equals(user.UserId))
                        .OrderBy(x => x.AddressId)
                        .LastOrDefault();

                    if (address != null)
                    {
                        user.ZipCode = address.PostalCode;
                    }
                }
            }


            switch (sortBy)
            {
                case "Name":


                    if (sortOrder == "Asc")
                    {
                        users = users.OrderBy(x => x.FirstName)
                                                .ThenBy(x => x.LastName)
                                                    .ToList();
                    }
                    else
                    {
                        users = users.OrderByDescending(x => x.FirstName)
                                               .ThenByDescending(x => x.LastName)
                                                   .ToList();
                    }


                    break;

                case "UserTypeId":


                    if (sortOrder == "Asc")
                    {
                        users = users.OrderBy(x => x.UserTypeId)
                                                    .ToList();
                    }
                    else
                    {
                        users = users.OrderByDescending(x => x.UserTypeId)
                                                     .ToList();
                    }


                    break;

                case "DOR":

                    if (sortOrder == "Asc")
                    {
                        users = users.OrderBy(x => x.CreatedDate)
                                                    .ToList();
                    }
                    else
                    {
                        users = users.OrderByDescending(x => x.CreatedDate)
                                                     .ToList();
                    }
                    break;

                case "Phone":



                    if (sortOrder == "Asc")
                    {
                        users = users.OrderBy(x => x.Mobile)
                                                    .ToList();
                    }
                    else
                    {
                        users = users.OrderByDescending(x => x.Mobile)
                                                   .ToList();
                    }
                    break;

                case "Postal":

                    if (sortOrder == "Asc")
                    {
                        users = users.OrderBy(x => x.ZipCode).ToList();
                    }
                    else
                    {
                        users = users.OrderByDescending(x => x.ZipCode).ToList();
                    }
                    break;

                case "Status":

                    if (sortOrder == "Asc")
                    {
                        users = users.OrderBy(x => x.Status).ToList();
                    }
                    else
                    {
                        users = users.OrderByDescending(x => x.Status).ToList();
                    }
                    break;

                default:
                    users = users.OrderBy(x => x.FirstName)
                                   .ThenBy(x => x.LastName)
                                    .ToList();
                    break;
            }


            if (GetUser.UserId!=0)
            {
                users = users.Where(x => x.UserId.Equals(GetUser.UserId)).ToList();
            }

            if (GetUser.UserTypeId != 0)
            {
                users = users.Where(x => x.UserTypeId.Equals(GetUser.UserTypeId)).ToList();
            }

            if (GetUser.Mobile != null)
            {
                users = users.Where(x => x.Mobile.Contains(GetUser.Mobile)).ToList();
            }

            if (GetUser.PostalCode!= null)
            {
                List<User> newUsers = new List<User>();

                foreach(User u in users)
                {
                    if(u.UserTypeId==2 && u.ZipCode != null && u.ZipCode.Contains(GetUser.PostalCode))
                    {
                        newUsers.Add(u);
                    }
                    else if(u.UserTypeId==1)
                    {
                        UserAddress address = _context.UserAddresses.Where(x => x.UserId.Equals(u.UserId))
                        .OrderBy(x => x.AddressId)
                        .LastOrDefault();

                        if (address != null && address.PostalCode.Contains(GetUser.PostalCode))
                        {
                            newUsers.Add(u);
                        }
                        
                    }
                }
                //users = users.Where(x => x.ZipCode.Equals(GetUser.PostalCode)).ToList();
                users = newUsers;
            }

            if (GetUser.Email !=null)
            {
                users = users.Where(x => x.Email.Contains(GetUser.Email)).ToList();
            }

            if (GetUser.FromDate != null)
            {
                users = users.Where(x => x.CreatedDate>=GetUser.FromDate).ToList();
            }

            if (GetUser.ToDate != null)
            {
                if(GetUser.ToDate < GetUser.FromDate)
                {
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return Json(new { message = "To Date must be Greater than From Date"});
                }

                users = users.Where(x => x.CreatedDate <= GetUser.ToDate).ToList();
            }

            int pageNumber = (int)HttpContext.Session.GetInt32("pageNumber");
            int pageSize = (int)HttpContext.Session.GetInt32("pageSize");

            PagedResult<User> result = GetDefaultUserManage(users, pageNumber, pageSize);

            ViewBag.sortBy = sortBy;

            switch (sortBy)
            {
                case "Name":
                    ViewBag.Name = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "UserTypeId":
                    ViewBag.UserTypeId = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "DOR":
                    ViewBag.DOR = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Phone":
                    ViewBag.Phone = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Postal":
                    ViewBag.Postal = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Status":
                    ViewBag.Status = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                default:
                    ViewBag.Name = "Desc";
                    break;
            }

            HttpContext.Session.SetString("GetService", JsonConvert.SerializeObject(GetUser));

            ViewBag.cities = TempData["cities"];
            return PartialView("_UserResultPartial", result);

        }

        [HttpPost]
        public IActionResult Activate(int userId, int pageNumber = 1, int pageSize = 5)
        {
            User user = _context.Users.Find(userId);
            user.IsActive = true;

            this._context.Users.Update(user);
            this._context.SaveChanges();

            string sortBy = HttpContext.Session.GetString("sortBy");
            string sortOrder = HttpContext.Session.GetString("sortOrder");

            PagedResult<User> result = GetDefaultUserManage(pageNumber, pageSize,sortBy,sortOrder);

            ViewBag.sortBy = sortBy;

            switch (sortBy)
            {
                case "Name":
                    ViewBag.Name = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "UserTypeId":
                    ViewBag.UserTypeId = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "DOR":
                    ViewBag.DOR = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Phone":
                    ViewBag.Phone = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Postal":
                    ViewBag.Postal = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Status":
                    ViewBag.Status = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                default:
                    ViewBag.Name = "Desc";
                    break;
            }

            ViewBag.cities = TempData["cities"];
            return PartialView("_UserResultPartial", result);

        }
        
        [HttpPost]
        public IActionResult Deactivate(int userId, int pageNumber = 1, int pageSize = 5)
        {
            User user = _context.Users.Find(userId);
            user.IsActive = false;

            this._context.Users.Update(user);
            this._context.SaveChanges();

            string sortBy = HttpContext.Session.GetString("sortBy");
            string sortOrder = HttpContext.Session.GetString("sortOrder");

            PagedResult<User> result = GetDefaultUserManage(pageNumber, pageSize, sortBy, sortOrder);

            ViewBag.sortBy = sortBy;
            switch (sortBy)
            {
                case "Name":
                    ViewBag.Name = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "UserTypeId":
                    ViewBag.UserTypeId = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "DOR":
                    ViewBag.DOR = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Phone":
                    ViewBag.Phone = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Postal":
                    ViewBag.Postal = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Status":
                    ViewBag.Status = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                default:
                    ViewBag.Name = "Desc";
                    break;
            }

            ViewBag.cities = TempData["cities"];
            return PartialView("_UserResultPartial", result);

        }

        [HttpPost]
        public IActionResult Approve(int userId, int pageNumber = 1, int pageSize = 5)
        {
            User user = _context.Users.Find(userId);
            user.IsApproved = true;
            user.IsActive = true;

            this._context.Users.Update(user);
            this._context.SaveChanges();

            string sortBy = HttpContext.Session.GetString("sortBy");
            string sortOrder = HttpContext.Session.GetString("sortOrder");

            PagedResult<User> result = GetDefaultUserManage(pageNumber, pageSize, sortBy, sortOrder);

            ViewBag.sortBy = sortBy;
            switch (sortBy)
            {
                case "Name":
                    ViewBag.Name = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "UserTypeId":
                    ViewBag.UserTypeId = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "DOR":
                    ViewBag.DOR = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Phone":
                    ViewBag.Phone = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Postal":
                    ViewBag.Postal = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                case "Status":
                    ViewBag.Status = sortOrder == "Asc" ? "Desc" : "Asc";
                    break;
                default:
                    ViewBag.Name = "Desc";
                    break;
            }

            ViewBag.cities = TempData["cities"];
            return PartialView("_UserResultPartial", result);

        }
    

        
        public IActionResult Export(int pageNumber = 1, int pageSize = 5)
        {
            PagedResult<User> result = GetDefaultUserManage(pageNumber, pageSize);

            List<string> cities = (List<string>)TempData["cities"];

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("User Name,User Type,Date of Registration,Phone Number,Postal Code,City,User Status");
            var i = 0;
            foreach (User user in result.Data)
            {

                var name = user.FirstName + " " + user.LastName;
                string userType = "";
                if (user.UserTypeId == 1)
                {
                    userType = "Customer";
                }
                else if (user.UserTypeId == 2)
                {
                    userType = "Service Provider";
                }
                else if (user.UserTypeId == 3)
                {
                    userType = "Admin";
                }
                var date = user.CreatedDate.ToString("dd/MM/yyyy").Replace("-", "/");

                string userStatus = "";
                if (user.IsActive == true)
                {
                    userStatus = "Active";
                }
                else
                {
                    userStatus = "Deactive";
                }

                stringBuilder.AppendLine($"{name},{userType},{date},{user.Mobile},{user.ZipCode},{cities[i]},{userStatus}");
                i++;
            }

            return File(Encoding.UTF8.GetBytes(stringBuilder.ToString()), "text/csv", "history.csv");
        }


    }
}
