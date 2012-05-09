using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

using CMS.FormEngine;
using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.CMSHelper;
using System.Text;

public partial class CMSModules_Membership_FormControls_Passwords_PasswordStrength : FormEngineUserControl
{
    #region "Variables"

    string mSiteName = null;
    int mPreferedLength = 12;
    int mPreferedNonAlphaNumChars = 2;
    string mClassPrefix = "PasswordStrength";
    bool mAllowEmpty = false;
    bool mShowValidationOnNewLine = false;
    string mValidationGroup = string.Empty;
    string mTextBoxClass = "TextBoxField";
    bool mUseStylesForStrenghtIndicator = true;
    private bool mShowStrengthIndicator = true;

    #endregion


    #region "Private properties"

    /// <summary>
    /// Returns current site name.
    /// </summary>
    private string SiteName
    {
        get
        {
            if (mSiteName == null)
            {
                mSiteName = CMSContext.CurrentSiteName;
            }

            return mSiteName;
        }
    }


    /// <summary>
    /// Returns whether password policy is used.
    /// </summary>
    private bool UsePasswordPolicy
    {
        get
        {
            return SettingsKeyProvider.GetBoolValue(SiteName + ".CMSUsePasswordPolicy");
        }
    }


    /// <summary>
    /// Returns password minimal length.
    /// </summary>
    private int MinLength
    {
        get
        {
            return SettingsKeyProvider.GetIntValue(SiteName + ".CMSPolicyMinimalLength");
        }
    }


    /// <summary>
    /// Returns number of non alpha numeric characters
    /// </summary>
    private int MinNonAlphaNumChars
    {
        get
        {
            return SettingsKeyProvider.GetIntValue(SiteName + ".CMSPolicyNumberOfNonAlphaNumChars");
        }
    }


    /// <summary>
    /// Returns password gegular expression.
    /// </summary>
    private string RegularExpression
    {
        get
        {
            return SettingsKeyProvider.GetStringValue(SiteName + ".CMSPolicyRegularExpression");
        }
    }

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the value that indicates whether inline styles should be used for strenght indicator
    /// </summary>
    public bool UseStylesForStrenghtIndicator
    {
        get
        {
            return mUseStylesForStrenghtIndicator;
        }
        set
        {
            mUseStylesForStrenghtIndicator = value;
        }
    }

    /// <summary>
    /// Gets or sets value of from control.
    /// </summary>
    public override object Value
    {
        get
        {
            return txtPassword.Text;
        }
        set
        {
            txtPassword.Text = ValidationHelper.GetString(value, string.Empty);
        }
    }


    /// <summary>
    /// Gets or sets prefered length.
    /// </summary>
    public int PreferedLength
    {
        get
        {
            return mPreferedLength;
        }
        set
        {
            mPreferedLength = value;
        }
    }


    /// <summary>
    /// Gets or sets prefered number of non alpha numeric characters.
    /// </summary>
    public int PreferedNonAlphaNumChars
    {
        get
        {
            return mPreferedNonAlphaNumChars;
        }
        set
        {
            mPreferedNonAlphaNumChars = value;
        }
    }


    /// <summary>
    /// Class prefix for labels.
    /// </summary>
    public string ClassPrefix
    {
        get
        {
            return mClassPrefix;
        }
        set
        {
            mClassPrefix = value;
        }
    }


    /// <summary>
    /// Gets or sets value of from control in string type.
    /// </summary>
    public override string Text
    {
        get
        {
            return txtPassword.Text;
        }
        set
        {
            txtPassword.Text = value;
        }
    }


    /// <summary>
    /// Gets or sets whether password could be empty.
    /// </summary>
    public bool AllowEmpty
    {
        get
        {
            return mAllowEmpty;
        }
        set
        {
            mAllowEmpty = value;
        }
    }


    /// <summary>
    /// Gets or sets whether validation control is shown under the control.
    /// </summary>
    public bool ShowValidationOnNewLine
    {
        get
        {
            return mShowValidationOnNewLine;
        }
        set
        {
            mShowValidationOnNewLine = value;
        }
    }


    /// <summary>
    /// Gets or sets validation group.
    /// </summary>
    public string ValidationGroup
    {
        get
        {
            return mValidationGroup;
        }
        set
        {
            mValidationGroup = value;
        }
    }


    /// <summary>
    /// Gets or sets class of textbox.
    /// </summary>
    public string TextBoxClass
    {
        get
        {
            return mTextBoxClass;
        }
        set
        {
            mTextBoxClass = value;
        }
    }


    /// <summary>
    /// Returns textbox attributes.
    /// </summary>
    public AttributeCollection TextBoxAttributes
    {
        get
        {
            return txtPassword.Attributes;
        }
    }


    /// <summary>
    /// Gets or sets maximal length of password. 
    /// </summary>
    public int MaxLength
    {
        get
        {
            return txtPassword.MaxLength;
        }
        set
        {
            txtPassword.MaxLength = value;
        }
    }


    /// <summary>
    /// Gets or sets HTML that is displayed next to password input and indicates password as required field.
    /// </summary>
    public string RequiredFieldMark
    {
        get
        {
            return this.lblRequiredFieldMark.Text;
        }
        set
        {
            this.lblRequiredFieldMark.Text = value;
        }
    }


    /// <summary>
    /// Gets or sets whether strength indicator is shown.
    /// </summary>
    public bool ShowStrengthIndicator
    {
        get
        {
            return mShowStrengthIndicator;
        }
        set
        {
            mShowStrengthIndicator = value;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Set class
        txtPassword.CssClass = TextBoxClass;

        if (ShowStrengthIndicator)
        {            
            string tooltipMessage = string.Empty;

            StringBuilder sb = new StringBuilder();
            if (UsePasswordPolicy)
            {
                sb.Append(GetString("passwordstrength.notacceptable"), ";", GetString("passwordstrength.weak"));
                tooltipMessage = string.Format(GetString("passwordstrength.hint"), MinLength, MinNonAlphaNumChars, PreferedLength, PreferedNonAlphaNumChars);
            }
            else
            {
                sb.Append(GetString("passwordstrength.weak"), ";", GetString("passwordstrength.weak"));
                tooltipMessage = string.Format(GetString("passwordstrength.recommend"), PreferedLength, PreferedNonAlphaNumChars);
            }

            // Register jQuery and registration of script which shows password strength        
            ScriptHelper.RegisterJQuery(this.Page);
            ScriptHelper.RegisterScriptFile(this.Page, "~/CMSScripts/membership.js");

            sb.Append(";", GetString("passwordstrength.acceptable"), ";", GetString("passwordstrength.average"), ";", GetString("passwordstrength.strong"), ";", GetString("passwordstrength.excellent"));

            string regex = "''";
            if (!string.IsNullOrEmpty(RegularExpression))
            {
                regex = "/" + RegularExpression + "/";
            }

            // Javascript for calling js function on keyup of textbox
            string txtVar = "txtSearch_" + txtPassword.ClientID;
            string script =
                txtVar + " = $j('#" + txtPassword.ClientID + @"');
        if (" + txtVar + @" ) {                    
           " + txtVar + @".keyup(function(event){
                ShowStrength('" + txtPassword.ClientID + "', '" + MinLength + "', '" + PreferedLength + "', '" + MinNonAlphaNumChars + "', '"
                                        + PreferedNonAlphaNumChars + "', " + regex + ", '" + lblEvaluation.ClientID + "', '" + sb.ToString() + "', '" + ClassPrefix + "', '" + UsePasswordPolicy + "', '" + pnlPasswIndicator.ClientID + "', '" + UseStylesForStrenghtIndicator + @"');                               
            });                   
        }";

            ScriptHelper.RegisterStartupScript(this, typeof(string), "PasswordStrength_" + txtPassword.ClientID, ScriptHelper.GetScript(script));

            if (UseStylesForStrenghtIndicator)
            {
                pnlPasswStrengthIndicator.Style.Add("height", "5px");
                pnlPasswStrengthIndicator.Style.Add("background-color", "#dddddd");

                pnlPasswIndicator.Style.Add("height", "5px");
            }

            ScriptHelper.RegisterTooltip(this.Page);
            ScriptHelper.AppendTooltip(lblPasswStregth, tooltipMessage, "help");
        }
        else
        {
            pnlPasswStrengthIndicator.Visible = false;
            lblEvaluation.Visible = false;
            lblPasswStregth.Visible = false;
        }

        // Set up required field validator
        if (AllowEmpty)
        {
            rfvPassword.Enabled = false;
        }
        else
        {
            rfvPassword.Text = GetString("general.requirespassword");
            rfvPassword.ValidationGroup = ValidationGroup;
            if (ShowValidationOnNewLine)
            {
                rfvPassword.Text += "<br />";
            }
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        this.lblRequiredFieldMark.Visible = !String.IsNullOrEmpty(this.lblRequiredFieldMark.Text);
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Returns whether 
    /// </summary>
    public override bool IsValid()
    {
        if (UsePasswordPolicy)
        {
            string password = txtPassword.Text;
            this.ValidationError = GetString("passwordpolicy.notaccetable");

            // Check minimal length
            if (password.Length < MinLength)
            {
                return false;
            }

            // Check number of non alphanum characters
            int counter = 0;
            foreach (char c in password)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    counter++;
                }
            }

            if (counter < MinNonAlphaNumChars)
            {
                return false;
            }

            // Check regular expression
            if (!string.IsNullOrEmpty(RegularExpression))
            {
                Regex regex = new Regex(RegularExpression);
                if (!regex.IsMatch(password))
                {
                    return false;
                }
            }
        }

        return true;
    }

    #endregion
}
