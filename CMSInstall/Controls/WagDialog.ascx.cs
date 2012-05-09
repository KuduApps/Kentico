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
using CMS.SiteProvider;
using CMS.LicenseProvider;
using CMS.IDataConnectionLibrary;
using CMS.SettingsProvider;

public partial class CMSInstall_Controls_WagDialog : CMSUserControl
{
    private string strPrefix = "Install.wag.";

    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        imgTrack.ImageUrl = "http://www.kentico.com/CMSPages/Track.aspx?c=wwag&siteurl=" + URLHelper.GetAbsoluteUrl(ResolveUrl("~"));
    }


    /// <summary>
    /// Creates requested license keys. Returns false if something fail.
    /// </summary>
    /// <param name="connectionString">Connection string</param>
    public bool ProcessRegistration(string connectionString)
    {
        string result = "";
        string lickey = "";

        // Delete all existing license keys
        DataSet ds = LicenseKeyInfoProvider.GetAllLicenseKeys();
        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                LicenseKeyInfo lki = new LicenseKeyInfo(dr);
                if (lki != null)
                {
                    LicenseKeyInfoProvider.DeleteLicenseKeyInfo(lki.LicenseKeyID);
                }
            }
        }

        // Create license keys for localhost and 127.0.0.1
        LicenseKeyInfo flki = new LicenseKeyInfo();
        flki.LoadLicense("DOMAIN:localhost\nPRODUCT:CF06\nEXPIRATION:00000000\nPACKAGES:\nSERVERS:1\n" +
            "DH0g3D1mDvNiMgqujyU5TspX3oB3TlsFUgSgR2p3MHe3VY+xgQvsn1kMRA6w+ZOLwLFbjjziznLX73V2DAzBXBBxQH1sSP6pnvua1qYyPXN5Mb3v9bd53nT1wgwiPvJSGLEKzsV/sf0RgtsrcBJTlGNSOAUG9qnkrSQlrLRkSdUzfZl4HdAidy53yZB4ydmGstxDOZWjCZvl7pOSL+PrOsYv5QbXz3eC4/dQJDNKG+lJkeuq/wXyMmhz/jj+JkSJQnvr9DIuEiYAqp1j4YNRVvGPv7FQqfPIixwQPXn73K7dBGwzUmmGzFUSXR0gLPpdG4nD5eoTOp1qaeKo4qy68A==", "localhost");
        LicenseKeyInfoProvider.SetLicenseKeyInfo(flki);
        flki = new LicenseKeyInfo();
        flki.LoadLicense("DOMAIN:127.0.0.1\nPRODUCT:CF06\nEXPIRATION:00000000\nPACKAGES:\nSERVERS:1\n" +
            "Mo0XU+buTy0eaW57psUxemEkBfo5pNAzGupE05V+LZmfykV75XnfFxX4Ac5O6kD4QQwDBKMBYf4qNYxOMI6JAXe0j3Qv8W9FJ6TG7TLD4ga/Ru9OOapXH6R86wepeLSdG0PKDL96JR4QNsUz1fZbRodxdOhvIEy8FvpF1JHUj04rc8DVFCJvkcrvmbfd+ZlfjGxo7GnfMOXE4FRlf3joK13jxiJypiGe4Tu71LiQgRlFFIWfXTm/WKgMT+wpQnIm/lUnWug5g0N2CkcEZ/dNfCOGBUqU4ImPjWGKdyfcYyS+F1FgvPI2sdzxIUnLtYu834fqgoEUzBsIuME3tNr8UQ==", "127.0.0.1");
        LicenseKeyInfoProvider.SetLicenseKeyInfo(flki);

        // Create free license keys for user defined domain
        string domainName = URLHelper.CorrectDomainName(txtUserDomain.Text.Trim());
        string firstName = txtUserFirstName.Text.Trim();
        string lastName = txtUserLastName.Text.Trim();
        string email = txtUserEmail.Text.Trim();

        bool userForm = !String.IsNullOrEmpty(firstName) || !String.IsNullOrEmpty(lastName) ||
            !String.IsNullOrEmpty(email) || !String.IsNullOrEmpty(txtPassword.Text);

        // Ignore localhost/127.0.0.1 licenses
        if ((String.Compare(domainName, "localhost", true) == 0) || (String.Compare(domainName, "127.0.0.1") == 0))
        {
            domainName = "";
        }

        // Do not modify anything if form is blank
        if (!String.IsNullOrEmpty(domainName) || userForm)
        {
            if (userForm)
            {
                result = new Validator().NotEmpty(firstName, ResHelper.GetString(strPrefix + "firstnamerequired"))
                    .NotEmpty(lastName, ResHelper.GetString(strPrefix + "lastnamerequired"))
                    .NotEmpty(email, ResHelper.GetString(strPrefix + "emailrequired"))
                    .IsEmail(email, ResHelper.GetString(strPrefix + "emailinvalidformat"))
                    .Result;

                if (String.IsNullOrEmpty(result) && plcPass.Visible && String.IsNullOrEmpty(txtPassword.Text))
                {
                    result = ResHelper.GetString(strPrefix + "passwordrequired");
                }
            }

            if (String.IsNullOrEmpty(result))
            {
                LS.CMSLicenseService ls = new LS.CMSLicenseService();
                if (userForm)
                {
                    if (ls.UserExists(email))
                    {
                        // If user with specified name (e-mail) already exist ask for her password
                        if (!plcPass.Visible)
                        {
                            plcPass.Visible = true;
                            result = ResHelper.GetString(strPrefix + "passwordrequired");
                        }
                        else
                        {
                            lickey = ls.GetFreeEditionKeyGeneral(domainName, firstName, lastName, email, txtPassword.Text, 6);
                        }
                    }
                    else
                    {
                        // Register new user and get license key
                        lickey = ls.GetFreeEditionKeyGeneral(domainName, firstName, lastName, email, UserInfoProvider.GenerateNewPassword(), 6);
                    }
                }
                else
                {
                    lickey = ls.GetFreeEditionKeyGeneral(domainName, null, null, null, null, 6);
                }

                if (String.IsNullOrEmpty(result) && !lickey.StartsWith("DOMAIN"))
                {
                    result = lickey;
                }
            }

            if (String.IsNullOrEmpty(result))
            {
                if (!String.IsNullOrEmpty(lickey))
                {
                    LicenseKeyInfo lki = new LicenseKeyInfo();
                    lki.LoadLicense(lickey, "");
                    LicenseKeyInfoProvider.SetLicenseKeyInfo(lki);
                }
            }
        }

        if (!String.IsNullOrEmpty(result))
        {
            lblError.Visible = true;
            lblError.Text = result;
        }

        return String.IsNullOrEmpty(result);
    }

    #endregion
}
