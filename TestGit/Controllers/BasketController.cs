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
    }
}