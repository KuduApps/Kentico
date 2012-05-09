CREATE TABLE [Media_LibraryRolePermission] (
		[LibraryID]        [int] NOT NULL,
		[RoleID]           [int] NOT NULL,
		[PermissionID]     [int] NOT NULL
) 
ALTER TABLE [Media_LibraryRolePermission]
	ADD
	CONSTRAINT [PK_Media_LibraryRolePermission]
	PRIMARY KEY
	CLUSTERED
	([LibraryID], [RoleID], [PermissionID])
	
	
ALTER TABLE [Media_LibraryRolePermission]
	WITH CHECK
	ADD CONSTRAINT [FK_Media_LibraryRolePermission_LibraryID_Media_Library]
	FOREIGN KEY ([LibraryID]) REFERENCES [Media_Library] ([LibraryID])
ALTER TABLE [Media_LibraryRolePermission]
	CHECK CONSTRAINT [FK_Media_LibraryRolePermission_LibraryID_Media_Library]
ALTER TABLE [Media_LibraryRolePermission]
	WITH CHECK
	ADD CONSTRAINT [FK_Media_LibraryRolePermission_PermissionID_CMS_Permission]
	FOREIGN KEY ([PermissionID]) REFERENCES [CMS_Permission] ([PermissionID])
ALTER TABLE [Media_LibraryRolePermission]
	CHECK CONSTRAINT [FK_Media_LibraryRolePermission_PermissionID_CMS_Permission]
ALTER TABLE [Media_LibraryRolePermission]
	WITH CHECK
	ADD CONSTRAINT [FK_Media_LibraryRolePermission_RoleID_CMS_Role]
	FOREIGN KEY ([RoleID]) REFERENCES [CMS_Role] ([RoleID])
ALTER TABLE [Media_LibraryRolePermission]
	CHECK CONSTRAINT [FK_Media_LibraryRolePermission_RoleID_CMS_Role]
