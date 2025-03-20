using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRentalMVC_ASP.NET.Controllers
{
    public class HomeController : Controller
    {
        // Action for main page
        public ActionResult Index()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Auth");

            return View();
        }

        // Action for about page
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        // Action for contact page
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        // Action for 404 error page
        public ActionResult Error()
        {
            return View();  
        }
    }
}