﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using TestGit.Models;

namespace TestGit.Controllers
{
    public class BasketController : Controller
    {
        private MyOpacDBContext db = new MyOpacDBContext();
        // GET: Basket
        public ActionResult Index()
        {
            List<item> list = getBasketItems();

            return View(list);
        }

        private List<item> getBasketItems()
        {
            Hashtable basket = new Hashtable();
            if (Session["basket"] != null)
            {
                basket = (Hashtable)Session["basket"];
            }

            ICollection Itemkeys = basket.Keys;

            List<item> list = new List<item>();
            foreach (int key in Itemkeys)
            {
                item item = db.items.Find(key);
                list.Add(item);
            }

            return list;
        }

        // GET: Basket/Reserve
        public ActionResult Reserve()
        {
            ViewBag.library = db.libraries.ToList();
            return View();

        }

        // POST: Basket/Reserve
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reserve(int user, int library, string notes)
        {

            if (ModelState.IsValid)
            {
                CustomSession.clear("basket");
                return RedirectToAction("Index");
            }

            ViewBag.library = db.libraries.ToList();
            return View();
        }


        [HttpPost]
        public ActionResult Add(int job, int id)
        {

            item item = db.items.Find(id);

            int value = 0;

            if (item != null)
            {
                switch (job)
                {
                    case 0:
                        value = 1;
                        CustomSession.removeItem(id);
                        break;
                    case 1:
                        value = 0;
                        CustomSession.setItem(id, item.category1.id);
                        break;
                }

            }
            var model = new { job = value };
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        
        public ActionResult SmallRefresh() {

            Basket basket = new Basket();

            var model = new { bl_ti = basket.numberOfItems, bl_s = basket.summary };
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public void RemoveItem(int id) {
            CustomSession.removeItem(id);
        }

        public ActionResult BigBasket() {

            List<item> list = getBasketItems();

            return PartialView("_BigBasket", list);
        }
    }
}