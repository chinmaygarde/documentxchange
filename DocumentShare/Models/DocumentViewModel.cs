using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentShare.Models
{
    public class DocumentViewModel
    {
        public List<Document> Documents;
        public Document CurrentDocument;

        public DocumentViewModel()
        {
            this.Documents = new List<Document>();
            this.CurrentDocument = new Document();
        }
    }
}
