using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using CMS.CMSHelper;
using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.OnlineMarketing;
using CMS.SettingsProvider;
using System.Collections.Generic;

public partial class CMSModules_ContactManagement_Controls_UI_Account_Edit : CMSAdminEditControl
{
    #region "Variables"

    private AccountInfo ai = null;
    private int mSiteID = 0;
    private bool mergedIntoSite = false;
    private bool mergedIntoGlobal = false;
    private AccountInfo parentAI = null;

    /// <summary>
    /// URL of modal dialog window for account editing.
    /// </summary>
    private const string ACCOUNT_DETAIL_DIALOG = "~/CMSModules/ContactManagement/Pages/Tools/Contact/Account_Detail.aspx";

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
            this.EnsureChildControls();
            base.IsLiveSite = value;
            parentAccount.IsLiveSite = value;
            accountStatus.IsLiveSite = value;
            accountOwner.IsLiveSite = value;
            countrySelector.IsLiveSite = value;
            primaryContact.IsLiveSite = value;
            secondaryContact.IsLiveSite = value;
            contactRolePrimary.IsLiveSite = value;
            contactRoleSecondary.IsLiveSite = value;
            htmlNotes.IsLiveSite = value;
        }
    }


    /// <summary>
    /// SiteID of current account.
    /// </summary>
    public int SiteID
    {
        get
        {
            return mSiteID;
        }
        set
        {
            mSiteID = value;

            if ((mSiteID > 0) && !CMSContext.CurrentUser.UserSiteManagerAdmin)
            {
                mSiteID = CMSContext.CurrentSiteID;
            }
            parentAccount.SiteID = mSiteID;
            accountStatus.SiteID = mSiteID;
            primaryContact.SiteID = mSiteID;
            secondaryContact.SiteID = mSiteID;
            contactRolePrimary.SiteID = mSiteID;
            contactRoleSecondary.SiteID = mSiteID;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Init(object sender, EventArgs e)
    {
        primaryContact.UniSelector.SelectionMode = SelectionModeEnum.SingleDropDownList;
        secondaryContact.UniSelector.SelectionMode = SelectionModeEnum.SingleDropDownList;

        // Get edited object
        if (CMSContext.EditedObject != null)
        {
            ai = (AccountInfo)CMSContext.EditedObject;
        }

        InitHeaderActions();
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.StopProcessing)
        {
            this.Visible = false;
        }
        else
        {
            // Display info label that account is merged into site account
            if ((ai != null) && (mergedIntoSite))
            {
                parentAI = AccountInfoProvider.GetAccountInfo(ai.AccountMergedWithAccountID);
                lblMergedInto.ResourceString = "om.account.mergedintosite";
            }
            // Display info that account is merged into global account
            else if ((ai != null) && (mergedIntoGlobal))
            {
                parentAI = AccountInfoProvider.GetAccountInfo(ai.AccountGlobalAccountID);
                lblMergedInto.ResourceString = "om.account.mergedintoglobal";
            }
            // Don't display any info
            else
            {
                lblMergedInto.Visible = false;
            }

            // Set basic properties
            pnlGeneral.GroupingText = GetString("general.general");
            pnlAddress.GroupingText = GetString("contentmenu.address");
            pnlContacts.GroupingText = GetString("om.contact.list");
            pnlNotes.GroupingText = GetString("om.account.notes");
            btnStamp.OnClientClick = "AddStamp('" + htmlNotes.ClientID + "'); return false;";
            Reload();
            RegisterScripts();

            // Display 'changes saved' or 'splitted' label
            if (!RequestHelper.IsPostBack())
            {
                if (QueryHelper.GetBoolean("saved", false))
                {
                    DisplayInfo(GetString("general.changessaved"));
                }
                else if (QueryHelper.GetBoolean("split", false))
                {
                    DisplayInfo(GetString("om.account.splitted"));
                }
            }
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Display panel with primary/secondary contacts
        if ((CMSContext.EditedObject != null) && primaryContact.UniSelector.HasData)
        {
            pnlContacts.Visible = true;
        }
        else
        {
            pnlContacts.Visible = false;
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Reloads control.
    /// </summary>
    public void Reload()
    {
        // Load controls with data
        if (ai != null)
        {
            this.SiteID = ai.AccountSiteID;

            // Load values from DB
            if (!RequestHelper.IsPostBack())
            {
                txtName.Text = ai.AccountName;
                accountStatus.Value = ai.AccountStatusID;
                parentAccount.Value = ai.AccountSubsidiaryOfID;
                accountOwner.Value = ai.AccountOwnerUserID;
                txtAddress1.Text = ai.AccountAddress1;
                txtAddress2.Text = ai.AccountAddress2;
                txtCity.Text = ai.AccountCity;
                txtZip.Text = ai.AccountZIP;
                countrySelector.CountryID = ai.AccountCountryID;
                countrySelector.StateID = ai.AccountStateID;
                txtPhone.Text = ai.AccountPhone;
                txtFax.Text = ai.AccountFax;
                emailInput.Value = ai.AccountEmail;
                txtURL.Text = ai.AccountWebSite;
                htmlNotes.Value = ai.AccountNotes;
                primaryContact.Value = ai.AccountPrimaryContactID;
                secondaryContact.Value = ai.AccountSecondaryContactID;

                // Get primary contact role
                AccountContactInfo aci = null;
                if (ai.AccountPrimaryContactID > 0)
                {
                    aci = AccountContactInfoProvider.GetAccountContactInfo(ai.AccountID, ai.AccountPrimaryContactID);
                    if (aci != null)
                    {
                        contactRolePrimary.Value = aci.ContactRoleID;
                    }
                }
                // Get secondary contact role
                if (ai.AccountSecondaryContactID > 0)
                {
                    aci = AccountContactInfoProvider.GetAccountContactInfo(ai.AccountID, ai.AccountSecondaryContactID);
                    if (aci != null)
                    {
                        contactRoleSecondary.Value = aci.ContactRoleID;
                    }
                }
            }

            // Setup selectors
            parentAccount.WhereCondition = "(AccountID NOT IN (SELECT * FROM Func_OM_Account_GetSubsidiaries(" + ai.AccountID + ", 1)) AND AccountMergedWithAccountID IS NULL)";
            primaryContact.WhereCondition = "(ContactID IN (SELECT ContactID FROM OM_AccountContact WHERE AccountID = " + ai.AccountID + "))";
            secondaryContact.WhereCondition = "(ContactID IN (SELECT ContactID FROM OM_AccountContact WHERE AccountID = " + ai.AccountID + "))";

            // Reload controls
            parentAccount.ReloadData();
            primaryContact.ReloadData();
            secondaryContact.ReloadData();
            accountOwner.ReloadData();
            contactRolePrimary.ReloadData();
            contactRoleSecondary.ReloadData();
            accountStatus.ReloadData();
        }
    }


    /// <summary>
    /// Registers JavaScripts on page.
    /// </summary>
    private void RegisterScripts()
    {
        string stamp = null;
        if (ContactHelper.IsSiteManager)
        {
            stamp = SettingsKeyProvider.GetStringValue("CMSCMStamp");
        }
        else
        {
            stamp = SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSCMStamp");
        } 
        ScriptHelper.RegisterClientScriptBlock(this.Page, typeof(string), "AddStamp", ScriptHelper.GetScript(
@"function InsertHTML(htmlString, ckClientID)
{
    // Get the editor instance that we want to interact with.
    var oEditor = oEditor = window.CKEDITOR.instances[ckClientID];
    // Check the active editing mode.
    if (oEditor != null) {
        // Check the active editing mode.
        if (oEditor.mode == 'wysiwyg') {
            // Insert the desired HTML.
            oEditor.focus();
            oEditor.insertHtml(htmlString);        
        }
    }    
    return false;
}   

function AddStamp(ckClientID)
{
InsertHTML('<div>" + CMSContext.CurrentResolver.ResolveMacros(stamp).Replace("'", @"\'") + @"</div>', ckClientID);
}

function EditAccount(accountID)
{
    modalDialog('" + ResolveUrl(ACCOUNT_DETAIL_DIALOG) + @"?accountid=' + accountID + '&isSiteManager=" + ContactHelper.IsSiteManager + @"', 'AccountParent', '1061px', '700px');
}

"));

        if (mergedIntoSite || mergedIntoGlobal)
        {
            ltlButton.Text = string.Format(@" {0} <img class='UnigridActionButton' style='cursor:pointer;' onclick='EditAccount({1});return false;' src='{2}' title='{3}'>",
                            HTMLHelper.HTMLEncode(parentAI.AccountName.Trim()),
                            parentAI.AccountID,
                            GetImageUrl("Design/Controls/UniGrid/Actions/accountdetail.png"),
                            GetString("om.account.viewdetail"));
        }
    }


    /// <summary>
    /// Initializes header action control.
    /// </summary>
    private void InitHeaderActions()
    {
        // Find out if current account is merged into another site or global account
        if (ai != null)
        {
            mergedIntoSite = ai.AccountMergedWithAccountID != 0;
            mergedIntoGlobal = ai.AccountGlobalAccountID != 0;
            mergedIntoGlobal &= AccountHelper.AuthorizedReadAccount(UniSelector.US_GLOBAL_RECORD, false);

            if (!ContactHelper.IsSiteManager)
            {
                mergedIntoGlobal &= SettingsKeyProvider.GetBoolValue(CMSContext.CurrentSiteName + ".CMSCMGlobalAccounts");
            }
        }

        // Header actions
        string[,] actions;
        if (mergedIntoSite || mergedIntoGlobal)
        {
            actions = new string[2, 11];
        }
        else
        {
            actions = new string[1, 11];
        }
        actions[0, 0] = HeaderActions.TYPE_SAVEBUTTON;
        actions[0, 1] = GetString("General.Save");
        actions[0, 5] = GetImageUrl("CMSModules/CMS_Content/EditMenu/save.png");
        actions[0, 6] = "save";
        actions[0, 8] = "true";

        if (mergedIntoSite || mergedIntoGlobal)
        {
            actions[1, 0] = HeaderActions.TYPE_SAVEBUTTON;
            actions[1, 1] = GetString("om.contact.splitfromparent");
            actions[1, 5] = GetImageUrl("CMSModules/CMS_ContactManagement/split.png");
            actions[1, 6] = "split";
            actions[1, 8] = "false";
        }
        ((CMSPage)Page).CurrentMaster.HeaderActions.LinkCssClass = "ContentSaveLinkButton";
        ((CMSPage)Page).CurrentMaster.HeaderActions.ActionPerformed += HeaderActions_ActionPerformed;
        ((CMSPage)Page).CurrentMaster.HeaderActions.Actions = actions;
    }


    /// <summary>
    /// Actions handler.
    /// </summary>
    protected void HeaderActions_ActionPerformed(object sender, CommandEventArgs e)
    {
        AccountHelper.AuthorizedModifyAccount(this.SiteID, true);

        // Split from parent account
        if (e.CommandName == "split")
        {
            List<AccountInfo> mergedAccount = new List<AccountInfo>();
            mergedAccount.Add(ai);
            AccountHelper.Split(parentAI, mergedAccount, false, false, false);
            ScriptHelper.RefreshTabHeader(this.Page, null);

            string url = URLHelper.AddParameterToUrl(URLHelper.CurrentURL, "split", "1");
            ScriptHelper.RegisterStartupScript(this.Page, typeof(string), "RefreshPage", ScriptHelper.GetScript("window.location.replace('" + url + "');"));
        }
        // Save contact
        else
        {
            Save();
        }
    }


    /// <summary>
    /// Validates fields.
    /// </summary>
    private bool IsValid()
    {
        // Validates account name
        string errorMessage = new Validator().NotEmpty(txtName.Text.Trim(), GetString("om.account.entername")).Result;
        if (!String.IsNullOrEmpty(errorMessage.ToString()))
        {
            DisplayError(errorMessage);
            return false;
        }

        // Validates email
        if (!emailInput.IsValid())
        {
            DisplayError(emailInput.ValidationError);
            return false;
        }

        return true;
    }


    /// <summary>
    /// Save data.
    /// </summary>
    protected void Save()
    {
        if (IsValid())
        {
            bool redirect = false;

            // Create new account 
            if (ai == null)
            {
                ai = new AccountInfo();
                EditedObject = ai;
                redirect = true;
            }

            // Set values
            ai.AccountName = txtName.Text.Trim();
            ai.AccountSubsidiaryOfID = parentAccount.AccountID;
            ai.AccountOwnerUserID = ValidationHelper.GetInteger(accountOwner.Value, 0);
            ai.AccountStatusID = accountStatus.AccountStatusID;
            ai.AccountAddress1 = txtAddress1.Text.Trim();
            ai.AccountAddress2 = txtAddress2.Text.Trim();
            ai.AccountCity = txtCity.Text.Trim();
            ai.AccountZIP = txtZip.Text.Trim();
            ai.AccountCountryID = countrySelector.CountryID;
            ai.AccountStateID = countrySelector.StateID;
            ai.AccountPhone = txtPhone.Text.Trim();
            ai.AccountFax = txtFax.Text.Trim();
            ai.AccountEmail = ValidationHelper.GetString(emailInput.Value, null);
            ai.AccountWebSite = txtURL.Text.Trim();
            ai.AccountPrimaryContactID = primaryContact.ContactID;
            ai.AccountSecondaryContactID = secondaryContact.ContactID;
            ai.AccountNotes = htmlNotes.Value;
            ai.AccountSiteID = this.SiteID;

            try
            {
                // Save account changes
                AccountInfoProvider.SetAccountInfo(ai);
                if (AssignContacts())
                {
                    DisplayInfo(GetString("general.changessaved"));
                }

                RaiseOnSaved();

                // Redirect page after newly created item is saved
                if (redirect)
                {
                    string url = "Frameset.aspx?accountId=" + ai.AccountID + "&saved=1";
                    url = URLHelper.AddParameterToUrl(url, "siteid", this.SiteID.ToString());
                    if (ContactHelper.IsSiteManager)
                    {
                        url = URLHelper.AddParameterToUrl(url, "issitemanager", "1");
                    }

                    URLHelper.Redirect(url);
                }
            }
            catch (Exception e)
            {
                DisplayError(e.Message);
            }
        }
    }


    /// <summary>
    /// Sets primary and secondary contacts.
    /// </summary>
    private bool AssignContacts()
    {
        ContactInfo contact = null;
        AccountContactInfo accountContact = null;
        // Asign primary contact to account and/or assing role
        if (primaryContact.ContactID > 0)
        {
            contact = ContactInfoProvider.GetContactInfo(primaryContact.ContactID);
            if (contact != null)
            {
                // Check if contact <-> account relation is already created
                accountContact = AccountContactInfoProvider.GetAccountContactInfo(ai.AccountID, primaryContact.ContactID);

                // Update relation
                if (accountContact != null)
                {
                    accountContact.ContactRoleID = contactRolePrimary.ContactRoleID;
                }
                AccountContactInfoProvider.SetAccountContactInfo(accountContact);

            }
            // Selected contact doesn't exist
            else
            {
                DisplayError(GetString("om.contact.primarynotexists"));
                return false;
            }
        }

        // Assign secondary contact to account and/or assing role
        if (secondaryContact.ContactID > 0)
        {
            contact = ContactInfoProvider.GetContactInfo(secondaryContact.ContactID);
            if (contact != null)
            {
                // Check if contact <-> account relation is already created
                accountContact = AccountContactInfoProvider.GetAccountContactInfo(ai.AccountID, secondaryContact.ContactID);

                // Update relation
                if (accountContact != null)
                {
                    accountContact.ContactRoleID = contactRoleSecondary.ContactRoleID;
                }
                AccountContactInfoProvider.SetAccountContactInfo(accountContact);
            }
            else
            {
                DisplayError(GetString("om.contact.secondarynotexists"));
                return false;
            }
        }

        return true;
    }


    /// <summary>
    /// Displayes error message in header section of page.
    /// </summary>
    private void DisplayError(string errorMessage)
    {
        ((CMSPage)Page).ShowError(TextHelper.LimitLength(errorMessage, 200), errorMessage);
    }


    /// <summary>
    /// Displayes info label.
    /// </summary>
    /// <param name="infoMessage"></param>
    private void DisplayInfo(string infoMessage)
    {
        ((CMSPage)Page).ShowInformation(infoMessage);
    }

    #endregion
}

