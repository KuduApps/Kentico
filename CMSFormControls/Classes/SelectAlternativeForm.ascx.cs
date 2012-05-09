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
using CMS.FormEngine;
using CMS.SettingsProvider;
using CMS.DataEngine;
using CMS.CMSHelper;

public partial class CMSFormControls_Classes_SelectAlternativeForm : CMS.FormControls.FormEngineUserControl, System.Web.UI.ICallbackEventHandler
{
    private string _callbackArg;


    #region "Properties"

    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return base.Enabled;
        }
        set
        {
            base.Enabled = value;
            this.txtName.Enabled = value;
            this.btnSelect.Enabled = value;
        }
    }


    ///<summary>Gets or sets field value.</summary>
    public override object Value
    {
        get
        {
            return this.txtName.Text;
        }
        set
        {
            this.txtName.Text = (string)value;
        }
    }


    /// <summary>
    /// Gets ClientID of the textbox with transformation name.
    /// </summary>
    public override string ValueElementID
    {
        get
        {
            return txtName.ClientID;
        }
    }

    #endregion


    #region "CallBackHandling"

    public void RaiseCallbackEvent(string eventArgument)
    {
        _callbackArg = eventArgument;
    }


    public string GetCallbackResult()
    {
        return Validate(_callbackArg, true);
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        bool isClassNames = true;
        string classNames = "";
        string javaScript = "";

        string argument = "document.getElementById('" + txtName.ClientID + "').value";
        string clientCallback = "CheckAlternativeForm";
        string CallbackRef = Page.ClientScript.GetCallbackEventReference(this, argument, clientCallback, "'" + lblStatus.ClientID + "'");
        txtName.Attributes["onchange"] = String.Format("javascript:{0}", CallbackRef);

        try
        {
            classNames = this.Form.Data.GetValue("ClassNames").ToString();
        }
        catch
        {
            isClassNames = false;
        }

        btnSelect.Text = GetString("general.select");
        btnSelect.OnClientClick = "SelectAltFormDialog_"+ this.ClientID+"(); return false;";

        string url = "~/CMSFormControls/Selectors/AlternativeFormSelection.aspx?lblElem=" + lblStatus.ClientID + "&txtElem=" + txtName.ClientID;
        if (isClassNames && classNames != string.Empty)
        {
            string[] splitClassNames = classNames.Split(';');
            url += "&classname=" + splitClassNames[0];
        }

        url += "&hash=" + QueryHelper.GetHash(url, false);
        javaScript = "function  SelectAltFormDialog_" + this.ClientID+"(){modalDialog('" + ResolveUrl(url) + "','AltFormSelection', 400, 500); return false;}";

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "SelectDialog_" + this.ClientID, ScriptHelper.GetScript(
            javaScript
        ));

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "SelectAlternativeForm", ScriptHelper.GetScript(
            "function SelectAltForm(formName,txtClientID,lblClientID){if((lblClientID != '') && (txtClientID != '')) { document.getElementById(txtClientID).value = formName;document.getElementById(lblClientID).innerHTML='';} return false;} " +
            "function CheckAlternativeForm(result, context){document.getElementById(context).innerHTML = result; return false; } "
        ));

        ScriptHelper.RegisterDialogScript(this.Page);
    }


    /// <summary>
    /// Returns true if user control is valid.
    /// </summary>
    public override bool IsValid()
    {
        string ValidationResult = Validate(txtName.Text, false);
        if (ValidationResult == string.Empty)
        {
            return true;
        }
        else
        {
            this.ValidationError = ValidationResult;
            return false;
        }
    }


    private string Validate(string value, bool allowPreselect)
    {
        if (!string.IsNullOrEmpty(value))
        {
            // If alternative form name contains macro or is not full name, it is always valid
            if (ContextResolver.ContainsMacro(value) || !ValidationHelper.IsFullName(value))
            {
                return string.Empty;
            }

            // Try to get alternative form object
            AlternativeFormInfo afi = AlternativeFormInfoProvider.GetAlternativeFormInfo(value);
            if (afi == null)
            {
                if (allowPreselect)
                {
                    // Alternative form does not exist
                    DataClassInfo di = DataClassInfoProvider.GetDataClass(value);
                    if ((di == null) && (value != string.Empty))
                    {
                        return GetString("altform.selectaltform.notexist").Replace("%%code%%", value);
                    }
                    else
                    {
                        return String.Empty;
                    }
                }
                else
                {
                    return GetString("altforms.selectaltform.formnotexist").Replace("%%code%%", value);
                }
            }
        }

        return string.Empty;
    }
}