using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InfTeh_test.DataContexts;

namespace InfTeh_test.Controllers
{
    public class HomeController : Controller
    {
        DataContext dc = new DataContext();

        public ActionResult Index()
        {
            //return RedirectToAction("GetNavigationBlock", "Navigation");
            return PartialView();
        }
    }
}