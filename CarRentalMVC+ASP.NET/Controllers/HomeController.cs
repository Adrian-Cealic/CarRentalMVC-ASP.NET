using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data_Access_Layer;

namespace CarRentalMVC_ASP.NET.Controllers
{
    public class HomeController : Controller
    {
        // Action for main page
        public ActionResult Index()
        {
            // if (Session["UserId"] == null)
            //     return RedirectToAction("Login", "Auth");

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
        public ActionResult AdminPanel()
        {
            // Check if the user is logged in and has the 'Admin' role
            var userRole = Session["UserRole"] as string; // Retrieve the role from the session

            if (userRole != "Admin")
            {
                return RedirectToAction("Index", "Home"); // Redirect non-admin users to homepage
            }

            // Create context and get all users
            var context = new AppDbContext();
            var users = context.Users.ToList(); // Assuming you have a Users DbSet in your context

            // Pass the users to the view
            return View(users);
        }

        public ActionResult Rent()
        {
            return View();
        }

    }
}