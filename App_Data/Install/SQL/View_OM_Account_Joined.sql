CREATE VIEW [View_OM_Account_Joined]
AS
SELECT     OM_Account.AccountID, OM_Account.AccountName, OM_Account.AccountAddress1, OM_Account.AccountAddress2, OM_Account.AccountCity, 
                      OM_Account.AccountZIP, OM_Account.AccountStateID, OM_Account.AccountCountryID, OM_Account.AccountWebSite, OM_Account.AccountPhone, 
                      OM_Account.AccountEmail, OM_Account.AccountFax, OM_Account.AccountPrimaryContactID, OM_Account.AccountSecondaryContactID, 
                      OM_Account.AccountStatusID, OM_Account.AccountNotes, OM_Account.AccountOwnerUserID, OM_Account.AccountSubsidiaryOfID, 
                      OM_Account.AccountMergedWithAccountID, OM_Account.AccountSiteID, OM_Account.AccountGUID, OM_Account.AccountLastModified, 
                      OM_Account.AccountCreated, OM_Account.AccountGlobalAccountID, PrimaryContact.ContactFirstName AS PrimaryContactFirstName, 
                      PrimaryContact.ContactMiddleName AS PrimaryContactMiddleName, PrimaryContact.ContactLastName AS PrimaryContactLastName, 
                      SecondaryContact.ContactFirstName AS SecondaryContactFirstName, SecondaryContact.ContactMiddleName AS SecondaryContactMiddleName, 
                      SecondaryContact.ContactLastName AS SecondaryContactLastName, CMS_User.FullName, SubsidiaryOf.AccountName AS SubsidiaryOfName, 
                      ISNULL(PrimaryContact.ContactFirstName, '') + CASE PrimaryContact.ContactFirstName WHEN '' THEN '' WHEN NULL 
                      THEN '' ELSE ' ' END + ISNULL(PrimaryContact.ContactMiddleName, '') + CASE PrimaryContact.ContactMiddleName WHEN '' THEN '' WHEN NULL 
                      THEN '' ELSE ' ' END + ISNULL(PrimaryContact.ContactLastName, '') AS PrimaryContactFullName, ISNULL(SecondaryContact.ContactFirstName, '') 
                      + CASE SecondaryContact.ContactFirstName WHEN '' THEN '' WHEN NULL THEN '' ELSE ' ' END + ISNULL(SecondaryContact.ContactMiddleName, '') 
                      + CASE SecondaryContact.ContactMiddleName WHEN '' THEN '' WHEN NULL THEN '' ELSE ' ' END + ISNULL(SecondaryContact.ContactLastName, '') 
                      AS SecondaryContactFullName, ISNULL(OM_Account.AccountAddress1, '') + CASE OM_Account.AccountAddress1 WHEN '' THEN '' WHEN NULL 
                      THEN '' ELSE ' ' END + OM_Account.AccountAddress2 AS AccountFullAddress
FROM         OM_Account LEFT OUTER JOIN
                      OM_Contact AS PrimaryContact ON OM_Account.AccountPrimaryContactID = PrimaryContact.ContactID LEFT OUTER JOIN
                      OM_Contact AS SecondaryContact ON OM_Account.AccountSecondaryContactID = SecondaryContact.ContactID LEFT OUTER JOIN
                      CMS_User ON OM_Account.AccountOwnerUserID = CMS_User.UserID LEFT OUTER JOIN
                      OM_Account AS SubsidiaryOf ON OM_Account.AccountSubsidiaryOfID = SubsidiaryOf.AccountID
GO
