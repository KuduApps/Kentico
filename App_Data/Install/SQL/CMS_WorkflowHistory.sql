CREATE TABLE [CMS_WorkflowHistory] (
		[WorkflowHistoryID]     [int] IDENTITY(1, 1) NOT NULL,
		[VersionHistoryID]      [int] NOT NULL,
		[StepID]                [int] NULL,
		[StepDisplayName]       [nvarchar](450) NOT NULL,
		[ApprovedByUserID]      [int] NULL,
		[ApprovedWhen]          [datetime] NULL,
		[Comment]               [nvarchar](max) NULL,
		[WasRejected]           [bit] NOT NULL,
		[StepName]              [nvarchar](440) NULL
)  
ALTER TABLE [CMS_WorkflowHistory]
	ADD
	CONSTRAINT [PK_CMS_WorkflowHistory]
	PRIMARY KEY
	CLUSTERED
	([WorkflowHistoryID])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_WorkflowHistory_ApprovedByUserID]
	ON [CMS_WorkflowHistory] ([ApprovedByUserID])
	
CREATE NONCLUSTERED INDEX [IX_CMS_WorkflowHistory_ApprovedWhen]
	ON [CMS_WorkflowHistory] ([ApprovedWhen])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_WorkflowHistory_StepID]
	ON [CMS_WorkflowHistory] ([StepID])
	
CREATE NONCLUSTERED INDEX [IX_CMS_WorkflowHistory_VersionHistoryID]
	ON [CMS_WorkflowHistory] ([VersionHistoryID])
	
ALTER TABLE [CMS_WorkflowHistory]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_WorkflowHistory_ApprovedByUserID_CMS_User]
	FOREIGN KEY ([ApprovedByUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [CMS_WorkflowHistory]
	CHECK CONSTRAINT [FK_CMS_WorkflowHistory_ApprovedByUserID_CMS_User]
ALTER TABLE [CMS_WorkflowHistory]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_WorkflowHistory_StepID_CMS_WorkflowStep]
	FOREIGN KEY ([StepID]) REFERENCES [CMS_WorkflowStep] ([StepID])
ALTER TABLE [CMS_WorkflowHistory]
	CHECK CONSTRAINT [FK_CMS_WorkflowHistory_StepID_CMS_WorkflowStep]
ALTER TABLE [CMS_WorkflowHistory]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_WorkflowHistory_VersionHistoryID_CMS_VersionHistory]
	FOREIGN KEY ([VersionHistoryID]) REFERENCES [CMS_VersionHistory] ([VersionHistoryID])
ALTER TABLE [CMS_WorkflowHistory]
	CHECK CONSTRAINT [FK_CMS_WorkflowHistory_VersionHistoryID_CMS_VersionHistory]
