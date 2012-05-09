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
using CMS.Ecommerce;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Pages_Tools_Configuration_Departments_Department_Header : CMSDepartmentsPage
{
    protected int departmentId = 0;
    protected int editedSiteId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        departmentId = QueryHelper.GetInteger("departmentId", 0);

        CMSMasterPage master = (CMSMasterPage)this.CurrentMaster;

        string currentDepartmentName = string.Empty;
        DepartmentInfo di = DepartmentInfoProvider.GetDepartmentInfo(departmentId);

        if (di != null)
        {
            editedSiteId = di.DepartmentSiteID;

            // Check site id
            CheckEditedObjectSiteID(editedSiteId);
            currentDepartmentName = ResHelper.LocalizeString(di.DepartmentDisplayName);
        }

        // initializes page title
        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = GetString("Department_Edit.ItemListLink");
        breadcrumbs[0, 1] = "~/CMSModules/Ecommerce/Pages/Tools/Configuration/Departments/Department_List.aspx?siteId=" + SiteID;
        breadcrumbs[0, 2] = "configEdit";
        breadcrumbs[1, 0] = FormatBreadcrumbObjectName(currentDepartmentName, editedSiteId);
        breadcrumbs[1, 1] = "";
        breadcrumbs[1, 2] = "";

        master.Title.Breadcrumbs = breadcrumbs;
        master.Title.TitleText = GetString("Department_Edit.HeaderCaption");
        master.Title.TitleImage = GetImageUrl("Objects/Ecommerce_Department/object.png");

        master.Title.HelpTopicName = "new_department";
        master.Title.HelpName = "helpTopic";

        master.Tabs.ModuleName = "CMS.Ecommerce";
        master.Tabs.ElementName = "Configuration.Departments";
        master.Tabs.UrlTarget = "content";
        master.Tabs.OnTabCreated += new UITabs.TabCreatedEventHandler(Tabs_OnTabCreated);
    }


    string[] Tabs_OnTabCreated(CMS.SiteProvider.UIElementInfo element, string[] parameters, int tabIndex)
    {
        // Add SiteId parameter to each tab
        if (parameters.Length > 2)
        {
            parameters[2] = URLHelper.AddParameterToUrl(parameters[2], "siteId", SiteID.ToString());
        }

        return parameters;
    }
}
