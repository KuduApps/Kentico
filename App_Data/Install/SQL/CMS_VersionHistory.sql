CREATE TABLE [CMS_VersionHistory] (
		[VersionHistoryID]           [int] IDENTITY(1, 1) NOT NULL,
		[NodeSiteID]                 [int] NULL,
		[DocumentID]                 [int] NULL,
		[DocumentNamePath]           [nvarchar](450) NOT NULL,
		[NodeXML]                    [nvarchar](max) NOT NULL,
		[ModifiedByUserID]           [int] NULL,
		[ModifiedWhen]               [datetime] NOT NULL,
		[VersionNumber]              [nvarchar](50) NULL,
		[VersionComment]             [nvarchar](max) NULL,
		[ToBePublished]              [bit] NOT NULL,
		[PublishFrom]                [datetime] NULL,
		[PublishTo]                  [datetime] NULL,
		[WasPublishedFrom]           [datetime] NULL,
		[WasPublishedTo]             [datetime] NULL,
		[VersionDocumentName]        [nvarchar](100) NULL,
		[VersionDocumentType]        [nvarchar](50) NULL,
		[VersionClassID]             [int] NULL,
		[VersionMenuRedirectUrl]     [nvarchar](450) NULL,
		[VersionWorkflowID]          [int] NULL,
		[VersionWorkflowStepID]      [int] NULL,
		[VersionNodeAliasPath]       [nvarchar](450) NULL,
		[VersionDeletedByUserID]     [int] NULL,
		[VersionDeletedWhen]         [datetime] NULL
)  
ALTER TABLE [CMS_VersionHistory]
	ADD
	CONSTRAINT [PK_CMS_VersionHistory]
	PRIMARY KEY
	NONCLUSTERED
	([VersionHistoryID])
	
	
ALTER TABLE [CMS_VersionHistory]
	ADD
	CONSTRAINT [DF__CMS_Versi__ToBeP__71D1E811]
	DEFAULT ((0)) FOR [ToBePublished]
CREATE CLUSTERED INDEX [IX_CMS_VersionHistory_DocumentID]
	ON [CMS_VersionHistory] ([DocumentID])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_VersionHistory_ModifiedByUserID]
	ON [CMS_VersionHistory] ([ModifiedByUserID])
	
CREATE NONCLUSTERED INDEX [IX_CMS_VersionHistory_NodeSiteID]
	ON [CMS_VersionHistory] ([NodeSiteID])
	
CREATE NONCLUSTERED INDEX [IX_CMS_VersionHistory_ToBePublished_PublishFrom_PublishTo]
	ON [CMS_VersionHistory] ([ToBePublished], [PublishFrom], [PublishTo])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_VersionHistory_VersionDeletedByUserID_VersionDeletedWhen]
	ON [CMS_VersionHistory] ([VersionDeletedByUserID], [VersionDeletedWhen] DESC)
	
ALTER TABLE [CMS_VersionHistory]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_VersionHistory_DeletedByUserID_CMS_User]
	FOREIGN KEY ([VersionDeletedByUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [CMS_VersionHistory]
	CHECK CONSTRAINT [FK_CMS_VersionHistory_DeletedByUserID_CMS_User]
ALTER TABLE [CMS_VersionHistory]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_VersionHistory_ModifiedByUserID_CMS_User]
	FOREIGN KEY ([ModifiedByUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [CMS_VersionHistory]
	CHECK CONSTRAINT [FK_CMS_VersionHistory_ModifiedByUserID_CMS_User]
ALTER TABLE [CMS_VersionHistory]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_VersionHistory_NodeSiteID_CMS_Site]
	FOREIGN KEY ([NodeSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [CMS_VersionHistory]
	CHECK CONSTRAINT [FK_CMS_VersionHistory_NodeSiteID_CMS_Site]
ALTER TABLE [CMS_VersionHistory]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_VersionHistory_VersionClassID_CMS_Class]
	FOREIGN KEY ([VersionClassID]) REFERENCES [CMS_Class] ([ClassID])
ALTER TABLE [CMS_VersionHistory]
	CHECK CONSTRAINT [FK_CMS_VersionHistory_VersionClassID_CMS_Class]
ALTER TABLE [CMS_VersionHistory]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_VersionHistory_VersionWorkflowID_CMS_Workflow]
	FOREIGN KEY ([VersionWorkflowID]) REFERENCES [CMS_Workflow] ([WorkflowID])
ALTER TABLE [CMS_VersionHistory]
	CHECK CONSTRAINT [FK_CMS_VersionHistory_VersionWorkflowID_CMS_Workflow]
ALTER TABLE [CMS_VersionHistory]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_VersionHistory_VersionWorkflowStepID_CMS_WorkflowStep]
	FOREIGN KEY ([VersionWorkflowStepID]) REFERENCES [CMS_WorkflowStep] ([StepID])
ALTER TABLE [CMS_VersionHistory]
	CHECK CONSTRAINT [FK_CMS_VersionHistory_VersionWorkflowStepID_CMS_WorkflowStep]
