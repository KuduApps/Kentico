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
using CMS.UIControls;

public partial class CMSAdminControls_UI_NameForm : CMSUserControl
{
    /// <summary>
    /// Gets or sets display name input box.
    /// </summary>
    public string DisplayName
    {
        get
        {
            return txtDisplayName.Text.Trim();
        }

        set
        {
            txtDisplayName.Text = value;
        }
    }


    /// <summary>
    /// Gets or sets code name input box.
    /// </summary>
    public string CodeName
    {
        get
        {
            return txtCodeName.Text.Trim();
        }
        set
        {
            txtCodeName.Text = value;
        }
    }


    /// <summary>
    /// Shows/hides submit button.
    /// </summary>
    public bool ShowSubmitButton
    {
        get
        {
            return btnOk.Visible;
        }
        set
        {
            btnOk.Visible = value;
        }
    }


    /// <summary>
    /// Gets or sets submit button text.
    /// </summary>
    public string ButtonText
    {
        get
        {
            return btnOk.Text;
        }
        set
        {
            btnOk.Text = value;
        }
    }


    /// <summary>
    /// Gets or sets error message to display name validator.
    /// </summary>
    public string DisplayNameRequired
    {
        get
        {
            return rfvDisplayName.ErrorMessage;
        }
        set
        {
            rfvDisplayName.ErrorMessage = value;
        }
    }


    /// <summary>
    /// Gets or sets error message to code name validator.
    /// </summary>
    public string CodeNameRequired
    {
        get
        {
            return rfvCodeName.ErrorMessage;
        }
        set
        {
            rfvCodeName.ErrorMessage = value;
        }
    }


    /// <summary>
    /// Event which raises when submit button is clicked.
    /// </summary>
    public event EventHandler Click;


    protected void Page_Load(object sender, EventArgs e)
    {
        lblDisplayName.Text = GetString("general.displayname") + ResHelper.Colon;
        lblCodeName.Text = GetString("general.codename") + ResHelper.Colon;

        if (btnOk.Visible)
        {
            if (btnOk.Text == String.Empty)
            {
                btnOk.Text = GetString("general.ok");
            }
            btnOk.Click += new EventHandler(btnOK_Click);
        }

        if (rfvCodeName.ErrorMessage.Trim() == String.Empty)
        {
            rfvCodeName.ErrorMessage = GetString("general.requirescodename");
        }
        if (rfvDisplayName.ErrorMessage.Trim() == String.Empty)
        {
            rfvDisplayName.ErrorMessage = GetString("general.requiresdisplayname");
        }
    }


    void btnOK_Click(object sender, EventArgs e)
    {
        if (Click != null)
        {
            Click(sender, e);
        }
    }
}
