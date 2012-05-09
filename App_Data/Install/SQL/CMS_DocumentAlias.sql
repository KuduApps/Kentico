CREATE TABLE [CMS_DocumentAlias] (
		[AliasID]               [int] IDENTITY(1, 1) NOT NULL,
		[AliasNodeID]           [int] NOT NULL,
		[AliasCulture]          [nvarchar](20) NULL,
		[AliasURLPath]          [nvarchar](450) NULL,
		[AliasExtensions]       [nvarchar](100) NULL,
		[AliasCampaign]         [nvarchar](100) NULL,
		[AliasWildcardRule]     [nvarchar](440) NULL,
		[AliasPriority]         [int] NULL,
		[AliasGUID]             [uniqueidentifier] NULL,
		[AliasLastModified]     [datetime] NOT NULL,
		[AliasSiteID]           [int] NOT NULL
) 
ALTER TABLE [CMS_DocumentAlias]
	ADD
	CONSTRAINT [PK_CMS_DocumentAlias]
	PRIMARY KEY
	NONCLUSTERED
	([AliasID])
	
	
ALTER TABLE [CMS_DocumentAlias]
	ADD
	CONSTRAINT [DEFAULT_CMS_DocumentAlias_AliasLastModified]
	DEFAULT ('10/22/2008 12:55:43 PM') FOR [AliasLastModified]
ALTER TABLE [CMS_DocumentAlias]
	ADD
	CONSTRAINT [DEFAULT_CMS_DocumentAlias_AliasSiteID]
	DEFAULT ((0)) FOR [AliasSiteID]
CREATE NONCLUSTERED INDEX [IX_CMS_Document_AliasCulture]
	ON [CMS_DocumentAlias] ([AliasCulture])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_DocumentAlias_AliasNodeID]
	ON [CMS_DocumentAlias] ([AliasNodeID])
	
CREATE NONCLUSTERED INDEX [IX_CMS_DocumentAlias_AliasSiteID]
	ON [CMS_DocumentAlias] ([AliasSiteID])
	
CREATE CLUSTERED INDEX [IX_CMS_DocumentAlias_AliasURLPath]
	ON [CMS_DocumentAlias] ([AliasURLPath])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_DocumentAlias_AliasWildcardRule_AliasPriority]
	ON [CMS_DocumentAlias] ([AliasWildcardRule], [AliasPriority])
	
	
ALTER TABLE [CMS_DocumentAlias]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_DocumentAlias_AliasNodeID_CMS_Tree]
	FOREIGN KEY ([AliasNodeID]) REFERENCES [CMS_Tree] ([NodeID])
ALTER TABLE [CMS_DocumentAlias]
	CHECK CONSTRAINT [FK_CMS_DocumentAlias_AliasNodeID_CMS_Tree]
ALTER TABLE [CMS_DocumentAlias]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_DocumentAlias_AliasSiteID_CMS_Site]
	FOREIGN KEY ([AliasSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [CMS_DocumentAlias]
	CHECK CONSTRAINT [FK_CMS_DocumentAlias_AliasSiteID_CMS_Site]
