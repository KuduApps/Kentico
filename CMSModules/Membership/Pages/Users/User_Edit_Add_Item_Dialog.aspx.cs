using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using System.Text;

public partial class CMSModules_Membership_Pages_Users_User_Edit_Add_Item_Dialog : CMSModalPage
{
    #region "Variables"

    /// <summary>
    /// Indicates if send notification field is used.
    /// </summary>
    private bool useSendNotification = false;

    #endregion


    protected override void OnPreInit(EventArgs e)
    {
        String identificator = QueryHelper.GetString("params", null);
        Hashtable parameters = (Hashtable)WindowHelper.GetItem(identificator);

        this.useSendNotification = (QueryHelper.GetInteger("UseSendNotification", 0) == 1);

        // Take only first column
        parameters["AdditionalColumns"] = null;

        base.OnPreInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Users", "Read"))
        {
            RedirectToAccessDenied("CMS.Users", "Read");
        }

        ScriptHelper.RegisterWOpenerScript(Page);
        ScriptHelper.RegisterJQuery(Page);


        // Try to get parameters...
        string identificator = QueryHelper.GetString("params", null);
        Hashtable parameters = (Hashtable)WindowHelper.GetItem(identificator);

        // ... and validate hash
        if ((QueryHelper.ValidateHash("hash", "selectedvalue")) && (parameters != null))
        {
            // Take only first column
            parameters["AdditionalColumns"] = null;

            selectionDialog.LocalizeItems = QueryHelper.GetBoolean("localize", true);

            // Load resource prefix
            string resourcePrefix = ValidationHelper.GetString(parameters["ResourcePrefix"], "general");

            // Set the page title
            string titleText = GetString(resourcePrefix + ".selectitem|general.selectitem");

            // Validity group text
            pnlDateTime.GroupingText = GetString(resourcePrefix + ".bindingproperties");

            CurrentMaster.Title.TitleText = titleText;
            Page.Title = titleText;

            string imgPath = ValidationHelper.GetString(parameters["IconPath"], null);
            if (String.IsNullOrEmpty(imgPath))
            {
                string objectType = ValidationHelper.GetString(parameters["ObjectType"], null);

                CurrentMaster.Title.TitleImage = GetObjectIconUrl(objectType, null);
            }
            else
            {
                CurrentMaster.Title.TitleImage = imgPath;
            }

            // Cancel button
            btnCancel.ResourceString = "general.cancel";
            btnCancel.Attributes.Add("onclick", "return US_Cancel();");

            SelectionModeEnum selectionMode = (SelectionModeEnum)parameters["SelectionMode"];
        }
        else
        {
            // Redirect to error page
            URLHelper.Redirect(ResolveUrl("~/CMSMessages/Error.aspx?title=" + ResHelper.GetString("dialogs.badhashtitle") + "&text=" + ResHelper.GetString("dialogs.badhashtext")));
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        btnOk.ResourceString = "general.ok";

        // Show/hide send notification field
        this.pnlSendNotification.Visible = this.useSendNotification;

        base.OnPreRender(e);
    }


    protected override void OnPreRenderComplete(EventArgs e)
    {
        base.OnPreRenderComplete(e);

        String okScript = null;

        if (selectionDialog.UniGrid.IsEmpty)
        {
            pnlDateTime.Visible = false;
            okScript = "return US_Cancel()";
        }
        else
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("function okScript() {");
            sb.AppendFormat("    if ((typeof(isDateTimeValid) != 'undefined') && (!isDateTimeValid('{0}'))) {{", this.ucDateTime.DateTimeTextBox.ClientID);
            sb.NewLine();
            sb.AppendFormat("        var lblErr = $j('#{0}');", this.lblError.ClientID);
            sb.NewLine();
            sb.AppendFormat("        lblErr.text ('{0}');", this.GetString("basicform.errorinvaliddatetimerange"));
            sb.NewLine();
            sb.AppendLine("        lblErr.show();");
            sb.AppendLine("    } else {");
            sb.AppendFormat("        var date = $j('#{0}').val();", this.ucDateTime.DateTimeTextBox.ClientID);
            sb.NewLine();
            sb.AppendFormat("        var sendNotification = ($j('#{0}').attr('checked') == 'checked') ? true : false;", this.chkSendNotification.ClientID);
            sb.NewLine();
            sb.AppendLine("        if (wopener.setNewDateTime != null) { wopener.setNewDateTime(date); }");
            sb.AppendLine("        if (wopener.setNewSendNotification != null) { wopener.setNewSendNotification(sendNotification); }");
            sb.AppendLine("        US_Submit();");
            sb.AppendLine("    }");
            sb.AppendLine("    return false;");
            sb.AppendLine("}");

            ScriptHelper.RegisterClientScriptBlock(this.Page, typeof(string), "ok", ScriptHelper.GetScript(sb.ToString()));

            okScript = "return okScript();";
        }

        btnOk.Attributes.Add("onclick", okScript);
    }
}

