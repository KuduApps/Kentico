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
using CMS.LicenseProvider;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_Licenses_Pages_License_New : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Setup page title text and image
        this.CurrentMaster.Title.TitleText = GetString("Licenses_License_New.Title");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_LicenseKey/new.png");

        this.CurrentMaster.Title.HelpTopicName = "new_license";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        lbLicenseKey.Text = GetString("Licenses_License_New.Key");
        rfvLicenseKey.ErrorMessage = GetString("Licenses_License_New.KeyError");
        btnOk.Text = GetString("general.ok");

        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = GetString("Licenses_License_New.Licenses");
        pageTitleTabs[0, 1] = "~/CMSModules/Licenses/Pages/License_List.aspx";
        pageTitleTabs[1, 0] = GetString("licenses_license_new.new");
        pageTitleTabs[1, 1] = "";
        this.CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
    }


    protected void btnOK_Click(object sender, EventArgs e)
    {
        lblError.Text = "";

        try
        {
            LicenseKeyInfo lk = new LicenseKeyInfo();
            lk.LoadLicense(tbLicenseKey.Text.Trim(), "");

            if (lk != null)
            {
                switch (lk.ValidationResult)
                {
                    case LicenseValidationEnum.Expired:
                        lblError.Text = GetString("Licenses_License_New.LicenseNotValid.Expired");
                        break;

                    case LicenseValidationEnum.Invalid:
                        lblError.Text = GetString("Licenses_License_New.LicenseNotValid.Invalid");
                        break;

                    case LicenseValidationEnum.NotAvailable:
                        lblError.Text = GetString("Licenses_License_New.LicenseNotValid.NotAvailable");
                        break;

                    case LicenseValidationEnum.WrongFormat:
                        lblError.Text = GetString("Licenses_License_New.LicenseNotValid.WrongFormat");
                        break;

                    case LicenseValidationEnum.Valid:
                        if (LicenseKeyInfoProvider.IsLicenseExistForDomain(lk))
                        {
                            // License for domain already exist
                            lblInfo.Visible = false;
                            lblError.Visible = true;
                            lblError.Text = GetString("Licenses_License_New.DomainAlreadyExists").Replace("%%name%%", lk.Domain);
                        }
                        else
                        {
                            // Insert license
                            LicenseKeyInfoProvider.SetLicenseKeyInfo(lk);
                            CMS.SiteProvider.UserInfoProvider.ClearLicenseValues();
                            Functions.ClearHashtables();
                            URLHelper.Redirect("License_List.aspx");
                        }                        
                        break;
                }
            }

            if (lblError.Text != "")
            {
                lblInfo.Visible = false;
                lblError.Visible = true;
            }
        }
        catch (Exception ex)
        {
            lblInfo.Visible = false;
            lblError.Visible = true;
            lblError.Text = ex.Message;
        }
    }
}
