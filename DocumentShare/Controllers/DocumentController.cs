using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using DocumentShare.Models;
using System.Data.Linq;

namespace DocumentShare.Controllers
{
    public class DocumentController : Controller
    {
        //
        // GET: /Document/

        public ActionResult Index()
        {
            DocumentViewModel model = new DocumentViewModel();
            using (var db = new DocumentDataContext())
            {
                DataLoadOptions options = new DataLoadOptions();
                options.LoadWith<Document>(u => u.User);
                db.LoadOptions = options;

                model.Documents = db.getAllDocuments();

            }
            return View(model);
        }

        //
        // GET: /Document/Details/5

        public ActionResult Details(int id)
        {
            DocumentViewModel model = new DocumentViewModel();
            using (var db = new DocumentDataContext())
            {
                DataLoadOptions options = new DataLoadOptions();
                options.LoadWith<Document>(d => d.User);
                options.LoadWith<Document>(d => d.Comments);
                options.LoadWith<Comment>(d => d.User);
                options.LoadWith<Document>(d => d.CategoryDocuments);
                options.LoadWith<CategoryDocument>(d => d.Category);
                db.LoadOptions = options;
                model.CurrentDocument = db.getDocument(id);
            }
            return View(model);
        }

        //
        // GET: /Document/Create

        [Authorize]
        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Document/Create
        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                using (var db = new DocumentDataContext())
                {
                    Document document = new Document 
                    {
                        Title = collection["Title"].ToString(),
                        Description = collection["Description"].ToString(),
                        CreatedAt = DateTime.Now,
                        UserId = db.getUserIdForUserName(User.Identity.Name)
                    };
                    db.Documents.InsertOnSubmit(document);
                    db.SubmitChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Document/Edit/5
 
        [Authorize]
        public ActionResult Edit(int id)
        {
            DocumentViewModel model = new DocumentViewModel();
            using (var db = new DocumentDataContext())
            {
                DataLoadOptions options = new DataLoadOptions();
                options.LoadWith<Document>(d => d.User);
                db.LoadOptions = options;
                model.CurrentDocument = db.getDocument(id);
            }

            //If the User tries to get smart with the query string
            if (model.CurrentDocument.User.UserName == User.Identity.Name)
                return View(model);
            else
                return RedirectToAction("Index");
                                
        }

        //
        // POST: /Document/Edit/5

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                using (var db = new DocumentDataContext())
                {
                    Document doc = db.getDocument(id);
                    //Check to see if the user who made the post was the user who created the document                    
                    if (doc.User.UserName != User.Identity.Name)
                        return RedirectToAction("Index");

                    doc.Title = collection["Title"];
                    doc.Description = collection["Description"];

                    db.updateDocument(doc);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
