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
using CMS.LicenseProvider;
using CMS.CMSHelper;
using CMS.Forums;
using CMS.UIControls;
using CMS.WebAnalytics;

public partial class CMSModules_Forums_Controls_Forums_ForumEdit : CMSAdminEditControl
{
    #region "Variables"

    protected int mForumId = 0;
    protected int groupId = 0;
    protected string script = "";

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the ID of the forum to edit.
    /// </summary>
    public int ForumID
    {
        get
        {
            return this.mForumId;
        }
        set
        {
            this.mForumId = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Visible)
        {
            this.EnableViewState = false;
        }

        txtForumDisplayName.IsLiveSite = this.IsLiveSite;
        txtForumDescription.IsLiveSite = this.IsLiveSite;

        // Hide code name in simple mode
        if (DisplayMode == ControlDisplayModeEnum.Simple)
        {
            this.plcCodeName.Visible = false;
            this.plcUseHtml.Visible = false;
        }

        // Control initializations
        this.rfvForumDisplayName.ErrorMessage = GetString("Forum_General.EmptyDisplayName");
        this.rfvForumName.ErrorMessage = GetString("Forum_General.EmptyCodeName");

        // Get strings for labels
        this.lblForumOpen.Text = GetString("Forum_Edit.ForumOpenLabel");
        this.lblForumLocked.Text = GetString("Forum_Edit.ForumLockedLabel");
        this.lblForumDisplayEmails.Text = GetString("Forum_Edit.ForumDisplayEmailsLabel");
        this.lblForumRequireEmail.Text = GetString("Forum_Edit.ForumRequireEmailLabel");
        this.lblForumDisplayName.Text = GetString("Forum_Edit.ForumDisplayNameLabel");
        this.lblForumName.Text = GetString("Forum_Edit.ForumNameLabel");
        this.lblUseHTML.Text = GetString("Forum_Edit.UseHtml");
        this.lblBaseUrl.Text = GetString("Forum_Edit.lblBaseUrl");
        this.lblCaptcha.Text = GetString("Forum_Edit.useCaptcha");
        this.lblUnsubscriptionUrl.Text = GetString("Forum_Edit.lblUnsubscriptionUrl");

        this.chkInheritBaseUrl.Text = GetString("Forum_Edit.InheritBaseUrl");
        this.chkInheritUnsubscribeUrl.Text = GetString("Forum_Edit.InheritUnsupscriptionUrl");

        this.btnOk.Text = GetString("General.OK");        
        string currentForum = GetString("Forum_Edit.NewItemCaption");

        // Create scripts 
        script = @"
                function LoadOption(clientId, value)
                {
                    var obj = document.getElementById(clientId);
                    if(obj!=null)
                    {
                        obj.checked = value;
                    }
                }                

                function LoadSetting(clientId, value, enabled, type)
                {
                    SetInheritance(clientId, value, type);
                    var obj = document.getElementById(clientId);
                    if (obj != null) {
                        obj.disabled = enabled;
                    }
                }

                function SetInheritance(clientIds, values, type)
                {
                    var idArray = clientIds.split(';');
                    var valueArray = values.toString().split(';');

                    for(var i = 0;i<idArray.length;i++)
                    {
                        var clientId = idArray[i];
                        var value = valueArray[i];
                        var obj = document.getElementById(clientId);
                        if (obj != null) {
                            obj.disabled = !obj.disabled;
                            if (obj.disabled)
                            {
                                if (type == 'txt') {
                                    obj.value = value;
                                } else {
                                    obj.checked = (value == 'true');
                                }
                            }
                        }
                    }
                }
                ";

        this.ltrScript.Text += ScriptHelper.GetScript(script);
        script = "";

        // Load object info from database
        ForumInfo forumObj = ForumInfoProvider.GetForumInfo(this.mForumId);
        if (forumObj != null)
        {
            currentForum = forumObj.ForumName;
            groupId = forumObj.ForumGroupID;

            if (!this.IsLiveSite && !RequestHelper.IsPostBack())
            {
                ReloadData(forumObj);
            }
            else
            {
                // Handle base and unsubscription urls 
                txtBaseUrl.Enabled = !chkInheritBaseUrl.Checked;
                txtUnsubscriptionUrl.Enabled = !chkInheritUnsubscribeUrl.Checked;
                this.txtMaxAttachmentSize.Enabled = !chkInheritMaxAttachmentSize.Checked;
                this.txtIsAnswerLimit.Enabled = !chkInheritIsAnswerLimit.Checked;
                this.txtImageMaxSideSize.Enabled = !chkInheritMaxSideSize.Checked;
            }

        }

        this.ltrScript.Text += ScriptHelper.GetScript(script);

        // Show/hide URL textboxes
        plcBaseAndUnsubUrl.Visible = (DisplayMode != ControlDisplayModeEnum.Simple);        
    }


    /// <summary>
    /// Reloads the data of the editing forum.
    /// </summary>
    public override void ReloadData()
    {
        ForumInfo forumObj = ForumInfoProvider.GetForumInfo(this.mForumId);
        if (forumObj != null)
        {
            ReloadData(forumObj);
        }
    }


    /// <summary>
    /// Reloads the data of the editing forum.
    /// </summary>
    /// <param name="forumObj">Forum object</param>
    private void ReloadData(ForumInfo forumObj)
    {
        // Set main properties
        this.chkForumOpen.Checked = forumObj.ForumOpen;
        this.chkForumLocked.Checked = forumObj.ForumIsLocked;
        this.txtForumDescription.Text = forumObj.ForumDescription;
        this.txtForumDisplayName.Text = forumObj.ForumDisplayName;
        this.txtForumName.Text = forumObj.ForumName;

        // Set other settings
        this.txtImageMaxSideSize.Text = forumObj.ForumImageMaxSideSize.ToString();
        this.txtIsAnswerLimit.Text = forumObj.ForumIsAnswerLimit.ToString();
        this.txtMaxAttachmentSize.Text = forumObj.ForumAttachmentMaxFileSize.ToString();
        this.txtBaseUrl.Text = forumObj.ForumBaseUrl;
        this.txtUnsubscriptionUrl.Text = forumObj.ForumUnsubscriptionUrl;

        // Check if is inherited value
        bool inheritAuthorDelete = (forumObj.GetValue("ForumAuthorDelete") == null);
        bool inheritAuthorEdit = (forumObj.GetValue("ForumAuthorEdit") == null);
        bool inheritImageMaxSideSize = (forumObj.GetValue("ForumImageMaxSideSize") == null);
        bool inheritMaxAttachmentSize = (forumObj.GetValue("ForumAttachmentMaxFileSize") == null);
        bool inheritIsAnswerLimit = (forumObj.GetValue("ForumIsAnswerLimit") == null);
        bool inheritType = (forumObj.GetValue("ForumType") == null);
        bool inheritUseHTML = (forumObj.GetValue("ForumHTMLEditor") == null);
        bool inheritDislayEmails = (forumObj.GetValue("ForumDisplayEmails") == null);
        bool inheritRequireEmail = (forumObj.GetValue("ForumRequireEmail") == null);
        bool inheritCaptcha = (forumObj.GetValue("ForumUseCAPTCHA") == null);
        bool inheritBaseUrl = (forumObj.GetValue("ForumBaseUrl") == null);
        bool inheritUnsubscriptionUrl = (forumObj.GetValue("ForumUnsubscriptionUrl") == null);
        bool inheritLogActivity = (forumObj.GetValue("ForumLogActivity") == null);
        // Discussion
        bool inheritDiscussion = (forumObj.GetValue("ForumDiscussionActions") == null);

        // Set properties
        this.chkInheritUseHTML.Checked = inheritUseHTML;
        this.chkInheritDisplayEmails.Checked = inheritDislayEmails;
        this.chkInheritRequireEmail.Checked = inheritRequireEmail;
        this.chkInheritCaptcha.Checked = inheritCaptcha;
        this.chkInheritAuthorDelete.Checked = inheritAuthorDelete;
        this.chkInheritAuthorEdit.Checked = inheritAuthorEdit;
        this.txtImageMaxSideSize.Enabled = !inheritImageMaxSideSize;
        this.chkInheritMaxSideSize.Checked = inheritImageMaxSideSize;
        this.txtIsAnswerLimit.Enabled = !inheritIsAnswerLimit;
        this.chkInheritIsAnswerLimit.Checked = inheritIsAnswerLimit;
        this.chkInheritType.Checked = inheritType;
        this.txtMaxAttachmentSize.Enabled = !inheritMaxAttachmentSize;
        this.chkInheritMaxAttachmentSize.Checked = inheritMaxAttachmentSize;
        this.txtBaseUrl.Enabled = !inheritBaseUrl;
        this.chkInheritBaseUrl.Checked = inheritBaseUrl;
        this.txtUnsubscriptionUrl.Enabled = !inheritUnsubscriptionUrl;
        this.chkInheritUnsubscribeUrl.Checked = inheritUnsubscriptionUrl;

        this.plcOnline.Visible = ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(CMSContext.CurrentSiteName);
        this.chkInheritLogActivity.Checked = inheritLogActivity;

        // Discussion
        this.chkInheritDiscussion.Checked = inheritDiscussion;

        // Create script for update inherit values
        this.ltrScript.Text += ScriptHelper.GetScript(
            "LoadSetting('" + chkForumRequireEmail.ClientID + "', " + (forumObj.ForumRequireEmail ? "true" : "false") + ", " + (inheritRequireEmail ? "true" : "false") + ", 'chk');" +
            "LoadSetting('" + chkCaptcha.ClientID + "', " + (forumObj.ForumUseCAPTCHA ? "true" : "false") + ", " + (inheritCaptcha ? "true" : "false") + ", 'chk');" +
            "LoadSetting('" + chkForumDisplayEmails.ClientID + "', " + (forumObj.ForumDisplayEmails ? "true" : "false") + ", " + (inheritDislayEmails ? "true" : "false") + ", 'chk');" +
            "LoadSetting('" + chkUseHTML.ClientID + "', " + (forumObj.ForumHTMLEditor ? "true" : "false") + ", " + (inheritUseHTML ? "true" : "false") + ", 'chk');" +
            "LoadSetting('" + chkAuthorDelete.ClientID + "', " + (forumObj.ForumAuthorDelete ? "true" : "false") + ", " + (inheritAuthorDelete ? "true" : "false") + ", 'chk');" +
            "LoadSetting('" + chkAuthorEdit.ClientID + "', " + (forumObj.ForumAuthorEdit ? "true" : "false") + ", " + (inheritAuthorEdit ? "true" : "false") + ", 'chk');" +
            "LoadSetting('" + chkLogActivity.ClientID + "', " + (forumObj.ForumLogActivity ? "true" : "false") + ", " + (inheritLogActivity ? "true" : "false") + ", 'chk');" +
            
            // Discussion
            "LoadSetting('" + radImageSimple.ClientID + "', " + (forumObj.ForumEnableImage ? "true" : "false") + ", " + (inheritDiscussion ? "true" : "false") + ", 'rad');" +
            "LoadSetting('" + radImageAdvanced.ClientID + "', " + (forumObj.ForumEnableAdvancedImage ? "true" : "false") + ", " + (inheritDiscussion ? "true" : "false") + ", 'rad');" +
            "LoadSetting('" + radImageNo.ClientID + "', " + (!(forumObj.ForumEnableAdvancedImage || forumObj.ForumEnableImage) ? "true" : "false") + ", " + (inheritDiscussion ? "true" : "false") + ", 'rad');" +
            "LoadSetting('" + radUrlSimple.ClientID + "', " + (forumObj.ForumEnableURL ? "true" : "false") + ", " + (inheritDiscussion ? "true" : "false") + ", 'rad');" +
            "LoadSetting('" + radUrlAdvanced.ClientID + "', " + (forumObj.ForumEnableAdvancedURL ? "true" : "false") + ", " + (inheritDiscussion ? "true" : "false") + ", 'rad');" +
            "LoadSetting('" + radUrlNo.ClientID + "', " + (!(forumObj.ForumEnableAdvancedURL || forumObj.ForumEnableURL) ? "true" : "false") + ", " + (inheritDiscussion ? "true" : "false") + ", 'rad');" +
            "LoadSetting('" + chkEnableQuote.ClientID + "', " + (forumObj.ForumEnableQuote ? "true" : "false") + ", " + (inheritDiscussion ? "true" : "false") + ", 'chk');" +
            "LoadSetting('" + chkEnableBold.ClientID + "', " + (forumObj.ForumEnableFontBold ? "true" : "false") + ", " + (inheritDiscussion ? "true" : "false") + ", 'chk');" +
            "LoadSetting('" + chkEnableItalic.ClientID + "', " + (forumObj.ForumEnableFontItalics ? "true" : "false") + ", " + (inheritDiscussion ? "true" : "false") + ", 'chk');" +
            "LoadSetting('" + chkEnableStrike.ClientID + "', " + (forumObj.ForumEnableFontStrike ? "true" : "false") + ", " + (inheritDiscussion ? "true" : "false") + ", 'chk');" +
            "LoadSetting('" + chkEnableUnderline.ClientID + "', " + (forumObj.ForumEnableFontUnderline ? "true" : "false") + ", " + (inheritDiscussion ? "true" : "false") + ", 'chk');" +
            "LoadSetting('" + chkEnableCode.ClientID + "', " + (forumObj.ForumEnableCodeSnippet ? "true" : "false") + ", " + (inheritDiscussion ? "true" : "false") + ", 'chk');" +
            "LoadSetting('" + chkEnableColor.ClientID + "', " + (forumObj.ForumEnableFontColor ? "true" : "false") + ", " + (inheritDiscussion ? "true" : "false") + ", 'chk');" +
            "LoadSetting('" + radTypeAnswer.ClientID + "', " + (forumObj.ForumType == 2 ? "true" : "false") + ", " + (inheritType ? "true" : "false") + ", 'rad');" +
            "LoadSetting('" + radTypeDiscussion.ClientID + "', " + (forumObj.ForumType == 1 ? "true" : "false") + ", " + (inheritType ? "true" : "false") + ", 'rad');" +
            "LoadSetting('" + radTypeChoose.ClientID + "', " + (forumObj.ForumType == 0 ? "true" : "false") + ", " + (inheritType ? "true" : "false") + ", 'rad');");

        ForumGroupInfo fgi = ForumGroupInfoProvider.GetForumGroupInfo(forumObj.ForumGroupID);

        this.chkInheritUnsubscribeUrl.Attributes.Add("onclick", "SetInheritance('" + txtUnsubscriptionUrl.ClientID + "', '" + fgi.GroupUnsubscriptionUrl.Replace("'","\\\'") + "', 'txt');");
        this.chkInheritBaseUrl.Attributes.Add("onclick", "SetInheritance('" + txtBaseUrl.ClientID + "', '" + fgi.GroupBaseUrl.Replace("'", "\\\'") + "', 'txt');");

        // Settings inheritance
        this.chkInheritUseHTML.Attributes.Add("onclick", "SetInheritance('" + chkUseHTML.ClientID + "', '" + fgi.GroupHTMLEditor.ToString().ToLower() + "', 'chk');");
        this.chkInheritCaptcha.Attributes.Add("onclick", "SetInheritance('" + chkCaptcha.ClientID + "', '" + fgi.GroupUseCAPTCHA.ToString().ToLower() + "', 'chk');");
        this.chkInheritRequireEmail.Attributes.Add("onclick", "SetInheritance('" + chkForumRequireEmail.ClientID + "', '" + fgi.GroupRequireEmail.ToString().ToLower() + "', 'chk');");
        this.chkInheritDisplayEmails.Attributes.Add("onclick", "SetInheritance('" + chkForumDisplayEmails.ClientID + "', '" + fgi.GroupDisplayEmails.ToString().ToLower() + "', 'chk');");
        this.chkInheritAuthorDelete.Attributes.Add("onclick", "SetInheritance('" + chkAuthorDelete.ClientID + "', '" + fgi.GroupAuthorDelete.ToString().ToLower() + "', 'chk');");
        this.chkInheritAuthorEdit.Attributes.Add("onclick", "SetInheritance('" + chkAuthorEdit.ClientID + "', '" + fgi.GroupAuthorEdit.ToString().ToLower() + "', 'chk');");
        this.chkInheritIsAnswerLimit.Attributes.Add("onclick", "SetInheritance('" + txtIsAnswerLimit.ClientID + "', '" + fgi.GroupIsAnswerLimit.ToString().Replace("'", "\\\'") + "', 'txt');");
        this.chkInheritMaxSideSize.Attributes.Add("onclick", "SetInheritance('" + txtImageMaxSideSize.ClientID + "', '" + fgi.GroupImageMaxSideSize.ToString().Replace("'", "\\\'") + "', 'txt');");
        this.chkInheritType.Attributes.Add("onclick", "SetInheritance('" + radTypeAnswer.ClientID + "', '" + (fgi.GroupType == 2 ? "true" : "false") + "', 'rad');" +
            "SetInheritance('" + radTypeDiscussion.ClientID + "', '" + (fgi.GroupType == 1 ? "true" : "false") + "', 'rad');" +
            "SetInheritance('" + radTypeChoose.ClientID + "', '" + (fgi.GroupType == 0 ? "true" : "false") + "', 'rad');");
        this.chkInheritMaxAttachmentSize.Attributes.Add("onclick", "SetInheritance('" + txtMaxAttachmentSize.ClientID + "','" + fgi.GroupAttachmentMaxFileSize.ToString().Replace("'", "\\\'") + "', 'txt');");
        this.chkInheritLogActivity.Attributes.Add("onclick", "SetInheritance('" + chkLogActivity.ClientID + "','" + fgi.GroupLogActivity.ToString().ToLower() + "', 'chk');");

        // Discussion
        string chkList = "'" + radImageSimple.ClientID + ";" + chkEnableBold.ClientID + ";" + chkEnableCode.ClientID + ";" +
                               chkEnableColor.ClientID + ";" + radUrlSimple.ClientID + ";" + chkEnableItalic.ClientID + ";" +
                               radImageAdvanced.ClientID + ";" + radUrlAdvanced.ClientID + ";" + chkEnableQuote.ClientID + ";" +
                               chkEnableStrike.ClientID + ";" + chkEnableUnderline.ClientID + ";" + radImageNo.ClientID + ";" + radUrlNo.ClientID + "'";
        string chkListValues = "'" + fgi.GroupEnableImage.ToString() + ";" + fgi.GroupEnableFontBold.ToString() + ";" + fgi.GroupEnableCodeSnippet.ToString() + ";" +
                               fgi.GroupEnableFontColor.ToString() + ";" + fgi.GroupEnableURL.ToString() + ";" + fgi.GroupEnableFontItalics.ToString() + ";" +
                               fgi.GroupEnableAdvancedImage.ToString() + ";" + fgi.GroupEnableAdvancedURL.ToString() + ";" + fgi.GroupEnableQuote.ToString() + ";" + 
                               fgi.GroupEnableFontStrike.ToString() + ";" + fgi.GroupEnableFontUnderline.ToString() + ";" +
                               !(fgi.GroupEnableAdvancedImage || fgi.GroupEnableImage) + ";" + !(fgi.GroupEnableAdvancedURL || fgi.GroupEnableURL) + "'";
        this.chkInheritDiscussion.Attributes.Add("onclick", "SetInheritance(" + chkList + ", " + chkListValues.ToLower() + ", 'chk');");

    }


    /// <summary>
    /// Sets data to database.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Check MODIFY permission
        if (!CheckPermissions("cms.forums", CMSAdminControl.PERMISSION_MODIFY))
        {
            return;
        }

        // Check required fields
        string errorMessage = new Validator().NotEmpty(txtForumDisplayName.Text, GetString("Forum_General.EmptyDisplayName")).Result;

        if ((errorMessage == String.Empty)&&(this.DisplayMode != ControlDisplayModeEnum.Simple))
        {
            errorMessage = new Validator().NotEmpty(txtForumName.Text, GetString("Forum_General.EmptyCodeName")).IsCodeName(txtForumName.Text.Trim(), GetString("general.errorcodenameinidentificatorformat")).Result;
        }

        if (errorMessage != "")
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
            return;
        }

        // Forum must be on some site
        if (CMSContext.CurrentSite != null)
        {
            int communityGroupId = 0;
            ForumInfo fi = ForumInfoProvider.GetForumInfo(mForumId);
            if (fi != null)
            {
                ForumGroupInfo fgi = ForumGroupInfoProvider.GetForumGroupInfo(fi.ForumGroupID);
                if (fgi != null)
                {
                    communityGroupId = fgi.GroupGroupID;
                }
            }

            ForumInfo forumObj = ForumInfoProvider.GetForumInfo(txtForumName.Text.Trim(), CMSContext.CurrentSiteID, communityGroupId);

            // If forum exists
            if ((forumObj == null) || (forumObj.ForumID == this.mForumId))
            {
                if (forumObj == null)
                {
                    forumObj = ForumInfoProvider.GetForumInfo(this.mForumId);
                }

                if (forumObj != null)
                {
                    if (txtForumDisplayName.Text.Trim() != forumObj.ForumDisplayName)
                    {
                        // Refresh a breadcrumb if used in the tabs layout
                        ScriptHelper.RefreshTabHeader(Page, string.Empty);
                    }

                    // Set properties
                    forumObj.ForumIsLocked = chkForumLocked.Checked;
                    forumObj.ForumOpen = chkForumOpen.Checked;
                    forumObj.ForumDisplayEmails = chkForumDisplayEmails.Checked;
                    forumObj.ForumDescription = txtForumDescription.Text.Trim();
                    forumObj.ForumRequireEmail = chkForumRequireEmail.Checked;
                    forumObj.ForumDisplayName = txtForumDisplayName.Text.Trim();
                    forumObj.ForumUseCAPTCHA = chkCaptcha.Checked;
                    
                    // If display mode is default set other properties
                    if (DisplayMode != ControlDisplayModeEnum.Simple)
                    {
                        forumObj.ForumHTMLEditor = chkUseHTML.Checked;
                        forumObj.ForumName = txtForumName.Text.Trim();

                        // Base URL
                        if (chkInheritBaseUrl.Checked)
                        {
                            forumObj.SetValue("ForumBaseUrl", null);
                        }
                        else
                        {
                            forumObj.ForumBaseUrl = txtBaseUrl.Text;
                        }

                        // Unsubcription URL
                        if (chkInheritUnsubscribeUrl.Checked)
                        {
                            forumObj.SetValue("ForumUnsubscriptionUrl", null);
                        }
                        else
                        {
                            forumObj.ForumUnsubscriptionUrl = txtUnsubscriptionUrl.Text.Trim();
                        }
                        
                    }

                    // Require e-mail adresses
                    if (chkInheritRequireEmail.Checked)
                    {
                        forumObj.SetValue("ForumRequireEmail", null);
                    }
                    else
                    {
                        forumObj.ForumRequireEmail = chkForumRequireEmail.Checked;
                    }

                    // Display e-mail adresses
                    if (chkInheritDisplayEmails.Checked)
                    {
                        forumObj.SetValue("ForumDisplayEmails", null);
                    }
                    else
                    {
                        forumObj.ForumDisplayEmails = chkForumDisplayEmails.Checked;
                    }

                    // Use WYSIWYG editor
                    if (chkInheritUseHTML.Checked)
                    {
                        forumObj.SetValue("ForumHTMLEditor", null);
                    }
                    else
                    {
                        forumObj.ForumHTMLEditor = chkUseHTML.Checked;
                    }

                    // Use CAPTCHA 
                    if (chkInheritCaptcha.Checked)
                    {
                        forumObj.SetValue("ForumUseCAPTCHA", null);
                    }
                    else
                    {
                        forumObj.ForumUseCAPTCHA = chkCaptcha.Checked;
                    }

                    // Author can delete own posts
                    if (chkInheritAuthorDelete.Checked)
                    {
                        forumObj.SetValue("ForumAuthorDelete", null);
                    }
                    else
                    {
                        forumObj.ForumAuthorDelete = chkAuthorDelete.Checked;
                    }

                    // Author can delete own posts
                    if (chkInheritAuthorEdit.Checked)
                    {
                        forumObj.SetValue("ForumAuthorEdit", null);
                    }
                    else
                    {
                        forumObj.ForumAuthorEdit = chkAuthorEdit.Checked;
                    }

                    // Image max side size
                    if (chkInheritMaxSideSize.Checked)
                    {
                        forumObj.SetValue("ForumImageMaxSideSize", null);
                    }
                    else
                    {
                        forumObj.ForumImageMaxSideSize = ValidationHelper.GetInteger(txtImageMaxSideSize.Text, 400);
                    }

                    // Answer limit
                    if (chkInheritIsAnswerLimit.Checked)
                    {
                        forumObj.SetValue("ForumIsAnswerLimit", null);
                    }
                    else
                    {
                        forumObj.ForumIsAnswerLimit = ValidationHelper.GetInteger(txtIsAnswerLimit.Text, 5);
                    }

                    // Forum type
                    if (chkInheritType.Checked)
                    {
                        forumObj.SetValue("ForumType", null);
                    }
                    else
                    {
                        forumObj.ForumType = (radTypeChoose.Checked ? 0 : (radTypeDiscussion.Checked ? 1 : 2));
                    }

                    // Inherited values
                    if (chkInheritBaseUrl.Checked)
                    {
                        forumObj.ForumBaseUrl = null;
                    }

                    if (chkInheritUnsubscribeUrl.Checked)
                    {
                        forumObj.ForumUnsubscriptionUrl = null;
                    }

                    if (chkInheritMaxAttachmentSize.Checked)
                    {
                        forumObj.ForumAttachmentMaxFileSize = -1;
                    }
                    else
                    {
                        forumObj.ForumAttachmentMaxFileSize = ValidationHelper.GetInteger(this.txtMaxAttachmentSize.Text, 0);
                    }

                    // Only if on-line marketing is available
                    if (this.plcOnline.Visible) 
                    {
                        if (chkInheritLogActivity.Checked)
                        {
                            forumObj.SetValue("ForumLogActivity", null);
                        }
                        else
                        {
                            forumObj.ForumLogActivity = chkLogActivity.Checked;
                        }
                    }


                    #region "Discussion"

                    if (chkInheritDiscussion.Checked)
                    {
                        // Inherited
                        forumObj.SetValue("ForumDiscussionActions", null);
                    }
                    else
                    {
                        // Set discusion properties
                        forumObj.ForumEnableQuote = chkEnableQuote.Checked;
                        forumObj.ForumEnableFontBold = chkEnableBold.Checked;
                        forumObj.ForumEnableFontItalics = chkEnableItalic.Checked;
                        forumObj.ForumEnableFontUnderline = chkEnableUnderline.Checked;
                        forumObj.ForumEnableFontStrike = chkEnableStrike.Checked;
                        forumObj.ForumEnableFontColor = chkEnableColor.Checked;
                        forumObj.ForumEnableCodeSnippet = chkEnableCode.Checked;
                        forumObj.ForumEnableAdvancedImage = radImageAdvanced.Checked;
                        forumObj.ForumEnableAdvancedURL = radUrlAdvanced.Checked;
                        forumObj.ForumEnableImage = radImageSimple.Checked;
                        forumObj.ForumEnableURL = radUrlSimple.Checked;
                    }

                    #endregion

                    ForumInfoProvider.SetForumInfo(forumObj);

                    ReloadData();

                    this.RaiseOnSaved();

                    lblInfo.Visible = true;
                    lblInfo.Text = GetString("General.ChangesSaved");
                }
                else
                {
                    // Forum does not exist
                    lblError.Visible = true;
                    lblError.Text = GetString("Forum_Edit.ForumDoesNotExist");
                }
            }
            else
            {
                // Forum already exists
                lblError.Visible = true;
                lblError.Text = GetString("Forum_Edit.ForumAlreadyExists");
            }
        }
    }

    #endregion
}
