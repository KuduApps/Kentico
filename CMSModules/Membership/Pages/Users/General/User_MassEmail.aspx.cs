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
using System.Net.Mail;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.EmailEngine;
using CMS.SiteProvider;
using CMS.FormControls;
using CMS.IO;

public partial class CMSModules_Membership_Pages_Users_General_User_MassEmail : CMSUsersPage
{
    private int siteId = 0;
    private FormEngineUserControl groupsControl = null;


    protected void Page_Load(object sender, EventArgs e)
    {
        // Don't show generic roles in role selector except role 'Everyone'
        roles.UseFriendlyMode = true;
        roles.DisplayEveryone = true;

        // Get current user
        CurrentUserInfo currentUser = CMSContext.CurrentUser;
        if (currentUser == null)
        {
            return;
        }

        this.CurrentMaster.DisplaySiteSelectorPanel = true;

        this.btnSend.Click += new EventHandler(btnSend_Click);
        this.siteSelector.UniSelector.OnSelectionChanged += new EventHandler(Site_Changed);
        this.siteSelector.DropDownSingleSelect.AutoPostBack = true;
        this.siteSelector.AllowSetSpecialFields = true;

        // Try to get site ID from query string
        siteId = SiteID;

        // Disable sites dropdown in cmsdesk
        if (siteId > 0)
        {
            // Hide site selector
            this.CurrentMaster.DisplaySiteSelectorPanel = false;
            siteSelector.StopProcessing = true;
        }
        else
        {
            if (!RequestHelper.IsPostBack())
            {
                siteSelector.Value = UniSelector.US_ALL_RECORDS;
            }

            // Load selected site from site selector
            if (RequestHelper.IsPostBack())
            {
                siteId = ValidationHelper.GetInteger(siteSelector.Value, 0);
            }
            // Set siteid to other selectors
            this.roles.GlobalRoles = (siteSelector.SiteID == UniSelector.US_ALL_RECORDS);
            this.roles.SiteRoles = !(siteSelector.SiteID == UniSelector.US_ALL_RECORDS);
        }

        this.roles.SiteID = siteId;
        this.roles.CurrentSelector.SelectionMode = SelectionModeEnum.Multiple;
        this.roles.CurrentSelector.ResourcePrefix = "addroles";
        this.roles.UseCodeNameForSelection = false;
        this.roles.ShowSiteFilter = false;

        this.users.SiteID = siteId;
        this.users.IsLiveSite = false;
        this.users.UniSelector.ReturnColumnName = "UserID";        
        this.users.ShowSiteFilter = false;

        // Display/hide specific selectors according to selected site
        EnsureSelectorPanels();

        // Add current user email at first load        
        if (!RequestHelper.IsPostBack() && (currentUser != null))
        {
            txtFrom.Text = currentUser.Email;
        }

        // Initialize masterpage and other controls
        this.lblSite.Text = GetString("general.site") + ResHelper.Colon;
        this.lblFrom.Text = GetString("general.fromemail") + ResHelper.Colon;
        this.lblSubject.Text = GetString("general.subject") + ResHelper.Colon;
        this.btnSend.Text = GetString("general.send");
        this.uploader.AddButtonImagePath = GetImageUrl("Objects/CMS_User/addattachment.png");
        this.pnlAttachments.GroupingText = GetString("general.attachments");

        ScriptHelper.RegisterClientScriptBlock(this.Page, typeof(string), "IsSubjectEmpty", ScriptHelper.GetScript(@"
        function IsEmailEmpty()
        {
            var obj = document.getElementById('" + txtSubject.ClientID + @"');
            if (obj != null)
            {
                if ((obj.value == null) || obj.value == '' || obj.value.replace(/ /g,'') == '')
                {
                    if (!confirm(" + ScriptHelper.GetString(GetString("massemail.emptyemailsubject")) + @"))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        "));

        btnSend.OnClientClick = "return IsEmailEmpty()";

        // Hides HTML or text area according to e-mail format in settings
        EnsureEmailFormatRegions();

        // Initialize the e-mail text editor
        InitHTMLEditor();
    }


    /// <summary>
    /// Handles change in site selection.
    /// </summary>
    protected void Site_Changed(object sender, EventArgs e)
    {
        // Delete users
        users.Value = null;
        users.ReloadData();

        // Delete roles
        roles.Value = null;
        roles.Reload(true);

        // Delete groups
        if (groupsControl != null)
        {
            groupsControl.Value = String.Empty;
            groupsControl.SetValue("reloaddata", true);
        }

        // Hide HTML or text area according to e-mail format in settings
        EnsureEmailFormatRegions();

        pnlUpdate.Update();
    }


    /// <summary>
    /// Displays or hides specific selectors according to selected site.
    /// </summary>
    protected void EnsureSelectorPanels()
    {
        if (siteId > 0)
        {
            // Check group availability and try to load group selector
            if (AddGroupSelector(siteId))
            {
                // Display groups panel
                pnlAccordionGroups.Visible = true;
            }
            else
            {
                // Hide groups panel
                pnlAccordionGroups.Visible = false;
            }
        }
        else
        {
            // Hide groups panels
            pnlAccordionGroups.Visible = false;
        }
    }


    /// <summary>
    /// Loads group selector control to the page.
    /// </summary>
    /// <param name="siteId">Site ID</param>
    /// <returns>Returns true if site contains group and group selector was loaded</returns>
    private bool AddGroupSelector(int siteId)
    {
        SiteInfo si = SiteInfoProvider.GetSiteInfo(siteId);
        if ((si != null) && (ModuleCommands.CommunitySiteHasGroup(si.SiteID)))
        {
            groupsControl = Page.LoadControl("~/CMSModules/Groups/FormControls/MultipleGroupSelector.ascx") as FormEngineUserControl;
            if (groupsControl != null)
            {
                this.groupsControl.FormControlParameter = siteId;
                this.groupsControl.IsLiveSite = false;
                this.groupsControl.ID = "selectgroups";
                this.groupsControl.ShortID = "sg";
                this.groupsControl.SetValue("ReturnColumnName", "GroupID");

                this.plcGroupSelector.Controls.Add(groupsControl);

                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Clear page.
    /// </summary>
    protected void btnClear_Click(object sender, EventArgs e)
    {
        this.txtSubject.Text = String.Empty;
        this.txtPlainText.Text = String.Empty;
        this.htmlText.ResolvedValue = String.Empty;
        this.txtFrom.Text = CMSContext.CurrentUser.Email;

        if (groupsControl != null)
        {
            groupsControl.Value = String.Empty;
            groupsControl.SetValue("reloaddata", true);
        }

        users.Value = String.Empty;
        users.ReloadData();

        roles.Value = String.Empty;
        roles.Reload(true);
    }


    /// <summary>
    /// Sends the email.
    /// </summary>
    protected void btnSend_Click(object sender, EventArgs e)
    {
        // Check "modify" permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Users", "Modify"))
        {
            RedirectToAccessDenied("CMS.Users", "Modify");
        }

        // Validate first
        string errorMessage = new Validator().IsEmail(this.txtFrom.Text, GetString("general.correctemailformat")).Result;

        // Get recipients
        string groupIds = null;
        if (groupsControl != null)
        {
            groupIds = Convert.ToString(this.groupsControl.Value);
        }
        string userIDs = Convert.ToString(this.users.Value);
        string roleIDs = Convert.ToString(this.roles.Value);

        if (string.IsNullOrEmpty(groupIds) && string.IsNullOrEmpty(userIDs) && string.IsNullOrEmpty(roleIDs))
        {
            errorMessage = GetString("massemail.norecipients");
        }

        if (!string.IsNullOrEmpty(errorMessage))
        {
            this.lblError.Text = errorMessage;
            this.lblError.Visible = true;
            return;
        }

        // Get resolver to resolve context macros
        ContextResolver resolver = new ContextResolver();

        // Create the message
        EmailMessage message = new EmailMessage();
        message.Subject = resolver.ResolveMacros(this.txtSubject.Text);
        message.From = this.txtFrom.Text;
        if (plcText.Visible)
        {
            message.Body = resolver.ResolveMacros(htmlText.ResolvedValue);
        }
        if (plcPlainText.Visible)
        {
            message.PlainTextBody = resolver.ResolveMacros(txtPlainText.Text);
        }

        // Get the attachments
        HttpPostedFile[] attachments = this.uploader.PostedFiles;
        foreach (HttpPostedFile att in attachments)
        {
            message.Attachments.Add(new EmailAttachment(StreamWrapper.New(att.InputStream), Path.GetFileName(att.FileName), Guid.NewGuid(), DateTime.Now, siteId));
        }

        // Check if list of roleIds contains generic role 'Everyone'
        bool containsEveryone = false;
        RoleInfo roleEveryone = null;

        if (!String.IsNullOrEmpty(roleIDs))
        {
            roleEveryone = RoleInfoProvider.GetRoleInfo(CMSConstants.ROLE_EVERYONE, siteId);
            if ((roleEveryone != null) && (";" + roleIDs + ";").Contains(";" + roleEveryone.RoleID.ToString() + ";"))
            {
                containsEveryone = true;
            }
        }

        // Send messages using email engine
        EmailSender.SendMassEmails(message, userIDs, roleIDs, groupIds, siteId, containsEveryone);

        this.lblInfo.Text = GetString("massemail.emailsent");
        this.lblInfo.Visible = true;

        this.btnClear.Visible = true;
    }


    /// <summary>
    /// Initializes HTML editor's settings.
    /// </summary>
    protected void InitHTMLEditor()
    {
        this.htmlText.AutoDetectLanguage = false;
        this.htmlText.DefaultLanguage = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        this.htmlText.ToolbarSet = "SimpleEdit";
        this.htmlText.MediaDialogConfig.UseFullURL = true;
        this.htmlText.LinkDialogConfig.UseFullURL = true;
        this.htmlText.QuickInsertConfig.UseFullURL = true;
    }


    /// <summary>
    /// Hides HTML or text area according to e-mail format in settings.
    /// </summary>
    protected void EnsureEmailFormatRegions()
    {
        string siteName = null;
        plcPlainText.Visible = true;
        plcText.Visible = true;

        if (siteId > 0)
        {
            // Get site name
            siteName = SiteInfoProvider.GetSiteName(siteId);
        }

        // Get e-mail format from settings
        EmailFormatEnum emailFormat = EmailHelper.GetEmailFormat(siteName);
        switch (emailFormat)
        {
            case EmailFormatEnum.Html:
                plcPlainText.Visible = false;
                break;

            case EmailFormatEnum.PlainText:
                plcText.Visible = false;
                break;
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Set proper header CSS classes for accordeon
        for (int i = 0; i < ajaxAccordion.Panes.Count; i++)
        {
            if (i == ajaxAccordion.SelectedIndex)
            {
                ajaxAccordion.Panes[i].HeaderCssClass = ajaxAccordion.HeaderSelectedCssClass;
            }
            else
            {
                ajaxAccordion.Panes[i].HeaderCssClass = ajaxAccordion.HeaderCssClass;
            }
        }
    }
}
