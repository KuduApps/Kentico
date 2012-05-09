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

using CMS.LicenseProvider;
using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_Licenses_Pages_License_List : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Setup page title text and image
        this.CurrentMaster.Title.TitleText = GetString("Licenses_License_List.Title");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_LicenseKey/object.png");

        this.CurrentMaster.Title.HelpTopicName = "licenses_list";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        // Prepare the new class header element
        string[,] actions = new string[3, 8];
        actions[0, 0] = "HyperLink";
        actions[0, 1] = GetString("Licenses_License_List.New");
        actions[0, 3] = "~/CMSModules/Licenses/Pages/License_New.aspx";
        actions[0, 5] = GetImageUrl("Objects/CMS_LicenseKey/add.png");
        actions[1, 0] = "HyperLink";
        actions[1, 1] = GetString("license.list.export");
        actions[1, 3] = "~/CMSModules/Licenses/Pages/License_Export_Domains.aspx";
        actions[1, 5] = GetImageUrl("Objects/CMS_LicenseKey/export.png");
        
        // Not supported in 5.0 final
        //actions[2, 1] = GetString("license.list.import");
        //actions[2, 3] = "~/CMSModules/Licenses/Pages/License_Import_Licenses.aspx";
        //actions[2, 5] = GetImageUrl("Objects/CMS_LicenseKey/import.png");

        this.CurrentMaster.HeaderActions.Actions = actions;

        UniGridLicenses.GridView.PageSize = 20;
        UniGridLicenses.OnAction += new OnActionEventHandler(UniGridLicenses_OnAction);
        UniGridLicenses.OnExternalDataBound += new OnExternalDataBoundEventHandler(UniGridLicenses_OnExternalDataBound);
        UniGridLicenses.ZeroRowsText = GetString("general.nodatafound");
    }


    /// <summary>
    /// External data binding handler.
    /// </summary>
    object UniGridLicenses_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "editionname":
                string edition = ValidationHelper.GetString(parameter, "").ToLower();
                try
                {
                    return LicenseHelper.GetEditionName(LicenseKeyInfoProvider.CharToEdition(edition));
                }
                catch
                {
                    return "#UNKNOWN#";
                }

            case "expiration":
                return GetString(Convert.ToString(parameter));

            case "licenseservers":
                int count = ValidationHelper.GetInteger(parameter, -1);
                if (count == LicenseKeyInfo.SERVERS_UNLIMITED)
                {
                    return GetString("general.unlimited");
                }
                else if (count > 0)
                {
                    return count.ToString();
                }
                return String.Empty;
        }
        return parameter;
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that threw event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void UniGridLicenses_OnAction(string actionName, object actionArgument)
    {
        if (actionName == "delete")
        {
            LicenseKeyInfoProvider.DeleteLicenseKeyInfo(Convert.ToInt32(actionArgument));
        }
        else if (actionName == "view")
        {
            URLHelper.Redirect("License_View.aspx?licenseid=" + actionArgument.ToString());
        }
    }
}
