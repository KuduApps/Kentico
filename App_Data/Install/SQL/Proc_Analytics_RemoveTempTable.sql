CREATE PROCEDURE [Proc_Analytics_RemoveTempTable]
AS
BEGIN
 IF  OBJECT_ID('tempdb..#AnalyticsTempTable') IS NOT NULL
	BEGIN
	   DROP TABLE #AnalyticsTempTable;
    END;
END
