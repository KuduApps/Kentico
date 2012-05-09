CREATE TABLE [PM_ProjectTaskStatus] (
		[TaskStatusID]               [int] IDENTITY(1, 1) NOT NULL,
		[TaskStatusName]             [nvarchar](200) NOT NULL,
		[TaskStatusDisplayName]      [nvarchar](200) NOT NULL,
		[TaskStatusOrder]            [int] NOT NULL,
		[TaskStatusColor]            [nvarchar](7) NULL,
		[TaskStatusIcon]             [nvarchar](450) NULL,
		[TaskStatusGUID]             [uniqueidentifier] NOT NULL,
		[TaskStatusLastModified]     [datetime] NOT NULL,
		[TaskStatusEnabled]          [bit] NOT NULL,
		[TaskStatusIsNotStarted]     [bit] NOT NULL,
		[TaskStatusIsFinished]       [bit] NOT NULL
) 
ALTER TABLE [PM_ProjectTaskStatus]
	ADD
	CONSTRAINT [PK_PM_ProjectTaskStatus]
	PRIMARY KEY
	CLUSTERED
	([TaskStatusID])
	
