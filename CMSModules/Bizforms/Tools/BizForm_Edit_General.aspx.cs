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
using CMS.FormEngine;
using CMS.SettingsProvider;
using CMS.DataEngine;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.WebAnalytics;

public partial class CMSModules_BizForms_Tools_BizForm_Edit_General : CMSBizFormPage
{
    protected BizFormInfo bfi = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check 'ReadForm' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.form", "ReadForm"))
        {
            RedirectToCMSDeskAccessDenied("cms.form", "ReadForm");
        }

        // Get form id from url
        int formId = QueryHelper.GetInteger("formid", 0);
        // Get form object
        bfi = BizFormInfoProvider.GetBizFormInfo(formId);
        EditedObject = bfi;

        rfvDisplayName.Text = GetString("BizFormGeneral.rfvDisplayName");

        if ((!RequestHelper.IsPostBack()) && (bfi != null))
        {
            LoadData();
        }
    }


    /// <summary>
    /// Load data to fields.
    /// </summary>
    protected void LoadData()
    {
        txtDisplayName.Text = bfi.FormDisplayName;
        txtCodeName.Text = bfi.FormName;

        DataClassInfo mDci = DataClassInfoProvider.GetDataClass(bfi.FormClassID);
        txtTableName.Text = mDci.ClassTableName;

        txtButtonText.Text = bfi.FormSubmitButtonText;
        txtSubmitButtonImage.Text = bfi.FormSubmitButtonImage;

        txtDisplay.Enabled = false;
        txtRedirect.Enabled = false;

        // Initialize 'after submitting' behavior
        if (!string.IsNullOrEmpty(bfi.FormDisplayText))
        {
            txtDisplay.Text = bfi.FormDisplayText;
            radDisplay.Checked = true;
            txtDisplay.Enabled = true;
        }
        else
        {
            if (!string.IsNullOrEmpty(bfi.FormRedirectToUrl))
            {
                txtRedirect.Text = bfi.FormRedirectToUrl;
                radRedirect.Checked = true;
                txtRedirect.Enabled = true;
            }
            else
            {
                if (bfi.FormClearAfterSave)
                {
                    radClear.Checked = true;
                }
                else
                {
                    radContinue.Checked = true;
                }
            }
        }
    }


    /// <summary>
    /// Save data to Database.
    /// </summary>
    protected void SaveData()
    {
        // Check display name emptiness
        if (string.IsNullOrEmpty(txtDisplayName.Text))
        {
            lblError.Visible = true;
            lblError.Text = GetString("BizFormGeneral.rfvDisplayName");
            return;
        }

        bfi.FormDisplayName = txtDisplayName.Text;
        bfi.FormName = txtCodeName.Text;
        bfi.FormSubmitButtonText = txtButtonText.Text;

        bfi.FormSubmitButtonImage = null;
        if (!string.IsNullOrEmpty(txtSubmitButtonImage.Text.Trim()))
        {
            bfi.FormSubmitButtonImage = txtSubmitButtonImage.Text.Trim();
        }

        // Set 'after submitting' behavior...
        bfi.FormRedirectToUrl = null;
        bfi.FormDisplayText = null;

        // ... clear form
        bfi.FormClearAfterSave = radClear.Checked;

        // ... display text
        if (radDisplay.Checked)
        {
            if (!string.IsNullOrEmpty(txtDisplay.Text.Trim()))
            {
                bfi.FormDisplayText = txtDisplay.Text.Trim();
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = GetString("BizFormGeneral.DisplayText");
                return;
            }
        }
        else
        {
            txtDisplay.Text = string.Empty;
        }

        // ... redirect
        if (radRedirect.Checked)
        {
            bfi.FormRedirectToUrl = txtRedirect.Text.Trim();
        }
        else
        {
            txtRedirect.Text = string.Empty;
        }

        BizFormInfoProvider.SetBizFormInfo(bfi);

        lblInfo.Visible = true;
        lblInfo.Text = GetString("General.ChangesSaved");

        // Reload header if changes were saved
        ScriptHelper.RefreshTabHeader(Page, GetString("general.general"));
    }


    protected void btnOk_Click(object sender, EventArgs e)
    {
        // Check 'EditForm' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.form", "EditForm"))
        {
            RedirectToCMSDeskAccessDenied("cms.form", "EditForm");
        }

        SaveData();
    }


    protected void radDisplay_CheckedChanged(object sender, EventArgs e)
    {
        txtDisplay.Enabled = false;
        txtRedirect.Enabled = false;
        if (radDisplay.Checked)
        {
            txtDisplay.Enabled = true;
        }
    }


    protected void radRedirect_CheckedChanged(object sender, EventArgs e)
    {
        txtDisplay.Enabled = false;
        txtRedirect.Enabled = false;
        if (radRedirect.Checked)
        {
            txtRedirect.Enabled = true;
        }
    }


    protected void radClear_CheckedChanged(object sender, EventArgs e)
    {
        txtDisplay.Enabled = false;
        txtRedirect.Enabled = false;
    }
}
