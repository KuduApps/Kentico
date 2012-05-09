CREATE VIEW [View_OM_AccountContact_AccountJoined]
AS
SELECT     OM_Account.AccountID, OM_Account.AccountName, OM_AccountContact.ContactID, OM_AccountContact.AccountContactID, 
                      OM_AccountContact.ContactRoleID, OM_Account.AccountSiteID, OM_Account.AccountMergedWithAccountID, OM_Account.AccountGlobalAccountID, 
                      OM_Account.AccountCountryID, OM_Account.AccountStatusID
FROM         OM_AccountContact INNER JOIN
                      OM_Account ON OM_AccountContact.AccountID = OM_Account.AccountID
GO
