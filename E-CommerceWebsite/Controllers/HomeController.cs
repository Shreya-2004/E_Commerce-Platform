using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace E_CommerceWebsite.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User UserData)
        {
            if(ModelState.IsValid)
            {
                using(Entities db = new Entities())
                {
                    var UserRecord = db.Users.Where(a => a.Email.Equals(UserData.Email) && a.Password.Equals(UserData.Password)).FirstOrDefault();
                    
                    if(UserRecord != null)
                    {
                        Session["FirstName"] = UserData.FirstName;
                        return RedirectToAction("UserDashBoard", UserRecord);
                    }

                }
            }
            ViewData["InvalidUserNamePass"] = "Your Email and Password not found !!!";
            return View(UserData);
        }

        // GET: Home
        public ActionResult UserSignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserSignUp(User UserData)
        {
            if (ModelState.IsValid)
            {
                using (Entities db = new Entities())
                {
                    try
                    {
                        db.Users.Add(UserData);
                        db.SaveChanges();
                        Session["FirstName"] = UserData.FirstName;
                        return RedirectToAction("UserDashBoard");

                    }
                    catch (Exception ex)
                    {
                        ViewData["UnableToSignUp"] = "Unable to Sign Up due to some issues!!!";
                        return View("UserSignUp");
                    }
                }
            }
            ViewData["UnableToSignUp"] = "Unable to Sign Up due to some issues!!!";
            return View("UserSignUp");
        }

        public ActionResult UserDashBoard()
        {
            if (Session["FirstName"] != null)
            {
                ViewData["FirstName"] = Session["FirstName"].ToString();
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }

        }
    }
}