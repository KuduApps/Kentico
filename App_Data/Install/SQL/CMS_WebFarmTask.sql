CREATE TABLE [CMS_WebFarmTask] (
		[TaskID]               [int] IDENTITY(1, 1) NOT NULL,
		[TaskType]             [nvarchar](50) NOT NULL,
		[TaskTextData]         [nvarchar](max) NULL,
		[TaskBinaryData]       [varbinary](max) NULL,
		[TaskCreated]          [datetime] NULL,
		[TaskEnabled]          [bit] NULL,
		[TaskTarget]           [nvarchar](450) NULL,
		[TaskMachineName]      [nvarchar](450) NULL,
		[TaskGUID]             [uniqueidentifier] NULL,
		[TaskIsAnonymous]      [bit] NULL,
		[TaskErrorMessage]     [nvarchar](max) NULL
)  
ALTER TABLE [CMS_WebFarmTask]
	ADD
	CONSTRAINT [PK_CMS_WebFarmTask]
	PRIMARY KEY
	CLUSTERED
	([TaskID])
	
	
ALTER TABLE [CMS_WebFarmTask]
	ADD
	CONSTRAINT [DEFAULT_CMS_WebFarmTask_TaskGUID]
	DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [TaskGUID]
CREATE NONCLUSTERED INDEX [IX_CMS_WebFarmTask_TaskEnabled]
	ON [CMS_WebFarmTask] ([TaskEnabled])
	
	
