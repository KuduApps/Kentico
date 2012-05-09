using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.Ecommerce;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Pages_Tools_Configuration_Departments_Department_List : CMSDepartmentsPage
{
    #region "Page Events"
    
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // Init Unigrid
        UniGrid.OnAction += new OnActionEventHandler(uniGrid_OnAction);
        UniGrid.OnExternalDataBound += new OnExternalDataBoundEventHandler(UniGrid_OnExternalDataBound);
        UniGrid.ZeroRowsText = GetString("general.nodatafound");

        // Init site selector
        SelectSite.Selector.SelectedIndexChanged += new EventHandler(Selector_SelectedIndexChanged);

        if (!RequestHelper.IsPostBack())
        {
            // Init site selector
            SelectSite.SiteID = SiteFilterStartupValue;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Prepare the new department header element
        string[,] actions = new string[1, 8];
        actions[0, 0] = "HyperLink";
        actions[0, 1] = GetString("Department_List.NewItemCaption");
        actions[0, 3] = "~/CMSModules/Ecommerce/Pages/Tools/Configuration/Departments/Department_New.aspx?siteId=" + SelectSite.SiteID;
        actions[0, 5] = GetImageUrl("Objects/Ecommerce_Department/add.png");
        this.hdrActions.Actions = actions;

        this.CurrentMaster.Title.TitleText = GetString("Department_List.HeaderCaption");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Ecommerce_Department/object.png");
        this.CurrentMaster.Title.HelpTopicName = "departments_list";
        this.CurrentMaster.Title.HelpName = "helpTopic";
        this.CurrentMaster.DisplaySiteSelectorPanel = AllowGlobalObjects;

        InitWhereCondition();
    }


    protected override void OnPreRender(EventArgs e)
    {
        bool both = (SelectSite.SiteID == UniSelector.US_GLOBAL_OR_SITE_RECORD);

        // Hide header actions if (both) selected
        hdrActions.Enabled = !both;
        lblWarnNew.Visible = both;

        base.OnPreRender(e);
        UniGrid.NamedColumns["DepartmentSiteID"].Visible = AllowGlobalObjects;
    }

    #endregion


    #region "Event Handlers"

    protected object UniGrid_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "departmentsiteid":
                return UniGridFunctions.ColoredSpanYesNo(parameter == DBNull.Value);
        }
        return parameter;
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that throws event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void uniGrid_OnAction(string actionName, object actionArgument)
    {
        if (actionName == "edit")
        {
            URLHelper.Redirect("Department_Frameset.aspx?departmentId=" + Convert.ToString(actionArgument) + "&siteId=" + SelectSite.SiteID);
        }
        else if (actionName == "delete")
        {
            DepartmentInfo deptInfoObj = DepartmentInfoProvider.GetDepartmentInfo(ValidationHelper.GetInteger(actionArgument, 0));
            // Nothing to delete
            if (deptInfoObj == null) return;

            // Check permissions
            CheckConfigurationModification(deptInfoObj.DepartmentSiteID);

            if (DepartmentInfoProvider.CheckDependencies(deptInfoObj.DepartmentID))
            {
                lblError.Visible = true;
                lblError.Text = GetString("Ecommerce.DeleteDisabledWithoutEnable");
                return;
            }
            // Delete DepartmentInfo object from database
            DepartmentInfoProvider.DeleteDepartmentInfo(deptInfoObj);
        }
    }


    /// <summary>
    /// Handles the SiteSelector's selection changed event.
    /// </summary>
    protected void Selector_SelectedIndexChanged(object sender, EventArgs e)
    {
        InitWhereCondition();
        UniGrid.ReloadData();

        // Save selected value
        StoreSiteFilterValue(SelectSite.SiteID);
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Creates where condition for UniGrid and reloads it.
    /// </summary>
    private void InitWhereCondition()
    {
        UniGrid.WhereCondition = SelectSite.GetSiteWhereCondition("DepartmentSiteID");
    }

    #endregion
}
