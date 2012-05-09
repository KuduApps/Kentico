CREATE PROCEDURE  [Proc_Media_Library_SetPermissions]
	-- Add the parameters for the stored procedure here
	@MediaLibraryID int,
	@RolesIDs text,
	@PermissionsIDs text 
	
AS
BEGIN
	SET NOCOUNT ON;
	
	-- Position varaibles for split functionality
	DECLARE @PosRoles int;
	DECLARE @NewPosRoles int;
	DECLARE @PosPerm int;
	DECLARE @NewPosPerm int;
	
	-- Separator
	DECLARE @SplitChar char;
	
	-- Helper variables for particular IDs
	DECLARE @RoleID int;
	DECLARE @PermID int;
	
	-- Initialization
	SET @SplitChar = ';'
	SET @PosRoles = 1
	
	WHILE(@PosRoles IS NOT NULL AND @PosRoles != 0)
	BEGIN
		SET @NewPosRoles = CHARINDEX(@SplitChar,@RolesIDs,@PosRoles)
		IF (@NewPosRoles IS NOT NULL AND @NewPosRoles != 0)
		BEGIN
			SET @RoleID = SUBSTRING(@RolesIDs,@PosRoles,@NewPosRoles - @PosRoles)
			IF (@RoleID > 0)
			BEGIN
				SET @PosPerm = 1
				WHILE(@PosPerm IS NOT NULL AND @PosPerm != 0)
				BEGIN
					SET @NewPosPerm = CHARINDEX(@SplitChar,@PermissionsIDs,@PosPerm)
					IF (@NewPosPerm IS NOT NULL AND @NewPosPerm != 0)
					BEGIN
						SET @PermID = SUBSTRING(@PermissionsIDs,@PosPerm,@NewPosPerm - @PosPerm)
						IF(@PermID > 0)
						BEGIN
							INSERT INTO Media_LibraryRolePermission ([LibraryID], [RoleID], [PermissionID]) VALUES (@MediaLibraryID, @RoleID, @PermID);
						END
						SET @PosPerm = @NewPosPerm + 1			
					END
					ELSE
						SET @PosPerm = 0
				END
			END
			SET @PosRoles = @NewPosRoles + 1			
		END
		ELSE
			SET @PosRoles = 0
	END
END
