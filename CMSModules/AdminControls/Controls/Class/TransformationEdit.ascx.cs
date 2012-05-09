using System;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data;
using System.Collections.Generic;

using CMS.CMSHelper;
using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.IO;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.UIControls;
using System.Web;

public partial class CMSModules_AdminControls_Controls_Class_TransformationEdit : CMSUserControl
{
    #region "Variables"

    private CMSMasterPage mCurrentMaster;

    private int transformationId;
    private string mClassName = string.Empty;

    private TransformationInfo ti = new TransformationInfo();

    private int mClassId;
    private string mTransformationName = string.Empty;
    private bool mIsSaved;

    private CurrentUserInfo user = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets the URL of editing page(redirect after save).
    /// </summary>
    public string EditingPage
    {
        get;
        set;
    }


    /// <summary>
    /// Gets or sets URL od listing page(breadcrumbs).
    /// </summary>
    public string ListPage
    {
        get;
        set;
    }


    /// <summary>
    /// Gets or sets the paramatername with transformation class id.
    /// </summary>
    public string ParameterName
    {
        get;
        set;
    }


    /// <summary>
    /// Indicates if class selector will be displayed.
    /// </summary>
    public bool UseClassSelector
    {
        get;
        set;
    }


    /// <summary>
    /// Gets or sets whether the control is in edit mode.
    /// </summary>
    private bool EditMode
    {
        get;
        set;
    }


    /// <summary>
    /// Gets or sets whether the control is in dialog mode.
    /// </summary>
    private bool DialogMode
    {
        get;
        set;
    }


    /// <summary>
    /// If true, control is used in site manager.
    /// </summary>
    public bool IsSiteManager
    {
        get
        {
            return filter.IsSiteManager;
        }
        set
        {
            filter.IsSiteManager = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Init(object sender, EventArgs e)
    {
        mTransformationName = GetString("DocumentType_Edit_Transformation_Edit.NewTransformation");

        GetParameters();

        user = CMSContext.CurrentUser;

        // Get CMS master page
        mCurrentMaster = Page.Master as CMSMasterPage;

        // Check master page
        if (mCurrentMaster == null)
        {
            throw new Exception("Page using this control must have CMSMasterPage master page.");
        }

        // Initializes validator
        RequiredFieldValidatorTransformationName.ErrorMessage = GetString("DocumentType_Edit_Transformation_Edit.TransformationNameRequired");

        // Initializes labels
        LabelsInit();

        // Initialize drop down list
        DropDownListInit();

        // Init transformation help link        
        lnkHelp.NavigateUrl = helpElem.DocumentationUrl + "newedit_transformation_methods.htm";
        lnkHelp.Target = "_target";

        // Init transofrmation help icon
        helpElem.HelpName = "helpTopic";
        helpElem.TopicName = "newedit_transformation_methods";

        if (transformationId <= 0)
        {
            mCurrentMaster.Title.HelpTopicName = "newedit_transformation";
            mCurrentMaster.Title.HelpName = "helpTopic";
        }

        // Page has been opened in CMSDesk and only transformation code editing is allowed        
        if (DialogMode)
        {
            // Sets dialog mode and validates input (return in case of error)
            if (SetDialogMode())
            {
                return;
            }
        }
        else
        {
            if (transformationId <= 0)
            {
                // Initializes PageTitle
                string transformations = GetString("DocumentType_Edit_Transformation_Edit.Transformations");
                string[,] pageTitleTabs = new string[2, 3];

                pageTitleTabs[0, 0] = transformations;
                pageTitleTabs[0, 1] = string.Format("{0}?{1}={2}", ListPage, ParameterName, mClassId);
                pageTitleTabs[1, 0] = mTransformationName;
                mCurrentMaster.Title.Breadcrumbs = pageTitleTabs;
            }
        }

        if (UseClassSelector)
        {
            plcDocTypeFilter.Visible = true;
            filter.IsLiveSite = IsLiveSite;
        }

        if (ti.TransformationID != 0)
        {
            if (ti.TransformationCheckedOutByUserID > 0)
            {
                // Disable textboxes when disabled
                txtCode.Editor.ReadOnly = false;
                txtCSS.Enabled = false;

                string username = null;
                UserInfo ui = UserInfoProvider.GetUserInfo(ti.TransformationCheckedOutByUserID);
                if (ui != null)
                {
                    username = HTMLHelper.HTMLEncode(ui.FullName);
                }

                // Checked out by current machine
                if (string.Equals(ti.TransformationCheckedOutMachineName, HTTPHelper.MachineName, StringComparison.OrdinalIgnoreCase))
                {
                    lblCheckOutInfo.Text = string.Format(GetString("Transformation.CheckedOut"), Server.MapPath(ti.TransformationCheckedOutFilename));
                }
                else
                {
                    lblCheckOutInfo.Text = string.Format(GetString("Transformation.CheckedOutOnAnotherMachine"), ti.TransformationCheckedOutMachineName, username);
                }
            }
            else
            {
                lblCheckOutInfo.Text = string.Format(GetString("Transformation.CheckOutInfo"), Server.MapPath(TransformationInfoProvider.GetTransformationUrl(ti.TransformationFullName, null, ti.TransformationType)));
            }
        }

        pnlCheckOutInfo.Visible = (transformationId > 0);

        InitHeaderActions(ti);

        // Hide generate button DDL for code if not coupled document or custom table
        if (!string.IsNullOrEmpty(mClassName))
        {
            DataClassInfo classInfo = DataClassInfoProvider.GetDataClass(mClassName);

            // Set corret help topic for custom tables
            if (classInfo.ClassIsCustomTable)
            {
                mCurrentMaster.Title.HelpTopicName = "customtable_edit_newedit_transformation";
            }

            if ((classInfo.ClassIsCustomTable) || (classInfo.ClassIsDocumentType && classInfo.ClassIsCoupledClass))
            {
                btnDefaultTransformation.Visible = true;

                // Hide code definition DDL if XSLT transformation type is selected
                drpTransformationCode.Visible = (drpType.SelectedValue == TransformationTypeEnum.Ascx.ToString());
            }
            else
            {
                drpTransformationCode.Visible = false;
                btnDefaultTransformation.Visible = false;
            }
        }

        this.txtCode.Editor.Width = new Unit("99%");
        this.txtCode.Editor.Height = new Unit("300px");
        this.txtCode.NamespaceUsings = new List<string>() { "Transformation" };
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Display controls
        if (ti != null)
        {
            if (!RequestHelper.IsPostBack())
            {
                // Fills form with transformation information
                drpType.SelectedValue = ti.TransformationType.ToString();

                tbTransformationName.Text = ti.TransformationName;
                txtCSS.Text = ti.TransformationCSS;

                if (ti.TransformationType == TransformationTypeEnum.Html)
                {
                    tbWysiwyg.ResolvedValue = ti.TransformationCode;
                }
                else
                {
                    txtCode.Text = ti.TransformationCode;
                }

                // Show the correct control
                if (ti.TransformationType == TransformationTypeEnum.Html)
                {
                    tbWysiwyg.Visible = true;
                }
                else
                {
                    txtCode.Visible = true;
                }
            }
        }

        // Hide or display directives
        UpdateDirectives();

        ScriptHelper.RegisterWOpenerScript(Page);

        // Hide/Display CSS section
        this.plcCssLink.Visible = String.IsNullOrEmpty(txtCSS.Text.Trim());
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        bool editingEnabled = true;
        bool isAscx = (drpType.SelectedValue.ToLower() == "ascx");

        // Disable items when virtual path provider is disabled
        if (!SettingsKeyProvider.UsingVirtualPathProvider && (ti != null))
        {
            if (isAscx)
            {
                lblVirtualInfo.Text = string.Format(GetString("Transformation.VirtualPathProviderNotRunning"), TransformationInfoProvider.GetTransformationUrl(ti.TransformationFullName, null, TransformationTypeEnum.Ascx));
                plcVirtualInfo.Visible = true;

                editingEnabled = false;
                txtCSS.Enabled = false;
            }

            tbWysiwyg.Enabled = !isAscx;
            pnlCheckOutInfo.Visible = false;
        }

        string info = null;

        // Setup the information and code type
        if (isAscx)
        {
            txtCode.Editor.Language = LanguageEnum.ASPNET;
            txtCode.UseAutoComplete = false;

            // Check the edit code permission
            if (!user.IsAuthorizedPerResource("CMS.Design", "EditCode"))
            {
                editingEnabled = false;
                info = ResHelper.GetString("EditCode.NotAllowed");
                ltlDirectives.Visible = false;
            }
        }
        else
        {
            txtCode.Editor.Language = LanguageEnum.HTMLMixed;
            txtCode.UseAutoComplete = true;
        }

        if (!String.IsNullOrEmpty(info))
        {
            lblTransformationInfo.Text = info;
        }
        lblTransformationInfo.Visible = !String.IsNullOrEmpty(lblTransformationInfo.Text);

        this.plcInfo.Visible = isAscx;

        this.txtCode.ReadOnly = !editingEnabled;
    }


    private void GetParameters()
    {
        // Gets classID from querystring, used when creating new transformation
        mClassId = QueryHelper.GetInteger(ParameterName, 0);
        if (mClassId > 0)
        {
            mClassName = DataClassInfoProvider.GetClassName(mClassId);
        }

        // Gets transformationID from querystring, used when editing transformation
        transformationId = QueryHelper.GetInteger("transformationid", 0);
        if (transformationId > 0)
        {
            // Get the transformation
            ti = TransformationInfoProvider.GetTransformation(transformationId);
            CMSPage.EditedObject = ti;

            mTransformationName = ti.TransformationName;

            mClassName = DataClassInfoProvider.GetClassName(ti.TransformationClassID);
            mClassId = DataClassInfoProvider.GetDataClass(mClassName).ClassID;

            if (!RequestHelper.IsPostBack())
            {
                // Shows that the new transformation was created or updated successfully
                if (QueryHelper.GetBoolean("saved", false))
                {
                    ShowInfo(GetString("General.ChangesSaved"));
                }
            }
        }
        else
        {
            ti.TransformationFullName = string.Empty;
        }

        DialogMode = QueryHelper.GetBoolean("editonlycode", false);

        if (DialogMode)
        {
            mTransformationName = QueryHelper.GetString("name", string.Empty);
        }

        EditMode = mTransformationName != string.Empty;
    }


    /// <summary>
    /// Initializes header action control.
    /// </summary>
    private void InitHeaderActions(TransformationInfo ti)
    {
        // Header actions
        string[,] actions = new string[4, 11];

        // Save button
        actions[0, 0] = HeaderActions.TYPE_SAVEBUTTON;
        actions[0, 1] = GetString("General.Save");
        actions[0, 5] = GetImageUrl("CMSModules/CMS_Content/EditMenu/save.png");
        actions[0, 6] = "save";
        actions[0, 8] = "true";

        if (SettingsKeyProvider.UsingVirtualPathProvider || (ti != null) && (ti.TransformationType == TransformationTypeEnum.Xslt))
        {
            // CheckOut
            actions[1, 0] = HeaderActions.TYPE_SAVEBUTTON;
            actions[1, 1] = GetString("General.CheckOutToFile");
            actions[1, 5] = GetImageUrl("CMSModules/CMS_Content/EditMenu/checkout.png");
            actions[1, 6] = "checkout";
            actions[1, 10] = "false";

            // CheckIn
            actions[2, 0] = HeaderActions.TYPE_SAVEBUTTON;
            actions[2, 1] = GetString("General.CheckInFromFile");
            actions[2, 5] = GetImageUrl("CMSModules/CMS_Content/EditMenu/checkin.png");
            actions[2, 6] = "checkin";
            actions[2, 10] = "false";

            // UndoCheckOut
            actions[3, 0] = HeaderActions.TYPE_SAVEBUTTON;
            actions[3, 1] = GetString("General.UndoCheckout");
            actions[3, 2] = string.Concat("return confirm(", ScriptHelper.GetString(GetString("General.ConfirmUndoCheckOut")), ");");
            actions[3, 5] = GetImageUrl("CMSModules/CMS_Content/EditMenu/undocheckout.png");
            actions[3, 6] = "undocheckout";
            actions[3, 10] = "false";

            if (ti.TransformationID != 0)
            {
                if (ti.TransformationCheckedOutByUserID > 0)
                {
                    // Checked out by current machine
                    if (string.Equals(ti.TransformationCheckedOutMachineName, HTTPHelper.MachineName, StringComparison.OrdinalIgnoreCase))
                    {
                        actions[2, 10] = "true";
                    }
                    if (user.UserSiteManagerAdmin)
                    {
                        actions[3, 10] = "true";
                    }
                }
                else
                {
                    actions[1, 10] = "true";
                }
            }
        }

        mCurrentMaster.HeaderActions.LinkCssClass = "ContentSaveLinkButton";
        mCurrentMaster.HeaderActions.ActionPerformed += HeaderActions_ActionPerformed;
        mCurrentMaster.HeaderActions.Actions = actions;
    }


    /// <summary>
    /// Actions handler.
    /// </summary>
    protected void HeaderActions_ActionPerformed(object sender, CommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "checkout":
                CheckOut();
                break;

            case "save":
                Save(false);
                break;

            case "checkin":
                CheckIn();
                break;

            case "undocheckout":
                UndoCheckOut();
                break;
        }
    }


    /// <summary>
    /// Initializes labels.
    /// </summary>
    private void LabelsInit()
    {
        // Initializes labels        
        string lang = DataHelper.GetNotEmpty(SettingsHelper.AppSettings["CMSProgrammingLanguage"], "C#");
        ltlDirectives.Text = string.Concat("&lt;%@ Control Language=\"", lang, "\" AutoEventWireup=\"true\" Inherits=\"CMS.Controls.CMSAbstractTransformation\" %&gt;<br />&lt;%@ Register TagPrefix=\"cc1\" Namespace=\"CMS.Controls\" Assembly=\"CMS.Controls\" %&gt;");

        int saved = QueryHelper.GetInteger("saved", 0);
        if (saved > 0)
        {
            ShowInfo(GetString("General.ChangesSaved"));
        }
    }


    /// <summary>
    /// Initializes dropdown lists.
    /// </summary>
    private void DropDownListInit()
    {
        // Initialize
        if ((transformationId > 0) || !String.IsNullOrEmpty(mTransformationName) || user.IsAuthorizedPerResource("CMS.Design", "EditCode"))
        {
            drpType.Items.Add(new ListItem(GetString("TransformationType.Ascx"), TransformationTypeEnum.Ascx.ToString()));
        }
        drpType.Items.Add(new ListItem(GetString("TransformationType.Text"), TransformationTypeEnum.Text.ToString()));
        drpType.Items.Add(new ListItem(GetString("TransformationType.Html"), TransformationTypeEnum.Html.ToString()));
        drpType.Items.Add(new ListItem(GetString("TransformationType.Xslt"), TransformationTypeEnum.Xslt.ToString()));
        drpType.Items.Add(new ListItem(GetString("TransformationType.jQuery"), TransformationTypeEnum.jQuery.ToString()));

        drpTransformationCode.Items.Add(new ListItem(GetString("TransformationTypeCode.Default"), DefaultTransformationTypeEnum.Default.ToString()));
        drpTransformationCode.Items.Add(new ListItem(GetString("TransformationTypeCode.Atom"), DefaultTransformationTypeEnum.Atom.ToString()));
        drpTransformationCode.Items.Add(new ListItem(GetString("TransformationTypeCode.RSS"), DefaultTransformationTypeEnum.RSS.ToString()));
        drpTransformationCode.Items.Add(new ListItem(GetString("TransformationTypeCode.XML"), DefaultTransformationTypeEnum.XML.ToString()));
    }


    /// <summary>
    /// Checks whether XSLT transformation text is valid.
    /// </summary>
    /// <param name="xmlText">XML text</param>
    /// <returns>Error message.</returns>
    protected string XMLValidator(string xmlText)
    {
        // Creates memory stream from transformation text
        Stream stream = MemoryStream.New();
        StreamWriter writer = StreamWriter.New(stream);
        writer.Write(xmlText);
        writer.Flush();
        stream.Seek(0, SeekOrigin.Begin);

        // New xml text reader from the stream
        XmlTextReader tr = new XmlTextReader(stream.SystemStream);
        try
        {
            // Need to read the data to validate
            while (tr.Read()) ;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }

        return string.Empty;
    }


    /// <summary>
    /// Default Transformation Click event.
    /// </summary>
    protected void btnDefaultTransformation_Click(object sender, EventArgs e)
    {
        DefaultTransformation();
        UpdateDirectives();
    }


    /// <summary>
    /// Validates the transformation and saves it.
    /// </summary>    
    /// <returns>true if there were any validation errors, otherwise false</returns>
    private bool ValidateAndSave()
    {
        tbTransformationName.Text = tbTransformationName.Text.Trim();

        // Validate transformation name for emptiness and code name format
        string result = new Validator()
            .NotEmpty(tbTransformationName.Text, GetString("DocumentType_Edit_Transformation_Edit.TransformationNameRequired"))
            .IsRegularExp(tbTransformationName.Text, "^[A-Za-z][A-Za-z0-9_-]*$", GetString("DocumentType_Edit.TransformationNameFormat"))
            .Result;

        if (result != string.Empty)
        {
            ShowError(result);
            return true;
        }

        if (drpType.SelectedValue.ToLower() == "xslt")
        {
            // Validates XSLT transformatin text
            result = XMLValidator(txtCode.Text);
        }

        if (result != string.Empty)
        {
            // XML validation error
            ShowError(string.Format("{0}'{1}'", GetString("DocumentType_Edit_Transformation_Edit.XSLTTransformationError"), result));
            return true;
        }

        mIsSaved = false;
        TransformationInfo newTransformation = null;

        // Initialize class name form class selector if used
        if (string.IsNullOrEmpty(mClassName) && UseClassSelector)
        {
            mClassName = DataClassInfoProvider.GetClassName(filter.ClassId);
        }

        // Gets full transformation name in the form - "ClassName.TransformationName"
        mTransformationName = (mClassName != string.Empty) ?
                                string.Concat(mClassName, ".", tbTransformationName.Text) :
                                tbTransformationName.Text;

        // Finds whether new or edited transformation is unique in the document type
        try
        {
            // Throws exception if transformation of the given name doesn't exist
            newTransformation = TransformationInfoProvider.GetTransformation(mTransformationName);
            if (newTransformation.TransformationID == transformationId)
            {
                mIsSaved = SaveTransformation();
            }
            else
            {
                string errorMsg = DataClassInfoProvider.GetDataClass(mClassName).ClassIsDocumentType ?
                    GetString("DocumentType_Edit_Transformation_Edit.UniqueTransformationNameDocType") :
                    GetString("DocumentType_Edit_Transformation_Edit.UniqueTransformationNameCustomTable");
                ShowError(errorMsg);
            }
        }
        catch
        {
            mIsSaved = SaveTransformation();
        }

        return false;
    }


    /// <summary>
    /// Generates transformation code depending on the transformation type (Xslt, Ascx).
    /// </summary>
    private void DefaultTransformation()
    {
        if (String.IsNullOrEmpty(mClassName) && UseClassSelector)
        {
            mClassName = DataClassInfoProvider.GetClassName(filter.ClassId);
        }

        // Gets Xml schema of the document type
        DataClassInfo dci = DataClassInfoProvider.GetDataClass(mClassName);
        string formDef = string.Empty;
        if (dci != null)
        {
            formDef = dci.ClassFormDefinition;
        }

        // Gets transformation type
        TransformationTypeEnum transformType = TransformationInfoProvider.GetTransformationTypeEnum(drpType.SelectedValue);

        DefaultTransformationTypeEnum transformCode = DefaultTransformationTypeEnum.Default;
        if (transformType == TransformationTypeEnum.Ascx)
        {
            switch (drpTransformationCode.SelectedValue)
            {
                // Atom transformation code
                case "Atom":
                    transformCode = DefaultTransformationTypeEnum.Atom;
                    break;

                // RSS transformation code
                case "RSS":
                    transformCode = DefaultTransformationTypeEnum.RSS;
                    break;

                // XML transformation code
                case "XML":
                    transformCode = DefaultTransformationTypeEnum.XML;
                    break;

                // Default ASCX transformation code
                default:
                    transformCode = DefaultTransformationTypeEnum.Default;
                    break;
            }
        }

        // Writes the result to the text box
        if (transformType == TransformationTypeEnum.Html)
        {
            txtCode.Visible = false;
            tbWysiwyg.Visible = true;
            tbWysiwyg.ResolvedValue = TransformationInfoProvider.GenerateTransformationCode(formDef, transformType, mClassName, transformCode);
        }
        else
        {
            tbWysiwyg.Visible = false;
            txtCode.Visible = true;
            txtCode.Text = TransformationInfoProvider.GenerateTransformationCode(formDef, transformType, mClassName, transformCode);
        }
    }


    /// <summary>
    /// Saves new or edited transformation of the given name and returns to the transformation list.
    /// </summary>
    /// <returns>True if transformation was succesfully saved</returns>
    private bool SaveTransformation()
    {
        // Sets transformation object's properties
        ti.TransformationName = tbTransformationName.Text;

        DataClassInfo dci = DataClassInfoProvider.GetDataClass(mClassName);
        if (dci != null)
        {
            TransformationTypeEnum transformationType = TransformationInfoProvider.GetTransformationTypeEnum(drpType.SelectedValue);

            ti.TransformationClassID = dci.ClassID;
            ti.TransformationCSS = txtCSS.Text;

            if (ti.TransformationCheckedOutByUserID <= 0)
            {
                // Save the code
                if ((transformationType != TransformationTypeEnum.Ascx) || user.IsAuthorizedPerResource("CMS.Design", "EditCode"))
                {
                    ti.TransformationType = transformationType;
                    ti.TransformationCode = (transformationType == TransformationTypeEnum.Html) ? tbWysiwyg.ResolvedValue : txtCode.Text;
                }

                drpType.SelectedValue = ti.TransformationType.ToString();
            }

            TransformationInfoProvider.SetTransformation(ti);

            return true;
        }
        else
        {
            ShowError(GetString("editedobject.notexists"));
            return false;
        }
    }


    protected void drpTransformationType_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateDirectives();

        if (ti.TransformationCheckedOutByUserID <= 0)
        {
            // Show checkout info corresponding to selected type
            TransformationTypeEnum type = TransformationInfoProvider.GetTransformationTypeEnum(drpType.SelectedValue);
            string path = Server.MapPath(TransformationInfoProvider.GetTransformationUrl(ti.TransformationFullName, null, type));
            lblCheckOutInfo.Text = string.Format(GetString("Transformation.CheckOutInfo"), path);
        }

        // Get the current code
        string code = "";
        if (txtCode.Visible)
        {
            code = this.txtCode.Text;
        }
        else
        {
            code = this.tbWysiwyg.ResolvedValue;
        }

        switch (drpType.SelectedValue.ToLower())
        {
            case "ascx":
                // Convert to ASCX syntax
                if (string.Equals(drpType.SelectedValue, "ascx", StringComparison.OrdinalIgnoreCase))
                {
                    code = MacroResolver.RemoveSecurityParameters(code, false, null);

                    code = code.Replace("{% Register", "<%@ Register").Replace("{%", "<%#").Replace("%}", "%>");
                }
                break;

            case "xslt":
                // No transformation
                break;

            default:
                // Convert to macro syntax
                code = code.Replace("<%@", "{%").Replace("<%#", "{%").Replace("<%=", "{%").Replace("<%", "{%").Replace("%>", "%}");
                break;
        }

        // Move the content if necessary
        if (string.Equals(drpType.SelectedValue, "html", StringComparison.OrdinalIgnoreCase))
        {
            // Move from text to WYSIWYG
            if (txtCode.Visible)
            {
                this.tbWysiwyg.ResolvedValue = code;
                this.tbWysiwyg.Visible = true;

                this.txtCode.Text = string.Empty;
                this.txtCode.Visible = false;
            }
        }
        else
        {
            // Move from WYSIWYG to text
            if (tbWysiwyg.Visible)
            {
                code = HttpUtility.HtmlDecode(code);

                this.txtCode.Text = code;
                this.txtCode.Visible = true;

                this.tbWysiwyg.ResolvedValue = string.Empty;
                this.tbWysiwyg.Visible = false;
            }
            else
            {
                this.txtCode.Text = code;
            }
        }
    }


    private void UpdateDirectives()
    {
        bool isAscx = string.Equals(drpType.SelectedValue, "ascx", StringComparison.OrdinalIgnoreCase);

        // Hide or display directives and examples depending on whether the transformation is ASCX
        lnkHelp.Visible = ltlDirectives.Visible = helpElem.Visible = plcTransformationCode.Visible = isAscx;
    }


    private bool SetDialogMode()
    {
        if (!string.IsNullOrEmpty(mTransformationName))
        {
            // Error message variable
            string errorMessage = null;

            // Get transformation info
            ti = TransformationInfoProvider.GetTransformation(mTransformationName);
            if (ti != null)
            {
                transformationId = ti.TransformationID;
                mClassName = DataClassInfoProvider.GetClassName(ti.TransformationClassID);

                // Check if document type is registered under current site
                DataSet ds = ClassSiteInfoProvider.GetClassSites("ClassID", "ClassID = " + ti.TransformationClassID + " AND SiteID = " + CMSContext.CurrentSiteID, null, -1);
                if (DataHelper.DataSourceIsEmpty(ds) && !user.UserSiteManagerAdmin)
                {
                    // Set error message
                    errorMessage = GetString("formcontrols_selecttransformation.classnotavailablesite").Replace("%%code%%", HTMLHelper.HTMLEncode(mClassName));
                }
                else
                {
                    tbTransformationName.ReadOnly = true;
                }
            }
            else
            {
                // Set error message
                errorMessage = GetString("formcontrols_selecttransformation.transofrmationnotexist").Replace("%%code%%", HTMLHelper.HTMLEncode(mTransformationName));
            }

            // Hide panel Menu and write error message
            if (!String.IsNullOrEmpty(errorMessage))
            {
                ShowError(errorMessage);
                pnlCheckOutInfo.Visible = plcControl.Visible = false;
                pnlContent.CssClass = "PageContent";
                return true;
            }
        }
        else
        {
            // Set page title
            mCurrentMaster.Title.TitleText = GetString("documenttype_edit_transformation_edit.newtransformation");
            mCurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_Transformation/transformation_new.png");

            // Display class selector
            UseClassSelector = true;
            filter.SelectedValue = QueryHelper.GetString("selectedvalue", null);
            filter.FilterMode = SettingsObjectType.TRANSFORMATION;
        }

        pnlContent.CssClass = "PageContent";

        SetDialogButtons();

        return false;
    }


    private void SetDialogButtons()
    {
        mCurrentMaster.PanelFooter.CssClass = "FloatRight";

        // Create new transformation
        if (!EditMode)
        {
            // Hide header actions when creating new transformation in dialog
            mCurrentMaster.HeaderActions.Parent.Visible = false;

            // Add save button
            LocalizedButton btnSave = new LocalizedButton
            {
                ID = "btnSave",
                ResourceString = "general.save",
                EnableViewState = false,
                CssClass = "SubmitButton"
            };
            btnSave.Click += (sender, e) => Save(false);
            mCurrentMaster.PanelFooter.Controls.Add(btnSave);
        }

        // Add save & close button
        LocalizedButton btnSaveAndCancel = new LocalizedButton
        {
            ID = "btnSaveCancel",
            ResourceString = "general.saveandclose",
            EnableViewState = false,
            CssClass = "LongSubmitButton"
        };
        btnSaveAndCancel.Click += (sender, e) =>
        {
            if (Save(true))
            {
                // Need to register one more window close because of the postback
                ScriptHelper.RegisterStartupScript(this, GetType(), "SaveAndClose", "window.top.close();", true);
            }
        };
        mCurrentMaster.PanelFooter.Controls.Add(btnSaveAndCancel);

        // Add close button every time
        mCurrentMaster.PanelFooter.Controls.Add(new LocalizedButton
        {
            ID = "btnCancel",
            ResourceString = "general.close",
            EnableViewState = false,
            OnClientClick = "window.top.close(); return false;",
            CssClass = "SubmitButton"
        });
    }


    private void CheckIn()
    {
        try
        {
            SiteManagerFunctions.CheckInTransformation(transformationId);
            URLHelper.Redirect(URLHelper.Url.AbsoluteUri);
        }
        catch (Exception ex)
        {
            ShowError(string.Concat(GetString("Transformation.ErrorCheckin"), ": ", ex.Message));
        }
    }


    private void CheckOut()
    {
        // Ensure version before check-out
        using (CMSActionContext context = new CMSActionContext())
        {
            context.AllowAsyncActions = false;

            // Validate and save the transformation
            if (ValidateAndSave())
            {
                return;
            }
        }

        try
        {
            SiteManagerFunctions.CheckOutTransformation(transformationId);
            URLHelper.Redirect(URLHelper.AddParameterToUrl(URLHelper.Url.AbsoluteUri, "saved", "1"));
        }
        catch (Exception ex)
        {
            ShowError(string.Concat(GetString("Transformation.ErrorCheckout"), ": ", ex.Message));
        }
    }


    private void UndoCheckOut()
    {
        try
        {
            SiteManagerFunctions.UndoCheckOutTransformation(transformationId);
            URLHelper.Redirect(URLHelper.Url.AbsoluteUri);
        }
        catch (Exception ex)
        {
            lblError.Text = string.Concat(GetString("Transformation.ErrorUndoCheckout"), ": ", ex.Message);
            lblError.Visible = true;
        }
    }


    private bool Save(bool closeOnSave)
    {
        if (ValidateAndSave())
        {
            return false;
        }

        if (!mIsSaved)
        {
            return false;
        }

        if (!DialogMode)
        {
            URLHelper.Redirect(string.Format("{0}?transformationid={1}&hash={2}&saved=1&tabmode={3}", EditingPage, ti.TransformationID, QueryHelper.GetHash("?transformationid=" + ti.TransformationID), QueryHelper.GetInteger("tabmode", 0)));
            return true;
        }

        if (DialogMode)
        {
            if (!UseClassSelector)
            {
                ShowInfo(GetString("General.ChangesSaved"));
                return true;
            }

            // Check for selector ID
            string selector = QueryHelper.GetString("selectorid", string.Empty);
            if (!string.IsNullOrEmpty(selector))
            {
                // Add selector refresh
                string script =
                    string.Format(@"var wopener = window.top.opener ? window.top.opener : window.top.dialogArguments;
		                            if (wopener) {{ wopener.US_SelectNewValue_{0}('{1}'); }}",
                                    selector, ti.TransformationFullName);

                if (closeOnSave)
                {
                    script += "window.top.close();";
                }
                else
                {
                    script += string.Format(@"window.name = '{0}';
		                                      window.open('{2}?name={1}&saved=1&editonlycode=1&hash={3}&selectorid={0}&tabmode={4}',window.name);",
                                              selector, ti.TransformationFullName, EditingPage, QueryHelper.GetHash("?editonlycode=1"), QueryHelper.GetInteger("tabmode", 0));
                }

                ScriptHelper.RegisterStartupScript(this, GetType(), "UpdateSelector", script, true);
            }
            return true;
        }

        // This should never happen :)
        return false;
    }


    private void ShowError(string message)
    {
        lblError.Visible = true;
        lblError.Text = message;
    }


    private void ShowInfo(string message)
    {
        lblInfo.Visible = true;
        lblInfo.Text = message;
    }

    #endregion
}