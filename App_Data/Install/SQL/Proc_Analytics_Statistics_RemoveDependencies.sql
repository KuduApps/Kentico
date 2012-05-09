CREATE PROCEDURE [Proc_Analytics_Statistics_RemoveDependencies]
@ID int
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
  -- interfering with SELECT statements.
  SET NOCOUNT ON;
  BEGIN TRANSACTION;
  DELETE FROM Analytics_DayHits WHERE HitsStatisticsID=@ID
  DELETE FROM Analytics_HourHits WHERE HitsStatisticsID=@ID
  DELETE FROM Analytics_MonthHits WHERE HitsStatisticsID=@ID
  DELETE FROM Analytics_WeekHits WHERE HitsStatisticsID=@ID
  DELETE FROM Analytics_YearHits WHERE HitsStatisticsID=@ID
    COMMIT TRANSACTION;
END
