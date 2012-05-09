CREATE PROCEDURE [Proc_Reporting_FileDownloads] 
	-- @CMSContextCurrentSiteID int,
    @CodeName nvarchar(50),
    @FromDate datetime,
    @ToDate datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    SELECT TOP 100 DocumentNamePath AS '@NamePathHeader', HitsCount AS '@HitsCountHeader'
    FROM Analytics_Statistics, Analytics_DayHits, CMS_Tree, CMS_Document WHERE
    (StatisticsSiteID = 1) AND (StatisticsCode=@CodeName)
    AND (StatisticsID = HitsStatisticsID)
    AND (DocumentID = NodeID)
    AND (StatisticsObjectID = NodeID)
    AND (HitsStartTime >= @FromDate)
    AND (HitsEndTime <= @ToDate)
    GROUP BY DocumentNamePath, HitsCount
    ORDER BY HitsCount DESC
END
