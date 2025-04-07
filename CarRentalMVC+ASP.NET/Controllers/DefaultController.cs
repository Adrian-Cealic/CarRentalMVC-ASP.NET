using System;
using System.Web.Mvc;
using MySql.Data.MySqlClient;

namespace CarRentalMVC_ASP.NET.Controllers
{
    public class DefaultController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TestDatabaseConnection()
        {
            string connectionString = "Server=localhost;Database=car_rental;User ID=root;Password=travelmate5335;";
            string message;

            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    message = "Connection successful!";
                }
                catch (Exception ex)
                {
                    message = "Connection failed: " + ex.Message;
                }
            }

            return Content(message);
        }
    }
}
