using ShoppingCartAssignment1.Models;
using ShoppingCartAssignment1.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCartAssignment1.Controllers
{
    [Authorize(Roles = "User")]
    public class SalesmanController : Controller
    {

        private IGenericRepository<Product> obj;

        public SalesmanController()
        {
            this.obj = new GenericRepository<Product>();
        }
        // GET: Salesman
        public ActionResult Index()
        {
            var data = obj.GetAll();
            return View(data);
        }
    }
}