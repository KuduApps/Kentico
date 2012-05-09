using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Text;

using CMS.UIControls;
using CMS.OnlineMarketing;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.GlobalHelper;

public partial class CMSModules_ContactManagement_Controls_UI_Account_MergeChoose : CMSAdminListControl
{
    #region "Variables and constants"

    /// <summary>
    /// URL of collision dialog.
    /// </summary>
    private const string MERGE_DIALOG = "~/CMSModules/ContactManagement/Pages/Tools/Account/CollisionDialog.aspx";

    private AccountInfo ai = null;

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
    /// Current contact.
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

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Account != null)
        {
            // Current account is global object
            if (this.Account.AccountSiteID == 0)
            {
                filter.SiteID = CMSContext.CurrentSiteID;
                // Display site selector in site manager
                if (ContactHelper.IsSiteManager)
                {
                    filter.SiteID = UniSelector.US_GLOBAL_RECORD;
                    filter.DisplaySiteSelector = true;
                }
                // Display 'site or global' selector in CMS desk for global objects
                else if (AccountHelper.AuthorizedModifyAccount(CMSContext.CurrentSiteID, false) && AccountHelper.AuthorizedReadAccount(CMSContext.CurrentSiteID, false))
                {
                    filter.DisplayGlobalOrSiteSelector = true;
                }
                filter.HideMergedIntoGlobal = true;
            }
            else
            {
                filter.SiteID = Account.AccountSiteID;
            }
            filter.ShowGlobalStatuses =
                ConfigurationHelper.AuthorizedReadConfiguration(UniSelector.US_GLOBAL_RECORD, false) &&
                (SettingsKeyProvider.GetBoolValue(CMSContext.CurrentSiteName + ".CMSCMGlobalConfiguration") || ContactHelper.IsSiteManager);
            gridElem.OnExternalDataBound += new OnExternalDataBoundEventHandler(CMControlsHelper.UniGridOnExternalDataBound);
            gridElem.WhereCondition = filter.WhereCondition;
            gridElem.WhereCondition = SqlHelperClass.AddWhereCondition(gridElem.WhereCondition, "AccountID <> " + this.Account.AccountID);
            gridElem.ZeroRowsText = GetString("om.account.noaccountsfound");

            btnMergeSelected.Click += new EventHandler(btnMergeSelected_Click);
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


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        pnlButton.Visible = !DataHelper.DataSourceIsEmpty(gridElem.GridView.DataSource);
        gridElem.NamedColumns["sitename"].Visible = ((filter.SelectedSiteID < 0) && (filter.SelectedSiteID != UniSelector.US_GLOBAL_RECORD));
    }


    /// <summary>
    /// Button merge all click
    /// </summary>
    void btnMergeAll_Click(object sender, EventArgs e)
    {
        if (AccountHelper.AuthorizedModifyAccount(this.Account.AccountSiteID, true))
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
    /// Button merge click.
    /// </summary>
    void btnMergeSelected_Click(object sender, EventArgs e)
    {
        if (AccountHelper.AuthorizedModifyAccount(this.Account.AccountSiteID, true))
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
    /// Sets the dialog parameters to the context.
    /// </summary>
    private void SetDialogParameters(bool allData)
    {
        Hashtable parameters = new Hashtable();
        DataSet ds = null;
        if (allData)
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