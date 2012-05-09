-- =============================================
-- Author:		Name
-- Create date: 25.7.2007
-- Description:	Stores log records to DB
-- =============================================
CREATE PROCEDURE [Proc_OM_RemoveDataFromWebAnalytics]
	@Name nvarchar(100),
	@SiteID int
AS
BEGIN
	 DELETE FROM Analytics_YearHits WHERE HitsStatisticsID IN (SELECT StatisticsID FROM Analytics_Statistics WHERE
	 StatisticsCode LIKE 'abconversion;'+@Name+';%' AND StatisticsSiteID = @SiteID )
	 
	 DELETE FROM Analytics_MonthHits WHERE HitsStatisticsID IN (SELECT StatisticsID FROM Analytics_Statistics WHERE
		StatisticsCode LIKE 'abconversion;'+@Name+';%' AND StatisticsSiteID = @SiteID)
	 
	 DELETE FROM Analytics_WeekHits WHERE HitsStatisticsID IN (SELECT StatisticsID FROM Analytics_Statistics WHERE
		 StatisticsCode LIKE 'abconversion;'+@Name+';%' AND StatisticsSiteID = @SiteID)
	 
	 DELETE FROM Analytics_DayHits WHERE HitsStatisticsID IN (SELECT StatisticsID FROM Analytics_Statistics WHERE
		 StatisticsCode LIKE 'abconversion;'+@Name+';%' AND StatisticsSiteID = @SiteID)
	 
	 DELETE FROM Analytics_HourHits WHERE HitsStatisticsID IN (SELECT StatisticsID FROM Analytics_Statistics WHERE
		 StatisticsCode LIKE 'abconversion;'+@Name+';%' AND StatisticsSiteID = @SiteID)
		 
     DELETE FROM Analytics_Statistics WHERE StatisticsCode LIKE 'abconversion;'+@Name+';%' AND StatisticsSiteID = @SiteID
	 
END
