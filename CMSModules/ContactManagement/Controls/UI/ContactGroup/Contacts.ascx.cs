using System;
using System.Collections;
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

public partial class CMSModules_ContactManagement_Controls_UI_ContactGroup_Contacts : CMSAdminListControl
{
    #region "Variables"

    private ContactGroupInfo cgi = null;
    private int siteID = -1;
    private CurrentUserInfo currentUser;
    private bool readSiteContacts;
    private bool readGlobalContacts;
    private bool modifySiteContacts;
    private bool modifyGlobalContacts;
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
    /// URL of modal dialog window for contact editing.
    /// </summary>
    public const string CONTACT_DETAIL_DIALOG = "~/CMSModules/ContactManagement/Pages/Tools/Account/Contact_Detail.aspx";

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
            this.contactSelector.StopProcessing = value;
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
            contactSelector.IsLiveSite = value;
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
            readSiteContacts = ContactHelper.AuthorizedReadContact(CMSContext.CurrentSiteID, false);
            modifySiteContacts = ContactHelper.AuthorizedModifyContact(CMSContext.CurrentSiteID, false);
            modifySiteCG = ContactGroupHelper.AuthorizedModifyContactGroup(CMSContext.CurrentSiteID, false);
            if (siteID <= 0)
            {
                readGlobalContacts = ContactHelper.AuthorizedReadContact(UniSelector.US_GLOBAL_RECORD, false);
                modifyGlobalContacts = ContactHelper.AuthorizedModifyContact(UniSelector.US_GLOBAL_RECORD, false);
                modifyGlobalCG = ContactGroupHelper.AuthorizedModifyContactGroup(UniSelector.US_GLOBAL_RECORD, false);
            }

            // Setup unigrid
            gridElem.WhereCondition = GetWhereCondition();
            gridElem.OnAction += new OnActionEventHandler(gridElem_OnAction);
            gridElem.ZeroRowsText = GetString("om.contact.nocontacts");
            gridElem.OnBeforeDataReload += new OnBeforeDataReload(gridElem_OnBeforeDataReload);
            gridElem.OnExternalDataBound += new OnExternalDataBoundEventHandler(gridElem_OnExternalDataBound);

            modifyCombined =
                // Site contact group -> only site Contacts can be added
                    ((siteID > 0) && (modifySiteContacts || modifySiteCG))
                // Global contact group -> both site and global Contacts can be added
                    || ((siteID <= 0) && (
                // User can display only global Contacts
                    (readGlobalContacts && !readSiteContacts && (modifyGlobalContacts || modifyGlobalCG)) ||
                // User can display only site Contacts
                    (readSiteContacts && !readGlobalContacts && (modifySiteContacts || modifySiteCG)) ||
                // User can display both site and global Contacts
                    (readSiteContacts && readGlobalContacts && (modifySiteCG || modifySiteContacts) && (modifyGlobalCG || modifyGlobalContacts))
                    ));

            if (!string.IsNullOrEmpty(cgi.ContactGroupDynamicCondition))
            {
                // Set specific confirmation to remove grid action
                CMS.UIControls.UniGridConfig.Action removeAction = (CMS.UIControls.UniGridConfig.Action)gridElem.GridActions.Actions[1];
                removeAction.Confirmation = "$om.contactgroupmember.confirmremove$";
            }

            // Initialize dropdown lists
            if (!RequestHelper.IsPostBack())
            {
                // Display mass actions
                if (modifyCombined)
                {
                    drpAction.Items.Add(new ListItem(GetString("general." + Action.SelectAction), Convert.ToInt32(Action.SelectAction).ToString()));
                    drpAction.Items.Add(new ListItem(GetString("general.remove"), Convert.ToInt32(Action.Remove).ToString()));
                    drpWhat.Items.Add(new ListItem(GetString("om.contact." + What.Selected), Convert.ToInt32(What.Selected).ToString()));
                    drpWhat.Items.Add(new ListItem(GetString("om.contact." + What.All), Convert.ToInt32(What.All).ToString()));
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
            contactSelector.UniSelector.ButtonImage = GetImageUrl("/Objects/OM_Contact/add.png");
            contactSelector.ImageDialog.CssClass = "NewItemImage";
            contactSelector.UniSelector.OnItemsSelected += new EventHandler(UniSelector_OnItemsSelected);
            contactSelector.UniSelector.SelectionMode = SelectionModeEnum.MultipleButton;

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
                if (ValidationHelper.GetInteger(drv["ContactSiteID"], 0) > 0)
                {
                    btn.Enabled = modifySiteContacts || modifySiteCG;
                }
                else
                {
                    btn.Enabled = modifyGlobalContacts || modifyGlobalCG;
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
        gridElem.NamedColumns["SiteName"].Visible = !(siteID > 0) && !(siteID == UniSelector.US_GLOBAL_RECORD) && readSiteContacts;
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
            int contactId = ValidationHelper.GetInteger(actionArgument, 0);
            ContactInfo contact = ContactInfoProvider.GetContactInfo(contactId);
            if (contact != null)
            {
                // User has no permission to modify site contacts
                if (((contact.ContactSiteID > 0) && !modifySiteContacts) || !ContactGroupHelper.AuthorizedModifyContactGroup(cgi.ContactGroupSiteID, false))
                {
                    CMSPage.RedirectToCMSDeskAccessDenied("CMS.ContactManagement", "ModifyContacts");
                }
                // User has no permission to modify global contacts
                else if ((contact.ContactSiteID == 0) && !modifyGlobalContacts || !ContactGroupHelper.AuthorizedModifyContactGroup(cgi.ContactGroupSiteID, false))
                {
                    CMSPage.RedirectToCMSDeskAccessDenied("CMS.ContactManagement", "ModifyGlobalContacts");
                }
                // User has permission
                else
                {
                    // Get the relationship object
                    ContactGroupMemberInfo mi = ContactGroupMemberInfoProvider.GetContactGroupMemberInfoByData(cgi.ContactGroupID, contactId, ContactGroupMemberTypeEnum.Contact);
                    if (mi != null)
                    {
                        ContactGroupMemberInfoProvider.DeleteContactGroupMemberInfo(mi);
                    }
                }
            }

            // Check modify permission
            if ((siteID > 0) && !(CheckPermissions("cms.contactmanagement", "ModifyContactGroups")))
            {
                return;
            }
            else if ((siteID == 0) && !(CheckPermissions("cms.contactmanagement", "ModifyGlobalContactGroups")))
            {
                return;
            }
        }
    }


    /// <summary>
    /// Items changed event handler.
    /// </summary>
    protected void UniSelector_OnItemsSelected(object sender, EventArgs e)
    {
        if (modifyCombined)
        {
            // Get new items from selector
            string newValues = ValidationHelper.GetString(contactSelector.Value, null);
            string[] newItems = newValues.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            if (newItems != null)
            {
                ContactGroupMemberInfo cgmi;
                int itemId;

                // Get all selected items

                foreach (string item in newItems)
                {
                    // Check if relation already exists
                    itemId = ValidationHelper.GetInteger(item, 0);
                    cgmi = ContactGroupMemberInfoProvider.GetContactGroupMemberInfoByData(cgi.ContactGroupID, itemId, ContactGroupMemberTypeEnum.Contact);
                    if (cgmi == null)
                    {
                        ContactGroupMemberInfoProvider.SetContactGroupMemberInfo(cgi.ContactGroupID, itemId, ContactGroupMemberTypeEnum.Contact, MemberAddedHowEnum.Manual);
                    }
                    else if (!cgmi.ContactGroupMemberFromManual)
                    {
                        cgmi.ContactGroupMemberFromManual = true;
                        ContactGroupMemberInfoProvider.SetContactGroupMemberInfo(cgmi);
                    }
                }

                gridElem.ReloadData();
                pnlUpdate.Update();
                contactSelector.Value = null;
            }
        }
        // No permissions
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
                where = SqlHelperClass.GetWhereCondition<int>("ContactGroupMemberRelatedID", ContactHelper.GetSafeArray(gridElem.SelectedItems), false);
                where = SqlHelperClass.AddWhereCondition(where, "ContactGroupMemberContactGroupID = " + cgi.ContactGroupID);
                break;
            default:
                return;
        }

        // Set constraint for contact relations only
        where = SqlHelperClass.AddWhereCondition(where, "(ContactGroupMemberType = 0)");

        switch (action)
        {
            // Action 'Remove'
            case Action.Remove:
                // Delete the relations between contact group and contacts
                ContactGroupMemberInfoProvider.DeleteContactGroupMembers(where, cgi.ContactGroupID, false, false);
                resultMessage = GetString("om.contact.massaction.removed");
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
        where = SqlHelperClass.AddWhereCondition(where, "((ContactSiteID IS NULL AND ContactGlobalContactID IS NULL) OR (ContactSiteID > 0 AND ContactMergedWithContactID IS NULL))");

        // Filter site objects
        if (siteID > 0)
        {
            if (readSiteContacts)
            {
                where = SqlHelperClass.AddWhereCondition(where, "(ContactSiteID = " + siteID.ToString() + ")");
                contactSelector.SiteID = siteID;
            }
            else
            {
                CMSPage.RedirectToCMSDeskAccessDenied("CMS.ContactManagement", "ReadContacts");
            }
        }
        // Current group is global object
        else if (siteID == 0)
        {
            // In CMS Desk display current site and global objects
            if (!ContactHelper.IsSiteManager)
            {
                if (readSiteContacts && readGlobalContacts)
                {
                    where = SqlHelperClass.AddWhereCondition(where, "(ContactSiteID IS NULL) OR (ContactSiteID = " + CMSContext.CurrentSiteID + ")");
                    contactSelector.SiteID = UniSelector.US_GLOBAL_OR_SITE_RECORD;
                }
                else if (readGlobalContacts)
                {
                    where = SqlHelperClass.AddWhereCondition(where, "(ContactSiteID IS NULL)");
                    contactSelector.SiteID = UniSelector.US_GLOBAL_RECORD;
                }
                else if (readSiteContacts)
                {
                    where = SqlHelperClass.AddWhereCondition(where, "ContactSiteID = " + CMSContext.CurrentSiteID);
                    contactSelector.SiteID = CMSContext.CurrentSiteID;
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
                contactSelector.SiteID = UniSelector.US_ALL_RECORDS;
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

        // Register script to open dialogs for contact editing
        script.Append(@"
function EditContact(contactID)
{
    modalDialog('" + ResolveUrl(CONTACT_DETAIL_DIALOG) + @"?contactid=' + contactID" + sitemanagerAppend + @", 'ContactDetail', '1061px', '700px');
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
        label.innerHTML = '", GetString("om.contact.massaction.select"), @"';
    }
    else {
        switch(action) {
            case '", (int)Action.Remove, @"':
                confirmation = '", (!string.IsNullOrEmpty(cgi.ContactGroupDynamicCondition)) ? GetString("om.contactgroupmember.confirmremove") : GetString("General.ConfirmRemove"), @"';
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