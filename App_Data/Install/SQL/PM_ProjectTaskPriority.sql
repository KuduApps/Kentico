CREATE TABLE [PM_ProjectTaskPriority] (
		[TaskPriorityID]               [int] IDENTITY(1, 1) NOT NULL,
		[TaskPriorityName]             [nvarchar](200) NOT NULL,
		[TaskPriorityDisplayName]      [nvarchar](200) NOT NULL,
		[TaskPriorityOrder]            [int] NULL,
		[TaskPriorityGUID]             [uniqueidentifier] NOT NULL,
		[TaskPriorityLastModified]     [datetime] NOT NULL,
		[TaskPriorityEnabled]          [bit] NOT NULL,
		[TaskPriorityDefault]          [bit] NOT NULL
) 
ALTER TABLE [PM_ProjectTaskPriority]
	ADD
	CONSTRAINT [PK_PM_ProjectTaskPriority]
	PRIMARY KEY
	CLUSTERED
	([TaskPriorityID])
	
