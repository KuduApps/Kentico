using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSSiteManager_Development_Cultures_Culture_List : SiteManagerPage
{
    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentMaster.Title.TitleText = GetString("Administration-Culture_List.Title");
        CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_Culture/object.png");
        CurrentMaster.Title.HelpTopicName = "cultures_list";
        CurrentMaster.Title.HelpName = "helpTopic";

        // New item link
        string[,] actions = new string[1, 6];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("Administration-Culture_List.NewCulture");
        actions[0, 2] = null;
        actions[0, 3] = ResolveUrl("Culture_New.aspx");
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("Objects/CMS_Culture/add.png");
        CurrentMaster.HeaderActions.Actions = actions;

        UniGridCultures.OnAction += UniGridCultures_OnAction;
        UniGridCultures.OnExternalDataBound += UniGridCultures_OnExternalDataBound;        
    }


    /// <summary>
    /// External data bound handler.
    /// </summary>
    protected object UniGridCultures_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "culturename":
                DataRowView drv = (DataRowView)parameter;

                string name = ValidationHelper.GetString(drv["CultureName"], "");
                string code = ValidationHelper.GetString(drv["CultureCode"], "");

                return string.Format("<img class=\"Image16\" style=\"vertical-align:middle;\" src=\"{0}\" alt=\"{1}\" />&nbsp;{1}", 
                                     UIHelper.GetFlagIconUrl(this.Page, code, "16x16"), 
                                     HTMLHelper.HTMLEncode(name));
        }

        return parameter;
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that threw event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void UniGridCultures_OnAction(string actionName, object actionArgument)
    {
        switch (actionName)
        {
            case "edit":
                URLHelper.Redirect("Culture_Edit_Frameset.aspx?cultureID=" + actionArgument.ToString());
                break;

            case "delete":
                DeleteCulture(ValidationHelper.GetInteger(actionArgument, 0));
                break;

            default:                
                break;
        }
    }


    private void DeleteCulture(int cultureId)
    {
        CultureInfo culture = CultureInfoProvider.GetSafeCulture(cultureId);
        EditedObject = culture;

        string cultureCode = culture.CultureCode;
        DataSet ds = CultureInfoProvider.GetCultureSites(cultureCode);
        if (DataHelper.DataSourceIsEmpty(ds))
        {
            CultureInfoProvider.DeleteCultureInfo(cultureCode);
        }
        else
        {
            lblError.Visible = true;
            lblError.Text += GetString("culture_list.errorremoveculture") + "\n";            
        }
    }

    #endregion
}