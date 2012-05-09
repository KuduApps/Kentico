using System;

using CMS.CMSHelper;
using CMS.FormControls;
using CMS.SettingsProvider;
using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSFormControls_Classes_SelectTransformation : FormEngineUserControl
{
    #region "Variables"

    private bool mDisplayClearButton = true;
    private string mNewDialogPath = "~/CMSModules/DocumentTypes/Pages/Development/DocumentType_Edit_Transformation_Edit.aspx";
    private bool mShowHierarchicalTransformation = false;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return base.Enabled;
        }
        set
        {
            base.Enabled = value;
            if (this.uniSelector != null)
            {
                this.uniSelector.Enabled = value;
            }
        }
    }


    /// <summary>
    /// If true, control is in site manager.
    /// </summary>
    public bool IsSiteManager
    {
        get
        {
            return uniSelector.IsSiteManager;
        }
        set
        {
            uniSelector.IsSiteManager = value;
        }
    }


    /// <summary>
    /// If true selector shows hierarchical transformation.
    /// </summary>
    public bool ShowHierarchicalTransformation
    {
        get
        {
            return mShowHierarchicalTransformation;
        }
        set
        {
            mShowHierarchicalTransformation = value;
        }
    }


    /// <summary>
    /// Returns ClientID of the textbox with transformation.
    /// </summary>
    public override string ValueElementID
    {
        get
        {
            return this.uniSelector.TextBoxSelect.ClientID;
        }
    }


    /// <summary>
    /// Name of the edit window.
    /// </summary>
    public string EditWindowName
    {
        get
        {
            return uniSelector.EditWindowName;
        }
        set
        {
            uniSelector.EditWindowName = value;
        }
    }


    /// <summary>
    /// Path to the dialog for uniselector.
    /// </summary>
    public string NewDialogPath
    {
        get
        {
            return mNewDialogPath;
        }
        set
        {
            mNewDialogPath = value;
        }
    }


    /// <summary>
    /// Gets or sets the field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return this.uniSelector.Value;
        }
        set
        {
            if (uniSelector == null)
            {
                this.pnlUpdate.LoadContainer();
            }
            this.uniSelector.Value = value;
        }
    }


    /// <summary>
    /// Gets or sets the value which determines, whether to display Clear button.
    /// </summary>
    public bool DisplayClearButton
    {
        get
        {
            return this.mDisplayClearButton;
        }
        set
        {
            this.mDisplayClearButton = value;
            if (this.uniSelector != null)
            {
                this.uniSelector.AllowEmpty = value;
            }
        }
    }

    #endregion


    #region "Methods"


    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.StopProcessing)
        {
            this.uniSelector.StopProcessing = true;
        }
        else
        {
            ReloadData();
        }
    }


    /// <summary>
    /// Reloads the data in the selector.
    /// </summary>
    public void ReloadData()
    {
        this.uniSelector.IsLiveSite = this.IsLiveSite;
        this.uniSelector.ButtonClear.Visible = false;
        this.uniSelector.AllowEmpty = this.DisplayClearButton;
        this.uniSelector.SetValue("FilterMode", SettingsObjectType.TRANSFORMATION);

        // Check if user can edit the transformation
        CurrentUserInfo currentUser = CMSContext.CurrentUser;
        bool deskAuthorized = currentUser.IsAuthorizedPerUIElement("CMS.Desk", "Content");
        bool contentAuthorized = currentUser.IsAuthorizedPerUIElement("CMS.Content", new string[] { "Design", "Design.WebPartProperties" }, CMSContext.CurrentSiteName);

        if (deskAuthorized && contentAuthorized)
        {
            bool editAuthorized = currentUser.IsAuthorizedPerUIElement("CMS.Content", new string[] { "WebPartProperties.EditTransformations" }, CMSContext.CurrentSiteName);
            bool createAuthorized = currentUser.IsAuthorizedPerUIElement("CMS.Content", new string[] { "WebPartProperties.NewTransformations" }, CMSContext.CurrentSiteName);

            // Transformation editing authorized
            if (editAuthorized)
            {
                string isSiteManagerStr = IsSiteManager ? "&siteManager=true" : String.Empty;
                string url = "~/CMSModules/DocumentTypes/Pages/Development/DocumentType_Edit_Transformation_Frameset.aspx?name=##ITEMID##" + isSiteManagerStr + "&editonlycode=1";
                url = URLHelper.AddParameterToUrl(url, "hash", QueryHelper.GetHash("?editonlycode=1"));
                this.uniSelector.EditItemPageUrl = url;
            }

            // Creating of new transformation authorized
            if (createAuthorized)
            {
                string isSiteManagerStr = IsSiteManager ? "&siteManager=true" : String.Empty;
                string url = NewDialogPath + "?editonlycode=1" + isSiteManagerStr + "&selectedvalue=##ITEMID##";
                url = URLHelper.AddParameterToUrl(url, "hash", QueryHelper.GetHash("?editonlycode=1"));
                this.uniSelector.NewItemPageUrl = url;
            }
        }

        if (!ShowHierarchicalTransformation)
        {
            this.uniSelector.WhereCondition = "(TransformationIsHierarchical IS NULL) OR (TransformationIsHierarchical = 0)";
        }
    }


    /// <summary>
    /// Returns true if user control is valid.
    /// </summary>
    public override bool IsValid()
    {
        // If macro or special value, do not validate
        string value = this.uniSelector.TextBoxSelect.Text.Trim();
        if (!ContextResolver.ContainsMacro(value) && (value != string.Empty))
        {
            // Check if culture exists
            TransformationInfo ti = TransformationInfoProvider.GetTransformation(value);
            if (ti == null)
            {
                this.ValidationError = GetString("formcontrols_selecttransformation.notexist").Replace("%%code%%", value);
                return false;
            }
            else
            {
                return true;
            }
        }
        return true;
    }

    #endregion
}