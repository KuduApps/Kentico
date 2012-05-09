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
using CMS.DataEngine;
using CMS.SiteProvider;
using CMS.Ecommerce;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Pages_Tools_Configuration_TaxClasses_TaxClass_New : CMSTaxClassesPage
{
    protected int taxclassid = 0;


    protected void Page_Load(object sender, EventArgs e)
    {
        rfvTaxClassDisplayName.ErrorMessage = GetString("general.requiresdisplayname");
        rfvTaxClassName.ErrorMessage = GetString("general.requirescodename");

        // control initializations				
        lblTaxClassDisplayName.Text = GetString("TaxClass_Edit.TaxClassDisplayNameLabel");
        lblTaxClassName.Text = GetString("TaxClass_Edit.TaxClassNameLabel");
        lblTaxZero.Text = GetString("TaxClass_Edit.lblTaxZero");

        btnOk.Text = GetString("General.OK");

        string currentTaxClass = GetString("TaxClass_Edit.NewItemCaption");

        // initializes page title control		
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = GetString("TaxClass_Edit.ItemListLink");
        pageTitleTabs[0, 1] = "~/CMSModules/Ecommerce/Pages/Tools/Configuration/TaxClasses/TaxClass_List.aspx?SiteId=" + SiteID;
        pageTitleTabs[0, 2] = "";
        pageTitleTabs[1, 0] = currentTaxClass;
        pageTitleTabs[1, 1] = "";
        pageTitleTabs[1, 2] = "";
        this.CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
        this.CurrentMaster.Title.TitleText = GetString("TaxClass_Edit.HeaderCaption_New");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Ecommerce_TaxClass/new.png");
        this.CurrentMaster.Title.HelpTopicName = "CMS_Ecommerce_Configuration_TaxClasses_General";
        this.CurrentMaster.Title.HelpName = "helpTopic";
    }


    /// <summary>
    /// Sets data to database.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        CheckConfigurationModification();

        string errorMessage = new Validator()
            .NotEmpty(txtTaxClassDisplayName.Text.Trim(), GetString("general.requiresdisplayname"))
            .NotEmpty(txtTaxClassName.Text.Trim(), GetString("general.requirescodename")).Result;

        if (!ValidationHelper.IsCodeName(txtTaxClassName.Text.Trim()))
        {
            errorMessage = GetString("General.ErrorCodeNameInIdentificatorFormat");
        }

        if (errorMessage == "")
        {
            TaxClassInfo taxClassObj = TaxClassInfoProvider.GetTaxClassInfo(txtTaxClassName.Text.Trim(), SiteInfoProvider.GetSiteName(ConfiguredSiteID));

            if (taxClassObj == null)
            {
                // If taxClassName value is unique for configured site
                taxClassObj = new TaxClassInfo();

                taxClassObj.TaxClassDisplayName = txtTaxClassDisplayName.Text.Trim();
                taxClassObj.TaxClassName = txtTaxClassName.Text.Trim();
                taxClassObj.TaxClassZeroIfIDSupplied = chkTaxZero.Checked;
                taxClassObj.TaxClassSiteID = ConfiguredSiteID;

                TaxClassInfoProvider.SetTaxClassInfo(taxClassObj);

                URLHelper.Redirect("TaxClass_Frameset.aspx?taxclassid=" + Convert.ToString(taxClassObj.TaxClassID) + "&saved=1&siteId="+SiteID);
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = GetString("TaxClass_Edit.TaxClassNameExists");
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
        }
    }
}
