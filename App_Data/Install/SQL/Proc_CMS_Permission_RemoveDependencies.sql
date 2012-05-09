CREATE PROCEDURE [Proc_CMS_Permission_RemoveDependencies] 
    @ID int
AS
BEGIN
    DELETE FROM Forums_ForumRoles WHERE PermissionID = @ID
    DELETE FROM CMS_RolePermission WHERE PermissionID = @ID    
    DELETE FROM Community_GroupRolePermission WHERE PermissionID = @ID    
    DELETE FROM Media_LibraryRolePermission WHERE PermissionID = @ID
    DELETE FROM CMS_WidgetRole WHERE PermissionID = @ID;
END
