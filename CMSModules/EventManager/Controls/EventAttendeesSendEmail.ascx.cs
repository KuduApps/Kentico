using System;
using System.Data;

using CMS.CMSHelper;
using CMS.EventManager;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.TreeEngine;
using CMS.UIControls;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_EventManager_Controls_EventAttendeesSendEmail : CMSAdminControl
{
    #region "Variables"

    private int mEventID = 0;

    #endregion


    #region "Properties"

    /// <summary>
    /// Event ID.
    /// </summary>
    public int EventID
    {
        get
        {
            return mEventID;
        }
        set
        {
            mEventID = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        htmlEmail.AutoDetectLanguage = false;
        htmlEmail.DefaultLanguage = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        htmlEmail.EditorAreaCSS = String.Empty;
    }

    public override void ReloadData(bool forceLoad)
    {
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
        TreeNode nd = tree.SelectSingleNode(EventID, CMSContext.PreferredCultureCode, tree.CombineWithDefaultCulture, false);
        if (nd == null)
        {
            lblError.Text = GetString("editedobject.notexists");
            lblError.Visible = true;
            plcSend.Visible = false;
            lblTitle.Visible = false;
            return;
        }

        //Enable controls
        txtSenderName.Enabled = true;
        txtSenderEmail.Enabled = true;
        txtSubject.Enabled = true;
        htmlEmail.Enabled = true;
        btnSend.Enabled = true;
        lblInfo.Visible = true;

        if (forceLoad)
        {
            string siteName = CMSContext.CurrentSiteName;
            txtSenderEmail.Text = SettingsKeyProvider.GetStringValue(siteName + ".CMSEventManagerInvitationFrom");
            txtSenderName.Text = SettingsKeyProvider.GetStringValue(siteName + ".CMSEventManagerSenderName");
            txtSubject.Text = SettingsKeyProvider.GetStringValue(siteName + ".CMSEventManagerInvitationSubject");
        }

        // Disable form if no attendees present or user doesn't have modify permission
        if (CMSContext.CurrentUser.IsAuthorizedPerResource("cms.eventmanager", "Modify"))
        {
            DataSet ds = EventAttendeeInfoProvider.GetEventAttendees(EventID, null, null, "AttendeeID", 1);
            if (DataHelper.DataSourceIsEmpty(ds))
            {
                DisableForm();
                lblInfo.Text = GetString("Events_List.NoAttendees");
                lblInfo.Visible = true;
            }
        }
        else
        {
            DisableForm();
            lblInfo.Text = GetString("events_sendemail.modifypermission");
            lblInfo.Visible = true;
        }
    }


    /// <summary>
    /// Disable form.
    /// </summary>
    private void DisableForm()
    {
        txtSenderName.Enabled = false;
        txtSenderEmail.Enabled = false;
        txtSubject.Enabled = false;
        htmlEmail.Enabled = false;
        btnSend.Enabled = false;
    }


    protected void btnSend_Click(object sender, EventArgs e)
    {
        // Check 'Modify' permission
        if (!CheckPermissions("cms.eventmanager", "Modify"))
        {
            return;
        }

        txtSenderName.Text = txtSenderName.Text.Trim();
        txtSenderEmail.Text = txtSenderEmail.Text.Trim();
        txtSubject.Text = txtSubject.Text.Trim();

        // Validate the fields
        string errorMessage = new Validator().NotEmpty(txtSenderName.Text, GetString("Events_SendEmail.EmptySenderName"))
            .NotEmpty(txtSenderEmail.Text, GetString("Events_SendEmail.EmptySenderEmail"))
            .NotEmpty(txtSubject.Text, GetString("Events_SendEmail.EmptyEmailSubject"))
            .IsEmail(txtSenderEmail.Text, GetString("Events_SendEmail.InvalidEmailFormat"))
            .Result;

        if (!String.IsNullOrEmpty(errorMessage))
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
            return;
        }

        string subject = txtSubject.Text;
        string emailBody = htmlEmail.ResolvedValue;

        // Get event node data
        TreeProvider mTree = new TreeProvider();
        TreeNode node = mTree.SelectSingleNode(EventID);

        if (node != null && String.Equals(node.NodeClassName, "cms.bookingevent", StringComparison.InvariantCultureIgnoreCase))
        {
            // Initialize macro resolver
            MacroResolver resolver = new MacroResolver();
            resolver.KeepUnresolvedMacros = true;
            resolver.SourceData = new object[] { node };

            // Resolve e-mail body and subject macros and make links absolute
            emailBody = resolver.ResolveMacros(emailBody);
            emailBody = URLHelper.MakeLinksAbsolute(emailBody);
            subject = TextHelper.LimitLength(resolver.ResolveMacros(subject), 450);

            // EventSendEmail manages sending e-mails to all attendees
            EventSendEmail ese = new EventSendEmail(EventID, CMSContext.CurrentSiteName,
            subject, emailBody, txtSenderName.Text.Trim(), txtSenderEmail.Text.Trim());

            lblInfo.Visible = true;
            lblInfo.Text = GetString("Events_SendEmail.EmailSent");
        }
    }

    #endregion
}
