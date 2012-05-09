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
using CMS.LicenseProvider;
using CMS.UIControls;

public partial class CMSMessages_invalidlicensekey : MessagePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.titleElem.TitleText = GetString("InvalidLicense.Header");
        this.titleElem.TitleImage = GetImageUrl("Others/Messages/denied.png");

        this.lblRawUrl.Text = GetString("InvalidLicense.RawUrl");
        this.lblResult.Text = GetString("InvalidLicense.Result");

        // URL
        string rawUrl = QueryHelper.GetString("rawUrl", String.Empty).Trim();
        if (rawUrl != String.Empty)
        {
            lblRawUrlValue.Text = HttpUtility.HtmlEncode(rawUrl);
        }
        else
        {
            lblRawUrl.Visible = false;
            lblRawUrlValue.Visible = false;
        }

        // Validation result
        LicenseValidationEnum mResult = (LicenseValidationEnum)QueryHelper.GetInteger("Result", Convert.ToInt32(LicenseValidationEnum.NotAvailable));
        string mStringResult = String.Empty;
        switch (mResult)
        {
            case LicenseValidationEnum.Expired:
                mStringResult = "KeyExpired";
                break;

            case LicenseValidationEnum.Invalid:
                mStringResult = "InvalidKey";
                break;

            case LicenseValidationEnum.Valid:
                mStringResult = "ValidKey";
                break;

            case LicenseValidationEnum.WrongFormat:
                mStringResult = "WrongFormat";
                break;

            case LicenseValidationEnum.Unknown:
                mStringResult = "Unknown";
                break;

            default: 
                mStringResult = "NotAvailable";
                break;
        }

        lblResultValue.Text = GetString("invalidlicense." + mStringResult);

        // URL 'Go to:'
        lnkGoToValue.ToolTip = URLHelper.GetAbsoluteUrl(URLHelper.WebApplicationVirtualPath) + "CMSSiteManager";
        lnkGoToValue.NavigateUrl = URLHelper.GetAbsoluteUrl(URLHelper.WebApplicationVirtualPath) + "CMSSiteManager/default.aspx?section=licenses";
        lblAddLicenseValue.Text = URLHelper.GetCurrentDomain();
    }
}
