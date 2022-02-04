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

        public ViewResult Index()
        {
            return View();
        }

        public ViewResult Faq()
        {
            return View();
        }

        public ViewResult Price()
        {
            return View();
        } 

        public ViewResult AboutUs()
        {
            return View();
        }

        public ViewResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public ViewResult ContactUs(ContactU contactU)
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
            }

            return View("Index");
        }

        public ViewResult ServiceProvider()
        {
            return View();
        }

        [HttpPost]
        public ViewResult ServiceProvider(User user)
        {
            user.UserTypeId = 2;
            DateTime now = DateTime.Now;
            user.CreatedDate = now;
            user.ModifiedDate = now;
            this._context.Users.Add(user);
            this._context.SaveChanges();
            return View("Index");
        }

        public ViewResult Register()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Register(User user)
        {
            user.UserTypeId = 1;
            DateTime now = DateTime.Now;
            user.CreatedDate = now;
            user.ModifiedDate = now;
            user.ModifiedBy = 0;
            this._context.Users.Add(user);
            this._context.SaveChanges();
            return View("Index");
        }

        [HttpPost]
        public ViewResult ForgotPassowrd(User user)
        {
            User identity = (User)_context.Users.Where(x => x.Email == user.Email).FirstOrDefault();

            if (identity != null)
            {
                var name = identity.FirstName + " " + identity.LastName;
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
                ViewBag.msg = "Use link sent to your Email to reset Password";
                ViewBag.ModalName = "#forgot-password-Modal";
                ViewData["ModalName"]= "#forgot-password-Modal";
            }
            else
            {
                ViewBag.success = false;
            }

            return View("Index");
        }

        public ViewResult ConfirmPassword( string email)
        {
            ViewBag.email = email;
            return View();
        }

        [HttpPost]
        public ViewResult ResetPassword(User user)
        {
            User identity = _context.Users.Where(x => x.Email == user.Email).FirstOrDefault();

            if(identity!=null)
            {
                identity.Password = user.Password;
                this._context.Update(identity);
                this._context.SaveChanges();
            }

            return View("Index");
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            User identity = _context.Users.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();
            
            if(identity!=null)
            {
                var Name = identity.FirstName + " " + identity.LastName;
                ViewBag.userType = identity.UserTypeId;
                HttpContext.Session.SetString("isLoggedIn", true.ToString());
                HttpContext.Session.SetString("Name", Name);
                if (identity.UserTypeId == 1)
                {
                    HttpContext.Session.SetString("UserTypeId", identity.UserTypeId.ToString());
                    return RedirectToAction("Index", "Customer");
                   
                }
                else if (identity.UserTypeId == 2)
                {
                    HttpContext.Session.SetString("UserTypeId", identity.UserTypeId.ToString());
                    return RedirectToAction("Index", "ServiceProvider");
                }
            }

            return View("Index");

        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
