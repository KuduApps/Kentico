CREATE TABLE [CMS_UserRole] (
		[UserID]         [int] NOT NULL,
		[RoleID]         [int] NOT NULL,
		[ValidTo]        [datetime] NULL,
		[UserRoleID]     [int] IDENTITY(1, 1) NOT NULL
) 
ALTER TABLE [CMS_UserRole]
	ADD
	CONSTRAINT [PK_CMS_UserRole]
	PRIMARY KEY
	CLUSTERED
	([UserRoleID])
	
CREATE NONCLUSTERED INDEX [IX_CMS_UserRole_UserID]
	ON [CMS_UserRole] ([UserID])
	INCLUDE ([RoleID], [ValidTo])
	
ALTER TABLE [CMS_UserRole]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_UserRole_RoleID_CMS_Role]
	FOREIGN KEY ([RoleID]) REFERENCES [CMS_Role] ([RoleID])
ALTER TABLE [CMS_UserRole]
	CHECK CONSTRAINT [FK_CMS_UserRole_RoleID_CMS_Role]
ALTER TABLE [CMS_UserRole]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_UserRole_UserID_CMS_User]
	FOREIGN KEY ([UserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [CMS_UserRole]
	CHECK CONSTRAINT [FK_CMS_UserRole_UserID_CMS_User]
