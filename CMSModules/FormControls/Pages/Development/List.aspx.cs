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
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_FormControls_Pages_Development_List : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.CurrentMaster.Title.TitleText = GetString("Development-FormUserControl_List.Title");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_FormControl/object.png");
        this.CurrentMaster.Title.HelpTopicName = "form_controls_list";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        // New item link
        string[,] actions = new string[1, 6];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("Development-FormUserControl_List.New");
        actions[0, 2] = null;
        actions[0, 3] = ResolveUrl("New.aspx");
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("Objects/CMS_FormControl/add.png");
        this.CurrentMaster.HeaderActions.Actions = actions;

        // Retrieve WHERE condition according to filter
        drpType.IncludeAllItem = true;
        string where = null;
        if (ValidationHelper.GetInteger(drpType.Value, -1) >= 0)
        {
            where = "UserControlType=" + ((int)drpType.ControlType).ToString();
        }

        grdList.WhereCondition = SqlHelperClass.AddWhereCondition(where, fltName.GetCondition());
        grdList.ZeroRowsText = GetString("general.nodatafound");
        grdList.OnAction += new OnActionEventHandler(grdList_OnAction);
        grdList.OnExternalDataBound += new OnExternalDataBoundEventHandler(grdList_OnExternalDataBound);
    }


    #region "Unigrid events"

    protected object grdList_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        if (sourceName.ToLower() == "controltype")
        {
            if ((parameter != null) && (parameter != DBNull.Value))
            {
                return GetString("formcontrolstype." + FormUserControlInfoProvider.GetTypeEnum(ValidationHelper.GetInteger(parameter, 0)).ToString());
            }
        }

        return null;
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that threw event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void grdList_OnAction(string actionName, object actionArgument)
    {
        switch (actionName)
        {
            case "edit":
                URLHelper.Redirect("Frameset.aspx?controlid=" + actionArgument.ToString());
                break;
            case "delete":
                FormUserControlInfoProvider.DeleteFormUserControlInfo(ValidationHelper.GetInteger(actionArgument, 0));
                break;
        }
    }

    #endregion
}
