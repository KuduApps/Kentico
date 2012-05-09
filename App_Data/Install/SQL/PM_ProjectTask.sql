CREATE TABLE [PM_ProjectTask] (
		[ProjectTaskID]                   [int] IDENTITY(1, 1) NOT NULL,
		[ProjectTaskProjectID]            [int] NULL,
		[ProjectTaskDisplayName]          [nvarchar](200) NOT NULL,
		[ProjectTaskDeadline]             [datetime] NULL,
		[ProjectTaskStatusID]             [int] NOT NULL,
		[ProjectTaskPriorityID]           [int] NOT NULL,
		[ProjectTaskDescription]          [nvarchar](max) NULL,
		[ProjectTaskOwnerID]              [int] NULL,
		[ProjectTaskCreatedByID]          [int] NULL,
		[ProjectTaskAssignedToUserID]     [int] NULL,
		[ProjectTaskProgress]             [int] NOT NULL,
		[ProjectTaskHours]                [float] NOT NULL,
		[ProjectTaskGUID]                 [uniqueidentifier] NOT NULL,
		[ProjectTaskLastModified]         [datetime] NOT NULL,
		[ProjectTaskProjectOrder]         [int] NULL,
		[ProjectTaskUserOrder]            [int] NULL,
		[ProjectTaskNotificationSent]     [bit] NULL,
		[ProjectTaskIsPrivate]            [bit] NULL
)  
ALTER TABLE [PM_ProjectTask]
	ADD
	CONSTRAINT [PK_PM_ProjectTask]
	PRIMARY KEY
	CLUSTERED
	([ProjectTaskID])
	
CREATE NONCLUSTERED INDEX [IX_PM_ProjectTask_ProjectTaskAssignedToUserID]
	ON [PM_ProjectTask] ([ProjectTaskAssignedToUserID])
	
CREATE NONCLUSTERED INDEX [IX_PM_ProjectTask_ProjectTaskOwnerID]
	ON [PM_ProjectTask] ([ProjectTaskOwnerID])
	
CREATE NONCLUSTERED INDEX [IX_PM_ProjectTask_ProjectTaskPriorityID]
	ON [PM_ProjectTask] ([ProjectTaskPriorityID])
	
CREATE NONCLUSTERED INDEX [IX_PM_ProjectTask_ProjectTaskProjectID]
	ON [PM_ProjectTask] ([ProjectTaskProjectID])
	
CREATE NONCLUSTERED INDEX [IX_PM_ProjectTask_ProjectTaskStatusID]
	ON [PM_ProjectTask] ([ProjectTaskStatusID])
	
ALTER TABLE [PM_ProjectTask]
	WITH CHECK
	ADD CONSTRAINT [FK_PM_ProjectTask_ProjectTaskAssignedToUserID_CMS_User]
	FOREIGN KEY ([ProjectTaskAssignedToUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [PM_ProjectTask]
	CHECK CONSTRAINT [FK_PM_ProjectTask_ProjectTaskAssignedToUserID_CMS_User]
ALTER TABLE [PM_ProjectTask]
	WITH CHECK
	ADD CONSTRAINT [FK_PM_ProjectTask_ProjectTaskCreatedByID_CMS_User]
	FOREIGN KEY ([ProjectTaskCreatedByID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [PM_ProjectTask]
	CHECK CONSTRAINT [FK_PM_ProjectTask_ProjectTaskCreatedByID_CMS_User]
ALTER TABLE [PM_ProjectTask]
	WITH CHECK
	ADD CONSTRAINT [FK_PM_ProjectTask_ProjectTaskOwnerID_ CMS_User]
	FOREIGN KEY ([ProjectTaskOwnerID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [PM_ProjectTask]
	CHECK CONSTRAINT [FK_PM_ProjectTask_ProjectTaskOwnerID_ CMS_User]
ALTER TABLE [PM_ProjectTask]
	WITH CHECK
	ADD CONSTRAINT [FK_PM_ProjectTask_ProjectTaskPriorityID_PM_ProjectTaskPriority]
	FOREIGN KEY ([ProjectTaskPriorityID]) REFERENCES [PM_ProjectTaskPriority] ([TaskPriorityID])
ALTER TABLE [PM_ProjectTask]
	CHECK CONSTRAINT [FK_PM_ProjectTask_ProjectTaskPriorityID_PM_ProjectTaskPriority]
ALTER TABLE [PM_ProjectTask]
	WITH CHECK
	ADD CONSTRAINT [FK_PM_ProjectTask_ProjectTaskProjectID_PM_Project]
	FOREIGN KEY ([ProjectTaskProjectID]) REFERENCES [PM_Project] ([ProjectID])
ALTER TABLE [PM_ProjectTask]
	CHECK CONSTRAINT [FK_PM_ProjectTask_ProjectTaskProjectID_PM_Project]
ALTER TABLE [PM_ProjectTask]
	WITH CHECK
	ADD CONSTRAINT [FK_PM_ProjectTask_ProjectTaskStatusID_PM_ProjectTaskStatus]
	FOREIGN KEY ([ProjectTaskStatusID]) REFERENCES [PM_ProjectTaskStatus] ([TaskStatusID])
ALTER TABLE [PM_ProjectTask]
	CHECK CONSTRAINT [FK_PM_ProjectTask_ProjectTaskStatusID_PM_ProjectTaskStatus]
