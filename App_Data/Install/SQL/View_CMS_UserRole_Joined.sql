CREATE VIEW [View_CMS_UserRole_Joined]
AS
SELECT     CMS_UserRole.UserID, CMS_UserRole.RoleID, CMS_UserRole.ValidTo, CMS_User.UserName,CMS_User.FullName, CMS_User.UserGUID, CMS_Role.RoleName, CMS_Role.RoleDisplayName,
                      CMS_Role.RoleGUID, CMS_Role.RoleGroupID,CMS_Site.SiteID, CMS_Site.SiteName, CMS_Site.SiteGUID
FROM         CMS_Role INNER JOIN
                      CMS_UserRole ON CMS_Role.RoleID = CMS_UserRole.RoleID INNER JOIN
                      CMS_User ON CMS_UserRole.UserID = CMS_User.UserID LEFT OUTER JOIN
                      CMS_Site ON CMS_Role.SiteID = CMS_Site.SiteID 
GO
