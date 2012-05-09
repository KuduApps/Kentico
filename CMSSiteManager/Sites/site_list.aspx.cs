using System;
using System.Data;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.CMSHelper;

public partial class CMSSiteManager_Sites_site_list : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string title = GetString("Site_List.Title");

        // Setup page title text and image
        CurrentMaster.Title.TitleText = title;
        CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_Site/object.png");

        CurrentMaster.Title.HelpTopicName = "sites_list";
        CurrentMaster.Title.HelpName = "helpTopic";

        // Prepare the new class header element
        string[,] actions = new string[3, 8];
        actions[0, 0] = "HyperLink";
        actions[0, 1] = GetString("Site_List.NewSite");
        actions[0, 3] = "~/CMSModules/ImportExport/SiteManager/Site_New.aspx";
        actions[0, 5] = GetImageUrl("Objects/CMS_Site/add.png");
        actions[1, 0] = "HyperLink";
        actions[1, 1] = GetString("Site_List.ImportSite");
        actions[1, 3] = "~/CMSModules/ImportExport/SiteManager/ImportSite.aspx";
        actions[1, 5] = GetImageUrl("CMSModules/CMS_Sites/importsite.png");
        actions[2, 0] = "HyperLink";
        actions[2, 1] = GetString("Site_List.ExportSettings");
        actions[2, 3] = "~/CMSModules/ImportExport/SiteManager/ExportObjects.aspx";
        actions[2, 5] = GetImageUrl("CMSModules/CMS_Sites/exportobjects.png");

        CurrentMaster.HeaderActions.Actions = actions;

        UniGridSites.OnAction += UniGridSites_OnAction;
        UniGridSites.OnExternalDataBound += UniGridSites_OnExternalDataBound;
        UniGridSites.ZeroRowsText = GetString("general.nodatafound");

        lblErrorMsg.Visible = false;
    }


    /// <summary>
    /// External data binding handler.
    /// </summary>
    object UniGridSites_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        bool running = true;
        switch (sourceName.ToLower())
        {
            case "editcontent":
                // Edit content action
                running = ValidationHelper.GetString(((DataRowView)((GridViewRow)parameter).DataItem).Row["sitestatus"], "").ToUpper() == SiteInfoProvider.SiteStatusToString(SiteStatusEnum.Running);
                if (!running)
                {
                    ImageButton button = ((ImageButton)sender);
                    button.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/Editcontentdisabled.png");
                    button.Enabled = false;
                }
                break;

            case "openlivesite":
                // Open live site action
                running = ValidationHelper.GetString(((DataRowView)((GridViewRow)parameter).DataItem).Row["sitestatus"], "").ToUpper() == SiteInfoProvider.SiteStatusToString(SiteStatusEnum.Running);
                if (!running)
                {
                    ImageButton button = ((ImageButton)sender);
                    button.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/Livesitedisabled.png");
                    button.Enabled = false;
                }
                break;

            case "sitestatus":
                // Status
                {
                    DataRowView drv = (DataRowView)parameter;
                    running = SiteInfoProvider.SiteStatusToEnum(ValidationHelper.GetString(drv["SiteStatus"], "")) == SiteStatusEnum.Running;
                    bool offline = ValidationHelper.GetBoolean(drv["SiteIsOffline"], false);

                    if (running)
                    {
                        if (offline)
                        {
                            return UniGridFunctions.SpanMsg(GetString("Site_List.Offline"), "SiteStatusOffline");
                        }
                        else
                        {
                            return UniGridFunctions.SpanMsg(GetString("Site_List.Running"), "SiteStatusRunning");
                        }
                    }
                    else
                    {
                        return UniGridFunctions.SpanMsg(GetString("Site_List.Stopped"), "SiteStatusStopped");
                    }
                }

            case "culture":
                // Culture
                {
                    DataRowView drv = (DataRowView)parameter;
                    string siteName = ValidationHelper.GetString(drv["SiteName"], "");
                    string cultureCode = CultureHelper.GetDefaultCulture(siteName);
                    return UniGridFunctions.DocumentCultureFlag(cultureCode, null, Page);
                }
        }
        return parameter;
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that throws event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void UniGridSites_OnAction(string actionName, object actionArgument)
    {
        SiteInfo si = SiteInfoProvider.GetSiteInfo(ValidationHelper.GetInteger(actionArgument, 0));
        if (si != null)
        {
            string siteName = si.SiteName;

            switch (actionName)
            {
                case "editSite":
                    URLHelper.Redirect("Site_Edit_Frameset.aspx?siteid=" + actionArgument);
                    break;

                case "delete":
                    URLHelper.Redirect("site_delete.aspx?siteid=" + actionArgument);
                    break;

                case "editContent":
                    {
                        // Make url for site in form 'http(s)://sitedomain/application/cmsdesk'.
                        string sitedomain = si.DomainName.TrimEnd('/');

                        string application = null;
                        // Support of multiple web sites on single domain
                        if (!sitedomain.Contains("/"))
                        {
                            application = ResolveUrl("~/.").TrimEnd('/');
                        }

                        // Application includes string '/cmsdesk'.
                        application += "/cmsdesk/";

                        string url = URLHelper.Url.Scheme + "://" + sitedomain + application;

                        ltlScript.Text = ScriptHelper.GetScript("window.open('" + url + "');");
                    }
                    break;

                case "openLiveSite":
                    {
                        // Make url for site in form 'http(s)://sitedomain/application'.
                        string sitedomain = si.DomainName.TrimEnd('/');

                        string application = null;
                        // Support of multiple web sites on single domain
                        if (!sitedomain.Contains("/"))
                        {
                            application = ResolveUrl("~/.").TrimEnd('/');
                        }
                        string url = URLHelper.Url.Scheme + "://" + sitedomain + application + "/";

                        ltlScript.Text = ScriptHelper.GetScript("window.open('" + url + "');");
                    }
                    break;

                case "start":
                    try
                    {
                        SiteInfoProvider.RunSite(siteName);
                    }
                    catch (Exception ex)
                    {
                        lblErrorMsg.Visible = true;
                        lblErrorMsg.Text = GetString("Site_List.ErrorMsg") + " " + ex.Message;
                    }
                    break;

                case "stop":
                    SiteInfoProvider.StopSite(siteName);
                    SessionManager.Clear(siteName);
                    break;

                case "export":
                    URLHelper.Redirect("~/CMSModules/ImportExport/SiteManager/ExportSite.aspx?siteid=" + actionArgument);
                    break;
            }
        }
    }
}
