using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using CarRentalMVC_ASP.NET.Models; // Asigură-te că ai referința corectă la modelul User
using Data_Access_Layer; // Asigură-te că ai referința corectă la contextul bazei de date

namespace CarRentalMVC_ASP.NET.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly AppDbContext db = new AppDbContext(); // înlocuiește cu contextul tău

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Delete action called with id: " + id);
                var user = db.Users.Find(id);
                if (user != null)
                {
                    db.Users.Remove(user);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Loghează excepția pentru depanare
                System.Diagnostics.Debug.WriteLine("Delete error: " + ex.Message);
                // Poți returna o pagină de eroare personalizată sau ViewBag cu mesajul
                return View("Error", (object)ex.Message);
            }
        }

        // Adaugă și alte acțiuni pentru administrare după nevoie
    }
}