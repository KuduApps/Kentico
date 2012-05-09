using System;
using System.Collections;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI;

using CMS.EmailEngine;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.UIControls;

public partial class CMSModules_EmailQueue_EmailQueue_Details : CMSModalSiteManagerPage
{
    #region "Protected variables"

    protected int emailId;


    protected int prevId;


    protected int nextId;


    protected Hashtable mParameters;

    #endregion


    #region "Properties"

    /// <summary>
    /// Hashtable containing dialog parameters.
    /// </summary>
    private Hashtable Parameters
    {
        get
        {
            if (mParameters == null)
            {
                string identificator = QueryHelper.GetString("params", null);
                mParameters = (Hashtable)WindowHelper.GetItem(identificator);
            }
            return mParameters;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!QueryHelper.ValidateHash("hash", "emailid") || Parameters == null)
        {
            return;
        }

        CurrentMaster.Title.TitleText = GetString("emailqueue.details.title");
        CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_EmailQueue/emaildetail.png");

        // Get the ORDER BY column and starting event ID
        string orderBy = ValidationHelper.GetString(Parameters["orderby"], "EmailID DESC");
        if (orderBy.IndexOf(';') >= 0)
        {
            orderBy = "EmailID DESC";  // ORDER BY with semicolon is considered to be dangerous
        }
        string whereCondition = ValidationHelper.GetString(Parameters["where"], string.Empty);

        // Get e-mail ID from query string
        emailId = QueryHelper.GetInteger("emailid", 0);

        if (!RequestHelper.IsPostBack())
        {
            LoadData();
        }

        // Initialize next/previous buttons
        int[] prevNext = EmailInfoProvider.GetPreviousNext(emailId, whereCondition, orderBy);
        if (prevNext != null)
        {
            prevId = prevNext[0];
            nextId = prevNext[1];

            btnPrevious.Enabled = (prevId != 0);
            btnNext.Enabled = (nextId != 0);

            btnPrevious.Click += btnPrevious_Click;
            btnNext.Click += btnNext_Click;
        }

        // Set button caption
        btnNext.Text = GetString("general.next") + " >";
        btnPrevious.Text = "< " + GetString("general.back");
    }

    #endregion


    #region "Button handling"

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        // Redirect to previous
        URLHelper.Redirect(URLHelper.UpdateParameterInUrl(URLHelper.CurrentURL, "emailId", string.Empty + prevId));
    }


    protected void btnNext_Click(object sender, EventArgs e)
    {
        // Redirect to next
        URLHelper.Redirect(URLHelper.UpdateParameterInUrl(URLHelper.CurrentURL, "emailId", string.Empty + nextId));
    }

    #endregion


    #region "Protected methods"

    /// <summary>
    /// Loads data of specific e-mail from DB.
    /// </summary>
    protected void LoadData()
    {
        if (emailId <= 0)
        {
            return;
        }

        // Get specific e-mail
        EmailInfo ei = EmailInfoProvider.GetEmailInfo(emailId);
        EditedObject = ei;

        if (ei == null)
        {
            plcDetails.Visible = false;
            lblInfo.Visible = true;
            return;
        }

        lblFromValue.Text = HTMLHelper.HTMLEncode(ei.EmailFrom);

        if (!ei.EmailIsMass)
        {
            lblToValue.Text = HTMLHelper.HTMLEncode(ei.EmailTo);
        }
        else
        {
            lblToValue.Text = GetString("emailqueue.detail.multiplerecipients");
        }

        lblCcValue.Text = HTMLHelper.HTMLEncode(ei.EmailCc);
        lblBccValue.Text = HTMLHelper.HTMLEncode(ei.EmailBcc);
        lblSubjectValue.Text = HTMLHelper.HTMLEncode(ei.EmailSubject);

        string body = null;

        if (string.IsNullOrEmpty(ei.EmailPlainTextBody))
        {
            body = GetHTMLBody(ei);
        }
        else
        {
            body = GetPlainTextBody(ei);
        }

        // Show/hide send result message
        if (!string.IsNullOrEmpty(ei.EmailLastSendResult))
        {
            lblErrorMessageValue.Text = HTMLHelper.HTMLEncode(ei.EmailLastSendResult);
            plcErrorMessage.Visible = true;
        }
        else
        {
            plcErrorMessage.Visible = false;
        }

        GetAttachments();
    }


    /// <summary>
    /// Gets the HTML body of the e-mail message.
    /// </summary>
    /// <param name="ei">The e-mail message object</param>
    /// <returns>HTML body</returns>
    private string GetHTMLBody(EmailInfo ei)
    {
        string body = ei.EmailBody;

        // Regular expression to search the tracking image in HTML code
        Regex regExp = RegexHelper.GetRegex("(src=\"[^\"]+Track.ashx)\\?[^\"]*", RegexOptions.IgnoreCase);
        Match match = regExp.Match(body);
        if (match.Success && (match.Groups.Count > 0))
        {
            // Remove parameters from tracking image URL so the statistics are not influenced by e-mail previews
            body = regExp.Replace(body, match.Groups[1].Value);
        }

        htmlTemplateBody.Visible = true;
        htmlTemplateBody.ResolvedValue = body;
        htmlTemplateBody.AutoDetectLanguage = false;
        htmlTemplateBody.DefaultLanguage = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        return body;
    }


    /// <summary>
    /// Gets the plain text body of the e-mail message.
    /// </summary>
    /// <param name="ei">The e-mail message object</param>
    /// <returns>Plain-text body</returns>
    private string GetPlainTextBody(EmailInfo ei)
    {
        DiscussionMacroHelper dmh = new DiscussionMacroHelper { ResolveToPlainText = true };
        string body = dmh.ResolveMacros(ei.EmailPlainTextBody);

        body = HTMLHelper.HTMLEncode(body);

        ltlBodyValue.Visible = true;

        // Replace line breaks with br tags and modify discussion macros
        ltlBodyValue.Text = DiscussionMacroHelper.RemoveTags(HTMLHelper.HTMLEncodeLineBreaks(body));

        return body;
    }


    /// <summary>
    /// Gets the attachments for the specified e-mail message.
    /// </summary>
    private void GetAttachments()
    {
        // Get basic info about all attachments attached to current e-mail
        DataSet ds = EmailAttachmentInfoProvider.GetEmailAttachmentInfos(emailId, null, -1, "AttachmentID, AttachmentName, AttachmentSize");
        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            plcAttachments.Visible = true;
            if (ds.Tables.Count > 0)
            {
                int i = 0;
                EmailAttachmentInfo eai = null;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (i > 0)
                    {
                        pnlAttachmentsList.Controls.Add(new LiteralControl("<br />"));
                    }
                    eai = new EmailAttachmentInfo(dr);
                    pnlAttachmentsList.Controls.Add(new LiteralControl(HTMLHelper.HTMLEncode(eai.AttachmentName) +
                        "&nbsp;(" + SqlHelperClass.GetSizeString(eai.AttachmentSize) + ")"));
                    i++;
                }
            }
        }
        else
        {
            plcAttachments.Visible = false;
        }
    }

    #endregion
}