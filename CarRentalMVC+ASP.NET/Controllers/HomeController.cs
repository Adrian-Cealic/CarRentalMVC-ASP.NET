using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data_Access_Layer;
using BusinessLogic;
using Domain;

namespace CarRentalMVC_ASP.NET.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserService _userService;

        public HomeController()
        {
            var context = new AppDbContext();
            _userService = new UserService(context);
        }

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

            // Use UserService to get all users
            var users = _userService.GetAllUsers(); 

            // Pass the users to the view
            return View(users);
        }

        // GET: Home/EditUserRole/5
        public ActionResult EditUserRole(int id) // 'id' matches the route parameter
        {
            if (Session["UserRole"] as string != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }

            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            // ViewBag.Roles = new List<string> { "Admin", "User" }; // Example for dropdown
            return View(user);
        }

        // POST: Home/EditUserRole/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUserRole(int id, string role) // Parameter 'role' from the form
        {
            if (Session["UserRole"] as string != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }

            // It's good practice to re-validate the model if you were binding to a full model
            // For a single field like 'role', direct validation or service-layer validation is key.
            // if (ModelState.IsValid) // More relevant if binding to a complex model
            // {
                var success = _userService.UpdateUserRole(id, role);
                if (success)
                {
                    TempData["Message"] = "User role updated successfully!";
                    return RedirectToAction("AdminPanel");
                }
                else
                {
                    // If update failed, user not found or role was invalid
                    ModelState.AddModelError("", "Failed to update user role. User not found or invalid role.");
                    var user = _userService.GetUserById(id); // Re-fetch user to repopulate view
                    if (user == null) return HttpNotFound(); // Should not happen if update failed due to invalid role
                    // ViewBag.Roles = new List<string> { "Admin", "User" }; // Repopulate for dropdown
                    return View(user); // Return to view with error
                }
            // }
            // var userToEdit = _userService.GetUserById(id); // Re-fetch if ModelState was invalid
            // if (userToEdit == null) return HttpNotFound();
            // ViewBag.Roles = new List<string> { "Admin", "User" }; // Repopulate for dropdown
            // return View(userToEdit);
        }

        public ActionResult Rent()
        {
            return View();
        }

    }
}