CREATE TABLE [CMS_TagGroup] (
		[TagGroupID]               [int] IDENTITY(1, 1) NOT NULL,
		[TagGroupDisplayName]      [nvarchar](250) NOT NULL,
		[TagGroupName]             [nvarchar](250) NOT NULL,
		[TagGroupDescription]      [nvarchar](max) NOT NULL,
		[TagGroupSiteID]           [int] NOT NULL,
		[TagGroupIsAdHoc]          [bit] NOT NULL,
		[TagGroupLastModified]     [datetime] NOT NULL,
		[TagGroupGUID]             [uniqueidentifier] NOT NULL
)  
ALTER TABLE [CMS_TagGroup]
	ADD
	CONSTRAINT [PK_CMS_TagGroup]
	PRIMARY KEY
	NONCLUSTERED
	([TagGroupID])
	
	
CREATE CLUSTERED INDEX [IX_CMS_TagGroup_TagGroupDisplayName]
	ON [CMS_TagGroup] ([TagGroupDisplayName])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_TagGroup_TagGroupSiteID]
	ON [CMS_TagGroup] ([TagGroupSiteID])
	
ALTER TABLE [CMS_TagGroup]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_TagGroup_TagGroupSiteID_CMS_Site]
	FOREIGN KEY ([TagGroupSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [CMS_TagGroup]
	CHECK CONSTRAINT [FK_CMS_TagGroup_TagGroupSiteID_CMS_Site]
