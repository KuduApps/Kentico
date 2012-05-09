using System;
using System.Data;
using System.Linq;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.DataEngine;
using CMS.OnlineMarketing;
using CMS.SettingsProvider;
using CMS.WebAnalytics;

[Title(Text = "Contact management", ImageUrl = "Objects/OM_ContactManagement/object.png")]
public partial class CMSAPIExamples_Code_Modules_ContactManagement_Default : CMSAPIExamplePage
{
    #region "Initialization"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Contact role
        this.apiCreateContactRole.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateContactRole);
        this.apiGetAndUpdateContactRole.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateContactRole);
        this.apiGetAndBulkUpdateContactRoles.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateContactRoles);
        this.apiDeleteContactRole.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteContactRole);

        // Contact status
        this.apiCreateContactStatus.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateContactStatus);
        this.apiGetAndUpdateContactStatus.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateContactStatus);
        this.apiGetAndBulkUpdateContactStatuses.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateContactStatuses);
        this.apiDeleteContactStatus.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteContactStatus);

        // Account status
        this.apiCreateAccountStatus.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateAccountStatus);
        this.apiGetAndUpdateAccountStatus.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateAccountStatus);
        this.apiGetAndBulkUpdateAccountStatuses.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateAccountStatuses);
        this.apiDeleteAccountStatus.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteAccountStatus);

        // Contact
        this.apiCreateContact.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateContact);
        this.apiGetAndUpdateContact.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateContact);
        this.apiGetAndBulkUpdateContacts.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateContacts);
        this.apiDeleteContact.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteContact);

        // Contact status
        this.apiAddContactStatusToContact.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(AddContactStatusToContact);
        this.apiRemoveContactStatusFromContact.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(RemoveContactStatusFromContact);

        // Contact membership
        this.apiAddMembership.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(AddMembership);
        this.apiRemoveMembership.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(RemoveMembership);

        // Contact IP address
        this.apiAddIPAddress.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(AddIPAddress);
        this.apiRemoveIPAddress.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(RemoveIPAddress);

        // Contact user agent
        this.apiAddUserAgent.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(AddUserAgent);
        this.apiRemoveUserAgent.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(RemoveUserAgent);

        // Account
        this.apiCreateAccount.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateAccount);
        this.apiGetAndUpdateAccount.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateAccount);
        this.apiGetAndBulkUpdateAccounts.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateAccounts);
        this.apiDeleteAccount.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteAccount);

        // Account status
        this.apiAddAccountStatusToAccount.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(AddAccountStatusToAccount);
        this.apiRemoveAccountStatusFromAccount.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(RemoveAccountStatusFromAccount);

        // Account contacts
        this.apiAddContactToAccount.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(AddContactToAccount);
        this.apiRemoveContactFromAccount.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(RemoveContactFromAccount);

        // Contact group
        this.apiCreateContactGroup.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateContactGroup);
        this.apiGetAndUpdateContactGroup.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateContactGroup);
        this.apiGetAndBulkUpdateContactGroups.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateContactGroups);
        this.apiDeleteContactGroup.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteContactGroup);

        // Contact in contact group
        this.apiAddContactToGroup.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(AddContactToGroup);
        this.apiRemoveContactFromGroup.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(RemoveContactFromGroup);

        // Account in contact group
        this.apiAddAccountToGroup.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(AddAccountToGroup);
        this.apiRemoveAccountFromGroup.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(RemoveAccountFromGroup);

        // Activity
        this.apiCreateActivity.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateActivity);
        this.apiGetAndUpdateActivity.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateActivity);
        this.apiGetAndBulkUpdateActivities.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateActivities);
        this.apiDeleteActivity.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteActivity);
    }

    #endregion


    #region "Mass actions"

    /// <summary>
    /// Runs all creating and managing examples.
    /// </summary>
    public override void RunAll()
    {
        base.RunAll();

        // Contact role
        this.apiCreateContactRole.Run();
        this.apiGetAndUpdateContactRole.Run();
        this.apiGetAndBulkUpdateContactRoles.Run();

        // Contact status
        this.apiCreateContactStatus.Run();
        this.apiGetAndUpdateContactStatus.Run();
        this.apiGetAndBulkUpdateContactStatuses.Run();

        // Account status
        this.apiCreateAccountStatus.Run();
        this.apiGetAndUpdateAccountStatus.Run();
        this.apiGetAndBulkUpdateAccountStatuses.Run();

        // Contact
        this.apiCreateContact.Run();
        this.apiGetAndUpdateContact.Run();
        this.apiGetAndBulkUpdateContacts.Run();

        // Contact with contact status
        this.apiAddContactStatusToContact.Run();

        // Contact memebership
        this.apiAddMembership.Run();

        // Contact IP address
        this.apiAddIPAddress.Run();

        // Contact user agent
        this.apiAddUserAgent.Run();

        // Account
        this.apiCreateAccount.Run();
        this.apiGetAndUpdateAccount.Run();
        this.apiGetAndBulkUpdateAccounts.Run();

        // Account with account status
        this.apiAddAccountStatusToAccount.Run();

        // Account with contact
        this.apiAddContactToAccount.Run();

        // Contact group
        this.apiCreateContactGroup.Run();
        this.apiGetAndUpdateContactGroup.Run();
        this.apiGetAndBulkUpdateContactGroups.Run();

        // Contact in group
        this.apiAddContactToGroup.Run();

        // Account in group
        this.apiAddAccountToGroup.Run();

        // Activity
        this.apiCreateActivity.Run();
        this.apiGetAndUpdateActivity.Run();
        this.apiGetAndBulkUpdateActivities.Run();
    }


    /// <summary>
    /// Runs all cleanup examples.
    /// </summary>
    public override void CleanUpAll()
    {
        base.CleanUpAll();

        // Activity
        this.apiDeleteActivity.Run();

        // Contact group
        this.apiRemoveContactFromGroup.Run();
        this.apiRemoveAccountFromGroup.Run();
        this.apiDeleteContactGroup.Run();

        // Account
        this.apiRemoveContactFromAccount.Run();
        this.apiRemoveAccountStatusFromAccount.Run();
        this.apiDeleteAccount.Run();

        // Contact
        this.apiRemoveIPAddress.Run();
        this.apiRemoveUserAgent.Run();
        this.apiRemoveMembership.Run();
        this.apiRemoveContactStatusFromContact.Run();
        this.apiDeleteContact.Run();

        // Account status
        this.apiDeleteAccountStatus.Run();

        // Contact status
        this.apiDeleteContactStatus.Run();

        // Contact role
        this.apiDeleteContactRole.Run();
    }

    #endregion


    #region "API examples - Contact role"

    /// <summary>
    /// Creates contact role. Called when the "Create role" button is pressed.
    /// </summary>
    private bool CreateContactRole()
    {
        // Create new contact role object
        ContactRoleInfo newRole = new ContactRoleInfo()
            {
                ContactRoleDisplayName = "My new role",
                ContactRoleName = "MyNewRole",
                ContactRoleSiteID = CMSContext.CurrentSiteID
            };

        // Save the contact role
        ContactRoleInfoProvider.SetContactRoleInfo(newRole);

        return true;
    }


    /// <summary>
    /// Gets and updates contact role. Called when the "Get and update role" button is pressed.
    /// Expects the CreateContactRole method to be run first.
    /// </summary>
    private bool GetAndUpdateContactRole()
    {
        // Get the contact role
        ContactRoleInfo updateRole = ContactRoleInfoProvider.GetContactRoleInfo("MyNewRole", CMSContext.CurrentSiteName);
        if (updateRole != null)
        {
            // Update a property
            updateRole.ContactRoleDisplayName = updateRole.ContactRoleDisplayName.ToLower();

            // Save the changes
            ContactRoleInfoProvider.SetContactRoleInfo(updateRole);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates contact roles. Called when the "Get and bulk update roles" button is pressed.
    /// Expects the CreateContactRole method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateContactRoles()
    {
        // Get the contact roles dataset
        string where = "ContactRoleName LIKE N'MyNewRole%'";
        InfoDataSet<ContactRoleInfo> roles = ContactRoleInfoProvider.GetContactRoles(where, null);

        if (!DataHelper.DataSourceIsEmpty(roles))
        {
            foreach (ContactRoleInfo role in roles)
            {
                // Update the properties
                role.ContactRoleDisplayName = role.ContactRoleDisplayName.ToUpper();

                // Save the changes
                ContactRoleInfoProvider.SetContactRoleInfo(role);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes contact role. Called when the "Delete role" button is pressed.
    /// Expects the CreateContactRole method to be run first.
    /// </summary>
    private bool DeleteContactRole()
    {
        // Get the contact role
        ContactRoleInfo deleteRole = ContactRoleInfoProvider.GetContactRoleInfo("MyNewRole", CMSContext.CurrentSiteName);

        if (deleteRole != null)
        {
            // Delete the contact role
            ContactRoleInfoProvider.DeleteContactRoleInfo(deleteRole);

            return true;
        }

        return false;
    }

    #endregion


    #region "API examples - Contact status"

    /// <summary>
    /// Creates contact status. Called when the "Create status" button is pressed.
    /// </summary>
    private bool CreateContactStatus()
    {
        // Create new contact status object
        ContactStatusInfo newStatus = new ContactStatusInfo()
            {
                ContactStatusDisplayName = "My new status",
                ContactStatusName = "MyNewStatus",
                ContactStatusSiteID = CMSContext.CurrentSiteID
            };

        // Save the contact status
        ContactStatusInfoProvider.SetContactStatusInfo(newStatus);

        return true;
    }


    /// <summary>
    /// Gets and updates contact status. Called when the "Get and update status" button is pressed.
    /// Expects the CreateContactStatus method to be run first.
    /// </summary>
    private bool GetAndUpdateContactStatus()
    {
        // Get the contact status
        ContactStatusInfo updateStatus = ContactStatusInfoProvider.GetContactStatusInfo("MyNewStatus", CMSContext.CurrentSiteName);
        if (updateStatus != null)
        {
            // Update a property
            updateStatus.ContactStatusDisplayName = updateStatus.ContactStatusDisplayName.ToLower();

            // Save the changes
            ContactStatusInfoProvider.SetContactStatusInfo(updateStatus);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates contact statuses. Called when the "Get and bulk update statuses" button is pressed.
    /// Expects the CreateContactStatus method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateContactStatuses()
    {
        // Get the contact statuses dataset
        string where = "ContactStatusName LIKE N'MyNewStatus%'";
        InfoDataSet<ContactStatusInfo> statuses = ContactStatusInfoProvider.GetContactStatuses(where, null);

        if (!DataHelper.DataSourceIsEmpty(statuses))
        {
            foreach (ContactStatusInfo contactStatus in statuses)
            {
                // Update a property
                contactStatus.ContactStatusDisplayName = contactStatus.ContactStatusDisplayName.ToUpper();

                // Save the changes
                ContactStatusInfoProvider.SetContactStatusInfo(contactStatus);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes contact status. Called when the "Delete status" button is pressed.
    /// Expects the CreateContactStatus method to be run first.
    /// </summary>
    private bool DeleteContactStatus()
    {
        // Get the contact status
        ContactStatusInfo deleteStatus = ContactStatusInfoProvider.GetContactStatusInfo("MyNewStatus", CMSContext.CurrentSiteName);

        if (deleteStatus != null)
        {
            // Delete the contact status
            ContactStatusInfoProvider.DeleteContactStatusInfo(deleteStatus);

            return true;
        }

        return false;
    }

    #endregion


    #region "API examples - Account status"

    /// <summary>
    /// Creates account status. Called when the "Create status" button is pressed.
    /// </summary>
    private bool CreateAccountStatus()
    {
        // Create new account status object
        AccountStatusInfo newStatus = new AccountStatusInfo()
            {
                AccountStatusDisplayName = "My new status",
                AccountStatusName = "MyNewStatus",
                AccountStatusSiteID = CMSContext.CurrentSiteID
            };

        // Save the account status
        AccountStatusInfoProvider.SetAccountStatusInfo(newStatus);

        return true;
    }


    /// <summary>
    /// Gets and updates account status. Called when the "Get and update status" button is pressed.
    /// Expects the CreateAccountStatus method to be run first.
    /// </summary>
    private bool GetAndUpdateAccountStatus()
    {
        // Get the account status
        AccountStatusInfo updateStatus = AccountStatusInfoProvider.GetAccountStatusInfo("MyNewStatus", CMSContext.CurrentSiteName);
        if (updateStatus != null)
        {
            // Update a property
            updateStatus.AccountStatusDisplayName = updateStatus.AccountStatusDisplayName.ToLower();

            // Save the changes
            AccountStatusInfoProvider.SetAccountStatusInfo(updateStatus);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates account statuses. Called when the "Get and bulk update statuses" button is pressed.
    /// Expects the CreateAccountStatus method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateAccountStatuses()
    {
        // Get the account status dataset
        string where = "AccountStatusName LIKE N'MyNewStatus%'";
        InfoDataSet<AccountStatusInfo> statuses = AccountStatusInfoProvider.GetAccountStatuses(where, null);

        if (!DataHelper.DataSourceIsEmpty(statuses))
        {
            foreach (AccountStatusInfo accountStatus in statuses)
            {
                // Update a property
                accountStatus.AccountStatusDisplayName = accountStatus.AccountStatusDisplayName.ToUpper();

                // Save the changes
                AccountStatusInfoProvider.SetAccountStatusInfo(accountStatus);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes account status. Called when the "Delete status" button is pressed.
    /// Expects the CreateAccountStatus method to be run first.
    /// </summary>
    private bool DeleteAccountStatus()
    {
        // Get the account status
        AccountStatusInfo deleteStatus = AccountStatusInfoProvider.GetAccountStatusInfo("MyNewStatus", CMSContext.CurrentSiteName);

        if (deleteStatus != null)
        {
            // Delete the account status
            AccountStatusInfoProvider.DeleteAccountStatusInfo(deleteStatus);

            return true;
        }

        return false;
    }

    #endregion


    #region "API examples - Contact"

    /// <summary>
    /// Creates contact. Called when the "Create contact" button is pressed.
    /// </summary>
    private bool CreateContact()
    {
        // Create new contact object
        ContactInfo newContact = new ContactInfo()
            {
                ContactLastName = "My New Contact",
                ContactFirstName = "My New Firstname",
                ContactSiteID = CMSContext.CurrentSiteID,
                ContactIsAnonymous = true
            };

        // Save the contact
        ContactInfoProvider.SetContactInfo(newContact);

        return true;
    }


    /// <summary>
    /// Gets and updates contact. Called when the "Get and update contact" button is pressed.
    /// Expects the CreateContact method to be run first.
    /// </summary>
    private bool GetAndUpdateContact()
    {
        // Get dataset of contacts
        string where = "ContactLastName LIKE N'My New Contact%'";
        InfoDataSet<ContactInfo> contacts = ContactInfoProvider.GetContacts(where, null, 1, null);

        if (!DataHelper.DataSourceIsEmpty(contacts))
        {
            // Get the contact from dataset
            ContactInfo contact = contacts.First<ContactInfo>();

            // Update a property
            contact.ContactLastName = contact.ContactLastName.ToLower();

            // Save the changes
            ContactInfoProvider.SetContactInfo(contact);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates contacts. Called when the "Get and bulk update contacts" button is pressed.
    /// Expects the CreateContact method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateContacts()
    {
        // Get dataset of contacts
        string where = "ContactLastName LIKE N'My New Contact%'";
        InfoDataSet<ContactInfo> contacts = ContactInfoProvider.GetContacts(where, null);

        if (!DataHelper.DataSourceIsEmpty(contacts))
        {
            foreach (ContactInfo contact in contacts)
            {
                // Update a property of each contact
                contact.ContactLastName = contact.ContactLastName.ToUpper();

                // And save them
                ContactInfoProvider.SetContactInfo(contact);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Adds contact status to contact. Called when the "Add contact status to contact" button is pressed.
    /// Expects the CreateContact and CreateContactStatus methods to be run first.
    /// </summary>
    private bool AddContactStatusToContact()
    {
        // Get dataset of contacts
        string where = "ContactLastName LIKE N'My New Contact%'";
        InfoDataSet<ContactInfo> contacts = ContactInfoProvider.GetContacts(where, null, 1, null);

        // Get the contact status
        ContactStatusInfo contactStatus = ContactStatusInfoProvider.GetContactStatusInfo("MyNewStatus", CMSContext.CurrentSiteName);

        if (!DataHelper.DataSourceIsEmpty(contacts) && (contactStatus != null))
        {
            // Get the contact from dataset
            ContactInfo contact = contacts.First<ContactInfo>();

            // If relationship doesn't already exist
            if (contact.ContactStatusID != contactStatus.ContactStatusID)
            {
                // Add contact status to contact
                contact.ContactStatusID = contactStatus.ContactStatusID;

                // Save the changes
                ContactInfoProvider.SetContactInfo(contact);

                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Removes contact status from contact. Called when the "Remove status from contact" button is pressed.
    /// Expects the CreateContact, CreateContactStatus and AddContactStatusToContact methods to be run first.
    /// </summary>
    private bool RemoveContactStatusFromContact()
    {
        // Get dataset of contacts
        string where = "ContactLastName LIKE N'My New Contact%'";
        InfoDataSet<ContactInfo> contacts = ContactInfoProvider.GetContacts(where, null, 1, null);

        // Get the contact status
        ContactStatusInfo contactStatus = ContactStatusInfoProvider.GetContactStatusInfo("MyNewStatus", CMSContext.CurrentSiteName);

        if (!DataHelper.DataSourceIsEmpty(contacts) && (contactStatus != null))
        {
            // Get the contact from dataset
            ContactInfo contact = contacts.First<ContactInfo>();

            // If relationship exists
            if (contact.ContactStatusID == contactStatus.ContactStatusID)
            {
                // Remove the status
                contact.ContactStatusID = 0;

                // Save the changes
                ContactInfoProvider.SetContactInfo(contact);

                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Adds current user to contact. Called when the "Add membership to contact" button is pressed.
    /// Expects the CreateContact method to be run first.
    /// </summary>
    private bool AddMembership()
    {
        // Get dataset of contacts
        string where = "ContactLastName LIKE N'My New Contact%'";
        InfoDataSet<ContactInfo> contacts = ContactInfoProvider.GetContacts(where, null, 1, null);

        if (!DataHelper.DataSourceIsEmpty(contacts))
        {
            // Get the contact from dataset
            ContactInfo contact = contacts.First<ContactInfo>();

            // Set relationship to user
            CMS.OnlineMarketing.MembershipInfoProvider.SetRelationship(
                CMSContext.CurrentUser.UserID,
                MemberTypeEnum.CmsUser,
                contact.ContactID,
                contact.ContactID,
                false);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Removes current user from contact. Called when the "Remove membership from contact" button is pressed.
    /// Expects the CreateContact and AddMembership methods to be run first.
    /// </summary>
    private bool RemoveMembership()
    {
        // Get dataset of contacts
        string where = "ContactLastName LIKE N'My New Contact%'";
        InfoDataSet<ContactInfo> contacts = ContactInfoProvider.GetContacts(where, null, 1, null);

        if (!DataHelper.DataSourceIsEmpty(contacts))
        {
            // Get the contact from dataset
            ContactInfo contact = contacts.First<ContactInfo>();

            // Get the membership
            CMS.OnlineMarketing.MembershipInfo membership = CMS.OnlineMarketing.MembershipInfoProvider.GetMembershipInfo(contact.ContactID, contact.ContactID, CMSContext.CurrentUser.UserID, MemberTypeEnum.CmsUser);

            // Delete the membership
            CMS.OnlineMarketing.MembershipInfoProvider.DeleteRelationship(membership.MembershipID);

            return (membership != null);
        }

        return false;
    }


    /// <summary>
    /// Adds IP address to contact. Called when the "Add IP to contact" button is pressed.
    /// Expects the CreateContact method to be run first.
    /// </summary>
    private bool AddIPAddress()
    {
        // Get dataset of contacts
        string where = "ContactLastName LIKE N'My New Contact%'";
        InfoDataSet<ContactInfo> contacts = ContactInfoProvider.GetContacts(where, null, 1, null);

        if (!DataHelper.DataSourceIsEmpty(contacts))
        {
            // Get the contact from dataset
            ContactInfo contact = contacts.First<ContactInfo>();

            // Create new IP address
            IPInfo newIP = new IPInfo()
                {
                    IPAddress = "127.0.0.1",
                    IPOriginalContactID = contact.ContactID,
                    IPActiveContactID = contact.ContactID
                };

            // Save the IP info
            IPInfoProvider.SetIPInfo(newIP);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Removes IP address from contact. Called when the "Remove IP from contact" button is pressed.
    /// Expects the CreateContact and AddIPAddress methods to be run first.
    /// </summary>
    private bool RemoveIPAddress()
    {
        // Get dataset of contacts
        string where = "ContactLastName LIKE N'My New Contact%'";
        InfoDataSet<ContactInfo> contacts = ContactInfoProvider.GetContacts(where, null, 1, null);

        if (!DataHelper.DataSourceIsEmpty(contacts))
        {
            // Get the contact from dataset
            ContactInfo contact = contacts.First<ContactInfo>();

            // Get contact's IP
            where = String.Format("IPOriginalContactID = '{0}' AND IPAddress = '{1}'", contact.ContactID, "127.0.0.1");
            InfoDataSet<IPInfo> deleteIPs = IPInfoProvider.GetIps(where, null, 1, "IPID");

            if (!DataHelper.DataSourceIsEmpty(deleteIPs))
            {
                // Delete IP
                IPInfoProvider.DeleteIPInfo(deleteIPs.First<IPInfo>());

                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Adds user agent to contact. Called when the "Add user agent to contact" button is pressed.
    /// Expects the CreateContact method to be run first.
    /// </summary>
    private bool AddUserAgent()
    {
        // Get dataset of contacts
        string where = "ContactLastName LIKE N'My New Contact%'";
        InfoDataSet<ContactInfo> contacts = ContactInfoProvider.GetContacts(where, null, 1, null);

        if (!DataHelper.DataSourceIsEmpty(contacts))
        {
            // Get the contact from dataset
            ContactInfo contact = contacts.First<ContactInfo>();

            // Create new agent info
            UserAgentInfo agentInfo = new UserAgentInfo()
                {
                    UserAgentActiveContactID = contact.ContactID,
                    UserAgentOriginalContactID = contact.ContactID,
                    UserAgentString = "My User Agent"
                };

            // Save the agent info
            UserAgentInfoProvider.SetUserAgentInfo(agentInfo);

            return true;
        }

        return false;
    }



    /// <summary>
    /// Removes user agent from contact. Called when the "Remove user agent from contact" button is pressed.
    /// Expects the CreateContact and AddUserAgent methods to be run first.
    /// </summary>
    private bool RemoveUserAgent()
    {
        // Get dataset of contacts
        string where = "ContactLastName LIKE N'My New Contact%'";
        InfoDataSet<ContactInfo> contacts = ContactInfoProvider.GetContacts(where, null, 1, null);

        if (!DataHelper.DataSourceIsEmpty(contacts))
        {
            // Get the contact from dataset
            ContactInfo contact = contacts.First<ContactInfo>();

            // Get the user agent info
            where = String.Format("UserAgentOriginalContactID = '{0}' AND UserAgentString = '{1}'", contact.ContactID, "My User Agent");
            InfoDataSet<UserAgentInfo> deleteAgents = UserAgentInfoProvider.GetUserAgents(where, null, 1, null);

            if (!DataHelper.DataSourceIsEmpty(deleteAgents))
            {
                // Delete the user agent info
                UserAgentInfoProvider.DeleteUserAgentInfo(deleteAgents.First<UserAgentInfo>());

                return true;
            }
        }

        return false;
    }



    /// <summary>
    /// Deletes contact. Called when the "Delete contact" button is pressed.
    /// Expects the CreateContact method to be run first.
    /// </summary>
    private bool DeleteContact()
    {
        // Get dataset of contacts
        string where = "ContactLastName LIKE N'My New Contact%'";
        InfoDataSet<ContactInfo> contacts = ContactInfoProvider.GetContacts(where, null, 1, null);

        if (!DataHelper.DataSourceIsEmpty(contacts))
        {
            // Get the contact from dataset
            ContactInfo contact = contacts.First<ContactInfo>();

            // Delete the contact
            ContactInfoProvider.DeleteContactInfo(contact);

            return true;
        }

        return false;
    }

    #endregion


    #region "API examples - Account"

    /// <summary>
    /// Creates account. Called when the "Create account" button is pressed.
    /// </summary>
    private bool CreateAccount()
    {
        // Create new account object
        AccountInfo newAccount = new AccountInfo()
            {
                AccountName = "My New Account",
                AccountSiteID = CMSContext.CurrentSiteID
            };

        // Save the account
        AccountInfoProvider.SetAccountInfo(newAccount);

        return true;
    }


    /// <summary>
    /// Gets and updates account. Called when the "Get and update account" button is pressed.
    /// Expects the CreateAccount method to be run first.
    /// </summary>
    private bool GetAndUpdateAccount()
    {
        // Get the account
        AccountInfo updateAccount = AccountInfoProvider.GetAccountInfo("My New Account", CMSContext.CurrentSiteName);

        if (updateAccount != null)
        {
            // Update a property
            updateAccount.AccountName = updateAccount.AccountName.ToLower();

            // And save it
            AccountInfoProvider.SetAccountInfo(updateAccount);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates accounts. Called when the "Get and bulk update accounts" button is pressed.
    /// Expects the CreateAccount method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateAccounts()
    {
        // Get dataset of accounts
        string where = "AccountName LIKE N'My New Account%'";
        InfoDataSet<AccountInfo> accounts = AccountInfoProvider.GetAccounts(where, null);

        if (!DataHelper.DataSourceIsEmpty(accounts))
        {
            foreach (AccountInfo account in accounts)
            {
                // Update each one's property
                account.AccountName = account.AccountName.ToUpper();

                // And save it
                AccountInfoProvider.SetAccountInfo(account);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates accounts. Called when the "Get and bulk update accounts" button is pressed.
    /// Expects the CreateAccount method to be run first.
    /// </summary>
    private bool AddAccountStatusToAccount()
    {
        // Get the account
        AccountInfo account = AccountInfoProvider.GetAccountInfo("My New Account", CMSContext.CurrentSiteName);

        // Get the account status
        AccountStatusInfo accountStatus = AccountStatusInfoProvider.GetAccountStatusInfo("MyNewStatus", CMSContext.CurrentSiteName);

        if ((account != null) && (accountStatus != null))
        {
            // Check that account doesn't have this status 
            if (account.AccountStatusID != accountStatus.AccountStatusID)
            {
                // Set new status
                account.AccountStatusID = accountStatus.AccountStatusID;

                // Save changes to the object
                AccountInfoProvider.SetAccountInfo(account);

                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Removes account status from account. Called when the "Remove status from account" button is pressed.
    /// Expects the CreateAccount, CreateAccountStatus and AddAccountStatusToAccount methods to be run first.
    /// </summary>
    private bool RemoveAccountStatusFromAccount()
    {
        // Get the account
        AccountInfo account = AccountInfoProvider.GetAccountInfo("My New Account", CMSContext.CurrentSiteName);

        // Get the account status
        AccountStatusInfo accountStatus = AccountStatusInfoProvider.GetAccountStatusInfo("MyNewStatus", CMSContext.CurrentSiteName);

        if ((account != null) && (accountStatus != null))
        {
            // Check if account has this status set
            if (account.AccountStatusID == accountStatus.AccountStatusID)
            {
                // Remove the status from account
                account.AccountStatusID = 0;

                // Save the object
                AccountInfoProvider.SetAccountInfo(account);

                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Adds contact under account.
    /// </summary>
    private bool AddContactToAccount()
    {
        // Get dataset of contacts
        string where = "ContactLastName LIKE N'My New Contact%'";
        InfoDataSet<ContactInfo> contacts = ContactInfoProvider.GetContacts(where, null, 1, null);

        // Get the account
        AccountInfo account = AccountInfoProvider.GetAccountInfo("My New Account", CMSContext.CurrentSiteName);

        // Get the role
        ContactRoleInfo role = ContactRoleInfoProvider.GetContactRoleInfo("MyNewRole", CMSContext.CurrentSiteName);

        if (!DataHelper.DataSourceIsEmpty(contacts) && (account != null) && (role != null))
        {
            // Get the contact from dataset
            ContactInfo contact = contacts.First<ContactInfo>();

            // Create new account - contact relationship
            AccountContactInfo accountContact = new AccountContactInfo()
                {
                    AccountID = account.AccountID,
                    ContactID = contact.ContactID,
                    ContactRoleID = role.ContactRoleID
                };

            // And save it
            AccountContactInfoProvider.SetAccountContactInfo(accountContact);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Removes contact from account.
    /// </summary>
    private bool RemoveContactFromAccount()
    {
        // Get dataset of contacts
        string where = "ContactLastName LIKE N'My New Contact%'";
        InfoDataSet<ContactInfo> contacts = ContactInfoProvider.GetContacts(where, null, 1, null);

        // Get the account
        AccountInfo account = AccountInfoProvider.GetAccountInfo("My New Account", CMSContext.CurrentSiteName);

        if (!DataHelper.DataSourceIsEmpty(contacts) && (account != null))
        {
            // Get the contact from dataset
            ContactInfo contact = contacts.First<ContactInfo>();

            // Find account - contact relationship
            AccountContactInfo accountContact = AccountContactInfoProvider.GetAccountContactInfo(account.AccountID, contact.ContactID);

            if (accountContact != null)
            {
                // Delete the object
                AccountContactInfoProvider.DeleteAccountContactInfo(accountContact);

                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Deletes account. Called when the "Delete account" button is pressed.
    /// Expects the CreateAccount method to be run first.
    /// </summary>
    private bool DeleteAccount()
    {
        // Get the account
        AccountInfo deleteAccount = AccountInfoProvider.GetAccountInfo("My New Account", CMSContext.CurrentSiteName);

        if (deleteAccount != null)
        {
            // Delete the account
            AccountInfoProvider.DeleteAccountInfo(deleteAccount);

            return true;
        }

        return false;
    }

    #endregion


    #region "API examples - Contact group"

    /// <summary>
    /// Creates contact group. Called when the "Create group" button is pressed.
    /// </summary>
    private bool CreateContactGroup()
    {
        // Create new contact group object
        ContactGroupInfo newGroup = new ContactGroupInfo()
            {
                ContactGroupDisplayName = "My new group",
                ContactGroupName = "MyNewGroup",
                ContactGroupSiteID = CMSContext.CurrentSiteID,
                ContactGroupDynamicCondition = "{%Contact.ContactLastName.Contains(\"My new\")%}"
            };

        // Save the contact group to database
        ContactGroupInfoProvider.SetContactGroupInfo(newGroup);

        return true;
    }


    /// <summary>
    /// Gets and updates contact group. Called when the "Get and update group" button is pressed.
    /// Expects the CreateContactGroup method to be run first.
    /// </summary>
    private bool GetAndUpdateContactGroup()
    {
        // Get the contact group
        ContactGroupInfo updateGroup = ContactGroupInfoProvider.GetContactGroupInfo("MyNewGroup", CMSContext.CurrentSiteName);
        if (updateGroup != null)
        {
            // Update contact group's properties
            updateGroup.ContactGroupDisplayName = updateGroup.ContactGroupDisplayName.ToLower();

            // Save the contact group
            ContactGroupInfoProvider.SetContactGroupInfo(updateGroup);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates contact groups. Called when the "Get and bulk update groups" button is pressed.
    /// Expects the CreateContactGroup method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateContactGroups()
    {
        // Get the contact groups
        string where = "ContactGroupName LIKE N'MyNewGroup%'";
        InfoDataSet<ContactGroupInfo> groups = ContactGroupInfoProvider.GetContactGroups(where, null);

        if (!DataHelper.DataSourceIsEmpty(groups))
        {
            foreach (ContactGroupInfo group in groups)
            {
                // Update a property
                group.ContactGroupDisplayName = group.ContactGroupDisplayName.ToUpper();

                // Save the contact group
                ContactGroupInfoProvider.SetContactGroupInfo(group);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Adds contact to group. Called when the "Add contact to group" button is pressed.
    /// Exepects the CreateContact and CreateContactGroup methods to be run first.
    /// </summary>
    private bool AddContactToGroup()
    {
        // Get dataset of contacts
        string where = "ContactLastName LIKE N'My New Contact%'";
        InfoDataSet<ContactInfo> contacts = ContactInfoProvider.GetContacts(where, null, 1, null);

        // Get the contact group
        ContactGroupInfo group = ContactGroupInfoProvider.GetContactGroupInfo("MyNewGroup", CMSContext.CurrentSiteName);

        if (!DataHelper.DataSourceIsEmpty(contacts) && (group != null))
        {
            // Get the contact from dataset
            ContactInfo contact = contacts.First<ContactInfo>();

            // Create the contact - contactgroup relationship
            ContactGroupMemberInfo newContactGroupMember = new ContactGroupMemberInfo()
                {
                    ContactGroupMemberContactGroupID = group.ContactGroupID,
                    ContactGroupMemberType = ContactGroupMemberTypeEnum.Contact,
                    ContactGroupMemberRelatedID = contact.ContactID,
                    ContactGroupMemberFromManual = true
                };

            // Save the contact group
            ContactGroupMemberInfoProvider.SetContactGroupMemberInfo(newContactGroupMember);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Removes contact from group. Called when the "Remove contact from group" button is pressed.
    /// Expects the CreateContact, CreateContactGroup and AddContactToGroup methods to be run first.
    /// </summary>
    private bool RemoveContactFromGroup()
    {
        // Get dataset of contacts
        string where = "ContactLastName LIKE N'My New Contact%'";
        InfoDataSet<ContactInfo> contacts = ContactInfoProvider.GetContacts(where, null, 1, null);

        // Get the contact group
        ContactGroupInfo group = ContactGroupInfoProvider.GetContactGroupInfo("MyNewGroup", CMSContext.CurrentSiteName);

        if (!DataHelper.DataSourceIsEmpty(contacts) && (group != null))
        {
            // Get the contact from dataset
            ContactInfo contact = contacts.First<ContactInfo>();

            // Get the contact - contactgroup relationship
            ContactGroupMemberInfo deleteContactGroupMember = ContactGroupMemberInfoProvider.GetContactGroupMemberInfoByData(group.ContactGroupID, contact.ContactID, ContactGroupMemberTypeEnum.Contact);

            if (deleteContactGroupMember != null)
            {
                // Delete the info
                ContactGroupMemberInfoProvider.DeleteContactGroupMemberInfo(deleteContactGroupMember);

                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Adds account to group. Called when the "Add account to group" button is pressed.
    /// Expects the CreateAccount and CreateGroup methods to be run first.
    /// </summary>
    private bool AddAccountToGroup()
    {
        // Get the account
        AccountInfo account = AccountInfoProvider.GetAccountInfo("My New Account", CMSContext.CurrentSiteName);

        // Get the contact group
        ContactGroupInfo group = ContactGroupInfoProvider.GetContactGroupInfo("MyNewGroup", CMSContext.CurrentSiteName);

        if ((account != null) && (group != null))
        {
            // Create new account - contact group relationship
            ContactGroupMemberInfo newContactGroupMember = new ContactGroupMemberInfo()
                {
                    ContactGroupMemberContactGroupID = group.ContactGroupID,
                    ContactGroupMemberType = ContactGroupMemberTypeEnum.Account,
                    ContactGroupMemberRelatedID = account.AccountID
                };

            // Save the object
            ContactGroupMemberInfoProvider.SetContactGroupMemberInfo(newContactGroupMember);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Removes account from group. Called when the "Remove account from group" button is pressed.
    /// Expects the CreateAccount, CreateGroup and AddContactToGroup methods to be run first.
    /// </summary>
    private bool RemoveAccountFromGroup()
    {
        // Get the account
        AccountInfo account = AccountInfoProvider.GetAccountInfo("My New Account", CMSContext.CurrentSiteName);

        // Get the contact group
        ContactGroupInfo group = ContactGroupInfoProvider.GetContactGroupInfo("MyNewGroup", CMSContext.CurrentSiteName);

        if ((account != null) && (group != null))
        {
            // Get the account - contactgroup relationship
            ContactGroupMemberInfo deleteContactGroupMember = ContactGroupMemberInfoProvider.GetContactGroupMemberInfoByData(group.ContactGroupID, account.AccountID, ContactGroupMemberTypeEnum.Account);

            if (deleteContactGroupMember != null)
            {
                // Delete the info
                ContactGroupMemberInfoProvider.DeleteContactGroupMemberInfo(deleteContactGroupMember);

                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Deletes contact group. Called when the "Delete group" button is pressed.
    /// Expects the CreateContactGroup method to be run first.
    /// </summary>
    private bool DeleteContactGroup()
    {
        // Get the contact group
        ContactGroupInfo deleteGroup = ContactGroupInfoProvider.GetContactGroupInfo("MyNewGroup", CMSContext.CurrentSiteName);

        if (deleteGroup != null)
        {
            // Delete the contact group
            ContactGroupInfoProvider.DeleteContactGroupInfo(deleteGroup);

            return true;
        }

        return false;
    }

    #endregion


    #region "API examples - Activity"

    /// <summary>
    /// Creates activity. Called when the "Create activity" button is pressed.
    /// </summary>
    private bool CreateActivity()
    {
        // Get dataset of contacts
        string where = "ContactLastName LIKE N'My New Contact%'";
        InfoDataSet<ContactInfo> contacts = ContactInfoProvider.GetContacts(where, null, 1, null);

        if (!DataHelper.DataSourceIsEmpty(contacts))
        {
            // Get the contact from dataset
            ContactInfo contact = contacts.First<ContactInfo>();

            // Get an activity type
            ActivityTypeInfo activityType = ActivityTypeInfoProvider.GetActivityTypes(null, null, 1, null).First<ActivityTypeInfo>();

            // Create new activity object
            ActivityInfo newActivity = new ActivityInfo()
                {
                    ActivityType = activityType.ActivityTypeName,
                    ActivityTitle = "My new activity",
                    ActivitySiteID = CMSContext.CurrentSiteID,
                    ActivityOriginalContactID = contact.ContactID,
                    ActivityActiveContactID = contact.ContactID
                };

            // Save the activity
            ActivityInfoProvider.SetActivityInfo(newActivity);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and updates activity. Called when the "Get and update activity" button is pressed.
    /// Expects the CreateActivity method to be run first.
    /// </summary>
    private bool GetAndUpdateActivity()
    {
        // Get dataset of contacts
        string where = "ContactLastName LIKE N'My New Contact%'";
        InfoDataSet<ContactInfo> contacts = ContactInfoProvider.GetContacts(where, null, 1, null);

        if (!DataHelper.DataSourceIsEmpty(contacts))
        {
            // Get the contact from dataset
            ContactInfo contact = contacts.First<ContactInfo>();

            // Get all activities associated with user
            where = String.Format("ActivityActiveContactID = '{0}'", contact.ContactID);
            InfoDataSet<ActivityInfo> updateActivities = ActivityInfoProvider.GetActivities(where, null);

            if (!DataHelper.DataSourceIsEmpty(updateActivities))
            {
                // Get just the first activity
                ActivityInfo activity = updateActivities.First<ActivityInfo>();

                // Update the activity
                activity.ActivityTitle = activity.ActivityTitle.ToLower();

                // Save the activity
                ActivityInfoProvider.SetActivityInfo(activity);

                return true;
            }
        }

        return false;
    }
    /// <summary>
    /// Gets and bulk updates activities. Called when the "Get and bulk update activities" button is pressed.
    /// Expects the CreateActivity method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateActivities()
    {
        // Get dataset of contacts
        string where = "ContactLastName LIKE N'My New Contact%'";
        InfoDataSet<ContactInfo> contacts = ContactInfoProvider.GetContacts(where, null, 1, null);

        if (!DataHelper.DataSourceIsEmpty(contacts))
        {
            // Get the contact from dataset
            ContactInfo contact = contacts.First<ContactInfo>();

            // Get all activities associated with contact
            where = String.Format("ActivityActiveContactID = '{0}'", contact.ContactID);
            InfoDataSet<ActivityInfo> updateActivities = ActivityInfoProvider.GetActivities(where, null);

            if (!DataHelper.DataSourceIsEmpty(updateActivities))
            {

                foreach (ActivityInfo activity in updateActivities)
                {
                    // Update activity content
                    activity.ActivityTitle = activity.ActivityTitle.ToUpper();

                    // Save the activity
                    ActivityInfoProvider.SetActivityInfo(activity);
                }

                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Deletes activity. Called when the "Delete activity" button is pressed.
    /// Expects the CreateActivity method to be run first.
    /// </summary>
    private bool DeleteActivity()
    {
        // Get dataset of contacts
        string where = "ContactLastName LIKE N'My New Contact%'";
        InfoDataSet<ContactInfo> contacts = ContactInfoProvider.GetContacts(where, null, 1, null);

        if (!DataHelper.DataSourceIsEmpty(contacts))
        {
            // Get the contact from dataset
            ContactInfo contact = contacts.First<ContactInfo>();

            // Get all activities associated with contact
            where = String.Format("ActivityOriginalContactID = '{0}'", contact.ContactID);
            InfoDataSet<ActivityInfo> activities = ActivityInfoProvider.GetActivities(where, null);

            if (!DataHelper.DataSourceIsEmpty(activities))
            {
                foreach (ActivityInfo activity in activities)
                {
                    // Delete the object
                    ActivityInfoProvider.DeleteActivityInfo(activity);
                }

                return true;
            }
        }

        return false;
    }

    #endregion

}
