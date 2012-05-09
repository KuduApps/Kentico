CREATE TABLE [CMS_Document] (
		[DocumentID]                                [int] IDENTITY(1, 1) NOT NULL,
		[DocumentName]                              [nvarchar](100) NOT NULL,
		[DocumentNamePath]                          [nvarchar](1500) NULL,
		[DocumentModifiedWhen]                      [datetime] NULL,
		[DocumentModifiedByUserID]                  [int] NULL,
		[DocumentForeignKeyValue]                   [int] NULL,
		[DocumentCreatedByUserID]                   [int] NULL,
		[DocumentCreatedWhen]                       [datetime] NULL,
		[DocumentCheckedOutByUserID]                [int] NULL,
		[DocumentCheckedOutWhen]                    [datetime] NULL,
		[DocumentCheckedOutVersionHistoryID]        [int] NULL,
		[DocumentPublishedVersionHistoryID]         [int] NULL,
		[DocumentWorkflowStepID]                    [int] NULL,
		[DocumentPublishFrom]                       [datetime] NULL,
		[DocumentPublishTo]                         [datetime] NULL,
		[DocumentUrlPath]                           [nvarchar](450) NULL,
		[DocumentCulture]                           [nvarchar](10) NOT NULL,
		[DocumentNodeID]                            [int] NOT NULL,
		[DocumentPageTitle]                         [nvarchar](max) NULL,
		[DocumentPageKeyWords]                      [nvarchar](max) NULL,
		[DocumentPageDescription]                   [nvarchar](max) NULL,
		[DocumentShowInSiteMap]                     [bit] NOT NULL,
		[DocumentMenuItemHideInNavigation]          [bit] NOT NULL,
		[DocumentMenuCaption]                       [nvarchar](200) NULL,
		[DocumentMenuStyle]                         [nvarchar](100) NULL,
		[DocumentMenuItemImage]                     [nvarchar](200) NULL,
		[DocumentMenuItemLeftImage]                 [nvarchar](200) NULL,
		[DocumentMenuItemRightImage]                [nvarchar](200) NULL,
		[DocumentPageTemplateID]                    [int] NULL,
		[DocumentMenuJavascript]                    [nvarchar](450) NULL,
		[DocumentMenuRedirectUrl]                   [nvarchar](450) NULL,
		[DocumentUseNamePathForUrlPath]             [bit] NULL,
		[DocumentStylesheetID]                      [int] NULL,
		[DocumentContent]                           [nvarchar](max) NULL,
		[DocumentMenuClass]                         [nvarchar](100) NULL,
		[DocumentMenuStyleOver]                     [nvarchar](200) NULL,
		[DocumentMenuClassOver]                     [nvarchar](100) NULL,
		[DocumentMenuItemImageOver]                 [nvarchar](200) NULL,
		[DocumentMenuItemLeftImageOver]             [nvarchar](200) NULL,
		[DocumentMenuItemRightImageOver]            [nvarchar](200) NULL,
		[DocumentMenuStyleHighlighted]              [nvarchar](200) NULL,
		[DocumentMenuClassHighlighted]              [nvarchar](100) NULL,
		[DocumentMenuItemImageHighlighted]          [nvarchar](200) NULL,
		[DocumentMenuItemLeftImageHighlighted]      [nvarchar](200) NULL,
		[DocumentMenuItemRightImageHighlighted]     [nvarchar](200) NULL,
		[DocumentMenuItemInactive]                  [bit] NULL,
		[DocumentCustomData]                        [nvarchar](max) NULL,
		[DocumentExtensions]                        [nvarchar](100) NULL,
		[DocumentCampaign]                          [nvarchar](100) NULL,
		[DocumentTags]                              [nvarchar](max) NULL,
		[DocumentTagGroupID]                        [int] NULL,
		[DocumentWildcardRule]                      [nvarchar](440) NULL,
		[DocumentWebParts]                          [nvarchar](max) NULL,
		[DocumentRatingValue]                       [float] NULL,
		[DocumentRatings]                           [int] NULL,
		[DocumentPriority]                          [int] NULL,
		[DocumentType]                              [nvarchar](50) NULL,
		[DocumentLastPublished]                     [datetime] NULL,
		[DocumentUseCustomExtensions]               [bit] NULL,
		[DocumentGroupWebParts]                     [nvarchar](max) NULL,
		[DocumentCheckedOutAutomatically]           [bit] NULL,
		[DocumentTrackConversionName]               [nvarchar](200) NULL,
		[DocumentConversionValue]                   [nvarchar](100) NULL,
		[DocumentSearchExcluded]                    [bit] NULL,
		[DocumentLastVersionName]                   [nvarchar](100) NULL,
		[DocumentLastVersionNumber]                 [nvarchar](50) NULL,
		[DocumentIsArchived]                        [bit] NULL,
		[DocumentLastVersionType]                   [nvarchar](50) NULL,
		[DocumentLastVersionMenuRedirectUrl]        [nvarchar](450) NULL,
		[DocumentHash]                              [nvarchar](32) NULL,
		[DocumentLogVisitActivity]                  [bit] NULL,
		[DocumentGUID]                              [uniqueidentifier] NULL,
		[DocumentWorkflowCycleGUID]                 [uniqueidentifier] NULL
)  
ALTER TABLE [CMS_Document]
	ADD
	CONSTRAINT [PK_CMS_Document]
	PRIMARY KEY
	CLUSTERED
	([DocumentID])
	
	
ALTER TABLE [CMS_Document]
	ADD
	CONSTRAINT [DEFAULT_CMS_Document_DocumentUseCustomExtensions]
	DEFAULT ((0)) FOR [DocumentUseCustomExtensions]
CREATE NONCLUSTERED INDEX [IX_CMS_Document_DocumentCheckedOutByUserID]
	ON [CMS_Document] ([DocumentCheckedOutByUserID])
	
CREATE NONCLUSTERED INDEX [IX_CMS_Document_DocumentCreatedByUserID]
	ON [CMS_Document] ([DocumentCreatedByUserID])
	
CREATE NONCLUSTERED INDEX [IX_CMS_Document_DocumentCulture]
	ON [CMS_Document] ([DocumentCulture])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_Document_DocumentForeignKeyValue_DocumentID_DocumentNodeID]
	ON [CMS_Document] ([DocumentForeignKeyValue], [DocumentID], [DocumentNodeID])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_Document_DocumentMenuItemHideInNavigation]
	ON [CMS_Document] ([DocumentMenuItemHideInNavigation])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_Document_DocumentModifiedByUserID]
	ON [CMS_Document] ([DocumentModifiedByUserID])
	
CREATE NONCLUSTERED INDEX [IX_CMS_Document_DocumentNodeID_DocumentID]
	ON [CMS_Document] ([DocumentNodeID], [DocumentID])
	
CREATE NONCLUSTERED INDEX [IX_CMS_Document_DocumentPageTemplateID]
	ON [CMS_Document] ([DocumentPageTemplateID])
	
CREATE NONCLUSTERED INDEX [IX_CMS_Document_DocumentShowInSiteMap]
	ON [CMS_Document] ([DocumentShowInSiteMap])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_Document_DocumentTagGroupID]
	ON [CMS_Document] ([DocumentTagGroupID])
	
CREATE NONCLUSTERED INDEX [IX_CMS_Document_DocumentUrlPath_DocumentID_DocumentNodeID]
	ON [CMS_Document] ([DocumentUrlPath])
	INCLUDE ([DocumentID], [DocumentNodeID])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_Document_DocumentWildcardRule_DocumentPriority]
	ON [CMS_Document] ([DocumentWildcardRule], [DocumentPriority])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_Document_WorkflowColumns]
	ON [CMS_Document] ([DocumentID], [DocumentNodeID], [DocumentCulture], [DocumentCheckedOutVersionHistoryID], [DocumentPublishedVersionHistoryID], [DocumentPublishFrom], [DocumentPublishTo], [DocumentWorkflowStepID], [DocumentIsArchived])
	
	
ALTER TABLE [CMS_Document]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_Document_DocumentCheckedOutByUserID_CMS_User]
	FOREIGN KEY ([DocumentCheckedOutByUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [CMS_Document]
	CHECK CONSTRAINT [FK_CMS_Document_DocumentCheckedOutByUserID_CMS_User]
ALTER TABLE [CMS_Document]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_Document_DocumentCheckedOutVersionHistoryID_CMS_VersionHistory]
	FOREIGN KEY ([DocumentCheckedOutVersionHistoryID]) REFERENCES [CMS_VersionHistory] ([VersionHistoryID])
ALTER TABLE [CMS_Document]
	CHECK CONSTRAINT [FK_CMS_Document_DocumentCheckedOutVersionHistoryID_CMS_VersionHistory]
ALTER TABLE [CMS_Document]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_Document_DocumentCreatedByUserID_CMS_User]
	FOREIGN KEY ([DocumentCreatedByUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [CMS_Document]
	CHECK CONSTRAINT [FK_CMS_Document_DocumentCreatedByUserID_CMS_User]
ALTER TABLE [CMS_Document]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_Document_DocumentModifiedByUserID_CMS_User]
	FOREIGN KEY ([DocumentModifiedByUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [CMS_Document]
	CHECK CONSTRAINT [FK_CMS_Document_DocumentModifiedByUserID_CMS_User]
ALTER TABLE [CMS_Document]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_Document_DocumentNodeID_CMS_Tree]
	FOREIGN KEY ([DocumentNodeID]) REFERENCES [CMS_Tree] ([NodeID])
ALTER TABLE [CMS_Document]
	CHECK CONSTRAINT [FK_CMS_Document_DocumentNodeID_CMS_Tree]
ALTER TABLE [CMS_Document]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_Document_DocumentPageTemplateID_CMS_Template]
	FOREIGN KEY ([DocumentPageTemplateID]) REFERENCES [CMS_PageTemplate] ([PageTemplateID])
ALTER TABLE [CMS_Document]
	CHECK CONSTRAINT [FK_CMS_Document_DocumentPageTemplateID_CMS_Template]
ALTER TABLE [CMS_Document]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_Document_DocumentPublishedVersionHistoryID_CMS_VersionHistory]
	FOREIGN KEY ([DocumentPublishedVersionHistoryID]) REFERENCES [CMS_VersionHistory] ([VersionHistoryID])
ALTER TABLE [CMS_Document]
	CHECK CONSTRAINT [FK_CMS_Document_DocumentPublishedVersionHistoryID_CMS_VersionHistory]
ALTER TABLE [CMS_Document]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_Document_DocumentTagGroupID_CMS_TagGroup]
	FOREIGN KEY ([DocumentTagGroupID]) REFERENCES [CMS_TagGroup] ([TagGroupID])
ALTER TABLE [CMS_Document]
	CHECK CONSTRAINT [FK_CMS_Document_DocumentTagGroupID_CMS_TagGroup]
ALTER TABLE [CMS_Document]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_Document_DocumentWorkflowStepID_CMS_WorkflowStep]
	FOREIGN KEY ([DocumentWorkflowStepID]) REFERENCES [CMS_WorkflowStep] ([StepID])
ALTER TABLE [CMS_Document]
	CHECK CONSTRAINT [FK_CMS_Document_DocumentWorkflowStepID_CMS_WorkflowStep]
