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

using CMS.Newsletter;
using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_Newsletters_Tools_Newsletters_Newsletter_ContentEditorFooter : CMSUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.btnInsertField.Attributes.Add("onMouseOver", "RememberFocusedRegion();");
        this.btnInsertField.Attributes.Add("onClick", " if ( window.InsertMacro != null ) {  window.InsertMacro('{%' + document.getElementById('" + this.lstInsertField.ClientID + "').value +  '%}'); } return false;");

        btnInsertField.Text = GetString("NewsletterTemplate_Edit.Insert");
        lblInsertField.Text = GetString("NewsletterTemplate_Edit.TemplateInsertFieldLabel");
        lblInsertMacro.Text = GetString("macroselector.insertmacro") + ":";

        macroSelectorElm.Resolver = EmailTemplateMacros.NewsletterResolver;
        if (QueryHelper.GetInteger("issueid", 0) == 0)
        {
            macroSelectorElm.TopOffset = 135;
            macroSelectorElm.LeftOffset = 10;
        }

        if (!RequestHelper.IsPostBack())
        {
            FillFieldsList();
        }

        // If item in list is selected, it gets focus => we must remember id of last focused editable region
        lstInsertField.Attributes.Add("onMouseOver", "RememberFocusedRegion();");
    }


    /// <summary>
    /// Fills list of available fields.
    /// </summary>
    protected void FillFieldsList()
    {
        ListItem newItem = new ListItem(GetString("general.email"), IssueHelper.MacroEmail);
        lstInsertField.Items.Add(newItem);
        newItem = new ListItem(GetString("NewsletterTemplate.MacroFirstName"), IssueHelper.MacroFirstName);
        lstInsertField.Items.Add(newItem);
        newItem = new ListItem(GetString("NewsletterTemplate.MacroLastName"), IssueHelper.MacroLastName);
        lstInsertField.Items.Add(newItem);
        newItem = new ListItem(GetString("NewsletterTemplate.MacroUnsubscribeLink"), IssueHelper.MacroUnsubscribeLink);
        lstInsertField.Items.Add(newItem);
    }
}
