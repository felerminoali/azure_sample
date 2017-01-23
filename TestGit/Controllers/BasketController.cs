using System;
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

            Hashtable basket = (Hashtable) Session["basket"];
            ICollection Itemkeys = basket.Keys;

            List<item> list = new List<item>();
            foreach (int key in Itemkeys)
            {
                item item = db.items.Find(key);
                list.Add(item);
            }

            return View(list);
        }

        // GET: Basket/Reserve
        public ActionResult Reserve() {
            ViewBag.library = db.libraries.ToList();
            return View();
            
        }

        // POST: Basket/Reserve
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Reserve()
        //{

        //    return View();
        //}
    }
}