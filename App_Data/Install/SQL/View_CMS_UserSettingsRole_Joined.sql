CREATE VIEW [View_CMS_UserSettingsRole_Joined]
AS
SELECT     CMS_UserRole.UserID, 
           CMS_UserRole.RoleID, 
           CMS_User.UserName, 
           CMS_User.FullName, 
           CMS_User.Email, 
           CMS_Role.RoleName,
           CMS_Role.RoleDisplayName,
           CMS_Role.RoleDescription,
           CMS_Site.SiteID, 
           CMS_Site.SiteName,
           ISNULL(CMS_UserSettings.UserBounces, 0) AS UserBounces
           
FROM       CMS_UserRole 
		   INNER JOIN
           CMS_Role ON CMS_UserRole.RoleID = CMS_Role.RoleID 
           INNER JOIN
           CMS_User ON CMS_UserRole.UserID = CMS_User.UserID 
           INNER JOIN
           CMS_Site ON CMS_Role.SiteID = CMS_Site.SiteID
           INNER JOIN
           CMS_UserSettings ON CMS_UserRole.UserID = CMS_UserSettings.UserSettingsUserID
GO
