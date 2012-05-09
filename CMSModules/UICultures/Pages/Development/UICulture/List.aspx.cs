using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_UICultures_Pages_Development_UICulture_List : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        InitializeMasterPage();

        string url = "http://www.kentico.com/Download/Localization-Packs.aspx";
        string downloadPage = string.Format(@"<a href=""{0}"" target=""_blank"" >{1}</a> ", url, HTMLHelper.HTMLEncode(url));
        ShowInformation(string.Format(GetString("uiculture.culturedownload"), downloadPage));        

        UniGridUICultures.OnAction += UniGridUICultures_OnAction;
        UniGridUICultures.OnExternalDataBound += UniGridUICultures_OnExternalDataBound;        
    }


    /// <summary>
    /// External data bound handler.
    /// </summary>
    protected object UniGridUICultures_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName)
        {
            case "culturename":
                DataRowView drv = (DataRowView)parameter;

                string name = ValidationHelper.GetString(drv["UICultureName"], string.Empty);
                string code = ValidationHelper.GetString(drv["UICultureCode"], string.Empty);

                if (code == CultureHelper.DefaultUICulture)
                {
                    name = string.Concat(name, " ", ResHelper.GetString("General.DefaultChoice"));                    
                }

                return string.Format("<img class=\"Image16\" style=\"vertical-align:middle;\" src=\"{0}\" alt=\"{1}\" />&nbsp;{1}", 
                    UIHelper.GetFlagIconUrl(this, code, "16x16"), 
                    HTMLHelper.HTMLEncode(name));

            default:
                return parameter;
        }        
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that threw event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void UniGridUICultures_OnAction(string actionName, object actionArgument)
    {
        switch (actionName)
        {
            case "edit":
                URLHelper.Redirect("Frameset.aspx?uicultureid=" + actionArgument.ToString());
                break;

            case "delete":
                DeleteUICulture(ValidationHelper.GetInteger(actionArgument, 0));
                break;

            default:
                break;
        }
    }


    protected void InitializeMasterPage()
    {
        // Set actions
        string[,] actions = new string[1, 8];
        actions[0, 0] = "HyperLink";
        actions[0, 1] = GetString("Development-UICulture_List.NewUICulture");
        actions[0, 3] = ResolveUrl("New.aspx");
        actions[0, 5] = GetImageUrl("Objects/CMS_UICulture/add.png");

        CurrentMaster.HeaderActions.Actions = actions;

        // Set help
        CurrentMaster.Title.HelpTopicName = "ui_cultures_list";
        CurrentMaster.Title.HelpName = "helpTopic";
    }


    private void DeleteUICulture(int cultureId)
    {
        UICultureInfo culture = UICultureInfoProvider.GetSafeUICulture(cultureId);
        EditedObject = culture;        

        if (!string.Equals(culture.UICultureCode, CultureHelper.DefaultUICulture, StringComparison.OrdinalIgnoreCase))
        {
            // Delete UI culture object if it is not the default one
            UICultureInfoProvider.DeleteUICultureInfo(culture);
        }
        else
        {
            ShowError(string.Format(GetString("Development-UICulture_List.DeleteError"), culture.UICultureName));            
        }
    }
}