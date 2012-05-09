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

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.ExtendedControls;
using CMS.UIControls;
using CMS.Ecommerce;

public partial class CMSModules_Ecommerce_Pages_Tools_Configuration_Configuration_Frameset : CMSEcommerceSharedConfigurationPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string headerUrl = "Configuration_Header.aspx";
        if (this.SiteID != CMSContext.CurrentSiteID)
        {
            headerUrl += "?SiteId=" + this.SiteID.ToString();
        }

        configHeader.Attributes["src"] = headerUrl;
        if (CultureHelper.IsUICultureRTL())
        {
            ControlsHelper.ReverseFrames(this.colsFrameset);
        }
    }
}
