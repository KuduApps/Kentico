CREATE VIEW [View_NewsletterSubscriberUserRole_Joined]
AS
SELECT Newsletter_Subscriber.* ,CMS_User.UserName, CMS_User.UserID, CMS_User.FirstName, CMS_User.MiddleName, CMS_User.LastName, CMS_User.FullName, CMS_User.Email, CMS_User.UserEnabled, 
        CMS_Role.RoleID, CMS_Role.RoleName
FROM Newsletter_Subscriber LEFT OUTER JOIN CMS_User ON (SubscriberRelatedID = UserID AND SubscriberType = 'cms.user')
      LEFT OUTER JOIN CMS_Role ON (SubscriberRelatedID = RoleID AND SubscriberType = 'cms.role')
GO
