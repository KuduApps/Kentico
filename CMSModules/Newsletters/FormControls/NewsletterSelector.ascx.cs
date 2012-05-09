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

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Newsletter;
using CMS.UIControls;

public partial class CMSModules_Newsletters_FormControls_NewsletterSelector : CMS.FormControls.FormEngineUserControl
{
    #region "Variables"

    private string mValue = String.Empty;
    private string mResourcePrefix = String.Empty;

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
            this.EnsureChildControls();
            base.Enabled = value;
            this.usNewsletters.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            EnsureChildControls();
            return usNewsletters.Value;
        }
        set
        {
            this.EnsureChildControls();
            mValue = ValidationHelper.GetString(value, "");
            try
            {
                usNewsletters.Value = mValue;
            }
            catch
            {
            }
        }
    }


    /// <summary>
    /// Gets ClientID of the dropdownlist with newsletters.
    /// </summary>
    public override string ValueElementID
    {
        get
        {
            EnsureChildControls();
            return usNewsletters.ClientID;
        }
    }


    /// <summary>
    /// Uniselector mode.
    /// </summary>
    public UniSelector UniSelector
    {
        get
        {
            EnsureChildControls();
            return usNewsletters;
        }
    }


    /// <summary>
    /// Gets image dialog.
    /// </summary>
    public Image ImageDialog
    {
        get
        {
            EnsureChildControls();
            return usNewsletters.ImageDialog;
        }
    }


    /// <summary>
    /// Gets hyperlink dialog.
    /// </summary>
    public HyperLink LinkDialog
    {
        get
        {
            EnsureChildControls();
            return usNewsletters.LinkDialog;
        }
    }


    /// <summary>
    /// Gets or sets if site filter should be shown or not.
    /// </summary>
    public bool ShowSiteFilter
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("ShowSiteFilter"), true);
        }
        set
        {
            SetValue("ShowSiteFilter", value);
        }
    }


    /// <summary>
    /// Gets or sets the resource prefix of uni selector. If not set default values are used.
    /// </summary>
    public string ResourcePrefix
    {
        get
        {
            return mResourcePrefix;
        }
        set
        {
            mResourcePrefix = value;
            EnsureChildControls();
            usNewsletters.ResourcePrefix = value;
        }
    }


    /// <summary>
    /// Gets dialog button.
    /// </summary>
    public Button DialogButton
    {
        get
        {
            EnsureChildControls();
            return usNewsletters.ButtonDialog;
        }
    }

    #endregion


    #region "Methods"

    protected override void CreateChildControls()
    {
        if (usNewsletters == null)
        {
            pnlUpdate.LoadContainer();
        }
        base.CreateChildControls();
        if (!StopProcessing)
        {
            ReloadData();
        }
    }


    protected void ReloadData()
    {
        usNewsletters.WhereCondition = "NewsletterSiteID = " + CMSContext.CurrentSiteID;
        usNewsletters.ButtonRemoveSelected.CssClass = "XLongButton";
        usNewsletters.ButtonAddItems.CssClass = "XLongButton";
        usNewsletters.SpecialFields = new string[,] { { GetString("NewsletterSelect.LetUserChoose"), "NWSLetUserChoose" } };
        usNewsletters.ReturnColumnName = "NewsletterName";

        try
        {
            usNewsletters.Value = mValue;
        }
        catch
        {
        }
    }

    #endregion
}
