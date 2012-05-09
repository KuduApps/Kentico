CREATE TABLE [Analytics_MonthHits] (
		[HitsID]               [int] IDENTITY(1, 1) NOT NULL,
		[HitsStatisticsID]     [int] NOT NULL,
		[HitsStartTime]        [datetime] NOT NULL,
		[HitsEndTime]          [datetime] NOT NULL,
		[HitsCount]            [int] NOT NULL,
		[HitsValue]            [float] NULL
) 
ALTER TABLE [Analytics_MonthHits]
	ADD
	CONSTRAINT [PK_Analytics_MonthHits]
	PRIMARY KEY
	NONCLUSTERED
	([HitsID])
	
	
CREATE CLUSTERED INDEX [IX_Analytics_MonthHits_HitsStartTime_HitsEndTime]
	ON [Analytics_MonthHits] ([HitsStartTime] DESC, [HitsEndTime] DESC)
	
	
CREATE NONCLUSTERED INDEX [IX_Analytics_MonthHits_HitsStatisticsID]
	ON [Analytics_MonthHits] ([HitsStatisticsID])
	
	
ALTER TABLE [Analytics_MonthHits]
	WITH CHECK
	ADD CONSTRAINT [FK_Analytics_MonthHits_HitsStatisticsID_Analytics_Statistics]
	FOREIGN KEY ([HitsStatisticsID]) REFERENCES [Analytics_Statistics] ([StatisticsID])
ALTER TABLE [Analytics_MonthHits]
	CHECK CONSTRAINT [FK_Analytics_MonthHits_HitsStatisticsID_Analytics_Statistics]
