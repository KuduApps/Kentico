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
using CMS.Ecommerce;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Pages_Development_DocumentTypes_DocumentType_Edit_Ecommerce : CMSEcommercePage
{
    int docTypeId = 0;
    DataClassInfo dci = null;


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.RequireSite = false;
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        lblName.Text = GetString("DocType.Ecommerce.lblName");
        lblWeight.Text = GetString("DocType.Ecommerce.lblWeight");
        lblHeight.Text = GetString("DocType.Ecommerce.lblHeight");
        lblWidth.Text = GetString("DocType.Ecommerce.lblWidth");
        lblDepth.Text = GetString("DocType.Ecommerce.lblDepth");
        lblImage.Text = GetString("DocType.Ecommerce.lblImage");
        lblPrice.Text = GetString("DocType.Ecommerce.lblPrice");
        lblDescription.Text = GetString("DocType.Ecommerce.lblDescription");
        btnOk.Text = GetString("General.OK");
        lblTitle.Text = GetString("DocType.Ecommerce.lblTitle");
        chkGenerateSKU.Text = GetString("DocType.Ecommerce.lblGenerateSKU");
        lblDepartments.Text = GetString("DocType.Ecommerce.lblDepartments");

        docTypeId = QueryHelper.GetInteger("documenttypeid", 0);
        dci = DataClassInfoProvider.GetDataClass(docTypeId);

        if (!RequestHelper.IsPostBack())
        {
            FillDropDownLists();
            SelectMappings();

            if (dci != null)
            {
                // Create automatically
                chkGenerateSKU.Checked = dci.ClassCreateSKU;

                // Select specified department
                if (string.IsNullOrEmpty(dci.ClassSKUDefaultDepartmentName))
                {
                    departmentElem.DepartmentID = dci.ClassSKUDefaultDepartmentID;
                }
                else
                {
                    departmentElem.DepartmentName = dci.ClassSKUDefaultDepartmentName;
                }

                // Load default product type
                this.defaultProductType.Value = this.dci.ClassSKUDefaultProductType;
            }
        }
    }


    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Validate form
        string errorMessage = ValidateForm();

        if (errorMessage == "")
        {
            dci = DataClassInfoProvider.GetDataClass(docTypeId);
            if (dci != null)
            {
                //string mappings = null;
                string key = "";

                #region "Set mappings"

                key = "SKUImagePath";
                if (drpImage.SelectedIndex > 0)
                {
                    dci.SKUMappings[key] = drpImage.SelectedValue;
                }
                else if (dci.SKUMappings.Contains(key))
                {
                    dci.SKUMappings.Remove(key);
                }

                key = "SKUName";
                if (drpName.SelectedIndex > 0)
                {
                    dci.SKUMappings[key] = drpName.SelectedValue;
                }
                else if (dci.SKUMappings.Contains(key))
                {
                    dci.SKUMappings.Remove(key);
                }

                key = "SKUWeight";
                if (drpWeight.SelectedIndex > 0)
                {
                    dci.SKUMappings[key] = drpWeight.SelectedValue;
                }
                else if (dci.SKUMappings.Contains(key))
                {
                    dci.SKUMappings.Remove(key);
                }

                key = "SKUHeight";
                if (drpHeight.SelectedIndex > 0)
                {
                    dci.SKUMappings[key] = drpHeight.SelectedValue;
                }
                else if (dci.SKUMappings.Contains(key))
                {
                    dci.SKUMappings.Remove(key);
                }

                key = "SKUWidth";
                if (drpWidth.SelectedIndex > 0)
                {
                    dci.SKUMappings[key] = drpWidth.SelectedValue;
                }
                else if (dci.SKUMappings.Contains(key))
                {
                    dci.SKUMappings.Remove(key);
                }

                key = "SKUDepth";
                if (drpDepth.SelectedIndex > 0)
                {
                    dci.SKUMappings[key] = drpDepth.SelectedValue;
                }
                else if (dci.SKUMappings.Contains(key))
                {
                    dci.SKUMappings.Remove(key);
                }

                key = "SKUPrice";
                if (drpPrice.SelectedIndex > 0)
                {
                    dci.SKUMappings[key] = drpPrice.SelectedValue;
                }
                else if (dci.SKUMappings.Contains(key))
                {
                    dci.SKUMappings.Remove(key);
                }

                key = "SKUDescription";
                if (drpDescription.SelectedIndex > 0)
                {
                    dci.SKUMappings[key] = drpDescription.SelectedValue;
                }
                else if (dci.SKUMappings.Contains(key))
                {
                    dci.SKUMappings.Remove(key);
                }

                #endregion

                dci.ClassSKUDefaultDepartmentName = departmentElem.DepartmentName;
                dci.ClassCreateSKU = chkGenerateSKU.Checked;
                dci.ClassSKUDefaultProductType = (string)this.defaultProductType.Value;

                try
                {
                    // Save changed to database
                    DataClassInfoProvider.SetDataClass(dci);
                    lblInfo.Visible = true;
                    lblInfo.Text = GetString("General.ChangesSaved");
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                }
            }
        }

        // Show error message
        if (errorMessage != "")
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
        }
    }


    /// <summary>
    /// Validates form and returns error message when occures.
    /// </summary>
    protected string ValidateForm()
    {
        string errorMessage = "";

        if (chkGenerateSKU.Checked)
        {
            // Product name and Product price fields must be mapped
            if ((drpName.SelectedValue == "") || (drpPrice.SelectedValue == ""))
            {
                errorMessage = GetString("DocType.Ecommerce.MappingMissing");
            }
        }

        return errorMessage;
    }


    /// <summary>
    /// Selects specified mappings in dropdownlists.
    /// </summary>
    protected void SelectMappings()
    {
        if (dci != null)
        {
            drpName.SelectedValue = ValidationHelper.GetString(dci.SKUMappings["SKUName"], "0");
            drpImage.SelectedValue = ValidationHelper.GetString(dci.SKUMappings["SKUImagePath"], "0");
            drpWeight.SelectedValue = ValidationHelper.GetString(dci.SKUMappings["SKUWeight"], "0");
            drpHeight.SelectedValue = ValidationHelper.GetString(dci.SKUMappings["SKUHeight"], "0");
            drpWidth.SelectedValue = ValidationHelper.GetString(dci.SKUMappings["SKUWidth"], "0");
            drpDepth.SelectedValue = ValidationHelper.GetString(dci.SKUMappings["SKUDepth"], "0");
            drpPrice.SelectedValue = ValidationHelper.GetString(dci.SKUMappings["SKUPrice"], "0");
            drpDescription.SelectedValue = ValidationHelper.GetString(dci.SKUMappings["SKUDescription"], "0");
        }
    }


    /// <summary>
    /// Fills dropdownlists with data.
    /// </summary>
    protected void FillDropDownLists()
    {
        FormFieldInfo[] fields = null;

        if (dci != null)
        {
            FormInfo fi = FormHelper.GetFormInfo(dci.ClassName, false);
            fields = fi.GetFields(true, true);
        }

        // Insert '( none )' item into all dropdownlists
        string noneString = GetString("general.selectnone");
        drpImage.Items.Add(new ListItem(noneString, ""));
        drpHeight.Items.Add(new ListItem(noneString, ""));
        drpDepth.Items.Add(new ListItem(noneString, ""));
        drpWeight.Items.Add(new ListItem(noneString, ""));
        drpWidth.Items.Add(new ListItem(noneString, ""));
        drpName.Items.Add(new ListItem(noneString, ""));
        drpPrice.Items.Add(new ListItem(noneString, ""));
        drpDescription.Items.Add(new ListItem(noneString, ""));

        // Insert 'Document name' item into all dropdownlists                        
        string docNameValue = "DocumentName";
        drpImage.Items.Add(new ListItem(docNameValue, docNameValue));
        drpHeight.Items.Add(new ListItem(docNameValue, docNameValue));
        drpDepth.Items.Add(new ListItem(docNameValue, docNameValue));
        drpWeight.Items.Add(new ListItem(docNameValue, docNameValue));
        drpWidth.Items.Add(new ListItem(docNameValue, docNameValue));
        drpName.Items.Add(new ListItem(docNameValue, docNameValue));
        drpPrice.Items.Add(new ListItem(docNameValue, docNameValue));
        drpDescription.Items.Add(new ListItem(docNameValue, docNameValue));

        // Fill dropdownlists with data
        if (fields != null)
        {
            foreach (FormFieldInfo ffi in fields)
            {
                if (ffi.Name.ToLower() != "documentname")
                {
                    drpImage.Items.Add(new ListItem(ffi.Name, ffi.Name));
                    drpHeight.Items.Add(new ListItem(ffi.Name, ffi.Name));
                    drpDepth.Items.Add(new ListItem(ffi.Name, ffi.Name));
                    drpWeight.Items.Add(new ListItem(ffi.Name, ffi.Name));
                    drpWidth.Items.Add(new ListItem(ffi.Name, ffi.Name));
                    drpName.Items.Add(new ListItem(ffi.Name, ffi.Name));
                    drpPrice.Items.Add(new ListItem(ffi.Name, ffi.Name));
                    drpDescription.Items.Add(new ListItem(ffi.Name, ffi.Name));
                }
            }
        }
    }
}
