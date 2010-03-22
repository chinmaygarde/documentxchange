using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using DocumentShare.Models;

namespace DocumentShare.Controllers
{
    public class CategoryController : Controller
    {
        ////
        //// GET: /Category/

        //public ActionResult Index()
        //{
        //    return View();
        //}

        ////
        //// GET: /Category/Details/5

        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        ////
        //// GET: /Category/Create

        //public ActionResult Create()
        //{
        //    return View();
        //} 

        //
        // POST: /Category/Create

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                //TODO: Don't know if it is worth the risk, but would be better
                // to check if the person signed on actually owns the document
                int documentId = int.Parse(collection["DocumentId"]);
                string categories = collection["Categories"];
                List<string> categoryList = new List<string>();
                foreach(string category in categories.Split(','))
                {
                    categoryList.Add(category.Trim());
                }
                using (var db = new DocumentDataContext())
                {
                    List<Category> categoriesPresent =  db.getCategoriesForDocument(documentId);
                    foreach (Category c in categoriesPresent)
                    {
                        categoryList.RemoveAll(delegate(string catName) { return catName == c.Name; });
                    }
                    db.addCategoriesToDocument(categoryList, documentId);
                }
                return RedirectToAction("Edit", "Document", new { id = documentId});
            }
            catch
            {
                return View();
            }
        }

        ////
        //// GET: /Category/Edit/5
 
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        ////
        //// POST: /Category/Edit/5

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
