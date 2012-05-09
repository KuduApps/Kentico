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

public partial class CMSModules_Ecommerce_Pages_Tools_Configuration_TaxClasses_TaxClass_General : CMSTaxClassesPage
{
    protected int taxclassid = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check permissions for CMS Desk -> Ecommerce
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Ecommerce", "Configuration.TaxClasses.General"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Ecommerce", "Configuration.TaxClasses.General");
        }

        rfvTaxClassDisplayName.ErrorMessage = GetString("general.requiresdisplayname");
        rfvTaxClassName.ErrorMessage = GetString("general.requirescodename");

        // Control initializations				
        lblTaxClassDisplayName.Text = GetString("TaxClass_Edit.TaxClassDisplayNameLabel");
        lblTaxClassName.Text = GetString("TaxClass_Edit.TaxClassNameLabel");
        lblTaxZero.Text = GetString("TaxClass_Edit.lblTaxZero");

        btnOk.Text = GetString("General.OK");

        // Get taxClass id from querystring		
        taxclassid = QueryHelper.GetInteger("taxclassid", 0);
        if (taxclassid > 0)
        {
            TaxClassInfo taxClassObj = TaxClassInfoProvider.GetTaxClassInfo(taxclassid);
            EditedObject = taxClassObj;

            if (taxClassObj != null)
            {
                CheckEditedObjectSiteID(taxClassObj.TaxClassSiteID);
                // Fill editing form
                if (!RequestHelper.IsPostBack())
                {
                    LoadData(taxClassObj);

                    // Show that the taxClass was created or updated successfully
                    if (QueryHelper.GetString("saved", "") == "1")
                    {
                        lblInfo.Visible = true;
                        lblInfo.Text = GetString("General.ChangesSaved");
                    }
                }
            }
            else
            {
                btnOk.Enabled = false;
                lblError.Visible = true;
                lblError.Text = GetString("TaxClass_Edit.TaxClassDoesNotExists");
                return;
            }

        }
    }


    /// <summary>
    /// Load data from edited taxClass object to form.
    /// </summary>
    /// <param name="taxClassObj">TaxClass object</param>
    protected void LoadData(TaxClassInfo taxClassObj)
    {
        txtTaxClassDisplayName.Text = taxClassObj.TaxClassDisplayName;
        txtTaxClassName.Text = taxClassObj.TaxClassName;
        chkTaxZero.Checked = taxClassObj.TaxClassZeroIfIDSupplied;
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
            // TaxClassName must be unique
            TaxClassInfo taxClassObj = TaxClassInfoProvider.GetTaxClassInfo(txtTaxClassName.Text.Trim(), SiteInfoProvider.GetSiteName(ConfiguredSiteID));

            // If taxClassName value is unique
            if ((taxClassObj == null) || (taxClassObj.TaxClassID == taxclassid))
            {
                // If taxClassName value is unique -> determine whether it is update or insert 
                if (taxClassObj == null)
                {
                    // Get TaxClassInfo object by primary key
                    taxClassObj = TaxClassInfoProvider.GetTaxClassInfo(taxclassid);
                }
                if (taxClassObj != null)
                {
                    taxClassObj.TaxClassDisplayName = txtTaxClassDisplayName.Text.Trim();
                    taxClassObj.TaxClassName = txtTaxClassName.Text.Trim();
                    taxClassObj.TaxClassZeroIfIDSupplied = chkTaxZero.Checked;

                    TaxClassInfoProvider.SetTaxClassInfo(taxClassObj);

                    lblInfo.Visible = true;
                    lblInfo.Text = GetString("General.ChangesSaved");
                }
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
