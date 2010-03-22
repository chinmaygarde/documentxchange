using System.Web.Mvc;

namespace DocumentShare.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string DocumentFileUrl(this UrlHelper helper, string fileName)
        {
            if(string.IsNullOrEmpty(fileName))
                fileName = "Default";
            return helper.Content(string.Format("~/Content/FileContent/{0}", fileName));
        }
    }
}