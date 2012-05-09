CREATE TABLE [CMS_WidgetRole] (
		[WidgetID]         [int] NOT NULL,
		[RoleID]           [int] NOT NULL,
		[PermissionID]     [int] NOT NULL
) 
ALTER TABLE [CMS_WidgetRole]
	ADD
	CONSTRAINT [PK_CMS_WidgetRole]
	PRIMARY KEY
	CLUSTERED
	([WidgetID], [RoleID], [PermissionID])
	
	
ALTER TABLE [CMS_WidgetRole]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_WidgetRole_PermissionID_CMS_Permission]
	FOREIGN KEY ([PermissionID]) REFERENCES [CMS_Permission] ([PermissionID])
ALTER TABLE [CMS_WidgetRole]
	CHECK CONSTRAINT [FK_CMS_WidgetRole_PermissionID_CMS_Permission]
ALTER TABLE [CMS_WidgetRole]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_WidgetRole_RoleID_CMS_Role]
	FOREIGN KEY ([RoleID]) REFERENCES [CMS_Role] ([RoleID])
ALTER TABLE [CMS_WidgetRole]
	CHECK CONSTRAINT [FK_CMS_WidgetRole_RoleID_CMS_Role]
ALTER TABLE [CMS_WidgetRole]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_WidgetRole_WidgetID_CMS_Widget]
	FOREIGN KEY ([WidgetID]) REFERENCES [CMS_Widget] ([WidgetID])
ALTER TABLE [CMS_WidgetRole]
	CHECK CONSTRAINT [FK_CMS_WidgetRole_WidgetID_CMS_Widget]
