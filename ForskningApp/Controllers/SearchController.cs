using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;

namespace ForskningApp.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        //Har adda DAL som refference til Controlleren for å teste om jquery funker.
        public JsonResult getSearchValue(string search)
        {
            using (var db = new dbEntities())
            {
                List<Models.Person> allSearch = db.person.Where(p => p.fornavn.Contains(search)).Select(p => new Models.Person
                {
                    cristinID = p.cristinID,
                    Navn = p.fornavn + " " + p.etternavn,
                }).ToList();
                return new JsonResult { Data = allSearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
    }
}