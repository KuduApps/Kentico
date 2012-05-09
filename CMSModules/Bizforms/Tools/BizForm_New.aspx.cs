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
using CMS.FormEngine;
using CMS.SettingsProvider;
using CMS.CMSHelper;
using CMS.LicenseProvider;
using CMS.UIControls;
using CMS.EventLog;

public partial class CMSModules_BizForms_Tools_BizForm_New : CMSBizFormPage
{
    private const string bizFormNamespace = "BizForm";
    private string mFormTablePrefix = null;


    /// <summary>
    /// Returns prefix for bizform table name.
    /// </summary>
    private string FormTablePrefix
    {
        get
        {
            if (string.IsNullOrEmpty(mFormTablePrefix))
            {
                mFormTablePrefix = String.Format("Form_{0}_", ValidationHelper.GetIdentifier(CMSContext.CurrentSiteName));
            }

            return mFormTablePrefix;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Check 'CreateForm' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.form", "CreateForm"))
        {
            RedirectToCMSDeskAccessDenied("cms.form", "CreateForm");
        }

        // Validator initializations
        rfvFormDisplayName.ErrorMessage = GetString("BizForm_Edit.ErrorEmptyFormDispalyName");
        rfvFormName.ErrorMessage = GetString("BizForm_Edit.ErrorEmptyFormName");
        rfvTableName.ErrorMessage = GetString("BizForm_Edit.ErrorEmptyFormTableName");

        // Control initializations
        lblFormDisplayName.Text = GetString("BizForm_Edit.FormDisplayNameLabel");
        lblFormName.Text = GetString("BizForm_Edit.FormNameLabel");
        lblTableName.Text = GetString("BizForm_Edit.TableNameLabel");
        lblPrefix.Text = FormTablePrefix + "&nbsp;";
        // Remove prefix lenght from maximum allowed length of table name
        txtTableName.MaxLength = 100 - FormTablePrefix.Length;
        btnOk.Text = GetString("General.OK");

        // Page title control initialization
        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = GetString("BizForm_Edit.ItemListLink");
        breadcrumbs[0, 1] = "~/CMSModules/BizForms/Tools/BizForm_List.aspx";
        breadcrumbs[0, 2] = "";
        breadcrumbs[1, 0] = GetString("BizForm_Edit.NewItemCaption");
        breadcrumbs[1, 1] = "";
        breadcrumbs[1, 2] = "";

        this.CurrentMaster.Title.TitleText = GetString("BizForm_New.HeaderCaption");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_Form/new.png");
        this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;
        this.CurrentMaster.Title.HelpTopicName = "new_bizform";
    }


    /// <summary>
    /// Sets data to database.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        DataClassInfo dci = null;
        BizFormInfo bizFormObj = null;

        string errorMessage = new Validator().NotEmpty(txtFormDisplayName.Text, rfvFormDisplayName.ErrorMessage).
                                               NotEmpty(txtFormName.Text, rfvFormName.ErrorMessage).
                                               NotEmpty(txtTableName.Text, rfvTableName.ErrorMessage).
                                               IsIdentificator(txtFormName.Text, GetString("BizForm_Edit.ErrorFormNameInIdentificatorFormat")).
                                               IsIdentificator(txtTableName.Text, GetString("BizForm_Edit.ErrorFormTableNameInIdentificatorFormat")).Result;
        if (String.IsNullOrEmpty(errorMessage))
        {
            string formCodeName = txtFormName.Text.Trim();

            // Form name must be unique
            BizFormInfo testObj = BizFormInfoProvider.GetBizFormInfo(formCodeName, CMSContext.CurrentSiteName);

            // If formName value is unique...
            if (testObj == null)
            {
                string primaryKey = formCodeName + "ID";
                string formDisplayName = txtFormDisplayName.Text.Trim();
                string className = bizFormNamespace + "." + formCodeName;
                // Table name is combined from prefix ('BizForm_<sitename>_') and custom table name
                string tableName = FormTablePrefix + txtTableName.Text.Trim();

                if (!BizFormInfoProvider.LicenseVersionCheck(URLHelper.GetCurrentDomain(), FeatureEnum.BizForms, VersionActionEnum.Insert))
                {
                    lblError.Visible = true;
                    lblError.Text = GetString("LicenseVersion.BizForm");
                }

                if (!lblError.Visible)
                {
                    try
                    {
                        // Create new table in DB
                        TableManager.CreateTable(tableName, primaryKey);
                    }
                    catch (Exception ex)
                    {
                        errorMessage = ex.Message;

                        // Table with the same name already exists
                        lblError.Visible = true;
                        lblError.Text = string.Format(GetString("bizform_edit.errortableexists"), tableName);
                    }

                    if (String.IsNullOrEmpty(errorMessage))
                    {
                        // Change table owner
                        try
                        {
                            string owner = SqlHelperClass.GetDBSchema(CMSContext.CurrentSiteName);
                            if ((!String.IsNullOrEmpty(owner)) && (owner.ToLower() != "dbo"))
                            {
                                TableManager.ChangeDBObjectOwner(tableName, owner);
                                tableName = owner + "." + tableName;
                            }
                        }
                        catch (Exception ex)
                        {
                            EventLogProvider.LogException("BIZFORM_NEW", "E", ex);
                        }

                        // Convert default datetime to string in english format
                        string defaultDateTime = DateTime.Now.ToString(CultureHelper.EnglishCulture.DateTimeFormat);

                        try
                        {
                            // Add FormInserted and FormUpdated columns to the table
                            TableManager.AddTableColumn(tableName, "FormInserted", "datetime", false, defaultDateTime);
                            TableManager.AddTableColumn(tableName, "FormUpdated", "datetime", false, defaultDateTime);
                        }
                        catch (Exception ex)
                        {
                            errorMessage = ex.Message;

                            // Column wasnt added successfuly
                            lblError.Visible = true;
                            lblError.Text = errorMessage;

                            // Delete created table
                            TableManager.DropTable(tableName);
                        }
                    }

                    if (String.IsNullOrEmpty(errorMessage))
                    {
                        dci = BizFormInfoProvider.CreateBizFormDataClass(className, formDisplayName, tableName, primaryKey);

                        try
                        {
                            // Create new bizform dataclass
                            using (CMSActionContext context = new CMSActionContext())
                            {
                                // Disable logging of tasks
                                context.DisableLogging();

                                DataClassInfoProvider.SetDataClass(dci);
                            }
                        }
                        catch (Exception ex)
                        {
                            errorMessage = ex.Message;

                            // Class with the same name already exists
                            lblError.Visible = true;
                            lblError.Text = errorMessage;

                            // Delete created table
                            TableManager.DropTable(tableName);
                        }
                    }

                    if (String.IsNullOrEmpty(errorMessage))
                    {
                        // Create new bizform
                        bizFormObj = new BizFormInfo();
                        bizFormObj.FormDisplayName = formDisplayName;
                        bizFormObj.FormName = formCodeName;
                        bizFormObj.FormClassID = dci.ClassID;
                        bizFormObj.FormSiteID = CMSContext.CurrentSiteID;
                        bizFormObj.FormEmailAttachUploadedDocs = true;
                        bizFormObj.FormItems = 0;
                        bizFormObj.FormClearAfterSave = false;
                        bizFormObj.FormLogActivity = true;

                        try
                        {
                            // Create new bizform
                            BizFormInfoProvider.SetBizFormInfo(bizFormObj);

                            // Generate basic queries
                            SqlGenerator.GenerateQuery(className, "selectall", SqlOperationTypeEnum.SelectAll, false);
                            SqlGenerator.GenerateQuery(className, "delete", SqlOperationTypeEnum.DeleteQuery, false);
                            SqlGenerator.GenerateQuery(className, "insert", SqlOperationTypeEnum.InsertQuery, true);
                            SqlGenerator.GenerateQuery(className, "update", SqlOperationTypeEnum.UpdateQuery, false);
                            SqlGenerator.GenerateQuery(className, "select", SqlOperationTypeEnum.SelectQuery, false);
                        }
                        catch (Exception ex)
                        {
                            errorMessage = ex.Message;

                            lblError.Visible = true;
                            lblError.Text = errorMessage;

                            // Delete created class (includes deleting its table)
                            if (dci != null)
                            {
                                using (CMSActionContext context = new CMSActionContext())
                                {
                                    // Disable logging of tasks
                                    context.DisableLogging();

                                    DataClassInfoProvider.DeleteDataClass(dci);
                                }
                            }
                        }

                        if (String.IsNullOrEmpty(errorMessage))
                        {
                            // Redirect to edit dialog
                            URLHelper.Redirect(string.Format("BizForm_Frameset.aspx?formId={0}&newform=1", bizFormObj.FormID));
                        }
                    }
                }
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = GetString("BizForm_Edit.FormNameExists");
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
        }
    }
}
