CREATE VIEW [View_OM_Contact_Activity]
AS
SELECT OM_Contact.*, OM_Activity.* FROM OM_Contact INNER JOIN OM_Activity ON OM_Contact.ContactID=OM_Activity.ActivityActiveContactID
GO
