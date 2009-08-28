using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentShare.Models
{
    public partial class DocumentDataContext
    {
        public List<Document> getAllDocuments()
        {
            var documents = from document in this.Documents
                            orderby document.CreatedAt descending
                            select document;
            return Documents.ToList();
        }
        public Document getDocument(int id)
        {
            return this.Documents.SingleOrDefault(d => d.Id == id);
        }
        public void insertDocument(Document document)
        {
            this.Documents.InsertOnSubmit(document);
            this.SubmitChanges();
        }
        public void deleteDocument(int documentId)
        {
            var document = from doc in this.Documents
                           where doc.Id == documentId
                           select doc;
            this.Documents.DeleteOnSubmit(document.First());
            this.SubmitChanges();
        }
        public void updateDocument(Document updatedDocument)
        {
            Document document = this.Documents.SingleOrDefault(d => d.Id == updatedDocument.Id);
            document.Title = updatedDocument.Title;
            document.Description = updatedDocument.Description;
            this.SubmitChanges();
        }
        public Guid getUserIdForUserName(string userName)
        {
            return getUserForUserName(userName).UserId;
        }
        public User getUserForUserName(string username)
        {
            var users = from user in this.Users
                        where user.UserName == username
                        select user;
            return users.First();
        }
        /*public Document getDocument(int id)
        {
            var dcouments = from d in this.Documents
                            where d.Id == id
                            select d;
            return dcouments.First();
        }*/
        public void insertComment(Comment comment)
        {
            this.Comments.InsertOnSubmit(comment);
            this.SubmitChanges();
        }

        public List<Category> getCategoriesForDocument(int documentId)
        {
            List<CategoryDocument> catDocs = (from c in this.CategoryDocuments
                          where c.DocumentId == documentId
                          select c).ToList();
            List<Category> categoryList = new List<Category>();
            foreach (CategoryDocument cd in catDocs)
            {
                categoryList.Add(cd.Category);
            }
            return categoryList;
        }
        //TODO: Refactor
        public void addCategoriesToDocument(List<string> categoryList, int documentId)
        {
            List<Category> allCategories = (
                                            from c in this.Categories
                                            select c
                                            ).ToList();
            List<string> allCategoryNames = new List<string>();
            foreach(Category cat in allCategories)
            {
                allCategoryNames.Add(cat.Name);
            }
            foreach (string categoryToBeAdded in categoryList)
            {
                if (allCategoryNames.Contains(categoryToBeAdded))
                {
                    //No need to add the category. Just add the association
                    Category category = allCategories.Find(delegate(Category c) { return c.Name == categoryToBeAdded; });
                    CategoryDocument catDoc = new CategoryDocument { CategoryId = category.Id, DocumentId = documentId };
                    CategoryDocuments.InsertOnSubmit(catDoc);
                    this.SubmitChanges();
                }
                else
                {
                    //The category does not already exist. Add a new category object too.
                    Category category = new Category { Name = categoryToBeAdded };
                    Categories.InsertOnSubmit(category);
                    this.SubmitChanges();
                    Category insertedCategory = (from c in this.Categories
                                                 where c.Name == categoryToBeAdded
                                                 select c).First();
                    CategoryDocument catDoc = new CategoryDocument { DocumentId = documentId, CategoryId = insertedCategory.Id };
                    CategoryDocuments.InsertOnSubmit(catDoc);
                    this.SubmitChanges();
                }
            }
        }
    }
}
