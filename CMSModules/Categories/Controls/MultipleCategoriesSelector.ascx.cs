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
using System.Text;

using CMS.SiteProvider;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.DataEngine;
using CMS.TreeEngine;
using CMS.Controls;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_Categories_Controls_MultipleCategoriesSelector : CMSAdminEditControl
{
    #region "Variables"

    private bool mSelectOnlyEnabled = true;
    private int mDocumentID = 0;
    private bool mFormControlMode = false;
    private int mUserID = 0;
    private bool mEnabled = true;
    private bool isSaved = false;
    private bool mDisplaySavedMessage = true;

    private string mCurrentValues = "";

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets the DocumentID for which the categories should be loaded.
    /// </summary>
    public int DocumentID
    {
        get
        {
            return this.mDocumentID;
        }
        set
        {
            this.mDocumentID = value;
        }
    }


    /// <summary>
    /// Gets or sets the UserID whose categories should be displayed.
    /// </summary>
    public int UserID
    {
        get
        {
            if (this.mUserID > 0)
            {
                return this.mUserID;
            }
            else
            {
                return CMSContext.CurrentUser.UserID;
            }
        }
        set
        {
            this.mUserID = value;
        }
    }


    /// <summary>
    /// Gets or sets the value which determines whether to display only enabled categories.
    /// </summary>
    public bool SelectOnlyEnabled
    {
        get
        {
            return this.mSelectOnlyEnabled;
        }
        set
        {
            this.mSelectOnlyEnabled = value;
        }
    }


    /// <summary>
    /// Gets or sets the value which determines if form control mode is On or not.
    /// </summary>
    public bool FormControlMode
    {
        get
        {
            return this.mFormControlMode;
        }
        set
        {
            this.mFormControlMode = value;
        }
    }


    public UniSelector UniSelector
    {
        get
        {
            return this.selectCategory;
        }
    }


    /// <summary>
    /// Enabled state of the control.
    /// </summary>
    public bool Enabled
    {
        get
        {
            return mEnabled;
        }
        set
        {
            mEnabled = value;
        }
    }


    /// <summary>
    /// Indicates if control has to display its own 'The changes were saved' message.
    /// </summary>
    public bool DisplaySavedMessage
    {
        get
        {
            return mDisplaySavedMessage;
        }
        set
        {
            mDisplaySavedMessage = value;
        }
    }


    /// <summary>
    /// String of categories IDs.
    /// </summary>
    public string Value
    {
        get
        {
            string values = selectCategory.Value as string;

            if (!string.IsNullOrEmpty(values))
            {
                // Return categories
                return values.Replace(selectCategory.ValuesSeparator, ",");
            }

            return "";
        }
        set
        {
            string values = value ?? "";
            selectCategory.Value = values.Replace(",", selectCategory.ValuesSeparator);
        }
    }

    #endregion


    #region "Events"

    /// <summary>
    /// Occurs after data is saved to the database.
    /// </summary>
    public delegate void OnAfterSaveEventHandler();

    /// <summary>
    /// OnAfterSave event.
    /// </summary>
    public event OnAfterSaveEventHandler OnAfterSave;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.StopProcessing)
        {
            // Propagate options
            selectCategory.IsLiveSite = this.IsLiveSite;
            selectCategory.Enabled = Enabled;
            selectCategory.GridName = "~/CMSModules/Categories/Controls/Categories.xml";
            selectCategory.OnAdditionalDataBound += new CMSAdminControls_UI_UniSelector_UniSelector.AdditionalDataBoundEventHandler(selectCategory_OnAdditionalDataBound);
            selectCategory.UniGrid.OnAfterRetrieveData += new OnAfterRetrieveData(UniGrid_OnAfterRetrieveData);
            selectCategory.ItemsPerPage = 25;

            // Select appropriate dialog window
            if (this.IsLiveSite)
            {
                selectCategory.SelectItemPageUrl = "~/CMSModules/Categories/CMSPages/LiveCategorySelection.aspx";
            }
            else
            {
                selectCategory.SelectItemPageUrl = "~/CMSModules/Categories/Dialogs/CategorySelection.aspx";
            }

            // Prepare selected values
            DataSet ds = CategoryInfoProvider.GetDocumentCategories(DocumentID, null, null);
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                mCurrentValues = TextHelper.Join(";", SqlHelperClass.GetStringValues(ds.Tables[0], "CategoryID"));
            }

            if (!RequestHelper.IsPostBack())
            {
                selectCategory.Value = mCurrentValues;
            }

            isSaved = QueryHelper.GetBoolean("saved", false);
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (DisplaySavedMessage && !this.FormControlMode && isSaved)
        {
            // Changes saved
            lblInfo.Text = GetString("General.ChangesSaved");
            pnlInfo.Visible = true;
        }
    }

    #endregion


    #region "Methods"

    protected object selectCategory_OnAdditionalDataBound(object sender, string sourceName, object parameter, string value)
    {
        switch (sourceName.ToLowerInvariant())
        {
            // Resolve category name
            case "name":
                string namePath = parameter as string;
                if (!string.IsNullOrEmpty(namePath))
                {
                    namePath = namePath.TrimStart('/');
                    namePath = namePath.Replace("/", "&nbsp;&gt;&nbsp;");
                    value = HTMLHelper.HTMLEncode(ResHelper.LocalizeString(namePath));
                }

                break;
        }

        return value;
    }


    DataSet UniGrid_OnAfterRetrieveData(DataSet ds)
    {
        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            DataTable table = ds.Tables[0];
            Hashtable toDelete = new Hashtable();

            // Remove categories having child in the table
            foreach (DataRow dr in table.Rows)
            {
                string namePath = ValidationHelper.GetString(dr["CategoryNamePath"], "");
                int id = ValidationHelper.GetInteger(dr["CategoryID"], 0);
                
                // Check if table contains any child
                foreach (DataRow drChild in table.Rows)
                {
                    string childNamePath = ValidationHelper.GetString(drChild["CategoryNamePath"], "");
                    if (!toDelete.Contains(id) && childNamePath.StartsWith(namePath + "/"))
                    {
                        // Place parent on the black list
                        toDelete.Add(id, dr);

                        break;
                    }
                }
            }

            // Remove categories from blacklist
            foreach (DataRow row in toDelete.Values)
            {
                row.Delete();
            }

            // Accept changes
            ds.AcceptChanges();
        }

        return ds;
    }


    /// <summary>
    /// Saves the values.
    /// </summary>
    public void Save()
    {
        if (!RaiseOnCheckPermissions(CMSAdminControl.PERMISSION_MODIFY, this))
        {
            CurrentUserInfo cui = CMSContext.CurrentUser;
            if ((cui == null) || ((this.UserID != cui.UserID) && !cui.IsAuthorizedPerResource("CMS.Users", CMSAdminControl.PERMISSION_MODIFY)))
            {
                RedirectToAccessDenied("CMS.Users", CMSAdminControl.PERMISSION_MODIFY);
            }
        }

        // Remove old items
        string newValues = ValidationHelper.GetString(selectCategory.Value, null);
        string items = DataHelper.GetNewItemsInList(newValues, mCurrentValues);
        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                // Add all new items to user
                foreach (string item in newItems)
                {
                    int categoryId = ValidationHelper.GetInteger(item, 0);
                    DocumentCategoryInfoProvider.RemoveDocumentFromCategory(this.mDocumentID, categoryId);
                }
            }
        }

        // Add new items
        items = DataHelper.GetNewItemsInList(mCurrentValues, newValues);
        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                // Add all new items to user
                foreach (string item in newItems)
                {
                    int categoryId = ValidationHelper.GetInteger(item, 0);

                    // Make sure, that category still exists
                    if (CategoryInfoProvider.GetCategoryInfo(categoryId) != null)
                    {
                        DocumentCategoryInfoProvider.AddDocumentToCategory(this.mDocumentID, categoryId);
                    }
                }
            }
        }

        // Raise on after save
        if (OnAfterSave != null)
        {
            OnAfterSave();
        }

        if (!this.FormControlMode)
        {
            // Refresh page
            URLHelper.Redirect(URLHelper.AddParameterToUrl(URLHelper.CurrentURL, "saved", "1"));
        }

        isSaved = true;
    }

    #endregion
}
