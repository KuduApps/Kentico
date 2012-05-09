CREATE VIEW [View_CMS_SiteRoleResourceUIElementValid_Joined]
AS
SELECT X.RoleName, X.RoleID, X.ElementName, X.SiteName, 
                      X.ResourceName, X.RoleSiteID,UserID, X.ValidTo FROM
(
SELECT      CMS_Role.RoleName, CMS_Role.RoleID, CMS_UIElement.ElementName, CMS_Site.SiteName, 
                      CMS_Resource.ResourceName, CMS_Role.SiteID AS 'RoleSiteID',CMS_UserRole.UserID, ValidTo
FROM         CMS_Role INNER JOIN
                      CMS_RoleUIElement ON CMS_Role.RoleID = CMS_RoleUIElement.RoleID LEFT JOIN
                      CMS_Site ON (CMS_Role.SiteID = CMS_Site.SiteID) INNER JOIN
                      CMS_UIElement ON CMS_RoleUIElement.ElementID = CMS_UIElement.ElementID INNER JOIN
                      CMS_Resource ON CMS_UIElement.ElementResourceID = CMS_Resource.ResourceID LEFT OUTER JOIN
                      CMS_UserRole ON CMS_UserRole.RoleID = CMS_Role.RoleID                                         
			
UNION ALL SELECT 
				RoleTable.RoleName, RoleTable.RoleID, CMS_UIElement.ElementName, CMS_Site.SiteName, 
                    CMS_Resource.ResourceName, RoleTable.SiteID AS 'RoleSiteID',CMS_MembershipUser.UserID, ValidTo 
              FROM    CMS_Role AS RoleTable INNER JOIN                     
                      CMS_RoleUIElement ON RoleTable.RoleID = CMS_RoleUIElement.RoleID LEFT JOIN
                      CMS_Site ON (RoleTable.SiteID = CMS_Site.SiteID) INNER JOIN
                      CMS_UIElement ON CMS_RoleUIElement.ElementID = CMS_UIElement.ElementID INNER JOIN
                      CMS_Resource ON CMS_UIElement.ElementResourceID = CMS_Resource.ResourceID 
                      INNER JOIN CMS_MembershipUser  ON
						 CMS_MembershipUser.MembershipID IN (
											SELECT MembershipID FROM CMS_MembershipRole 
											WHERE CMS_MembershipRole.RoleID = RoleTable.RoleID 
                      )                    		
) AS X
GO
