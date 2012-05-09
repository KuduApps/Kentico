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
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSSiteManager_Development_Cultures_Culture_New : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        rfvCultureName.ErrorMessage = GetString("Culture_New.ErrorEmptyCultureName");
        rfvCultureCode.ErrorMessage = GetString("Culture_New.ErrorEmptyCultureCode");
        rfvCultureShortName.ErrorMessage = GetString("Culture_New.ErrorEmptyCultureShortName");

        // Init breadcrumbs
        string cultures = GetString("Culture_New.Cultures");
        string currentCulture = GetString("Culture_New.CurrentCulture");
        string title = GetString("Culture_New.CurrentCulture");

        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = cultures;
        breadcrumbs[0, 1] = "~/CMSSiteManager/Development/Cultures/Culture_List.aspx";
        breadcrumbs[0, 2] = "";
        breadcrumbs[1, 0] = currentCulture;
        breadcrumbs[1, 1] = "";
        breadcrumbs[1, 2] = "";

        // Init master page properties (page title)
        CurrentMaster.Title.Breadcrumbs = breadcrumbs;
        CurrentMaster.Title.TitleText = title;
        CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_Culture/new.png");
        CurrentMaster.Title.HelpTopicName = "general_tabnew";
        CurrentMaster.Title.HelpName = "helpTopic";
    }


    /// <summary>
    /// Handles btnOK's OnClick event - Save culture info.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Validate the input
        string result = new Validator().NotEmpty(txtCultureName.Text.Trim(), rfvCultureName.ErrorMessage).NotEmpty(txtCultureCode.Text.Trim(), rfvCultureCode.ErrorMessage).Result;

        if (txtCultureCode.Text.Trim().Length > 10)
        {
            result = GetString("Culture.MaxLengthError");
        }

        // Validate the culture code
        try
        {
            // Check if global culture exists
            if (new System.Globalization.CultureInfo(txtCultureCode.Text.Trim()) == null)
            {
                result = GetString("Culture.ErrorNoGlobalCulture");
            }
        }
        catch
        {
            result = GetString("Culture.ErrorNoGlobalCulture");
        }

        txtCultureAlias.Text = URLHelper.GetSafeUrlPart(txtCultureAlias.Text.Trim(), String.Empty);
        string cultureAlias = txtCultureAlias.Text.Trim();
        
        // Check whether culture alias is unique
        if (!String.IsNullOrEmpty(cultureAlias))
        {
            CultureInfo ci = CultureInfoProvider.GetCultureInfoForCulture(cultureAlias);
            if ((ci != null) || (String.Compare(cultureAlias, txtCultureCode.Text.Trim(), true) == 0))
            {
                result = GetString("Culture.AliasNotUnique");
            }
        }

        if (result == "")
        {
            // Check if culture already exists
            CultureInfo ci = CultureInfoProvider.GetCultureInfoForCulture(txtCultureCode.Text.Trim());
            if (ci == null)
            {
                // Save culture info
                ci = new CultureInfo();
                ci.CultureName = txtCultureName.Text.Trim();
                ci.CultureCode = txtCultureCode.Text.Trim();
                ci.CultureShortName = txtCultureShortName.Text.Trim();
                ci.CultureAlias = cultureAlias;
                CultureInfoProvider.SetCultureInfo(ci);

                URLHelper.Redirect("Culture_Edit_Frameset.aspx?cultureID=" + ci.CultureID + "&saved=1");
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = GetString("Culture_New.CultureExists");
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = result;
        }
    }
}