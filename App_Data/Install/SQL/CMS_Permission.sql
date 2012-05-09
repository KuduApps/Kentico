CREATE TABLE [CMS_Permission] (
		[PermissionID]                        [int] IDENTITY(1, 1) NOT NULL,
		[PermissionDisplayName]               [nvarchar](100) NOT NULL,
		[PermissionName]                      [nvarchar](100) NOT NULL,
		[ClassID]                             [int] NULL,
		[ResourceID]                          [int] NULL,
		[PermissionGUID]                      [uniqueidentifier] NOT NULL,
		[PermissionLastModified]              [datetime] NOT NULL,
		[PermissionDescription]               [nvarchar](max) NULL,
		[PermissionDisplayInMatrix]           [bit] NULL,
		[PermissionOrder]                     [int] NULL,
		[PermissionEditableByGlobalAdmin]     [bit] NULL
)  
ALTER TABLE [CMS_Permission]
	ADD
	CONSTRAINT [PK_CMS_Permission]
	PRIMARY KEY
	CLUSTERED
	([PermissionID])
	
	
ALTER TABLE [CMS_Permission]
	ADD
	CONSTRAINT [DEFAULT_CMS_Permission_PermissionDisplayInMatrix]
	DEFAULT ((1)) FOR [PermissionDisplayInMatrix]
CREATE NONCLUSTERED INDEX [IX_CMS_Permission_ClassID_PermissionName]
	ON [CMS_Permission] ([ClassID], [PermissionName])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_Permission_ResourceID_PermissionName]
	ON [CMS_Permission] ([ResourceID], [PermissionName])
	
	
CREATE STATISTICS [IX_CMS_Permission_PermissionID_PermissionName_ResorceID]
	ON [CMS_Permission] ([PermissionID], [PermissionName], [ResourceID])
CREATE STATISTICS [IX_CMS_Permission_ResourceID_PermisionID]
	ON [CMS_Permission] ([ResourceID], [PermissionID])
ALTER TABLE [CMS_Permission]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_Permission_ClassID_CMS_Class]
	FOREIGN KEY ([ClassID]) REFERENCES [CMS_Class] ([ClassID])
ALTER TABLE [CMS_Permission]
	CHECK CONSTRAINT [FK_CMS_Permission_ClassID_CMS_Class]
ALTER TABLE [CMS_Permission]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_Permission_ResourceID_CMS_Resource]
	FOREIGN KEY ([ResourceID]) REFERENCES [CMS_Resource] ([ResourceID])
ALTER TABLE [CMS_Permission]
	CHECK CONSTRAINT [FK_CMS_Permission_ResourceID_CMS_Resource]
