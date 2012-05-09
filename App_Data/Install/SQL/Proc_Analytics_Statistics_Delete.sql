-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_Analytics_Statistics_Delete] 
    @SiteID int,
    @CodeName nvarchar(50),
    @FromDate datetime,
    @ToDate datetime,
    @Week1Start datetime,
    @Week1End datetime,
    @Week2Start datetime,
    @Week2End datetime,
    @Month1Start datetime,
    @Month1End datetime,
    @Month2Start datetime,
    @Month2End datetime,
    @Year1Start datetime,
    @Year1End datetime,
    @Year2Start datetime,
    @Year2End datetime
    
AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @HitsStatID int;
	DECLARE @Cnt int;
	DECLARE @CntL int;
	DECLARE @CntM int;
	DECLARE @CntR int;
	DECLARE @hitsID int;
	DECLARE @hitsCount int;
	DECLARE mycursor CURSOR FOR SELECT HitsStatisticsID FROM Analytics_Statistics, Analytics_DayHits
		  WHERE StatisticsSiteID=@SiteID AND StatisticsCode LIKE @CodeName AND
		  StatisticsID=HitsStatisticsID AND HitsStartTime >= @FromDate AND
		  @ToDate >= HitsEndTime
	OPEN mycursor;
	FETCH NEXT FROM mycursor INTO @HitsStatID
	WHILE @@FETCH_STATUS = 0
	BEGIN
-- WEEKS
    IF (@Week1End < @ToDate)
    BEGIN
        SET @CntL = 0;
		SELECT @CntL = SUM(HitsCount) FROM Analytics_Statistics, Analytics_DayHits
			  WHERE StatisticsCode=@CodeName AND StatisticsID=HitsStatisticsID AND
              HitsStartTime >= @FromDate AND @Week1End >= HitsEndTime AND
              StatisticsID = @HitsStatID
		GROUP BY StatisticsID, StatisticsObjectName, StatisticsObjectID, StatisticsObjectCulture, HitsStatisticsID
		SET @hitsID = 0;
		SELECT @hitsID = HitsID, @hitsCount = HitsCount FROM [Analytics_WeekHits]
		WHERE HitsStatisticsID=@HitsStatID AND HitsStartTime>=@Week1Start AND HitsEndTime<=@Week1End;
		IF @hitsID > 0
		BEGIN
			UPDATE [Analytics_WeekHits] SET HitsCount=(@hitsCount-@CntL) WHERE HitsID=@hitsID;
		END;
    END
    IF (@Week2Start > @FromDate)
    BEGIN
        SET @CntR = 0;
		SELECT @CntR = SUM(HitsCount) FROM Analytics_Statistics, Analytics_DayHits
			  WHERE StatisticsCode=@CodeName AND StatisticsID=HitsStatisticsID AND
              HitsStartTime >= @Week2Start AND @ToDate >= HitsEndTime AND
              StatisticsID = @HitsStatID
		GROUP BY StatisticsID, StatisticsObjectName, StatisticsObjectID, StatisticsObjectCulture, HitsStatisticsID
		SET @hitsID = 0;
		SELECT @hitsID = HitsID, @hitsCount = HitsCount FROM [Analytics_WeekHits]
		WHERE HitsStatisticsID=@HitsStatID AND HitsStartTime>=@Week2Start AND HitsEndTime<=@Week2End;
		IF @hitsID > 0
		BEGIN
			UPDATE [Analytics_WeekHits] SET HitsCount=(@hitsCount-@CntR) WHERE HitsID=@hitsID;
		END;
    END
    IF (@Week1Start <= @FromDate AND @ToDate <= @Week1End)
    BEGIN
        SET @CntM = 0;
		SELECT @CntM = SUM(HitsCount) FROM Analytics_Statistics, Analytics_DayHits
			  WHERE StatisticsCode=@CodeName AND StatisticsID=HitsStatisticsID AND
              HitsStartTime >= @FromDate AND @ToDate >= HitsEndTime AND
              StatisticsID = @HitsStatID
		GROUP BY StatisticsID, StatisticsObjectName, StatisticsObjectID, StatisticsObjectCulture, HitsStatisticsID
		SET @hitsID = 0;
		SELECT @hitsID = HitsID, @hitsCount = HitsCount FROM [Analytics_WeekHits]
		WHERE HitsStatisticsID=@HitsStatID AND HitsStartTime>=@Week1Start AND HitsEndTime<=@Week1End;
		IF @hitsID > 0
		BEGIN
			UPDATE [Analytics_WeekHits] SET HitsCount=(@hitsCount-@CntM) WHERE HitsID=@hitsID;
		END;
    END;
-- MONTHS
    IF (@Month1End < @ToDate)
    BEGIN
        SET @CntL = 0;
		SELECT @CntL = SUM(HitsCount) FROM Analytics_Statistics, Analytics_DayHits
			  WHERE StatisticsCode=@CodeName AND StatisticsID=HitsStatisticsID AND
              HitsStartTime >= @FromDate AND @Month1End >= HitsEndTime AND
              StatisticsID = @HitsStatID
		GROUP BY StatisticsID, StatisticsObjectName, StatisticsObjectID, StatisticsObjectCulture, HitsStatisticsID
		SET @hitsID = 0;
		SELECT @hitsID = HitsID, @hitsCount = HitsCount FROM [Analytics_MonthHits]
		WHERE HitsStatisticsID=@HitsStatID AND HitsStartTime>=@Month1Start AND HitsEndTime<=@Month1End;
		IF @hitsID > 0
		BEGIN
			UPDATE [Analytics_MonthHits] SET HitsCount=(@hitsCount-@CntL) WHERE HitsID=@hitsID;
		END;
    END
    IF (@Month2Start > @FromDate)
    BEGIN
        SET @CntR = 0;
		SELECT @CntR = SUM(HitsCount) FROM Analytics_Statistics, Analytics_DayHits
			  WHERE StatisticsCode=@CodeName AND StatisticsID=HitsStatisticsID AND
              HitsStartTime >= @Month2Start AND @ToDate >= HitsEndTime AND
              StatisticsID = @HitsStatID
		GROUP BY StatisticsID, StatisticsObjectName, StatisticsObjectID, StatisticsObjectCulture, HitsStatisticsID
		SET @hitsID = 0;
		SELECT @hitsID = HitsID, @hitsCount = HitsCount FROM [Analytics_MonthHits]
		WHERE HitsStatisticsID=@HitsStatID AND HitsStartTime>=@Month2Start AND HitsEndTime<=@Month2End;
		IF @hitsID > 0
		BEGIN
			UPDATE [Analytics_MonthHits] SET HitsCount=(@hitsCount-@CntR) WHERE HitsID=@hitsID;
		END;
    END
    IF (@Month1Start <= @FromDate AND @ToDate <= @Month1End)
    BEGIN
        SET @CntM = 0;
		SELECT @CntM = SUM(HitsCount) FROM Analytics_Statistics, Analytics_DayHits
			  WHERE StatisticsCode=@CodeName AND StatisticsID=HitsStatisticsID AND
              HitsStartTime >= @FromDate AND @ToDate >= HitsEndTime AND
              StatisticsID = @HitsStatID
		GROUP BY StatisticsID, StatisticsObjectName, StatisticsObjectID, StatisticsObjectCulture, HitsStatisticsID
		SET @hitsID = 0;
		SELECT @hitsID = HitsID, @hitsCount = HitsCount FROM [Analytics_MonthHits]
		WHERE HitsStatisticsID=@HitsStatID AND HitsStartTime>=@Month1Start AND HitsEndTime<=@Month1End;
		IF @hitsID > 0
		BEGIN
			UPDATE [Analytics_MonthHits] SET HitsCount=(@hitsCount-@CntM) WHERE HitsID=@hitsID;
		END;
    END;
-- YEARS
    IF (@Year1End < @ToDate)
    BEGIN
        SET @CntL = 0;
		SELECT @CntL = SUM(HitsCount) FROM Analytics_Statistics, Analytics_DayHits
			  WHERE StatisticsCode=@CodeName AND StatisticsID=HitsStatisticsID AND
              HitsStartTime >= @FromDate AND @Year1End >= HitsEndTime AND
              StatisticsID = @HitsStatID
		GROUP BY StatisticsID, StatisticsObjectName, StatisticsObjectID, StatisticsObjectCulture, HitsStatisticsID
		SET @hitsID = 0;
		SELECT @hitsID = HitsID, @hitsCount = HitsCount FROM [Analytics_YearHits]
		WHERE HitsStatisticsID=@HitsStatID AND HitsStartTime>=@Year1Start AND HitsEndTime<=@Year1End;
		IF @hitsID > 0
		BEGIN
			UPDATE [Analytics_YearHits] SET HitsCount=(@hitsCount-@CntL) WHERE HitsID=@hitsID;
		END;
    END
    IF (@Year2Start > @FromDate)
    BEGIN
        SET @CntR = 0;
		SELECT @CntR = SUM(HitsCount) FROM Analytics_Statistics, Analytics_DayHits
			  WHERE StatisticsCode=@CodeName AND StatisticsID=HitsStatisticsID AND
              HitsStartTime >= @Year2Start AND @ToDate >= HitsEndTime AND
              StatisticsID = @HitsStatID
		GROUP BY StatisticsID, StatisticsObjectName, StatisticsObjectID, StatisticsObjectCulture, HitsStatisticsID
		SET @hitsID = 0;
		SELECT @hitsID = HitsID, @hitsCount = HitsCount FROM [Analytics_YearHits]
		WHERE HitsStatisticsID=@HitsStatID AND HitsStartTime>=@Year2Start AND HitsEndTime<=@Year2End;
		IF @hitsID > 0
		BEGIN
			UPDATE [Analytics_YearHits] SET HitsCount=(@hitsCount-@CntR) WHERE HitsID=@hitsID;
		END;
    END
    IF (@Year1Start <= @FromDate AND @ToDate <= @Year1End)
    BEGIN
        SET @CntM = 0;
		SELECT @CntM = SUM(HitsCount) FROM Analytics_Statistics, Analytics_DayHits
			  WHERE StatisticsCode=@CodeName AND StatisticsID=HitsStatisticsID AND
              HitsStartTime >= @FromDate AND @ToDate >= HitsEndTime AND
              StatisticsID = @HitsStatID
		GROUP BY StatisticsID, StatisticsObjectName, StatisticsObjectID, StatisticsObjectCulture, HitsStatisticsID
		SET @hitsID = 0;
		SELECT @hitsID = HitsID, @hitsCount = HitsCount FROM [Analytics_YearHits]
		WHERE HitsStatisticsID=@HitsStatID AND HitsStartTime>=@Year1Start AND HitsEndTime<=@Year1End;
		IF @hitsID > 0
		BEGIN
			UPDATE [Analytics_YearHits] SET HitsCount=(@hitsCount-@CntM) WHERE HitsID=@hitsID;
		END;
    END;
    DELETE FROM [Analytics_HourHits] WHERE 
      HitsStatisticsID=@HitsStatID AND HitsStartTime>=@FromDate AND HitsEndTime<=@ToDate;
    DELETE FROM [Analytics_DayHits] WHERE 
      HitsStatisticsID=@HitsStatID AND HitsStartTime>=@FromDate AND HitsEndTime<=@ToDate;
    IF (@FromDate <= @Week1End AND @Week2Start <= @ToDate)
    BEGIN
    DELETE FROM [Analytics_WeekHits] WHERE 
      HitsStatisticsID=@HitsStatID AND HitsStartTime>=@Week1End AND HitsEndTime<=@Week2Start;
    END;
    IF (@FromDate <= @Month1End AND @Month2Start <= @ToDate)
    BEGIN
    DELETE FROM [Analytics_MonthHits] WHERE 
      HitsStatisticsID=@HitsStatID AND HitsStartTime>=@Month1End AND HitsEndTime<=@Month2Start;
    END;    
    IF (@FromDate <= @Year1End AND @Year2Start <= @ToDate)
    BEGIN
    DELETE FROM [Analytics_YearHits] WHERE 
      HitsStatisticsID=@HitsStatID AND HitsStartTime>=@Year1End AND HitsEndTime<=@Year2Start;
    END;    
	FETCH NEXT FROM mycursor INTO @HitsStatID
	END
	DEALLOCATE mycursor;
	-- Delete zero stats
	DELETE FROM [Analytics_HourHits] WHERE HitsCount <= 0
	DELETE FROM [Analytics_DayHits] WHERE HitsCount <= 0
	DELETE FROM [Analytics_MonthHits] WHERE HitsCount <= 0
	DELETE FROM [Analytics_WeekHits] WHERE HitsCount <= 0
	DELETE FROM [Analytics_YearHits] WHERE HitsCount <= 0
	DECLARE @stat TABLE (
	  StatisticsID int
	)
	-- Get stats ID with no stats
	INSERT INTO @stat SELECT StatisticsID FROM (
	(SELECT StatisticsID FROM [Analytics_Statistics] WHERE StatisticsID NOT IN (SELECT HitsStatisticsID FROM [Analytics_HourHits])) UNION
	(SELECT StatisticsID FROM [Analytics_Statistics] WHERE StatisticsID NOT IN (SELECT HitsStatisticsID FROM [Analytics_DayHits])) UNION
	(SELECT StatisticsID FROM [Analytics_Statistics] WHERE StatisticsID NOT IN (SELECT HitsStatisticsID FROM [Analytics_WeekHits])) UNION
	(SELECT StatisticsID FROM [Analytics_Statistics] WHERE StatisticsID NOT IN (SELECT HitsStatisticsID FROM [Analytics_MonthHits])) UNION
	(SELECT StatisticsID FROM [Analytics_Statistics] WHERE StatisticsID NOT IN (SELECT HitsStatisticsID FROM [Analytics_YearHits]))
	) as tab
	-- Remove dependencies
	DELETE FROM [Analytics_HourHits] WHERE HitsStatisticsID IN (SELECT StatisticsID FROM @stat)
	DELETE FROM [Analytics_DayHits] WHERE HitsStatisticsID IN (SELECT StatisticsID FROM @stat)
	DELETE FROM [Analytics_WeekHits] WHERE HitsStatisticsID IN (SELECT StatisticsID FROM @stat)
	DELETE FROM [Analytics_MonthHits] WHERE HitsStatisticsID IN (SELECT StatisticsID FROM @stat)
	DELETE FROM [Analytics_YearHits] WHERE HitsStatisticsID IN (SELECT StatisticsID FROM @stat)
	-- Remove master record
	DELETE FROM [Analytics_Statistics] WHERE StatisticsID IN (SELECT StatisticsID FROM @stat)
END
