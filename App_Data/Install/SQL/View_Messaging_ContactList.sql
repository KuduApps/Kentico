CREATE VIEW [View_Messaging_ContactList]
AS
SELECT     UserNickName, UserName, FullName, ContactListUserID, ContactListContactUserID
FROM       View_CMS_User INNER JOIN
           Messaging_ContactList ON Messaging_ContactList.ContactListContactUserID = UserID
GO
