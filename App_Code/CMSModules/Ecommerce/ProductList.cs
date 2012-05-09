using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;

using CMS.WorkflowEngine;
using CMS.TreeEngine;
using CMS.GlobalHelper;
using CMS.Ecommerce;
using CMS.CMSHelper;
using CMS.SettingsProvider;


/// <summary>
/// Web service - methods for Product List silverlight application.
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class ProductList : System.Web.Services.WebService
{
    /// <summary>
    /// Returns all products from the specified path.
    /// </summary>
    /// <param name="path">Path the products should be selected from</param>
    [WebMethod]
    public string GetProducts(string path)
    {
        // Ensure path the products should be selected from
        if (path == "")
        {
            path = "/Products";
        }
        path += "/%"; 

        // Product types which should be selected
        string productTypes = "cmsproduct.cellphone;cmsproduct.camera;cmsproduct.laptop;cmsproduct.computer;cmsproduct.mediaplayer;cmsproduct.pda;";

        // Get documents which represents products 
        DataSet ds = TreeHelper.GetDocuments(CMSContext.CurrentSiteName, path, TreeProvider.ALL_CULTURES, false, productTypes, "", "SKUName", TreeProvider.ALL_LEVELS, true, -1);

        // Build output XML
        StringBuilder sb = new StringBuilder();
        sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
        sb.Append("<Products>");

        // Add products
        if (!DataHelper.DataSourceIsEmpty(ds))
        {      
            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append("<Product>");

                    // Add product name
                    sb.Append("<SKUName>");
                    sb.Append(dr["SKUName"].ToString());
                    sb.Append("</SKUName>");

                    // Add formatted product price
                    sb.Append("<SKUPrice>");
                    sb.Append(dr["SKUPrice"].ToString());
                    sb.Append("</SKUPrice>");

                    // Add product description
                    sb.Append("<SKUDescription>");
                    sb.Append(HTMLHelper.StripTags(dr["SKUDescription"].ToString()).Replace('&',' '));
                    sb.Append("</SKUDescription>");

                    // Add product image full URL
                    sb.Append("<SKUImagePath>");

                    string imgPath = dr["SKUImagePath"].ToString();
                    if (imgPath == "")
                    {
                        // Get default product image
                        imgPath = SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSDefaultProductImageUrl");
                    }
                    sb.Append(URLHelper.GetAbsoluteUrl(imgPath));
                    sb.Append("</SKUImagePath>");

                    sb.Append("</Product>");
                }
            }
        }

        sb.Append("</Products>");

        return sb.ToString();
    }


    /// <summary>
    /// Returns products categories from the current site.
    /// </summary>
    [WebMethod]
    public string GetCategories()
    {
        // Get documents which represents product categories
        DataSet ds = TreeHelper.GetDocuments(CMSContext.CurrentSiteName, "/Products/%", TreeProvider.ALL_CULTURES, false, "cms.menuitem", "", "DocumentName", 1, true, -1);

        // Build output XML
        StringBuilder sb = new StringBuilder();
        sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
        sb.Append("<Categories>");

        // Add categories
        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                sb.Append("<Category>");

                // Add category name
                sb.Append("<CategoryName>");
                sb.Append(dr["DocumentName"].ToString());
                sb.Append("</CategoryName>");

                // Add category path
                sb.Append("<CategoryPath>");
                sb.Append(dr["NodeAliasPath"].ToString());
                sb.Append("</CategoryPath>");

                sb.Append("</Category>");
            }
        }
        sb.Append("</Categories>");

        return sb.ToString();
    }
}
