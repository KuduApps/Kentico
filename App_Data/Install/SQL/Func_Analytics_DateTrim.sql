CREATE FUNCTION [Func_Analytics_DateTrim]
(
        @FromDate DATETIME,
        @DayPart   VARCHAR(5) 
)        
RETURNS DATETIME
AS BEGIN      
	DECLARE @StartDate DATETIME;	
	IF (@DayPart = 'year') BEGIN
		-- Trim time
		SET @FromDate =  DATEADD(dd, DATEDIFF(dd, 0, @FromDate), 0);
		-- Get first day of year
		SET @StartDate = DATEADD(yy, DATEDIFF(yy, 0, @FromDate), 0)
		-- if not same show next year
		IF (@StartDate <> @FromDate)
		BEGIN
			SET @FromDate = @StartDate;
		END; 
	END;
	
	IF (@DayPart = 'month') 
	BEGIN
		-- Trim time
		SET @FromDate =  DATEADD(dd, DATEDIFF(dd, 0, @FromDate), 0);
	
		-- Get first day of month
		SET @StartDate = DATEADD(dd,-(DAY(@FromDate)-1),@FromDate);
		-- if not same show next month
		IF (@StartDate <> @FromDate)
		BEGIN
			SET @FromDate = @StartDate;
		END;
	END;
	
	IF (@DayPart = 'week') BEGIN		
		-- Trim time
		SET @FromDate =  DATEADD(dd, DATEDIFF(dd, 0, @FromDate), 0);
		---- Get first day of week
		SET @StartDate = DATEADD(wk, DATEDIFF(wk, 6, @FromDate), 6);
		--if not same show next week
		IF (@StartDate <> @FromDate)
		BEGIN
			SET @FromDate = @StartDate;
		END;
	END;
	
	IF (@DayPart ='day') BEGIN
		SET @FromDate =  DATEADD(dd, DATEDIFF(dd, 0, @FromDate), 0);
	END;
	
	IF (@DayPart = 'hour') BEGIN
		SET @FromDate =  DATEADD(hh, DATEDIFF(hh, 0, @FromDate), 0);
	END;
	
	RETURN (@FromDate)
		
END;
