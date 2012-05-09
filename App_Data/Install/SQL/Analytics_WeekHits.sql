CREATE TABLE [Analytics_WeekHits] (
		[HitsID]               [int] IDENTITY(1, 1) NOT NULL,
		[HitsStatisticsID]     [int] NOT NULL,
		[HitsStartTime]        [datetime] NOT NULL,
		[HitsEndTime]          [datetime] NOT NULL,
		[HitsCount]            [int] NOT NULL,
		[HitsValue]            [float] NULL
) 
ALTER TABLE [Analytics_WeekHits]
	ADD
	CONSTRAINT [PK_Analytics_WeekHits]
	PRIMARY KEY
	NONCLUSTERED
	([HitsID])
	
	
CREATE CLUSTERED INDEX [IX_Analytics_WeekHits_HitsStartTime_HitsEndTime]
	ON [Analytics_WeekHits] ([HitsStartTime] DESC, [HitsEndTime] DESC)
	
	
CREATE NONCLUSTERED INDEX [IX_Analytics_WeekHits_HitsStatisticsID]
	ON [Analytics_WeekHits] ([HitsStatisticsID])
	
	
ALTER TABLE [Analytics_WeekHits]
	WITH CHECK
	ADD CONSTRAINT [FK_Analytics_WeekHits_HitsStatisticsID_Analytics_Statistics]
	FOREIGN KEY ([HitsStatisticsID]) REFERENCES [Analytics_Statistics] ([StatisticsID])
ALTER TABLE [Analytics_WeekHits]
	CHECK CONSTRAINT [FK_Analytics_WeekHits_HitsStatisticsID_Analytics_Statistics]
