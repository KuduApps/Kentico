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
using CMS.CMSHelper;
using CMS.FormControls;
using CMS.LicenseProvider;

public partial class CMSFormControls_LicenseSelector : FormEngineUserControl
{
    private string mEditions = "";

    #region " Public properties"

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
            this.chckLicenses.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            // Get  actual selection
            mEditions = GetLicensesString();
            return mEditions;
        }
        set
        {
            mEditions = ValidationHelper.GetString(value, "");
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!StopProcessing && (chckLicenses.Items.Count == 0))
        {
            LoadData();
        }
    }


    protected void LoadData()
    {
        // For each edition
        foreach (ProductEditionEnum edition in Enum.GetValues(typeof(ProductEditionEnum)))
        {
            // Get edition infromation
            string editionChar = LicenseKeyInfoProvider.EditionToChar(edition);
            string editionName = Enum.GetName(typeof(ProductEditionEnum), edition);

            // Add item
            ListItem item = new ListItem(editionName, editionChar);
            item.Selected = mEditions.Contains(editionChar);
            chckLicenses.Items.Add(item);
        }
    }


    private string GetLicensesString()
    {
        string editions = "";
        foreach (ListItem item in chckLicenses.Items)
        {
            if (item.Selected)
            {
                editions += item.Value + ";";
            }
        }
        return editions;
    }


    public override bool IsValid()
    {
        return (ValidationHelper.GetString(this.Value, "") != "");
    }
}