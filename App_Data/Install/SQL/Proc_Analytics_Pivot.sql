CREATE PROCEDURE [Proc_Analytics_Pivot]
@Type nvarchar(5),
@StaticColumns nvarchar(MAX) = ''
AS
BEGIN
	IF  OBJECT_ID('tempdb..#AnalyticsTempTable') IS NULL
	BEGIN
	   RETURN;
    END;
	IF ((SELECT COUNT (*) FROM #AnalyticsTempTable WHERE Name IS NOT NULL) > 0)
	BEGIN
        
		------ Get all columns for PIVOT functions
		DECLARE @Columns NVARCHAR(4000)
		DECLARE @ColumnsNull NVARCHAR (4000)
		---- Columns for pivot
		SELECT @Columns = COALESCE(@Columns + ',[' + CAST(Name AS VARCHAR(MAX)) + ']',
			'[' + CAST(Name AS VARCHAR(MAX))+ ']')
			FROM #AnalyticsTempTable
			GROUP BY Name ORDER BY Name
		---- Columns for ISNULL function
		SELECT @ColumnsNull = COALESCE(@ColumnsNull + ',ISNULL([' + CAST(Name AS VARCHAR(MAX)) + '],0) AS '''+CAST(Name AS VARCHAR(MAX))+'''',
			'ISNULL([' + CAST(Name AS VARCHAR(MAX))+ '],0) AS '''+ CAST(Name AS VARCHAR(MAX))+'''')
			FROM #AnalyticsTempTable
			GROUP BY Name ORDER BY Name
			
		------ Pivot function
		DECLARE @Query NVARCHAR(4000)
		IF (@Type ='week')
		BEGIN
			SET @Query = 'SELECT  CONVERT (NVARCHAR(2),DATEPART(wk,[StartTime]))+''/''+CONVERT (NVARCHAR(4),DATEPART(YEAR,[StartTime])), '+  @Columns + @StaticColumns + ' FROM #AnalyticsTempTable PIVOT ( SUM(Hits) FOR [Name] IN (' + @Columns + ') ) AS p'-- ORDER BY StartTime'
		END
		ELSE
		BEGIN
			SET @Query = 'SELECT  StartTime, '+  @Columns + @StaticColumns + '  FROM #AnalyticsTempTable PIVOT ( SUM(Hits) FOR [Name] IN (' + @Columns + ') ) AS p ORDER BY StartTime'
		END
		
		EXECUTE(@Query)
		
	END
	ELSE 
    BEGIN 
		IF (@Type ='week')
		BEGIN
			 EXECUTE('SELECT CONVERT (NVARCHAR(2),DATEPART(wk,[StartTime]))+''/''+CONVERT (NVARCHAR(4),DATEPART(YEAR,[StartTime])),Hits '+ @StaticColumns  +' FROM #AnalyticsTempTable');
		END
		ELSE
		BEGIN
			EXECUTE('SELECT StartTime,Hits'+@StaticColumns+' FROM #AnalyticsTempTable')
		END
	END
END
