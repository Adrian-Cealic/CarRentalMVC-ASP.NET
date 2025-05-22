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
        private readonly RentalService _rentalService;

        public HomeController()
        {
            var context = new AppDbContext();
            _userService = new UserService(context);
            _carService = new CarService(context);
            _rentalService = new RentalService(context);
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

        // GET: Home/Rent/5
        public ActionResult Rent(int id)
        {
            // Check if user is logged in
            if (Session["UserId"] == null)
            {
                // Store the car ID in TempData so we can redirect back after login
                TempData["RentCarId"] = id;
                TempData["ReturnUrl"] = Url.Action("Rent", "Home", new { id = id });
                
                // Redirect to login page
                return RedirectToAction("Login", "Auth");
            }
            
            // User is logged in, proceed with rental
            var car = _carService.GetCarById(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            
            // Check if car is available
            if (!car.IsAvailable)
            {
                TempData["ErrorMessage"] = "Sorry, this car is not available for rent.";
                return RedirectToAction("Cars");
            }
            
            // Create a new rental with default dates
            var rental = new Domain.Rental
            {
                CarId = car.Id,
                UserId = (int)Session["UserId"],
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(1)
            };
            
            ViewBag.Car = car;
            return View(rental);
        }

        // POST: Home/Rent
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Rent(Rental rental)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            if (ModelState.IsValid)
            {
                // Ensure the user ID is set from the session
                rental.UserId = (int)Session["UserId"];
                
                // Validate dates
                if (rental.StartDate < DateTime.Today)
                {
                    ModelState.AddModelError("StartDate", "Start date cannot be in the past.");
                    ViewBag.Car = _carService.GetCarById(rental.CarId);
                    return View(rental);
                }
                
                if (rental.EndDate < rental.StartDate)
                {
                    ModelState.AddModelError("EndDate", "End date must be after start date.");
                    ViewBag.Car = _carService.GetCarById(rental.CarId);
                    return View(rental);
                }
                
                if (_rentalService.CreateRental(rental))
                {
                    TempData["Message"] = "Your rental has been successfully booked!";
                    return RedirectToAction("MyRentals");
                }
                
                ModelState.AddModelError("", "Unable to create rental. The car may no longer be available.");
            }
            
            ViewBag.Car = _carService.GetCarById(rental.CarId);
            return View(rental);
        }

        // GET: Home/MyRentals
        public ActionResult MyRentals()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            
            int userId = (int)Session["UserId"];
            var rentals = _rentalService.GetUserRentals(userId);
            return View(rentals);
        }

        // GET: Home/ManageRentals (Admin only)
        public ActionResult ManageRentals()
        {
            if (Session["UserRole"] as string != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            
            var rentals = _rentalService.GetAllRentals();
            return View(rentals);
        }

        // POST: Home/UpdateRentalStatus
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateRentalStatus(int id, RentalStatus status)
        {
            if (Session["UserRole"] as string != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            
            if (_rentalService.UpdateRentalStatus(id, status))
            {
                TempData["Message"] = "Rental status updated successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update rental status.";
            }
            
            return RedirectToAction("ManageRentals");
        }

        // POST: Home/CancelRental
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CancelRental(int id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            
            if (_rentalService.CancelRental(id))
            {
                TempData["Message"] = "Your rental has been cancelled.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to cancel rental.";
            }
            
            return RedirectToAction("MyRentals");
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