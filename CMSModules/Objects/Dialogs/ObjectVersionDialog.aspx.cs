using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.DataEngine;
using CMS.CMSHelper;
using CMS.ExtendedControls;
using CMS.LicenseProvider;
using CMS.SettingsProvider;

public partial class CMSModules_Objects_Dialogs_ObjectVersionDialog : CMSModalDesignPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        // Check the license
        if (DataHelper.GetNotEmpty(URLHelper.GetCurrentDomain(), "") != "")
        {
            LicenseHelper.CheckFeatureAndRedirect(URLHelper.GetCurrentDomain(), FeatureEnum.ObjectVersioning);
        }
        
        if (!QueryHelper.ValidateHash("hash"))
        {
            RedirectToAccessDenied(ResHelper.GetString("dialogs.badhashtitle"));
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Get query string parameters
        string objectType = QueryHelper.GetString("objecttype", String.Empty);
        int objectId = QueryHelper.GetInteger("objectid", 0);

        // Set version list control
        versionList.ObjectID = objectId;
        versionList.ObjectType = objectType;
        versionList.IsLiveSite = false;

        btnClose.Text = GetString("General.Close");

        // Register refresh script to refresh wopener
        StringBuilder script = new StringBuilder();
        script.Append(@"
function RefreshContent() {
  if(wopener != null) {
    if (wopener.RefreshPage != null) {
      wopener.RefreshPage();
    }
    else if (wopener.Refresh != null) {
      wopener.Refresh();
    }
  }
}");
        // Register script
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "WOpenerRefresh", ScriptHelper.GetScript(script.ToString()));
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        string title = String.Format(GetString("objectversioning.objectversiondialog.title"), GetString("objecttype." + versionList.ObjectType.Replace(".", "_")), (versionList.Object != null) ? HTMLHelper.HTMLEncode(versionList.Object.ObjectDisplayName) : String.Empty);
        
        // Set title and close button
        SetTitle("CMSModules/CMS_ObjectVersioning/ViewVersion.png", title, "objectversioning_general", "helpTopic");
    }
}
