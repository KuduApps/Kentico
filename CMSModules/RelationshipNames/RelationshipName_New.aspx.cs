using System;
using System.Web.UI.WebControls;

using CMS.SiteProvider;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_RelationshipNames_RelationshipName_New : SiteManagerPage
{
    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check 'Assign with current web site' check box
        if ((CMSContext.CurrentSite != null) && (!RequestHelper.IsPostBack()))
        {
            chkAssign.Text = GetString("General.AssignWithWebSite") + " " + CMSContext.CurrentSite.DisplayName;
            chkAssign.Checked = true;
            chkAssign.Visible = true;
        }

        // Initialize drop down list with relationship type
        if (!RequestHelper.IsPostBack())
        {
            drpRelType.Items.Clear();
            drpRelType.Items.Add(new ListItem(GetString("RelationshipNames.Documents"), ";" + CMSObjectHelper.GROUP_DOCUMENTS + ";"));
            drpRelType.Items.Add(new ListItem(GetString("RelationshipNames.Objects"), ";" + CMSObjectHelper.GROUP_OBJECTS + ";"));
            drpRelType.SelectedIndex = 0;
        }

        // Initializes validators
        RequiredFieldValidatorCodeName.ErrorMessage = GetString("General.RequiresCodeName");
        RequiredFieldValidatorDisplayName.ErrorMessage = GetString("General.RequiresDisplayName");

        // Initializes page title
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = GetString("RelationshipNames.RelationshipNames");
        pageTitleTabs[0, 1] = "~/CMSModules/RelationshipNames/RelationshipName_List.aspx";
        pageTitleTabs[1, 0] = GetString("RelationshipNames.NewRelationshipName");

        CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
        CurrentMaster.Title.TitleText = GetString("RelationshipNames.TitleNew");
        CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_RelationshipName/new.png");
        CurrentMaster.Title.HelpTopicName = "new_namegeneral_tab";
        CurrentMaster.Title.HelpName = "helpTopic";
    }

    #endregion


    #region "Control events"

    /// <summary>
    /// Saves new relationship name's data and redirects to RelationshipName_Edit.aspx.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Event arguments</param>
    protected void ButtonOK_Click(object sender, EventArgs e)
    {
        // finds whether required fields are not empty
        string result = new Validator().NotEmpty(txtRelationshipNameDisplayName.Text, GetString("General.RequiresDisplayName")).NotEmpty(txtRelationshipNameCodeName.Text, GetString("General.RequiresCodeName"))
            .IsCodeName(txtRelationshipNameCodeName.Text, GetString("general.invalidcodename"))
            .Result;

        if (result == string.Empty)
        {
            RelationshipNameInfo rni = RelationshipNameInfoProvider.GetRelationshipNameInfo(txtRelationshipNameCodeName.Text);
            if (rni == null)
            {
                int relationshipNameId = SaveNewRelationshipName();

                if (relationshipNameId > 0)
                {
                    URLHelper.Redirect("RelationshipName_Edit.aspx?relationshipnameid=" + relationshipNameId + "&saved=1");
                }
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = GetString("RelationshipNames.RelationshipNameAlreadyExists");
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = result;
        }
    }

    #endregion


    #region "Protected methods"

    /// <summary>
    /// Saves new relationship name's data into DB.
    /// </summary>
    /// <returns>Returns ID of created relationship name</returns>
    protected int SaveNewRelationshipName()
    {
        RelationshipNameInfo rni = new RelationshipNameInfo();
        rni.RelationshipDisplayName = txtRelationshipNameDisplayName.Text;
        rni.RelationshipName = txtRelationshipNameCodeName.Text;
        rni.RelationshipAllowedObjects = drpRelType.SelectedValue;
        RelationshipNameInfoProvider.SetRelationshipNameInfo(rni);
        if (chkAssign.Visible && chkAssign.Checked && (CMSContext.CurrentSite != null) && (rni.RelationshipNameId > 0))
        {
            // Add new relationship name to the actual site
            RelationshipNameSiteInfoProvider.AddRelationshipNameToSite(rni.RelationshipNameId, CMSContext.CurrentSite.SiteID);
        }
        return rni.RelationshipNameId;
    }

    #endregion
}
