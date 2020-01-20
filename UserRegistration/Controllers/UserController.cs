using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using UserRegistration.Models;

namespace UserRegistration.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Registration()
        {
            return View();
        }
        //Registration Post Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Exclude = "IsEmailVerified,ActivationCode")] UserDetail user)
        {
            bool status = false;
            string message = "";

            //Model Validation
            if (ModelState.IsValid)
            {
                //Email is already exists
                var isExist = IsEmailExist(user.EmailID);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email already exist");
                    return View(user);
                }
                //Generate Activation Code
                user.ActivtionCode = Guid.NewGuid();

                //password Hashing
                user.Password = Crypto.Hash(user.Password);
                user.ConfirmPassword = Crypto.Hash(user.ConfirmPassword);

                user.IsEmailVerified = false;
                //Save to database
                using (sampleEntities dc = new sampleEntities())
                {
                    dc.UserDetails.Add(user);
                    dc.SaveChanges();

                    //Send Email to User
                    /*  SendVerificationLinkEmail(user.EmailID, user.ActivtionCode.ToString());*/
                    message = "Registration successfully done. ";
                    status = true;
                }

            }
            else
            {
                message = "Invalid Request";
            }
            ViewBag.Message = message;
            ViewBag.Status = status;
            return View(user);
        }
        [NonAction]
        
        private bool IsEmailExist(string emailID)
        {
            using (sampleEntities dc = new sampleEntities())
            {
                var v = dc.UserDetails.Where(a => a.EmailID == emailID).FirstOrDefault();
                return v != null;
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserLogin login,string ReturnURL="")
        {
            string message = "";
            using (sampleEntities se = new sampleEntities())
            {
                var v = se.UserDetails.Where(a => a.EmailID == login.EmailID).FirstOrDefault();
                if(v != null)
                {
                    if(string.Compare(Crypto.Hash(login.Password), v.Password) == 0)
                    {
                        int timeout = login.RememberMe ? 525600 : 20; // 525600 min = 1 year
                        var ticket = new FormsAuthenticationTicket(login.EmailID, login.RememberMe, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);

                        if (Url.IsLocalUrl(ReturnURL))
                        {
                            return Redirect(ReturnURL);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        message = "Invalid Credential provided";
                    }
                }
                else
                {
                    message = "Invalid credential provided";
                }
            }
            ViewBag.Message = message;
            return View();
        }//End of hhtppost login

        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }
    }
}