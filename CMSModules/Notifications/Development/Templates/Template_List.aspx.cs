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

using CMS.SiteProvider;
using CMS.GlobalHelper;
using CMS.Notifications;
using CMS.UIControls;

public partial class CMSModules_Notifications_Development_Templates_Template_List : CMSNotificationsPage
{
    #region "Page Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Process site ID parameter if supplied
        string siteId = QueryHelper.GetString("siteid", "");
        if (!URLHelper.IsPostback())
        {
            this.siteSelector.Value = siteId;
        }

        // Initialize the grid view        
        this.gridTemplates.OnAction += new OnActionEventHandler(gridTemplates_OnAction);
        this.gridTemplates.OrderBy = "TemplateDisplayName";
        this.gridTemplates.ZeroRowsText = GetString("general.nodatafound");
        
        string where = "";
        int siteid = ValidationHelper.GetInteger(this.siteSelector.Value, 0);
        if (siteid > 0)
        {
            where = "TemplateSiteID = " + siteid.ToString();
        }
        else
        {
            where = "TemplateSiteID IS NULL";
        }
        this.gridTemplates.WhereCondition = where;

        // Prepare the new class header element
        string[,] actions = new string[1, 11];
        actions[0, 0] = "HyperLink";
        actions[0, 1] = GetString("notifications.template_edit.new");
        actions[0, 3] = "javascript: AddNewItem();";
        actions[0, 5] = GetImageUrl("Objects/Notification_Template/add.png");

        this.CurrentMaster.HeaderActions.Actions = actions;
        this.CurrentMaster.DisplaySiteSelectorPanel = true;

        this.siteSelector.DropDownSingleSelect.AutoPostBack = true;
        this.siteSelector.UniSelector.SpecialFields = new string[,] { { GetString("general.global"), "" } };
        this.siteSelector.UniSelector.OnSelectionChanged += new EventHandler(UniSelector_OnSelectionChanged);
        this.siteSelector.IsLiveSite = false;        
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        ScriptHelper.RegisterClientScriptBlock(this.Page, typeof(string), "AddNewItem", ScriptHelper.GetScript(
            "function AddNewItem() { this.window.location = '" + ResolveUrl("~/CMSModules/Notifications/Development/Templates/Template_New.aspx?siteid=" + this.siteSelector.Value) + "'; }"));
    }


    protected void UniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        this.gridTemplates.ReloadData();
        this.pnlUpdate.Update();
    }

    #endregion


    #region "UniGrid Events"

    private void gridTemplates_OnAction(string actionName, object actionArgument)
    {
        // Get template id
        int templateId = ValidationHelper.GetInteger(actionArgument, 0);

        switch (actionName.ToLower())
        {
            case "edit":
                URLHelper.Redirect("Template_Edit.aspx?templateid=" + templateId.ToString() + "&siteid=" + this.siteSelector.Value.ToString());
                break;

            case "delete":
                NotificationTemplateInfoProvider.DeleteNotificationTemplateInfo(templateId);
                break;
        }
    }

    #endregion
}
