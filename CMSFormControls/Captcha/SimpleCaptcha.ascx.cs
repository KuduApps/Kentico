using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.FormControls;
using CMS.GlobalHelper;

public partial class CMSFormControls_Captcha_SimpleCaptcha : FormEngineUserControl, ICaptchaControl
{
    #region "Variables"

    private bool mKeepCodeAutomatically = true;

    #endregion


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
            txtSecurityCode.Enabled = value;
        }
    }


    /// <summary>
    /// Indicates whether the info label should be displayed.
    /// </summary>
    public bool ShowInfoLabel
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("ShowInfoLabel"), false);
        }
        set
        {
            SetValue("ShowInfoLabel", value);
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return txtSecurityCode.Text;
        }
        set
        {
            txtSecurityCode.Text = (string)value;
        }
    }


    /// <summary>
    /// Width of the CAPTCHA image.
    /// </summary>
    public int ImageWidth
    {
        get
        {
            return ValidationHelper.GetInteger(GetValue("ImageWidth"), 80);
        }
        set
        {
            SetValue("ImageWidth", value);
        }
    }


    /// <summary>
    /// Height of the CAPTCHA image.
    /// </summary>
    public int ImageHeight
    {
        get
        {
            return ValidationHelper.GetInteger(GetValue("ImageHeight"), 20);
        }
        set
        {
            SetValue("ImageHeight", value);
        }
    }

    #endregion
    

    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Capta image url with anti cache query string parameter
        imgSecurityCode.ImageUrl = ResolveUrl(string.Format("~/CMSPages/Dialogs/CaptchaImage.aspx?hash={0}&captcha={1}&width={2}&height={3}", Guid.NewGuid(), ClientID, ImageWidth, ImageHeight));

        // Show info label
        if (ShowInfoLabel)
        {
            lblSecurityCode.AssociatedControlClientID = txtSecurityCode.ClientID;
            lblSecurityCode.Text = ResHelper.GetString("SecurityCode.lblSecurityCode");
            lblSecurityCode.Visible = true;
        }

        // Show after text
        plcAfterText.Visible = ShowAfterText;
        if (plcAfterText.Visible)
        {
            lblAfterText.Text = GetString("SecurityCode.Aftertext");
        }

        if (!RequestHelper.IsPostBack())
        {
            // Create a random code and store it in the Session object.
            if (KeepCodeAutomatically || (SessionHelper.GetValue("CaptchaImageText" + ClientID) == null))
            {
                WindowHelper.Add("CaptchaImageText" + ClientID, GenerateRandomCode());
            }
        }
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        // Regenerate security code if it is not valid after postback
        if (RequestHelper.IsPostBack() && !IsValid() && AlwaysGenerate)
        {
            GenerateNew();
        }
    }


    /// <summary>
    /// Returns true if user control is valid.
    /// </summary>
    public override bool IsValid()
    {
        bool isValid = false;
        if (WindowHelper.GetItem("CaptchaImageText" + ClientID) != null)
        {
            isValid = (txtSecurityCode.Text == Convert.ToString(WindowHelper.GetItem("CaptchaImageText" + ClientID)));
        }

        return isValid;
    }


    /// <summary>
    /// Returns a string of six random digits.
    /// <summary>
    private string GenerateRandomCode()
    {
        Random random = new Random(ClientID.GetHashCode() + (int)DateTime.Now.Ticks);

        string s = string.Empty;

        for (int i = 0; i < 6; i++)
        {
            s = String.Concat(s, random.Next(10).ToString());
        }

        return s;
    }

    #endregion


    #region "ICaptchaControl Members"

    /// <summary>
    /// Generates new code.
    /// </summary>
    public void GenerateNew()
    {
        txtSecurityCode.Text = string.Empty;
        WindowHelper.Add("CaptchaImageText" + ClientID, GenerateRandomCode());
    }


    /// <summary>
    /// Indicates if CAPTCHA image is always generated and stored to the session.
    /// </summary>
    public bool KeepCodeAutomatically
    {
        get
        {
            return mKeepCodeAutomatically;
        }
        set
        {
            mKeepCodeAutomatically = value;
        }
    }


    /// <summary>
    /// Indicates whether the after text should be displayed.
    /// </summary>
    public bool ShowAfterText
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("ShowAfterText"), false);
        }
        set
        {
            SetValue("ShowAfterText", value);
        }
    }


    /// <summary>
    /// Indicates whether new random number is generated after the wrong number was entered.
    /// </summary>
    public bool AlwaysGenerate
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("AlwaysGenerate"), true);
        }
        set
        {
            SetValue("AlwaysGenerate", value);
        }
    }

    #endregion
}