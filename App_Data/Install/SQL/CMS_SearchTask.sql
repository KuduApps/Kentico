CREATE TABLE [CMS_SearchTask] (
		[SearchTaskID]             [int] IDENTITY(1, 1) NOT NULL,
		[SearchTaskType]           [nvarchar](100) NOT NULL,
		[SearchTaskObjectType]     [nvarchar](100) NULL,
		[SearchTaskField]          [nvarchar](200) NULL,
		[SearchTaskValue]          [nvarchar](600) NOT NULL,
		[SearchTaskServerName]     [nvarchar](200) NULL,
		[SearchTaskStatus]         [nvarchar](100) NOT NULL,
		[SearchTaskPriority]       [int] NOT NULL,
		[SearchTaskCreated]        [datetime] NOT NULL
) 
ALTER TABLE [CMS_SearchTask]
	ADD
	CONSTRAINT [PK_CMS_SearchTask]
	PRIMARY KEY
	NONCLUSTERED
	([SearchTaskID])
	
	
ALTER TABLE [CMS_SearchTask]
	ADD
	CONSTRAINT [DEFAULT_cms_SearchTask_SearchTaskCreated]
	DEFAULT ('4/15/2009 11:23:52 AM') FOR [SearchTaskCreated]
ALTER TABLE [CMS_SearchTask]
	ADD
	CONSTRAINT [DEFAULT_cms_SearchTask_SearchTaskStatus]
	DEFAULT ('') FOR [SearchTaskStatus]
ALTER TABLE [CMS_SearchTask]
	ADD
	CONSTRAINT [DEFAULT_cms_SearchTask_SearchTaskType]
	DEFAULT ('') FOR [SearchTaskType]
ALTER TABLE [CMS_SearchTask]
	ADD
	CONSTRAINT [DEFAULT_cms_SearchTask_SearchTaskValue]
	DEFAULT ('') FOR [SearchTaskValue]
ALTER TABLE [CMS_SearchTask]
	ADD
	CONSTRAINT [DF_CMS_SearchTask_SearchTaskPriority]
	DEFAULT ((0)) FOR [SearchTaskPriority]
CREATE CLUSTERED INDEX [IX_CMS_SearchTask_SearchTaskPriority_SearchTaskStatus_SearchTaskServerName]
	ON [CMS_SearchTask] ([SearchTaskPriority] DESC, [SearchTaskStatus], [SearchTaskServerName])
	
	
