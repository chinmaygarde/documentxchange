using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DocumentShare.Models;

namespace DocumentShare.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to Document Share";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}