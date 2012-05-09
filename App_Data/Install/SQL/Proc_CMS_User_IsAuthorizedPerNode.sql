CREATE PROCEDURE [Proc_CMS_User_IsAuthorizedPerNode]
@UserID int, @NodeID int, @PermissionName nvarchar(100), @GenericRoles nvarchar(200), @SiteID int, @Date DateTime
AS
DECLARE @SelectedUserID int;
SELECT TOP 1 @SelectedUserID = CMS_User.UserID
FROM CMS_User
INNER JOIN View_CMS_UserRoleMembershipRole AS CMS_Role ON  CMS_Role.UserID = CMS_User.UserID
INNER JOIN CMS_RolePermission on CMS_RolePermission.RoleID = CMS_Role.RoleID
INNER JOIN CMS_Permission on CMS_Permission.PermissionID = CMS_RolePermission.PermissionID
INNER JOIN CMS_Class on CMS_Class.ClassID = CMS_Permission.ClassID
INNER JOIN CMS_Tree on CMS_Tree.NodeClassID = CMS_Class.ClassID
WHERE CMS_Tree.NodeID = @NodeID
AND CMS_User.UserID = @UserID
AND CMS_Permission.PermissionName = @PermissionName
AND (CMS_Role.SiteID = @SiteID OR CMS_Role.SiteID IS NULL)
AND (CMS_Role.ValidTo > @Date OR CMS_Role.ValidTo IS NULL)
-- Try to process special roles
IF (@SelectedUserID IS NULL) OR (@SelectedUserID = 0)
BEGIN
    DECLARE @SQL varchar(600)
    SET @SQL = 
    'SELECT TOP 1 CMS_Role.RoleID
    FROM CMS_Role
    INNER JOIN CMS_RolePermission on CMS_RolePermission.RoleID = CMS_Role.RoleID
    INNER JOIN CMS_Permission on CMS_Permission.PermissionID = CMS_RolePermission.PermissionID
    INNER JOIN CMS_Class on CMS_Class.ClassID = CMS_Permission.ClassID
    INNER JOIN CMS_Tree on CMS_Tree.NodeClassID = CMS_Class.ClassID
    WHERE CMS_Tree.NodeID = ' + CAST(@NodeID AS nvarchar) + ' AND CMS_Permission.PermissionName =''' + @PermissionName + 
    ''' AND CMS_Role.RoleName IN (' + @GenericRoles + ') AND (CMS_Role.SiteID IS NULL OR CMS_Role.SiteID=' + CAST(@SiteID AS nvarchar)+')'
    EXEC(@SQL)
END
ELSE
SELECT @SelectedUserID
