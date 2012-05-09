CREATE VIEW [View_OM_AccountContact_ContactJoined]
AS
SELECT     OM_Contact.ContactID, OM_Contact.ContactFirstName, OM_Contact.ContactMiddleName, OM_Contact.ContactLastName, 
                      OM_Contact.ContactEmail, OM_Contact.ContactSiteID, OM_Contact.ContactMergedWithContactID, OM_Contact.ContactGlobalContactID, 
                      OM_AccountContact.AccountID, OM_AccountContact.AccountContactID, OM_Contact.ContactCountryID, OM_Contact.ContactStatusID, 
                      OM_AccountContact.ContactRoleID
FROM         OM_AccountContact INNER JOIN
                      OM_Contact ON OM_AccountContact.ContactID = OM_Contact.ContactID
GO
