CREATE TABLE [Analytics_YearHits] (
		[HitsID]               [int] IDENTITY(1, 1) NOT NULL,
		[HitsStatisticsID]     [int] NOT NULL,
		[HitsStartTime]        [datetime] NOT NULL,
		[HitsEndTime]          [datetime] NOT NULL,
		[HitsCount]            [int] NOT NULL,
		[HitsValue]            [float] NULL
) 
ALTER TABLE [Analytics_YearHits]
	ADD
	CONSTRAINT [PK_Analytics_YearHits]
	PRIMARY KEY
	NONCLUSTERED
	([HitsID])
	
	
CREATE NONCLUSTERED INDEX [IX_Analytics_WeekYearHits_HitsStatisticsID]
	ON [Analytics_YearHits] ([HitsStatisticsID])
	
	
CREATE CLUSTERED INDEX [IX_Analytics_YearHits_HitsStartTime_HitsEndTime]
	ON [Analytics_YearHits] ([HitsStartTime] DESC, [HitsEndTime] DESC)
	
	
ALTER TABLE [Analytics_YearHits]
	WITH CHECK
	ADD CONSTRAINT [FK_Analytics_YearHits_HitsStatisticsID_Analytics_Statistics]
	FOREIGN KEY ([HitsStatisticsID]) REFERENCES [Analytics_Statistics] ([StatisticsID])
ALTER TABLE [Analytics_YearHits]
	CHECK CONSTRAINT [FK_Analytics_YearHits_HitsStatisticsID_Analytics_Statistics]
