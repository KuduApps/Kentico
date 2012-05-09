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
using CMS.CMSHelper;

public partial class CMSModules_TagGroups_Pages_Development_TagGroup_List : SiteManagerPage
{
    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Get site id
        int siteId = QueryHelper.GetInteger("siteid", CMSContext.CurrentSiteID);

        if (!URLHelper.IsPostback())
        {
            this.siteSelector.Value = siteId.ToString();
        }

        // Setup page title text and image
        this.CurrentMaster.Title.TitleText = GetString("tags.taggroup_list.title");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_TagGroup/object.png");
        
        this.CurrentMaster.DisplaySiteSelectorPanel = true;

        // Initialize the grid view      
        this.gridTagGroups.OnAction += new OnActionEventHandler(gridTagGroups_OnAction);
        this.gridTagGroups.GridName = "~/CMSModules/TagGroups/Pages/Development/TagGroup_List.xml";
        this.gridTagGroups.OrderBy = "TagGroupDisplayName";
        this.gridTagGroups.ZeroRowsText = GetString("general.nodatafound");
        this.gridTagGroups.WhereCondition = "(TagGroupIsAdHoc = 0)";

        if (this.siteSelector.UniSelector.HasData)
        {
            int selectSiteId = ValidationHelper.GetInteger(this.siteSelector.Value, 0);
            if (selectSiteId > 0)
            {
                this.gridTagGroups.WhereCondition += " AND (TagGroupSiteID = " + selectSiteId + ")";
            }
        }

        this.CurrentMaster.Title.HelpTopicName = "taggroups_list";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        // Prepare the new class header element
        string[,] actions = new string[1, 11];
        actions[0, 0] = "HyperLink";
        actions[0, 1] = GetString("tags.taggroup_list.newgroup");
        actions[0, 3] = "javascript: AddNewItem();";
        actions[0, 5] = GetImageUrl("Objects/CMS_TagGroup/add.png");

        this.CurrentMaster.HeaderActions.Actions = actions;

        this.siteSelector.DropDownSingleSelect.AutoPostBack = true;
        this.siteSelector.UniSelector.OnSelectionChanged += new EventHandler(UniSelector_OnSelectionChanged);
        this.siteSelector.IsLiveSite = false;
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        if (this.siteSelector.UniSelector.HasData)
        {
            ScriptHelper.RegisterClientScriptBlock(this.Page, typeof(string), "AddNewItem", ScriptHelper.GetScript(
                "function AddNewItem() { this.window.location = '" + ResolveUrl("~/CMSModules/TagGroups/Pages/Development/TagGroup_New.aspx?siteid=" + this.siteSelector.Value) + "'; }"));
        }
        else
        {
            this.lblInfo.Text = GetString("tags.taggroup_list.nosites");
            this.lblInfo.Visible = true;
            this.gridTagGroups.Visible = false;
            this.CurrentMaster.PanelBody.FindControl("pnlAdditionalControls").Visible = false;
            this.CurrentMaster.PanelBody.FindControl("pnlActions").Visible = false;
        }
    }


    protected void UniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        this.gridTagGroups.ReloadData();
        this.pnlUpdate.Update();
    }

    #endregion


    #region "UniGrid Events"

    private void gridTagGroups_OnAction(string actionName, object actionArgument)
    {
        int groupId = -1;
        string siteId = null;

        switch (actionName)
        {
            // Editing of the category fired
            case "edit":
                // Get category ID
                groupId = ValidationHelper.GetInteger(actionArgument, -1);
                siteId = Convert.ToString(this.siteSelector.Value);

                // Create a target site URL and pass the category ID as a parameter
                string editUrl = "TagGroup_Edit.aspx?groupid=" + groupId.ToString() + "&siteid=" + siteId;
                URLHelper.Redirect(editUrl);
                break;

            // Deleteing of the category was fired
            case "delete":
                groupId = ValidationHelper.GetInteger(actionArgument, -1);

                if (groupId > -1)
                {
                    // If no item depends on the current group                
                    DataSet ds = TagInfoProvider.GetTags("TagGroupID = " + groupId, null);
                    if (DataHelper.DataSourceIsEmpty(ds))
                    {
                        // Delete the class
                        TagGroupInfoProvider.DeleteTagGroupInfo(groupId);
                    }
                    else
                    {
                        // Display error on deleting
                        this.lblError.Visible = true;
                        this.lblError.Text = GetString("tags.taggroup_list.hasdependencies");
                    }
                }
                break;
        }
    }

    #endregion
}
