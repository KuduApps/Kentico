using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.OnlineMarketing;
using CMS.SettingsProvider;
using CMS.UIControls;
using System.Collections;

public partial class CMSModules_ContactManagement_Controls_UI_ContactGroup_Accounts : CMSAdminListControl
{
    #region "Variables"

    private ContactGroupInfo cgi = null;
    private int siteID = -1;
    private CurrentUserInfo currentUser;
    private bool readSiteAccounts;
    private bool readGlobalAccounts;
    private bool modifySiteAccounts;
    private bool modifyGlobalAccounts;
    private bool modifySiteCG;
    private bool modifyGlobalCG;
    private bool modifyCombined;

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
    /// Inner grid.
    /// </summary>
    public UniGrid Grid
    {
        get
        {
            return this.gridElem;
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
            this.gridElem.StopProcessing = value;
            this.accountSelector.StopProcessing = value;
        }
    }


    /// <summary>
    /// Indicates if  filter is used on live site or in UI.
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
            gridElem.IsLiveSite = value;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Get edited object (contact group)
        if (CMSContext.EditedObject != null)
        {
            currentUser = CMSContext.CurrentUser;
            cgi = (ContactGroupInfo)CMSContext.EditedObject;
            siteID = cgi.ContactGroupSiteID;

            // Check permissions
            readSiteAccounts = AccountHelper.AuthorizedReadAccount(CMSContext.CurrentSiteID, false);
            modifySiteAccounts = AccountHelper.AuthorizedModifyAccount(CMSContext.CurrentSiteID, false);
            modifySiteCG = ContactGroupHelper.AuthorizedModifyContactGroup(CMSContext.CurrentSiteID, false);
            if (siteID <= 0)
            {
                readGlobalAccounts = AccountHelper.AuthorizedReadAccount(UniSelector.US_GLOBAL_RECORD, false);
                modifyGlobalAccounts = AccountHelper.AuthorizedModifyAccount(UniSelector.US_GLOBAL_RECORD, false);
                modifyGlobalCG = ContactGroupHelper.AuthorizedModifyContactGroup(UniSelector.US_GLOBAL_RECORD, false);
            }

            // Setup unigrid
            gridElem.WhereCondition = GetWhereCondition();
            gridElem.OnAction += new OnActionEventHandler(gridElem_OnAction);
            gridElem.ZeroRowsText = GetString("om.account.noaccountsfound");
            gridElem.OnBeforeDataReload += new OnBeforeDataReload(gridElem_OnBeforeDataReload);
            gridElem.OnExternalDataBound += new OnExternalDataBoundEventHandler(gridElem_OnExternalDataBound);

            modifyCombined =
                // Site contact group -> only site accounts can be added
                    ((siteID > 0) && (modifySiteAccounts || modifySiteCG))
                // Global contact group -> both site and global accounts can be added
                    || ((siteID <= 0) && (
                // User can display only global accounts
                    (readGlobalAccounts && !readSiteAccounts && (modifyGlobalAccounts || modifyGlobalCG)) ||
                // User can display only site accounts
                    (readSiteAccounts && !readGlobalAccounts && (modifySiteAccounts || modifySiteCG)) ||
                // User can display both site and global accounts
                    (readSiteAccounts && readGlobalAccounts && (modifySiteCG || modifySiteAccounts) && (modifyGlobalCG || modifyGlobalAccounts))
                    ));

            // Initialize dropdown lists
            if (!RequestHelper.IsPostBack())
            {
                // Display mass actions
                if (modifyCombined)
                {
                    drpAction.Items.Add(new ListItem(GetString("general." + Action.SelectAction), Convert.ToInt32(Action.SelectAction).ToString()));
                    drpAction.Items.Add(new ListItem(GetString("general.remove"), Convert.ToInt32(Action.Remove).ToString()));
                    drpWhat.Items.Add(new ListItem(GetString("om.account." + What.Selected), Convert.ToInt32(What.Selected).ToString()));
                    drpWhat.Items.Add(new ListItem(GetString("om.account." + What.All), Convert.ToInt32(What.All).ToString()));
                }
            }
            else
            {
                if (RequestHelper.CausedPostback(btnOk))
                {
                    // Set delayed reload for unigrid if mass action is performed
                    gridElem.DelayedReload = true;
                }
            }

            // Initialize contact selector
            accountSelector.UniSelector.ButtonImage = GetImageUrl("/Objects/OM_Account/add.png");
            accountSelector.ImageDialog.CssClass = "NewItemImage";
            accountSelector.UniSelector.OnItemsSelected += new EventHandler(UniSelector_OnItemsSelected);
            accountSelector.UniSelector.SelectionMode = SelectionModeEnum.MultipleButton;

            // Register JS scripts
            RegisterScripts();
        }
        else
        {
            this.StopProcessing = true;
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        // Hide footer if grid is empty
        pnlFooter.Visible = !gridElem.IsEmpty && (drpAction.Items.Count > 0);
    }

    #endregion


    #region "Events"

    /// <summary>
    /// UniGrid external databound.
    /// </summary>
    object gridElem_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        ImageButton btn = null;
        switch (sourceName.ToLower())
        {
            // Display delete button
            case "remove":
                btn = (ImageButton)sender;

                // Display delete button only for users with appropriate permission
                DataRowView drv = (parameter as GridViewRow).DataItem as DataRowView;
                if (ValidationHelper.GetInteger(drv["AccountSiteID"], 0) > 0)
                {
                    btn.Enabled = modifySiteAccounts || modifySiteCG;
                }
                else
                {
                    btn.Enabled = modifyGlobalAccounts || modifyGlobalCG;
                }
                if (!btn.Enabled)
                {
                    btn.Attributes.Add("src", GetImageUrl("Design/Controls/UniGrid/Actions/DeleteDisabled.png"));
                }
                break;
        }
        return CMControlsHelper.UniGridOnExternalDataBound(sender, sourceName, parameter);
    }


    /// <summary>
    /// OnBeforeDataReload event handler.
    /// </summary>
    void gridElem_OnBeforeDataReload()
    {
        gridElem.NamedColumns["SiteName"].Visible = !(siteID > 0) && !(siteID == UniSelector.US_GLOBAL_RECORD) && readSiteAccounts;
    }


    /// <summary>
    /// Unigrid button clicked.
    /// </summary>
    protected void gridElem_OnAction(string actionName, object actionArgument)
    {
        // Perform 'remove' action
        if (actionName == "remove")
        {
            // Delete the object
            int accountId = ValidationHelper.GetInteger(actionArgument, 0);
            AccountInfo account = AccountInfoProvider.GetAccountInfo(accountId);
            if (account != null)
            {
                // User has no permission to modify site accounts
                if (((account.AccountSiteID > 0) && !modifySiteAccounts) || !ContactGroupHelper.AuthorizedModifyContactGroup(cgi.ContactGroupSiteID, false))
                {
                    CMSPage.RedirectToCMSDeskAccessDenied("CMS.ContactManagement", "ModifyAccounts");
                }
                // User has no permission to modify global accounts
                else if ((account.AccountSiteID == 0) && !modifyGlobalAccounts || !ContactGroupHelper.AuthorizedModifyContactGroup(cgi.ContactGroupSiteID, false))
                {
                    CMSPage.RedirectToCMSDeskAccessDenied("CMS.ContactManagement", "ModifyGlobalAccounts");
                }
                // User has permission
                else
                {
                    // Get the relationship object
                    ContactGroupMemberInfo mi = ContactGroupMemberInfoProvider.GetContactGroupMemberInfoByData(cgi.ContactGroupID, accountId, ContactGroupMemberTypeEnum.Account);
                    if (mi != null)
                    {
                        ContactGroupMemberInfoProvider.DeleteContactGroupMemberInfo(mi);
                    }
                }
            }
        }
    }


    /// <summary>
    /// Items changed event handler.
    /// </summary>
    protected void UniSelector_OnItemsSelected(object sender, EventArgs e)
    {
        // Check modify permission
        if (modifyCombined)
        {
            // Get new items from selector
            string newValues = ValidationHelper.GetString(accountSelector.Value, null);
            string[] newItems = newValues.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            if (newItems != null)
            {
                int itemID;

                // Get all selected items
                foreach (string item in newItems)
                {
                    // Check if relation already exists
                    itemID = ValidationHelper.GetInteger(item, 0);
                    if (ContactGroupMemberInfoProvider.GetContactGroupMemberInfoByData(cgi.ContactGroupID, itemID, ContactGroupMemberTypeEnum.Account) == null)
                    {
                        ContactGroupMemberInfoProvider.SetContactGroupMemberInfo(cgi.ContactGroupID, itemID, ContactGroupMemberTypeEnum.Account, MemberAddedHowEnum.Manual);
                    }
                }

                gridElem.ReloadData();
                pnlUpdate.Update();
                accountSelector.Value = null;
            }
        }
        else
        {
            if (siteID > 0)
            {
                CMSPage.RedirectToCMSDeskAccessDenied("CMS.ContactManagement", "ModifyContactGroups");
            }
            else
            {
                CMSPage.RedirectToCMSDeskAccessDenied("CMS.ContactManagement", "ModifyGlobalContactGroups");
            }
        }
    }


    /// <summary>
    /// Mass action 'ok' button clicked.
    /// </summary>
    protected void btnOk_Click(object sender, EventArgs e)
    {
        string resultMessage = string.Empty;

        Action action = (Action)ValidationHelper.GetInteger(drpAction.SelectedItem.Value, 0);
        What what = (What)ValidationHelper.GetInteger(drpWhat.SelectedItem.Value, 0);

        string where = string.Empty;

        switch (what)
        {
            // All items
            case What.All:
                where = CMSContext.ResolveMacros("ContactGroupMemberContactGroupID = " + cgi.ContactGroupID);
                break;
            // Selected items
            case What.Selected:
                // Convert array to integer values to make sure no sql injection is possible (via string values)
                where = SqlHelperClass.GetWhereCondition<int>("ContactGroupMemberID", ContactHelper.GetSafeArray(gridElem.SelectedItems), false);
                where = SqlHelperClass.AddWhereCondition(where, "ContactGroupMemberContactGroupID = " + cgi.ContactGroupID);
                break;
            default:
                return;
        }

        // Set constraint for account relations only
        where = SqlHelperClass.AddWhereCondition(where, "(ContactGroupMemberType = 1)");

        switch (action)
        {
            // Action 'Remove'
            case Action.Remove:
                // Delete the relations between contact group and accounts
                ContactGroupMemberInfoProvider.DeleteContactGroupMembers(where, cgi.ContactGroupID, true, true);
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

    #endregion


    #region "Methods"

    /// <summary>
    /// Returns WHERE condition
    /// </summary>
    private string GetWhereCondition()
    {
        string where = "(ContactGroupMemberContactGroupID = " + cgi.ContactGroupID + ")";
        where = SqlHelperClass.AddWhereCondition(where, "((AccountSiteID IS NULL AND AccountGlobalAccountID IS NULL) OR (AccountSiteID > 0 AND AccountMergedWithAccountID IS NULL))");

        // Filter site objects
        if (siteID > 0)
        {
            if (readSiteAccounts)
            {
                where = SqlHelperClass.AddWhereCondition(where, "(AccountSiteID = " + siteID.ToString() + ")");
                accountSelector.SiteID = siteID;
            }
            else
            {
                CMSPage.RedirectToCMSDeskAccessDenied("CMS.ContactManagement", "ReadAccounts");
            }
        }
        // Current group is global object
        else if (siteID == 0)
        {
            // In CMS Desk display current site and global objects
            if (!ContactHelper.IsSiteManager)
            {
                if (readSiteAccounts && readGlobalAccounts)
                {
                    where = SqlHelperClass.AddWhereCondition(where, "(AccountSiteID IS NULL) OR (AccountSiteID = " + CMSContext.CurrentSiteID + ")");
                    accountSelector.SiteID = UniSelector.US_GLOBAL_OR_SITE_RECORD;
                }
                else if (readGlobalAccounts)
                {
                    where = SqlHelperClass.AddWhereCondition(where, "(AccountSiteID IS NULL)");
                    accountSelector.SiteID = UniSelector.US_GLOBAL_RECORD;
                }
                else if (readSiteAccounts)
                {
                    where = SqlHelperClass.AddWhereCondition(where, "AccountSiteID = " + CMSContext.CurrentSiteID);
                    accountSelector.SiteID = CMSContext.CurrentSiteID;
                }
                else
                {
                    pnlSelector.Visible = false;
                }
            }
            // In Site manager display for global contact group all site and global contacts 
            else
            {
                // No WHERE condition required = displaying all data

                // Set contact selector only
                accountSelector.SiteID = UniSelector.US_ALL_RECORDS;
            }
        }
        return where;
    }


    /// <summary>
    /// Registers JS.
    /// </summary>
    private void RegisterScripts()
    {
        ScriptHelper.RegisterDialogScript(this.Page);
        StringBuilder script = new StringBuilder();
        string sitemanagerAppend = ContactHelper.IsSiteManager ? " + '&issitemanager=1'" : null;

        // Register script to open dialogs for account editing
        script.Append(@"
function EditAccount(accountID)
{
    modalDialog('" + ResolveUrl(ACCOUNT_DETAIL_DIALOG) + @"?accountid=' + accountID " + sitemanagerAppend + @", 'AccountDetail', '1061px', '700px');
}
function Refresh()
{
    __doPostBack('" + pnlUpdate.ClientID + @"', '');
}
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

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "Actions", ScriptHelper.GetScript(script.ToString()));

        // Add action to button
        btnOk.OnClientClick = "return PerformAction('" + gridElem.GetCheckSelectionScript() + "','" + drpAction.ClientID + "','" + lblInfo.ClientID + "','" + drpWhat.ClientID + "');";
    }

    #endregion
}