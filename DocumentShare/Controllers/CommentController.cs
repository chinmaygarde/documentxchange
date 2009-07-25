using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using DocumentShare.Models;

namespace DocumentShare.Controllers
{
    public class CommentController : Controller
    {
        ////
        //// GET: /Comment/

        //public ActionResult Index()
        //{
        //    return View();
        //}

        ////
        //// GET: /Comment/Details/5

        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        ////
        //// GET: /Comment/Create
        //[Authorize]
        //public ActionResult Create()
        //{
        //    return View();
        //} 

        //
        // POST: /Comment/Create

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                using (var db = new DocumentDataContext())
                {
                    Comment comment = new Comment
                    {
                        DocumentId = int.Parse(collection["DocumentId"]),
                        CreatedAt = DateTime.Now,
                        Description = collection["Description"],
                        UserId = db.getUserIdForUserName(User.Identity.Name)
                    };
                    db.insertComment(comment);
                }
                return RedirectToAction("Details", "Document", new { id = collection["DocumentId"]});                
            }
            catch
            {
                return View();
            }
        }

        ////
        //// GET: /Comment/Edit/5
 
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        ////
        //// POST: /Comment/Edit/5

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
