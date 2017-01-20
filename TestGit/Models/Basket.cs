using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Web.Mvc;


namespace TestGit.Models
{
    public class Basket
    {
        //public instCatalogue;
        public bool emptyBasket;
        public int numberOfItems;
        public string summary;

        public Basket()
        {

            Hashtable items = new Hashtable();

            items.Add(2, 2);
            items.Add(12, 2);
            items.Add(32, 12);
            items.Add(42, 12);
            items.Add(52, 22);
            items.Add(62, 22);


            HttpContext.Current.Session["basket"] = items;

            emptyBasket = (HttpContext.Current.Session["basket"] == null) ? true : false;

            noItems();
            summarize();
        }

        public void noItems()
        {
            int value = 0;
            if (!emptyBasket)
            {

                // Get a collection of the keys.
                Hashtable items = (Hashtable)HttpContext.Current.Session["basket"];
                ICollection ItemKeys = items.Keys;

                foreach (int key in ItemKeys)
                {
                    value++;
                }

            }
            numberOfItems = value;
        }

        public void summarize()
        {
            
            if (!emptyBasket)
            {
                MyOpacDBContext db = new MyOpacDBContext();

                Hashtable basket = (Hashtable)HttpContext.Current.Session["basket"];
                ICollection Itemkeys = basket.Keys;

                List<string> list = new List<string>();
                foreach (int key in Itemkeys)
                {
                    //category category = db.categories.Find(basket[key]);
                    //item item = db.items.Find(key);

                    category category = db.categories.Single(cat => cat.id == (int) basket[key]);
                    item item = db.items.Single(i => i.id == key);

                    if (category !=null && item == null) {
                        list.Add(category.name + ": (" + shortenString(item.title, 120) + ")");
                    }

                    list.Add("item: "+ key+" cate: "+ basket[key]);
                    
                }

                string[] str = list.ToArray();
                summary = "Items: " + string.Join(", ", str);
            }
        }

        

        private string shortenString(string str, int length)
        {
            if (str.Length > length)
            {
                return string.Concat(str.Substring(0, length), "...");
            }

            return string.Concat(str, "...");
        }


        public static IHtmlString removeButton(int id)
        {
            string str = "";
            if (((Hashtable)HttpContext.Current.Session["basket"]).Contains(id) && HttpContext.Current.Session["basket"] != null)
            {
                str += "<a href='#' class='remove_basket'";
                str += "rel='" + id + "'>Remove</a>";
            }
            return new MvcHtmlString(str);
        }


        //public static string activeButton(int itemId)
            public static IHtmlString activeButton(int itemId)
        {
            int id;
            string label;
            if (((Hashtable)HttpContext.Current.Session["basket"]).Contains(itemId) && HttpContext.Current.Session["basket"] != null)
            {
                id = 0;
                label = "Remove from reservation";
            }
            else {
                id = 1;
                label = "Add to reservation";
            }

            string str = "<a href='#' class='add_to_basket";
            str += (id == 0) ? " red":null;
            str += " ' rel='";
            str += itemId + "_" + id;
            str += "'>" + label + "</a>";

            return new MvcHtmlString(str);
        }

    }
}