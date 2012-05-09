CREATE TABLE [CMS_WorkflowStep] (
		[StepID]               [int] IDENTITY(1, 1) NOT NULL,
		[StepDisplayName]      [nvarchar](450) NOT NULL,
		[StepName]             [nvarchar](440) NULL,
		[StepOrder]            [int] NOT NULL,
		[StepWorkflowID]       [int] NOT NULL,
		[StepGUID]             [uniqueidentifier] NOT NULL,
		[StepLastModified]     [datetime] NOT NULL
) 
ALTER TABLE [CMS_WorkflowStep]
	ADD
	CONSTRAINT [PK_CMS_WorkflowStep]
	PRIMARY KEY
	NONCLUSTERED
	([StepID])
	
	
CREATE CLUSTERED INDEX [IX_CMS_WorkflowStep_StepID_StepName]
	ON [CMS_WorkflowStep] ([StepID], [StepName])
	
CREATE UNIQUE NONCLUSTERED INDEX [IX_CMS_WorkflowStep_StepWorkflowID_StepName]
	ON [CMS_WorkflowStep] ([StepWorkflowID], [StepName])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_WorkflowStep_StepWorkflowID_StepOrder]
	ON [CMS_WorkflowStep] ([StepWorkflowID], [StepOrder])
	
	
ALTER TABLE [CMS_WorkflowStep]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_WorkflowStep_StepWorkflowID]
	FOREIGN KEY ([StepWorkflowID]) REFERENCES [CMS_Workflow] ([WorkflowID])
ALTER TABLE [CMS_WorkflowStep]
	CHECK CONSTRAINT [FK_CMS_WorkflowStep_StepWorkflowID]
