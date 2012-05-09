CREATE TABLE [CMS_WorkflowStepRoles] (
		[StepID]     [int] NOT NULL,
		[RoleID]     [int] NOT NULL
) 
ALTER TABLE [CMS_WorkflowStepRoles]
	ADD
	CONSTRAINT [PK_CMS_WorkflowStepRoles]
	PRIMARY KEY
	CLUSTERED
	([StepID], [RoleID])
	
	
ALTER TABLE [CMS_WorkflowStepRoles]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_WorkflowStepRoles_RoleID_CMS_Role]
	FOREIGN KEY ([RoleID]) REFERENCES [CMS_Role] ([RoleID])
ALTER TABLE [CMS_WorkflowStepRoles]
	CHECK CONSTRAINT [FK_CMS_WorkflowStepRoles_RoleID_CMS_Role]
ALTER TABLE [CMS_WorkflowStepRoles]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_WorkflowStepRoles_StepID_CMS_WorkflowStep]
	FOREIGN KEY ([StepID]) REFERENCES [CMS_WorkflowStep] ([StepID])
ALTER TABLE [CMS_WorkflowStepRoles]
	CHECK CONSTRAINT [FK_CMS_WorkflowStepRoles_StepID_CMS_WorkflowStep]
