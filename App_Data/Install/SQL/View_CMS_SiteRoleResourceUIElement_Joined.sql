CREATE VIEW [View_CMS_SiteRoleResourceUIElement_Joined]
AS
SELECT     CMS_Role.RoleName, CMS_Role.RoleID, CMS_UIElement.ElementName, CMS_Site.SiteName, 
                      CMS_Resource.ResourceName, CMS_Role.SiteID AS 'RoleSiteID'
FROM         CMS_Role INNER JOIN
                      CMS_RoleUIElement ON CMS_Role.RoleID = CMS_RoleUIElement.RoleID LEFT JOIN
                      CMS_Site ON (CMS_Role.SiteID = CMS_Site.SiteID) INNER JOIN
                      CMS_UIElement ON CMS_RoleUIElement.ElementID = CMS_UIElement.ElementID INNER JOIN
                      CMS_Resource ON CMS_UIElement.ElementResourceID = CMS_Resource.ResourceID
GO
