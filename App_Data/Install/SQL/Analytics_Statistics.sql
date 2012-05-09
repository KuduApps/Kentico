CREATE TABLE [Analytics_Statistics] (
		[StatisticsID]                [int] IDENTITY(1, 1) NOT NULL,
		[StatisticsSiteID]            [int] NULL,
		[StatisticsCode]              [nvarchar](400) NOT NULL,
		[StatisticsObjectName]        [nvarchar](450) NULL,
		[StatisticsObjectID]          [int] NULL,
		[StatisticsObjectCulture]     [nvarchar](10) NULL
) 
ALTER TABLE [Analytics_Statistics]
	ADD
	CONSTRAINT [PK_Analytics_Statistics]
	PRIMARY KEY
	NONCLUSTERED
	([StatisticsID])
	
	
ALTER TABLE [Analytics_Statistics]
	ADD
	CONSTRAINT [DEFAULT_Analytics_Statistics_StatisticsCode]
	DEFAULT ('') FOR [StatisticsCode]
CREATE CLUSTERED INDEX [IX_Analytics_Statistics_StatisticsCode_StatisticsSiteID_StatisticsObjectID_StatisticsObjectCulture]
	ON [Analytics_Statistics] ([StatisticsCode])
	
ALTER TABLE [Analytics_Statistics]
	WITH CHECK
	ADD CONSTRAINT [FK_Analytics_Statistics_StatisticsSiteID_CMS_Site]
	FOREIGN KEY ([StatisticsSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [Analytics_Statistics]
	CHECK CONSTRAINT [FK_Analytics_Statistics_StatisticsSiteID_CMS_Site]
