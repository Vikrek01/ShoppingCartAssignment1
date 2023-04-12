using ShoppingCartAssignment1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCartAssignment1.Controllers
{
    public class ShoppingController : Controller
    {
        // GET: Shopping
        ApplicationDbContext db=new ApplicationDbContext();
        public ActionResult AddToCart(Guid id)
        {
            Product data = db.Products.Find(id);
            db.Carts.Add(new Cart { PId = data.PId, PName = data.PName, PImage = data.PImage, PDescription = data.PDescription, PPrice = data.PPrice });
            db.SaveChanges();
            ViewBag.msg = "<script> alert('Product added')</script>";
            return RedirectToAction("Index", "Salesman");
           
        }

        public ActionResult Show()
        {

            return View(db.Carts.ToList());
        }


    }
}