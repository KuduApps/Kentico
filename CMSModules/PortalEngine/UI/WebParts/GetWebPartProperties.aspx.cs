using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;

using CMS.GlobalHelper;
using CMS.PortalEngine;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.FormEngine;
using CMS.UIControls;
using CMS.PortalControls;

public partial class CMSModules_PortalEngine_UI_WebParts_GetWebPartProperties : CMSDeskPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string aliasPath = QueryHelper.GetString("aliaspath", "");
        string webpartId = QueryHelper.GetString("webpartid", "");
        string zoneId = QueryHelper.GetString("zoneid", "");
        Guid webpartGuid = QueryHelper.GetGuid("webpartguid", Guid.Empty);

        // Get page info
        PageInfo pi = PageInfoProvider.GetPageInfo(CMSContext.CurrentSiteName, aliasPath, CMSContext.PreferredCultureCode, null, CMSContext.CurrentSite.CombineWithDefaultCulture);
        if (pi != null)
        {
            // Get template
            PageTemplateInfo pti = pi.GetInheritedTemplateInfo(CMSContext.PreferredCultureCode, CMSContext.CurrentSite.CombineWithDefaultCulture);

            // Get web part
            WebPartInstance webPart = pti.GetWebPart(webpartGuid, webpartId);
            if (webPart != null)
            {
                StringBuilder sb = new StringBuilder();
                Hashtable properties = webPart.Properties;

                // Get the webpart object
                WebPartInfo wi = WebPartInfoProvider.GetWebPartInfo(webPart.WebPartType);
                if (wi != null)
                {
                    // Add the header
                    sb.Append("Webpart properties (" + wi.WebPartDisplayName + ")" + Environment.NewLine + Environment.NewLine);
                    sb.Append("Alias path: " + aliasPath + Environment.NewLine);
                    sb.Append("Zone ID: " + zoneId + Environment.NewLine + Environment.NewLine);


                    string wpProperties = "<default></default>";
                    // Get the form info object and load it with the data

                    if (wi.WebPartParentID > 0)
                    {
                        // Load parent properties
                        WebPartInfo wpi = WebPartInfoProvider.GetWebPartInfo(wi.WebPartParentID);
                        if (wpi != null)
                        {
                            wpProperties = wpi.WebPartProperties;
                        }
                    }
                    else
                    {
                        wpProperties = wi.WebPartProperties;
                    }

                    FormInfo fi = new FormInfo(wpProperties);

                    // General properties of webparts
                    string beforeFormDefinition = PortalHelper.GetWebPartProperties((WebPartTypeEnum)wi.WebPartType, PropertiesPosition.Before);
                    string afterFormDefinition = PortalHelper.GetWebPartProperties((WebPartTypeEnum)wi.WebPartType, PropertiesPosition.After);

                    // General properties before custom
                    if (!String.IsNullOrEmpty(beforeFormDefinition))
                    {
                        // Load before properties
                        FormInfo bfi = new FormInfo(beforeFormDefinition);
                        bfi.UpdateExistingFields(fi);
                        sb.Append(Environment.NewLine + "Default" + Environment.NewLine + Environment.NewLine + Environment.NewLine);
                        sb.Append(GetProperties(bfi.GetFormElements(true, false), webPart));
                    }

                    // Generate custom properties
                    sb.Append(GetProperties(fi.GetFormElements(true, false), webPart));

                    // General properties after custom
                    if (!String.IsNullOrEmpty(afterFormDefinition))
                    {
                        FormInfo afi = new FormInfo(afterFormDefinition);
                        // Load before properties
                        afi.UpdateExistingFields(fi);

                        sb.Append(GetProperties(afi.GetFormElements(true, false), webPart));
                    }

                    // Send the text file to the user to download
                    UTF8Encoding enc = new UTF8Encoding();
                    byte[] file = enc.GetBytes(sb.ToString());

                    Response.AddHeader("Content-disposition", "attachment; filename=webpartproperties_" + webPart.ControlID + ".txt");
                    Response.ContentType = "text/plain";
                    Response.BinaryWrite(file);

                    RequestHelper.EndResponse();
                }
            }
        }
    }


    /// <summary>
    /// Exports properties given in the array list.
    /// </summary>
    /// <param name="elem">List of properties to export</param>
    /// <param name="webPart">WebPart instance with data</param>
    private string GetProperties(ArrayList elem, WebPartInstance webPart)
    {
        StringBuilder sb = new StringBuilder();

        // Iterate through all fields
        foreach (object o in elem)
        {
            FormCategoryInfo fci = o as FormCategoryInfo;
            if (fci != null)
            {
                // We have category now
                sb.Append(Environment.NewLine + fci.CategoryCaption + Environment.NewLine + Environment.NewLine + Environment.NewLine);
            }
            else
            {
                // Form element
                FormFieldInfo ffi = o as FormFieldInfo;
                if (ffi != null)
                {
                    object value = webPart.GetValue(ffi.Name);
                    sb.Append(ffi.Caption + ": " + (value == null ? "" : value.ToString()) + Environment.NewLine + Environment.NewLine);
                }
            }
        }

        return sb.ToString();
    }
}
