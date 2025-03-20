using System.Web;
using System.Web.Mvc;
using BusinessLogic;
using Domain;

namespace MyMvcProject.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController()
        {
            _authService = new AuthService();
        }

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var user = _authService.Authenticate(username, password);
            if (user != null)
            {
                Session["UserId"] = user.Id;
                Session["Username"] = user.Username;
                Session["UserRole"] = user.Role;
                return RedirectToAction("Index", "Home"); // Redirect to homepage
            }
            ViewBag.Message = "Invalid credentials!";
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
