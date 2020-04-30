using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUD.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Controllers
{
    [Route("product")]
    public class ProductController : Controller
    {
        private DatabaseContext db;

        public ProductController(DatabaseContext _db)
        {
            db = _db;
        }

        [Route("")]
        [Route("~/")]
        [Route("index")]
        public IActionResult Index()
        {
            ViewBag.products = db.Products.ToList();
            return View();
        }
        [HttpGet]
        [Route("add")]
        public IActionResult Add()
        {
           
            return View("Add", new Product());
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            db.Products.Remove(db.Products.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var product = db.Products.Find(id);
            return View("Edit", product);
        }

        [HttpGet]
        [Route("edit/{id}")]
        public IActionResult Edit(int id, Product product)
        {
            db.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified ;
            db.SaveChanges();                 
            return View("Edit", product);
        }
    }
}

