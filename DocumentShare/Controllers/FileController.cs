using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using DocumentShare.Models;
using System.Text;
using DocumentShare.Extensions;
namespace DocumentShare.Controllers
{
    public class FileController : Controller
    {
        //
        // GET: /File/

        //public ActionResult Index()
        //{
        //    return View();
        //}

        ////
        //// GET: /File/Details/5

        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        ////
        //// GET: /File/Create

        //public ActionResult Create()
        //{
        //    return View();
        //} 

        //
        // POST: /File/Create

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                using (var db = new DocumentDataContext())
                {
                    if (Request.Files.Count == 1 && Request.Files[0].ContentLength < 102400000)
                    {
                        string internalFileName = new StringBuilder(12).AppendRandomString(12).ToString();
                        while (System.IO.File.Exists(Server.MapPath(Url.DocumentFileUrl(internalFileName))))
                        {
                            internalFileName = new StringBuilder(12).AppendRandomString(12).ToString();
                        }
                        Request.Files[0].SaveAs(Server.MapPath(Url.DocumentFileUrl(internalFileName)));

                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        ////
        //// GET: /File/Edit/5
 
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        ////
        //// POST: /File/Edit/5

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}