using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WCFTestClient.CategoryServiceReference;
using WCFTestClient.ProductServiceReference;

namespace WCFTestClient.Controllers
{
    public class ProductsController : Controller
    {
        ProductServiceClient prod;
        CategoryServiceClient cate;
        public ProductsController()
        {
            prod = new ProductServiceClient();
            cate = new CategoryServiceClient();
        }
        // GET: Products
        public ActionResult Index()
        {
            return View(prod.Get());
        }

        // GET: Products/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(cate.Get(), "Id", "Name");
            return View();

        }

        // POST: Products/Create
        [HttpPost]
        public ActionResult Create(ProductServiceReference.Product pro)
        {
            ViewBag.CategoryId = new SelectList(cate.Get(), "Id", "Name");
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    prod.Add(pro);
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Edit/5
        public ActionResult Edit(string id)
        {
            
            var p = prod.GetById(id);
            ProductServiceReference.Product pro = new ProductServiceReference.Product();
            pro.Id = p.Id;
            pro.Name = p.Name;
            pro.Price = p.Price;
            pro.Descriptions = p.Descriptions;
            pro.Status = p.Status;
            pro.CategoryId = p.CategoryId;
            ViewBag.CategoryId = new SelectList(cate.Get(), "Id", "Name",pro.CategoryId);
            return View(pro);
        }

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductServiceReference.Product pro)
        {
            ViewBag.CategoryId = new SelectList(cate.Get(), "Id", "Name", pro.CategoryId);
            try
            {
                prod.Edit(pro);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Delete(string id)
        {
            try
            {
                // TODO: Add delete logic here
                prod.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
