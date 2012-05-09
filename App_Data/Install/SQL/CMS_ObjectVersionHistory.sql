CREATE TABLE [CMS_ObjectVersionHistory] (
		[VersionID]                    [int] IDENTITY(1, 1) NOT NULL,
		[VersionObjectID]              [int] NULL,
		[VersionObjectType]            [nvarchar](100) NOT NULL,
		[VersionObjectSiteID]          [int] NULL,
		[VersionObjectDisplayName]     [nvarchar](450) NOT NULL,
		[VersionXML]                   [nvarchar](max) NOT NULL,
		[VersionBinaryDataXML]         [nvarchar](max) NULL,
		[VersionModifiedByUserID]      [int] NULL,
		[VersionModifiedWhen]          [datetime] NOT NULL,
		[VersionDeletedByUserID]       [int] NULL,
		[VersionDeletedWhen]           [datetime] NULL,
		[VersionNumber]                [nvarchar](50) NOT NULL,
		[VersionSiteBindingIDs]        [nvarchar](max) NULL
)  
ALTER TABLE [CMS_ObjectVersionHistory]
	ADD
	CONSTRAINT [DEFAULT_CMS_ObjectVersionHistory_VersionNumber]
	DEFAULT ('') FOR [VersionNumber]
CREATE NONCLUSTERED INDEX [IX_CMS_ObjectVersionHistory_VersionDeletedByUserID_VersionDeletedWhen]
	ON [CMS_ObjectVersionHistory] ([VersionDeletedByUserID], [VersionDeletedWhen] DESC)
	
CREATE NONCLUSTERED INDEX [IX_CMS_ObjectVersionHistory_VersionObjectSiteID_VersionDeletedWhen]
	ON [CMS_ObjectVersionHistory] ([VersionObjectSiteID], [VersionDeletedWhen] DESC)
	
CREATE NONCLUSTERED INDEX [IX_CMS_ObjectVersionHistory_VersionObjectType_VersionObjectID_VersionModifiedWhen]
	ON [CMS_ObjectVersionHistory] ([VersionObjectType], [VersionObjectID], [VersionModifiedWhen] DESC)
	
CREATE UNIQUE CLUSTERED INDEX [PK_CMS_ObjectVersionHistory]
	ON [CMS_ObjectVersionHistory] ([VersionObjectType], [VersionObjectID], [VersionID] DESC)
	
ALTER TABLE [CMS_ObjectVersionHistory]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_ObjectVersionHistory_VersionDeletedByUserID_CMS_User]
	FOREIGN KEY ([VersionDeletedByUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [CMS_ObjectVersionHistory]
	CHECK CONSTRAINT [FK_CMS_ObjectVersionHistory_VersionDeletedByUserID_CMS_User]
ALTER TABLE [CMS_ObjectVersionHistory]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_ObjectVersionHistory_VersionModifiedByUserID_CMS_User]
	FOREIGN KEY ([VersionModifiedByUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [CMS_ObjectVersionHistory]
	CHECK CONSTRAINT [FK_CMS_ObjectVersionHistory_VersionModifiedByUserID_CMS_User]
ALTER TABLE [CMS_ObjectVersionHistory]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_ObjectVersionHistory_VersionObjectSiteID_CMS_Site]
	FOREIGN KEY ([VersionObjectSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [CMS_ObjectVersionHistory]
	CHECK CONSTRAINT [FK_CMS_ObjectVersionHistory_VersionObjectSiteID_CMS_Site]
