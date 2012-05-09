using System;

using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_CustomTables_Controls_CustomTableForm : CMSUserControl
{
    #region "Private variables"

    private string editItemPage = "~/CMSModules/CustomTables/Tools/CustomTable_Data_EditItem.aspx";
    private DataClassInfo dci = null;
    private string mEditItemPageAdditionalParams = null;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Custom table class id.
    /// </summary>
    public int CustomTableId
    {
        get
        {
            return customTableForm.CustomTableId;
        }
        set
        {
            customTableForm.CustomTableId = value;
        }
    }


    /// <summary>
    /// Custom table item id.
    /// </summary>
    public int ItemId
    {
        get
        {
            return customTableForm.ItemID;
        }
        set
        {
            customTableForm.ItemID = value;
        }
    }


    /// <summary>
    /// Gets or sets additional parameters for edit page.
    /// </summary>
    public string EditItemPageAdditionalParams
    {
        get
        {
            return mEditItemPageAdditionalParams;
        }
        set
        {
            mEditItemPageAdditionalParams = value;
        }
    }

    #endregion


    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (CustomTableId > 0)
        {
            // Get form info
            dci = DataClassInfoProvider.GetDataClass(CustomTableId);

            if (dci != null)
            {
                customTableForm.ShowPrivateFields = true;

                if (dci.ClassEditingPageURL != String.Empty)
                {
                    editItemPage = dci.ClassEditingPageURL;
                }

                customTableForm.OnAfterSave += customTableForm_OnAfterSave;
                customTableForm.OnBeforeSave += customTableForm_OnBeforeSave;
            }
        }
        // Show message about successful save
        if (QueryHelper.GetString("saved", String.Empty) != String.Empty)
        {
            lblInfo.Text = GetString("general.changessaved");
            lblInfo.Visible = true;
        }
    }


    private void customTableForm_OnBeforeSave(object sender, EventArgs e)
    {
        // If editing item
        if (ItemId > 0)
        {
            // Check 'Modify' permission
            if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.customtables", "Modify") &&
                !CMSContext.CurrentUser.IsAuthorizedPerClassName(dci.ClassName, "Modify"))
            {
                // Show error message
                lblError.Visible = true;
                lblError.Text = String.Format(GetString("customtable.permissiondenied.modify"), dci.ClassName);
                // Hide 'successful' message
                lblInfo.Visible = false;
                customTableForm.StopProcessing = true;
            }
        }
        else
        {
            // Check 'Create' permission
            if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.customtables", "Create") &&
                !CMSContext.CurrentUser.IsAuthorizedPerClassName(dci.ClassName, "Create"))
            {
                // Show error message
                lblError.Visible = true;
                lblError.Text = String.Format(GetString("customtable.permissiondenied.create"), dci.ClassName);
                // Hide 'successful' message
                lblInfo.Visible = false;
                customTableForm.StopProcessing = true;
            }
        }
    }


    private void customTableForm_OnAfterSave(object sender, EventArgs e)
    {
        string param = "&saved=1";

        // Include additional parameters if any
        param += (String.IsNullOrEmpty(this.EditItemPageAdditionalParams) ? String.Empty : "&" + this.EditItemPageAdditionalParams);

        // Reflect new in query string
        param += (QueryHelper.GetString("new", String.Empty) != String.Empty) ? "&new=1" : String.Empty;

        //Redirect to edit page with saved parameter and new itemId (new item)
        URLHelper.Redirect(URLHelper.GetAbsoluteUrl(editItemPage) + "?customtableid=" + CustomTableId + "&itemid=" + customTableForm.ItemID + param);
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        if (customTableForm.BasicForm != null)
        {
            customTableForm.BasicForm.SubmitButton.CssClass = "SubmitButton";
        }
    }
}
