CREATE VIEW [View_OM_ContactGroupMember_ContactJoined]
AS
SELECT     OM_Contact.ContactID, OM_Contact.ContactFirstName, OM_Contact.ContactMiddleName, OM_Contact.ContactLastName, 
                      OM_Contact.ContactSalutation, OM_Contact.ContactTitleBefore, OM_Contact.ContactTitleAfter, OM_Contact.ContactJobTitle, 
                      OM_Contact.ContactAddress1, OM_Contact.ContactAddress2, OM_Contact.ContactCity, OM_Contact.ContactZIP, OM_Contact.ContactStateID, 
                      OM_Contact.ContactCountryID, OM_Contact.ContactMobilePhone, OM_Contact.ContactHomePhone, OM_Contact.ContactBusinessPhone, 
                      OM_Contact.ContactEmail, OM_Contact.ContactWebSite, OM_Contact.ContactBirthday, OM_Contact.ContactGender, OM_Contact.ContactStatusID, 
                      OM_Contact.ContactNotes, OM_Contact.ContactOwnerUserID, OM_Contact.ContactMonitored, OM_Contact.ContactMergedWithContactID, 
                      OM_Contact.ContactIsAnonymous, OM_Contact.ContactSiteID, OM_Contact.ContactGUID, OM_Contact.ContactLastModified, 
                      OM_Contact.ContactCreated, OM_Contact.ContactMergedWhen, OM_Contact.ContactGlobalContactID, OM_Contact.ContactBounces, 
                      OM_Contact.ContactLastLogon, OM_Contact.ContactCampaign, OM_ContactGroupMember.ContactGroupMemberContactGroupID, 
                      OM_ContactGroupMember.ContactGroupMemberFromCondition, OM_ContactGroupMember.ContactGroupMemberFromAccount, 
                      OM_ContactGroupMember.ContactGroupMemberFromManual, OM_ContactGroupMember.ContactGroupMemberID, 
                      OM_ContactGroup.ContactGroupDisplayName, OM_ContactGroup.ContactGroupName, OM_ContactGroup.ContactGroupID, 
                      OM_ContactGroup.ContactGroupSiteID
FROM         OM_ContactGroupMember INNER JOIN
                      OM_Contact ON OM_ContactGroupMember.ContactGroupMemberRelatedID = OM_Contact.ContactID INNER JOIN
                      OM_ContactGroup ON OM_ContactGroup.ContactGroupID = OM_ContactGroupMember.ContactGroupMemberContactGroupID
WHERE     (OM_ContactGroupMember.ContactGroupMemberType = 0)
GO
