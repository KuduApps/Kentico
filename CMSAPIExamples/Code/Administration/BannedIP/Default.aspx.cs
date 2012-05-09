using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.LicenseProvider;
using CMS.SettingsProvider;


[Title(Text = "Banned IPs", ImageUrl = "Objects/CMS_BannedIP/object.png")]
public partial class CMSAPIExamples_Code_Administration_BannedIP_Default : CMSAPIExamplePage
{
    #region "Initialization"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check license
        LicenseHelper.CheckFeatureAndRedirect(URLHelper.GetCurrentDomain(), FeatureEnum.BannedIP);

        // Banned ip
        this.apiCreateBannedIp.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateBannedIp);
        this.apiGetAndUpdateBannedIp.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateBannedIp);
        this.apiGetAndBulkUpdateBannedIps.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateBannedIps);
        this.apiDeleteBannedIp.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteBannedIp);
        this.apiCheckBannedIp.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CheckBannedIp);
    }

    #endregion


    #region "Mass actions"

    /// <summary>
    /// Runs all creating and managing examples.
    /// </summary>
    public override void RunAll()
    {
        base.RunAll();

        // Banned ip
        this.apiCreateBannedIp.Run();
        this.apiGetAndUpdateBannedIp.Run();
        this.apiGetAndBulkUpdateBannedIps.Run();
        this.apiCheckBannedIp.Run();
    }


    /// <summary>
    /// Runs all cleanup examples.
    /// </summary>
    public override void CleanUpAll()
    {
        base.CleanUpAll();

        // Banned ip
        this.apiDeleteBannedIp.Run();
    }

    #endregion


    #region "API examples - Banned IP"

    /// <summary>
    /// Creates banned ip. Called when the "Create ip" button is pressed.
    /// </summary>
    private bool CreateBannedIp()
    {
        // Create new banned ip object
        BannedIPInfo newIp = new BannedIPInfo();

        // Set the properties
        newIp.IPAddress = "MyNewIp";
        newIp.IPAddressBanReason = "Ban reason";
        newIp.IPAddressAllowed = true;
        newIp.IPAddressAllowOverride = true;
        newIp.IPAddressBanType = BannedIPInfoProvider.BanControlEnumString(BanControlEnum.AllNonComplete);
        newIp.IPAddressBanEnabled = true;

        // Save the banned IP
        BannedIPInfoProvider.SetBannedIPInfo(newIp);

        return true;
    }


    /// <summary>
    /// Gets and updates banned IP. Called when the "Get and update IP" button is pressed.
    /// Expects the CreateBannedIp method to be run first.
    /// </summary>
    private bool GetAndUpdateBannedIp()
    {

        string where = "IPAddress LIKE N'MyNewIp%'";

        DataSet ips = BannedIPInfoProvider.GetBannedIPs(where, null);

        if (!DataHelper.DataSourceIsEmpty(ips))
        {
            // Create object from DataRow
            BannedIPInfo modifyIp = new BannedIPInfo(ips.Tables[0].Rows[0]);

            // Update the properties
            modifyIp.IPAddress = modifyIp.IPAddress.ToLower();

            // Save the changes
            BannedIPInfoProvider.SetBannedIPInfo(modifyIp);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates banned IPs. Called when the "Get and bulk update ips" button is pressed.
    /// Expects the CreateBannedIp method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateBannedIps()
    {
        // Prepare the parameters
        string where = "IPAddress LIKE N'MyNewIp%'";

        // Get the data
        DataSet ips = BannedIPInfoProvider.GetBannedIPs(where, null);
        if (!DataHelper.DataSourceIsEmpty(ips))
        {
            // Loop through the individual items
            foreach (DataRow ipDr in ips.Tables[0].Rows)
            {
                // Create object from DataRow
                BannedIPInfo modifyIp = new BannedIPInfo(ipDr);

                // Update the properties
                modifyIp.IPAddress = modifyIp.IPAddress.ToUpper();

                // Save the changes
                BannedIPInfoProvider.SetBannedIPInfo(modifyIp);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes banned ip. Called when the "Delete ip" button is pressed.
    /// Expects the CreateBannedIp method to be run first.
    /// </summary>
    private bool DeleteBannedIp()
    {
        string where = "IPAddress LIKE N'MyNewIp%'";

        // Get DataSet
        DataSet ips = BannedIPInfoProvider.GetBannedIPs(where, null);

        if (!DataHelper.DataSourceIsEmpty(ips))
        {
            // Get the first banned ip
            BannedIPInfo deleteIp = new BannedIPInfo(ips.Tables[0].Rows[0]);

            // Delete the banned ip
            BannedIPInfoProvider.DeleteBannedIPInfo(deleteIp);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Checks banned ip if current action is allowed. Called when the "Check banned IP for action" button is pressed.
    /// Expects the CreateBannedIp method to be run first.
    /// </summary>
    private bool CheckBannedIp()
    {
        string where = "IPAddress LIKE N'MyNewIp%'";

        // Get DataSet
        DataSet ips = BannedIPInfoProvider.GetBannedIPs(where, null);

        if (!DataHelper.DataSourceIsEmpty(ips))
        {
            // Get the first banned ip
            BannedIPInfo checkIp = new BannedIPInfo(ips.Tables[0].Rows[0]);

            if (!BannedIPInfoProvider.IsAllowed(checkIp.IPAddress, CMSContext.CurrentSiteName, BanControlEnum.AllNonComplete))
            {
                return true;
            }
        }

        return false;
    }

    #endregion
}
