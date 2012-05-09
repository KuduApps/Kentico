using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.SiteProvider;

public partial class CMSModules_FormControls_Pages_Development_New : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        rfvControlName.ErrorMessage = GetString("Development_FormUserControl_Edit.rfvCodeName");
        rfvControlDisplayName.ErrorMessage = GetString("Development_FormUserControl_Edit.rfvDisplayName");

        // Initialize breadcrumbs
        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = GetString("Development_FormUserControl_Edit.Controls");
        breadcrumbs[0, 1] = "~/CMSModules/FormControls/Pages/Development/List.aspx";
        breadcrumbs[0, 2] = "";
        breadcrumbs[1, 0] = GetString("Development_FormUserControl_Edit.New");
        breadcrumbs[1, 1] = "";
        breadcrumbs[1, 2] = "";

        // Initialize page
        this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;
        this.CurrentMaster.Title.TitleText = GetString("Development_FormUserControl_Edit.Title");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_FormControl/new.png");
        this.CurrentMaster.Title.HelpTopicName = "new_form_control";
        this.CurrentMaster.Title.HelpName = "helpTopic";

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
    /// Handles btnOK's OnClick event.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        FormUserControlInfo fi = null;

        // Finds whether required fields are not empty
        string result = new Validator().NotEmpty(txtControlName.Text, rfvControlName.ErrorMessage).NotEmpty(txtControlDisplayName, rfvControlDisplayName.ErrorMessage).Result;

        // Check input file validity
        if (String.IsNullOrEmpty(result))
        {
            if (!tbFileName.IsValid())
            {
                result = tbFileName.ValidationError;
            }
        }

        // Try to create new form control if everything is OK
        if (String.IsNullOrEmpty(result))
        {
            fi = new FormUserControlInfo();
            fi.UserControlDisplayName = txtControlDisplayName.Text.Trim();
            fi.UserControlCodeName = txtControlName.Text.Trim();
            fi.UserControlFileName = tbFileName.Value.ToString();
            fi.UserControlType = drpTypeSelector.ControlType;
            fi.UserControlForText = false;
            fi.UserControlForLongText = false;
            fi.UserControlForInteger = false;
            fi.UserControlForLongInteger = false;
            fi.UserControlForDecimal = false;
            fi.UserControlForDateTime = false;
            fi.UserControlForBoolean = false;
            fi.UserControlForFile = false;
            fi.UserControlForDocAttachments = false;
            fi.UserControlForGUID = false;
            fi.UserControlForVisibility = false;
            fi.UserControlShowInBizForms = false;
            fi.UserControlDefaultDataType = "Text";
            try
            {
                FormUserControlInfoProvider.SetFormUserControlInfo(fi);
            }
            catch (Exception ex)
            {
                lblError.Visible = true;
                lblError.Text = ex.Message.Replace("%%name%%", fi.UserControlCodeName);
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = result;
        }

        // If control was succesfully created then redirect to editing page
        if (String.IsNullOrEmpty(lblError.Text) && (fi != null))
        {
            URLHelper.Redirect("Frameset.aspx?controlId=" + Convert.ToString(fi.UserControlID));
        }
    }
}
