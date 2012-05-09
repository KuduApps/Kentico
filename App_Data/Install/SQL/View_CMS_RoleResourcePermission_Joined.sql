CREATE VIEW [View_CMS_RoleResourcePermission_Joined]
AS
SELECT     CMS_RolePermission.RoleID, CMS_Resource.ResourceName, CMS_Permission.PermissionName
FROM         CMS_RolePermission INNER JOIN
                      CMS_Permission ON CMS_Permission.PermissionID = CMS_RolePermission.PermissionID INNER JOIN
                      CMS_Resource ON CMS_Permission.ResourceID = CMS_Resource.ResourceID
GO
