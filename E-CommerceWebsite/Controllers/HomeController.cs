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
                        
                        return RedirectToAction("UserDashBoard", UserRecord);
                    }
                }
            }
            return View(UserData);
        }

        public ActionResult UserDashBoard()
        {
            if (Session["FirstName"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }

        }
    }
}