using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace TestGit.Models
{
    public class CustomSession
    {
        public static void setItem(int itemID, int category) {


            Hashtable items = (Hashtable)HttpContext.Current.Session["basket"];

            if (items == null) { 
                items = new Hashtable();
            }

            items.Add(itemID, category);

            HttpContext.Current.Session["basket"] = items;

        }

        public static void removeItem(int id) {
            Hashtable items = (Hashtable)HttpContext.Current.Session["basket"];
            if (items != null)
            {
                items.Remove(id);
                HttpContext.Current.Session["basket"] = items;
            }
        }

        

        public static void setSession(String name, Hashtable items) {
            HttpContext.Current.Session[name] = items;
        }

        public static void clear(string name) {
            HttpContext.Current.Session.Remove(name);
        }

    }
}