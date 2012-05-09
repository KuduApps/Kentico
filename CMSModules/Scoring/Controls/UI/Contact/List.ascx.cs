using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

using CMS.CMSHelper;
using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.OnlineMarketing;
using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.FormEngine;

public partial class CMSModules_Scoring_Controls_UI_Contact_List : CMSAdminListControl, ICallbackEventHandler
{
    #region "Variables"

    private bool modifyPermission = false;
    private int mSiteId = 0;


    /// <summary>
    /// Available actions in mass action selector.
    /// </summary>
    protected enum Action
    {
        SelectAction = 0,
        AddToGroup = 1,
        ChangeStatus = 2
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
    /// URL of modal dialog for contact status selection.
    /// </summary>
    protected const string CONTACT_STATUS_DIALOG = "~/CMSModules/ContactManagement/FormControls/ContactStatusDialog.aspx";


    /// <summary>
    /// URL of modal dialog for contact group selection.
    /// </summary>
    protected const string CONTACT_GROUP_DIALOG = "~/CMSModules/ContactManagement/FormControls/ContactGroupDialog.aspx";


    /// <summary>
    /// URL of modal dialog for displaying contact details.
    /// </summary>
    protected const string CONTACT_DETAIL_DIALOG = "~/CMSModules/ContactManagement/Pages/Tools/Account/Contact_Detail.aspx";

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
        }
    }


    /// <summary>
    /// Dialog control identifier.
    /// </summary>
    private string Identifier
    {
        get
        {
            string identifier = hdnIdentificator.Value;
            if (string.IsNullOrEmpty(identifier))
            {
                identifier = Guid.NewGuid().ToString();
                hdnIdentificator.Value = identifier;
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
    /// Gets current site ID.
    /// </summary>
    private int SiteId
    {
        get
        {
            if (mSiteId == 0)
            {
                mSiteId = CMSContext.CurrentSiteID;
            }
            return mSiteId;
        }
    }

    #endregion


    #region "Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize score filter
        FormFieldInfo ffi = new FormFieldInfo();
        ffi.Name = " HAVING SUM(Value)";
        ffi.DataType = FormFieldDataTypeEnum.Integer;
        ucScoreFilter.FieldInfo = ffi;
        ucScoreFilter.DefaultOperator = ">=";
        ucScoreFilter.WhereConditionFormat = "{0} {2} {1}";

        // Get modify permission of current user
        modifyPermission = ContactHelper.AuthorizedModifyContact(SiteId, false);

        // Set where condition    
        gridElem.WhereCondition = "(ScoreId = @ScoreID) AND (Expiration IS NULL OR Expiration > @CurrentDate) GROUP BY OM_SelectScoreContact.ContactID, ContactFullNameJoined, ContactStatusID"
            + ucScoreFilter.GetWhereCondition();

        // Add parameters
        QueryDataParameters parameters = new QueryDataParameters();
        parameters.AddDateTime("@CurrentDate", DateTime.Now);
        parameters.AddId("@CurrentSiteID", SiteId);
        parameters.AddId("@ScoreID", QueryHelper.GetInteger("ScoreID", 0));
        gridElem.QueryParameters = parameters;

        // Register OnExternalDataBound
        gridElem.OnExternalDataBound += new OnExternalDataBoundEventHandler(gridElem_OnExternalDataBound);
        gridElem.OnBeforeFiltering += new OnBeforeFiltering(gridElem_OnBeforeFiltering);

        // Initialize dropdown lists
        if (!RequestHelper.IsPostBack())
        {
            drpAction.Items.Add(new ListItem(GetString("general." + Action.SelectAction), Convert.ToInt32(Action.SelectAction).ToString()));
            if ((modifyPermission || ContactGroupHelper.AuthorizedModifyContactGroup(SiteId, false)) && ContactGroupHelper.AuthorizedReadContactGroup(SiteId, false))
            {
                drpAction.Items.Add(new ListItem(GetString("om.account." + Action.AddToGroup), Convert.ToInt32(Action.AddToGroup).ToString()));
            }
            if (modifyPermission)
            {
                drpAction.Items.Add(new ListItem(GetString("om.account." + Action.ChangeStatus), Convert.ToInt32(Action.ChangeStatus).ToString()));
            }
            drpWhat.Items.Add(new ListItem(GetString("om.contact." + What.Selected), Convert.ToInt32(What.Selected).ToString()));
            drpWhat.Items.Add(new ListItem(GetString("om.contact." + What.All), Convert.ToInt32(What.All).ToString()));
        }
        else
        {
            if (RequestHelper.CausedPostback(btnOk))
            {
                // Set delayed reload for unigrid if mass action is performed
                gridElem.DelayedReload = true;
            }
        }

        // Register JS scripts
        RegisterScripts();
    }   


    protected void Page_PreRender(object sender, EventArgs e)
    {
        pnlFooter.Visible = !gridElem.IsEmpty && (drpAction.Items.Count > 1);
    }


    /// <summary>
    /// On external databound.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="sourceName">Source name</param>
    /// <param name="parameter">Parameter</param>
    object gridElem_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        ImageButton btn = null;

        switch (sourceName.ToLower())
        {
            case "edit":
                btn = ((ImageButton)sender);
                // Add ability to open contact details
                btn.Attributes.Add("onClick", "EditContact(" + btn.CommandArgument + "); return false;");
                break;
            case "#statusdisplayname":
                ContactStatusInfo statusInfo = ContactStatusInfoProvider.GetContactStatusInfo(ValidationHelper.GetInteger(parameter, 0));
                if (statusInfo != null)
                {
                    return HTMLHelper.HTMLEncode(statusInfo.ContactStatusDisplayName);
                }
                break;
        }

        return null;
    }


    /// <summary>
    /// Event where WHERE condition for unigrid filter could be changed.
    /// </summary>
    /// <param name="whereCondition">Where condition from filter</param>
    string gridElem_OnBeforeFiltering(string whereCondition)
    {
        // Parse value from filter
        Regex re = RegexHelper.GetRegex(@"^\(\[Score\]\s([<>=][<>]?\s\d+)\)$");
        Match m = re.Match(whereCondition);

        if (m.Groups.Count == 2)
        {
            // Get value
            string value = m.Groups[1].ToString();            

            // Add to where condition
            gridElem.WhereCondition += " HAVING SUM(Value) " + value;            
        }

        // Returns empty because filter condition is added to WhereCondition property
        return string.Empty;
    }


    /// <summary>
    /// Mass operation button "OK" click.
    /// </summary>
    protected void btnOk_Click(object sender, EventArgs e)
    {
        string resultMessage = string.Empty;
        // Get where condition depending on mass action selection
        string where = null;
        ArrayList contactIds = null;

        What what = (What)ValidationHelper.GetInteger(drpWhat.SelectedValue, 0);
        switch (what)
        {
            // All items
            case What.All:
                // Get selected IDs based on where condition
                DataSet contacts = gridElem.InfoObject.GetData(gridElem.QueryParameters, gridElem.CompleteWhereCondition, null, -1, "OM_SelectScoreContact.ContactID", false);
                if (!DataHelper.DataSourceIsEmpty(contacts))
                {
                    // Get array list with IDs
                    contactIds = DataHelper.GetUniqueValues(contacts.Tables[0], "ContactID", true);
                }
                break;
            // Selected items
            case What.Selected:
                // Get selected IDs from unigrid
                contactIds = gridElem.SelectedItems;
                break;
        }

        // Prepare where condition
        if ((contactIds != null) && (contactIds.Count > 0))
        {
            where = SqlHelperClass.GetWhereCondition<int>("ContactID", (string[])contactIds.ToArray(typeof(string)), false);
        }
        else
        {
            where = "0=1";
        }

        Action action = (Action)ValidationHelper.GetInteger(drpAction.SelectedItem.Value, 0);
        switch (action)
        {
            // Action 'Change status'
            case Action.ChangeStatus:
                // Get selected status ID from hidden field
                int statusId = ValidationHelper.GetInteger(hdnIdentificator.Value, -1);
                // If status ID is 0, the status will be removed
                if (statusId >= 0)
                {
                    ContactInfoProvider.UpdateContactStatus(statusId, where);
                    resultMessage = GetString("om.contact.massaction.statuschanged");
                }
                break;
            // Action 'Add to contact group'
            case Action.AddToGroup:
                // Get contact group ID from hidden field
                int groupId = ValidationHelper.GetInteger(hdnIdentificator.Value, 0);
                if ((groupId > 0) && (contactIds != null))
                {
                    int contactId = 0;
                    // Add each selected contact to the contact group, skip contacts that are already members of the group
                    foreach (string item in contactIds)
                    {
                        contactId = ValidationHelper.GetInteger(item, 0);
                        if (contactId > 0)
                        {
                            ContactGroupMemberInfoProvider.SetContactGroupMemberInfo(groupId, contactId, ContactGroupMemberTypeEnum.Contact, MemberAddedHowEnum.Manual);
                        }
                    }
                    // Get contact group to show result message with its display name
                    ContactGroupInfo group = ContactGroupInfoProvider.GetContactGroupInfo(groupId);
                    if (group != null)
                    {
                        resultMessage = String.Format(GetString("om.contact.massaction.addedtogroup"), group.ContactGroupDisplayName);
                    }
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

    #endregion


    #region "Methods"

    /// <summary>
    /// Registers JS.
    /// </summary>
    private void RegisterScripts()
    {
        ScriptHelper.RegisterDialogScript(this.Page);
        StringBuilder script = new StringBuilder();

        // Register script to open dialogs
        script.Append(@"
function SelectStatus(queryParameters)
{
    modalDialog('" + ResolveUrl(CONTACT_STATUS_DIALOG) + @"' + queryParameters, 'selectStatus', '660px', '590px');
}
function SelectContactGroup(queryParameters)
{
    modalDialog('" + ResolveUrl(CONTACT_GROUP_DIALOG) + @"' + queryParameters, 'selectGroup', '500px', '435px');
}
function EditContact(contactID)
{
    modalDialog('" + ResolveUrl(CONTACT_DETAIL_DIALOG) + @"?contactid=' + contactID, 'ContactDetail', '1061px', '700px');
}
function Refresh()
{
    __doPostBack('" + pnlUpdate.ClientID + @"', '');
}
var dialogParams_" + ClientID + @" = '';
");

        // Register script for mass actions
        script.Append(
@"
function PerformAction(selectionFunction, selectionField, actionId, actionLabel, whatId) {
    var confirmation = null;
    var label = document.getElementById(actionLabel);
    var action = document.getElementById(actionId).value;
    var whatDrp = document.getElementById(whatId);
    var selectionFieldElem = document.getElementById(selectionField);
    label.innerHTML = '';
    if (action == '", (int)Action.SelectAction, @"') {
        label.innerHTML = '", GetString("MassAction.SelectSomeAction"), @"'
    }
    else if (eval(selectionFunction) && (whatDrp.value == '", (int)What.Selected, @"')) {
        label.innerHTML = '", GetString("om.contact.massaction.select"), @"';
    }
    else {
        var param = 'massaction;' + whatDrp.value;
        if (whatDrp.value == '", (int)What.Selected, @"') {
            param = param + '#' + selectionFieldElem.value;
        }
        switch(action) {
            case '", (int)Action.ChangeStatus, @"':
                dialogParams_", ClientID, @" = param + ';", (int)Action.ChangeStatus, @"';", Page.ClientScript.GetCallbackEventReference(this, "dialogParams_" + ClientID, "SelectStatus", null), @";
                break;
            case '", (int)Action.AddToGroup, @"':
                dialogParams_", ClientID, @" = param + ';", (int)Action.AddToGroup, @"';", Page.ClientScript.GetCallbackEventReference(this, "dialogParams_" + ClientID, "SelectContactGroup", null), @";
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
function SelectValue_" + this.ClientID + @"(valueID) {
    document.getElementById('" + hdnIdentificator.ClientID + @"').value = valueID;" +
    ControlsHelper.GetPostBackEventReference(btnOk, null) + @";
}
");

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "MassActions", ScriptHelper.GetScript(script.ToString()));

        // Add action to button
        btnOk.OnClientClick = "return PerformAction('" + gridElem.GetCheckSelectionScript() + "','" + gridElem.GetSelectionFieldClientID() + "','" + drpAction.ClientID + "','" + lblInfo.ClientID + "','" + drpWhat.ClientID + "');";
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
            Hashtable mParameters = new Hashtable();
            // ...for mass action
            if (CallbackArgument.StartsWith("massaction;", StringComparison.InvariantCultureIgnoreCase))
            {
                // Get values of callback argument
                string[] selection = CallbackArgument.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                if (selection.Length != 3)
                {
                    return null;
                }

                // Get selected actions from DD-list
                Action action = (Action)ValidationHelper.GetInteger(selection[2], 0);
                switch (action)
                {
                    case Action.ChangeStatus:
                        mParameters["allownone"] = true;
                        mParameters["clientid"] = ClientID;
                        break;
                    case Action.AddToGroup:
                        break;
                    default:
                        return null;
                }
            }

            mParameters["clientid"] = ClientID;
            mParameters["siteid"] = SiteId;
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
