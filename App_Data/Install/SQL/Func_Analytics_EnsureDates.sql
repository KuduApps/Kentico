CREATE FUNCTION [Func_Analytics_EnsureDates]
        (
        @StartDate DATETIME,
        @EndDate   DATETIME,
        @DayPart   VARCHAR(5) -- support 'day','month','week','year','hour', default 'day'
        )
RETURNS @rtnTable TABLE
(
	[DATE] datetime PRIMARY KEY
)
AS
BEGIN
	WITH dateTable AS
	(
		SELECT TOP (
			CASE @DayPart 
				WHEN 'day'   THEN DATEDIFF(dd,@StartDate,@EndDate)+1
				WHEN 'month' THEN DATEDIFF(mm,@StartDate,@EndDate)+1
				WHEN 'year'  THEN DATEDIFF(yy,@StartDate,@EndDate)+1
				WHEN 'hour'  THEN DATEDIFF(hh,@StartDate,@EndDate)+1
				WHEN 'week'  THEN DATEDIFF(ww,@StartDate,@EndDate)+1
			END  
		)
		ROW_NUMBER() OVER (ORDER BY GetDate())-1 AS N
		FROM sys.All_Columns Sc1
		CROSS JOIN sys.All_Columns Sc2
    )
    INSERT INTO @rtnTable
    SELECT CASE @DayPart 
		WHEN 'day'   THEN DATEADD(dd,t.N,@StartDate)
		WHEN 'month' THEN DATEADD(mm,t.N,@StartDate)
		WHEN 'year'  THEN DATEADD(yy,t.N,@StartDate)
		WHEN 'hour'  THEN DATEADD(hh,t.N,@StartDate)
		WHEN 'week'  THEN DATEADD(ww,t.N,@StartDate)
	END AS DATE
    FROM dateTable t
    
    RETURN
END
