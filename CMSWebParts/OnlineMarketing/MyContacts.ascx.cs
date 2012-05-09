using System;
using System.Web.UI.WebControls;
using System.Text;

using CMS.GlobalHelper;
using CMS.PortalControls;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSWebParts_OnlineMarketing_MyContacts : CMSAbstractWebPart
{
    #region "Constants"

    /// <summary>
    /// URL of modal dialog window for contact editing.
    /// </summary>
    public const string CONTACT_DETAIL_DIALOG = "~/CMSModules/ContactManagement/Pages/Tools/Account/Contact_Detail.aspx";

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets list of visible fields (columns).
    /// </summary>
    public string VisibleFields
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("VisibleFields"), "");
        }
        set
        {
            this.SetValue("VisibleFields", value);
        }
    }


    /// <summary>
    /// Gets or sets page size.
    /// </summary>
    public int PageSize
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("PageSize"), 0);
        }
        set
        {
            this.SetValue("PageSize", value);
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    protected void SetupControl()
    {
        // Check permissions
        CurrentUserInfo user = CMSContext.CurrentUser;
        bool siteContactsAllowed = user.IsAuthorizedPerResource("CMS.ContactManagement", "ReadContacts");
        bool globalContactsAllowed = user.IsAuthorizedPerResource("CMS.ContactManagement", "ReadGlobalContacts") && SettingsKeyProvider.GetBoolValue(CMSContext.CurrentSiteName + ".CMSCMGlobalContacts");
        if (!siteContactsAllowed && !globalContactsAllowed)
        {
            lblInfo.Visible = true;
            lblInfo.Text = GetString("om.myaccounts.notallowedtoreadcontacts");
            return;
        }

        // Create additional restriction if only site or global objects are allowed
        string where = null;
        if (!globalContactsAllowed)
        {
            where = SqlHelperClass.AddWhereCondition(where, "ContactSiteID IS NOT NULL");
        }
        if (!siteContactsAllowed)
        {
            where = SqlHelperClass.AddWhereCondition(where, "ContactSiteID IS NULL");
        }

        // Display accounts on current site or global site (if one of those shouldn't be displayed, it's filtered above)
        where = SqlHelperClass.AddWhereCondition(where, "ContactSiteID = " + CMSContext.CurrentSiteID + " OR ContactSiteID IS NULL");

        gridElem.Visible = true;
        gridElem.WhereCondition = SqlHelperClass.AddWhereCondition("ContactOwnerUserID=" + user.UserID + " AND ContactMergedWithContactID IS NULL", where);
        gridElem.OnExternalDataBound += new OnExternalDataBoundEventHandler(gridElem_OnExternalDataBound);
        gridElem.Pager.DefaultPageSize = PageSize;
        RegisterScripts();
        SetVisibleColumns();
    }


    /// <summary>
    /// Hide unwanted columns.
    /// </summary>
    protected void SetVisibleColumns()
    {
        string visibleCols = "|" + VisibleFields.Trim('|') + "|";
        string colName = null;
        // Hide unwanted columns
        for (int i = gridElem.GridColumns.Columns.Count - 1; i >= 0; i--)
        {
            if (!String.IsNullOrEmpty(colName = gridElem.GridColumns.Columns[i].Name))
            {
                if (visibleCols.IndexOf("|" + colName + "|", StringComparison.Ordinal) == -1)
                {
                    gridElem.GridColumns.Columns[i].Visible = false;
                    gridElem.GridColumns.Columns[i].Filter = null;
                }
                else
                {
                    gridElem.GridColumns.Columns[i].Visible = true;
                }
            }
        }
    }


    protected object gridElem_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "edit":
                ImageButton btn = ((ImageButton)sender);
                btn.Attributes.Add("onClick", "EditContact(" + btn.CommandArgument + "); return false;");
                break;
            case "website":
                string url = ValidationHelper.GetString(parameter, null);
                if (url != null)
                {
                    return "<a target=\"_blank\" href=\"" + url + "\" \">" + HTMLHelper.HTMLEncode(url) + "</a>";
                }
                else
                {
                    return GetString("general.na");
                }
        }
        return parameter;
    }

    /// <summary>
    /// Registers JS.
    /// </summary>
    private void RegisterScripts()
    {
        ScriptHelper.RegisterDialogScript(this.Page);
        StringBuilder script = new StringBuilder();

        // Register script to open dialogs for contact editing
        script.Append(@" function EditContact(contactID) {  modalDialog('" + ResolveUrl(CONTACT_DETAIL_DIALOG) + @"?contactid=' + contactID, 'ContactDetail', '1061px', '700px'); }");

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "MyContactActions", ScriptHelper.GetScript(script.ToString()));
    }

    #endregion
}
