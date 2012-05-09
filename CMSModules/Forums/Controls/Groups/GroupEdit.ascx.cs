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

using CMS.Forums;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.CMSOutputFilter;
using CMS.WebAnalytics;

public partial class CMSModules_Forums_Controls_Groups_GroupEdit : CMSAdminEditControl
{
    #region "Variables"

    private int mGroupId = 0;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the ID of the group to edit.
    /// </summary>
    public int GroupID
    {
        get
        {
            return this.mGroupId;
        }
        set
        {
            this.mGroupId = value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Visible)
        {
            this.EnableViewState = false;
        }

        // Hide code name edit for simple mode
        if (DisplayMode == ControlDisplayModeEnum.Simple)
        {
            this.plcCodeName.Visible = false;
            this.plcUseHtml.Visible = false;
        }

        txtGroupDisplayName.IsLiveSite = this.IsLiveSite;
        txtGroupDescription.IsLiveSite = this.IsLiveSite;

        // Control initializations				
        this.rfvGroupDisplayName.ErrorMessage = GetString("general.requiresdisplayname");
        this.rfvGroupName.ErrorMessage = GetString("general.requirescodename");

        this.lblGroupDisplayName.Text = GetString("Group_General.GroupDisplayNameLabel");
        this.lblGroupName.Text = GetString("Group_General.GroupNameLabel");
        this.lblForumBaseUrl.Text = GetString("Group_General.ForumBaseUrlLabel");
        this.lblUnsubscriptionUrl.Text = GetString("Group_General.UnsubscriptionUrlLabel");

        this.btnOk.Text = GetString("General.OK");

        // Show on-line marketing settings
        string siteName = CMSContext.CurrentSiteName;
        plcOnline.Visible = (ActivitySettingsHelper.ForumPostSubscriptionEnabled(siteName) || ActivitySettingsHelper.ForumPostsEnabled(siteName))
            && ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(siteName);

        // Fill editing form
        if (!this.IsLiveSite && !RequestHelper.IsPostBack())
        {
            ReloadData();
        }

        // Show/hide URL textboxes
        plcBaseAndUnsubUrl.Visible = (DisplayMode != ControlDisplayModeEnum.Simple);

        if (plcBaseAndUnsubUrl.Visible)
        {
            SetUrl();
        }
    }


    /// <summary>
    /// Sets base and unsubscription URLs.
    /// </summary>
    private void SetUrl()
    {
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "CheckBoxes", ScriptHelper.GetScript(@"
                    function check(txtId,chk,inhV)  
                    {
                        txt = document.getElementById(txtId);
                        if ((txt != null)&&(chk != null))
                        {
                            if (chk.checked)
                            {
                                txt.disabled = 'disabled';
                                txt.value = inhV;
                            }
                            else
                            {
                                txt.disabled = '';
                            }
                        }
                    }
                   "));


        // Force output filter to not resolve URLs in textboxes
        OutputFilter.CanResolveAllUrls = false;

        // Get base and unsubscription url from settings
        string baseUrl = ValidationHelper.GetString(SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSForumBaseUrl"), "");
        string unsubscriptionUrl = ValidationHelper.GetString(SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSForumUnsubscriptionUrl"), "");

        if (chkInheritBaseUrl.Checked)
        {
            txtForumBaseUrl.Text = baseUrl;
            txtForumBaseUrl.Enabled = false;
        }
        else
        {
            txtForumBaseUrl.Enabled = true;
        }

        if (chkInheritUnsubUrl.Checked)
        {
            txtUnsubscriptionUrl.Text = unsubscriptionUrl;
            txtUnsubscriptionUrl.Enabled = false;
        }
        else
        {
            txtUnsubscriptionUrl.Enabled = true;
        }

        chkInheritBaseUrl.Attributes.Add("onclick", "check('" + txtForumBaseUrl.ClientID + "', this,'" + baseUrl + "')");
        chkInheritUnsubUrl.Attributes.Add("onclick", "check('" + txtUnsubscriptionUrl.ClientID + "', this,'" + unsubscriptionUrl + "')");
    }


    /// <summary>
    /// Reloads the data in the form.
    /// </summary>
    public override void ReloadData()
    {
        ForumGroupInfo fgi = ForumGroupInfoProvider.GetForumGroupInfo(this.GroupID);
        if (fgi != null)
        {
            this.txtGroupDescription.Text = fgi.GroupDescription;
            this.txtGroupDisplayName.Text = fgi.GroupDisplayName;
            this.txtGroupName.Text = fgi.GroupName;
            this.chkCaptcha.Checked = fgi.GroupUseCAPTCHA;
            this.chkForumDisplayEmails.Checked = fgi.GroupDisplayEmails;
            this.chkForumRequireEmail.Checked = fgi.GroupRequireEmail;
            this.chkUseHTML.Checked = fgi.GroupHTMLEditor;
            this.txtMaxAttachmentSize.Text = fgi.GroupAttachmentMaxFileSize.ToString();

            // Show/hide URL textboxes
            plcBaseAndUnsubUrl.Visible = (DisplayMode != ControlDisplayModeEnum.Simple);
            if (DisplayMode != ControlDisplayModeEnum.Simple)
            {
                this.txtForumBaseUrl.Text = fgi.GroupBaseUrl;
                this.txtUnsubscriptionUrl.Text = fgi.GroupUnsubscriptionUrl;


                chkInheritBaseUrl.Checked = (fgi.GetValue("GroupBaseUrl") == null);
                chkInheritUnsubUrl.Checked = (fgi.GetValue("GroupUnsubscriptionUrl") == null);

                txtForumBaseUrl.Enabled = !chkInheritBaseUrl.Checked;
                txtUnsubscriptionUrl.Enabled = !chkInheritUnsubUrl.Checked;

                SetUrl();
            }


            // Settings
            this.chkAuthorEdit.Checked = fgi.GroupAuthorEdit;
            this.chkAuthorDelete.Checked = fgi.GroupAuthorDelete;
            this.txtImageMaxSideSize.Text = fgi.GroupImageMaxSideSize.ToString();
            this.txtIsAnswerLimit.Text = fgi.GroupIsAnswerLimit.ToString();
            this.radTypeChoose.Checked = fgi.GroupType == 0;
            this.radTypeDiscussion.Checked = fgi.GroupType == 1;
            this.radTypeAnswer.Checked = fgi.GroupType == 2;

            // Discussion
            this.chkEnableQuote.Checked = fgi.GroupEnableQuote;
            this.chkEnableBold.Checked = fgi.GroupEnableFontBold;
            this.chkEnableItalic.Checked = fgi.GroupEnableFontItalics;
            this.chkEnableUnderline.Checked = fgi.GroupEnableFontUnderline;
            this.chkEnableStrike.Checked = fgi.GroupEnableFontStrike;
            this.chkEnableCode.Checked = fgi.GroupEnableCodeSnippet;
            this.chkEnableColor.Checked = fgi.GroupEnableFontColor;
            this.radImageAdvanced.Checked = fgi.GroupEnableAdvancedImage;
            this.radImageSimple.Checked = fgi.GroupEnableImage;
            this.radImageNo.Checked = !(fgi.GroupEnableImage || fgi.GroupEnableAdvancedImage);
            this.radUrlAdvanced.Checked = fgi.GroupEnableAdvancedURL;
            this.radUrlSimple.Checked = fgi.GroupEnableURL;
            this.radUrlNo.Checked = !(fgi.GroupEnableURL || fgi.GroupEnableAdvancedURL);
            this.chkLogActivity.Checked = fgi.GroupLogActivity;
        }
    }


    /// <summary>
    /// Sets data to database.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (!CheckPermissions("cms.forums", CMSAdminControl.PERMISSION_MODIFY))
        {
            return;
        }

        string errorMessage = new Validator().NotEmpty(txtGroupDisplayName.Text, GetString("general.requiresdisplayname")).Result;

        if ((errorMessage == String.Empty) && (DisplayMode != ControlDisplayModeEnum.Simple))
        {
            errorMessage = new Validator().NotEmpty(txtGroupName.Text, GetString("general.requirescodename"))
                                          .IsCodeName(txtGroupName.Text.Trim(), GetString("general.errorcodenameinidentificatorformat")).Result;
        }

        if (errorMessage != "")
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
            return;
        }

        int communityGroupId = 0;

        ForumGroupInfo forumGroupObj = ForumGroupInfoProvider.GetForumGroupInfo(this.GroupID);
        if (forumGroupObj != null)
        {
            communityGroupId = forumGroupObj.GroupGroupID;
        }

        forumGroupObj = ForumGroupInfoProvider.GetForumGroupInfo(txtGroupName.Text.Trim(), CMSContext.CurrentSiteID, communityGroupId);
        if ((forumGroupObj == null) || (forumGroupObj.GroupID == this.mGroupId))
        {
            if (forumGroupObj == null)
            {
                forumGroupObj = ForumGroupInfoProvider.GetForumGroupInfo(this.GroupID);
            }

            if (forumGroupObj != null)
            {
                if (txtGroupDisplayName.Text.Trim() != forumGroupObj.GroupDisplayName)
                {
                    // Refresh a breadcrumb if used in the tabs layout
                    ScriptHelper.RefreshTabHeader(Page, string.Empty);
                }

                forumGroupObj.GroupDescription = txtGroupDescription.Text.Trim();
                forumGroupObj.GroupDisplayName = txtGroupDisplayName.Text.Trim();

                forumGroupObj.GroupUseCAPTCHA = chkCaptcha.Checked;
                forumGroupObj.GroupDisplayEmails = chkForumDisplayEmails.Checked;
                forumGroupObj.GroupRequireEmail = chkForumRequireEmail.Checked;

                if (DisplayMode != ControlDisplayModeEnum.Simple)
                {
                    forumGroupObj.GroupUnsubscriptionUrl = chkInheritUnsubUrl.Checked ? null : txtUnsubscriptionUrl.Text.Trim();
                    forumGroupObj.GroupBaseUrl = chkInheritBaseUrl.Checked ? null : txtForumBaseUrl.Text.Trim();
                    forumGroupObj.GroupName = txtGroupName.Text.Trim();
                    forumGroupObj.GroupHTMLEditor = chkUseHTML.Checked;
                }

                // Settings
                forumGroupObj.GroupAuthorEdit = chkAuthorEdit.Checked;
                forumGroupObj.GroupAuthorDelete = chkAuthorDelete.Checked;
                forumGroupObj.GroupImageMaxSideSize = ValidationHelper.GetInteger(txtImageMaxSideSize.Text, 400);
                forumGroupObj.GroupIsAnswerLimit = ValidationHelper.GetInteger(txtIsAnswerLimit.Text, 5);
                forumGroupObj.GroupType = (radTypeChoose.Checked ? 0 : (radTypeDiscussion.Checked ? 1 : 2));
                forumGroupObj.GroupAttachmentMaxFileSize = ValidationHelper.GetInteger(txtMaxAttachmentSize.Text, 0);

                // Discussion
                forumGroupObj.GroupEnableQuote = chkEnableQuote.Checked;
                forumGroupObj.GroupEnableFontBold = this.chkEnableBold.Checked;
                forumGroupObj.GroupEnableFontItalics = this.chkEnableItalic.Checked;
                forumGroupObj.GroupEnableFontUnderline = this.chkEnableUnderline.Checked;
                forumGroupObj.GroupEnableFontStrike = this.chkEnableStrike.Checked;
                forumGroupObj.GroupEnableCodeSnippet = this.chkEnableCode.Checked;
                forumGroupObj.GroupEnableFontColor = this.chkEnableColor.Checked;
                forumGroupObj.GroupEnableAdvancedImage = this.radImageAdvanced.Checked;
                forumGroupObj.GroupEnableAdvancedURL = this.radUrlAdvanced.Checked;
                forumGroupObj.GroupEnableImage = this.radImageSimple.Checked;
                forumGroupObj.GroupEnableURL = this.radUrlSimple.Checked;

                if (plcOnline.Visible)
                {
                    forumGroupObj.GroupLogActivity = this.chkLogActivity.Checked;
                }

                ForumGroupInfoProvider.SetForumGroupInfo(forumGroupObj);

                lblInfo.Visible = true;
                lblInfo.Text = GetString("General.ChangesSaved");

                // Load form with data
                ReloadData();

                this.RaiseOnSaved();
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = GetString("Group_General.GroupNotFound");
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = GetString("Group_General.GroupAlreadyExists");
        }
    }
}
