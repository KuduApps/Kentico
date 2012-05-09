CREATE TABLE [CMS_RolePermission] (
		[RoleID]           [int] NOT NULL,
		[PermissionID]     [int] NOT NULL
) 
ALTER TABLE [CMS_RolePermission]
	ADD
	CONSTRAINT [PK_CMS_RolePermission]
	PRIMARY KEY
	CLUSTERED
	([RoleID], [PermissionID])
	
	
ALTER TABLE [CMS_RolePermission]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_RolePermission_PermissionID_CMS_Permission]
	FOREIGN KEY ([PermissionID]) REFERENCES [CMS_Permission] ([PermissionID])
ALTER TABLE [CMS_RolePermission]
	CHECK CONSTRAINT [FK_CMS_RolePermission_PermissionID_CMS_Permission]
ALTER TABLE [CMS_RolePermission]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_RolePermission_RoleID_CMS_Role]
	FOREIGN KEY ([RoleID]) REFERENCES [CMS_Role] ([RoleID])
ALTER TABLE [CMS_RolePermission]
	CHECK CONSTRAINT [FK_CMS_RolePermission_RoleID_CMS_Role]
