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
using CMS.SettingsProvider;
using CMS.PortalEngine;
using CMS.DataEngine;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.IO;
using CMS.FormEngine;
using CMS.EventLog;

public partial class CMSModules_DocumentTypes_Pages_Development_DocumentType_Edit_General : SiteManagerPage
{
    #region "Variables"

    private int documentTypeId = 0;
    private DataClassInfo classInfo = null;
    string className = null;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        // Gets classID from querystring        
        documentTypeId = QueryHelper.GetInteger("documenttypeid", 0);

        // Initializes labels
        lblFullCodeName.Text = GetString("DocumentType_Edit_General.FullCodeName");
        lblCodeName.Text = GetString("DocumentType_Edit_General.CodeName");
        lblNamespaceName.Text = GetString("DocumentType_Edit_General.Namespace");
        lblDisplayName.Text = GetString("DocumentType_Edit_General.DisplayName");
        lblEditingPage.Text = GetString("DocumentType_Edit_General.EditingPage");
        lblNewPage.Text = GetString("DocumentType_Edit_General.NewPage");
        lblListPage.Text = GetString("DocumentType_Edit_General.ListPage");
        lblTemplateSelection.Text = GetString("DocumentType_Edit_General.TemplateSelection");
        lblViewPage.Text = GetString("DocumentType_Edit_General.ViewPage");
        lblPreviewPage.Text = GetString("DocumentType_Edit_General.PreviewPage");
        lblClassUsePublishFromTo.Text = GetString("DocumentType_Edit_General.UsePublishFromTo");
        lblIsMenuItem.Text = GetString("DocumentType_Edit_General.IsMenuItem");
        lblDefaultTemplate.Text = GetString("DocumentType_Edit_General.DefaultTemplate");
        lblIsProduct.Text = GetString("DocumentType_Edit_General.IsProduct");
        lblTableName.Text = GetString("class.TableName");
        
        // Initializes validators
        RequiredFieldValidatorDisplayName.ErrorMessage = GetString("DocumentType_Edit_General.DisplayNameRequired");
        RequiredFieldValidatorNamespaceName.ErrorMessage = GetString("DocumentType_Edit_General.NamespaceNameRequired");
        RequiredFieldValidatorCodeName.ErrorMessage = GetString("DocumentType_Edit_General.CodeNameRequired");
        RegularExpressionValidatorNameSpaceName.ErrorMessage = GetString("DocumentType_Edit_General.NamespaceNameIdentificator");
        RegularExpressionValidatorCodeName.ErrorMessage = GetString("DocumentType_Edit_General.CodeNameIdentificator");

        this.lblLoadGeneration.Text = GetString("LoadGeneration.Title");
        this.plcLoadGeneration.Visible = SettingsKeyProvider.DevelopmentMode;

        btnOk.Text = GetString("general.ok");

        if (documentTypeId > 0)
        {
            classInfo = DataClassInfoProvider.GetDataClass(documentTypeId);
            EditedObject = classInfo;

            if (classInfo != null)
            {
                plcFields.Visible = classInfo.ClassIsCoupledClass;

                selInherits.WhereCondition = "ClassIsCoupledClass = 1 AND ClassID <> " + documentTypeId + " AND (ClassInheritsFromClassID IS NULL OR ClassInheritsFromClassID <> " + documentTypeId + ")";

                className = classInfo.ClassName;

                // Setup the icons
                dfuSmall.TargetFileName = className.Replace('.', '_').ToLower();
                dfuLarge.TargetFileName = className.Replace('.', '_').ToLower();

                if (SettingsKeyProvider.DevelopmentMode)
                {
                    // Only 'gif' images in development mode
                    dfuSmall.AllowedExtensions = "gif;png";
                    dfuLarge.AllowedExtensions = "gif;png";
                }

                // Hide direct file uploader for external storage
                if (StorageHelper.IsExternalStorage)
                {
                    dfuLarge.Visible = false;
                    dfuSmall.Visible = false;
                }

                // Sets regular expression for identificator validation
                RegularExpressionValidatorNameSpaceName.ValidationExpression = ValidationHelper.IdentifierRegExp.ToString();
                RegularExpressionValidatorCodeName.ValidationExpression = ValidationHelper.IdentifierRegExp.ToString();

                if (!RequestHelper.IsPostBack())
                {
                    // Loads data to the form
                    LoadData();
                }
            }
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Setup the icons
        imgSmall.ImageUrl = GetDocumentTypeIconUrl(className) + "?chset=" + Guid.NewGuid();
        imgSmall.ToolTip = GetString("DocumentType.SmallIcon");

        string btnImage = GetImageUrl("Design/Buttons/ContentButton.png");

        dfuSmall.ImageUrl = btnImage;
        dfuSmall.TargetFileName = className.Replace('.', '_');
        dfuSmall.InnerDivHtml = String.Format("<span>{0}</span>", GetString("DocumentType.UploadIcon"));

        imgLarge.ImageUrl = GetDocumentTypeIconUrl(className, "48x48") + "?chset=" + Guid.NewGuid();
        imgLarge.ToolTip = GetString("DocumentType.LargeIcon");

        dfuLarge.ImageUrl = btnImage;
        dfuLarge.TargetFileName = className.Replace('.', '_');
        dfuLarge.InnerDivHtml = String.Format("<span>{0}</span>", GetString("DocumentType.UploadIcon"));

        this.ltlScript.Text += ScriptHelper.GetScript("function RefreshIcons() { " + this.ClientScript.GetPostBackEventReference(this.btnHidden, "") + " }");
    }


    /// <summary>
    /// Loads data of edited document type from DB.
    /// </summary>
    protected void LoadData()
    {
        string[] fullName = ClassNameParser(className);

        tbDisplayName.Text = classInfo.ClassDisplayName;
        tbNamespaceName.Text = fullName[0];
        tbCodeName.Text = fullName[1];
        tbEditingPage.Text = classInfo.ClassEditingPageURL;
        tbListPage.Text = classInfo.ClassListPageURL;
        txtViewPage.Text = classInfo.ClassViewPageUrl;
        txtPreviewPage.Text = classInfo.ClassPreviewPageUrl;
        txtNewPage.Text = classInfo.ClassNewPageURL;
        chkClassUsePublishFromTo.Checked = classInfo.ClassUsePublishFromTo;
        chkTemplateSelection.Checked = classInfo.ClassShowTemplateSelection;
        chkIsMenuItem.Checked = classInfo.ClassIsMenuItemType;
        chkIsProduct.Checked = classInfo.ClassIsProduct;
        templateDefault.PageTemplateID = classInfo.ClassDefaultPageTemplateID;
        templateDefault.ShowOnlySiteTemplates = false;
        chkTemplateSelection_CheckedChanged(null, null);
        lblTableNameValue.Text = classInfo.ClassTableName;
        drpGeneration.Value = classInfo.ClassLoadGeneration;

        selInherits.Value = classInfo.ClassInheritsFromClassID;
    }


    /// <summary>
    /// Parses the class name into the string array.
    /// </summary>
    /// <param name="className">Class name to parse</param>
    private static string[] ClassNameParser(string className)
    {
        string[] fullName = new string[2];
        // finds last dot in full class name
        int lastDot = className.LastIndexOf('.');

        if (lastDot > 0)
        {
            // full class name = namespace + '.' + class name
            fullName[0] = className.Substring(0, lastDot);
            fullName[1] = className.Substring(lastDot + 1, className.Length - lastDot - 1);
        }
        else
        {
            // full transformation name = query name
            fullName[0] = "";
            fullName[1] = className;
        }

        return fullName;
    }


    /// <summary>
    /// Handles the ButtonOK's onClick event.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // finds whether required fields are not empty or well filled
        string result = new Validator().NotEmpty(tbDisplayName.Text.Trim(), GetString("DocumentType_Edit_General.DisplayNameRequired")).
            NotEmpty(tbNamespaceName.Text.Trim(), GetString("DocumentType_Edit_General.NamespaceNameRequired")).
            NotEmpty(tbCodeName.Text.Trim(), GetString("DocumentType_Edit_General.CodeNameRequired")).
            IsIdentificator(tbNamespaceName.Text.Trim(), GetString("DocumentType_Edit_General.NamespaceNameIdentificator")).
            IsCodeName(tbCodeName.Text.Trim(), GetString("DocumentType_Edit_General.CodeNameIdentificator")).Result;

        // Get full class name
        string newClassName = tbNamespaceName.Text.Trim() + "." + tbCodeName.Text.Trim();
        DataClassInfo classInfo = DataClassInfoProvider.GetDataClass(documentTypeId);

        // Check if class exists
        if (classInfo == null)
        {
            return;
        }

        className = classInfo.ClassName;
        bool classNameChanged = (String.Compare(className, newClassName, true) != 0);

        // Check if new class name is unique
        if (classNameChanged)
        {
            DataClassInfo ci = DataClassInfoProvider.GetDataClass(newClassName);
            if ((ci != null) && (ci.ClassID != classInfo.ClassID))
            {
                result += String.Format(GetString("class.exists"), newClassName);
            }
        }

        if (result == "")
        {
            classInfo = classInfo.Clone();

            int originalInherits = classInfo.ClassInheritsFromClassID;
            int inherits = ValidationHelper.GetInteger(selInherits.Value, 0);

            classInfo.ClassInheritsFromClassID = inherits;

            // sets properties of the classInfo
            classInfo.ClassDisplayName = tbDisplayName.Text.Trim();
            classInfo.ClassName = newClassName;
            classInfo.ClassNewPageURL = txtNewPage.Text.Trim();
            classInfo.ClassEditingPageURL = tbEditingPage.Text.Trim();
            classInfo.ClassListPageURL = tbListPage.Text.Trim();
            classInfo.ClassViewPageUrl = txtViewPage.Text.Trim();
            classInfo.ClassPreviewPageUrl = txtPreviewPage.Text.Trim();
            classInfo.ClassUsePublishFromTo = chkClassUsePublishFromTo.Checked;
            classInfo.ClassShowTemplateSelection = chkTemplateSelection.Checked;
            classInfo.ClassIsMenuItemType = chkIsMenuItem.Checked;
            classInfo.ClassDefaultPageTemplateID = templateDefault.PageTemplateID;
            classInfo.ClassIsProduct = chkIsProduct.Checked;
            classInfo.ClassLoadGeneration = drpGeneration.Value;

            // Refresh the tabs according to ClassIsProduct setting
            ScriptHelper.RefreshTabHeader(Page, null);

            try
            {
                bool structureChanged = false;

                // Ensure (update) the inheritance
                if (originalInherits != inherits)
                {
                    if (inherits > 0)
                    {
                        // Update the inherited fields
                        DataClassInfo parentClass = DataClassInfoProvider.GetDataClass(inherits);
                        if (parentClass != null)
                        {
                            FormHelper.UpdateInheritedClass(parentClass, classInfo);
                        }
                    }
                    else
                    {
                        // Remove the inherited fields
                        FormHelper.RemoveInheritance(classInfo, false);
                    }

                    structureChanged = true;
                }

                // updates dataclass in DB (inner unique classname check)
                DataClassInfoProvider.SetDataClass(classInfo);

                if ((className.ToLower() != newClassName.ToLower()) && (classInfo.ClassIsDocumentType))
                {
                    structureChanged = true;

                    // Class name was changed -> update queries
                    SqlGenerator.GenerateQuery(newClassName, "searchtree", SqlOperationTypeEnum.SearchTree, false);
                    SqlGenerator.GenerateQuery(newClassName, "selectdocuments", SqlOperationTypeEnum.SelectDocuments, false);
                    SqlGenerator.GenerateQuery(newClassName, "selectversions", SqlOperationTypeEnum.SelectVersions, false);
                }

                if (structureChanged)
                {
                    // Create view for document types
                    SqlGenerator.GenerateDefaultView(classInfo, null);

                    // Clear class strucures
                    ClassStructureInfo.Remove(classInfo.ClassName, true);
                }

                // Refresh doctype icons
                bool classIsCoupled = classInfo.ClassIsCoupledClass;

                RefreshIcon(className, newClassName, classNameChanged, classIsCoupled, null);
                RefreshIcon(className, newClassName, classNameChanged, classIsCoupled, "48x48");

                className = newClassName;

                lblInfo.Visible = true;
                lblInfo.Text = GetString("General.ChangesSaved");
            }
            catch (Exception ex)
            {
                EventLogProvider.LogException("Document types", "UPDATE", ex);

                // Most probably exception with non unique class name
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }

        }
        else
        {
            // hides asp validators in case the javascript is disabled
            RequiredFieldValidatorDisplayName.Visible = false;
            RequiredFieldValidatorNamespaceName.Visible = false;
            RequiredFieldValidatorCodeName.Visible = false;
            RegularExpressionValidatorNameSpaceName.Visible = false;
            RegularExpressionValidatorCodeName.Visible = false;

            lblError.Visible = true;
            lblError.Text = result;
        }
    }


    private void RefreshIcon(string className, string newClassName, bool classNameChanged, bool isCoupled, string iconSet)
    {
        string sourceFile = GetDocumentTypeIconUrl(className, iconSet, true);
        string targetFile = GetDocumentTypeIconUrl(newClassName, iconSet, false);
        string defaultFile = GetDocumentTypeIconUrl("default", iconSet, false);

        sourceFile = Server.MapPath(sourceFile);
        targetFile = Server.MapPath(targetFile);
        defaultFile = Server.MapPath(defaultFile);

        // Ensure same extension
        if (sourceFile.ToLower().EndsWith(".gif"))
        {
            targetFile = targetFile.Replace(".png", ".gif");
        }

        // Rename icon file
        if (File.Exists(sourceFile))
        {
            if (classNameChanged)
            {
                try
                {
                    if (sourceFile != defaultFile)
                    {
                        File.Move(sourceFile, targetFile);
                    }
                    else
                    {
                        // Copy default file
                        if (!isCoupled)
                        {
                            sourceFile = GetDocumentTypeIconUrl("defaultcontainer", iconSet, false);
                            sourceFile = Server.MapPath(sourceFile);
                        }
                        if (File.Exists(sourceFile))
                        {
                            File.Copy(sourceFile, targetFile);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception
                    EventLogProvider.LogException("Document types", "MOVEICON", ex);
                }
            }
        }
    }


    protected void chkTemplateSelection_CheckedChanged(object sender, EventArgs e)
    {
        this.templateDefault.Enabled = !this.chkTemplateSelection.Checked;
        if (!this.templateDefault.Enabled)
        {
            templateDefault.PageTemplateID = 0;
        }
    }
}
