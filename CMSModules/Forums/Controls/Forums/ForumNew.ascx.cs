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
using CMS.SettingsProvider;

public partial class CMSModules_Forums_Controls_Forums_ForumNew : CMSAdminEditControl
{
    #region "Variables"

    private int mGroupId = 0;
    private int mForumId = 0;
    private Guid mCommunityGroupGUID = Guid.Empty;
    ForumGroupInfo mForumGroup = null;
    int? mCommunityGroupID = null;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the ID of the group for which the new forum should be created.
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


    public ForumGroupInfo ForumGroup
    {
        get
        {
            if (mForumGroup == null)
            {
                mForumGroup = ForumGroupInfoProvider.GetForumGroupInfo(GroupID);
            }

            return mForumGroup;
        }
    }

    /// <summary>
    /// Gets or sets the community group GUID.
    /// </summary>
    public Guid CommunityGroupGUID
    {
        get
        {
            return this.mCommunityGroupGUID;
        }
        set
        {
            this.mCommunityGroupGUID = value;
        }
    }


    /// <summary>
    /// Community group ID
    /// </summary>
    protected int CommunityGroupID
    {
        get
        {
            if (mCommunityGroupID == null)
            {
                BaseInfo groupInfo = BaseInfo.EMPTY_INFO;
                if (CommunityGroupGUID != Guid.Empty)
                {
                    groupInfo = ModuleCommands.CommunityGetGroupInfoByGuid(CommunityGroupGUID);
                }
                else if ((ForumGroup != null) && (ForumGroup.GroupGroupID > 0))
                {
                    groupInfo = ModuleCommands.CommunityGetGroupInfo(ForumGroup.GroupGroupID);
                }

                if (groupInfo != null)
                {
                    mCommunityGroupID = groupInfo.Generalized.ObjectID;
                }
                else
                {
                    mCommunityGroupID = 0;
                }
            }

            return mCommunityGroupID.Value;
        }
    }


    /// <summary>
    /// Gets the ID of the forum which has been created using the control.
    /// </summary>
    public int ForumID
    {
        get
        {
            return this.mForumId;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Visible || this.StopProcessing)
        {
            this.EnableViewState = false;
        }

        // Code name is not editable in simple mode
        if (DisplayMode == ControlDisplayModeEnum.Simple)
        {
            this.plcCodeName.Visible = false;
            this.plcUseHtml.Visible = false;
        }

        txtForumDisplayName.IsLiveSite = this.IsLiveSite;
        txtForumDescription.IsLiveSite = this.IsLiveSite;

        // Control initializations
        this.rfvForumDisplayName.ErrorMessage = GetString("Forum_General.EmptyDisplayName");
        this.rfvForumName.ErrorMessage = GetString("Forum_General.EmptyCodeName");

        // Set strings for labels
        this.lblForumOpen.Text = GetString("Forum_Edit.ForumOpenLabel");
        this.lblForumLocked.Text = GetString("Forum_Edit.ForumLockedLabel");
        this.lblForumDisplayEmails.Text = GetString("Forum_Edit.ForumDisplayEmailsLabel");
        this.lblForumRequireEmail.Text = GetString("Forum_Edit.ForumRequireEmailLabel");
        this.lblForumDisplayName.Text = GetString("Forum_Edit.ForumDisplayNameLabel");
        this.lblForumName.Text = GetString("Forum_Edit.ForumNameLabel");
        this.lblForumModerated.Text = GetString("Forum_Edit.ForumModeratedLabel");
        this.lblUseHTML.Text = GetString("Forum_Edit.UseHtml");
        this.lblCaptcha.Text = GetString("Forum_Edit.useCaptcha");

        this.lblBaseUrl.Text = GetString("Forum_Edit.lblBaseUrl");
        this.lblUnsubscriptionUrl.Text = GetString("Forum_Edit.lblUnsubscriptionUrl");

        // Set strings for checkboxes
        this.chkInheritBaseUrl.Text = GetString("Forum_Edit.InheritBaseUrl");
        this.chkInheritUnsubscribeUrl.Text = GetString("Forum_Edit.InheritUnsupscriptionUrl");
        this.chkInheritCaptcha.Text = GetString("Forum_Edit.InheritUnsupscriptionUrl");
        this.chkInheritForumDisplayEmails.Text = GetString("Forum_Edit.InheritUnsupscriptionUrl");
        this.chkInheritForumRequireEmail.Text = GetString("Forum_Edit.InheritUnsupscriptionUrl");
        this.chkInheritUseHTML.Text = GetString("Forum_Edit.InheritUnsupscriptionUrl");

        this.btnOk.Text = GetString("General.OK");

        // Check whether the forum group still exists
        if (mGroupId > 0)
        {
            EditedObject = ForumGroup;
        }

        if (ForumGroup != null)
        {
            if (!this.IsLiveSite && !RequestHelper.IsPostBack())
            {
                ReloadData();
            }

            string script = "";

            // Add onclick actions for 'inherit' checkboxes
            chkInheritUnsubscribeUrl.Attributes.Add("onclick", "SetInheritance('" + txtUnsubscriptionUrl.ClientID + "', '" + ForumGroup.GroupUnsubscriptionUrl + "', 'txt')");
            chkInheritBaseUrl.Attributes.Add("onclick", "SetInheritance('" + txtBaseUrl.ClientID + "','" + ForumGroup.GroupBaseUrl + "', 'txt')");
            chkInheritCaptcha.Attributes.Add("onclick", "SetInheritance('" + chkCaptcha.ClientID + "'," + ForumGroup.GroupUseCAPTCHA.ToString().ToLower() + ", 'chk')");
            chkInheritForumDisplayEmails.Attributes.Add("onclick", "SetInheritance('" + chkForumDisplayEmails.ClientID + "'," + ForumGroup.GroupDisplayEmails.ToString().ToLower() + ", 'chk')");
            chkInheritForumRequireEmail.Attributes.Add("onclick", "SetInheritance('" + chkForumRequireEmail.ClientID + "'," + ForumGroup.GroupRequireEmail.ToString().ToLower() + ", 'chk')");
            chkInheritUseHTML.Attributes.Add("onclick", "SetInheritance('" + chkUseHTML.ClientID + "'," + ForumGroup.GroupHTMLEditor.ToString().ToLower() + ", 'chk')");

            // Create script for handle inherited values
            script = @"
                function LoadDefault(clientId, inheritClientId)
                {
                    var objToDisable = document.getElementById(clientId);
                    var objToCheck = document.getElementById(inheritClientId);
                    if (objToDisable != null) {
                        objToDisable.disabled = true;
                        objToCheck.checked = true;
                    }
                }

                function SetInheritance(clientId, value, type)
                {
                    var obj = document.getElementById(clientId);
                    if (obj != null) {
                        if(obj.disabled)
                        {
                            obj.disabled = false;
                        }
                        else
                        {
                            obj.disabled = true;
                            if (type == 'txt') {
                                obj.value = value;
                            } else {
                                obj.checked = value;
                            }
                        }
                    }
                }";

            ltrScript.Text = ScriptHelper.GetScript(script);
        }

        // Show/hide URL textboxes
        plcBaseAndUnsubUrl.Visible = (DisplayMode != ControlDisplayModeEnum.Simple);

        // Base URL textbox enable or disable
        if (chkInheritBaseUrl.Checked)
        {
            txtBaseUrl.Text = ForumGroup.GroupBaseUrl;
            txtBaseUrl.Attributes.Add("disabled", "true");
        }
        else
        {
            txtBaseUrl.Attributes.Remove("disabled");
        }

        // Unsubscription URL textbox enable or disable
        if (chkInheritUnsubscribeUrl.Checked)
        {
            txtUnsubscriptionUrl.Text = ForumGroup.GroupUnsubscriptionUrl;
            txtUnsubscriptionUrl.Attributes.Add("disabled", "true");
        }
        else
        {
            txtUnsubscriptionUrl.Attributes.Remove("disabled");
        }
    }



    public override void ReloadData()
    {
        ClearForm();
        string defScript = "";

        // Set properties
        txtUnsubscriptionUrl.Text = ForumGroup.GroupUnsubscriptionUrl;
        txtBaseUrl.Text = ForumGroup.GroupBaseUrl;
        chkUseHTML.Checked = ForumGroup.GroupHTMLEditor;
        chkForumRequireEmail.Checked = ForumGroup.GroupRequireEmail;
        chkForumDisplayEmails.Checked = ForumGroup.GroupDisplayEmails;
        chkCaptcha.Checked = ForumGroup.GroupUseCAPTCHA;

        txtBaseUrl.Attributes.Add("disabled", "true");
        txtUnsubscriptionUrl.Attributes.Add("disabled", "true");

        // Create script for load default values
        defScript += "LoadDefault('" + chkUseHTML.ClientID + "', '" + chkInheritUseHTML.ClientID + "'); ";
        defScript += "LoadDefault('" + chkForumDisplayEmails.ClientID + "','" + chkInheritForumDisplayEmails.ClientID + "'); ";
        defScript += "LoadDefault('" + chkForumRequireEmail.ClientID + "','" + chkInheritForumRequireEmail.ClientID + "'); ";
        defScript += "LoadDefault('" + chkCaptcha.ClientID + "','" + chkInheritCaptcha.ClientID + "'); ";

        chkInheritBaseUrl.Checked = true;
        chkInheritUnsubscribeUrl.Checked = true;

        ltrScript.Text += ScriptHelper.GetScript(defScript);
    }


    /// <summary>
    /// Clears the form fields to defalut values.
    /// </summary>
    public override void ClearForm()
    {
        // Clears all textboxes
        this.txtBaseUrl.Text = "";
        this.txtForumDescription.Text = "";
        this.txtForumDisplayName.Text = "";
        this.txtForumName.Text = "";
        this.txtUnsubscriptionUrl.Text = "";

        // Uncheck all checkboxes
        this.chkCaptcha.Checked = false;
        this.chkForumDisplayEmails.Checked = false;
        this.chkForumModerated.Checked = false;
        this.chkForumOpen.Checked = true;
        this.chkForumLocked.Checked = false;
        this.chkForumRequireEmail.Checked = false;
        this.chkInheritBaseUrl.Checked = true;
        this.chkInheritUnsubscribeUrl.Checked = true;
        this.chkUseHTML.Checked = false;
    }


    /// <summary>
    /// Sets data to database.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Check MODIFY permission for forums
        if (!CheckPermissions("cms.forums", CMSAdminControl.PERMISSION_MODIFY))
        {
            return;
        }

        string codeName = txtForumName.Text.Trim();

        // Get safe code name for simple display mode
        if (DisplayMode == ControlDisplayModeEnum.Simple)
        {
            codeName = ValidationHelper.GetCodeName(txtForumDisplayName.Text.Trim(), 50) + "_group_" + this.CommunityGroupGUID;
        }

        // Check required fields
        string errorMessage = new Validator().NotEmpty(txtForumDisplayName.Text, GetString("Forum_General.EmptyDisplayName")).NotEmpty(codeName, GetString("Forum_General.EmptyCodeName")).Result;

        if (errorMessage == String.Empty && !ValidationHelper.IsCodeName(codeName))
        {
            errorMessage = GetString("general.errorcodenameinidentificatorformat");
        }

        if (errorMessage == "")
        {
            if (CMSContext.CurrentSite != null)
            {
                // If forum with given name already exists show error message
                if (ForumInfoProvider.GetForumInfo(codeName, CMSContext.CurrentSiteID, CommunityGroupID) != null)
                {
                    lblError.Visible = true;
                    lblError.Text = GetString("Forum_Edit.ForumAlreadyExists");
                    return;
                }

                // Create new object
                ForumInfo forumObj = new ForumInfo();

                // Set new properties
                forumObj.ForumSiteID = CMSContext.CurrentSite.SiteID;
                forumObj.ForumIsLocked = chkForumLocked.Checked;
                forumObj.ForumOpen = chkForumOpen.Checked;
                forumObj.ForumDisplayEmails = chkForumDisplayEmails.Checked;
                forumObj.ForumDescription = txtForumDescription.Text.Trim();
                forumObj.ForumRequireEmail = chkForumRequireEmail.Checked;
                forumObj.ForumDisplayName = txtForumDisplayName.Text.Trim();
                forumObj.ForumName = codeName;
                forumObj.ForumGroupID = this.mGroupId;
                forumObj.ForumModerated = chkForumModerated.Checked;
                forumObj.ForumAccess = 40000;
                forumObj.ForumPosts = 0;
                forumObj.ForumThreads = 0;
                forumObj.ForumPostsAbsolute = 0;
                forumObj.ForumThreadsAbsolute = 0;
                forumObj.ForumOrder = 0;
                forumObj.ForumUseCAPTCHA = chkCaptcha.Checked;
                forumObj.ForumCommunityGroupID = CommunityGroupID;

                // For simple display mode skip some properties
                if (DisplayMode != ControlDisplayModeEnum.Simple)
                {
                    forumObj.ForumBaseUrl = txtBaseUrl.Text.Trim();
                    forumObj.ForumUnsubscriptionUrl = txtUnsubscriptionUrl.Text.Trim();
                    forumObj.ForumHTMLEditor = chkUseHTML.Checked;

                    if (chkInheritBaseUrl.Checked)
                    {
                        forumObj.ForumBaseUrl = null;
                    }

                    if (chkInheritUnsubscribeUrl.Checked)
                    {
                        forumObj.ForumUnsubscriptionUrl = null;
                    }
                }

                // Clear inherited values
                if (chkInheritUseHTML.Checked)
                {
                    forumObj.SetValue("ForumHTMLEditor", null);
                }

                if (chkInheritForumDisplayEmails.Checked)
                {
                    forumObj.SetValue("ForumDisplayEmails", null);
                }

                if (chkInheritForumRequireEmail.Checked)
                {
                    forumObj.SetValue("ForumRequireEmail", null);
                }

                if (chkInheritCaptcha.Checked)
                {
                    forumObj.SetValue("ForumUseCAPTCHA", null);
                }

                // Check licence
                if (ForumInfoProvider.LicenseVersionCheck(URLHelper.GetCurrentDomain(), FeatureEnum.Forums, VersionActionEnum.Insert))
                {
                    ForumInfoProvider.SetForumInfo(forumObj);
                    this.mForumId = forumObj.ForumID;
                    this.RaiseOnSaved();
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = GetString("LicenseVersionCheck.Forums");
                }
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
        }
    }
}
