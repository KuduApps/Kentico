CREATE TABLE [PM_ProjectRolePermission] (
		[ProjectID]        [int] NOT NULL,
		[RoleID]           [int] NOT NULL,
		[PermissionID]     [int] NOT NULL
) 
ALTER TABLE [PM_ProjectRolePermission]
	ADD
	CONSTRAINT [PK_PM_ProjectRolePermission_RoleID_PermissionID]
	PRIMARY KEY
	CLUSTERED
	([ProjectID], [RoleID], [PermissionID])
	
ALTER TABLE [PM_ProjectRolePermission]
	WITH CHECK
	ADD CONSTRAINT [FK_PM_ProjectRolePermission_PermissionID_CMS_Permission]
	FOREIGN KEY ([PermissionID]) REFERENCES [CMS_Permission] ([PermissionID])
ALTER TABLE [PM_ProjectRolePermission]
	CHECK CONSTRAINT [FK_PM_ProjectRolePermission_PermissionID_CMS_Permission]
ALTER TABLE [PM_ProjectRolePermission]
	WITH CHECK
	ADD CONSTRAINT [FK_PM_ProjectRolePermission_ProjectID_PM_Project]
	FOREIGN KEY ([ProjectID]) REFERENCES [PM_Project] ([ProjectID])
ALTER TABLE [PM_ProjectRolePermission]
	CHECK CONSTRAINT [FK_PM_ProjectRolePermission_ProjectID_PM_Project]
ALTER TABLE [PM_ProjectRolePermission]
	WITH CHECK
	ADD CONSTRAINT [FK_PM_ProjectRolePermission_RoleID_CMS_Role]
	FOREIGN KEY ([RoleID]) REFERENCES [CMS_Role] ([RoleID])
ALTER TABLE [PM_ProjectRolePermission]
	CHECK CONSTRAINT [FK_PM_ProjectRolePermission_RoleID_CMS_Role]
