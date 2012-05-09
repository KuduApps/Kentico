using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Text;

using CMS.UIControls;
using CMS.CMSHelper;
using CMS.OnlineMarketing;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.DataEngine;

using Lucene.Net.Util;

public partial class CMSModules_ContactManagement_Controls_UI_Account_MergeSuggested : CMSAdminListControl
{
    #region "Variables"

    protected int mSiteId = -1;
    protected AccountInfo ai;

    /// <summary>
    /// URL of collision dialog.
    /// </summary>
    private const string MERGE_DIALOG = "~/CMSModules/ContactManagement/Pages/Tools/Account/CollisionDialog.aspx";

    #endregion


    #region "Properties"

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
            base.StopProcessing = value;
            this.gridElem.StopProcessing = value;
        }
    }


    /// <summary>
    /// Indicates if the control is used on the live site.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            return base.IsLiveSite;
        }
        set
        {
            base.IsLiveSite = value;
            gridElem.IsLiveSite = value;
            filter.IsLiveSite = value;
        }
    }


    /// <summary>
    /// Gets or sets the site id.
    /// </summary>
    public int SiteID
    {
        get
        {
            return mSiteId;
        }
        set
        {
            mSiteId = value;
        }
    }


    /// <summary>
    /// Modal dialog identificator.
    /// </summary>
    private string Identificator
    {
        get
        {
            // Try to load data from control viewstate
            string identificator = ValidationHelper.GetString(hdnIdentificator.Value, String.Empty);
            if (string.IsNullOrEmpty(identificator))
            {
                // Create new Guid
                identificator = Guid.NewGuid().ToString();
                hdnIdentificator.Value = identificator;
            }

            return identificator;
        }
    }


    /// <summary>
    /// Current account.
    /// </summary>
    public AccountInfo Account
    {
        get
        {
            if (ai == null)
            {
                if (CMSPage.EditedObject != null)
                {
                    ai = (AccountInfo)CMSPage.EditedObject;
                }
            }
            return ai;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Get edited object
        if (this.Account != null)
        {
            this.SiteID = ai.AccountSiteID;
            gridElem.OnExternalDataBound += new OnExternalDataBoundEventHandler(CMControlsHelper.UniGridOnExternalDataBound);
            gridElem.WhereCondition = filter.GetWhereCondition();
            gridElem.ZeroRowsText = GetString("om.account.noaccountsfound");
            gridElem.OnBeforeDataReload += new OnBeforeDataReload(gridElem_OnBeforeDataReload);
            btnMergeSelected.Click += new EventHandler(btnMerge_Click);
            btnMergeAll.Click += new EventHandler(btnMergeAll_Click);
            
            if (QueryHelper.GetBoolean("saved", false))
            {
                lblInfo.Visible = true;
            }
        }
        else
        {
            this.StopProcessing = true;
            this.Visible = false;
        }
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        pnlFooter.Visible = !DataHelper.DataSourceIsEmpty(gridElem.GridView.DataSource);
    }


    void gridElem_OnBeforeDataReload()
    {
        // Hide address columns
        if (!filter.AddressChecked)
        {
            gridElem.NamedColumns["address1"].Visible
                = gridElem.NamedColumns["address2"].Visible
                = gridElem.NamedColumns["city"].Visible
                = gridElem.NamedColumns["zip"].Visible = false;
        }
        // Hide web column
        gridElem.NamedColumns["website"].Visible = filter.URLChecked;

        // Hide phone & fax columns
        if (!filter.PhonesChecked)
        {
            gridElem.NamedColumns["phone"].Visible
                = gridElem.NamedColumns["fax"].Visible = false;
        }
        // Hide email column
        gridElem.NamedColumns["email"].Visible = filter.EmailChecked;

        // Hide contact columns
        if (!filter.ContactsChecked)
        {
            gridElem.NamedColumns["primarycontactfullname"].Visible
                = gridElem.NamedColumns["secondarycontactfullname"].Visible = false;
        }

        // Hide site name column
        gridElem.NamedColumns["sitename"].Visible = ((filter.SelectedSiteID < 0) && (filter.SelectedSiteID != UniSelector.US_GLOBAL_RECORD));
    }


    /// <summary>
    /// Button merge click.
    /// </summary>
    void btnMerge_Click(object sender, EventArgs e)
    {
        if (AccountHelper.AuthorizedModifyAccount(this.SiteID, true))
        {
            if (gridElem.SelectedItems.Count > 0)
            {
                SetDialogParameters(false);
                OpenWindow();
            }
            else
            {
                lblError.Visible = true;
                lblError.ResourceString = "om.account.selectaccounts";
            }
        }
    }


    /// <summary>
    /// Button merge click.
    /// </summary>
    void btnMergeAll_Click(object sender, EventArgs e)
    {
        if (AccountHelper.AuthorizedModifyAccount(this.SiteID, true))
        {
            if (!DataHelper.DataSourceIsEmpty(gridElem.GridView.DataSource))
            {
                SetDialogParameters(true);
                OpenWindow();
            }
            else
            {
                lblError.Visible = true;
                lblError.ResourceString = "om.account.noaccounts";
            }
        }
    }


    /// <summary>
    /// Sets the dialog parameters to the context.
    /// </summary>
    private void SetDialogParameters(bool mergeAll)
    {
        Hashtable parameters = new Hashtable();
        DataSet ds = null;
        if (mergeAll)
        {
            ds = new AccountListInfo().Generalized.GetData(null, gridElem.WhereCondition, null, -1, null, false);
        }
        else
        {
            string[] array = new string[gridElem.SelectedItems.Count];
            gridElem.SelectedItems.CopyTo(array);
            ds = new AccountListInfo().Generalized.GetData(null, SqlHelperClass.GetWhereCondition("AccountID", array), null, -1, null, false);
        }

        parameters["MergedAccounts"] = ds;
        parameters["ParentAccount"] = this.Account;
        parameters["issitemanager"] = ContactHelper.IsSiteManager;
        WindowHelper.Add(Identificator, parameters);
    }


    /// <summary>
    /// Registers JS for opening window.
    /// </summary>
    private void OpenWindow()
    {
        ScriptHelper.RegisterDialogScript(this.Page);
        string url = MERGE_DIALOG + "?params=" + Identificator;
        url += "&hash=" + QueryHelper.GetHash(url, false);
        StringBuilder script = new StringBuilder();
        script.Append(@"modalDialog('" + ResolveUrl(url) + @"', 'mergeDialog', 700, 700, null, null, true);");
        ScriptHelper.RegisterStartupScript(this, typeof(string), "MergeDialog" + ClientID, ScriptHelper.GetScript(script.ToString()));
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "RefreshPageScript", ScriptHelper.GetScript("function RefreshPage() { window.location.replace('" + URLHelper.AddParameterToUrl(URLHelper.CurrentURL, "saved", "true") + "'); }"));
    }

    #endregion
}