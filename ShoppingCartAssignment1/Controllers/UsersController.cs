using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ShoppingCartAssignment1.Models;

namespace ShoppingCartAssignment1.Controllers
{
    public class UsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserName,UserPassword")] User user)
        {
            if (ModelState.IsValid)
            {
                User user1 = db.Users.FirstOrDefault(emp=>emp.UserName==user.UserName);

                if (user1 == null)
                {
                    user.UserPassword = encryptPassword(user.UserPassword);

                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Login", "Credentials");
                }
                else
                {
                    ModelState.AddModelError("", "Already exist");
                }
            }

            return View(user);
        }

        private string encryptPassword(string pass)
        {
            string password = "";
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(pass));
                password = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
            return password;
        }
    }
}
