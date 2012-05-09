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
using CMS.CMSHelper;
using CMS.SettingsProvider;

public partial class CMSModules_Categories_Controls_CategoryEdit : CMSAdminEditControl
{
    #region "Variables"

    private int mCategoryId = 0;
    private int mParentCategoryId = 0;
    private CategoryInfo mParentCategory = null;
    private CategoryInfo mCategory = null;
    private int mUserID = 0;
    private UserInfo mUser = null;
    private int mSiteId = -1;
    private bool mShowEnabled = true;
    private bool mShowCodeName = true;
    private string mRefreshPageURL = null;
    private bool mAllowCreateOnlyGlobal = false;
    private bool? mAllowGlobalCategories = null;
    private bool canModifySite = false;
    private bool canModifyGlobal = false;
    private bool mDisplayOkButton = true;
    private bool mWasSaved = false;
    private bool mAllowDisabledParents = false;
    
    #endregion


    #region "Public properties"

    /// <summary>
    /// Current category ID.
    /// </summary>
    public int CategoryID
    {
        get
        {
            return mCategoryId;
        }
        set
        {
            mCategoryId = value;
            mCategory = null;
            mParentCategory = null;
            mParentCategoryId = 0;
        }
    }


    /// <summary>
    /// Edited category object.
    /// </summary>
    public CategoryInfo Category
    {
        get
        {
            if (mCategory == null)
            {
                mCategory = CategoryInfoProvider.GetCategoryInfo(mCategoryId);
            }

            return mCategory;
        }
        set
        {
            mCategory = value;
            mCategoryId = 0;
            mParentCategoryId = 0;
            mParentCategory = null;

            if (value != null)
            {
                mCategoryId = value.CategoryID;
                mParentCategoryId = value.CategoryParentID;
            }
        }
    }


    /// <summary>
    /// Parent category ID for creating new category. Applies only when CategoryID is 0.
    /// </summary>
    public int ParentCategoryID
    {
        get
        {
            return mParentCategoryId;
        }
        set
        {
            mParentCategoryId = value;
            mParentCategory = null;
        }
    }


    /// <summary>
    /// Parent category ID for creating new category. Applies only when CategoryID is 0.
    /// </summary>
    public CategoryInfo ParentCategory
    {
        get
        {
            if (mParentCategory == null)
            {
                mParentCategory = CategoryInfoProvider.GetCategoryInfo(mParentCategoryId);
            }

            return mParentCategory;
        }
        set
        {
            mParentCategory = value;

            mParentCategoryId = 0;
            if (value != null)
            {
                mParentCategoryId = value.CategoryID;
            }
        }
    }


    /// <summary>
    /// ID of the site to create categories for.
    /// </summary>
    public int SiteID
    {
        get
        {
            if (mSiteId < 0)
            {
                mSiteId = CMSContext.CurrentSiteID;
            }

            return mSiteId;
        }
        set
        {
            mSiteId = value;

            mAllowGlobalCategories = null;
        }
    }


    /// <summary>
    /// ID of the user to create/edit category for.
    /// </summary>
    public int UserID
    {
        get
        {
            return this.mUserID;
        }
        set
        {
            this.mUserID = value;
            this.mUser = null;
        }
    }


    /// <summary>
    /// User object to create/edit category for.
    /// </summary>
    public UserInfo User
    {
        get
        {
            if (mUser == null)
            {
                mUser = UserInfoProvider.GetUserInfo(UserID);
            }

            return mUser;
        }
        set
        {
            mUser = value;
            mUserID = 0;

            if (value != null)
            {
                mUserID = value.UserID;
            }
        }
    }


    /// <summary>
    /// Indicates if global categories are created by default under global parent category.
    /// </summary>
    public bool AllowCreateOnlyGlobal
    {
        get
        {
            return mAllowCreateOnlyGlobal;
        }
        set
        {
            mAllowCreateOnlyGlobal = value;
        }
    }


    /// <summary>
    /// Show enabled checkbox.
    /// </summary>
    public bool ShowEnabled
    {
        get
        {
            return this.mShowEnabled;
        }
        set
        {
            this.mShowEnabled = value;
        }
    }


    /// <summary>
    /// Show code name textbox.
    /// </summary>
    public bool ShowCodeName
    {
        get
        {
            return this.mShowCodeName;
        }
        set
        {
            this.mShowCodeName = value;
        }
    }


    /// <summary>
    /// The URL of the page where the result is redirected.
    /// </summary>
    public string RefreshPageURL
    {
        get
        {
            return this.mRefreshPageURL;
        }
        set
        {
            this.mRefreshPageURL = value;
        }
    }


    /// <summary>
    /// Indicates whether the category changes were saved.
    /// </summary>
    public bool WasSaved
    {
        get
        {
            return mWasSaved || !string.IsNullOrEmpty(QueryHelper.GetString("saved", string.Empty));
        }
        set
        {
            mWasSaved = value;
        }
    }


    /// <summary>
    /// Indicates if the control should perform the operations.
    /// </summary>
    public override bool StopProcessing
    {
        get
        {
            return base.StopProcessing;
        }
        set
        {
            EnsureChildControls();
            base.StopProcessing = value;

            selectParentCategory.StopProcessing = value;
        }
    }


    /// <summary>
    /// Indicates if OK button will be shown.
    /// </summary>
    public bool DisplayOkButton
    {
        get
        {
            return mDisplayOkButton;
        }
        set
        {
            mDisplayOkButton = value;
        }
    }


    /// <summary>
    /// Indicates if global categories are allowed even when disabled by settings.
    /// </summary>
    public bool AllowGlobalCategories
    {
        get
        {
            if (!mAllowGlobalCategories.HasValue)
            {
                // Get site name
                string siteName = SiteInfoProvider.GetSiteName(SiteID);

                // Figure out from settings
                mAllowGlobalCategories = SettingsKeyProvider.GetBoolValue(siteName + ".CMSAllowGlobalCategories");
            }

            return mAllowGlobalCategories ?? false;
        }
        set
        {
            mAllowGlobalCategories = value;
        }
    }


    /// <summary>
    /// Indicates if disabled categories are allowed in parent category selector. Default value is false.
    /// </summary>
    public bool AllowDisabledParents
    {
        get
        {
            return mAllowDisabledParents;
        }
        set
        {
            mAllowDisabledParents = value;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize the controls
        SetupControl();

        if (!StopProcessing && !RequestHelper.IsPostBack())
        {
            LoadData();
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (this.WasSaved)
        {
            // Display information on success
            this.lblInfo.Text = GetString("general.changessaved");
            this.lblInfo.Visible = true;
        }
    }

    #endregion


    #region "Event handling"

    public void btnSaveCategory_Click(object sender, EventArgs e)
    {
        // Validate form entries
        string errorMessage = "";

        // Check "modify" permission
        if (!this.RaiseOnCheckPermissions("Modify", this))
        {
            if ((this.UserID > 0) && (this.UserID != CMSContext.CurrentUser.UserID) && (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Users", "Modify")))
            {
                string.Format(GetString("general.permissionresource"), "Modify", "CMS.Users");
            }
        }

        // Prepare ID of the site for new category
        int newSiteId = SiteID;
        if (canModifyGlobal && (AllowCreateOnlyGlobal || radGlobal.Checked))
        {
            // Create global categories only under global categories or root
            if ((ParentCategory == null) || (ParentCategory.CategoryIsGlobal && !ParentCategory.CategoryIsPersonal))
            {
                newSiteId = 0;
            }
        }

        bool personal = false;
        bool global = false;

        if (Category != null)
        {
            // Prepare personal and global flag for existing category
            personal = Category.CategoryIsPersonal;
            global = Category.CategoryIsGlobal;
        }
        else
        {
            // Prepare personal and global flag for new category
            personal = UserID > 0;
            global = newSiteId == 0;
        }

        errorMessage = ValidateForm(personal, global);

        if (errorMessage == "")
        {
            // Need Modify or GlobalModify permission to edit non-personal categories
            string permission = global ? "GlobalModify" : "Modify";
            if (!personal && !CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Categories", permission))
            {
                errorMessage = string.Format(GetString("general.permissionresource"), permission, "CMS.Categories");
            }
        }


        if (errorMessage == "")
        {
            try
            {
                CategoryInfo category = null;
                // Update existing item
                if (Category != null)
                {
                    category = Category;

                    if (!category.CategoryIsPersonal)
                    {
                        CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Categories", category.CategoryIsGlobal ? "GlobalModify" : "Modify");

                        // Update code name only if not personal category
                        category.CategoryName = GetCodeName();
                    }

                    // Update parent category ID
                    category.CategoryParentID = this.selectParentCategory.CategoryID;

                    EditedObject = category;
                }
                else
                {
                    // Create a new category info
                    category = new CategoryInfo();

                    // Initialize the default count when creating new category
                    category.CategoryCount = 0;
                    category.CategoryParentID = ParentCategoryID;
                    category.CategoryName = GetCodeName();

                    if (UserID > 0)
                    {
                        category.CategoryUserID = UserID;
                    }
                    else
                    {
                        category.CategorySiteID = newSiteId;
                    }
                }

                if (category != null)
                {
                    // Update category fields
                    txtDisplayName.Save();
                    category.CategoryDisplayName = this.txtDisplayName.Text.Trim();
                    category.CategoryDescription = this.txtDescription.Text;
                    category.CategoryEnabled = this.chkEnabled.Checked;

                    // Save category in the database
                    CategoryInfoProvider.SetCategoryInfo(category);
                    Category = category;

                    if (!string.IsNullOrEmpty(this.RefreshPageURL))
                    {
                        URLHelper.Redirect(this.RefreshPageURL + "?categoryid=" + Category.CategoryID.ToString() + "&saved=1");
                    }

                    RaiseOnSaved();

                    // Display information on success
                    this.lblInfo.Text = GetString("general.changessaved");
                    this.lblInfo.Visible = true;
                    this.lblError.Visible = false;
                }
            }
            catch (Exception ex)
            {
                // Display error message
                this.lblError.Text = GetString("general.erroroccurred") + " " + ex.Message;
                this.lblError.Visible = true;
            }
        }
        else
        {
            // Display error message
            this.lblError.Text = errorMessage;
            this.lblError.Visible = true;
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Reloads the category data in the control.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        SetupControl();
        LoadData();
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Initializes the contol.
    /// </summary>
    private void SetupControl()
    {
        // Get and store permissions
        canModifySite = CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Categories", "Modify");
        canModifyGlobal = CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Categories", "GlobalModify");
        canModifyGlobal &= AllowGlobalCategories;

        // Intialize the control labels and error messages
        this.rfvDisplayName.ErrorMessage = GetString("general.requiresdisplayname");
        this.rfvCodeName.ErrorMessage = GetString("general.requirescodename");
        this.btnSaveCategory.Text = GetString("general.ok");
        this.radGlobal.Text = GetString("category_edit.globalcategory");
        this.radSite.Text = GetString("category_edit.sitecategory");
        this.selectParentCategory.AllowDisabledCategories = this.AllowDisabledParents;

        this.btnSaveCategory.Visible = DisplayOkButton;

        txtDisplayName.IsLiveSite = txtDescription.IsLiveSite = IsLiveSite;

        // Handle show enabled checkbox
        if (!this.ShowEnabled)
        {
            this.lblEnabled.Visible = false;
            this.chkEnabled.Visible = false;
            this.chkEnabled.Checked = true;
        }

        // Handle show code name
        if (!this.ShowCodeName)
        {
            this.lblCodeName.Visible = false;
            this.txtCodeName.Visible = false;
            this.rfvCodeName.Visible = false;
            if (this.CategoryID <= 0)
            {
                this.txtCodeName.Text = Guid.NewGuid().ToString();
            }
        }
    }


    /// <summary>
    /// Loads the data.
    /// </summary>
    private void LoadData()
    {
        // Handle existing category editing - prepare the data
        if (this.CategoryID > 0)
        {
            // Perform actions when editing existing category
            HandleExistingCategory();
        }
        else
        {
            // Clear textboxes
            this.txtCodeName.Text = "";
            this.txtDescription.Text = "";
            this.txtDisplayName.Text = "";
            this.selectParentCategory.SiteID = SiteID;
            this.selectParentCategory.UserID = UserID;
            this.selectParentCategory.CategoryID = ParentCategoryID;
            this.plcParentCategory.Visible = false;
            //this.selectParentCategory.Enabled = false;

            bool personal = UserID > 0;

            // Display type selector only if there is a choice (according to permissions and settings) 
            bool canChooseType = canModifyGlobal && canModifySite;

            if (ParentCategory != null)
            {
                plcCategorySite.Visible = canChooseType && ParentCategory.CategoryIsGlobal && !ParentCategory.CategoryIsPersonal && !AllowCreateOnlyGlobal;
            }
            else
            {
                plcCategorySite.Visible = canChooseType && !personal && !AllowCreateOnlyGlobal;
            }

            // Select site type when can not create global category
            if (!canModifySite)
            {
                radGlobal.Checked = true;
                radSite.Checked = false;
            }

            // Select global type when can not create site category
            if (!canModifyGlobal)
            {
                radGlobal.Checked = false;
                radSite.Checked = true;
            }

            plcEnabled.Visible = !personal;
            plcCodeName.Visible = !personal;
        }

        this.selectParentCategory.ReloadData();
    }


    /// <summary>
    /// Handles actions related to the existing category editing.
    /// </summary>
    private void HandleExistingCategory()
    {
        // Get information on current category
        EditedObject = Category;
        if (Category != null)
        {
            // Pre-fill the data of current category
            this.txtDisplayName.Text = Category.CategoryDisplayName;
            this.txtCodeName.Text = Category.CategoryName;
            this.txtDescription.Text = Category.CategoryDescription;
            this.chkEnabled.Checked = Category.CategoryEnabled;

            this.selectParentCategory.UserID = Category.CategoryUserID;

            this.selectParentCategory.SiteID = Category.CategorySiteID;

            this.selectParentCategory.CategoryID = Category.CategoryParentID;
            this.selectParentCategory.ExcludeCategoryID = Category.CategoryID;
            this.selectParentCategory.DisableSiteCategories = Category.IsGlobal;
            this.plcParentCategory.Visible = true;
            this.selectParentCategory.Enabled = true;
            plcCategorySite.Visible = false;

            bool personal = Category.CategoryIsPersonal;

            plcEnabled.Visible = !personal;
            plcCodeName.Visible = !personal;
        }
    }


    /// <summary>
    /// Returns true if given codename is unique, otherwise false.
    /// </summary>
    /// <param name="newCodeName">Code name of the category.</param>
    /// <param name="isPersonal">Indicates if category is personal.</param>
    /// <param name="isGlobal">Indicates if category is global.</param>
    private bool CodeNameIsUnique(string newCodeName, bool isPersonal, bool isGlobal)
    {
        string where = "CategoryName = N'" + SqlHelperClass.GetSafeQueryString(newCodeName, false) + "'";

        // Check if site category
        if (!isPersonal && !isGlobal)
        {
            // Look for category in global, personal and selected site categories
            where = SqlHelperClass.AddWhereCondition(where, "CategorySiteID = " + SiteID + " OR CategorySiteID IS NULL");
        }

        // Get existing category
        DataSet ds = CategoryInfoProvider.GetCategories(where, "CategoryID", 1, "CategoryID");
        if (SqlHelperClass.DataSourceIsEmpty(ds))
        {
            return true;
        }
        else
        {
            int id = ValidationHelper.GetInteger(ds.Tables[0].Rows[0]["CategoryID"], 0);

            // If the existing category is updated the code name already exists
            return (id > 0) && (this.CategoryID == id);
        }
    }


    /// <summary>
    /// Validates the form entries. Returns empty string if validation passed otherwise error message is returned.
    /// </summary>
    /// <param name="isPersonal">Indicates if category is personal.</param>
    /// <param name="isGlobal">Indicates if category is global.</param>
    private string ValidateForm(bool isPersonal, bool isGlobal)
    {
        string codeName = GetCodeName();

        string errorMessage = new Validator()
            .NotEmpty(codeName, GetString("general.requirescodename"))
            .NotEmpty(txtDisplayName.Text.Trim(), GetString("general.requiresdisplayname")).Result;

        if (string.IsNullOrEmpty(errorMessage) && !ValidationHelper.IsCodeName(codeName))
        {
            errorMessage = GetString("General.ErrorCodeNameInIdentificatorFormat");
        }

        if (string.IsNullOrEmpty(errorMessage) && (txtDisplayName.Text.IndexOf('/') >= 0))
        {
            errorMessage = GetString("category_edit.SlashNotAllowedInDisplayName");
        }

        if (string.IsNullOrEmpty(errorMessage))
        {
            // Validate display name & code name entry
            this.rfvDisplayName.Validate();
            this.rfvCodeName.Validate();

            if ((this.rfvDisplayName.IsValid) && (this.rfvCodeName.IsValid))
            {
                // Validate code name entry
                if (!CodeNameIsUnique(codeName, isPersonal, isGlobal))
                {
                    errorMessage = GetString("general.codenameexists");

                    // Display validation error message
                    this.lblError.Text = errorMessage;
                    this.lblError.Visible = true;
                }
            }
            else
            {
                errorMessage = this.rfvCodeName.ErrorMessage;
            }
        }

        return errorMessage;
    }


    /// <summary>
    /// Generates code name for currently created/edited category.
    /// </summary>
    private string GetCodeName()
    {
        if (Category != null)
        {
            // Use text box value when editing non-personal category
            if (!Category.CategoryIsPersonal)
            {
                return this.txtCodeName.Text.Trim();
            }

            // Personal categories can not change codename
            return Category.CategoryName;
        }
        else
        {
            if (UserID > 0)
            {
                if (User != null)
                {
                    // Generate codename for new personal category
                    string codeName = ValidationHelper.GetCodeName(User.UserName + "_" + this.txtDisplayName.Text.Trim());
                    return TextHelper.LimitLength(codeName, 250, "", false);
                }
            }

            // Use text box value when creating non-personal category
            return this.txtCodeName.Text.Trim();
        }
    }

    #endregion
}
