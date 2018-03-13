using ForskningApp;
using Iveonik.Stemmers;
using NetSpell.SpellChecker;
using NetSpell.SpellChecker.Dictionary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Console = System.Console;


namespace ForskningApp.Controllers
{
    public class HomeController : Controller
    {
        // add new action in your controller for getting a view, 
        // where we will implement our first js view
        public ActionResult Index()
        {
            return View();
        }

        // add another MVC action for return JSON data
        // for showing in react JS component
        public JsonResult getmessage()
        {
            return new JsonResult
            {
                Data = "Hello World. I am from server-side",
                JsonRequestBehavior = JsonRequestBehaviour.AllowGet
            };
        }
    }
}
