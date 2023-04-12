using ShoppingCartAssignment1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ShoppingCartAssignment1.Controllers
{
    public class CredentialsController : Controller
    {
        // GET: Credentials
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserModel model)
        {
            User user = db.Users.FirstOrDefault(x => x.UserName == model.UserName);
            var encrypt_password = encryptPassword(model.UserPassword);
            if (user != null)
            {
                if (user.UserPassword == encrypt_password)
                {
                    if (user.Role == "Admin")
                    {
                        Session["username"] = model.UserName;
                        FormsAuthentication.SetAuthCookie(model.UserName, false);
                        return RedirectToAction("Index", "Product");
                    }
                    else if (user.Role == "User")
                    {
                        Session["username"] = model.UserName;
                        FormsAuthentication.SetAuthCookie(model.UserName, false);
                        return RedirectToAction("Index", "Salesman");
                    }

                }
                ModelState.AddModelError("", "Invalid username or password");
                return View();
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View();
            }


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


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["username"] = null;
            return RedirectToAction("Login");
        }
    }
}