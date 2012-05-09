using System;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.FormEngine;
using CMS.GlobalHelper;
using CMS.PortalEngine;
using CMS.SettingsProvider;
using CMS.TreeEngine;
using CMS.UIControls;
using CMS.WorkflowEngine;

using TreeNode = CMS.TreeEngine.TreeNode;
using CMS.CKEditor;

public partial class CMSModules_Content_CMSDesk_Properties_Advanced_EditableContent_Main : CMSModalPage
{
    #region "Private variables"

    private int nodeId = 0;
    private string keyName = null;
    private EditableContentType keyType;

    protected TreeNode node = null;
    protected TreeProvider mTreeProvider = null;
    private WorkflowManager mWorkflowManager = null;
    private WorkflowInfo wi = null;
    private VersionManager mVersionManager = null;
    private string content = null;
    private bool createNew = false;
    private bool checkin = false;

    private Control invokeControl = null;

    private enum EditingForms { EditableImage = 0, HTMLEditor = 1, TextArea = 2, TextBox = 3 };
    private enum EditableContentType { webpart = 0, region = 1 };

    #endregion


    #region "Properties"

    /// <summary>
    /// Indicates if check-in/check-out functionality is automatic
    /// </summary>
    protected bool AutoCheck
    {
        get
        {
            if (node != null)
            {
                // Get workflow info
                wi = WorkflowManager.GetNodeWorkflow(node);

                // Check if the document uses workflow
                if (wi != null)
                {
                    return !wi.UseCheckInCheckOut(CMSContext.CurrentSiteName);
                }
            }
            return false;
        }
    }


    /// <summary>
    /// Gets Workflow manager instance.
    /// </summary>
    protected WorkflowManager WorkflowManager
    {
        get
        {
            return mWorkflowManager ?? (mWorkflowManager = new WorkflowManager(TreeProvider));
        }
    }


    /// <summary>
    /// Gets Version manager instance.
    /// </summary>
    protected VersionManager VersionManager
    {
        get
        {
            return mVersionManager ?? (mVersionManager = new VersionManager(TreeProvider));
        }
    }


    /// <summary>
    /// Tree provider instance.
    /// </summary>
    protected TreeProvider TreeProvider
    {
        get
        {
            return mTreeProvider ?? (mTreeProvider = new TreeProvider(CMSContext.CurrentUser));
        }
    }

    #endregion


    #region "Page events"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        CurrentUserInfo user = CMSContext.CurrentUser;

        // Check 'read' permissions
        if (!user.IsAuthorizedPerResource("CMS.Content", "Read"))
        {
            RedirectToAccessDenied("CMS.Content", "Read");
        }

        // Check UIProfile
        if (!user.IsAuthorizedPerUIElement("CMS.Content", "Properties.General"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "Properties.General");
        }

        if (!user.IsAuthorizedPerUIElement("CMS.Content", "General.Advanced"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "General.Advanced");
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!RequestHelper.IsPostBack())
        {
            // Fill dropdown list
            InitEditorOptions();

            // Inform user about saving
            if (QueryHelper.GetBoolean("imagesaved", false))
            {
                lblInfo.Text = GetString("general.changessaved");
                lblInfo.Visible = true;
                drpEditControl.SelectedIndex = 1;
            }
        }

        // Initialize HTML editor
        InitHTMLEditor();

        // Find postback invoker
        string invokerName = Page.Request.Params.Get("__EVENTTARGET");
        invokeControl = !string.IsNullOrEmpty(invokerName) ? Page.FindControl(invokerName) : null;

        // Set whether new item is to be created
        createNew = QueryHelper.GetBoolean("createNew", false);

        if (invokerName != null)
        {
            if (createNew && (invokeControl == drpEditControl))
            {
                createNew = true;
            }
            else
            {
                if (invokeControl == drpEditControl)
                {
                    createNew = false;
                    lblInfo.Text = string.Empty;
                    lblInfo.Visible = false;
                }
            }
        }


        // Get query parameters
        keyName = QueryHelper.GetString("nodename", string.Empty);
        nodeId = QueryHelper.GetInteger("nodeid", 0);

        // Set editable content type enum
        switch (QueryHelper.GetString("nodetype", "webpart"))
        {
            case "webpart":
                keyType = EditableContentType.webpart;
                break;

            case "region":
                keyType = EditableContentType.region;
                break;
        }

        // Get node
        node = DocumentHelper.GetDocument(nodeId, CMSContext.PreferredCultureCode, TreeProvider);
        // Set edited document
        EditedDocument = node;

        // Initialize javascripts
        ltlScript.Text += ScriptHelper.GetScript("mainUrl = '" + ResolveUrl("~/CMSModules/Content/CMSDesk/Properties/Advanced/EditableContent/main.aspx") + "?nodeid=" + nodeId + "';");

        lblError.Visible = false;


        // Show editing controls
        if ((createNew || keyName != string.Empty))
        {
            pnlMenu.Visible = true;
            menuElem.AllowSave = IsAuthorizedToModify();
            pnlEditableContent.Visible = true;
        }

        // Get content
        if ((node != null) && (keyName != string.Empty) || createNew)
        {
            if (!RequestHelper.IsPostBack() && !createNew)
            {
                txtName.Text = MultiKeyHashtable.GetFirstKey(keyName);
            }

            if (!createNew)
            {
                content = GetContent();

                // Disable HTML editor if content is typeof image
                if (content != null)
                {
                    if (content.StartsWith("<image>"))
                    {
                        ListItem li =
                            drpEditControl.Items.FindByValue(Convert.ToInt32(EditingForms.HTMLEditor).ToString());
                        if (li != null)
                        {
                            drpEditControl.Items.Remove(li);
                        }
                    }
                }
            }
        }

        // Hide all content controls
        txtAreaContent.Visible = txtContent.Visible = htmlContent.Visible = imageContent.Visible = false;

        // Set up editing forms
        switch (((EditingForms)Convert.ToInt32(drpEditControl.SelectedValue)))
        {
            case EditingForms.TextArea:
                txtAreaContent.Visible = true;
                break;

            case EditingForms.HTMLEditor:
                htmlContent.Visible = true;
                break;

            case EditingForms.EditableImage:
                imageContent.Visible = true;
                imageContent.ImageTitle = HTMLHelper.HTMLEncode(MultiKeyHashtable.GetFirstKey(keyName));
                break;

            case EditingForms.TextBox:
                txtContent.Visible = true;
                break;
        }
        lblContent.Visible = txtContent.Visible;
    }


    protected override void OnPreRender(EventArgs e)
    {
        AddScript("function LocalCheckIn(){" + Page.ClientScript.GetPostBackEventReference(btnCheckIn, null) + ";}");

        // Load content to controls
        bool isMenuAction = false;
        foreach (Control control in menuElem.Controls)
        {
            if (control == invokeControl && control != menuElem.FindControl("btnSave"))
            {
                isMenuAction = true;
            }
        }

        // Get actualized content for undo checkout operation 
        if (invokeControl == menuElem.FindControl("btnUndoCheckout"))
        {
            node = DocumentHelper.GetDocument(menuElem.NodeID, CMSContext.PreferredCultureCode, TreeProvider);
            content = GetContent();
            if ((EditingForms)Convert.ToInt32(drpEditControl.SelectedValue) == EditingForms.EditableImage)
            {
                imageContent.LoadContent(content, true);
            }

            // Reload tree
            ltlScript.Text +=
                ScriptHelper.GetScript("parent.frames['tree'].location.replace('" +
                                       ResolveUrl("~/CMSModules/Content/CMSDesk/Properties/Advanced/EditableContent/tree.aspx") + "?nodeid=" +
                                       nodeId + "&selectednodename=" + txtName.Text.Trim() + "&selectednodetype=" + keyType +
                                       "');");
        }

        // Load content to text fields
        if (!RequestHelper.IsPostBack() || invokeControl == drpEditControl || isMenuAction)
        {
            txtAreaContent.Text = content;
            htmlContent.ResolvedValue = content;
            txtContent.Text = content;
        }

        if (menuElem.AllowEdit && menuElem.AllowSave)
        {
            ScriptHelper.RegisterShortcuts(this);
            SetEnableMode(true);
            LoadImageData(ViewModeEnum.Edit);

        }
        else
        {
            SetEnableMode(false);
            LoadImageData(ViewModeEnum.EditDisabled);
        }

        if (!string.IsNullOrEmpty(keyName))
        {
            // Prepare script for refresh menu in Tree
            string script = "parent.frames['tree'].location.replace('" +
                                          ResolveUrl("~/CMSModules/Content/CMSDesk/Properties/Advanced/EditableContent/tree.aspx") + "?nodeid=" +
                                          nodeId + "&selectednodename=" + keyName + "&selectednodetype=" + keyType + "');";

            // Script for UpdateMenu int Tree
            ScriptHelper.RegisterStartupScript(this, typeof(string), "RefreshTree", ScriptHelper.GetScript(script));
        }

        if (!IsAuthorizedToModify())
        {
            SetEnableMode(false);
        }

        lblWorkflow.Text = menuElem.WorkflowInfo;
        lblWorkflow.Visible = !string.IsNullOrEmpty(lblWorkflow.Text);
        base.OnPreRender(e);
    }

    #endregion


    #region "Button handling"

    /// <summary>
    /// Checkes in node.
    /// </summary>
    protected void btnCheckIn_Click(object sender, EventArgs e)
    {
        // Do validation
        if (!ValidateEntries())
        {
            return;
        }

        try
        {
            // Do save
            checkin = true;
            btnSave_Click(sender, e);

            // Check in
            if (node != null)
            {
                // Check in the document        
                VersionManager.CheckIn(node, null, null);

                lblInfo.Text += " " + GetString("ContentEdit.WasCheckedIn");
            }
        }
        catch (WorkflowException)
        {
            lblError.Visible = true;
            lblInfo.Visible = false;
            lblError.Text += ResHelper.GetString("EditContent.DocumentCannotCheckIn");
        }
        catch (Exception ex)
        {
            lblError.Visible = true;
            lblInfo.Visible = false;
            lblError.Text += ex.Message;
        }

        // Reload menu
        menuElem.Node = node;
        menuElem.ReloadMenu();
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        // Check modify permissions
        if (!IsAuthorizedToModify())
        {
            return;
        }

        EditingForms editingForm = (EditingForms)Convert.ToInt32(drpEditControl.SelectedValue);
        // Get content to save
        switch (editingForm)
        {
            case EditingForms.TextArea:
                content = txtAreaContent.Text.Trim();
                break;

            case EditingForms.HTMLEditor:
                content = htmlContent.ResolvedValue;
                break;

            case EditingForms.EditableImage:
                content = imageContent.GetContent();
                break;

            case EditingForms.TextBox:
                content = txtContent.Text.Trim();
                break;
        }

        if (!ValidateEntries())
        {
            return;
        }

        if (!QueryHelper.GetBoolean("imagesaved", false))
        {
            createNew = !node.DocumentContent.EditableWebParts.Contains(keyName) &&
                        !node.DocumentContent.EditableRegions.ContainsKey(keyName);
        }

        // Code name
        string codeName = txtName.Text.Trim().ToLower();

        // Set PageInfo
        switch (keyType)
        {
            case EditableContentType.webpart:
                if (!createNew)
                {
                    // If editing -> remove old
                    node.DocumentContent.EditableWebParts.Remove(keyName);
                }

                if (!node.DocumentContent.EditableWebParts.ContainsKey(codeName))
                {
                    node.DocumentContent.EditableWebParts.Add(codeName, content);
                }
                else
                {
                    // Set error label
                    lblError.Visible = true;
                    lblError.Text = GetString("EditableContent.ItemExists");
                    return;
                }
                break;

            case EditableContentType.region:
                if (!createNew)
                {
                    // If editing -> remove old
                    node.DocumentContent.EditableRegions.Remove(keyName);
                }

                if (!node.DocumentContent.EditableRegions.ContainsKey(codeName))
                {
                    node.DocumentContent.EditableRegions.Add(codeName, content);
                }
                else
                {
                    // Set error label
                    lblError.Visible = true;
                    lblError.Text = GetString("EditableContent.ItemExists");
                    return;
                }
                break;
        }

        // Save node
        SaveNode();

        if (txtName.Text != string.Empty)
        {
            keyName = codeName;
        }

        // Reload menu
        menuElem.Node = node;
        menuElem.ReloadMenu();

        // Inform user
        lblInfo.Text = GetString("general.changessaved");
        lblInfo.Visible = true;

        // Refresh tree
        if (createNew)
        {
            ltlScript.Text +=
                ScriptHelper.GetScript("parent.frames['tree'].location.replace('" +
                                       ResolveUrl("~/CMSModules/Content/CMSDesk/Properties/Advanced/EditableContent/tree.aspx") + "?nodeid=" +
                                       nodeId + "&selectednodename=" + codeName + "&selectednodetype=" + keyType +
                                       "');SelectNode('" + codeName + "', '" + keyType + "')");
            if (editingForm == EditingForms.EditableImage)
            {
                ltlScript.Text +=
                    ScriptHelper.GetScript("SelectNodeAfterImageSave(" + ScriptHelper.GetString(keyName) + ", '" + keyType + "');");
            }
        }
        else
        {
            ltlScript.Text +=
                ScriptHelper.GetScript("RefreshNode(" + ScriptHelper.GetString(codeName) + ", '" + keyType + "', " + nodeId + ");");
        }
        createNew = false;
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Validates inputs.
    /// </summary>
    /// <returns>True if valid</returns>
    private bool ValidateEntries()
    {
        string codeName = txtName.Text.Trim();
        // Validate
        string errorMessage = new Validator().NotEmpty(codeName, GetString("general.invalidcodename")).IsRegularExp(codeName, ValidationHelper.CodenameRegExp.ToString(), GetString("general.invalidcodename")).Result;
        if (errorMessage != string.Empty)
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
            return false;
        }
        else
        {
            string value = null;
            EditingForms editingForm = (EditingForms)Convert.ToInt32(drpEditControl.SelectedValue);

            // Check content
            switch (editingForm)
            {
                case EditingForms.TextArea:
                    value = txtAreaContent.Text.Trim();
                    break;

                case EditingForms.HTMLEditor:
                    value = htmlContent.ResolvedValue;
                    break;

                case EditingForms.EditableImage:
                    value = imageContent.GetContent();
                    break;

                case EditingForms.TextBox:
                    value = txtContent.Text.Trim();
                    break;
            }

            errorMessage = new Validator().NotEmpty(value, GetString("EditableContent.NotEmpty")).Result;

            if (!String.IsNullOrEmpty(errorMessage))
            {
                lblError.Visible = true;
                lblError.Text = errorMessage;
                return false;
            }
        }
        return true;
    }


    /// <summary>
    /// Saves node, ensures workflow.
    /// </summary>
    protected void SaveNode()
    {
        // Get content
        content = node.DocumentContent.GetContentXml();
        // Save content
        if (node != null)
        {
            // If not using check-in/check-out, check out automatically
            if (AutoCheck && !checkin)
            {
                if (!node.IsCheckedOut)
                {
                    // Check out
                    VersionManager.CheckOut(node, node.IsPublished, true);
                }
            }
            node.UpdateOriginalValues();

            node.SetValue("DocumentContent", content);
            DocumentHelper.UpdateDocument(node, TreeProvider);

            // Check in the document
            if (AutoCheck && !checkin)
            {
                VersionManager.CheckIn(node, null, null);
            }
        }
    }


    /// <summary>
    /// Loads data to editable image.
    /// </summary>
    protected void LoadImageData(ViewModeEnum mode)
    {
        // Set view mode
        PortalContext.ViewMode = mode;

        // Initialize editable image properties
        imageContent.DisplaySelectorTextBox = false;
        imageContent.SelectOnlyPublished = false;

        // Ensure loading image
        if (!string.IsNullOrEmpty(content))
        {
            if (content.StartsWith("<image>"))
            {
                // Initialize editable image
                imageContent.LoadContent(content);
            }
        }
    }


    /// <summary>
    /// Initializes HTML editor's settings.
    /// </summary>
    protected void InitHTMLEditor()
    {
        htmlContent.AutoDetectLanguage = false;
        htmlContent.DefaultLanguage = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        // Set direction
        htmlContent.ContentsLangDirection = LanguageDirection.LeftToRight;
        if (CultureHelper.IsPreferredCultureRTL())
        {
            htmlContent.ContentsLangDirection = LanguageDirection.RightToLeft;
        }
        if (CMSContext.CurrentSite != null)
        {
            htmlContent.EditorAreaCSS = FormHelper.GetHtmlEditorAreaCss(CMSContext.CurrentSiteName);
        }
    }


    /// <summary>
    /// Initializes dropdown list with editing options.
    /// </summary>
    protected void InitEditorOptions()
    {
        drpEditControl.Items.AddRange(new ListItem[] {
                new ListItem(GetString("EditableContent.HTMLEditor"), Convert.ToInt32(EditingForms.HTMLEditor).ToString()),
                new ListItem(GetString("EditableContent.EditableImage"), Convert.ToInt32(EditingForms.EditableImage).ToString()), 
                new ListItem(GetString("EditableContent.TextArea"), Convert.ToInt32(EditingForms.TextArea).ToString()) ,
                new ListItem(GetString("EditableContent.TextBox"),Convert.ToInt32(EditingForms.TextBox).ToString() )});
    }


    /// <summary>
    /// Checks whether current user is authorized to modify editable content.
    /// </summary>
    /// <returns>True if authorized.</returns>
    protected bool IsAuthorizedToModify()
    {
        if (node != null)
        {
            return (CMSContext.CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Allowed);
        }
        return false;
    }


    /// <summary>
    /// Gets editable element content.
    /// </summary>
    protected string GetContent()
    {
        string content = null;
        if ((node != null) && !String.IsNullOrEmpty(keyName))
        {
            // Set content variable
            switch (keyType)
            {
                case EditableContentType.webpart:
                    if (node.DocumentContent.EditableWebParts.ContainsKey(keyName))
                    {
                        content =
                            ValidationHelper.GetString(
                                node.DocumentContent.EditableWebParts[keyName].ToString(), string.Empty);
                    }
                    break;

                case EditableContentType.region:
                    if (node.DocumentContent.EditableRegions.ContainsKey(keyName))
                    {
                        content =
                            ValidationHelper.GetString(
                                node.DocumentContent.EditableRegions[keyName].ToString(), string.Empty);
                    }
                    break;
            }
        }
        return content;
    }


    /// <summary>
    /// Sets enable mode of controls.
    /// </summary>
    /// <param name="enableMode">Value of enable mode</param>
    protected void SetEnableMode(bool enableMode)
    {
        // Set info labels mode
        lblInfo.Enabled = enableMode;
        lblError.Enabled = enableMode;
        lblWorkflow.Enabled = enableMode;

        // Set data controls mode
        lblEditControl.Enabled = enableMode;
        drpEditControl.Enabled = enableMode;
        lblName.Enabled = enableMode;
        txtName.Enabled = enableMode;

        // Set content preview mode
        txtAreaContent.Enabled = enableMode;
        htmlContent.Enabled = enableMode;
        lblContent.Enabled = enableMode;
        txtContent.Enabled = enableMode;
    }


    /// <summary>
    /// Adds the script to the output request window.
    /// </summary>
    /// <param name="script">Script to add</param>
    public override void AddScript(string script)
    {
        ScriptHelper.RegisterStartupScript(this, typeof(string), script.GetHashCode().ToString(), ScriptHelper.GetScript(script));
    }

    #endregion
}
