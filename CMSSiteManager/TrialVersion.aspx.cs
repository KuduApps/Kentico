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
using CMS.UIControls;
using CMS.CMSHelper;

public partial class CMSSiteManager_TrialVersion :  CMSDeskPage
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        this.RequireSite = false;
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        string info = null;

        if (Request.QueryString["appexpires"] != null)
        {
            // Application expires
            int appExpiration = QueryHelper.GetInteger("appexpires", 0);
            if (CMSContext.SYSTEM_VERSION_SUFFIX.Contains("BETA"))
            {
                if (appExpiration <= 0)
                {
                    info = GetString("Beta.AppExpired");
                }
                else
                {
                    info = string.Format(GetString("Beta.AppExpiresIn"), appExpiration);
                }
            }
            else
            {
                if (appExpiration <= 0)
                {
                    info = string.Format(GetString("Preview.AppExpired"), CMSContext.SYSTEM_VERSION_SUFFIX);
                }
                else
                {
                    info = string.Format(GetString("Preview.AppExpiresIn"), CMSContext.SYSTEM_VERSION_SUFFIX, appExpiration);
                }
            }
        }
        else
        {
            // Trial version expiration date
            int expiration = QueryHelper.GetInteger("expirationdate", 0);
            if (expiration <= 0)
            {
                info = GetString("Trial.Expired");
            }
            else
            {
                info = string.Format(GetString("Trial.ExpiresIn"), expiration);
            }
        }

        ltlText.Text = info;
    }
}
