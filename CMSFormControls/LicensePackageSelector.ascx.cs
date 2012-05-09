using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.FormControls;
using CMS.LicenseProvider;

public partial class CMSFormControls_LicensePackageSelector : FormEngineUserControl
{
    #region "Variables"

    private string mPackages = "";

    #endregion


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
            this.chckPackages.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            // Get actual selection
            mPackages = GetPackagesString();
            return mPackages;
        }
        set
        {
            mPackages = ValidationHelper.GetString(value, "");
        }
    }

    #endregion


    #region "Page methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!StopProcessing && (chckPackages.Items.Count == 0))
        {
            LoadData();
        }
    }


    protected void LoadData()
    {
        // For each package
        foreach (PackagesEnum package in Enum.GetValues(typeof(PackagesEnum)))
        {
            // Get package infromation
            string packageCode = PackagesEnumFunctions.FromEnum(package);
            string packageName = Enum.GetName(typeof(PackagesEnum), package);

            // Add item
            ListItem item = new ListItem(packageName, packageCode);
            item.Selected = mPackages.Contains(packageCode);
            chckPackages.Items.Add(item);
        }
    }

    #endregion


    #region "Private methods"

    private string GetPackagesString()
    {
        string packages = "";
        foreach (ListItem item in chckPackages.Items)
        {
            if (item.Selected)
            {
                packages += item.Value + ";";
            }
        }
        return packages;
    }

    #endregion

}
