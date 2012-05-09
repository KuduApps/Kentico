using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.OnlineMarketing;
using CMS.SettingsProvider;
using CMS.UIControls;

public partial class CMSModules_ContactManagement_Controls_UI_Contact_Accounts : CMSAdminListControl, ICallbackEventHandler
{
    #region "Variables"

    private ContactInfo ci = null;
    private int mSiteID = -1;
    private Hashtable mParameters;
    private bool modifyAccountContact = false;


    /// <summary>
    /// Available actions in mass action selector.
    /// </summary>
    protected enum Action
    {
        SelectAction = 0,
        Remove = 1,
        SelectRole = 2,
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
    /// Mass action selector parameters.
    /// </summary>
    protected enum Argument
    {
        Action = 0,
        AllSelected = 1,
        Items = 2
    }


    /// <summary>
    /// URL of modal dialog window for contact role selection.
    /// </summary>
    public const string CONTACT_ROLE_DIALOG = "~/CMSModules/ContactManagement/FormControls/ContactRoleDialog.aspx";


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
            accountSelector.StopProcessing = value;
        }
    }


    /// <summary>
    /// Gets current site ID.
    /// </summary>
    private int SiteID
    {
        get
        {
            return mSiteID;
        }
        set
        {
            mSiteID = value;
            accountSelector.SiteID = value;
        }
    }


    /// <summary>
    /// Dialog control identifier.
    /// </summary>
    private string Identifier
    {
        get
        {
            string identifier = hdnValue.Value;
            if (string.IsNullOrEmpty(identifier))
            {
                identifier = Guid.NewGuid().ToString();
                hdnValue.Value = identifier;
            }

            return identifier;
        }
    }


    /// <summary>
    /// Gets or sets the callback argument.
    /// </summary>
    private string CallbackArgument
    {
        get;
        set;
    }


    /// <summary>
    /// Indicates if control is used on live site.
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
        // Get edited object
        if (CMSContext.EditedObject != null)
        {
            ci = (ContactInfo)CMSContext.EditedObject;
            SiteID = ci.ContactSiteID;

            // Setup unigrid
            gridElem.GridOptions.ShowSelection = (ci.ContactMergedWithContactID == 0);
            gridElem.WhereCondition = SqlHelperClass.AddWhereCondition(gridElem.WhereCondition, "(ContactID = " + ci.ContactID + ") AND ((AccountMergedWithAccountID IS NULL AND AccountSiteID > 0) OR (AccountGlobalAccountID IS NULL AND AccountSiteID IS NULL))");
            gridElem.OnAction += new OnActionEventHandler(gridElem_OnAction);
            gridElem.OnExternalDataBound += new OnExternalDataBoundEventHandler(gridElem_OnExternalDataBound);
            gridElem.ZeroRowsText = GetString("om.contact.noaccounts");

            // Initialize dropdown lists
            if (!RequestHelper.IsPostBack())
            {
                drpAction.Items.Add(new ListItem(GetString("general." + Action.SelectAction), Convert.ToInt32(Action.SelectAction).ToString()));
                drpAction.Items.Add(new ListItem(GetString("general.remove"), Convert.ToInt32(Action.Remove).ToString()));
                drpAction.Items.Add(new ListItem(GetString("om.contactrole.select"), Convert.ToInt32(Action.SelectRole).ToString()));
                drpWhat.Items.Add(new ListItem(GetString("om.account." + What.Selected), Convert.ToInt32(What.Selected).ToString()));
                drpWhat.Items.Add(new ListItem(GetString("om.account." + What.All), Convert.ToInt32(What.All).ToString()));
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
            accountSelector.UniSelector.OnItemsSelected += new EventHandler(UniSelector_OnItemsSelected);
            accountSelector.UniSelector.SelectionMode = SelectionModeEnum.MultipleButton;
            accountSelector.ImageDialog.CssClass = "NewItemImage";
            accountSelector.UniSelector.SelectItemPageUrl = "~/CMSModules/ContactManagement/Pages/Tools/Contact/Add_Account_Dialog.aspx";
            accountSelector.UniSelector.SetValue("SiteID", this.SiteID);
            accountSelector.UniSelector.SetValue("IsLiveSite", false);
            accountSelector.IsSiteManager = ContactHelper.IsSiteManager;

            modifyAccountContact = AccountHelper.AuthorizedModifyAccount(this.SiteID, false) || ContactHelper.AuthorizedModifyContact(this.SiteID, false);

            // Registr JS scripts
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
        // Hide footer if grid is empty or if the contact is merged (is not active)
        pnlFooter.Visible = (!gridElem.IsEmpty) && (gridElem.GridOptions.ShowSelection);

        if ((ci.ContactMergedWithContactID != 0) || !modifyAccountContact)
        {
            pnlFooter.Visible = false;
            pnlSelector.Visible = false;
        }
    }

    #endregion


    #region "Events"

    /// <summary>
    /// Unigrid external databoud event handler.
    /// </summary>
    protected object gridElem_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        ImageButton btn = null;

        switch (sourceName.ToLower())
        {
            case "edit":
                btn = ((ImageButton)sender);
                btn.Attributes.Add("onClick", "EditAccount(" + btn.CommandArgument + "); return false;");
                break;

            case "selectrole":
                btn = (ImageButton)sender;
                if ((ci.ContactMergedWithContactID == 0) && modifyAccountContact)
                {
                    btn.OnClientClick = string.Format("dialogParams_{0} = '{1}';{2};return false;",
                        ClientID,
                        btn.CommandArgument,
                        Page.ClientScript.GetCallbackEventReference(this, "dialogParams_" + ClientID, "SelectRole", null));
                }
                else
                {
                    btn.Enabled = false;
                    btn.Attributes.Add("src", GetImageUrl("Design/Controls/UniGrid/Actions/contactroledisabled.png"));
                }
                break;

            case "remove":
                if ((ci.ContactMergedWithContactID != 0) || !modifyAccountContact)
                {
                    btn = (ImageButton)sender;
                    btn.Enabled = false;
                    btn.Attributes.Add("src", GetImageUrl("Design/Controls/UniGrid/Actions/DeleteDisabled.png"));

                }
                break;
        }
        return CMControlsHelper.UniGridOnExternalDataBound(sender, sourceName, parameter);
    }


    /// <summary>
    /// Unigrid button clicked.
    /// </summary>
    protected void gridElem_OnAction(string actionName, object actionArgument)
    {
        if (actionName == "remove")
        {
            // User has permission modify
            if (modifyAccountContact)
            {
                int relationId = ValidationHelper.GetInteger(actionArgument, 0);
                AccountContactInfoProvider.DeleteAccountContactInfo(relationId);
            }
            // User doesn't have sufficient permissions
            else
            {
                if (this.SiteID > 0)
                {
                    CMSPage.RedirectToCMSDeskAccessDenied("CMS.ContactManagement", "ModifyContacts");
                }
                else
                {
                    CMSPage.RedirectToCMSDeskAccessDenied("CMS.ContactManagement", "ModifyGlobalContacts");
                }
            }
        }
    }


    /// <summary>
    /// Items changed event handler.
    /// </summary>
    protected void UniSelector_OnItemsSelected(object sender, EventArgs e)
    {
        if (modifyAccountContact)
        {
            // Get new items from selector
            string newValues = ValidationHelper.GetString(accountSelector.Value, null);
            if (!String.IsNullOrEmpty(newValues))
            {
                string[] newItems = newValues.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                if (newItems != null)
                {
                    int previousStop = 0;
                    string where = FetchNextAccounts(ref previousStop, newItems, 1000);

                    while (!String.IsNullOrEmpty(where))
                    {
                        AccountContactInfoProvider.SetAccountsIntoContact(ci.ContactID, "AccountID IN (" + where + ")", ValidationHelper.GetInteger(hdnRoleID.Value, 0));

                        where = FetchNextAccounts(ref previousStop, newItems, 1000);
                    }
                }

                gridElem.ReloadData();
                pnlUpdate.Update();
                accountSelector.Value = null;
            }
        }
        // No permission modify
        else
        {
            if (this.SiteID > 0)
            {
                CMSPage.RedirectToCMSDeskAccessDenied("CMS.ContactManagement", "ModifyContacts");
            }
            else
            {
                CMSPage.RedirectToCMSDeskAccessDenied("CMS.ContactManagement", "ModifyGlobalContacts");
            }
        }
    }


    protected void btnOk_Click(object sender, EventArgs e)
    {
        if (modifyAccountContact)
        {
            string resultMessage = string.Empty;

            Action action = (Action)ValidationHelper.GetInteger(drpAction.SelectedItem.Value, 0);
            What what = (What)ValidationHelper.GetInteger(drpWhat.SelectedItem.Value, 0);

            string where = string.Empty;

            switch (what)
            {
                // All items
                case What.All:
                    where = SqlHelperClass.AddWhereCondition(gridElem.WhereCondition, gridElem.WhereClause);
                    where = string.Format("ContactID={0} AND AccountID IN (SELECT AccountID FROM View_OM_AccountContact_AccountJoined WHERE {1})", ci.ContactID, where);
                    break;
                // Selected items
                case What.Selected:
                    where = SqlHelperClass.GetWhereCondition<int>("AccountContactID", ContactHelper.GetSafeArray(gridElem.SelectedItems), false);
                    break;
                default:
                    return;
            }

            switch (action)
            {
                // Action 'Remove'
                case Action.Remove:
                    // Reset accounts' main contact IDs if the contact was set as primary or secondary contact
                    AccountContactInfoProvider.ResetAccountMainContacts(0, ci.ContactID, where);
                    // Delete the relations between contact and accounts
                    AccountContactInfoProvider.DeleteAllAccountContacts(where);
                    resultMessage = GetString("om.account.massaction.removed");
                    break;
                // Action 'Select role'
                case Action.SelectRole:
                    // Get selected role ID from hidden field
                    int roleId = ValidationHelper.GetInteger(hdnValue.Value, -1);
                    if (roleId >= 0 && modifyAccountContact)
                    {
                        AccountContactInfoProvider.UpdateContactRole(roleId, where);
                        resultMessage = GetString("om.contact.massaction.roleassigned");
                    }
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
        // No permission modify
        else
        {
            if (this.SiteID > 0)
            {
                CMSPage.RedirectToCMSDeskAccessDenied("CMS.ContactManagement", "ModifyContacts");
            }
            else
            {
                CMSPage.RedirectToCMSDeskAccessDenied("CMS.ContactManagement", "ModifyGlobalContacts");
            }
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Returns limited number of accounts to be added.
    /// </summary>
    /// <param name="previousStop">Previous position</param>
    /// <param name="newItems">Array of items to be added</param>
    /// <param name="howMuch">How much of records to fetch</param>
    /// <returns>Returns items separated by colon.</returns>
    private string FetchNextAccounts(ref int previousStop, string[] newItems, int howMuch)
    {
        StringBuilder whereBuild = new StringBuilder();

        // Get new where
        for (int i = previousStop; (i < (previousStop + howMuch)) && (i < newItems.Length); i++)
        {
            whereBuild.Append(ValidationHelper.GetInteger(newItems[i], 0) + ",");
        }

        // Update last position
        if (previousStop + howMuch > newItems.Length)
        {
            previousStop = newItems.Length;
        }
        else
        {
            previousStop += howMuch;
        }

        // Return WHERE
        String where = whereBuild.ToString();
        if (!String.IsNullOrEmpty(where))
        {
            return where.Remove(where.Length - 1, 1);
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Registers JS.
    /// </summary>
    private void RegisterScripts()
    {
        ScriptHelper.RegisterDialogScript(this.Page);
        StringBuilder script = new StringBuilder();

        // Register script to open dialogs for role selection and for account editing
        script.Append(@"
function SelectRole(queryParameters)
{
    modalDialog('" + ResolveUrl(CONTACT_ROLE_DIALOG) + @"' + queryParameters, 'selectRole', '660px', '590px');
}
function EditAccount(accountID)
{
    modalDialog('" + ResolveUrl(ACCOUNT_DETAIL_DIALOG) + @"?accountid=' + accountID + '&isSiteManager=" + ContactHelper.IsSiteManager + @"', 'AccountDetail', '1061px', '700px');
}
function Refresh()
{
    __doPostBack('" + pnlUpdate.ClientID + @"', '');
}
function setRole(roleID) 
{
    $j('#" + hdnRoleID.ClientID + @"').val(roleID);
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
            case '", (int)Action.SelectRole, @"':
                dialogParams_", ClientID, @" = 'ismassaction';", Page.ClientScript.GetCallbackEventReference(this, "dialogParams_" + ClientID, "SelectRole", null), @";
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
}
function AssignContactRole_" + this.ClientID + @"(roleId) {
    document.getElementById('" + hdnValue.ClientID + @"').value = roleId;" +
    ControlsHelper.GetPostBackEventReference(btnOk, null) + @";
}
var dialogParams_" + ClientID + @" = '';
");

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "Actions", ScriptHelper.GetScript(script.ToString()));

        // Add action to button
        btnOk.OnClientClick = "return PerformAction('" + gridElem.GetCheckSelectionScript() + "','" + drpAction.ClientID + "','" + lblInfo.ClientID + "','" + drpWhat.ClientID + "');";
    }

    #endregion


    #region "ICallbackEventHandler Members"

    /// <summary>
    /// Gets callback result.
    /// </summary>
    public string GetCallbackResult()
    {
        string queryString = string.Empty;

        if (!string.IsNullOrEmpty(CallbackArgument))
        {
            // Prepare parameters...
            mParameters = new Hashtable();
            if (CallbackArgument.Equals("ismassaction", StringComparison.InvariantCultureIgnoreCase))
            {
                // for mass action
                mParameters["ismassaction"] = "1";
                mParameters["siteid"] = SiteID;
                mParameters["clientid"] = ClientID;
            }
            else
            {
                // for unigrid action
                mParameters["accountcontactid"] = CallbackArgument;
            }
            mParameters["allownone"] = "1";
            mParameters["issitemanager"] = ContactHelper.IsSiteManager;

            WindowHelper.Add(Identifier, mParameters);

            queryString = "?params=" + Identifier;
            queryString = URLHelper.AddParameterToUrl(queryString, "hash", QueryHelper.GetHash(queryString));
        }

        return queryString;
    }


    /// <summary>
    /// Raise callback method.
    /// </summary>
    public void RaiseCallbackEvent(string eventArgument)
    {
        CallbackArgument = eventArgument;
    }

    #endregion
}