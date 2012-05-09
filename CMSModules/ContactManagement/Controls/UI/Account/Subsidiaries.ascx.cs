using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.DataEngine;
using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.OnlineMarketing;
using CMS.SettingsProvider;
using CMS.UIControls;

public partial class CMSModules_ContactManagement_Controls_UI_Account_Subsidiaries : CMSAdminListControl
{
    #region "Variables"

    protected AccountInfo ai = null;
    protected int mSiteId = -1;
    protected bool? mModifyAccountPermission;


    /// <summary>
    /// Available actions in mass action selector.
    /// </summary>
    protected enum Action
    {
        SelectAction = 0,
        Remove = 1
    }


    /// <summary>
    /// Selected objects in mass action selector.
    /// </summary>
    protected enum What
    {
        Selected = 0,
        All = 1
    }


    /// <summary>
    /// URL of modal dialog window for account editing.
    /// </summary>
    public const string ACCOUNT_DETAIL_DIALOG = "~/CMSModules/ContactManagement/Pages/Tools/Contact/Account_Detail.aspx";

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets permission for modifying an account.
    /// </summary>
    protected bool ModifyAccountPermission
    {
        get
        {
            return (bool)(mModifyAccountPermission ?? 
                (mModifyAccountPermission = AccountHelper.AuthorizedModifyAccount(this.SiteID, false)));
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
            base.StopProcessing = value;
            accountSelector.StopProcessing = value;
            filter.StopProcessing = value;
            gridElem.StopProcessing = value;
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
            accountSelector.IsLiveSite = value;
            filter.IsLiveSite = value;
            gridElem.IsLiveSite = value;
        }
    }


    /// <summary>
    /// SiteID of current account.
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
            filter.SiteID = value;
            accountSelector.SiteID = value;
        }
    }

    #endregion


    #region "Page events and methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (CMSContext.EditedObject != null)
        {
            ai = (AccountInfo)CMSContext.EditedObject;
            this.SiteID = ai.AccountSiteID;

            // Setup unigrid
            gridElem.OnExternalDataBound += new OnExternalDataBoundEventHandler(gridElem_OnExternalDataBound);
            gridElem.OnAction += new OnActionEventHandler(gridElem_OnAction);
            gridElem.WhereCondition = SqlHelperClass.AddWhereCondition(gridElem.WhereCondition, filter.WhereCondition);
            gridElem.ZeroRowsText = GetString("om.account.noaccountsfound");

            // Initialize dropdown lists
            if (!RequestHelper.IsPostBack())
            {
                drpAction.Items.Add(new ListItem(GetString("general." + Action.SelectAction), Convert.ToInt32(Action.SelectAction).ToString()));
                drpAction.Items.Add(new ListItem(GetString("general.remove"), Convert.ToInt32(Action.Remove).ToString()));
                drpWhat.Items.Add(new ListItem(GetString("om.account." + What.Selected), Convert.ToInt32(What.Selected).ToString()));
                drpWhat.Items.Add(new ListItem(GetString("om.account." + What.All), Convert.ToInt32(What.All).ToString()));
            }

            // Initialize account selectr
            accountSelector.UniSelector.Enabled = AccountHelper.AuthorizedModifyAccount(this.SiteID, false);
            accountSelector.UniSelector.ButtonImage = GetImageUrl("/Objects/OM_Account/add.png");
            accountSelector.ImageDialog.CssClass = "NewItemImage";
            accountSelector.UniSelector.OnItemsSelected += new EventHandler(UniSelector_OnItemsSelected);
            accountSelector.UniSelector.SelectionMode = SelectionModeEnum.MultipleButton;
            // Isn't child of someone else
            accountSelector.WhereCondition = "AccountMergedWithAccountID IS NULL AND AccountSubsidiaryOfID IS NULL";
            // And isn't recursive parent of edited account
            accountSelector.WhereCondition = SqlHelperClass.AddWhereCondition(accountSelector.WhereCondition, "AccountID NOT IN (SELECT * FROM Func_OM_Account_GetSubsidiaryOf(" + ai.AccountID + ", 1))");
            // Registr JS scripts
            RegisterScripts();
        }
        else
        {
            this.StopProcessing = true;
        }
    }


    object gridElem_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        ImageButton btn = null;

        switch (sourceName.ToLower())
        {
            case "remove":
                if (!ModifyAccountPermission)
                {
                    btn = (ImageButton)sender;
                    btn.Enabled = false;
                    btn.Attributes.Add("src", GetImageUrl("Design/Controls/UniGrid/Actions/DeleteDisabled.png"));
                }
                break;
        }

        return CMControlsHelper.UniGridOnExternalDataBound(sender, sourceName, parameter);
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        // Hide footer if grid is empty
        pnlFooter.Visible = !gridElem.IsEmpty;
    }

    #endregion


    #region "Events"

    /// <summary>
    /// Unigrid button clicked.
    /// </summary>
    void gridElem_OnAction(string actionName, object actionArgument)
    {
        if (actionName == "remove")
        {
            if (AccountHelper.AuthorizedModifyAccount(this.SiteID, true))
            {
                int relationId = ValidationHelper.GetInteger(actionArgument, 0);
                AccountInfo editedObject = AccountInfoProvider.GetAccountInfo(relationId);
                if (editedObject != null)
                {
                    editedObject.AccountSubsidiaryOfID = 0;
                    AccountInfoProvider.SetAccountInfo(editedObject);
                }
            }
        }
    }


    /// <summary>
    /// Items changed event handler.
    /// </summary>
    void UniSelector_OnItemsSelected(object sender, EventArgs e)
    {
        if (AccountHelper.AuthorizedModifyAccount(this.SiteID, true))
        {
            // Get new items from selector
            string newValues = ValidationHelper.GetString(accountSelector.Value, null);
            string[] newItems = newValues.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            if (newItems != null)
            {
                // Set HQ ID of selected accounts to edited account ID
                string where = SqlHelperClass.GetWhereCondition<int>("AccountID", newItems, false);
                AccountInfoProvider.UpdateAccountHQ(ai.AccountID, where);

                gridElem.ReloadData();
                pnlUpdate.Update();
                accountSelector.Value = null;
            }
        }
    }


    protected void btnOk_Click(object sender, EventArgs e)
    {
        if (AccountHelper.AuthorizedModifyAccount(this.SiteID, true))
        {
            string resultMessage = string.Empty;

            Action action = (Action)ValidationHelper.GetInteger(drpAction.SelectedItem.Value, 0);
            What what = (What)ValidationHelper.GetInteger(drpWhat.SelectedItem.Value, 0);

            string where = string.Empty;

            switch (what)
            {
                // All items
                case What.All:
                    where = CMSContext.ResolveMacros(gridElem.WhereCondition);
                    break;
                // Selected items
                case What.Selected:
                    where = SqlHelperClass.GetWhereCondition<int>("AccountID", (string[])gridElem.SelectedItems.ToArray(typeof(string)), false);
                    break;
                default:
                    return;
            }

            switch (action)
            {
                // Action 'Remove'
                case Action.Remove:
                    // Clear HQ ID of selected accounts
                    AccountInfoProvider.UpdateAccountHQ(0, where);
                    resultMessage = GetString("om.account.massaction.removed");
                    break;
                default:
                    return;
            }

            if (!string.IsNullOrEmpty(resultMessage))
            {
                lblInfo.Text = resultMessage;
                lblInfo.Visible = true;
            }

            // Reload unigrid
            gridElem.ClearSelectedItems();
            gridElem.ReloadData();
            pnlUpdate.Update();
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Registers JS.
    /// </summary>
    private void RegisterScripts()
    {
        ScriptHelper.RegisterDialogScript(this.Page);
        StringBuilder script = new StringBuilder();

        // Register script to open dialogs for role selection and for account editing
        script.Append(@"
function EditAccount(accountID)
{
    modalDialog('" + ResolveUrl(ACCOUNT_DETAIL_DIALOG) + @"?accountid=' + accountID + '&isSiteManager=" + ContactHelper.IsSiteManager + @"', 'AccountSubsidiary', '1061px', '700px');
}
function Refresh()
{
    __doPostBack('" + pnlUpdate.ClientID + @"', '');
}
");
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "GridActions", ScriptHelper.GetScript(script.ToString()));

        // Register script for mass actions
        script = new StringBuilder();
        script.Append(
@"
function PerformAction(selectionFunction, actionId, actionLabel, whatId) {
    var confirmation = null;
    var label = document.getElementById(actionLabel);
    var action = document.getElementById(actionId).value;
    var whatDrp = document.getElementById(whatId);
    label.innerHTML = '';
    if (action == '", (int)Action.SelectAction, @"') {
        label.innerHTML = '", GetString("MassAction.SelectSomeAction"), @"'
    }
    else if (eval(selectionFunction) && (whatDrp.value == '", (int)What.Selected, @"')) {
        label.innerHTML = '", GetString("om.account.massaction.select"), @"';
    }
    else {
        switch(action) {
            case '", (int)Action.Remove, @"':
                confirmation = ", ScriptHelper.GetString(GetString("General.ConfirmRemove")), @";
                break;
            default:
                confirmation = null;
                break;
        }
        if (confirmation != null) {
            return confirm(confirmation)
        }
    }
    return false;
}");

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "MassActions", ScriptHelper.GetScript(script.ToString()));

        // Add action to button
        btnOk.OnClientClick = "return PerformAction('" + gridElem.GetCheckSelectionScript() + "','" + drpAction.ClientID + "','" + lblInfo.ClientID + "','" + drpWhat.ClientID + "');";
    }

    #endregion
}