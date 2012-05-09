CREATE VIEW [View_CMS_UserRoleMembershipRole]
AS
SELECT DISTINCT  * FROM
(
  SELECT CMS_Role.RoleID,CMS_Role.RoleName,CMS_Role.SiteID,CMS_UserRole.UserID,CMS_UserRole.ValidTo FROM CMS_Role
  INNER JOIN CMS_UserRole ON CMS_UserRole.RoleID = CMS_Role.RoleID --WHERE CMS_Role.RoleGroupID IS NULL
  UNION ALL 
  SELECT CMS_Role.RoleID,CMS_Role.RoleName,CMS_Role.SiteID,CMS_MembershipUser.UserID,CMS_MembershipUser.ValidTo FROM CMS_Role
  INNER JOIN CMS_MembershipRole ON CMS_MembershipRole.RoleID = CMS_Role.RoleID
  INNER JOIN CMS_MembershipUser ON CMS_MembershipUser.MembershipID= CMS_MembershipRole.MembershipID
 ) AS X  
 
GO
