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
        private readonly CarService _carService;

        public HomeController()
        {
            var context = new AppDbContext();
            _userService = new UserService(context);
            _carService = new CarService(context);
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

        // CAR MANAGEMENT ACTIONS (ADMIN)

        // GET: Home/ManageCars
        public ActionResult ManageCars()
        {
            if (Session["UserRole"] as string != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            var cars = _carService.GetAllCars();
            return View(cars);
        }

        // GET: Home/AddCar
        public ActionResult AddCar()
        {
            if (Session["UserRole"] as string != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            return View(new Car()); // Pass a new Car model to the view
        }

        // POST: Home/AddCar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCar(Car car)
        {
            if (Session["UserRole"] as string != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                if (_carService.AddCar(car))
                {
                    TempData["Message"] = "Car added successfully!";
                    return RedirectToAction("ManageCars");
                }
                ModelState.AddModelError("", "Could not add the car. Please try again.");
            }
            // If model state is not valid, return view with the car model to show errors
            return View(car);
        }

        // GET: Home/EditCar/5
        public ActionResult EditCar(int id)
        {
            if (Session["UserRole"] as string != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            var car = _carService.GetCarById(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Home/EditCar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCar(Car car)
        {
            if (Session["UserRole"] as string != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                if (_carService.UpdateCar(car))
                {
                    TempData["Message"] = "Car updated successfully!";
                    return RedirectToAction("ManageCars");
                }
                ModelState.AddModelError("", "Could not update the car. Please try again.");
            }
            return View(car);
        }

        // POST: Home/DeleteCar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCar(int id)
        {
            if (Session["UserRole"] as string != "Admin")
            {
                // Or return a Forbidden status
                return RedirectToAction("Index", "Home");
            }

            if (_carService.DeleteCar(id))
            {
                TempData["Message"] = "Car deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Could not delete the car. It might not exist or an error occurred.";
            }
            return RedirectToAction("ManageCars");
        }

        public ActionResult Rent()
        {
            return View();
        }

        // Add this action to your HomeController
        public ActionResult Cars()
        {
            // Get all available cars to display to customers
            var cars = _carService.GetAllCars().Where(c => c.IsAvailable).ToList();
            return View(cars);
        }

    }
}