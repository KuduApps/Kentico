CREATE TABLE [Community_GroupRolePermission] (
		[GroupID]          [int] NOT NULL,
		[RoleID]           [int] NOT NULL,
		[PermissionID]     [int] NOT NULL
) 
ALTER TABLE [Community_GroupRolePermission]
	ADD
	CONSTRAINT [PK_Community_GroupRolePermission]
	PRIMARY KEY
	CLUSTERED
	([GroupID], [RoleID], [PermissionID])
	
	
ALTER TABLE [Community_GroupRolePermission]
	ADD
	CONSTRAINT [DEFAULT_community_GroupRolePermission_PermissionID]
	DEFAULT ((0)) FOR [PermissionID]
ALTER TABLE [Community_GroupRolePermission]
	ADD
	CONSTRAINT [DEFAULT_community_GroupRolePermission_RoleID]
	DEFAULT ((0)) FOR [RoleID]
ALTER TABLE [Community_GroupRolePermission]
	WITH CHECK
	ADD CONSTRAINT [FK_community_GroupRolePermission_GroupID_Community_Group]
	FOREIGN KEY ([GroupID]) REFERENCES [Community_Group] ([GroupID])
ALTER TABLE [Community_GroupRolePermission]
	CHECK CONSTRAINT [FK_community_GroupRolePermission_GroupID_Community_Group]
ALTER TABLE [Community_GroupRolePermission]
	WITH CHECK
	ADD CONSTRAINT [FK_community_GroupRolePermission_PermissionID_CMS_Permission]
	FOREIGN KEY ([PermissionID]) REFERENCES [CMS_Permission] ([PermissionID])
ALTER TABLE [Community_GroupRolePermission]
	CHECK CONSTRAINT [FK_community_GroupRolePermission_PermissionID_CMS_Permission]
ALTER TABLE [Community_GroupRolePermission]
	WITH CHECK
	ADD CONSTRAINT [FK_community_GroupRolePermission_RoleID_CMS_Role]
	FOREIGN KEY ([RoleID]) REFERENCES [CMS_Role] ([RoleID])
ALTER TABLE [Community_GroupRolePermission]
	CHECK CONSTRAINT [FK_community_GroupRolePermission_RoleID_CMS_Role]
