CREATE PROCEDURE [Proc_Analytics_ProcessLog]
@SiteID int,
    @Codename nvarchar(250),
    @Culture nvarchar(10),
    @ObjectName nvarchar(450),
    @ObjectID int,
    @Hits int,
    @HourStart datetime,
    @HourEnd datetime,
    @DayStart datetime,
    @DayEnd datetime,
    @WeekStart datetime,
    @WeekEnd datetime,
    @MonthStart datetime,
    @MonthEnd datetime,
    @YearStart datetime,
    @YearEnd datetime,
    @Value float
AS
BEGIN
/* Declare the @statisticsID variable */
	DECLARE @statisticsID int;
	SET @statisticsID = 0;
    SELECT @statisticsID = StatisticsID FROM Analytics_Statistics
	WHERE (StatisticsSiteID = @SiteID) AND (StatisticsCode = @Codename) AND ((StatisticsObjectName = @ObjectName) OR (StatisticsObjectName IS NULL AND @ObjectName IS NULL))
		AND	((StatisticsObjectID = @ObjectID) OR (StatisticsObjectID IS NULL OR @ObjectID IS NULL)) AND ((StatisticsObjectCulture = @Culture) OR (StatisticsObjectCulture IS NULL AND @Culture IS NULL));
	
	/* If @statisticsID is 0 insert new record */
	IF @statisticsID = 0
	BEGIN
		INSERT INTO Analytics_Statistics (StatisticsSiteID, StatisticsCode, StatisticsObjectName, StatisticsObjectID, StatisticsObjectCulture)
		VALUES (@SiteID, @Codename, @ObjectName, @ObjectID, @Culture);
		/* Get StatisticsID */
        SELECT @statisticsID = StatisticsID FROM Analytics_Statistics
     	WHERE (StatisticsSiteID = @SiteID) AND (StatisticsCode = @Codename) AND ((StatisticsObjectName = @ObjectName) OR (StatisticsObjectName IS NULL AND @ObjectName IS NULL))
	  		AND	((StatisticsObjectID = @ObjectID) OR (StatisticsObjectID IS NULL OR @ObjectID IS NULL)) AND ((StatisticsObjectCulture = @Culture) OR (StatisticsObjectCulture IS NULL AND @Culture IS NULL));
	END
	
	/* Declare @hitsID and @hitsCount variables */
	DECLARE @hitsID int, @hitsCount int, @hitsValue float;
	SET @hitsCount = 0;
	SET @hitsID = 0;
	SET @hitsValue = 0;
	
	
	/* HOURS */	
	SELECT @hitsID = HitsID, @hitsCount = HitsCount, @hitsValue = ISNULL(HitsValue,0) FROM [Analytics_HourHits]
	WHERE HitsStatisticsID=@statisticsID AND HitsStartTime=@HourStart AND HitsEndTime=@HourEnd;
	IF @hitsID > 0
		UPDATE [Analytics_HourHits] SET HitsCount=(@hitsCount+@Hits), HitsValue=(@hitsValue + @Value)  WHERE HitsID=@hitsID;
	ELSE
		INSERT INTO [Analytics_HourHits] ([HitsStatisticsID],[HitsStartTime],[HitsEndTime],[HitsCount],[HitsValue])
		VALUES (@statisticsID,@HourStart,@HourEnd,@Hits,@Value);
	
	/* DAYS */
	SET @hitsID = 0;
	SELECT @hitsID = HitsID, @hitsCount = HitsCount, @hitsValue = ISNULL(HitsValue,0) FROM [Analytics_DayHits]
	WHERE HitsStatisticsID=@statisticsID AND HitsStartTime=@DayStart AND HitsEndTime=@DayEnd;
	IF @hitsID > 0
		UPDATE [Analytics_DayHits] SET HitsCount=(@hitsCount+@Hits), HitsValue=(@hitsValue + @Value) WHERE HitsID=@hitsID;
	ELSE
		INSERT INTO [Analytics_DayHits] ([HitsStatisticsID],[HitsStartTime],[HitsEndTime],[HitsCount],[HitsValue])
		VALUES (@statisticsID,@DayStart,@DayEnd,@Hits,@Value);
	
	/* WEEKS */
	SET @hitsID = 0;
	SELECT @hitsID = HitsID, @hitsCount = HitsCount, @hitsValue = ISNULL(HitsValue,0) FROM [Analytics_WeekHits]
	WHERE HitsStatisticsID=@statisticsID AND HitsStartTime=@WeekStart AND HitsEndTime=@WeekEnd;
	IF @hitsID > 0
		UPDATE [Analytics_WeekHits] SET HitsCount=(@hitsCount+@Hits), HitsValue=(@hitsValue + @Value) WHERE HitsID=@hitsID;
	ELSE
		INSERT INTO [Analytics_WeekHits] ([HitsStatisticsID],[HitsStartTime],[HitsEndTime],[HitsCount],[HitsValue])
		VALUES (@statisticsID,@WeekStart,@WeekEnd,@Hits,@Value);
	
	/* MONTHS */
	SET @hitsID = 0;
	SELECT @hitsID = HitsID, @hitsCount = HitsCount, @hitsValue = ISNULL(HitsValue,0) FROM [Analytics_MonthHits]
	WHERE HitsStatisticsID=@statisticsID AND HitsStartTime=@MonthStart AND HitsEndTime=@MonthEnd;
	IF @hitsID > 0
		UPDATE [Analytics_MonthHits] SET HitsCount=(@hitsCount+@Hits), HitsValue=(@hitsValue + @Value) WHERE HitsID=@hitsID;
	ELSE
		INSERT INTO [Analytics_MonthHits] ([HitsStatisticsID],[HitsStartTime],[HitsEndTime],[HitsCount],[HitsValue])
		VALUES (@statisticsID,@MonthStart,@MonthEnd,@Hits,@Value);
	
	/* YEARS */
	SET @hitsID = 0;
	SELECT @hitsID = HitsID, @hitsCount = HitsCount, @hitsValue = ISNULL(HitsValue,0) FROM [Analytics_YearHits]
	WHERE HitsStatisticsID=@statisticsID AND HitsStartTime=@YearStart AND HitsEndTime=@YearEnd;
	IF @hitsID > 0
		UPDATE [Analytics_YearHits] SET HitsCount=(@hitsCount+@Hits), HitsValue=(@hitsValue + @Value) WHERE HitsID=@hitsID;
	ELSE
		INSERT INTO [Analytics_YearHits] ([HitsStatisticsID],[HitsStartTime],[HitsEndTime],[HitsCount],[HitsValue])
		VALUES (@statisticsID,@YearStart,@YearEnd,@Hits,@Value);
END
