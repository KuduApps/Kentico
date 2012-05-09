CREATE TABLE [Forums_ForumRoles] (
		[ForumID]          [int] NOT NULL,
		[RoleID]           [int] NOT NULL,
		[PermissionID]     [int] NOT NULL
) 
ALTER TABLE [Forums_ForumRoles]
	ADD
	CONSTRAINT [PK_Forums_ForumRoles]
	PRIMARY KEY
	CLUSTERED
	([ForumID], [RoleID], [PermissionID])
	
	
ALTER TABLE [Forums_ForumRoles]
	WITH CHECK
	ADD CONSTRAINT [FK_Forums_ForumRoles_ForumID_Forums_Forum]
	FOREIGN KEY ([ForumID]) REFERENCES [Forums_Forum] ([ForumID])
ALTER TABLE [Forums_ForumRoles]
	CHECK CONSTRAINT [FK_Forums_ForumRoles_ForumID_Forums_Forum]
ALTER TABLE [Forums_ForumRoles]
	WITH CHECK
	ADD CONSTRAINT [FK_Forums_ForumRoles_PermissionID_CMS_Permission]
	FOREIGN KEY ([PermissionID]) REFERENCES [CMS_Permission] ([PermissionID])
ALTER TABLE [Forums_ForumRoles]
	CHECK CONSTRAINT [FK_Forums_ForumRoles_PermissionID_CMS_Permission]
ALTER TABLE [Forums_ForumRoles]
	WITH CHECK
	ADD CONSTRAINT [FK_Forums_ForumRoles_RoleID_CMS_Role]
	FOREIGN KEY ([RoleID]) REFERENCES [CMS_Role] ([RoleID])
ALTER TABLE [Forums_ForumRoles]
	CHECK CONSTRAINT [FK_Forums_ForumRoles_RoleID_CMS_Role]
