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

using CMS.Notifications;
using CMS.GlobalHelper;
using CMS.FormEngine;
using CMS.CMSHelper;

public partial class CMSModules_Notifications_Controls_TemplateTextEdit : TemplateTextEdit
{
    #region "Variables"

    public string noneStyle = string.Empty;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Sets/Gets TemplateSubject textbox field.
    /// </summary>
    public override string TemplateSubject
    {
        get
        {
            return DataHelper.GetNotEmpty(txtSubject.Text, "");
        }
        set
        {
            txtSubject.Text = value;
        }
    }


    /// <summary>
    /// Sets/Gets TemplatePlainText textarea field.
    /// </summary>
    public override string TemplatePlainText
    {
        get
        {
            return DataHelper.GetNotEmpty(txtPlainText.Text, "");
        }
        set
        {
            txtPlainText.Text = value;
        }
    }


    /// <summary>
    /// Sets/Gets TemplateHTMLText textarea field.
    /// </summary>
    public override string TemplateHTMLText
    {
        get
        {
            return DataHelper.GetNotEmpty(htmlText.ResolvedValue, "");
        }
        set
        {
            htmlText.Value = value;
        }
    }

    #endregion


    #region "Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // If ID´s not specified return 
        if ((this.TemplateID == 0) || (this.GatewayID == 0))
        {
            return;
        }

        // Get resource strings  
        lblHTMLText.ResourceString = "notification.template.html";
        lblPlainText.ResourceString = "notification.template.plain";
        lblSubject.ResourceString = "general.subject";

        // Get gateway name
        NotificationGatewayInfo ngi = NotificationGatewayInfoProvider.GetNotificationGatewayInfo(this.GatewayID);
        if (ngi == null)
        {
            throw new Exception("NotificationGatewayInfo with this GatewayID does not exist.");
        }

        // Setup control according to NotificationGatewayInfo
        this.plcSubject.Visible = ngi.GatewaySupportsEmail;
        this.plcPlainText.Visible = ngi.GatewaySupportsPlainText;
        this.plcHTMLText.Visible = ngi.GatewaySupportsHTMLText;

        if (this.plcHTMLText.Visible)
        {
            // Initialize HTML editor
            htmlText.AutoDetectLanguage = false;
            htmlText.DefaultLanguage = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            htmlText.EditorAreaCSS = FormHelper.GetHtmlEditorAreaCss(CMSContext.CurrentSiteName);
            htmlText.ToolbarSet = "Basic";
            htmlText.MediaDialogConfig.UseFullURL = true;
            htmlText.LinkDialogConfig.UseFullURL = true;
            htmlText.QuickInsertConfig.UseFullURL = true;
        }

        // If gateway does not support any of text fields inform about it. 
        if (!ngi.GatewaySupportsEmail && !ngi.GatewaySupportsHTMLText && !ngi.GatewaySupportsPlainText)
        {
            noneStyle = "width:100%";
            plcNoTextbox.Visible = true;
            lblNoTextbox.Text = string.Format(GetString("notifications.templatetext.notextbox"), HTMLHelper.HTMLEncode(ngi.GatewayDisplayName));
        }

        // Get existing TemplateTextInfoObject or create new object
        NotificationTemplateTextInfo ntti = NotificationTemplateTextInfoProvider.GetNotificationTemplateTextInfo(this.GatewayID, this.TemplateID);
        if (ntti == null)
        {
            ntti = new NotificationTemplateTextInfo();
        }

        // Setup properties
        if (!URLHelper.IsPostback())
        {
            this.TemplateSubject = ntti.TemplateSubject;
            this.TemplateHTMLText = ntti.TemplateHTMLText;
            this.TemplatePlainText = ntti.TemplatePlainText;
        }
    }

    #endregion
}
