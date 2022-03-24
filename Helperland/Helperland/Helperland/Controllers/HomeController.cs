using Helperland.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Helperland.Data;

using System.IO;
using System.Net;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;

namespace Helperland.Controllers
{
    public class HomeController : Controller
    {
        private readonly HelperlandContext _context;

        public HomeController(HelperlandContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult Faq()
        {
            return View();
        }

        public IActionResult Price()
        {
            return View();
        } 

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ContactUs(ContactU contactU)
        {

            var mailsent=false;
            contactU.Name = contactU.FirstName + " " + contactU.LastName;

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Helperland", "helperlandindia@gmail.com"));
            message.To.Add(new MailboxAddress("Admin", "mayurodedra.40st@gmail.com"));
            message.Subject = contactU.Subject;
            message.Body = new TextPart("plain")
            {
                Text=contactU.Name+" have query regarding "+contactU.Subject+
                "\n\n"+contactU.Message+"\nThankyou \n\nContacts:\n"
                +"Email:"+contactU.Email+"\n"
                +"Phone No: +49"+contactU.PhoneNumber
            };
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com",587,false);
                client.Authenticate("helperlandindia@gmail.com", "Helperland@123");
                client.Send(message);
                mailsent = true;
                client.Disconnect(true);
            }

            if(mailsent)
            {
            DateTime now = DateTime.Now;
            contactU.CreatedOn = now;
            this._context.ContactUs.Add(contactU);
            this._context.SaveChanges();

                TempData["Message"] = "Your Issue is Registered and will be resolved soon";
                TempData["ModalName"] = "#logout-Modal";

                return RedirectToAction("ContactUs", "Home");

            }
            TempData["Message"] = "Unable to register your Problem, try again after sometime";
            TempData["ModalName"] = "ContactU";
            return View("ContactUs", contactU);
        }

        public IActionResult ServiceProvider()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ServiceProvider(User user)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View("ServiceProvider", user);
            //}

            user.UserTypeId = 2;
            DateTime now = DateTime.Now;
            user.CreatedDate = now;
            user.ModifiedDate = now;
            user.IsActive = false;
            user.IsApproved = false;
            this._context.Users.Add(user);
            this._context.SaveChanges();

            TempData["Message"] = "Successfully Registered, wait for Admin to approve your Acccount";
            TempData["ModalName"] = "#logout-Modal";
            TempData["Modal2Name"] = "#loginModal";
            return RedirectToAction("Index", "Home");

        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
           

            user.UserTypeId = 1;
            DateTime now = DateTime.Now;
            user.CreatedDate = now;
            user.ModifiedDate = now;
            user.IsApproved = true;
            user.IsActive = true;
            this._context.Users.Add(user);
            this._context.SaveChanges();

            TempData["Message"] = "Successfully Registered";
            TempData["ModalName"] = "#logout-Modal";
            TempData["Modal2Name"] = "#loginModal";
            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        public IActionResult ForgotPassowrd(User user)
        {

            User identity = (User)_context.Users.Where(x => x.Email == user.Email).FirstOrDefault();

            if (identity != null)
            {
                var name = identity.FirstName+" "+identity.LastName;
                var url = "http://localhost:28935/home/confirmpassword";
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Helperland", "helperlandindia@gmail.com"));
                message.To.Add(new MailboxAddress(name, identity.Email));
                message.Subject = "Forgot Password";
                message.Body = new TextPart("plain")
                {
                    Text = "Hello " + name + "\n click on the below link to reset password \n" + url + "?email=" +identity.Email

                };
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("helperlandindia@gmail.com", "Helperland@123");
                    client.Send(message);
                    client.Disconnect(true);
                }

                TempData["Message"] = "Use link sent to your Email to reset Password";
                TempData["ModalName"] = "#logout-Modal";

                return RedirectToAction("Index", "Home");

            }
            else
            {
                TempData["Message"] = "No Account Found with this Email";
                TempData["ModalName"] = "#forgot-password-Modal";

                return View("Index", user);
            }
        }

        public IActionResult ConfirmPassword(string email)
        {
            if(email!=null)
            {
            ViewBag.email = email;
                var identity = _context.Users.Where(x => x.Email == email).FirstOrDefault();
                if (identity == null)
                {
                    TempData["Message"] = "Link has been expired, try again";
                    TempData["ModalName"] = "#forgot-password-Modal";
                    return View("Index");
                }
                else
                {
                    return View();
                }

            }
            else
            {
                TempData["Message"] = "Link has been expired, try again";
                TempData["ModalName"] = "#forgot-password-Modal";
                return View("Index");
            }
        }

        [HttpPost]
        public IActionResult ResetPassword(User user)
        {
            User identity = _context.Users.Where(x => x.Email == user.Email).FirstOrDefault();

            if(identity!=null)
            {
                identity.Password = user.Password;
                this._context.Update(identity);
                this._context.SaveChanges();
                TempData["ModalName"] = "#logout-Modal";
                TempData["Message"] = "Password has been successfully reset";
                //TempData["Modal2Name"] = "#loginModal";
                return RedirectToAction("Index", "Home", user);
            }
            else
            {
                ViewData["ModalName"] = "#forgot-password-Modal";
                TempData["Message"] = "Unable to update Password, Try again after sometime";
                return View("ConfirmPassword", user);
            }


        }

        
        [HttpPost]
        public IActionResult Login(User user)
        {

            User identity = _context.Users.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();
            
            if(identity!=null)
            {
                var Name = identity.FirstName + " " + identity.LastName;
                
                if (identity.UserTypeId == 1)
                {
                    ViewBag.userType = identity.UserTypeId;
                    HttpContext.Session.SetString("isLoggedIn", true.ToString());
                    HttpContext.Session.SetString("UserTypeId", identity.UserTypeId.ToString());
                    HttpContext.Session.SetString("Name", Name);
                    HttpContext.Session.SetString("UserId", identity.UserId.ToString());
                    return RedirectToAction("Index", "Customer");
                   
                }
                else if (identity.UserTypeId == 2 && identity.IsApproved==true)
                {
                    ViewBag.userType = identity.UserTypeId;
                    HttpContext.Session.SetString("isLoggedIn", true.ToString());
                    HttpContext.Session.SetString("UserTypeId", identity.UserTypeId.ToString());
                    HttpContext.Session.SetString("Name", Name);
                    HttpContext.Session.SetString("UserId", identity.UserId.ToString());
                    if (identity.ZipCode!=null)
                    {
                    HttpContext.Session.SetString("code", identity.ZipCode);

                    }
                    else
                    {
                        HttpContext.Session.SetString("code", "0");

                    }
                    return RedirectToAction("Index", "ServiceProvider");
                }
                else if (identity.UserTypeId == 2 && identity.IsApproved == false)
                {

                    TempData["Message"] = "Your account is Not Yet approved by Admin";
                    TempData["ModalName"] = "#loginModal";
                    return View("Index", user);
                }
                else if (identity.UserTypeId == 3)
                {
                    ViewBag.userType = identity.UserTypeId;
                    HttpContext.Session.SetString("isLoggedIn", true.ToString());
                    HttpContext.Session.SetString("UserTypeId", identity.UserTypeId.ToString());
                    HttpContext.Session.SetString("Name", Name);
                    HttpContext.Session.SetString("UserId", identity.UserId.ToString());
                    return RedirectToAction("Index","Admin");

                }
                else
                {
                    TempData["Message"] = "Some error occured, try after some time";
                    TempData["ModalName"] = "#loginModal";
                    return View("Index", user);
                }
            }
            else
            {
                TempData["Message"] = "User Not Found, Enter Correct Email and Password";
                TempData["ModalName"] = "#loginModal";
                return View("Index", user);
            }
               
            
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["Message"] = "You have successfully logged out";
            TempData["ModalName"] = "#logout-Modal";
            //return RedirectPermanent("Index", "Home"); 
            //TempData["IsLoggedOut"] = true;
            return RedirectToAction("Index","Home");
        }
    }
}
