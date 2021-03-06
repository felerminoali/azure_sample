﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Web.Mvc;


namespace TestGit.Models
{
    public class Basket
    {

        public bool emptyBasket;
        public int numberOfItems;
        public string summary;

        public Basket()
        {

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
                Hashtable basket = (Hashtable)HttpContext.Current.Session["basket"];
                //var basket = HttpContext.Current.Session["basket"] as Hashtable;
                ICollection Itemkeys = basket.Keys;

                MyOpacDBContext db = new MyOpacDBContext();

                List<string> list = new List<string>();
                foreach (int key in Itemkeys)
                {
                    item item = db.items.Find(key);
                    category category = db.categories.Find(basket[key]);

                    if (item != null)
                    {
                        list.Add(category.name + ": (" + shortenString(item.title, 15) + ")");
                    }

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

        public static IHtmlString activeButton(int itemId)
        {
            int id;
            string label;

            if (HttpContext.Current.Session["basket"] != null)
            {
                if (((Hashtable)HttpContext.Current.Session["basket"]).Contains(itemId))
                {
                    id = 0;
                    label = "Remove from reservation";
                    return new MvcHtmlString(anchorWrap(itemId, id, label));
                }

            }

            id = 1;
            label = "Add to reservation";

            return new MvcHtmlString(anchorWrap(itemId, id, label));
        }

        private static string anchorWrap(int itemId, int id, string label)
        {
            string str = "<a href='#' class='add_to_basket";
            str += (id == 0) ? " red" : null;
            str += " ' rel='";
            str += itemId + "_" + id;
            str += "'>" + label + "</a>";
            return str;
        }
    }
}