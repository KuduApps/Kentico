using System.Web.Mvc;
using System.Data;

using CMS.CMSHelper;
using CMS.TreeEngine;
using CMS.GlobalHelper;
using CMS.URLRewritingEngine;

namespace CMS.Controllers.Global
{
    /// <summary>
    /// Sample controller for the news.
    /// </summary>
    public class NewsMVCController : Controller
    {
        /// <summary>
        /// Process the detail action.
        /// </summary>
        public ActionResult Detail()
        {
            // Prepare the data for view
            TreeNode document = TreeHelper.GetDocument(CMSContext.CurrentSiteName, "/News/" + RouteData.Values["id"], CMSContext.PreferredCultureCode, true, "CMS.News", true);
            if (document != null)
            {
                document.SetValue("NewsTitle", document.GetValue("NewsTitle"));
                ViewData["Document"] = document;
            }
            else
            {
                // Document not found
                URLRewriter.PageNotFound();
                return null;
            }

            return View();
        }


        /// <summary>
        /// Process the list action.
        /// </summary>
        public ActionResult List()
        {
            // Prepare the data for view
            var documents = TreeHelper.GetDocuments(CMSContext.CurrentSiteName, "/News/%", CMSContext.PreferredCultureCode, true, "CMS.News", null, "NewsReleaseDate DESC", -1, true, 0);
            if (documents != null)
            {
                ViewData["NewsList"] = documents;
            }

            return View();
        }
    }
}
