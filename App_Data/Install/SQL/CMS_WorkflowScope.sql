CREATE TABLE [CMS_WorkflowScope] (
		[ScopeID]               [int] IDENTITY(1, 1) NOT NULL,
		[ScopeStartingPath]     [nvarchar](450) NOT NULL,
		[ScopeWorkflowID]       [int] NOT NULL,
		[ScopeClassID]          [int] NULL,
		[ScopeSiteID]           [int] NOT NULL,
		[ScopeGUID]             [uniqueidentifier] NOT NULL,
		[ScopeLastModified]     [datetime] NOT NULL,
		[ScopeCultureID]        [int] NULL
) 
ALTER TABLE [CMS_WorkflowScope]
	ADD
	CONSTRAINT [PK_CMS_WorkflowScope]
	PRIMARY KEY
	NONCLUSTERED
	([ScopeID])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_WorkflowScope_ScopeClassID]
	ON [CMS_WorkflowScope] ([ScopeClassID])
	
CREATE NONCLUSTERED INDEX [IX_CMS_WorkflowScope_ScopeCultureID]
	ON [CMS_WorkflowScope] ([ScopeCultureID])
	
CREATE NONCLUSTERED INDEX [IX_CMS_WorkflowScope_ScopeSiteID]
	ON [CMS_WorkflowScope] ([ScopeSiteID])
	
CREATE CLUSTERED INDEX [IX_CMS_WorkflowScope_ScopeStartingPath]
	ON [CMS_WorkflowScope] ([ScopeStartingPath])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_WorkflowScope_ScopeWorkflowID]
	ON [CMS_WorkflowScope] ([ScopeWorkflowID])
	
ALTER TABLE [CMS_WorkflowScope]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_WorkflowScope_ScopeClassID_CMS_Class]
	FOREIGN KEY ([ScopeClassID]) REFERENCES [CMS_Class] ([ClassID])
ALTER TABLE [CMS_WorkflowScope]
	CHECK CONSTRAINT [FK_CMS_WorkflowScope_ScopeClassID_CMS_Class]
ALTER TABLE [CMS_WorkflowScope]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_WorkflowScope_ScopeCultureID_CMS_Culture]
	FOREIGN KEY ([ScopeCultureID]) REFERENCES [CMS_Culture] ([CultureID])
ALTER TABLE [CMS_WorkflowScope]
	CHECK CONSTRAINT [FK_CMS_WorkflowScope_ScopeCultureID_CMS_Culture]
ALTER TABLE [CMS_WorkflowScope]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_WorkflowScope_ScopeSiteID_CMS_Site]
	FOREIGN KEY ([ScopeSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [CMS_WorkflowScope]
	CHECK CONSTRAINT [FK_CMS_WorkflowScope_ScopeSiteID_CMS_Site]
ALTER TABLE [CMS_WorkflowScope]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_WorkflowScope_ScopeWorkflowID_CMS_WorkflowID]
	FOREIGN KEY ([ScopeWorkflowID]) REFERENCES [CMS_Workflow] ([WorkflowID])
ALTER TABLE [CMS_WorkflowScope]
	CHECK CONSTRAINT [FK_CMS_WorkflowScope_ScopeWorkflowID_CMS_WorkflowID]
