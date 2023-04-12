using ShoppingCartAssignment1.Models;
using ShoppingCartAssignment1.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCartAssignment1.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ProductController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        private IGenericRepository<Product> obj;

        public ProductController()
        {
            this.obj = new GenericRepository<Product>();
        }
        // GET: Home
        public ActionResult Index()
        {
            var data = obj.GetAll();
            return View(data);
        }

        // GET: Home/Details/5
        [AllowAnonymous]
        public ActionResult Details(Guid id)
        {
            Product model = obj.GetById(id);
            return View(model);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create(HttpPostedFileBase file, Product product)
        {
           
            
            string filename = Path.GetFileName(file.FileName);
            string _filename = DateTime.Now.ToString("yymmssfff") + filename;
            string extension = Path.GetExtension(file.FileName);


            string path = Path.Combine(Server.MapPath("~/Content/Images/"), _filename);
            product.PImage = "~/Content/Images/" + _filename;

            if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
            {
                if (file.ContentLength < 100000)
                {
                    obj.Insert(product);
                    obj.Save();

                    file.SaveAs(path);
                    ViewBag.msg = "Student Added";
                    ModelState.Clear();
                    return RedirectToAction("Index");
                   
                }
                else
                {
                    ViewBag.msg = "File Size should be Less than 1 Mb";
                }
            }
            else
            {
                ViewBag.msg = "Invalid File Type";
            }
            return View();


        }

        // GET: Home/Edit/5
        public ActionResult Edit(Guid id)
        {
            Product model = obj.GetById(id);
            Session["ImgPath"] = model.PImage;
            return View(model);
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(HttpPostedFileBase file, Product product)
        {

            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string filename = Path.GetFileName(file.FileName);
                    string _filename = DateTime.Now.ToString("yymmssfff") + filename;
                    string extension = Path.GetExtension(file.FileName);


                    string path = Path.Combine(Server.MapPath("~/Content/Images/"), _filename);
                    product.PImage = "~/Content/Images/" + _filename;

                    if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                    {
                        if (file.ContentLength < 10000)
                        {
                            // db.Entry(product).State = EntityState.Modified;
                            string OldImgPath = Request.MapPath(Session["ImgPath"].ToString());
                            obj.Update(product);
                            obj.Save();

                            file.SaveAs(path);
                            if (System.IO.File.Exists(OldImgPath))
                            {
                                System.IO.File.Delete(OldImgPath);
                            }
                            ViewBag.msg = "Student Added";
                            TempData["msg"] = "Data Updated";
                            return RedirectToAction("Index");

                        }
                        else
                        {
                            ViewBag.msg = "File Size should be Less than 1 Mb";
                        }
                    }
                    else
                    {
                        ViewBag.msg = "Invalid File Type";
                    }
                }
            }
            else
            {
                product.PImage = Session["ImgPath"].ToString();
                obj.Update(product);
                obj.Save();
                return RedirectToAction("Index");
            }

            return View();

        }

        // GET: Home/Delete/5
        public ActionResult Delete(Guid id)
        {
            var model = obj.GetById(id);
            return View(model);
        }

        // POST: Home/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, Product product)
        {
            obj.Delete(id);
            obj.Save();

            return RedirectToAction("Index");

        }
    }
}
