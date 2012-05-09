using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.CMSHelper;
using System.Data;
using CMS.SiteProvider;
using CMS.GlobalHelper;

public partial class CMSAdminControls_UI_HeaderLinks : CMSUserControl
{
    /// <summary>
    /// URL to which the page should redirect
    /// </summary>
    public string RedirectURL 
    { 
        get; 
        set; 
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        CheckUICultureChange();

        if (!CMSContext.CurrentUser.UserSiteManagerAdmin)
        {
            this.plcLinks.Visible = false;
            return;
        }
        
        lnkLog.NavigateUrl = ResolveUrl("~/CMSModules/EventLog/EventLog.aspx");
        lnkDebug.NavigateUrl = ResolveUrl("~/CMSModules/System/Debug/System_DebugFrameset.aspx");
    }


    /// <summary>
    /// Check if UI changes was made.
    /// </summary>
    private void CheckUICultureChange()
    {
        DataSet ds = UICultureInfoProvider.GetUICultures(String.Empty, String.Empty, 0, "COUNT (UICultureID)");

        // Show selector only if there are more UI cultures than one
        if (!DataHelper.DataSourceIsEmpty(ds) && (ValidationHelper.GetInteger(ds.Tables[0].Rows[0][0], 0) > 1))
        {
            ucUICultures.ButtonImage = "";
            ucUICultures.LinkDialog.ImageUrl = URLHelper.ResolveUrl("~/App_Themes/Default/Images/Objects/CMS_Site/list.png");
            ucUICultures.LinkDialog.ToolTip = GetString("uicultures.change");

            string cultureName = ValidationHelper.GetString(ucUICultures.Value, String.Empty);
            if (cultureName != "")
            {
                CMSContext.CurrentUser.PreferredUICultureCode = cultureName;
                UserInfoProvider.SetUserInfo(CMSContext.CurrentUser);

                // Set selected UI culture and refresh all pages
                CultureHelper.SetPreferredUICulture(cultureName);

                if (!String.IsNullOrEmpty(RedirectURL))
                {
                    URLHelper.Redirect(RedirectURL);
                }
            }
        }
        else
        {
            pnlCultures.Visible = false;
        }
    }
}
