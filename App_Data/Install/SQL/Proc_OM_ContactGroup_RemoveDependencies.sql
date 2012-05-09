CREATE PROCEDURE [Proc_OM_ContactGroup_RemoveDependencies]
@ID int
AS
BEGIN
SET NOCOUNT ON;
-- Contact group member
    DELETE FROM OM_ContactGroupMember WHERE ContactGroupMemberContactGroupID = @ID;
-- Scheduled task of dynamic contact groups
    DELETE FROM CMS_ScheduledTask WHERE TaskID = (SELECT ContactGroupScheduledTaskID FROM OM_ContactGroup WHERE ContactGroupID = @ID);
-- Contact group newsletter and its bindings
    -- Newsletter_SubscriberNewsletter
    DELETE FROM Newsletter_SubscriberNewsletter WHERE SubscriberID = (SELECT SubscriberID FROM Newsletter_Subscriber WHERE SubscriberRelatedID = @ID AND SubscriberType LIKE 'om.contactgroup');
    -- Newsletter_Emails
    DELETE FROM Newsletter_Emails WHERE EmailSubscriberID = (SELECT SubscriberID FROM Newsletter_Subscriber WHERE SubscriberRelatedID = @ID AND SubscriberType LIKE 'om.contactgroup');
    -- Newsletter_SubscriberLink
    DELETE FROM Newsletter_SubscriberLink WHERE SubscriberID = (SELECT SubscriberID FROM Newsletter_Subscriber WHERE SubscriberRelatedID = @ID AND SubscriberType LIKE 'om.contactgroup');
    -- Newsletter_OpenedEmail
    DELETE FROM Newsletter_OpenedEmail WHERE SubscriberID = (SELECT SubscriberID FROM Newsletter_Subscriber WHERE SubscriberRelatedID = @ID AND SubscriberType LIKE 'om.contactgroup');
    -- Newsletter_Subscriber
    DELETE FROM Newsletter_Subscriber WHERE SubscriberRelatedID = @ID AND SubscriberType LIKE 'om.contactgroup';
END
