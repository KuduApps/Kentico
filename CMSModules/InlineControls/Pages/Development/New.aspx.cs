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
using CMS.DataEngine;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.ExtendedControls;

public partial class CMSModules_InlineControls_Pages_Development_New : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Control initializations				
        rfvDisplayName.ErrorMessage = GetString("InlineControl_Edit.ErrorDisplayName");
        rfvName.ErrorMessage = GetString("InlineControl_Edit.ErrorName");

        // Check 'Assign with current web site' check box
        if ((CMSContext.CurrentSite != null) && (!RequestHelper.IsPostBack()))
        {
            chkAssign.Text = GetString("General.AssignWithWebSite") + " " + CMSContext.CurrentSite.DisplayName;
            chkAssign.Checked = true;
            chkAssign.Visible = true;
        }

        // Initializes page title control
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = GetString("InlineControl_Edit.ItemListLink");
        pageTitleTabs[0, 1] = "~/CMSModules/InlineControls/Pages/Development/List.aspx";
        pageTitleTabs[0, 2] = string.Empty;
        pageTitleTabs[1, 0] = GetString("InlineControl_Edit.NewItemCaption");
        pageTitleTabs[1, 1] = string.Empty;
        pageTitleTabs[1, 2] = string.Empty;

        CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
        CurrentMaster.Title.TitleText = GetString("Edit.NewControl");
        CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_InlineControl/new.png");
        CurrentMaster.Title.HelpTopicName = "new_control";
        CurrentMaster.Title.HelpName = "helpTopic";

        FileSystemDialogConfiguration config = new FileSystemDialogConfiguration();
        config.DefaultPath = "CMSInlineControls";
        config.AllowedExtensions = "ascx";
        config.ShowFolders = false;
        FileSystemSelector.DialogConfig = config;
        FileSystemSelector.AllowEmptyValue = false;
        FileSystemSelector.SelectedPathPrefix = "~/CMSInlineControls/";
        FileSystemSelector.ValidationError = GetString("Edit.ErrorFileName");
    }


    /// <summary>
    /// Sets data to database.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Validate inputs
        string errorMessage = new Validator().NotEmpty(txtControlDisplayName.Text.Trim(), rfvDisplayName.ErrorMessage)
            .NotEmpty(txtControlName.Text.Trim(), rfvName.ErrorMessage)
            .IsCodeName(txtControlName.Text.Trim(), GetString("general.errorcodenameinidentificatorformat")).Result;

        if ((string.IsNullOrEmpty(errorMessage)) && (!FileSystemSelector.IsValid()))
        {
            errorMessage = FileSystemSelector.ValidationError;
        }

        if (string.IsNullOrEmpty(errorMessage))
        {
            // Create new inline control object
            InlineControlInfo inlineControlObj = new InlineControlInfo();

            inlineControlObj.ControlDescription = txtControlDescription.Text.Trim();
            inlineControlObj.ControlDisplayName = txtControlDisplayName.Text.Trim();
            inlineControlObj.ControlFileName = FileSystemSelector.Value.ToString().Trim();
            inlineControlObj.ControlParameterName = txtControlParameterName.Text.Trim();
            inlineControlObj.ControlName = txtControlName.Text.Trim();

            try
            {
                // Create new inline control
                InlineControlInfoProvider.SetInlineControlInfo(inlineControlObj);
                if ((chkAssign.Visible) && (chkAssign.Checked) && (CMSContext.CurrentSite != null))
                {
                    // Add new control to the actual site
                    InlineControlSiteInfoProvider.AddInlineControlToSite(inlineControlObj.ControlID, CMSContext.CurrentSiteID);
                }
                // Redirect to edit page
                URLHelper.Redirect("Frameset.aspx?inlinecontrolid=" + Convert.ToString(inlineControlObj.ControlID) + "&saved=1");
            }
            catch (Exception ex)
            {
                ShowError(ex.Message.Replace("%%name%%", txtControlName.Text));
            }
        }
        else
        {
            ShowError(errorMessage);
        }
    }
}
