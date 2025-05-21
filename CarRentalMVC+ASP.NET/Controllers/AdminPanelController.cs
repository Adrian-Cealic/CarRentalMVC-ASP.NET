using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRentalMVC_ASP.NET.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly YourDbContext db = new YourDbContext(); // înlocuiește cu contextul tău

        // GET: AdminPanel
        public ActionResult Index()
        {
            var users = db.Users.ToList();
            return View(users);
        }

        // Exemplu pentru editare user
        public ActionResult Edit(int id)
        {
            var user = db.Users.Find(id);
            if (user == null)
                return HttpNotFound();
            return View(user);
        }

        // Adaugă și alte acțiuni pentru administrare după nevoie
    }
}