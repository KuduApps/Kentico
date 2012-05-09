CREATE FUNCTION [Func_Analytics_EndDateTrim]
(
        @EndDate DATETIME,
        @DayPart   VARCHAR(5) 
)        
RETURNS DATETIME
AS BEGIN      
	
	IF (@DayPart = 'year') BEGIN
		-- Trim time
		SET @EndDate =  DATEADD(dd, DATEDIFF(dd, 0, @EndDate), 0);
		-- Get first day of next year
		SET @EndDate = DATEADD(yy, DATEDIFF(yy, 0, DATEADD(YEAR,1,@EndDate)), 0);		
	END;
	
	
	IF (@DayPart = 'month') 
	BEGIN
		-- Trim time
		SET @EndDate =  DATEADD(dd, DATEDIFF(dd, 0, @EndDate), 0);
	
		-- Get first day of month
		SET @EndDate = DATEADD(dd,-(DAY(@EndDate)-1),@EndDate);
		
		-- Add month
		SET @EndDate = DATEADD(mm,1,@EndDate);
			
	END;
	
	IF (@DayPart = 'week') BEGIN		
		-- Trim time
		SET @EndDate =  DATEADD(dd, DATEDIFF(dd, 0, @EndDate), 0);
		---- Get first day of week
		SET @EndDate = DATEADD(wk, DATEDIFF(wk, 6, @EndDate), 6);
		
		-- Add week
		SET @EndDate = DATEADD(dd,7,@EndDate);
	END;
	
	IF (@DayPart ='day') BEGIN
		SET @EndDate =  DATEADD(dd, DATEDIFF(dd, 0, @EndDate), 0);
		
		-- Add day
		SET @EndDate = DATEADD(dd,1,@EndDate);
	END;
	
	IF (@DayPart = 'hour') BEGIN
		SET @EndDate =  DATEADD(hh, DATEDIFF(hh, 0, @EndDate), 0);
		
		-- Add hour
		SET @EndDate = DATEADD(hh,1,@EndDate);
	END;
	
	RETURN (@EndDate)
		
END;
