using System;

using CMS.GlobalHelper;
using CMS.FormControls;

public partial class CMSFormControls_System_FieldSelector : FormEngineUserControl
{
    #region "Public properties"

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
            txtField.Enabled = value;
            btnClear.Enabled = value;
            btnSelect.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            if (hdnField.Value.Length > 400)
            {
                return hdnField.Value.Substring(0, 400);
            }
            else
            {
                return hdnField.Value;
            }
        }
        set
        {
            if (value != null)
            {
                string stringValue = ValidationHelper.GetString(value, string.Empty);
                string[] valueAndText = stringValue.Split('|');
                if (valueAndText.Length == 2)
                {
                    txtField.Text = valueAndText[1];
                }
                hdnField.Value = stringValue;
            }
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize dialog script
        ScriptHelper.RegisterDialogScript(Page);
        string url = "~/CMSFormControls/Selectors/FieldSelection.aspx";
        url = URLHelper.AddParameterToUrl(url, "cid", ClientID);
        url = URLHelper.AddParameterToUrl(url, "hash", QueryHelper.GetHash(url));
        btnSelect.OnClientClick = "modalDialog('" + ResolveUrl(url) + "','fieldSelector',430, 170); return false;";

        ScriptHelper.RegisterStartupScript(this, typeof(string), "selectFields_" + ClientID, ScriptHelper.GetScript(
@"function SetValue_" + ClientID + @"(value, text) {
        document.getElementById('" + txtField.ClientID + @"').value = text;
        document.getElementById('" + hdnField.ClientID + @"').value = value + '|' + text;
    }"));
        // Initialize clear button
        btnClear.OnClientClick = "document.getElementById('" + txtField.ClientID + "').value = ''; document.getElementById('" + hdnField.ClientID + "').value='';return false;";
        // Disable editing of textbox
        txtField.Attributes.Add("readonly", "readonly");
    }

    #endregion


    #region "Overriden methods"

    /// <summary>
    /// Returns true if user control is valid.
    /// </summary>
    public override bool IsValid()
    {
        return true;
    }

    #endregion
}
