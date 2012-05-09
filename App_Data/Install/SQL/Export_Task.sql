CREATE TABLE [Export_Task] (
		[TaskID]             [int] IDENTITY(1, 1) NOT NULL,
		[TaskSiteID]         [int] NULL,
		[TaskTitle]          [nvarchar](450) NOT NULL,
		[TaskData]           [nvarchar](max) NOT NULL,
		[TaskTime]           [datetime] NOT NULL,
		[TaskType]           [nvarchar](50) NOT NULL,
		[TaskObjectType]     [nvarchar](100) NULL,
		[TaskObjectID]       [int] NULL
)  
ALTER TABLE [Export_Task]
	ADD
	CONSTRAINT [PK_Export_Task]
	PRIMARY KEY
	CLUSTERED
	([TaskID])
	
	
CREATE NONCLUSTERED INDEX [IX_Export_Task_TaskSiteID_TaskObjectType]
	ON [Export_Task] ([TaskSiteID], [TaskObjectType])
	
	
ALTER TABLE [Export_Task]
	WITH CHECK
	ADD CONSTRAINT [FK_Export_Task_TaskSiteID_CMS_Site]
	FOREIGN KEY ([TaskSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [Export_Task]
	CHECK CONSTRAINT [FK_Export_Task_TaskSiteID_CMS_Site]
