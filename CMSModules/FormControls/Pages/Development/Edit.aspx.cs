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
using CMS.ExtendedControls;
using CMS.SettingsProvider;

public partial class CMSModules_FormControls_Pages_Development_Edit : SiteManagerPage
{
    #region "Private variables"

    int controlId = 0;

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Get strings for controls on page
        pnlUseFor.GroupingText = GetString("development.formcontrols.usefor");
        pnlShowIn.GroupingText = GetString("development.formcontrols.showin");
        rfvCodeName.ErrorMessage = GetString("Development_FormUserControl_Edit.rfvCodeName");
        rfvDisplayName.ErrorMessage = GetString("Development_FormUserControl_Edit.rfvDisplayName");

        controlId = QueryHelper.GetInteger("controlid", 0);

        this.plcDevelopment.Visible = SettingsKeyProvider.DevelopmentMode;

        if (!RequestHelper.IsPostBack())
        {
            LoadData();
        }

        drpDataType_SelectedIndexChanged(sender, e);
        chkForBizForms_CheckedChanged(sender, e);

        // Show 'saved' info message
        if (QueryHelper.GetString("saved", String.Empty) != String.Empty)
        {
            lblInfo.Visible = true;
        }

        // Initialize file selector
        FileSystemDialogConfiguration config = new FileSystemDialogConfiguration();
        config.DefaultPath = "CMSFormControls";
        config.AllowedExtensions = "ascx";
        config.ShowFolders = false;
        tbFileName.DialogConfig = config;
        tbFileName.AllowEmptyValue = false;
        tbFileName.SelectedPathPrefix = "~/CMSFormControls/";
        tbFileName.ValidationError = GetString("Development_FormUserControl_Edit.rfvFileName");
    }


    /// <summary>
    /// Loads data of edited form user control.
    /// </summary>
    protected void LoadData()
    {
        // Load drop-down list with default data types
        if (drpDataType.Items.Count <= 0)
        {
            drpDataType.Items.Add(new ListItem("Text", "Text"));
            drpDataType.Items.Add(new ListItem("Long Text", "LongText"));
            drpDataType.Items.Add(new ListItem("Integer", "Integer"));
            drpDataType.Items.Add(new ListItem("Decimal", "Decimal"));
            drpDataType.Items.Add(new ListItem("DateTime", "DateTime"));
            drpDataType.Items.Add(new ListItem("Boolean", "Boolean"));
            drpDataType.Items.Add(new ListItem("File", "File"));
        }

        FormUserControlInfo fi = FormUserControlInfoProvider.GetFormUserControlInfo(controlId);
        EditedObject = fi;
        if (fi != null)
        {
            // Set properties
            tbCodeName.Text = fi.UserControlCodeName;
            tbDisplayName.Text = fi.UserControlDisplayName;
            tbFileName.Value = fi.UserControlFileName;
            drpTypeSelector.ControlType = fi.UserControlType;

            drpModule.Value = fi.UserControlResourceID;

            // Set types
            chkForBizForms.Checked = fi.UserControlShowInBizForms;
            chkForBoolean.Checked = fi.UserControlForBoolean;
            chkForDateTime.Checked = fi.UserControlForDateTime;
            chkForDecimal.Checked = fi.UserControlForDecimal;
            chkForFile.Checked = fi.UserControlForFile;
            chkForInteger.Checked = fi.UserControlForInteger;
            chkForLongInteger.Checked = fi.UserControlForLongInteger;
            chkForLongText.Checked = fi.UserControlForLongText;
            chkForText.Checked = fi.UserControlForText;
            chkForGuid.Checked = fi.UserControlForGUID;
            chkForVisibility.Checked = fi.UserControlForVisibility;
            chkForDocAtt.Checked = fi.UserControlForDocAttachments;

            // Set modules
            chkShowInDocumentTypes.Checked = fi.UserControlShowInDocumentTypes;
            chkShowInSystemTables.Checked = fi.UserControlShowInSystemTables;
            chkShowInControls.Checked = fi.UserControlShowInWebParts;
            chkShowInReports.Checked = fi.UserControlShowInReports;
            chkShowInCustomTables.Checked = fi.UserControlShowInCustomTables;

            tbColumnSize.Text = fi.UserControlDefaultDataTypeSize.ToString();
            drpDataType.SelectedValue = fi.UserControlDefaultDataType;
        }
    }

    #endregion


    #region "Event handlers"

    /// <summary>
    /// Handles btnOK's OnClick event - Update FormUserControl info.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Finds whether required fields are not empty
        string result = new Validator().NotEmpty(tbCodeName.Text, rfvCodeName.ErrorMessage).NotEmpty(tbDisplayName, rfvDisplayName.ErrorMessage)
            .IsCodeName(tbCodeName.Text, GetString("general.invalidcodename"))
            .Result;

        if (String.IsNullOrEmpty(result))
        {
            if (!tbFileName.IsValid())
            {
                result = tbFileName.ValidationError;
            }
        }

        if (String.IsNullOrEmpty(result))
        {
            // Get object
            FormUserControlInfo fi = FormUserControlInfoProvider.GetFormUserControlInfo(controlId);
            EditedObject = fi;
            if (fi != null)
            {
                // Set properties of current object
                fi.UserControlCodeName = tbCodeName.Text.Trim();
                fi.UserControlDefaultDataType = drpDataType.SelectedValue;

                // If BizForm is selected set ColumnSize property
                if (tbColumnSize.Visible && chkForBizForms.Checked)
                {
                    try
                    {
                        int columnSize = ValidationHelper.GetInteger(tbColumnSize.Text, 0);
                        if (columnSize <= 0)
                        {
                            lblErrorSize.Visible = true;
                            lblInfo.Visible = false;
                            return;
                        }
                        else
                        {
                            fi.UserControlDefaultDataTypeSize = columnSize;
                            lblErrorSize.Visible = false;
                        }
                    }
                    catch
                    {
                        // Value of ColumnSize is not integer
                        lblError.Visible = true;
                        lblInfo.Visible = false;
                        lblError.Text = GetString("Development_FormUserControl_Edit.NotInteger").Replace("%%value%%", tbColumnSize.Text);
                        return;
                    }
                }

                // Set other properties
                fi.UserControlDisplayName = tbDisplayName.Text.Trim();
                fi.UserControlFileName = tbFileName.Value.ToString();
                fi.UserControlType = drpTypeSelector.ControlType;

                fi.UserControlResourceID = ValidationHelper.GetInteger(drpModule.Value, 0);

                // Set types
                fi.UserControlForBoolean = chkForBoolean.Checked;
                fi.UserControlForDateTime = chkForDateTime.Checked;
                fi.UserControlForDecimal = chkForDecimal.Checked;
                fi.UserControlForFile = chkForFile.Checked;
                fi.UserControlForDocAttachments = chkForDocAtt.Checked;
                fi.UserControlForInteger = chkForInteger.Checked;
                fi.UserControlForLongInteger = chkForLongInteger.Checked;
                fi.UserControlForLongText = chkForLongText.Checked;
                fi.UserControlForText = chkForText.Checked;
                fi.UserControlShowInBizForms = chkForBizForms.Checked;
                fi.UserControlForGUID = chkForGuid.Checked;
                fi.UserControlForVisibility = chkForVisibility.Checked;

                // Set modules
                fi.UserControlShowInDocumentTypes = chkShowInDocumentTypes.Checked;
                fi.UserControlShowInSystemTables = chkShowInSystemTables.Checked;
                fi.UserControlShowInWebParts = chkShowInControls.Checked;
                fi.UserControlShowInReports = chkShowInReports.Checked;
                fi.UserControlShowInCustomTables = chkShowInCustomTables.Checked;

                try
                {
                    // Save changes to database
                    FormUserControlInfoProvider.SetFormUserControlInfo(fi);
                    lblInfo.Visible = true;

                    // Refresh header with display name
                    ScriptHelper.RefreshTabHeader(Page, null);
                }
                catch (Exception ex)
                {
                    lblInfo.Visible = false;
                    lblError.Visible = true;
                    lblError.Text = ex.Message.Replace("%%name%%", fi.UserControlCodeName);
                }
            }
            // Form control by specified ID not found
            else
            {
                lblInfo.Visible = false;
                lblError.Visible = true;
                lblError.Text = GetString("development_formusercontrol_edit.notfound");
            }
        }
        // Validation failed
        else
        {
            lblInfo.Visible = false;
            lblError.Visible = true;
            lblError.Text = result;
        }
    }


    /// <summary>
    /// Data type changed event handler.
    /// </summary>
    protected void drpDataType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblErrorSize.Visible = false;
        lblInfo.Visible = false;
        // Show or hide ColumnSize control
        if (drpDataType.SelectedValue == "Text")
        {
            lblColumnSize.Visible = true;
            tbColumnSize.Visible = true;
        }
        else
        {
            lblColumnSize.Visible = false;
            tbColumnSize.Visible = false;
            tbColumnSize.Text = string.Empty;
        }
    }


    /// <summary>
    /// Form control for bizforms checkbox changed.
    /// </summary>
    protected void chkForBizForms_CheckedChanged(object sender, EventArgs e)
    {
        lblErrorSize.Visible = false;
        lblInfo.Visible = false;
        
        // Enable or disable ColumnSize control
        if (chkForBizForms.Checked)
        {
            drpDataType.Enabled = true;
            lblColumnSize.Enabled = true;
            tbColumnSize.Enabled = true;
        }
        else
        {
            tbColumnSize.Enabled = false;
            drpDataType.Enabled = false;
        }
    }

    #endregion
}
