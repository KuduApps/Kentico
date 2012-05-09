-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_CMS_Resource_RemoveDependencies]
	@ID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	BEGIN TRANSACTION;    
    DELETE FROM [CMS_ResourceSite] WHERE ResourceID = @ID;
	
	-- Permissions
    DELETE FROM [Forums_ForumRoles]				WHERE PermissionID IN (SELECT PermissionID FROM [CMS_Permission] WHERE ResourceID = @ID);
    DELETE FROM [CMS_RolePermission]			WHERE PermissionID IN (SELECT PermissionID FROM [CMS_Permission] WHERE ResourceID = @ID);
    DELETE FROM [Community_GroupRolePermission]	WHERE PermissionID IN (SELECT PermissionID FROM [CMS_Permission] WHERE ResourceID = @ID);
    DELETE FROM [Media_LibraryRolePermission]	WHERE PermissionID IN (SELECT PermissionID FROM [CMS_Permission] WHERE ResourceID = @ID);
    DELETE FROM [CMS_WidgetRole]				WHERE PermissionID IN (SELECT PermissionID FROM [CMS_Permission] WHERE ResourceID = @ID);
	DELETE FROM [CMS_Permission] WHERE ResourceID = @ID;
    -- UI elements
    DELETE FROM [CMS_RoleUIElement] WHERE ElementID IN (SELECT ElementID FROM CMS_UIElement WHERE ElementResourceID = @ID);
    DELETE FROM [CMS_UIElement] WHERE ElementResourceID = @ID;
    
    UPDATE [CMS_WebPart] SET WebPartResourceID = NULL WHERE WebPartResourceID = @ID;
    UPDATE [CMS_FormUserControl] SET UserControlResourceID = NULL WHERE UserControlResourceID = @ID;
    UPDATE [CMS_InlineControl] SET ControlResourceID = NULL WHERE ControlResourceID = @ID;
    UPDATE [CMS_ScheduledTask] SET TaskResourceID = NULL WHERE TaskResourceID = @ID;
        
	COMMIT TRANSACTION;
END
