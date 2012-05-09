CREATE VIEW [View_OM_ContactGroupMember_AccountJoined]
AS
SELECT     OM_Account.AccountID, OM_Account.AccountName, OM_Account.AccountAddress1, OM_Account.AccountAddress2, OM_Account.AccountCity, 
                      OM_Account.AccountZIP, OM_Account.AccountStateID, OM_Account.AccountCountryID, OM_Account.AccountWebSite, OM_Account.AccountPhone, 
                      OM_Account.AccountEmail, OM_Account.AccountFax, OM_Account.AccountPrimaryContactID, OM_Account.AccountSecondaryContactID, 
                      OM_Account.AccountStatusID, OM_Account.AccountNotes, OM_Account.AccountOwnerUserID, OM_Account.AccountSubsidiaryOfID, 
                      OM_Account.AccountMergedWithAccountID, OM_Account.AccountSiteID, OM_Account.AccountGUID, OM_Account.AccountLastModified, 
                      OM_Account.AccountCreated, OM_Account.AccountGlobalAccountID, OM_ContactGroupMember.ContactGroupMemberContactGroupID, 
                      OM_ContactGroupMember.ContactGroupMemberID
FROM         OM_ContactGroupMember INNER JOIN
                      OM_Account ON OM_ContactGroupMember.ContactGroupMemberRelatedID = OM_Account.AccountID
WHERE     (OM_ContactGroupMember.ContactGroupMemberType = 1)
GO
