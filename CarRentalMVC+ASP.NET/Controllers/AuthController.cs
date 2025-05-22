using System;
using System.Web;
using System.Web.Mvc;
using BusinessLogic;
using Data_Access_Layer;

namespace MyMvcProject.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController()
        {
            var context = new AppDbContext(); // manually create the context
            _authService = new AuthService(context); // pass it to your service
        }

        // Login Action (already exists)
        public ActionResult Login() => View();

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var user = _authService.Authenticate(username, password);
            if (user != null)
            {
                Session["UserId"] = user.Id;
                Session["Username"] = user.Username;
                Session["UserRole"] = user.Role;
                
                if (TempData["ReturnUrl"] != null)
                {
                    string returnUrl = TempData["ReturnUrl"].ToString();
                    return Redirect(returnUrl);
                }
                
                return RedirectToAction("Index", "Home");
            }
            
            ViewBag.Message = "Invalid username or password";
            return View();
        }

        // Logout Action (already exists)
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        // Register GET Action
        public ActionResult Register() => View();

        // Register POST Action
        [HttpPost]
        public ActionResult Register(string username, string password, string confirmPassword)
        {
            // Check if passwords match
            if (password != confirmPassword)
            {
                ViewBag.Message = "Passwords do not match.";
                return View();
            }

            // Call Register method in AuthService
            var success = _authService.Register(username, password);

            if (success)
                return RedirectToAction("Login");

            ViewBag.Message = "Username already exists.";
            return View();
        }
    }

}
