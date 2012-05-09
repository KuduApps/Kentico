using System;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.FormControls;
using CMS.CMSHelper;

public partial class CMSFormControls_Classes_SelectQuery : FormEngineUserControl
{
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
    /// Returns ClientID of the textbox with query.
    /// </summary>
    public override string ValueElementID
    {
        get
        {
            return this.uniSelector.TextBoxSelect.ClientID;
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
        uniSelector.IsLiveSite = this.IsLiveSite;
        uniSelector.AllowEmpty = false;
        uniSelector.SetValue("FilterMode", SettingsObjectType.QUERY);

        // Check if user can edit the transformation
        bool editAuthorized = CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Design", "EditSQLCode");
        bool createAuthorized = editAuthorized;

        SetDialog(editAuthorized, createAuthorized);
    }


    /// <summary>
    /// Returns true if user control is valid.
    /// </summary>
    public override bool IsValid()
    {
        string value = uniSelector.TextBoxSelect.Text.Trim();

        // If macro or special value, do not validate
        if (ContextResolver.ContainsMacro(value) || value == string.Empty)
        {
            return true;
        }

        // Check if culture exists
        Query query = QueryProvider.GetQuery(value, false);
        if (query == null)
        {
            this.ValidationError = GetString("query.queryorclassnotexist").Replace("%%code%%", value);
            return false;
        }
        else
        {
            return true;
        }
    }


    /// <summary>
    /// Sets edit and new dialog URLs depending on whether the user is authorized.
    /// </summary>
    /// <param name="allowEdit">True, if edit button should be active, otherwise false</param>
    /// <param name="allowEdit">True, if new button should be active, otherwise false</param>
    private void SetDialog(bool allowEdit, bool allowNew)
    {
        string baseUrl = "~/CMSModules/DocumentTypes/Pages/Development/DocumentType_Edit_Query_Edit.aspx?editonlycode=true";

        if (allowEdit)
        {
            uniSelector.EditItemPageUrl = URLHelper.AddParameterToUrl(baseUrl, "name", "##ITEMID##"); ;
        }

        if (allowNew)
        {
            uniSelector.NewItemPageUrl = URLHelper.AddParameterToUrl(baseUrl, "selectedvalue", "##ITEMID##");
        }
    }

    #endregion
}