SET IDENTITY_INSERT [Reporting_ReportTable] ON
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (102, N'table', N'table', N'DECLARE @PaveViews TABLE
(
  DocumentNamePath NVARCHAR(500),
  Pageviews INT,
  Percents DECIMAL(10,2)  
)
DECLARE @Sum DECIMAL;
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day'');
SELECT @Sum =   
  SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_DayHits ON Analytics_DayHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND StatisticsObjectCulture = DocumentCulture 
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
 AND (HitsEndTime <= @ToDate)
INSERT INTO @PaveViews (DocumentNamePath,pageViews,percents)
SELECT TOP 100
 CASE
  WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName
  ELSE DocumentNamePath
 END , SUM(HitsCount), (SUM(HitsCount)/@Sum)*100
 FROM Analytics_Statistics
 INNER JOIN Analytics_DayHits ON Analytics_DayHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
 LEFT JOIN View_CMS_Tree_Joined ON Analytics_Statistics.StatisticsObjectID = View_CMS_Tree_Joined.NodeID AND StatisticsObjectCulture = DocumentCulture
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
 AND (StatisticsCode=@CodeName)
 GROUP BY DocumentNamePath, StatisticsObjectName
 ORDER BY [DocumentNamePath] DESC
 
  
 SELECT DocumentNamePath AS ''{$general.documentname$}'',pageviews AS  ''{$reports_filedownloads_Day.hits_header$}'',
   CAST (Percents AS NVARCHAR(10))+''%'' AS ''{$reports_filedownloads_Day.hits_percent_header$}''
   FROM @PaveViews ORDER BY PageViews DESC', 0, 55, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><pagesize>10</pagesize><enablepaging>False</enablepaging><skinid>ReportGridAnalytics</skinid><pagemode>1</pagemode></customdata>', 'ba4442d3-5e9f-43b1-b13f-6d2ab3625737', '20110923 12:19:24')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (103, N'table', N'table', N'DECLARE @PaveViews TABLE
(
  DocumentNamePath NVARCHAR(500),
  Pageviews INT,
  Percents DECIMAL(10,2)  
)
DECLARE @Sum DECIMAL;
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''month'');
SELECT @Sum =   
  SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_MonthHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND StatisticsObjectCulture = DocumentCulture 
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
 AND (HitsEndTime <= @ToDate)
INSERT INTO @PaveViews (DocumentNamePath,pageViews,percents)
SELECT TOP 100
 CASE
  WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName
  ELSE DocumentNamePath
 END , SUM(HitsCount), (SUM(HitsCount)/@Sum)*100
 FROM Analytics_Statistics
 INNER JOIN Analytics_MonthHits ON Analytics_MonthHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
 LEFT JOIN View_CMS_Tree_Joined ON Analytics_Statistics.StatisticsObjectID = View_CMS_Tree_Joined.NodeID AND StatisticsObjectCulture = DocumentCulture
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
 AND (StatisticsCode=@CodeName)
 GROUP BY DocumentNamePath, StatisticsObjectName
 ORDER BY [DocumentNamePath] DESC
 
  
 SELECT DocumentNamePath AS ''{$general.documentname$}'',pageviews AS  ''{$reports_filedownloads_Month.hits_header$}'',
   CAST (Percents AS NVARCHAR(10))+''%'' AS ''{$reports_filedownloads_Month.hits_percent_header$}''
   FROM @PaveViews ORDER BY PageViews DESC', 0, 56, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><pagesize>10</pagesize><enablepaging>False</enablepaging><skinid>ReportGridAnalytics</skinid><pagemode>1</pagemode></customdata>', '380150e3-442f-4ea7-a543-c8fd77d7bf6d', '20110923 12:19:24')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (104, N'table', N'table', N'DECLARE @PaveViews TABLE
(
  DocumentNamePath NVARCHAR(500),
  Pageviews INT,
  Percents DECIMAL(10,2)  
)
DECLARE @Sum DECIMAL;
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''year'');
SELECT @Sum =   
  SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_YearHits ON Analytics_YearHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND StatisticsObjectCulture = DocumentCulture 
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
 AND (HitsEndTime <= @ToDate)
INSERT INTO @PaveViews (DocumentNamePath,pageViews,percents)
SELECT TOP 100
 CASE
  WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName
  ELSE DocumentNamePath
 END , SUM(HitsCount), (SUM(HitsCount)/@Sum)*100
 FROM Analytics_Statistics
 INNER JOIN Analytics_YearHits ON Analytics_YearHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
 LEFT JOIN View_CMS_Tree_Joined ON Analytics_Statistics.StatisticsObjectID = View_CMS_Tree_Joined.NodeID AND StatisticsObjectCulture = DocumentCulture
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
 AND (StatisticsCode=@CodeName)
 GROUP BY DocumentNamePath, StatisticsObjectName
 ORDER BY [DocumentNamePath] DESC
 
  
 SELECT DocumentNamePath AS ''{$general.documentname$}'',pageviews AS  ''{$reports_filedownloads_Year.hits_header$}'',
   CAST (Percents AS NVARCHAR(10))+''%'' AS ''{$reports_filedownloads_Year.hits_percent_header$}''
   FROM @PaveViews ORDER BY PageViews DESC', 0, 57, N'<customdata><querynorecordtext></querynorecordtext><exportenabled>True</exportenabled><pagesize>10</pagesize><enablepaging>False</enablepaging><skinid>ReportGridAnalytics</skinid><pagemode>1</pagemode></customdata>', '1ff97f22-6fbb-46d9-b102-ec2e00efccc3', '20110923 12:19:24')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (105, N'table', N'table', N'DECLARE @PaveViews TABLE
(
  DocumentNamePath NVARCHAR(500),
  Pageviews INT,
  Percents DECIMAL(10,2)  
)
DECLARE @Sum DECIMAL;
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''hour'');
SELECT @Sum =   
  SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_HourHits ON Analytics_HourHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND StatisticsObjectCulture = DocumentCulture 
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
 AND (HitsEndTime <= @ToDate)
INSERT INTO @PaveViews (DocumentNamePath,pageViews,percents)
SELECT TOP 100
 CASE
  WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName
  ELSE DocumentNamePath
 END , SUM(HitsCount), (SUM(HitsCount)/@Sum)*100
 FROM Analytics_Statistics
 INNER JOIN Analytics_HourHits ON Analytics_HourHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
 LEFT JOIN View_CMS_Tree_Joined ON Analytics_Statistics.StatisticsObjectID = View_CMS_Tree_Joined.NodeID AND StatisticsObjectCulture = DocumentCulture
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
 AND (StatisticsCode=@CodeName)
 GROUP BY DocumentNamePath, StatisticsObjectName
 ORDER BY [DocumentNamePath] DESC
 
  
 SELECT DocumentNamePath AS ''{$general.documentname$}'',pageviews AS  ''{$reports_filedownloads_Hour.hits_header$}'',
   CAST (Percents AS NVARCHAR(10))+''%'' AS ''{$reports_filedownloads_Hour.hits_percent_header$}''
   FROM @PaveViews ORDER BY PageViews DESC', 0, 58, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><pagesize>10</pagesize><enablepaging>False</enablepaging><skinid>ReportGridAnalytics</skinid><pagemode>1</pagemode></customdata>', '8127bec1-8f00-48ee-b356-4acbd72bd6ce', '20110923 12:19:24')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (106, N'RecentlyModifiedDocsTable', N'Recently modified docs table', N'Select DocumentName AS ''Document name'', NodeAliasPath AS ''Document alias path'', DocumentModifiedWhen AS ''Last modification date'', FirstName + '' ''  + LastName + '' ('' + UserName +'')'' AS ''Last modified by'', StepDisplayName AS ''Current workflow step''  FROM View_CMS_Tree_Joined LEFT JOIN CMS_User ON View_CMS_Tree_Joined.DocumentModifiedByUserID=CMS_User.UserID LEFT JOIN CMS_WorkflowStep ON View_CMS_Tree_Joined.DocumentWorkflowStepID = CMS_WorkflowStep.StepID Where (DocumentModifiedWhen >= @ModifiedSince) AND (NodeSiteID=@CMSContextCurrentSiteID) ORDER BY DocumentModifiedWhen DESC', 0, 50, N'<customdata><pagesize>10</pagesize><skinid></skinid><querynorecordtext>No recently modified documents found</querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', '84b838bf-a8e2-4852-a784-c7c99d0f2941', '20110819 07:07:34')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (107, N'table', N'table', N'DECLARE @PaveViews TABLE
(
  DocumentNamePath NVARCHAR(500),
  ObjectID INT,
  Pageviews INT,
  Percents DECIMAL(10,2),
  Average INT  
)
DECLARE @Sum DECIMAL;
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day'');
SELECT @Sum =   
  SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_DayHits ON Analytics_DayHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID  AND StatisticsObjectCulture = DocumentCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
 AND (HitsEndTime <= @ToDate)
INSERT INTO @PaveViews (DocumentNamePath,objectID,pageViews,percents)
SELECT TOP 100
 CASE
  WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName
  ELSE DocumentNamePath
 END , StatisticsObjectID, SUM(HitsCount), (SUM(HitsCount)/@Sum)*100
 FROM Analytics_Statistics
 INNER JOIN Analytics_DayHits ON Analytics_DayHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
 LEFT JOIN View_CMS_Tree_Joined ON Analytics_Statistics.StatisticsObjectID = View_CMS_Tree_Joined.NodeID AND StatisticsObjectCulture = DocumentCulture
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
 AND (StatisticsCode=@CodeName)
 GROUP BY DocumentNamePath, StatisticsObjectName,StatisticsObjectID
 ORDER BY SUM(HitsCount) DESC
 
 UPDATE @PaveViews SET Average = (SELECT SUM(HitsValue)/SUM(HitsCount) FROM Analytics_DayHits JOIN
      Analytics_Statistics ON HitsStatisticsID = StatisticsID
      WHERE HitsStartTime >= @FromDate AND HitsEndTime <= @ToDate AND StatisticsObjectID = objectID
        AND StatisticsCode =''avgtimeonpage'' AND StatisticsSiteID = @CMSContextCurrentSiteID
       )
 
 SELECT DocumentNamePath AS ''{$reports_pageviews_Day.path_header$}'',pageviews AS  ''{$reports_pageviews_Day.hits_header$}'',
      CAST (Percents AS NVARCHAR(10))+''%'' AS ''{$reports_pageviews.percent_header$}'', ISNULL(CONVERT(varchar, DATEADD(s, average, 0), 108),''00:00:00'') AS ''{$reports_pageviews.average$}''
  
   FROM @PaveViews ORDER BY PageViews DESC', 0, 59, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><pagesize>10</pagesize><enablepaging>False</enablepaging><skinid>ReportGridAnalytics</skinid><pagemode>1</pagemode></customdata>', '47f63e99-8a64-49c9-ad7e-4be17f5c9151', '20110920 14:00:49')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (108, N'table', N'table', N'DECLARE @PaveViews TABLE
(
  DocumentNamePath NVARCHAR(500),
  ObjectID INT,
  Pageviews INT,
  Percents DECIMAL(10,2),
  Average INT  
)
DECLARE @Sum DECIMAL;
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''month'');
SELECT @Sum =   
  SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_MonthHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID  AND StatisticsObjectCulture = DocumentCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
 AND (HitsEndTime <= @ToDate)
INSERT INTO @PaveViews (DocumentNamePath,objectID,pageViews,percents)
SELECT TOP 100
 CASE
  WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName
  ELSE DocumentNamePath
 END , StatisticsObjectID, SUM(HitsCount), (SUM(HitsCount)/@Sum)*100
 FROM Analytics_Statistics
 INNER JOIN Analytics_MonthHits ON Analytics_MonthHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
 LEFT JOIN View_CMS_Tree_Joined ON Analytics_Statistics.StatisticsObjectID = View_CMS_Tree_Joined.NodeID AND StatisticsObjectCulture = DocumentCulture
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
 AND (StatisticsCode=@CodeName)
 GROUP BY DocumentNamePath, StatisticsObjectName,StatisticsObjectID
 ORDER BY SUM(HitsCount) DESC
 
 UPDATE @PaveViews SET Average = (SELECT SUM(HitsValue)/SUM(HitsCount) FROM Analytics_MonthHits JOIN
      Analytics_Statistics ON HitsStatisticsID = StatisticsID
      WHERE HitsStartTime >= @FromDate AND HitsEndTime <= @ToDate AND StatisticsObjectID = objectID
        AND StatisticsCode =''avgtimeonpage'' AND StatisticsSiteID = @CMSContextCurrentSiteID
       )
 
 SELECT DocumentNamePath AS ''{$reports_pageviews_Month.path_header$}'',pageviews AS  ''{$reports_pageviews_Month.hits_header$}'',
      CAST (Percents AS NVARCHAR(10))+''%'' AS ''{$reports_pageviews.percent_header$}'', ISNULL(CONVERT(varchar, DATEADD(s, average, 0), 108),''00:00:00'') AS ''{$reports_pageviews.average$}''
  
   FROM @PaveViews ORDER BY PageViews DESC', 0, 60, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><pagesize>10</pagesize><enablepaging>False</enablepaging><skinid>ReportGridAnalytics</skinid><pagemode>1</pagemode></customdata>', '978ecb24-c417-468d-a901-c4c298a8b02a', '20110920 14:00:49')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (109, N'CheckedOutDocsTable', N'Checked out docs table', N'Select DocumentName AS ''Document name'', NodeAliasPath AS ''Document alias path'', FirstName + '' '' + LastName + '' ('' +UserName+'')'' AS ''Checked out by'', DocumentCheckedOutWhen AS ''Checked out on'', FirstName + '' ''  + LastName + '' ('' + UserName +'')'' AS ''Last modified by'', StepDisplayName AS ''Current workflow step'' FROM View_CMS_Tree_Joined LEFT JOIN CMS_User ON CMS_User.UserID = View_CMS_Tree_Joined.DocumentCheckedOutByUserID  LEFT JOIN CMS_WorkflowStep ON View_CMS_Tree_Joined.DocumentWorkflowStepID = CMS_WorkflowStep.StepID  Where (DocumentCheckedOutByUserID IS NOT NULL) AND (NodeSiteID=@CMSContextCurrentSiteID) ORDER BY DocumentCheckedOutWhen DESC', 0, 51, N'<customdata><pagesize>10</pagesize><skinid></skinid><querynorecordtext>No checked out documents found</querynorecordtext><exportenabled>true</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'b25b33ab-cdda-4018-b0bf-7cfcaaad651e', '20110621 11:09:30')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (110, N'table', N'table', N'DECLARE @PaveViews TABLE
(
  DocumentNamePath NVARCHAR(500),
  ObjectID INT,
  Pageviews INT,
  Percents DECIMAL(10,2),
  Average INT  
)
DECLARE @Sum DECIMAL;
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''year'');
SELECT @Sum =   
  SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_YearHits ON Analytics_YearHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID  AND StatisticsObjectCulture = DocumentCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
 AND (HitsEndTime <= @ToDate)
INSERT INTO @PaveViews (DocumentNamePath,objectID,pageViews,percents)
SELECT TOP 100
 CASE
  WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName
  ELSE DocumentNamePath
 END , StatisticsObjectID, SUM(HitsCount), (SUM(HitsCount)/@Sum)*100
 FROM Analytics_Statistics
 INNER JOIN Analytics_YearHits ON Analytics_YearHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
 LEFT JOIN View_CMS_Tree_Joined ON Analytics_Statistics.StatisticsObjectID = View_CMS_Tree_Joined.NodeID AND StatisticsObjectCulture = DocumentCulture
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
 AND (StatisticsCode=@CodeName)
 GROUP BY DocumentNamePath, StatisticsObjectName,StatisticsObjectID
 ORDER BY SUM(HitsCount) DESC
 
 UPDATE @PaveViews SET Average = (SELECT SUM(HitsValue)/SUM(HitsCount) FROM Analytics_YearHits JOIN
      Analytics_Statistics ON HitsStatisticsID = StatisticsID
      WHERE HitsStartTime >= @FromDate AND HitsEndTime <= @ToDate AND StatisticsObjectID = objectID
        AND StatisticsCode =''avgtimeonpage'' AND StatisticsSiteID = @CMSContextCurrentSiteID
       )
 
 SELECT DocumentNamePath AS ''{$reports_pageviews_Year.path_header$}'',pageviews AS  ''{$reports_pageviews_Year.hits_header$}'',
      CAST (Percents AS NVARCHAR(10))+''%'' AS ''{$reports_pageviews.percent_header$}'', ISNULL(CONVERT(varchar, DATEADD(s, average, 0), 108),''00:00:00'') AS ''{$reports_pageviews.average$}''
  
   FROM @PaveViews ORDER BY PageViews DESC', 0, 61, N'<customdata><querynorecordtext></querynorecordtext><exportenabled>True</exportenabled><pagesize>10</pagesize><enablepaging>False</enablepaging><skinid>ReportGridAnalytics</skinid><pagemode>1</pagemode></customdata>', 'e33dd959-8cfe-4dee-ae4e-e1fef7055e59', '20110920 14:00:49')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (111, N'ExpiredDocsTable', N'Expired docs table', N'Select DocumentName AS ''Document name'', NodeAliasPath AS ''Document alias path'', DocumentPublishTo AS ''Expiration date'', FirstName + '' ''  + LastName + '' ('' + UserName +'')'' AS ''Last modified by'', StepDisplayName AS ''Current workflow step'' FROM View_CMS_Tree_Joined LEFT JOIN CMS_User ON View_CMS_Tree_Joined.DocumentModifiedByUserID=CMS_User.UserID LEFT JOIN CMS_WorkflowStep ON View_CMS_Tree_Joined.DocumentWorkflowStepID = CMS_WorkflowStep.StepID Where (DocumentPublishTo IS NOT NULL) AND (DocumentPublishTo < @CMSContextCurrentTime) AND (NodeSiteID=@CMSContextCurrentSiteID) ORDER BY DocumentPublishTo DESC', 0, 52, N'<customdata><pagesize>10</pagesize><skinid></skinid><querynorecordtext>No expired documents found</querynorecordtext><exportenabled>true</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '305d166c-9965-4076-8ba1-0eb3535815b8', '20110621 11:09:34')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (112, N'ArchivedDocsTable', N'Archived docs table', N'Select DocumentName AS ''Document name'', NodeAliasPath AS ''Document alias path'', FirstName + '' ''  + LastName + '' ('' + UserName +'')'' AS ''Last modified by'', StepDisplayName AS ''Current workflow step'' FROM View_CMS_Tree_Joined LEFT JOIN CMS_User ON View_CMS_Tree_Joined.DocumentModifiedByUserID=CMS_User.UserID LEFT JOIN CMS_WorkflowStep ON View_CMS_Tree_Joined.DocumentWorkflowStepID = CMS_WorkflowStep.StepID WHERE (DocumentWorkflowStepID IN (SELECT StepID From CMS_WorkflowStep WHERE StepName=''archived'') AND (NodeSiteID=@CMSContextCurrentSiteID)) ORDER BY NodeAliasPath', 0, 53, N'<customdata><pagesize>10</pagesize><skinid></skinid><querynorecordtext>No archived documents found</querynorecordtext><exportenabled>true</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'eaff66b0-76df-4898-98c2-f04f748a4bfe', '20110621 11:09:31')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (113, N'DocsWaitingForApproval', N'Docs waiting for approval', N'Select DocumentName AS ''Document name'', NodeAliasPath AS ''Document alias path'', FirstName + '' ''  + LastName + '' ('' + UserName +'')'' AS ''Last modified by'', StepDisplayName AS ''Current workflow step'' FROM View_CMS_Tree_Joined_Versions LEFT JOIN CMS_User ON View_CMS_Tree_Joined_Versions.DocumentModifiedByUserID=CMS_User.UserID LEFT JOIN CMS_WorkflowStep ON View_CMS_Tree_Joined_Versions.DocumentWorkflowStepID = CMS_WorkflowStep.StepID WHERE View_CMS_Tree_Joined_Versions.DocumentWorkflowStepID IN (SELECT StepID FROM CMS_WorkflowStep WHERE (StepName <> ''published'' and StepName <>''archived'')) AND (NodeSiteID=@CMSContextCurrentSiteID) ORDER BY NodeAliasPath', 0, 54, N'<customdata><pagesize>10</pagesize><skinid></skinid><querynorecordtext>No waiting documents found</querynorecordtext><exportenabled>true</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '15feabe3-d47c-4a6b-8345-1b87c7b3e576', '20110621 11:09:31')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (114, N'table', N'table', N'DECLARE @PaveViews TABLE
(
  DocumentNamePath NVARCHAR(500),
  ObjectID INT,
  Pageviews INT,
  Percents DECIMAL(10,2),
  Average INT  
)
DECLARE @Sum DECIMAL;
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''hour'');
SELECT @Sum =   
  SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_HourHits ON Analytics_HourHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID  AND StatisticsObjectCulture = DocumentCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
 AND (HitsEndTime <= @ToDate)
INSERT INTO @PaveViews (DocumentNamePath,objectID,pageViews,percents)
SELECT TOP 100
 CASE
  WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName
  ELSE DocumentNamePath
 END , StatisticsObjectID, SUM(HitsCount), (SUM(HitsCount)/@Sum)*100
 FROM Analytics_Statistics
 INNER JOIN Analytics_HourHits ON Analytics_HourHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
 LEFT JOIN View_CMS_Tree_Joined ON Analytics_Statistics.StatisticsObjectID = View_CMS_Tree_Joined.NodeID AND StatisticsObjectCulture = DocumentCulture
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
 AND (StatisticsCode=@CodeName)
 GROUP BY DocumentNamePath, StatisticsObjectName,StatisticsObjectID
 ORDER BY SUM(HitsCount) DESC
 
 UPDATE @PaveViews SET Average = (SELECT SUM(HitsValue)/SUM(HitsCount) FROM Analytics_HourHits JOIN
      Analytics_Statistics ON HitsStatisticsID = StatisticsID
      WHERE HitsStartTime >= @FromDate AND HitsEndTime <= @ToDate AND StatisticsObjectID = objectID
        AND StatisticsCode =''avgtimeonpage'' AND StatisticsSiteID = @CMSContextCurrentSiteID
       )
 
 SELECT DocumentNamePath AS ''{$reports_pageviews_Hour.path_header$}'',pageviews AS  ''{$reports_pageviews_Hour.hits_header$}'',
      CAST (Percents AS NVARCHAR(10))+''%'' AS ''{$reports_pageviews.percent_header$}'', ISNULL(CONVERT(varchar, DATEADD(s, average, 0), 108),''00:00:00'') AS ''{$reports_pageviews.average$}''
  
   FROM @PaveViews ORDER BY PageViews DESC', 0, 62, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><pagesize>10</pagesize><enablepaging>False</enablepaging><skinid>ReportGridAnalytics</skinid><pagemode>1</pagemode></customdata>', '079ba574-57b1-4d4f-8615-66c576439f55', '20110920 14:00:49')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (115, N'TableDayPageNotFound', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day'');
SELECT TOP 100 
 StatisticsObjectName AS ''{$reports_pagenotfound_day.name_header$}'', 
 SUM(HitsCount) AS ''{$reports_pagenotfound_day.hits_header$}'' 
 FROM Analytics_Statistics
 INNER JOIN Analytics_DayHits ON Analytics_DayHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) 
 AND (StatisticsCode=@CodeName) 
 AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate) 
 GROUP BY StatisticsObjectName 
 ORDER BY SUM(HitsCount) DESC', 0, 63, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'fca58f03-6c62-41f6-9bce-67bdb3974abc', '20110621 11:09:34')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (116, N'TableMonthPageNotFound', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''month'');
SELECT TOP 100 
 StatisticsObjectName AS ''{$reports_pagenotfound_month.name_header$}'', 
 SUM(HitsCount) AS ''{$reports_pagenotfound_month.hits_header$}'' 
 FROM Analytics_Statistics
 INNER JOIN Analytics_MonthHits ON Analytics_MonthHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) 
 AND (StatisticsCode=@CodeName)  
 AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate) 
 GROUP BY StatisticsObjectName 
 ORDER BY SUM(HitsCount) DESC', 0, 64, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '36733865-50cb-4775-b1fb-bf5cee65114a', '20110621 11:09:35')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (117, N'TableYearPageNotFound', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''year'');
SELECT TOP 100 StatisticsObjectName AS ''{$reports_pagenotfound_year.name_header$}'', SUM(HitsCount) AS ''{$reports_pagenotfound_year.hits_header$}'' FROM 
Analytics_Statistics, Analytics_YearHits WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND (StatisticsCode=@CodeName) AND (StatisticsID = HitsStatisticsID) AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate) GROUP BY StatisticsObjectName ORDER BY SUM(HitsCount) DESC', 0, 65, N'<customdata><querynorecordtext></querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'afaa4805-20b0-44cd-98f9-a3083c3b2413', '20110621 11:09:31')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (118, N'TableHourPageNotFound', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''hour'');
SELECT TOP 100 
 StatisticsObjectName AS ''{$reports_pagenotfound_hour.path_header$}'', 
 SUM(HitsCount) AS ''{$reports_pagenotfound_hour.hits_header$}'' 
 FROM Analytics_Statistics
 INNER JOIN Analytics_HourHits ON Analytics_HourHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) 
 AND (StatisticsCode=@CodeName) 
 AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate) 
 GROUP BY StatisticsObjectName ORDER BY SUM(HitsCount) DESC', 0, 66, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'b426ae01-fe3b-4e96-baed-94ac4cd431a3', '20110621 11:09:31')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (119, N'table', N'table', N'DECLARE @Sum DECIMAL;
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day'');
SELECT @Sum = 
 SUM(HitsCount)  FROM
Analytics_Statistics, Analytics_DayHits WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
 AND (StatisticsCode=@CodeName) AND (StatisticsID = HitsStatisticsID) AND 
 (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate) 
SELECT TOP 100 StatisticsObjectName AS ''{$reports_referrals_day.name_header$}'', 
SUM(HitsCount) AS ''{$reports_referrals_day.hits_header$}'',
CAST (CAST ((SUM(HitsCount) / @Sum) *100 AS DECIMAL(10,2)) AS NVARCHAR(8)) +''%'' AS ''{$reports_referrals_day.percent_header$}''
  FROM
 Analytics_Statistics, Analytics_DayHits WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
 AND (StatisticsCode=@CodeName) AND (StatisticsID = HitsStatisticsID) AND 
 (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate) 
 GROUP BY StatisticsObjectName ORDER BY SUM(HitsCount) DESC', 0, 67, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '4959a779-0ec7-46e0-8c11-1a35f10905ff', '20110621 11:09:31')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (120, N'table', N'table', N'DECLARE @Sum DECIMAL;
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''month'');
SELECT @Sum = 
 SUM(HitsCount)  FROM
Analytics_Statistics, Analytics_MonthHits WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
 AND (StatisticsCode=@CodeName) AND (StatisticsID = HitsStatisticsID) AND 
 (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate) 
SELECT TOP 100 StatisticsObjectName AS ''{$reports_referrals_month.name_header$}'', 
SUM(HitsCount) AS ''{$reports_referrals_month.hits_header$}'',
CAST (CAST ((SUM(HitsCount) / @Sum) *100 AS DECIMAL(10,2)) AS NVARCHAR(8)) +''%'' AS ''{$reports_referrals_month.percent_header$}''
  FROM
 Analytics_Statistics, Analytics_MonthHits WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
 AND (StatisticsCode=@CodeName) AND (StatisticsID = HitsStatisticsID) AND 
 (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate) 
 GROUP BY StatisticsObjectName ORDER BY SUM(HitsCount) DESC', 0, 68, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'ab4b39b9-326c-4f7f-a8a7-7c92f0ca2726', '20110621 11:09:34')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (121, N'table', N'table', N'DECLARE @Sum DECIMAL;
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''year'');
SELECT @Sum = 
 SUM(HitsCount)  FROM
Analytics_Statistics, Analytics_YearHits WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
 AND (StatisticsCode=@CodeName) AND (StatisticsID = HitsStatisticsID) AND 
 (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate) 
SELECT TOP 100 StatisticsObjectName AS ''{$reports_referrals_year.name_header$}'', 
SUM(HitsCount) AS ''{$reports_referrals_year.hits_header$}'',
CAST (CAST ((SUM(HitsCount) / @Sum) *100 AS DECIMAL(10,2)) AS NVARCHAR(8)) +''%'' AS ''{$reports_referrals_year.percent_header$}''
  FROM
 Analytics_Statistics, Analytics_YearHits WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
 AND (StatisticsCode=@CodeName) AND (StatisticsID = HitsStatisticsID) AND 
 (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate) 
 GROUP BY StatisticsObjectName ORDER BY SUM(HitsCount) DESC', 0, 69, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '5a96924b-ed83-484a-a84f-bc6d08da0181', '20110621 11:09:34')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (122, N'table', N'table', N'DECLARE @Sum DECIMAL;
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''hour'');
SELECT @Sum = 
 SUM(HitsCount)  FROM
Analytics_Statistics, Analytics_HourHits WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
 AND (StatisticsCode=@CodeName) AND (StatisticsID = HitsStatisticsID) AND 
 (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate) 
SELECT TOP 100 StatisticsObjectName AS ''{$reports_referrals_hour.name_header$}'', 
SUM(HitsCount) AS ''{$reports_referrals_hour.hits_header$}'',
CAST (CAST ((SUM(HitsCount) / @Sum) *100 AS DECIMAL(10,2)) AS NVARCHAR(8)) +''%'' AS ''{$reports_referrals_hour.percent_header$}''
  FROM
 Analytics_Statistics, Analytics_HourHits WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
 AND (StatisticsCode=@CodeName) AND (StatisticsID = HitsStatisticsID) AND 
 (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate) 
 GROUP BY StatisticsObjectName ORDER BY SUM(HitsCount) DESC', 0, 70, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '4d6192d9-9a19-448b-b70b-61526c7a3a4a', '20110621 11:09:34')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (132, N'table', N'table', N'SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day'');
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
DECLARE @myselection TABLE (
   DocumentNamePath varchar(400),
   StatisticsObjectCulture varchar(400),
   Count float   
);
DECLARE @countedAll TABLE (
   Counted float   
);
INSERT INTO @myselection
  SELECT TOP 100
  CASE WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName ELSE DocumentNamePath END,
  StatisticsObjectCulture ,
  SUM(HitsCount) AS Count
  FROM Analytics_Statistics
  INNER JOIN Analytics_DayHits ON Analytics_DayHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND View_CMS_Tree_Joined.DocumentCulture = Analytics_Statistics.StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectCulture, DocumentNamePath, StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC
  
INSERT INTO @countedAll
  SELECT
  SUM(HitsCount) AS Count
  FROM Analytics_Statistics
  INNER JOIN Analytics_DayHits ON Analytics_DayHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND View_CMS_Tree_Joined.DocumentCulture = Analytics_Statistics.StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)   
 SELECT DocumentNamePath AS ''{$general.documentname$}'',
 StatisticsObjectCulture AS   ''{$general.culture$}'',
 Count AS ''{$reports_filedownloads_Day.hits_header$}'',
 CAST (CAST((100*Count)/(SELECT Counted FROM  @countedAll) as decimal(10,2))AS NVARCHAR(10))+''%'' as ''{$reports_filedownloads_Day.hits_percent_header$}''
 FROM @myselection', 0, 86, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'd2acff50-77c9-4f7e-aca1-8170a88c86f8', '20110621 11:09:31')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (134, N'table', N'table', N'SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''month'');
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
DECLARE @myselection TABLE (
   DocumentNamePath varchar(400),
   StatisticsObjectCulture varchar(400),
   Count float   
);
DECLARE @countedAll TABLE (
   Counted float   
);
INSERT INTO @myselection
  SELECT TOP 100
  CASE WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName ELSE DocumentNamePath END,
  StatisticsObjectCulture ,
  SUM(HitsCount) AS Count
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_MonthHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND View_CMS_Tree_Joined.DocumentCulture = Analytics_Statistics.StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectCulture, DocumentNamePath, StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC
  
INSERT INTO @countedAll
  SELECT
  SUM(HitsCount) AS Count
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_MonthHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND View_CMS_Tree_Joined.DocumentCulture = Analytics_Statistics.StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)   
 SELECT DocumentNamePath AS ''{$general.documentname$}'',
 StatisticsObjectCulture AS   ''{$general.culture$}'',
 Count AS ''{$reports_filedownloads_Month.hits_header$}'',
 CAST (CAST((100*Count)/(SELECT Counted FROM  @countedAll) as decimal(10,2))AS NVARCHAR(10))+''%'' as ''{$reports_filedownloads_Month.hits_percent_header$}''
 FROM @myselection', 0, 87, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'eb1dac2c-12b2-4e9e-be41-1d7156cafbc1', '20110621 11:09:33')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (135, N'table', N'table', N'SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''year'');
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
DECLARE @myselection TABLE (
   DocumentNamePath varchar(400),
   StatisticsObjectCulture varchar(400),
   Count float   
);
DECLARE @countedAll TABLE (
   Counted float   
);
INSERT INTO @myselection
  SELECT TOP 100
  CASE WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName ELSE DocumentNamePath END,
  StatisticsObjectCulture ,
  SUM(HitsCount) AS Count
  FROM Analytics_Statistics
  INNER JOIN Analytics_YearHits ON Analytics_YearHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND View_CMS_Tree_Joined.DocumentCulture = Analytics_Statistics.StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectCulture, DocumentNamePath, StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC
  
INSERT INTO @countedAll
  SELECT
  SUM(HitsCount) AS Count
  FROM Analytics_Statistics
  INNER JOIN Analytics_YearHits ON Analytics_YearHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND View_CMS_Tree_Joined.DocumentCulture = Analytics_Statistics.StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)   
 SELECT DocumentNamePath AS ''{$general.documentname$}'',
 StatisticsObjectCulture AS   ''{$general.culture$}'',
 Count AS ''{$reports_filedownloads_Year.hits_header$}'',
 CAST (CAST((100*Count)/(SELECT Counted FROM  @countedAll) as decimal(10,2))AS NVARCHAR(10))+''%'' as ''{$reports_filedownloads_Year.hits_percent_header$}''
 FROM @myselection', 0, 88, N'<customdata><querynorecordtext></querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '563dce7e-3087-4103-bce7-94d7aa0a3b9a', '20110621 11:09:33')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (136, N'table', N'table', N'SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''hour'');
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
DECLARE @myselection TABLE (
   DocumentNamePath varchar(400),
   StatisticsObjectCulture varchar(400),
   Count float   
);
DECLARE @countedAll TABLE (
   Counted float   
);
INSERT INTO @myselection
  SELECT TOP 100
  CASE WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName ELSE DocumentNamePath END,
  StatisticsObjectCulture ,
  SUM(HitsCount) AS Count
  FROM Analytics_Statistics
  INNER JOIN Analytics_HourHits ON Analytics_HourHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND View_CMS_Tree_Joined.DocumentCulture = Analytics_Statistics.StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectCulture, DocumentNamePath, StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC
  
INSERT INTO @countedAll
  SELECT
  SUM(HitsCount) AS Count
  FROM Analytics_Statistics
  INNER JOIN Analytics_HourHits ON Analytics_HourHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND View_CMS_Tree_Joined.DocumentCulture = Analytics_Statistics.StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)   
 SELECT DocumentNamePath AS ''{$general.documentname$}'',
 StatisticsObjectCulture AS   ''{$general.culture$}'',
 Count AS ''{$reports_filedownloads_Hour.hits_header$}'',
 CAST (CAST((100*Count)/(SELECT Counted FROM  @countedAll) as decimal(10,2))AS NVARCHAR(10))+''%'' as ''{$reports_filedownloads_Hour.hits_percent_header$}''
 FROM @myselection', 0, 89, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'd32f6ee1-7c21-411c-b4b7-75ce8e36360e', '20110621 11:09:37')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (137, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day'');
DECLARE @myselection TABLE (
   DocumentNamePath varchar(400),
   ObjectID INT,
   Culture varchar(400),
   Count float,
   Average int
);
DECLARE @countedAll TABLE (
   Counted float   
);
INSERT INTO @myselection (DocumentNamePath,ObjectID,Culture,Count)
  SELECT TOP 100
  CASE WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName ELSE DocumentNamePath END,
  StatisticsObjectID,StatisticsObjectCulture , 
  SUM(HitsCount) AS Count
  FROM Analytics_Statistics
  INNER JOIN Analytics_DayHits ON Analytics_DayHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND View_CMS_Tree_Joined.DocumentCulture = Analytics_Statistics.StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectCulture, DocumentNamePath, StatisticsObjectName, StatisticsObjectID
  ORDER BY SUM(HitsCount) DESC
  
INSERT INTO @countedAll
  SELECT
  SUM(HitsCount) AS Counted
  FROM Analytics_Statistics
  INNER JOIN Analytics_DayHits ON Analytics_DayHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND View_CMS_Tree_Joined.DocumentCulture = Analytics_Statistics.StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)
 UPDATE @myselection SET Average = (SELECT SUM (HitsValue) / SUM (HitsCount) FROM Analytics_HourHits JOIN
      Analytics_Statistics ON HitsStatisticsID = StatisticsID
      WHERE HitsStartTime >= @FromDate AND HitsEndTime <= @ToDate AND ObjectID = StatisticsObjectID
        AND StatisticsCode =''avgtimeonpage'' AND StatisticsSiteID = @CMSContextCurrentSiteID
        AND culture = Analytics_Statistics.StatisticsObjectCulture
       )       
 SELECT DocumentNamePath AS ''{$reports_pageviews_Day.path_header$}'',
 culture AS   ''{$general.culture$}'',
 Count AS ''{$reports_pageviews_Day.hits_header$}'',
 CAST (CAST((100*Count)/(SELECT Counted FROM  @countedAll) as decimal(10,2))AS NVARCHAR(10))+''%'' AS ''{$reports_pageviews_Day.hits_percent_header$}'',
 ISNULL(CONVERT(varchar, DATEADD(s, average, 0), 108),''00:00:00'') AS ''{$reports_pageviews.average$}''
 FROM @myselection', 0, 90, N'<customdata><pagesize>10</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'd088e963-ff55-404f-92c3-19f0976e5ca5', '20110621 11:09:31')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (138, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''month'');
DECLARE @myselection TABLE (
   DocumentNamePath varchar(400),
   ObjectID INT,
   Culture varchar(400),
   Count float,
   Average int
);
DECLARE @countedAll TABLE (
   Counted float   
);
INSERT INTO @myselection (DocumentNamePath,ObjectID,Culture,Count)
  SELECT TOP 100
  CASE WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName ELSE DocumentNamePath END,
  StatisticsObjectID,StatisticsObjectCulture , 
  SUM(HitsCount) AS Count
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_MonthHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND View_CMS_Tree_Joined.DocumentCulture = Analytics_Statistics.StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectCulture, DocumentNamePath, StatisticsObjectName, StatisticsObjectID
  ORDER BY SUM(HitsCount) DESC
  
INSERT INTO @countedAll
  SELECT
  SUM(HitsCount) AS Counted
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_MonthHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND View_CMS_Tree_Joined.DocumentCulture = Analytics_Statistics.StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)
 UPDATE @myselection SET Average = (SELECT SUM (HitsValue) / SUM (HitsCount) FROM Analytics_HourHits JOIN
      Analytics_Statistics ON HitsStatisticsID = StatisticsID
      WHERE HitsStartTime >= @FromDate AND HitsEndTime <= @ToDate AND ObjectID = StatisticsObjectID
        AND StatisticsCode =''avgtimeonpage'' AND StatisticsSiteID = @CMSContextCurrentSiteID
        AND culture = Analytics_Statistics.StatisticsObjectCulture
       )       
 SELECT DocumentNamePath AS ''{$reports_pageviews_Month.path_header$}'',
 culture AS   ''{$general.culture$}'',
 Count AS ''{$reports_pageviews_Month.hits_header$}'',
CAST (CAST((100*Count)/(SELECT Counted FROM  @countedAll) as decimal(10,2))AS NVARCHAR(10))+''%'' AS ''{$reports_pageviews_Month.hits_percent_header$}'',
 ISNULL(CONVERT(varchar, DATEADD(s, average, 0), 108),''00:00:00'') AS ''{$reports_pageviews.average$}''
 FROM @myselection', 0, 91, N'<customdata><pagesize>10</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '555720dc-dfd8-42f7-a93b-90b57f61bcd4', '20110621 11:09:31')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (139, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''year'');
DECLARE @myselection TABLE (
   DocumentNamePath varchar(400),
   ObjectID INT,
   Culture varchar(400),
   Count float,
   Average int
);
DECLARE @countedAll TABLE (
   Counted float   
);
INSERT INTO @myselection (DocumentNamePath,ObjectID,Culture,Count)
  SELECT TOP 100
  CASE WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName ELSE DocumentNamePath END,
  StatisticsObjectID,StatisticsObjectCulture , 
  SUM(HitsCount) AS Count
  FROM Analytics_Statistics
  INNER JOIN Analytics_YearHits ON Analytics_YearHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND View_CMS_Tree_Joined.DocumentCulture = Analytics_Statistics.StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectCulture, DocumentNamePath, StatisticsObjectName, StatisticsObjectID
  ORDER BY SUM(HitsCount) DESC
  
INSERT INTO @countedAll
  SELECT
  SUM(HitsCount) AS Counted
  FROM Analytics_Statistics
  INNER JOIN Analytics_YearHits ON Analytics_YearHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND View_CMS_Tree_Joined.DocumentCulture = Analytics_Statistics.StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)
 UPDATE @myselection SET Average = (SELECT SUM (HitsValue) / SUM (HitsCount) FROM Analytics_HourHits JOIN
      Analytics_Statistics ON HitsStatisticsID = StatisticsID
      WHERE HitsStartTime >= @FromDate AND HitsEndTime <= @ToDate AND ObjectID = StatisticsObjectID
        AND StatisticsCode =''avgtimeonpage'' AND StatisticsSiteID = @CMSContextCurrentSiteID
        AND culture = Analytics_Statistics.StatisticsObjectCulture
       )       
 SELECT DocumentNamePath AS ''{$reports_pageviews_Year.path_header$}'',
 culture AS   ''{$general.culture$}'',
 Count AS ''{$reports_pageviews_Year.hits_header$}'',
  CAST (CAST((100*Count)/(SELECT Counted FROM  @countedAll) as decimal(10,2))AS NVARCHAR(10))+''%'' AS ''{$reports_pageviews_Year.hits_percent_header$}'',
 ISNULL(CONVERT(varchar, DATEADD(s, average, 0), 108),''00:00:00'') AS ''{$reports_pageviews.average$}''
 FROM @myselection', 0, 92, N'<customdata><querynorecordtext></querynorecordtext><exportenabled>true</exportenabled><pagesize>10</pagesize><enablepaging>False</enablepaging><skinid>ReportGridAnalytics</skinid><pagemode>1</pagemode></customdata>', '44dc48c4-090f-4fb6-bec3-4814bdbb2a3d', '20110621 11:09:33')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (140, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''hour'');
DECLARE @myselection TABLE (
   DocumentNamePath varchar(400),
   ObjectID INT,
   Culture varchar(400),
   Count float,
   Average int
);
DECLARE @countedAll TABLE (
   Counted float   
);
INSERT INTO @myselection (DocumentNamePath,ObjectID,Culture,Count)
  SELECT TOP 100
  CASE WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName ELSE DocumentNamePath END,
  StatisticsObjectID,StatisticsObjectCulture , 
  SUM(HitsCount) AS Count
  FROM Analytics_Statistics
  INNER JOIN Analytics_HourHits ON Analytics_HourHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND View_CMS_Tree_Joined.DocumentCulture = Analytics_Statistics.StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectCulture, DocumentNamePath, StatisticsObjectName, StatisticsObjectID
  ORDER BY SUM(HitsCount) DESC
  
INSERT INTO @countedAll
  SELECT
  SUM(HitsCount) AS Counted
  FROM Analytics_Statistics
  INNER JOIN Analytics_HourHits ON Analytics_HourHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND View_CMS_Tree_Joined.DocumentCulture = Analytics_Statistics.StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)
 UPDATE @myselection SET Average = (SELECT SUM (HitsValue) / SUM (HitsCount) FROM Analytics_HourHits JOIN
      Analytics_Statistics ON HitsStatisticsID = StatisticsID
      WHERE HitsStartTime >= @FromDate AND HitsEndTime <= @ToDate AND ObjectID = StatisticsObjectID
        AND StatisticsCode =''avgtimeonpage'' AND StatisticsSiteID = @CMSContextCurrentSiteID
        AND culture = Analytics_Statistics.StatisticsObjectCulture
       )       
 SELECT DocumentNamePath AS ''{$reports_pageviews_Hour.path_header$}'',
 culture AS   ''{$general.culture$}'',
 Count AS ''{$reports_pageviews_Hour.hits_header$}'',
 CAST (CAST((100*Count)/(SELECT Counted FROM  @countedAll) as decimal(10,2))AS NVARCHAR(10))+''%'' AS ''{$reports_pageviews_Hour.hits_percent_header$}'',
 ISNULL(CONVERT(varchar, DATEADD(s, average, 0), 108),''00:00:00'') AS ''{$reports_pageviews.average$}''
 FROM @myselection', 0, 93, N'<customdata><pagesize>10</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '9ad190f2-6d1f-4419-92b2-261f14d0d468', '20110621 11:09:31')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (141, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day'');
SELECT TOP 100 CampaignDisplayName AS ''{$reports_campaign.name_header$}'', SUM(HitsCount) AS ''{$reports_campaign.hits_header$}'' FROM
Analytics_DayHits, Analytics_Statistics
INNER JOIN Analytics_Campaign ON StatisticsObjectName = CampaignName AND CampaignSiteID = @CMSContextCurrentSiteID
WHERE (@CampaignName ='''' OR @CampaignName = CampaignName) AND
(StatisticsSiteID = @CMSContextCurrentSiteID) AND (StatisticsCode=@CodeName) AND (StatisticsID = HitsStatisticsID) AND
(HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate) GROUP BY CampaignDisplayName ORDER BY SUM(HitsCount) DESC', 0, 94, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '2cd0dfae-8282-4a07-8b7b-4c85056537cb', '20110621 11:09:34')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (142, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''month'');
SELECT TOP 100 CampaignDisplayName AS ''{$reports_campaign.name_header$}'', SUM(HitsCount) AS ''{$reports_campaign.hits_header$}'' FROM
Analytics_MonthHits, Analytics_Statistics
INNER JOIN Analytics_Campaign ON StatisticsObjectName = CampaignName AND CampaignSiteID = @CMSContextCurrentSiteID
WHERE (@CampaignName ='''' OR @CampaignName = CampaignName) AND
(StatisticsSiteID = @CMSContextCurrentSiteID) AND (StatisticsCode=@CodeName) AND (StatisticsID = HitsStatisticsID) AND
(HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate) GROUP BY CampaignDisplayName ORDER BY SUM(HitsCount) DESC', 0, 95, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '5667ec53-453a-45c7-ad69-a76ea53b2877', '20110621 11:09:34')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (143, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''year'');
SELECT TOP 100 CampaignDisplayName AS ''{$reports_campaign.name_header$}'', SUM(HitsCount) AS ''{$reports_campaign.hits_header$}'' FROM
Analytics_YearHits, Analytics_Statistics
INNER JOIN Analytics_Campaign ON StatisticsObjectName = CampaignName AND CampaignSiteID = @CMSContextCurrentSiteID
WHERE (@CampaignName ='''' OR @CampaignName = CampaignName) AND
(StatisticsSiteID = @CMSContextCurrentSiteID) AND (StatisticsCode=@CodeName) AND (StatisticsID = HitsStatisticsID) AND
(HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate) GROUP BY CampaignDisplayName ORDER BY SUM(HitsCount) DESC', 0, 96, N'<customdata><querynorecordtext></querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '14bab9a1-14e9-4c9f-a2cf-4d343a4118d9', '20110621 11:09:34')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (144, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''hour'');
SELECT TOP 100 CampaignDisplayName AS ''{$reports_campaign.name_header$}'', SUM(HitsCount) AS ''{$reports_campaign.hits_header$}'' FROM
Analytics_HourHits, Analytics_Statistics
INNER JOIN Analytics_Campaign ON StatisticsObjectName = CampaignName AND CampaignSiteID = @CMSContextCurrentSiteID
WHERE (@CampaignName ='''' OR @CampaignName = CampaignName) AND
(StatisticsSiteID = @CMSContextCurrentSiteID) AND (StatisticsCode=@CodeName) AND (StatisticsID = HitsStatisticsID) AND
(HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate) GROUP BY CampaignDisplayName ORDER BY SUM(HitsCount) DESC', 0, 97, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '1bb584c6-62f9-47f1-b7fd-3f94b9fb2d84', '20110621 11:09:31')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (145, N'TableDayConversion', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day'');
SELECT TOP 100 ConversionDisplayName AS ''{$reports_conversion.name_header$}'', SUM(HitsCount) AS
''{$reports_conversion.hits_header$}'',SUM(HitsValue) AS ''{$reports_conversion.value_header$}'' FROM
Analytics_Statistics
JOIN Analytics_DayHits  ON HitsStatisticsID = StatisticsID
JOIN Analytics_Conversion ON ConversionName  = StatisticsObjectName AND ConversionSiteID = @CMSContextCurrentSiteID
WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND
(StatisticsCode=@CodeName) AND (StatisticsID = HitsStatisticsID) AND (HitsStartTime >= @FromDate) AND
(HitsEndTime <= @ToDate) GROUP BY ConversionDisplayName ORDER BY SUM(HitsCount) DESC', 0, 98, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><pagesize>10</pagesize><enablepaging>False</enablepaging><skinid>ReportGridAnalytics</skinid><pagemode>1</pagemode></customdata>', 'dba28818-8a32-4029-9449-1134db4d52c3', '20110727 12:10:50')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (146, N'TableMonthConversion', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''month'');
SELECT TOP 100 ConversionDisplayName AS ''{$reports_conversion.name_header$}'', SUM(HitsCount) AS
''{$reports_conversion.hits_header$}'',SUM(HitsValue) AS ''{$reports_conversion.value_header$}'' FROM
Analytics_Statistics
JOIN Analytics_MonthHits  ON HitsStatisticsID = StatisticsID
JOIN Analytics_Conversion ON ConversionName  = StatisticsObjectName AND ConversionSiteID = @CMSContextCurrentSiteID
WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND
(StatisticsCode=@CodeName) AND (StatisticsID = HitsStatisticsID) AND (HitsStartTime >= @FromDate) AND
(HitsEndTime <= @ToDate) GROUP BY ConversionDisplayName ORDER BY SUM(HitsCount) DESC', 0, 99, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><pagesize>10</pagesize><enablepaging>False</enablepaging><skinid>ReportGridAnalytics</skinid><pagemode>1</pagemode></customdata>', 'c350451e-e82d-4f51-89a7-1c7e32a88faa', '20110727 12:10:23')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (147, N'TableYearConversion', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''year'');
SELECT TOP 100 ConversionDisplayName AS ''{$reports_conversion.name_header$}'', SUM(HitsCount) AS
''{$reports_conversion.hits_header$}'',SUM(HitsValue) AS ''{$reports_conversion.value_header$}''  FROM
Analytics_Statistics 
JOIN Analytics_YearHits  ON HitsStatisticsID = StatisticsID
JOIN Analytics_Conversion ON ConversionName  = StatisticsObjectName AND ConversionSiteID = @CMSContextCurrentSiteID
WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND
(StatisticsCode=@CodeName) AND (StatisticsID = HitsStatisticsID) AND (HitsStartTime >= @FromDate) AND
(HitsEndTime <= @ToDate) GROUP BY ConversionDisplayName ORDER BY SUM(HitsCount) DESC', 0, 100, N'<customdata><querynorecordtext></querynorecordtext><exportenabled>True</exportenabled><pagesize>10</pagesize><enablepaging>False</enablepaging><skinid>ReportGridAnalytics</skinid><pagemode>1</pagemode></customdata>', '9394ce04-7501-4888-ae83-4835bedb43f5', '20110727 12:09:35')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (148, N'TableHourConversion', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''hour'');
SELECT TOP 100 ConversionDisplayName AS ''{$reports_conversion.name_header$}'', SUM(HitsCount) AS
''{$reports_conversion.hits_header$}'',SUM(HitsValue) AS ''{$reports_conversion.value_header$}'' FROM
Analytics_Statistics
JOIN Analytics_HourHits  ON HitsStatisticsID = StatisticsID
JOIN Analytics_Conversion ON ConversionName  = StatisticsObjectName AND ConversionSiteID = @CMSContextCurrentSiteID
WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND
(StatisticsCode=@CodeName) AND (StatisticsID = HitsStatisticsID) AND (HitsStartTime >= @FromDate) AND
(HitsEndTime <= @ToDate) GROUP BY ConversionDisplayName ORDER BY SUM(HitsCount) DESC', 0, 101, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><pagesize>10</pagesize><enablepaging>False</enablepaging><skinid>ReportGridAnalytics</skinid><pagemode>1</pagemode></customdata>', '75df0d0f-f86e-42bd-af2c-88024f27bd3a', '20110727 12:10:36')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (152, N'TableEcommInventory', N'TableEcommInventory', N'SELECT
COM_SKU.SKUName + ISNULL('' ('' + COM_OptionCategory.CategoryDisplayName + '')'', '''') AS [{$ecommerce.report_skuname$}],
COM_SKU.SKUNumber AS [{$ecommerce.report_productnumber$}],
COM_Department.DepartmentDisplayName AS [{$ecommerce.report_departmentname$}], 
ISNULL(CAST(COM_SKU.SKUAvailableItems AS nvarchar(16)), ''-'') AS [{$ecommerce.report_availableitems$}]
FROM COM_SKU
LEFT JOIN COM_Department ON COM_Department.DepartmentID = COM_SKU.SKUDepartmentID
LEFT JOIN COM_OptionCategory ON COM_OptionCategory.CategoryID = COM_SKU.SKUOptionCategoryID
WHERE
(ISNULL(SKUAvailableItems, 0) < @Limit)
AND (ISNULL(SKUSiteID, 0) = @SiteID)
ORDER BY SKUName ASC', 0, 105, N'<customdata><pagesize>10</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No invetory items found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '09730073-aed3-4e26-991e-11e91b2c8df8', '20111003 07:36:41')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (156, N'TableEcommTop100CustByVolOfSales', N'TableEcommTop100CustByVolOfSales', N'SELECT TOP 100
CustComp as ''{$ecommerce.report_customer_company$}'',
CustFirst as ''{$ecommerce.report_customer_firstname$}'',
CustLast as ''{$ecommerce.report_customer_lastname$}'',
CustEmail as ''{$ecommerce.report_customer$}'',
Income as ''{$ecommerce.report_income$}'' FROM (
SELECT CustomerEmail as CustEmail, CustomerCompany as CustComp, CustomerFirstName as CustFirst, CustomerLastName as CustLast, 
CAST(SUM(ISNULL(OrderTotalPriceInMainCurrency,0)) AS decimal(38,2)) as Income FROM COM_Order, COM_Customer
WHERE CustomerID = OrderCustomerID AND
OrderSiteID = @CMSContextCurrentSiteID
GROUP BY CustomerEmail, CustomerCompany, CustomerFirstName, CustomerLastName
) as tab1 ORDER BY Income DESC', 0, 109, N'<customdata><pagesize>10</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No customers found</querynorecordtext><exportenabled>true</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '6fe00b31-2adf-4d5e-8bf3-8820563ecfca', '20110621 11:09:30')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (157, N'TableDayBrowserType', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day'');
SELECT TOP 100 
 StatisticsObjectName AS ''{$reports_browsertype.name_header$}'', 
 SUM(HitsCount) AS ''{$reports_browsertype.hits_header$}'', 
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount) 
  FROM Analytics_Statistics
  INNER JOIN Analytics_DayHits ON Analytics_Statistics.StatisticsID = Analytics_DayHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID) 
  AND (StatisticsCode=@CodeName) 
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) 
 AS VARCHAR)+''%'' AS ''{$reports_browsertype.percent_header$}'' 
 FROM Analytics_Statistics
 INNER JOIN Analytics_DayHits ON Analytics_Statistics.StatisticsID = Analytics_DayHits.HitsStatisticsID
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) 
 AND (StatisticsCode=@CodeName) 
 AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate) 
 GROUP BY StatisticsObjectName ORDER BY SUM(HitsCount) DESC', 0, 110, N'<customdata><querynorecordtext></querynorecordtext><exportenabled>True</exportenabled><pagesize>10</pagesize><enablepaging>False</enablepaging><skinid>ReportGridAnalytics</skinid><pagemode>1</pagemode></customdata>', '2aaf1e80-15a2-44c6-b0c9-d9b1a09efc24', '20110726 09:48:29')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (158, N'TableHourBrowserType', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''hour'');
SELECT TOP 100 
 StatisticsObjectName AS ''{$reports_browsertype.name_header$}'', 
 SUM(HitsCount) AS ''{$reports_browsertype.hits_header$}'', 
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount) 
  FROM Analytics_Statistics
  INNER JOIN Analytics_HourHits ON Analytics_Statistics.StatisticsID = Analytics_HourHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID) 
  AND (StatisticsCode=@CodeName) 
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) AS VARCHAR)+''%'' AS ''{$reports_browsertype.percent_header$}'' 
  FROM Analytics_Statistics
  INNER JOIN Analytics_HourHits ON Analytics_Statistics.StatisticsID = Analytics_HourHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) 
  AND (StatisticsCode=@CodeName) 
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate) 
  GROUP BY StatisticsObjectName 
  ORDER BY SUM(HitsCount) DESC', 0, 111, N'<customdata><querynorecordtext></querynorecordtext><exportenabled>True</exportenabled><pagesize>10</pagesize><enablepaging>False</enablepaging><skinid>ReportGridAnalytics</skinid><pagemode>1</pagemode></customdata>', '4873edaa-a541-4055-bfb9-99f47fa5a916', '20110726 09:48:48')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (159, N'TableMonthBrowserType', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''month'');
SELECT TOP 100 
 StatisticsObjectName AS ''{$reports_browsertype.name_header$}'',
 SUM(HitsCount) AS ''{$reports_browsertype.hits_header$}'', 
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount) 
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_Statistics.StatisticsID = Analytics_MonthHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID) 
  AND (StatisticsCode=@CodeName) 
  AND (HitsStartTime >= @FromDate) 
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) AS VARCHAR)+''%'' AS ''{$reports_browsertype.percent_header$}'' 
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_Statistics.StatisticsID = Analytics_MonthHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) 
  AND (StatisticsCode=@CodeName) 
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate) 
  GROUP BY StatisticsObjectName 
  ORDER BY SUM(HitsCount) DESC', 0, 112, N'<customdata><pagesize>10</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext></querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', '6ecb01ea-2939-4a26-8d14-922e1c15f60b', '20110726 09:49:06')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (160, N'TableYearBrowserType', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''year'');
SELECT TOP 100
 StatisticsObjectName AS ''{$reports_browsertype.name_header$}'',
 SUM(HitsCount) AS ''{$reports_browsertype.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_YearHits ON Analytics_Statistics.StatisticsID = Analytics_YearHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) AS VARCHAR)+''%'' AS ''{$reports_browsertype.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_YearHits ON Analytics_Statistics.StatisticsID = Analytics_YearHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 113, N'<customdata><querynorecordtext></querynorecordtext><exportenabled>True</exportenabled><pagesize>10</pagesize><enablepaging>False</enablepaging><skinid>ReportGridAnalytics</skinid><pagemode>1</pagemode></customdata>', '98141f5c-a3dc-4137-b1c3-5ff017d53bda', '20110726 09:49:35')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (161, N'TableDayCountries', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day'');
SELECT TOP 100 
 StatisticsObjectName AS ''{$reports_countries.name_header$}'', 
 SUM(HitsCount) AS ''{$reports_countries.hits_header$}'' 
 FROM Analytics_Statistics
 INNER JOIN Analytics_DayHits ON Analytics_Statistics.StatisticsID = Analytics_DayHits.HitsStatisticsID
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) 
 AND (StatisticsCode=@CodeName) 
 AND (HitsStartTime >= @FromDate) 
 AND (HitsEndTime <= @ToDate) 
 GROUP BY StatisticsObjectName 
 ORDER BY SUM(HitsCount) DESC', 0, 126, N'<customdata><querynorecordtext></querynorecordtext><exportenabled>True</exportenabled><pagesize>10</pagesize><enablepaging>False</enablepaging><skinid>ReportGridAnalytics</skinid><pagemode>1</pagemode></customdata>', '0095a983-301d-4d44-b620-2bf501574349', '20110818 16:16:35')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (162, N'TableHourCountries', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''hour'');
SELECT TOP 100 
 StatisticsObjectName AS ''{$reports_countries.name_header$}'', 
 SUM(HitsCount) AS ''{$reports_countries.hits_header$}'' 
 FROM Analytics_Statistics
 INNER JOIN Analytics_HourHits ON Analytics_Statistics.StatisticsID = Analytics_HourHits.HitsStatisticsID
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) 
 AND (StatisticsCode=@CodeName) 
 AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate) 
 GROUP BY StatisticsObjectName ORDER BY SUM(HitsCount) DESC', 0, 128, N'<customdata><querynorecordtext></querynorecordtext><exportenabled>True</exportenabled><pagesize>10</pagesize><enablepaging>False</enablepaging><skinid>ReportGridAnalytics</skinid><pagemode>1</pagemode></customdata>', '181ec0f9-a70c-4048-a0df-f39d122fdd62', '20110818 16:17:26')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (163, N'TableMonthCountries', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''month'');
SELECT TOP 100 
 StatisticsObjectName AS ''{$reports_countries.name_header$}'', 
 SUM(HitsCount) AS ''{$reports_countries.hits_header$}'' 
 FROM Analytics_Statistics
 INNER JOIN Analytics_MonthHits ON Analytics_Statistics.StatisticsID = Analytics_MonthHits.HitsStatisticsID
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) 
 AND (StatisticsCode=@CodeName) 
 AND (HitsStartTime >= @FromDate) 
 AND (HitsEndTime <= @ToDate) 
 GROUP BY StatisticsObjectName 
 ORDER BY SUM(HitsCount) DESC', 0, 129, N'<customdata><querynorecordtext></querynorecordtext><exportenabled>True</exportenabled><pagesize>10</pagesize><enablepaging>False</enablepaging><skinid>ReportGridAnalytics</skinid><pagemode>1</pagemode></customdata>', 'e666ff48-ffdc-40f1-922b-356c177ae49e', '20110818 16:17:56')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (164, N'TableYearCountries', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''week'');
SELECT TOP 100 StatisticsObjectName AS ''{$reports_countries.name_header$}'', SUM(HitsCount) AS ''{$reports_countries.hits_header$}'' FROM
Analytics_Statistics, Analytics_WeekHits WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND (StatisticsCode=@CodeName) AND (StatisticsID = HitsStatisticsID) AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate) GROUP BY StatisticsObjectName ORDER BY SUM(HitsCount) DESC', 0, 130, N'<customdata><querynorecordtext></querynorecordtext><exportenabled>True</exportenabled><pagesize>10</pagesize><enablepaging>False</enablepaging><skinid>ReportGridAnalytics</skinid><pagemode>1</pagemode></customdata>', 'cbfb887e-752e-448a-a0bf-77cc756c86fd', '20110818 16:18:16')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (174, N'TableMonthRegisteredUsers', N'Table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''month'');
SELECT TOP 100 
 StatisticsObjectID AS ''{$reports_registeredusers.UserID_header$}'', 
 StatisticsObjectName AS ''{$reports_registeredusers.UserName_header$}'' 
 FROM Analytics_Statistics
 INNER JOIN Analytics_MonthHits ON Analytics_MonthHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) 
 AND (StatisticsCode=@CodeName) 
 AND (HitsStartTime >= @FromDate) 
 AND (HitsEndTime <= @ToDate)', 0, 146, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '9aced55d-b9a7-4f7c-a1a3-f51fa6a8bdce', '20110621 11:09:33')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (176, N'TableYearRegisteredUsers', N'Table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''year'');
SELECT TOP 100
 StatisticsObjectID AS ''{$reports_registeredusers.UserID_header$}'',
 StatisticsObjectName AS ''{$reports_registeredusers.UserName_header$}''
 FROM Analytics_Statistics
 INNER JOIN Analytics_YearHits ON Analytics_YearHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
 AND (StatisticsCode=@CodeName)
 AND (HitsStartTime >= @FromDate)
 AND (HitsEndTime <= @ToDate)', 0, 149, N'<customdata><querynorecordtext></querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'ab0b845a-f3ff-45fb-84dd-004c761fd1e5', '20110621 11:09:33')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (200, N'InventoryTable', N'InventoryTable', N'SELECT NodeAliasPath AS ''Alias Path'', 
	DocumentCulture AS ''Language'', 
	DocumentName AS ''Document name'', 
	DocumentModifiedWhen AS ''Last modified'', 
	UserName AS ''Last modified by'',
	StepDisplayName AS ''Workflow step'',
	DocumentPublishFrom AS ''Publish from'',
	DocumentPublishTo AS ''Publish to''
FROM View_CMS_Tree_Joined
LEFT JOIN CMS_User ON DocumentModifiedByUserID = UserID
LEFT JOIN CMS_WorkFlowStep ON DocumentWorkflowStepID = StepID
WHERE (@OnlyPublished = 0 OR Published = @OnlyPublished) 
AND (NodeSiteID = @CMSContextCurrentSiteID)
AND (@ModifiedFrom IS NULL OR DocumentModifiedWhen >= @ModifiedFrom)
AND (@ModifiedTo IS NULL OR DocumentModifiedWhen < @ModifiedTo) 
AND (NodeAliasPath LIKE @path)
AND (@Language IS NULL OR @Language = ''-1'' OR DocumentCulture = @Language)
AND (@name IS NULL OR DocumentName LIKE ''%''+@name+''%'')
ORDER BY NodeAliasPath', 0, 186, N'<customdata><pagesize>10</pagesize><skinid></skinid><querynorecordtext>No inventory data found</querynorecordtext><exportenabled>true</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '73ad09f4-db03-47ae-a09d-9b5ad4c76e11', '20110621 11:09:32')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (201, N'table', N'table', N'DECLARE @PaveViews TABLE
(
  DocumentNamePath NVARCHAR(500),
  Pageviews INT,
  Percents DECIMAL(10,2)  
)
DECLARE @Sum DECIMAL;
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''hour'');
SELECT @Sum =   
  SUM(HitsCount) 
  FROM Analytics_Statistics
  INNER JOIN Analytics_HourHits ON Analytics_HourHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID  AND StatisticsObjectCulture = DocumentCulture 
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
 AND (HitsEndTime <= @ToDate)
INSERT INTO @PaveViews (DocumentNamePath,pageViews,percents)
SELECT TOP 100
 CASE
  WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName
  ELSE DocumentNamePath
 END , SUM(HitsCount), (SUM(HitsCount)/@Sum)*100
 FROM Analytics_Statistics
 INNER JOIN Analytics_HourHits ON Analytics_HourHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
 LEFT JOIN View_CMS_Tree_Joined ON Analytics_Statistics.StatisticsObjectID = View_CMS_Tree_Joined.NodeID  AND StatisticsObjectCulture = DocumentCulture 
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
 AND (StatisticsCode=@CodeName)
 GROUP BY DocumentNamePath, StatisticsObjectName
 ORDER BY SUM(HitsCount) DESC
 
  
 SELECT DocumentNamePath AS ''{$reports_aggviews_Hour.path_header$}'',pageviews AS  ''{$reports_aggviews_Hour.hits_header$}'',
  CAST (Percents AS NVARCHAR(10)) +''%'' AS ''{$reports_aggviews_Hour.hits_percent_header$}''
   FROM @PaveViews ORDER BY PageViews DESC', 0, 187, N'<customdata><pagesize>10</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '5d1ae09c-51cd-4ecd-a49e-92d0cf105ea1', '20110920 12:21:24')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (202, N'table', N'table', N'DECLARE @PaveViews TABLE
(
  DocumentNamePath NVARCHAR(500),
  Pageviews INT,
  Percents DECIMAL(10,2)  
)
DECLARE @Sum DECIMAL;
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day'');
SELECT @Sum =   
  SUM(HitsCount) 
  FROM Analytics_Statistics
  INNER JOIN Analytics_DayHits ON Analytics_DayHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID  AND StatisticsObjectCulture = DocumentCulture 
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
 AND (HitsEndTime <= @ToDate)
INSERT INTO @PaveViews (DocumentNamePath,pageViews,percents)
SELECT TOP 100
 CASE
  WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName
  ELSE DocumentNamePath
 END , SUM(HitsCount), (SUM(HitsCount)/@Sum)*100
 FROM Analytics_Statistics
 INNER JOIN Analytics_DayHits ON Analytics_DayHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
 LEFT JOIN View_CMS_Tree_Joined ON Analytics_Statistics.StatisticsObjectID = View_CMS_Tree_Joined.NodeID  AND StatisticsObjectCulture = DocumentCulture 
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
 AND (StatisticsCode=@CodeName)
 GROUP BY DocumentNamePath, StatisticsObjectName
 ORDER BY SUM(HitsCount) DESC
 
  
 SELECT DocumentNamePath AS ''{$reports_aggviews_Day.path_header$}'',pageviews AS  ''{$reports_aggviews_Day.hits_header$}'',
  CAST (Percents AS NVARCHAR(10)) +''%'' AS ''{$reports_aggviews_Day.hits_percent_header$}''
   FROM @PaveViews ORDER BY PageViews DESC', 0, 188, N'<customdata><pagesize>10</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'd1e18337-1946-47e2-9cc4-ff7e2178a3e5', '20110920 12:21:23')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (203, N'table', N'table', N'DECLARE @PaveViews TABLE
(
  DocumentNamePath NVARCHAR(500),
  Pageviews INT,
  Percents DECIMAL(10,2)  
)
DECLARE @Sum DECIMAL;
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''month'');
SELECT @Sum =   
  SUM(HitsCount) 
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_MonthHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID  AND StatisticsObjectCulture = DocumentCulture 
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
 AND (HitsEndTime <= @ToDate)
INSERT INTO @PaveViews (DocumentNamePath,pageViews,percents)
SELECT TOP 100
 CASE
  WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName
  ELSE DocumentNamePath
 END , SUM(HitsCount), (SUM(HitsCount)/@Sum)*100
 FROM Analytics_Statistics
 INNER JOIN Analytics_MonthHits ON Analytics_MonthHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
 LEFT JOIN View_CMS_Tree_Joined ON Analytics_Statistics.StatisticsObjectID = View_CMS_Tree_Joined.NodeID  AND StatisticsObjectCulture = DocumentCulture 
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
 AND (StatisticsCode=@CodeName)
 GROUP BY DocumentNamePath, StatisticsObjectName
 ORDER BY SUM(HitsCount) DESC
 
  
 SELECT DocumentNamePath AS ''{$reports_aggviews_Month.path_header$}'',pageviews AS  ''{$reports_aggviews_Month.hits_header$}'',
  CAST (Percents AS NVARCHAR(10)) +''%'' AS ''{$reports_aggviews_Month.hits_percent_header$}''
   FROM @PaveViews ORDER BY PageViews DESC', 0, 189, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><pagesize>10</pagesize><enablepaging>False</enablepaging><skinid>ReportGridAnalytics</skinid><pagemode>1</pagemode></customdata>', 'db2d3ab2-ef30-430a-9d17-9fe01abb383c', '20110920 12:21:23')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (204, N'table', N'table', N'DECLARE @PaveViews TABLE
(
  DocumentNamePath NVARCHAR(500),
  Pageviews INT,
  Percents DECIMAL(10,2)  
)
DECLARE @Sum DECIMAL;
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''year'');
SELECT @Sum =   
  SUM(HitsCount) 
  FROM Analytics_Statistics
  INNER JOIN Analytics_YearHits ON Analytics_YearHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID  AND StatisticsObjectCulture = DocumentCulture 
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
 AND (HitsEndTime <= @ToDate)
INSERT INTO @PaveViews (DocumentNamePath,pageViews,percents)
SELECT TOP 100
 CASE
  WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName
  ELSE DocumentNamePath
 END , SUM(HitsCount), (SUM(HitsCount)/@Sum)*100
 FROM Analytics_Statistics
 INNER JOIN Analytics_YearHits ON Analytics_YearHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
 LEFT JOIN View_CMS_Tree_Joined ON Analytics_Statistics.StatisticsObjectID = View_CMS_Tree_Joined.NodeID  AND StatisticsObjectCulture = DocumentCulture 
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
 AND (StatisticsCode=@CodeName)
 GROUP BY DocumentNamePath, StatisticsObjectName
 ORDER BY SUM(HitsCount) DESC
 
  
 SELECT DocumentNamePath AS ''{$reports_aggviews_Year.path_header$}'',pageviews AS  ''{$reports_aggviews_Year.hits_header$}'',
  CAST (Percents AS NVARCHAR(10)) +''%'' AS ''{$reports_aggviews_Year.hits_percent_header$}''
   FROM @PaveViews ORDER BY PageViews DESC', 0, 190, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><pagesize>10</pagesize><enablepaging>False</enablepaging><skinid>ReportGridAnalytics</skinid><pagemode>1</pagemode></customdata>', '464f3553-f5e5-48a9-a1e6-8d88a1427603', '20110920 12:21:23')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (205, N'TableMonthAggViewsCulture', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''month'');
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
DECLARE @myselection TABLE (
   DocumentNamePath varchar(400),
   StatisticsObjectCulture varchar(400),
   Count float   
);
DECLARE @countedAll TABLE (
   Counted float   
);
INSERT INTO @myselection 
  SELECT TOP 100 
  CASE WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName ELSE DocumentNamePath END, 
  StatisticsObjectCulture , 
  SUM(HitsCount) AS Count 
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_MonthHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND View_CMS_Tree_Joined.DocumentCulture = Analytics_Statistics.StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) 
  AND (StatisticsCode=@CodeName) 
  AND (HitsStartTime >= @FromDate) 
  AND (HitsEndTime <= @ToDate) 
  GROUP BY StatisticsObjectCulture, DocumentNamePath, StatisticsObjectName 
  ORDER BY SUM(HitsCount) DESC
  
INSERT INTO @countedAll 
  SELECT
  SUM(HitsCount) AS Count 
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_MonthHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND View_CMS_Tree_Joined.DocumentCulture = Analytics_Statistics.StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) 
  AND (StatisticsCode=@CodeName) 
  AND (HitsStartTime >= @FromDate) 
  AND (HitsEndTime <= @ToDate)   
 SELECT DocumentNamePath AS ''{$reports_aggviews_month.path_header$}'', 
 StatisticsObjectCulture AS   ''{$general.culture$}'', 
 Count AS ''{$reports_aggviews_month.hits_header$}'', 
 CAST (CAST((100*Count)/(SELECT Counted FROM  @countedAll) as decimal(10,2))AS NVARCHAR(10))+''%'' as ''{$reports_aggviews_month.hits_percent_header$}'' 
 FROM @myselection', 0, 191, N'<customdata><pagesize>10</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '70c1bd35-c313-46d2-8ebc-e25d00f5eeac', '20110621 11:09:34')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (206, N'TableDayAggViewsCulture', N'table', N'SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day'');
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
DECLARE @myselection TABLE (
   DocumentNamePath varchar(400),
   StatisticsObjectCulture varchar(400),
   Count float   
);
DECLARE @countedAll TABLE (
   Counted float   
);
INSERT INTO @myselection 
  SELECT TOP 100 
  CASE WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName ELSE DocumentNamePath END AS ''{$reports_pageviews_day.path_header$}'',  
  StatisticsObjectCulture AS ''{$general.culture$}'', 
  SUM(HitsCount) AS ''{$reports_pageviews_day.hits_header$}'' 
  FROM Analytics_Statistics 
    INNER JOIN Analytics_DayHits ON Analytics_DayHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
    LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND Analytics_Statistics.StatisticsObjectCulture = View_CMS_Tree_Joined.DocumentCulture
    WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) 
  AND (StatisticsCode=@CodeName) 
  AND (HitsStartTime >= @FromDate) 
  AND (HitsEndTime <= @ToDate) 
  GROUP BY StatisticsObjectCulture, DocumentNamePath, StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC
  
INSERT INTO @countedAll 
  SELECT
  SUM(HitsCount) AS ''{$reports_pageviews_day.hits_header$}'' 
  FROM Analytics_Statistics 
    INNER JOIN Analytics_DayHits ON Analytics_DayHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
    LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND Analytics_Statistics.StatisticsObjectCulture = View_CMS_Tree_Joined.DocumentCulture
    WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) 
  AND (StatisticsCode=@CodeName) 
  AND (HitsStartTime >= @FromDate) 
  AND (HitsEndTime <= @ToDate)   
  
SELECT DocumentNamePath AS ''{$reports_aggviews_day.path_header$}'',  
StatisticsObjectCulture AS ''{$general.culture$}'', 
Count AS ''{$reports_aggviews_day.hits_header$}'',
CAST (CAST((100*Count)/(SELECT Counted FROM  @countedAll) as decimal(10,2))AS NVARCHAR(10))+''%''
as ''{$reports_aggviews_day.hits_percent_header$}'' 
FROM @myselection', 0, 192, N'<customdata><pagesize>10</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '21078700-7c9b-464b-9039-a9846e13d8e3', '20110621 11:09:32')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (207, N'TableHourFileAggViewsCulture', N'table', N'SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''hour'');
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
DECLARE @myselection TABLE (
   DocumentNamePath varchar(400),
   StatisticsObjectCulture varchar(400),
   Count float   
);
DECLARE @countedAll TABLE (
   Counted float   
);
INSERT INTO @myselection 
  SELECT TOP 100 
  CASE WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName ELSE DocumentNamePath END, 
  StatisticsObjectCulture, SUM(HitsCount) 
  FROM Analytics_Statistics
    INNER JOIN Analytics_HourHits ON Analytics_HourHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
    LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND View_CMS_Tree_Joined.DocumentCulture = Analytics_Statistics.StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) 
  AND (StatisticsCode=@CodeName) 
  AND (HitsStartTime >= @FromDate) 
  AND (HitsEndTime <= @ToDate) 
  GROUP BY StatisticsObjectCulture, DocumentNamePath, StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC, StatisticsObjectCulture
INSERT INTO @countedAll 
  SELECT
  SUM(HitsCount) 
  FROM Analytics_Statistics
    INNER JOIN Analytics_HourHits ON Analytics_HourHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
    LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND View_CMS_Tree_Joined.DocumentCulture = Analytics_Statistics.StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) 
  AND (StatisticsCode=@CodeName) 
  AND (HitsStartTime >= @FromDate) 
  AND (HitsEndTime <= @ToDate)   
  
 SELECT DocumentNamePath AS ''{$reports_aggviews_hour.path_header$}'', 
 StatisticsObjectCulture AS ''{$general.culture$}'', 
 Count AS ''{$reports_aggviews_hour.hits_header$}'',
 CAST (CAST((100*Count)/(SELECT Counted FROM  @countedAll) as decimal(10,2))AS NVARCHAR(10))+''%'' as ''{$reports_aggviews_hour.hits_percent_header$}'' 
 FROM @myselection', 0, 193, N'<customdata><pagesize>10</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '5518de48-41fd-4a57-9da9-47e7124497d6', '20110621 11:09:32')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (208, N'TableYearAggViewsCulture', N'table', N'SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''year'');
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
DECLARE @myselection TABLE (
   DocumentNamePath varchar(400),
   StatisticsObjectCulture varchar(400),
   Count float   
);
DECLARE @countedAll TABLE (
   Counted float   
);
INSERT INTO @myselection
  SELECT TOP 100
  CASE WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName ELSE DocumentNamePath END,
  StatisticsObjectCulture ,
  SUM(HitsCount) AS Count
  FROM Analytics_Statistics
  INNER JOIN Analytics_YearHits ON Analytics_YearHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND View_CMS_Tree_Joined.DocumentCulture = Analytics_Statistics.StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectCulture, DocumentNamePath, StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC
  
INSERT INTO @countedAll
  SELECT
  SUM(HitsCount) AS Count
  FROM Analytics_Statistics
  INNER JOIN Analytics_YearHits ON Analytics_YearHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND View_CMS_Tree_Joined.DocumentCulture = Analytics_Statistics.StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)   
 SELECT DocumentNamePath AS ''{$reports_aggviews_Year.path_header$}'',
 StatisticsObjectCulture AS   ''{$general.culture$}'',
 Count AS ''{$reports_aggviews_Year.hits_header$}'',
 CAST (CAST((100*Count)/(SELECT Counted FROM  @countedAll) as decimal(10,2))AS NVARCHAR(10))+''%'' as ''{$reports_aggviews_Year.hits_percent_header$}''
 FROM @myselection', 0, 194, N'<customdata><querynorecordtext></querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '4d861887-7695-4c86-9114-4894444dac8c', '20110621 11:09:32')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (285, N'SampleTable', N'Sample table', N'SELECT  ClassDisplayName as ''Class name'',  ((((ClassID+1)) % 10) *  (datepart(ms, GETDATE()) +1)) as ''Random value''  FROM CMS_Class WHERE ClassIsMenuItemType = 1', 0, 361, N'<customdata><pagesize>4</pagesize><skinid></skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><enablepaging>True</enablepaging><pagemode>1</pagemode></customdata>', '34e7c906-c29d-4e9d-b4a4-4b9f9e217972', '20110621 11:09:31')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (298, N'TableMonthBrowserType_1', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''week'');
SELECT TOP 100 
 StatisticsObjectName AS ''{$reports_browsertype.name_header$}'',
 SUM(HitsCount) AS ''{$reports_browsertype.hits_header$}'', 
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount) 
  FROM Analytics_Statistics
  INNER JOIN Analytics_WeekHits ON Analytics_Statistics.StatisticsID = Analytics_WeekHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID) 
  AND (StatisticsCode=@CodeName) 
  AND (HitsStartTime >= @FromDate) 
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) AS VARCHAR)+''%'' AS ''{$reports_browsertype.percent_header$}'' 
  FROM Analytics_Statistics
  INNER JOIN Analytics_WeekHits ON Analytics_Statistics.StatisticsID = Analytics_WeekHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) 
  AND (StatisticsCode=@CodeName) 
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate) 
  GROUP BY StatisticsObjectName 
  ORDER BY SUM(HitsCount) DESC', 0, 469, N'<customdata><pagesize>10</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext></querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', '2571db6b-78dd-4656-ab6e-7c28ff707e06', '20110726 09:49:20')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (299, N'TableMonthRegisteredUsers', N'Table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''hour'');
SELECT TOP 100 
 StatisticsObjectID AS ''{$reports_registeredusers.UserID_header$}'', 
 StatisticsObjectName AS ''{$reports_registeredusers.UserName_header$}'' 
 FROM Analytics_Statistics
 INNER JOIN Analytics_HourHits ON Analytics_HourHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) 
 AND (StatisticsCode=@CodeName) 
 AND (HitsStartTime >= @FromDate) 
 AND (HitsEndTime <= @ToDate)', 0, 471, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '04011971-ea7e-476c-a188-c21be0ace365', '20110621 11:09:34')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (300, N'TableMonthRegisteredUsers', N'Table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day'');
SELECT TOP 100 
 StatisticsObjectID AS ''{$reports_registeredusers.UserID_header$}'', 
 StatisticsObjectName AS ''{$reports_registeredusers.UserName_header$}'' 
 FROM Analytics_Statistics
 INNER JOIN Analytics_DayHits ON Analytics_DayHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) 
 AND (StatisticsCode=@CodeName) 
 AND (HitsStartTime >= @FromDate) 
 AND (HitsEndTime <= @ToDate)', 0, 472, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '899d074a-a51a-469a-8f9f-df67debb5233', '20110621 11:09:33')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (301, N'table', N'Table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''week'');
SELECT TOP 100 
 StatisticsObjectID AS ''{$reports_registeredusers.UserID_header$}'', 
 StatisticsObjectName AS ''{$reports_registeredusers.UserName_header$}'' 
 FROM Analytics_Statistics
 INNER JOIN Analytics_WeekHits ON Analytics_WeekHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) 
 AND (StatisticsCode=@CodeName) 
 AND (HitsStartTime >= @FromDate) 
 AND (HitsEndTime <= @ToDate)', 0, 473, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '4cf6e149-d101-4e78-a500-56e1307b00c4', '20110621 11:09:33')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (302, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''week'');
SELECT TOP 100 CampaignDisplayName AS ''{$reports_campaign.name_header$}'', SUM(HitsCount) AS ''{$reports_campaign.hits_header$}'' FROM
Analytics_WeekHits, Analytics_Statistics
INNER JOIN Analytics_Campaign ON StatisticsObjectName = CampaignName AND CampaignSiteID = @CMSContextCurrentSiteID
WHERE (@CampaignName ='''' OR @CampaignName = CampaignName) AND
(StatisticsSiteID = @CMSContextCurrentSiteID) AND (StatisticsCode=@CodeName) AND (StatisticsID = HitsStatisticsID) AND
(HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate) GROUP BY CampaignDisplayName ORDER BY SUM(HitsCount) DESC', 0, 474, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'a1153580-6c56-4262-9190-eb0716f6f079', '20110621 11:09:33')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (303, N'table', N'table', N'DECLARE @Sum DECIMAL;
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''week'');
SELECT @Sum = 
 SUM(HitsCount)  FROM
Analytics_Statistics, Analytics_WeekHits WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
 AND (StatisticsCode=@CodeName) AND (StatisticsID = HitsStatisticsID) AND 
 (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate) 
SELECT TOP 100 StatisticsObjectName AS ''{$reports_referrals_week.name_header$}'', 
SUM(HitsCount) AS ''{$reports_referrals_week.hits_header$}'',
CAST (CAST ((SUM(HitsCount) / @Sum) *100 AS DECIMAL(10,2)) AS NVARCHAR(8)) +''%'' AS ''{$reports_referrals_week.percent_header$}''
  FROM
 Analytics_Statistics, Analytics_WeekHits WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
 AND (StatisticsCode=@CodeName) AND (StatisticsID = HitsStatisticsID) AND 
 (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate) 
 GROUP BY StatisticsObjectName ORDER BY SUM(HitsCount) DESC', 0, 475, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '95647f58-b23d-4024-bca9-7da8dc9a4786', '20110621 11:09:34')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (304, N'table', N'table', N'DECLARE @PaveViews TABLE
(
  DocumentNamePath NVARCHAR(500),
  Pageviews INT,
  Percents DECIMAL(10,2)  
)
DECLARE @Sum DECIMAL;
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''week'');
SELECT @Sum =   
  SUM(HitsCount) 
  FROM Analytics_Statistics
  INNER JOIN Analytics_WeekHits ON Analytics_WeekHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID  AND StatisticsObjectCulture = DocumentCulture 
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
 AND (HitsEndTime <= @ToDate)
INSERT INTO @PaveViews (DocumentNamePath,pageViews,percents)
SELECT TOP 100
 CASE
  WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName
  ELSE DocumentNamePath
 END , SUM(HitsCount), (SUM(HitsCount)/@Sum)*100
 FROM Analytics_Statistics
 INNER JOIN Analytics_WeekHits ON Analytics_WeekHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
 LEFT JOIN View_CMS_Tree_Joined ON Analytics_Statistics.StatisticsObjectID = View_CMS_Tree_Joined.NodeID  AND StatisticsObjectCulture = DocumentCulture 
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
 AND (StatisticsCode=@CodeName)
 GROUP BY DocumentNamePath, StatisticsObjectName
 ORDER BY SUM(HitsCount) DESC
 
  
 SELECT DocumentNamePath AS ''{$reports_aggviews_Week.path_header$}'',pageviews AS  ''{$reports_aggviews_Week.hits_header$}'',
  CAST (Percents AS NVARCHAR(10)) +''%'' AS ''{$reports_aggviews_Week.hits_percent_header$}''
   FROM @PaveViews ORDER BY PageViews DESC', 0, 476, N'<customdata><pagesize>10</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'c3578044-57df-474f-ac35-29fd71719ec0', '20110920 12:21:23')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (305, N'table', N'table', N'SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''week'');
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week'');
DECLARE @myselection TABLE (
   DocumentNamePath varchar(400),
   StatisticsObjectCulture varchar(400),
   Count float   
);
DECLARE @countedAll TABLE (
   Counted float   
);
INSERT INTO @myselection
  SELECT TOP 100
  CASE WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName ELSE DocumentNamePath END,
  StatisticsObjectCulture ,
  SUM(HitsCount) AS Count
  FROM Analytics_Statistics
  INNER JOIN Analytics_WeekHits ON Analytics_WeekHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND View_CMS_Tree_Joined.DocumentCulture = Analytics_Statistics.StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectCulture, DocumentNamePath, StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC
  
INSERT INTO @countedAll
  SELECT
  SUM(HitsCount) AS Count
  FROM Analytics_Statistics
  INNER JOIN Analytics_WeekHits ON Analytics_WeekHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND View_CMS_Tree_Joined.DocumentCulture = Analytics_Statistics.StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)   
 SELECT DocumentNamePath AS ''{$reports_aggviews_Week.path_header$}'',
 StatisticsObjectCulture AS   ''{$general.culture$}'',
 Count AS ''{$reports_aggviews_Week.hits_header$}'',
 CAST (CAST((100*Count)/(SELECT Counted FROM  @countedAll) as decimal(10,2))AS NVARCHAR(10))+''%'' as ''{$reports_aggviews_Week.hits_percent_header$}''
 FROM @myselection', 0, 477, N'<customdata><querynorecordtext></querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'cba70eae-0ef5-42bd-8424-fbc45db31501', '20110621 11:09:33')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (306, N'table', N'table', N'DECLARE @PaveViews TABLE
(
  DocumentNamePath NVARCHAR(500),
  Pageviews INT,
  Percents DECIMAL(10,2)  
)
DECLARE @Sum DECIMAL;
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''week'');
SELECT @Sum =   
  SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_WeekHits ON Analytics_WeekHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND StatisticsObjectCulture = DocumentCulture 
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
 AND (HitsEndTime <= @ToDate)
INSERT INTO @PaveViews (DocumentNamePath,pageViews,percents)
SELECT TOP 100
 CASE
  WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName
  ELSE DocumentNamePath
 END , SUM(HitsCount), (SUM(HitsCount)/@Sum)*100
 FROM Analytics_Statistics
 INNER JOIN Analytics_WeekHits ON Analytics_WeekHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
 LEFT JOIN View_CMS_Tree_Joined ON Analytics_Statistics.StatisticsObjectID = View_CMS_Tree_Joined.NodeID AND StatisticsObjectCulture = DocumentCulture
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
 AND (StatisticsCode=@CodeName)
 GROUP BY DocumentNamePath, StatisticsObjectName
 ORDER BY [DocumentNamePath] DESC
 
  
 SELECT DocumentNamePath AS ''{$general.documentname$}'',pageviews AS  ''{$reports_filedownloads_Week.hits_header$}'',
   CAST (Percents AS NVARCHAR(10))+''%'' AS ''{$reports_filedownloads_Week.hits_percent_header$}''
   FROM @PaveViews ORDER BY PageViews DESC', 0, 478, N'<customdata><querynorecordtext></querynorecordtext><exportenabled>true</exportenabled><pagesize>10</pagesize><enablepaging>False</enablepaging><skinid>ReportGridAnalytics</skinid><pagemode>1</pagemode></customdata>', '2dcb9d1f-d9b2-4b01-b52b-1df9d03f5bd3', '20110923 12:19:24')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (307, N'table', N'table', N'SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''week'');
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week'');
DECLARE @myselection TABLE (
   DocumentNamePath varchar(400),
   StatisticsObjectCulture varchar(400),
   Count float   
);
DECLARE @countedAll TABLE (
   Counted float   
);
INSERT INTO @myselection
  SELECT TOP 100
  CASE WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName ELSE DocumentNamePath END,
  StatisticsObjectCulture ,
  SUM(HitsCount) AS Count
  FROM Analytics_Statistics
  INNER JOIN Analytics_WeekHits ON Analytics_WeekHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND View_CMS_Tree_Joined.DocumentCulture = Analytics_Statistics.StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectCulture, DocumentNamePath, StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC
  
INSERT INTO @countedAll
  SELECT
  SUM(HitsCount) AS Count
  FROM Analytics_Statistics
  INNER JOIN Analytics_WeekHits ON Analytics_WeekHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND View_CMS_Tree_Joined.DocumentCulture = Analytics_Statistics.StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)   
 SELECT DocumentNamePath AS ''{$general.documentname$}'',
 StatisticsObjectCulture AS   ''{$general.culture$}'',
 Count AS ''{$reports_filedownloads_Week.hits_header$}'',
 CAST (CAST((100*Count)/(SELECT Counted FROM  @countedAll) as decimal(10,2))AS NVARCHAR(10))+''%'' as ''{$reports_filedownloads_Week.hits_percent_header$}''
 FROM @myselection', 0, 479, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '6b0d63a6-0cea-43bf-af5a-c4b7cfe7f5be', '20110621 11:09:34')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (308, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''week'');
SELECT TOP 100 
 StatisticsObjectName AS ''{$reports_pagenotfound_Week.name_header$}'', 
 SUM(HitsCount) AS ''{$reports_pagenotfound_Week.hits_header$}'' 
 FROM Analytics_Statistics
 INNER JOIN Analytics_WeekHits ON Analytics_WeekHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) 
 AND (StatisticsCode=@CodeName)  
 AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate) 
 GROUP BY StatisticsObjectName 
 ORDER BY SUM(HitsCount) DESC', 0, 480, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'f6686f82-a930-49d2-a08a-ba28392e4877', '20110621 11:09:34')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (309, N'table', N'table', N'DECLARE @PaveViews TABLE
(
  DocumentNamePath NVARCHAR(500),
  ObjectID INT,
  Pageviews INT,
  Percents DECIMAL(10,2),
  Average INT  
)
DECLARE @Sum DECIMAL;
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''week'');
SELECT @Sum =   
  SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_WeekHits ON Analytics_WeekHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID  AND StatisticsObjectCulture = DocumentCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
 AND (HitsEndTime <= @ToDate)
INSERT INTO @PaveViews (DocumentNamePath,objectID,pageViews,percents)
SELECT TOP 100
 CASE
  WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName
  ELSE DocumentNamePath
 END , StatisticsObjectID, SUM(HitsCount), (SUM(HitsCount)/@Sum)*100
 FROM Analytics_Statistics
 INNER JOIN Analytics_WeekHits ON Analytics_WeekHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
 LEFT JOIN View_CMS_Tree_Joined ON Analytics_Statistics.StatisticsObjectID = View_CMS_Tree_Joined.NodeID AND StatisticsObjectCulture = DocumentCulture
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
 AND (StatisticsCode=@CodeName)
 GROUP BY DocumentNamePath, StatisticsObjectName,StatisticsObjectID
 ORDER BY SUM(HitsCount) DESC
 
 UPDATE @PaveViews SET Average = (SELECT SUM(HitsValue)/SUM(HitsCount) FROM Analytics_WeekHits JOIN
      Analytics_Statistics ON HitsStatisticsID = StatisticsID
      WHERE HitsStartTime >= @FromDate AND HitsEndTime <= @ToDate AND StatisticsObjectID = objectID
        AND StatisticsCode =''avgtimeonpage'' AND StatisticsSiteID = @CMSContextCurrentSiteID
       )
 
 SELECT DocumentNamePath AS ''{$reports_pageviews_Week.path_header$}'',pageviews AS  ''{$reports_pageviews_Week.hits_header$}'',
      CAST (Percents AS NVARCHAR(10))+''%'' AS ''{$reports_pageviews.percent_header$}'', ISNULL(CONVERT(varchar, DATEADD(s, average, 0), 108),''00:00:00'') AS ''{$reports_pageviews.average$}''
  
   FROM @PaveViews ORDER BY PageViews DESC', 0, 481, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><pagesize>10</pagesize><enablepaging>False</enablepaging><skinid>ReportGridAnalytics</skinid><pagemode>1</pagemode></customdata>', 'b48a5b0b-b1d4-4756-84a4-9aa09fcf2ab3', '20110920 14:00:49')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (310, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''week'');
DECLARE @myselection TABLE (
   DocumentNamePath varchar(400),
   ObjectID INT,
   Culture varchar(400),
   Count float,
   Average int
);
DECLARE @countedAll TABLE (
   Counted float   
);
INSERT INTO @myselection (DocumentNamePath,ObjectID,Culture,Count)
  SELECT TOP 100
  CASE WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName ELSE DocumentNamePath END,
  StatisticsObjectID,StatisticsObjectCulture , 
  SUM(HitsCount) AS Count
  FROM Analytics_Statistics
  INNER JOIN Analytics_WeekHits ON Analytics_WeekHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND View_CMS_Tree_Joined.DocumentCulture = Analytics_Statistics.StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectCulture, DocumentNamePath, StatisticsObjectName, StatisticsObjectID
  ORDER BY SUM(HitsCount) DESC
  
INSERT INTO @countedAll
  SELECT
  SUM(HitsCount) AS Counted
  FROM Analytics_Statistics
  INNER JOIN Analytics_WeekHits ON Analytics_WeekHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND View_CMS_Tree_Joined.DocumentCulture = Analytics_Statistics.StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)
 UPDATE @myselection SET Average = (SELECT SUM (HitsValue) / SUM (HitsCount) FROM Analytics_HourHits JOIN
      Analytics_Statistics ON HitsStatisticsID = StatisticsID
      WHERE HitsStartTime >= @FromDate AND HitsEndTime <= @ToDate AND ObjectID = StatisticsObjectID
        AND StatisticsCode =''avgtimeonpage'' AND StatisticsSiteID = @CMSContextCurrentSiteID
        AND culture = Analytics_Statistics.StatisticsObjectCulture
       )       
 SELECT DocumentNamePath AS ''{$reports_pageviews_Week.path_header$}'',
 culture AS   ''{$general.culture$}'',
 Count AS ''{$reports_pageviews_Week.hits_header$}'',
 CAST (CAST((100*Count)/(SELECT Counted FROM  @countedAll) as decimal(10,2))AS NVARCHAR(10))+''%'' AS ''{$reports_pageviews_Week.hits_percent_header$}'',
 ISNULL(CONVERT(varchar, DATEADD(s, average, 0), 108),''00:00:00'') AS ''{$reports_pageviews.average$}''
 FROM @myselection', 0, 482, N'<customdata><querynorecordtext></querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'c4dddf6e-aa81-4eae-9166-a53718aa3a65', '20110621 11:09:34')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (311, N'TableMonthConversion_1', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''week'');
SELECT TOP 100 ConversionDisplayName AS ''{$reports_conversion.name_header$}'', SUM(HitsCount) AS
''{$reports_conversion.hits_header$}'',SUM(HitsValue) AS ''{$reports_conversion.value_header$}'' FROM
Analytics_Statistics
JOIN Analytics_WeekHits  ON HitsStatisticsID = StatisticsID
JOIN Analytics_Conversion ON ConversionName  = StatisticsObjectName AND ConversionSiteID = @CMSContextCurrentSiteID
WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND
(StatisticsCode=@CodeName) AND (StatisticsID = HitsStatisticsID) AND (HitsStartTime >= @FromDate) AND
(HitsEndTime <= @ToDate) GROUP BY ConversionDisplayName ORDER BY SUM(HitsCount) DESC', 0, 483, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><pagesize>10</pagesize><enablepaging>False</enablepaging><skinid>ReportGridAnalytics</skinid><pagemode>1</pagemode></customdata>', '85728b46-42cd-42d4-8d54-e0f01f5a70d1', '20110727 12:10:07')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (314, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week'');
  SELECT CONVERT (NVARCHAR(2),DATEPART(wk,[Date]))+''/''+CONVERT (NVARCHAR(4),DATEPART(YEAR,[Date])) AS ''{$ecommerce.report_week$}'', COUNT(OrderDate) AS ''{$ecommerce.report_number$}'' FROM 
  {%DatabaseSchema%}.Func_Analytics_EnsureDates (@FromDate,@ToDate,''week'') AS Dates
  LEFT JOIN COM_Order  ON   
    ([Date] <= OrderDate  AND  DATEADD(DAY,7,[Date]) > OrderDate) AND 
    OrderSiteID = @CMSContextCurrentSiteID
  GROUP BY [Date]
ORDER BY [Date] DESC', 0, 489, N'<customdata><pagesize>10</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext></querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', '1f53407e-d610-41a7-b803-a6deaa20b223', '20111003 07:21:16')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (315, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
  SELECT [Date] AS ''{$ecommerce.report_hour$}'', COUNT(OrderDate) AS ''{$ecommerce.report_number$}''  FROM 
  {%DatabaseSchema%}.Func_Analytics_EnsureDates (@FromDate,@ToDate,''hour'') AS Dates
  LEFT JOIN COM_Order  ON   
    ([Date] <= OrderDate  AND  DATEADD(HOUR,1,[Date]) > OrderDate) AND 
    OrderSiteID = @CMSContextCurrentSiteID
  GROUP BY [Date]
ORDER BY [Date] DESC', 0, 490, N'<customdata><pagesize>10</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext></querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', 'a7dbcbda-1d17-46aa-9021-a88707265e35', '20111003 07:20:39')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (321, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
  SELECT DATEPART(YEAR,[Date]) AS ''{$ecommerce.report_year$}'',
 CAST(SUM(ISNULL(OrderTotalPriceInMainCurrency,0)) AS decimal(38,2)) AS ''{$ecommerce.report_income$}'' FROM 
  {%DatabaseSchema%}.Func_Analytics_EnsureDates (@FromDate,@ToDate,''year'') AS Dates
  LEFT JOIN COM_Order
    ON Dates.Date = DATENAME(year, OrderDate)
      AND  OrderSiteID = @CMSContextCurrentSiteID
  GROUP BY [Date]
ORDER BY [Date] DESC', 0, 496, N'<customdata><pagesize>10</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext></querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', '182b094b-cd05-4c1a-bda2-800a646041c6', '20111003 07:24:02')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (322, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week'');
  SELECT CONVERT (NVARCHAR(2),DATEPART(wk,[Date]))+''/''+CONVERT (NVARCHAR(4),DATEPART(YEAR,[Date])) AS ''{$ecommerce.report_week$}'', 
   CAST(SUM(ISNULL(OrderTotalPriceInMainCurrency,0)) AS decimal(38,2)) AS ''{$ecommerce.report_income$}'' FROM 
  {%DatabaseSchema%}.Func_Analytics_EnsureDates (@FromDate,@ToDate,''week'') AS Dates
  LEFT JOIN COM_Order  ON   
    ([Date] <= OrderDate  AND  DATEADD(DAY,7,[Date]) > OrderDate) AND 
    OrderSiteID = @CMSContextCurrentSiteID
  GROUP BY [Date]
ORDER BY [Date] DESC', 0, 497, N'<customdata><pagesize>10</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext></querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', '7cc18155-06ab-42c5-9dbf-16a86999ca95', '20111003 07:23:45')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (323, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
  SELECT DATENAME(MONTH,[Date])+'', ''+ DATENAME(YEAR, [Date]) AS ''{$ecommerce.report_month$}'',
  CAST(SUM(ISNULL(OrderTotalPriceInMainCurrency,0)) AS decimal(38,2)) AS ''{$ecommerce.report_income$}'' FROM 
  {%DatabaseSchema%}.Func_Analytics_EnsureDates (@FromDate,@ToDate,''month'') AS Dates
  LEFT JOIN COM_Order  ON   
    ([Date] <= OrderDate  AND  DATEADD(MONTH,1,[Date]) > OrderDate) AND 
    OrderSiteID = @CMSContextCurrentSiteID
  GROUP BY [Date]
ORDER BY [Date] DESC', 0, 498, N'<customdata><pagesize>10</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext></querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', 'c52f1ae0-3c4c-42cb-850b-3f2937b7b69e', '20111003 07:23:29')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (324, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
  SELECT [Date] AS ''{$ecommerce.report_hour$}'',  CAST(SUM(ISNULL(OrderTotalPriceInMainCurrency,0)) AS decimal(38,2)) AS ''{$ecommerce.report_income$}''  FROM 
  {%DatabaseSchema%}.Func_Analytics_EnsureDates (@FromDate,@ToDate,''hour'') AS Dates
  LEFT JOIN COM_Order  ON   
    ([Date] <= OrderDate  AND  DATEADD(HOUR,1,[Date]) > OrderDate) AND 
    OrderSiteID = @CMSContextCurrentSiteID
  GROUP BY [Date]
ORDER BY [Date] DESC', 0, 499, N'<customdata><pagesize>10</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext></querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', 'a765e078-d500-4692-afe2-6a760a35e55d', '20111003 07:23:15')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (325, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
  SELECT CAST(DATEPART(MONTH,[Date]) AS NVARCHAR(2)) +''/''+ DATENAME(DAY,[Date])+''/''+DATENAME(YEAR,[Date]) AS ''{$ecommerce.report_day$}'',
   CAST(SUM(ISNULL(OrderTotalPriceInMainCurrency,0)) AS decimal(38,2)) AS ''{$ecommerce.report_income$}''  FROM 
  {%DatabaseSchema%}.Func_Analytics_EnsureDates (@FromDate,@ToDate,''day'') AS Dates
  LEFT JOIN COM_Order  ON   
    ([Date] <= OrderDate  AND  DATEADD(DAY,1,[Date]) > OrderDate) AND 
    OrderSiteID = @CMSContextCurrentSiteID
  GROUP BY [Date]
ORDER BY [Date] DESC', 0, 500, N'<customdata><pagesize>10</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext></querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', '47e4aada-6b9c-4fa4-8d0f-32d20c4022f7', '20111003 07:22:55')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (327, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''year'');
SELECT TOP 100
 ''{''+''$analytics_codename.'' + StatisticsObjectName + ''$}'' AS ''{$reports_javasupport.name_header$}'',
 SUM(HitsCount) AS ''{$reports_javasupport.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_YearHits ON Analytics_Statistics.StatisticsID = Analytics_YearHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) AS VARCHAR)+''%'' AS ''{$reports_javasupport.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_YearHits ON Analytics_Statistics.StatisticsID = Analytics_YearHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 501, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext></querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '9203700e-7bbd-44ed-8e52-d149c57ba0f7', '20110907 11:16:52')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (328, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''month'');
SELECT TOP 100
 ''{''+''$analytics_codename.'' + StatisticsObjectName + ''$}'' AS ''{$reports_javasupport.name_header$}'',
 SUM(HitsCount) AS ''{$reports_javasupport.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_Statistics.StatisticsID = Analytics_MonthHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) AS VARCHAR)+''%'' AS ''{$reports_javasupport.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_Statistics.StatisticsID = Analytics_MonthHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 502, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext></querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'ef2b1ab9-87e5-43e7-a198-bb5832b764b3', '20110907 11:17:10')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (329, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''week'');
SELECT TOP 100
 ''{''+''$analytics_codename.'' + StatisticsObjectName + ''$}'' AS ''{$reports_javasupport.name_header$}'',
 SUM(HitsCount) AS ''{$reports_javasupport.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_WeekHits ON Analytics_Statistics.StatisticsID = Analytics_WeekHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) AS VARCHAR)+''%'' AS ''{$reports_javasupport.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_WeekHits ON Analytics_Statistics.StatisticsID = Analytics_WeekHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 503, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext></querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '3f89a211-0a4e-4428-a299-d5c50114d78f', '20110907 11:17:01')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (330, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day'');
SELECT TOP 100
 ''{''+''$analytics_codename.'' + StatisticsObjectName + ''$}'' AS ''{$reports_javasupport.name_header$}'',
 SUM(HitsCount) AS ''{$reports_javasupport.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_DayHits ON Analytics_Statistics.StatisticsID = Analytics_DayHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) AS VARCHAR)+''%'' AS ''{$reports_javasupport.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_DayHits ON Analytics_Statistics.StatisticsID = Analytics_DayHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 504, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext></querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'df007b78-ed2b-4507-b6ed-b6a2f24e85d3', '20110907 11:17:27')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (331, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''hour'');
SELECT TOP 100
 ''{''+''$analytics_codename.'' + StatisticsObjectName + ''$}'' AS ''{$reports_javasupport.name_header$}'',
 SUM(HitsCount) AS ''{$reports_javasupport.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_HourHits ON Analytics_Statistics.StatisticsID = Analytics_HourHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) AS VARCHAR)+''%'' AS ''{$reports_javasupport.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_HourHits ON Analytics_Statistics.StatisticsID = Analytics_HourHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 505, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext></querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'bbc5a3e1-d48f-40e4-ad88-f55927616f05', '20110907 11:17:18')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (332, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''month'');
SELECT TOP 100
 ''{''+''$analytics_codename.'' + StatisticsObjectName + ''$}'' AS ''{$reports_silverlightsupport.name_header$}'',
 SUM(HitsCount) AS ''{$reports_silverlightsupport.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_Statistics.StatisticsID = Analytics_MonthHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) AS VARCHAR)+''%'' AS ''{$reports_silverlightsupport.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_Statistics.StatisticsID = Analytics_MonthHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 506, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext></querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'ecb19453-51f8-45b3-a7f0-00d7c9c0694f', '20110907 11:13:28')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (333, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''month'');
SELECT TOP 100
 ''{''+''$analytics_codename.'' + StatisticsObjectName + ''$}'' AS ''{$reports_silverlightsupport.name_header$}'',
 SUM(HitsCount) AS ''{$reports_silverlightsupport.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_Statistics.StatisticsID = Analytics_MonthHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) AS VARCHAR)+''%'' AS ''{$reports_silverlightsupport.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_Statistics.StatisticsID = Analytics_MonthHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 507, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext></querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'fbd5e825-0dae-4b0a-9541-f1afbb3adc99', '20110907 11:13:58')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (334, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''month'');
SELECT TOP 100
 ''{''+''$analytics_codename.'' + StatisticsObjectName + ''$}'' AS ''{$reports_silverlightsupport.name_header$}'',
 SUM(HitsCount) AS ''{$reports_silverlightsupport.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_Statistics.StatisticsID = Analytics_MonthHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) AS VARCHAR)+''%'' AS ''{$reports_silverlightsupport.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_Statistics.StatisticsID = Analytics_MonthHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 508, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext></querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'dad6d423-5765-413a-99a4-5866f47eb0da', '20110907 11:13:38')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (335, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''month'');
SELECT TOP 100
 ''{''+''$analytics_codename.'' + StatisticsObjectName + ''$}'' AS ''{$reports_silverlightsupport.name_header$}'',
 SUM(HitsCount) AS ''{$reports_silverlightsupport.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_Statistics.StatisticsID = Analytics_MonthHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) AS VARCHAR)+''%'' AS ''{$reports_silverlightsupport.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_Statistics.StatisticsID = Analytics_MonthHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 509, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext></querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'f24e73ec-8a3f-4814-8904-982de95be061', '20110907 11:14:15')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (336, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''hour'');
SELECT TOP 100
 ''{''+''$analytics_codename.'' + StatisticsObjectName + ''$}'' AS ''{$reports_silverlightsupport.name_header$}'',
 SUM(HitsCount) AS ''{$reports_silverlightsupport.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_HourHits ON Analytics_Statistics.StatisticsID = Analytics_HourHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) AS VARCHAR)+''%'' AS ''{$reports_silverlightsupport.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_HourHits ON Analytics_Statistics.StatisticsID = Analytics_HourHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 510, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext></querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '46f0047f-dab7-4b19-adf5-a8339e1e9e20', '20110907 11:14:06')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (337, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''year'');
SELECT TOP 100
 ''{''+''$analytics_codename.'' + StatisticsObjectName + ''$}'' AS ''{$reports_flash.name_header$}'',
 SUM(HitsCount) AS ''{$reports_flash.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_YearHits ON Analytics_Statistics.StatisticsID = Analytics_YearHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) AS VARCHAR)+''%'' AS ''{$reports_flash.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_YearHits ON Analytics_Statistics.StatisticsID = Analytics_YearHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 511, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext></querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '683f5483-a9a8-442b-86a6-d7a4bc867299', '20110907 11:17:39')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (338, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''month'');
SELECT TOP 100
 ''{''+''$analytics_codename.'' + StatisticsObjectName + ''$}'' AS ''{$reports_flash.name_header$}'',
 SUM(HitsCount) AS ''{$reports_flash.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_Statistics.StatisticsID = Analytics_MonthHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) AS VARCHAR)+''%'' AS ''{$reports_flash.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_Statistics.StatisticsID = Analytics_MonthHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 512, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext></querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '488e807b-e09f-4eba-a3e4-994e79ad890d', '20110907 11:18:00')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (339, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''week'');
SELECT TOP 100
 ''{''+''$analytics_codename.'' + StatisticsObjectName + ''$}'' AS ''{$reports_flash.name_header$}'',
 SUM(HitsCount) AS ''{$reports_flash.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_WeekHits ON Analytics_Statistics.StatisticsID = Analytics_WeekHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) AS VARCHAR)+''%'' AS ''{$reports_flash.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_WeekHits ON Analytics_Statistics.StatisticsID = Analytics_WeekHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 513, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext></querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '122a03d6-773b-4935-866c-84eb5de4cd83', '20110907 11:17:48')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (340, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day'');
SELECT TOP 100
 ''{''+''$analytics_codename.'' + StatisticsObjectName + ''$}'' AS ''{$reports_flash.name_header$}'',
 SUM(HitsCount) AS ''{$reports_flash.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_DayHits ON Analytics_Statistics.StatisticsID = Analytics_DayHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) AS VARCHAR)+''%'' AS ''{$reports_flash.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_DayHits ON Analytics_Statistics.StatisticsID = Analytics_DayHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 514, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext></querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '3f829dd8-6bd6-4d4c-8967-47bbcb0f59b9', '20110907 11:18:17')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (341, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''hour'');
SELECT TOP 100
 ''{''+''$analytics_codename.'' + StatisticsObjectName + ''$}'' AS ''{$reports_flash.name_header$}'',
 SUM(HitsCount) AS ''{$reports_flash.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_HourHits ON Analytics_Statistics.StatisticsID = Analytics_HourHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) AS VARCHAR)+''%'' AS ''{$reports_flash.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_HourHits ON Analytics_Statistics.StatisticsID = Analytics_HourHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 515, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext></querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '2fd69c87-0202-49ab-9ee6-9983913f5bf2', '20110907 11:18:06')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (342, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''year'');
SELECT TOP 100
 StatisticsObjectName AS ''{$reports_screenresolution.name_header$}'',
 SUM(HitsCount) AS ''{$reports_screenresolution.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_YearHits ON Analytics_Statistics.StatisticsID = Analytics_YearHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) AS VARCHAR)+''%'' AS ''{$reports_screenresolution.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_YearHits ON Analytics_Statistics.StatisticsID = Analytics_YearHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 516, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext></querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'a7894103-89bb-465e-9cd8-a45d25d65fad', '20110907 11:14:28')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (344, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''month'');
SELECT TOP 100
 StatisticsObjectName AS ''{$reports_screenresolution.name_header$}'',
 SUM(HitsCount) AS ''{$reports_screenresolution.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_Statistics.StatisticsID = Analytics_MonthHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) AS VARCHAR)+''%'' AS ''{$reports_screenresolution.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_Statistics.StatisticsID = Analytics_MonthHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 518, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext></querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'e4b34917-7a27-4413-8cc1-991b40b7bb4c', '20110907 11:14:48')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (345, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''week'');
SELECT TOP 100
 StatisticsObjectName AS ''{$reports_screenresolution.name_header$}'',
 SUM(HitsCount) AS ''{$reports_screenresolution.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_WeekHits ON Analytics_Statistics.StatisticsID = Analytics_WeekHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) AS VARCHAR)+''%'' AS ''{$reports_screenresolution.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_WeekHits ON Analytics_Statistics.StatisticsID = Analytics_WeekHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 519, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext></querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '31e98907-d964-484d-974d-baec53876bbd', '20110907 11:14:38')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (346, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''hour'');
SELECT TOP 100
 StatisticsObjectName AS ''{$reports_screenresolution.name_header$}'',
 SUM(HitsCount) AS ''{$reports_screenresolution.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_HourHits ON Analytics_Statistics.StatisticsID = Analytics_HourHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) AS VARCHAR)+''%'' AS ''{$reports_screenresolution.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_HourHits ON Analytics_Statistics.StatisticsID = Analytics_HourHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 520, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext></querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '0dcdb07e-5bc1-41d9-babf-6ad18cbd8bb6', '20110907 11:14:55')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (347, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day'');
SELECT TOP 100
 StatisticsObjectName AS ''{$reports_screenresolution.name_header$}'',
 SUM(HitsCount) AS ''{$reports_screenresolution.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_DayHits ON Analytics_Statistics.StatisticsID = Analytics_DayHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) AS VARCHAR)+''%'' AS ''{$reports_screenresolution.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_DayHits ON Analytics_Statistics.StatisticsID = Analytics_DayHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 521, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext></querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'bfe0831f-5792-4b82-ae3f-e30e9cd77e68', '20110907 11:15:05')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (348, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''year'');
SELECT TOP 100
 StatisticsObjectName AS ''{$reports_screencolors.name_header$}'',
 SUM(HitsCount) AS ''{$reports_screencolors.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_YearHits ON Analytics_Statistics.StatisticsID = Analytics_YearHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) AS VARCHAR)+''%'' AS ''{$reports_screencolors.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_YearHits ON Analytics_Statistics.StatisticsID = Analytics_YearHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 522, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext></querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '585de056-96bc-4ff6-aee9-53b328e4f114', '20110907 11:15:21')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (349, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''month'');
SELECT TOP 100
 StatisticsObjectName AS ''{$reports_screencolors.name_header$}'',
 SUM(HitsCount) AS ''{$reports_screencolors.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_Statistics.StatisticsID = Analytics_MonthHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) AS VARCHAR)+''%'' AS ''{$reports_screencolors.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_Statistics.StatisticsID = Analytics_MonthHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 523, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext></querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'b17280af-8915-4c11-acc2-32f27a2f6767', '20110907 11:15:38')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (350, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''week'');
SELECT TOP 100
 StatisticsObjectName AS ''{$reports_screencolors.name_header$}'',
 SUM(HitsCount) AS ''{$reports_screencolors.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_WeekHits ON Analytics_Statistics.StatisticsID = Analytics_WeekHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) AS VARCHAR)+''%'' AS ''{$reports_screencolors.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_WeekHits ON Analytics_Statistics.StatisticsID = Analytics_WeekHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 524, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext></querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '2ff47b87-7ec2-4509-96e6-67b387f1ec97', '20110907 11:15:29')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (351, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day'');
SELECT TOP 100
 StatisticsObjectName AS ''{$reports_screencolors.name_header$}'',
 SUM(HitsCount) AS ''{$reports_screencolors.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_DayHits ON Analytics_Statistics.StatisticsID = Analytics_DayHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) AS VARCHAR)+''%'' AS ''{$reports_screencolors.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_DayHits ON Analytics_Statistics.StatisticsID = Analytics_DayHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 525, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext></querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'd8865ada-c680-45c8-8d25-61765119284d', '20110907 11:15:55')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (352, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''hour'');
SELECT TOP 100
 StatisticsObjectName AS ''{$reports_screencolors.name_header$}'',
 SUM(HitsCount) AS ''{$reports_screencolors.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_HourHits ON Analytics_Statistics.StatisticsID = Analytics_HourHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) AS VARCHAR)+''%'' AS ''{$reports_screencolors.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_HourHits ON Analytics_Statistics.StatisticsID = Analytics_HourHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 526, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext></querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'a42748aa-e8f4-4d88-9403-3cd280a5fbdf', '20110907 11:15:46')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (353, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''year'');
SELECT TOP 100
 StatisticsObjectName AS ''{$reports_operatingsystem.name_header$}'',
 SUM(HitsCount) AS ''{$reports_operatingsystem.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_YearHits ON Analytics_Statistics.StatisticsID = Analytics_YearHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) AS VARCHAR)+''%'' AS ''{$reports_operatingsystem.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_YearHits ON Analytics_Statistics.StatisticsID = Analytics_YearHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 527, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext></querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '025b3837-a3c7-4cd1-b3a1-7fc2733dc665', '20110907 11:16:05')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (354, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''month'');
SELECT TOP 100
 StatisticsObjectName AS ''{$reports_operatingsystem.name_header$}'',
 SUM(HitsCount) AS ''{$reports_operatingsystem.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_Statistics.StatisticsID = Analytics_MonthHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) AS VARCHAR)+''%'' AS ''{$reports_operatingsystem.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_Statistics.StatisticsID = Analytics_MonthHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 528, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext></querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '9e261f4b-4edc-4aa7-ad4a-8552b28a0da1', '20110907 11:16:25')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (355, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''week'');
SELECT TOP 100
 StatisticsObjectName AS ''{$reports_operatingsystem.name_header$}'',
 SUM(HitsCount) AS ''{$reports_operatingsystem.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_WeekHits ON Analytics_Statistics.StatisticsID = Analytics_WeekHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) AS VARCHAR)+''%'' AS ''{$reports_operatingsystem.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_WeekHits ON Analytics_Statistics.StatisticsID = Analytics_WeekHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 529, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext></querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'c767dfc8-26f5-47dc-93f0-679363255a5e', '20110907 11:16:15')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (356, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day'');
SELECT TOP 100
 StatisticsObjectName AS ''{$reports_operatingsystem.name_header$}'',
 SUM(HitsCount) AS ''{$reports_operatingsystem.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_DayHits ON Analytics_Statistics.StatisticsID = Analytics_DayHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) AS VARCHAR)+''%'' AS ''{$reports_operatingsystem.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_DayHits ON Analytics_Statistics.StatisticsID = Analytics_DayHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 530, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext></querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '85088e71-7fce-41cc-83ba-ef281a594ef9', '20110907 11:16:42')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (357, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''hour'');
SELECT TOP 100
 StatisticsObjectName AS ''{$reports_operatingsystem.name_header$}'',
 SUM(HitsCount) AS ''{$reports_operatingsystem.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_HourHits ON Analytics_Statistics.StatisticsID = Analytics_HourHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) AS VARCHAR)+''%'' AS ''{$reports_operatingsystem.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_HourHits ON Analytics_Statistics.StatisticsID = Analytics_HourHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 531, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext></querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'a0444a29-1d80-437f-afce-b2619de92a9d', '20110907 11:16:33')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (358, N'table', N'table', N'DECLARE @PaveViews TABLE
(
  DocumentNamePath NVARCHAR(500),
  Pageviews INT,
  Percents DECIMAL(10,2)  
)
DECLARE @Sum DECIMAL;
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''year'');
SELECT @Sum =   
  SUM(HitsCount) 
  FROM Analytics_Statistics
  INNER JOIN Analytics_YearHits ON Analytics_YearHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND DocumentCulture = StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
 AND (HitsEndTime <= @ToDate)
INSERT INTO @PaveViews (DocumentNamePath,pageViews,percents)
SELECT TOP 100
 CASE
  WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName
  ELSE DocumentNamePath
 END , SUM(HitsCount), (SUM(HitsCount)/@Sum)*100
 FROM Analytics_Statistics
 INNER JOIN Analytics_YearHits ON Analytics_YearHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
 LEFT JOIN View_CMS_Tree_Joined ON Analytics_Statistics.StatisticsObjectID = View_CMS_Tree_Joined.NodeID AND DocumentCulture = StatisticsObjectCulture
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
 AND (StatisticsCode=@CodeName)
 GROUP BY DocumentNamePath, StatisticsObjectName
 ORDER BY SUM(HitsCount) DESC
 
  
 SELECT DocumentNamePath AS ''{$reports_landingpage_Year.path_header$}'',pageviews AS  ''{$reports_landingpage_Year.hits_header$}'',
   CAST (Percents AS NVARCHAR(10))+''%'' AS ''{$reports_landingpage_Year.hits_percent_header$}''
   FROM @PaveViews ORDER BY PageViews DESC', 0, 532, N'<customdata><querynorecordtext></querynorecordtext><exportenabled>True</exportenabled><pagesize>10</pagesize><enablepaging>False</enablepaging><skinid>ReportGridAnalytics</skinid><pagemode>1</pagemode></customdata>', '4cff183e-d882-432b-8318-aa94fa4ed7fa', '20110920 12:18:26')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (359, N'table', N'table', N'DECLARE @PaveViews TABLE
(
  DocumentNamePath NVARCHAR(500),
  Pageviews INT,
  Percents DECIMAL(10,2)  
)
DECLARE @Sum DECIMAL;
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''month'');
SELECT @Sum =   
  SUM(HitsCount) 
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_MonthHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND DocumentCulture = StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
 AND (HitsEndTime <= @ToDate)
INSERT INTO @PaveViews (DocumentNamePath,pageViews,percents)
SELECT TOP 100
 CASE
  WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName
  ELSE DocumentNamePath
 END , SUM(HitsCount), (SUM(HitsCount)/@Sum)*100
 FROM Analytics_Statistics
 INNER JOIN Analytics_MonthHits ON Analytics_MonthHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
 LEFT JOIN View_CMS_Tree_Joined ON Analytics_Statistics.StatisticsObjectID = View_CMS_Tree_Joined.NodeID AND DocumentCulture = StatisticsObjectCulture
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
 AND (StatisticsCode=@CodeName)
 GROUP BY DocumentNamePath, StatisticsObjectName
 ORDER BY SUM(HitsCount) DESC
 
  
 SELECT DocumentNamePath AS ''{$reports_landingpage_Month.path_header$}'',pageviews AS  ''{$reports_landingpage_Month.hits_header$}'',
   CAST (Percents AS NVARCHAR(10))+''%'' AS ''{$reports_landingpage_Month.hits_percent_header$}''
   FROM @PaveViews ORDER BY PageViews DESC', 0, 533, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><pagesize>10</pagesize><enablepaging>False</enablepaging><skinid>ReportGridAnalytics</skinid><pagemode>1</pagemode></customdata>', '82672152-37eb-497a-ae68-f2ba7ca0d788', '20110920 12:18:26')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (360, N'table', N'table', N'DECLARE @PaveViews TABLE
(
  DocumentNamePath NVARCHAR(500),
  Pageviews INT,
  Percents DECIMAL(10,2)  
)
DECLARE @Sum DECIMAL;
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''week'');
SELECT @Sum =   
  SUM(HitsCount) 
  FROM Analytics_Statistics
  INNER JOIN Analytics_WeekHits ON Analytics_WeekHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND DocumentCulture = StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
 AND (HitsEndTime <= @ToDate)
INSERT INTO @PaveViews (DocumentNamePath,pageViews,percents)
SELECT TOP 100
 CASE
  WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName
  ELSE DocumentNamePath
 END , SUM(HitsCount), (SUM(HitsCount)/@Sum)*100
 FROM Analytics_Statistics
 INNER JOIN Analytics_WeekHits ON Analytics_WeekHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
 LEFT JOIN View_CMS_Tree_Joined ON Analytics_Statistics.StatisticsObjectID = View_CMS_Tree_Joined.NodeID AND DocumentCulture = StatisticsObjectCulture
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
 AND (StatisticsCode=@CodeName)
 GROUP BY DocumentNamePath, StatisticsObjectName
 ORDER BY SUM(HitsCount) DESC
 
  
 SELECT DocumentNamePath AS ''{$reports_landingpage_Week.path_header$}'',pageviews AS  ''{$reports_landingpage_Week.hits_header$}'',
   CAST (Percents AS NVARCHAR(10))+''%'' AS ''{$reports_landingpage_Week.hits_percent_header$}''
   FROM @PaveViews ORDER BY PageViews DESC', 0, 534, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><pagesize>10</pagesize><enablepaging>False</enablepaging><skinid>ReportGridAnalytics</skinid><pagemode>1</pagemode></customdata>', '9c150fce-622e-415d-9f08-0a0607cf00e2', '20110920 12:18:26')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (361, N'table', N'table', N'DECLARE @PaveViews TABLE
(
  DocumentNamePath NVARCHAR(500),
  Pageviews INT,
  Percents DECIMAL(10,2)  
)
DECLARE @Sum DECIMAL;
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day'');
SELECT @Sum =   
  SUM(HitsCount) 
  FROM Analytics_Statistics
  INNER JOIN Analytics_DayHits ON Analytics_DayHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND DocumentCulture = StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
 AND (HitsEndTime <= @ToDate)
INSERT INTO @PaveViews (DocumentNamePath,pageViews,percents)
SELECT TOP 100
 CASE
  WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName
  ELSE DocumentNamePath
 END , SUM(HitsCount), (SUM(HitsCount)/@Sum)*100
 FROM Analytics_Statistics
 INNER JOIN Analytics_DayHits ON Analytics_DayHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
 LEFT JOIN View_CMS_Tree_Joined ON Analytics_Statistics.StatisticsObjectID = View_CMS_Tree_Joined.NodeID AND DocumentCulture = StatisticsObjectCulture
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
 AND (StatisticsCode=@CodeName)
 GROUP BY DocumentNamePath, StatisticsObjectName
 ORDER BY SUM(HitsCount) DESC
 
  
 SELECT DocumentNamePath AS ''{$reports_landingpage_Day.path_header$}'',pageviews AS  ''{$reports_landingpage_Day.hits_header$}'',
   CAST (Percents AS NVARCHAR(10))+''%'' AS ''{$reports_landingpage_Day.hits_percent_header$}''
   FROM @PaveViews ORDER BY PageViews DESC', 0, 535, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><pagesize>10</pagesize><enablepaging>False</enablepaging><skinid>ReportGridAnalytics</skinid><pagemode>1</pagemode></customdata>', '78f4d473-d14f-49bc-9798-d9e24b2eeb0c', '20110920 12:18:26')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (362, N'table', N'table', N'DECLARE @PaveViews TABLE
(
  DocumentNamePath NVARCHAR(500),
  Pageviews INT,
  Percents DECIMAL(10,2)  
)
DECLARE @Sum DECIMAL;
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''hour'');
SELECT @Sum =   
  SUM(HitsCount) 
  FROM Analytics_Statistics
  INNER JOIN Analytics_HourHits ON Analytics_HourHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND DocumentCulture = StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
 AND (HitsEndTime <= @ToDate)
INSERT INTO @PaveViews (DocumentNamePath,pageViews,percents)
SELECT TOP 100
 CASE
  WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName
  ELSE DocumentNamePath
 END , SUM(HitsCount), (SUM(HitsCount)/@Sum)*100
 FROM Analytics_Statistics
 INNER JOIN Analytics_HourHits ON Analytics_HourHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
 LEFT JOIN View_CMS_Tree_Joined ON Analytics_Statistics.StatisticsObjectID = View_CMS_Tree_Joined.NodeID AND DocumentCulture = StatisticsObjectCulture
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
 AND (StatisticsCode=@CodeName)
 GROUP BY DocumentNamePath, StatisticsObjectName
 ORDER BY SUM(HitsCount) DESC
 
  
 SELECT DocumentNamePath AS ''{$reports_landingpage_Hour.path_header$}'',pageviews AS  ''{$reports_landingpage_Hour.hits_header$}'',
   CAST (Percents AS NVARCHAR(10))+''%'' AS ''{$reports_landingpage_Hour.hits_percent_header$}''
   FROM @PaveViews ORDER BY PageViews DESC', 0, 536, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><pagesize>10</pagesize><enablepaging>False</enablepaging><skinid>ReportGridAnalytics</skinid><pagemode>1</pagemode></customdata>', 'd7c8fc90-1907-4dae-a75e-8a6649dda1ec', '20110920 12:18:27')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (363, N'table', N'table', N'DECLARE @PaveViews TABLE
(
  DocumentNamePath NVARCHAR(500),
  Pageviews INT,
  Percents DECIMAL(10,2)  
)
DECLARE @Sum DECIMAL;
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''year'');
SELECT @Sum =   
  SUM(HitsCount) 
  FROM Analytics_Statistics
  INNER JOIN Analytics_YearHits ON Analytics_YearHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID  AND StatisticsObjectCulture = DocumentCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
 AND (HitsEndTime <= @ToDate)
INSERT INTO @PaveViews (DocumentNamePath,pageViews,percents)
SELECT TOP 100
 CASE
  WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName
  ELSE DocumentNamePath
 END , SUM(HitsCount), (SUM(HitsCount)/@Sum)*100
 FROM Analytics_Statistics
 INNER JOIN Analytics_YearHits ON Analytics_YearHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
 LEFT JOIN View_CMS_Tree_Joined ON Analytics_Statistics.StatisticsObjectID = View_CMS_Tree_Joined.NodeID AND StatisticsObjectCulture = DocumentCulture
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
 AND (StatisticsCode=@CodeName)
 GROUP BY DocumentNamePath, StatisticsObjectName
 ORDER BY Sum(HitsCount) DESC
 
  
 SELECT DocumentNamePath AS ''{$reports_exitpage_Year.path_header$}'',pageviews AS  ''{$reports_exitpage_Year.hits_header$}'',
   CAST (Percents AS NVARCHAR(10))+''%'' AS ''{$reports_exitpage_Year.hits_percent_header$}''
   FROM @PaveViews ORDER BY PageViews DESC', 0, 537, N'<customdata><querynorecordtext></querynorecordtext><exportenabled>true</exportenabled><pagesize>10</pagesize><enablepaging>False</enablepaging><skinid>ReportGridAnalytics</skinid><pagemode>1</pagemode></customdata>', 'a9534e33-cc1c-4f30-907e-9c40af12f3e2', '20110920 12:19:59')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (364, N'table', N'table', N'DECLARE @PaveViews TABLE
(
  DocumentNamePath NVARCHAR(500),
  Pageviews INT,
  Percents DECIMAL(10,2)  
)
DECLARE @Sum DECIMAL;
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''month'');
SELECT @Sum =   
  SUM(HitsCount) 
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_MonthHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID  AND StatisticsObjectCulture = DocumentCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
 AND (HitsEndTime <= @ToDate)
INSERT INTO @PaveViews (DocumentNamePath,pageViews,percents)
SELECT TOP 100
 CASE
  WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName
  ELSE DocumentNamePath
 END , SUM(HitsCount), (SUM(HitsCount)/@Sum)*100
 FROM Analytics_Statistics
 INNER JOIN Analytics_MonthHits ON Analytics_MonthHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
 LEFT JOIN View_CMS_Tree_Joined ON Analytics_Statistics.StatisticsObjectID = View_CMS_Tree_Joined.NodeID AND StatisticsObjectCulture = DocumentCulture
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
 AND (StatisticsCode=@CodeName)
 GROUP BY DocumentNamePath, StatisticsObjectName
 ORDER BY Sum(HitsCount) DESC
 
  
 SELECT DocumentNamePath AS ''{$reports_exitpage_Month.path_header$}'',pageviews AS  ''{$reports_exitpage_Month.hits_header$}'',
   CAST (Percents AS NVARCHAR(10))+''%'' AS ''{$reports_exitpage_Month.hits_percent_header$}''
   FROM @PaveViews ORDER BY PageViews DESC', 0, 538, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><pagesize>10</pagesize><enablepaging>False</enablepaging><skinid>ReportGridAnalytics</skinid><pagemode>1</pagemode></customdata>', '0341b692-f3ca-475f-9e1c-2a231c393cbc', '20110920 12:19:59')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (365, N'table', N'table', N'DECLARE @PaveViews TABLE
(
  DocumentNamePath NVARCHAR(500),
  Pageviews INT,
  Percents DECIMAL(10,2)  
)
DECLARE @Sum DECIMAL;
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''week'');
SELECT @Sum =   
  SUM(HitsCount) 
  FROM Analytics_Statistics
  INNER JOIN Analytics_WeekHits ON Analytics_WeekHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID  AND StatisticsObjectCulture = DocumentCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
 AND (HitsEndTime <= @ToDate)
INSERT INTO @PaveViews (DocumentNamePath,pageViews,percents)
SELECT TOP 100
 CASE
  WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName
  ELSE DocumentNamePath
 END , SUM(HitsCount), (SUM(HitsCount)/@Sum)*100
 FROM Analytics_Statistics
 INNER JOIN Analytics_WeekHits ON Analytics_WeekHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
 LEFT JOIN View_CMS_Tree_Joined ON Analytics_Statistics.StatisticsObjectID = View_CMS_Tree_Joined.NodeID AND StatisticsObjectCulture = DocumentCulture
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
 AND (StatisticsCode=@CodeName)
 GROUP BY DocumentNamePath, StatisticsObjectName
 ORDER BY Sum(HitsCount) DESC
 
  
 SELECT DocumentNamePath AS ''{$reports_exitpage_Week.path_header$}'',pageviews AS  ''{$reports_exitpage_Week.hits_header$}'',
   CAST (Percents AS NVARCHAR(10))+''%'' AS ''{$reports_exitpage_Week.hits_percent_header$}''
   FROM @PaveViews ORDER BY PageViews DESC', 0, 539, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><pagesize>10</pagesize><enablepaging>False</enablepaging><skinid>ReportGridAnalytics</skinid><pagemode>1</pagemode></customdata>', '23fe90b2-8035-4f1e-b36f-5f153383bc05', '20110920 12:19:59')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (366, N'table', N'table', N'DECLARE @PaveViews TABLE
(
  DocumentNamePath NVARCHAR(500),
  Pageviews INT,
  Percents DECIMAL(10,2)  
)
DECLARE @Sum DECIMAL;
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day'');
SELECT @Sum =   
  SUM(HitsCount) 
  FROM Analytics_Statistics
  INNER JOIN Analytics_DayHits ON Analytics_DayHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID  AND StatisticsObjectCulture = DocumentCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
 AND (HitsEndTime <= @ToDate)
INSERT INTO @PaveViews (DocumentNamePath,pageViews,percents)
SELECT TOP 100
 CASE
  WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName
  ELSE DocumentNamePath
 END , SUM(HitsCount), (SUM(HitsCount)/@Sum)*100
 FROM Analytics_Statistics
 INNER JOIN Analytics_DayHits ON Analytics_DayHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
 LEFT JOIN View_CMS_Tree_Joined ON Analytics_Statistics.StatisticsObjectID = View_CMS_Tree_Joined.NodeID AND StatisticsObjectCulture = DocumentCulture
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
 AND (StatisticsCode=@CodeName)
 GROUP BY DocumentNamePath, StatisticsObjectName
 ORDER BY Sum(HitsCount) DESC
 
  
 SELECT DocumentNamePath AS ''{$reports_exitpage_Day.path_header$}'',pageviews AS  ''{$reports_exitpage_Day.hits_header$}'',
   CAST (Percents AS NVARCHAR(10))+''%'' AS ''{$reports_exitpage_Day.hits_percent_header$}''
   FROM @PaveViews ORDER BY PageViews DESC', 0, 540, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><pagesize>10</pagesize><enablepaging>False</enablepaging><skinid>ReportGridAnalytics</skinid><pagemode>1</pagemode></customdata>', '7eb6d3ce-ed0f-42c1-9bef-e32aa21d5a23', '20110920 12:19:59')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (367, N'table', N'table', N'DECLARE @PaveViews TABLE
(
  DocumentNamePath NVARCHAR(500),
  Pageviews INT,
  Percents DECIMAL(10,2)  
)
DECLARE @Sum DECIMAL;
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''hour'');
SELECT @Sum =   
  SUM(HitsCount) 
  FROM Analytics_Statistics
  INNER JOIN Analytics_HourHits ON Analytics_HourHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID  AND StatisticsObjectCulture = DocumentCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
 AND (HitsEndTime <= @ToDate)
INSERT INTO @PaveViews (DocumentNamePath,pageViews,percents)
SELECT TOP 100
 CASE
  WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName
  ELSE DocumentNamePath
 END , SUM(HitsCount), (SUM(HitsCount)/@Sum)*100
 FROM Analytics_Statistics
 INNER JOIN Analytics_HourHits ON Analytics_HourHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
 LEFT JOIN View_CMS_Tree_Joined ON Analytics_Statistics.StatisticsObjectID = View_CMS_Tree_Joined.NodeID AND StatisticsObjectCulture = DocumentCulture
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
 AND (StatisticsCode=@CodeName)
 GROUP BY DocumentNamePath, StatisticsObjectName
 ORDER BY Sum(HitsCount) DESC
 
  
 SELECT DocumentNamePath AS ''{$reports_exitpage_Hour.path_header$}'',pageviews AS  ''{$reports_exitpage_Hour.hits_header$}'',
   CAST (Percents AS NVARCHAR(10))+''%'' AS ''{$reports_exitpage_Hour.hits_percent_header$}''
   FROM @PaveViews ORDER BY PageViews DESC', 0, 541, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><pagesize>10</pagesize><enablepaging>False</enablepaging><skinid>ReportGridAnalytics</skinid><pagemode>1</pagemode></customdata>', '093d4c18-6af6-412c-bd81-ef30b0baff6f', '20110920 12:19:59')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (368, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''year'');
SELECT TOP 100
StatisticsObjectName  AS ''{$reports_referringsites.name_header$}'',
 SUM(HitsCount) AS ''{$reports_referringsites.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_YearHits ON Analytics_Statistics.StatisticsID = Analytics_YearHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) AS VARCHAR)+''%'' AS ''{$reports_referringsites.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_YearHits ON Analytics_Statistics.StatisticsID = Analytics_YearHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 548, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'be8a2a86-c917-4145-bbe9-e9e2c8659657', '20110621 11:09:35')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (369, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''month'');
SELECT TOP 100
StatisticsObjectName  AS ''{$reports_referringsites.name_header$}'',
 SUM(HitsCount) AS ''{$reports_referringsites.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_Statistics.StatisticsID = Analytics_MonthHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) AS VARCHAR)+''%'' AS ''{$reports_referringsites.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_Statistics.StatisticsID = Analytics_MonthHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 550, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'dad43456-2a19-4930-9494-6a7b07b8b214', '20110621 11:09:36')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (370, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''week'');
SELECT TOP 100
StatisticsObjectName  AS ''{$reports_referringsites.name_header$}'',
 SUM(HitsCount) AS ''{$reports_referringsites.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_WeekHits ON Analytics_Statistics.StatisticsID = Analytics_WeekHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) AS VARCHAR)+''%'' AS ''{$reports_referringsites.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_WeekHits ON Analytics_Statistics.StatisticsID = Analytics_WeekHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 551, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '3390cd84-0e23-4636-ba11-1c748f4b5da3', '20110621 11:09:36')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (371, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day'');
SELECT TOP 100
StatisticsObjectName  AS ''{$reports_referringsites.name_header$}'',
 SUM(HitsCount) AS ''{$reports_referringsites.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_DayHits ON Analytics_Statistics.StatisticsID = Analytics_DayHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) AS VARCHAR)+''%'' AS ''{$reports_referringsites.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_DayHits ON Analytics_Statistics.StatisticsID = Analytics_DayHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 552, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '7159fcba-281c-4a9f-b8d4-e29456d832e4', '20110621 11:09:36')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (372, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''hour'');
SELECT TOP 100
StatisticsObjectName  AS ''{$reports_referringsites.name_header$}'',
 SUM(HitsCount) AS ''{$reports_referringsites.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_HourHits ON Analytics_Statistics.StatisticsID = Analytics_HourHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) AS VARCHAR)+''%'' AS ''{$reports_referringsites.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_HourHits ON Analytics_Statistics.StatisticsID = Analytics_HourHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 553, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '34e6f8c6-e73c-4b8d-9452-0597934c81e3', '20110621 11:09:36')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (373, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''year'');
SELECT TOP 100
StatisticsObjectName  AS ''{$reports_searchengine.name_header$}'',
 SUM(HitsCount) AS ''{$reports_searchengine.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_YearHits ON Analytics_Statistics.StatisticsID = Analytics_YearHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) AS VARCHAR)+''%'' AS ''{$reports_searchengine.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_YearHits ON Analytics_Statistics.StatisticsID = Analytics_YearHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 554, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '78a1aa84-eec3-493a-bafc-4152b6bb30c2', '20110621 11:09:36')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (374, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''month'');
SELECT TOP 100
StatisticsObjectName  AS ''{$reports_searchengine.name_header$}'',
 SUM(HitsCount) AS ''{$reports_searchengine.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_Statistics.StatisticsID = Analytics_MonthHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) AS VARCHAR)+''%'' AS ''{$reports_searchengine.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_Statistics.StatisticsID = Analytics_MonthHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 555, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'd53ae7e4-920a-4987-b08b-474f8978f318', '20110621 11:09:36')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (375, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''week'');
SELECT TOP 100
StatisticsObjectName  AS ''{$reports_searchengine.name_header$}'',
 SUM(HitsCount) AS ''{$reports_searchengine.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_WeekHits ON Analytics_Statistics.StatisticsID = Analytics_WeekHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) AS VARCHAR)+''%'' AS ''{$reports_searchengine.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_WeekHits ON Analytics_Statistics.StatisticsID = Analytics_WeekHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 556, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'ee466862-6c26-47ee-bf93-1a1c71907b8d', '20110621 11:09:36')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (376, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day'');
SELECT TOP 100
StatisticsObjectName  AS ''{$reports_searchengine.name_header$}'',
 SUM(HitsCount) AS ''{$reports_searchengine.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_DayHits ON Analytics_Statistics.StatisticsID = Analytics_DayHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) AS VARCHAR)+''%'' AS ''{$reports_searchengine.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_DayHits ON Analytics_Statistics.StatisticsID = Analytics_DayHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 557, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '9db67c9d-5413-4859-abca-424ac7d5f60f', '20110621 11:09:36')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (378, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''hour'');
SELECT TOP 100
StatisticsObjectName  AS ''{$reports_searchengine.name_header$}'',
 SUM(HitsCount) AS ''{$reports_searchengine.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_HourHits ON Analytics_Statistics.StatisticsID = Analytics_HourHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(5,2)) AS VARCHAR)+''%'' AS ''{$reports_searchengine.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_HourHits ON Analytics_Statistics.StatisticsID = Analytics_HourHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 559, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '0dff79fa-2e7b-4e80-9583-f5d08e454de5', '20110621 11:09:36')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (380, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''year'');
SELECT TOP 100
StatisticsObjectName  AS ''{$reports_searchkeyword.name_header$}'',
 SUM(HitsCount) AS ''{$reports_searchkeyword.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_YearHits ON Analytics_Statistics.StatisticsID = Analytics_YearHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(3)) AS VARCHAR)+''%'' AS ''{$reports_searchkeyword.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_YearHits ON Analytics_Statistics.StatisticsID = Analytics_YearHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 561, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'eee97e8c-5a86-444f-8ca4-3a1b132b0574', '20110621 11:09:36')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (381, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''week'');
SELECT TOP 100
StatisticsObjectName  AS ''{$reports_searchkeyword.name_header$}'',
 SUM(HitsCount) AS ''{$reports_searchkeyword.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_WeekHits ON Analytics_Statistics.StatisticsID = Analytics_WeekHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(3)) AS VARCHAR)+''%'' AS ''{$reports_searchkeyword.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_WeekHits ON Analytics_Statistics.StatisticsID = Analytics_WeekHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 562, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '73532c98-8016-4b05-9056-f7615ea0a221', '20110621 11:09:36')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (382, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day'');
SELECT TOP 100
StatisticsObjectName  AS ''{$reports_searchkeyword.name_header$}'',
 SUM(HitsCount) AS ''{$reports_searchkeyword.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_DayHits ON Analytics_Statistics.StatisticsID = Analytics_DayHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(3)) AS VARCHAR)+''%'' AS ''{$reports_searchkeyword.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_DayHits ON Analytics_Statistics.StatisticsID = Analytics_DayHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 563, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '3bfd68b8-491a-4f7c-9691-c15bf0a11a44', '20110621 11:09:36')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (383, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''hour'');
SELECT TOP 100
StatisticsObjectName  AS ''{$reports_searchkeyword.name_header$}'',
 SUM(HitsCount) AS ''{$reports_searchkeyword.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_HourHits ON Analytics_Statistics.StatisticsID = Analytics_HourHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(3)) AS VARCHAR)+''%'' AS ''{$reports_searchkeyword.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_HourHits ON Analytics_Statistics.StatisticsID = Analytics_HourHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 564, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '1e905917-817c-40fa-bea7-d6c6199b5616', '20110621 11:09:36')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (384, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''month'');
SELECT TOP 100
StatisticsObjectName  AS ''{$reports_searchkeyword.name_header$}'',
 SUM(HitsCount) AS ''{$reports_searchkeyword.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_Statistics.StatisticsID = Analytics_MonthHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(3)) AS VARCHAR)+''%'' AS ''{$reports_searchkeyword.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_Statistics.StatisticsID = Analytics_MonthHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 565, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '3a55bd6d-08fb-4bdc-8001-0e5f23436b86', '20110621 11:09:36')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (387, N'TableYearAggViewsCulture', N'table', N'SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''year'');
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
DECLARE @myselection TABLE (
   DocumentNamePath varchar(400),
   StatisticsObjectCulture varchar(400),
   Count float   
);
DECLARE @countedAll TABLE (
   Counted float   
);
INSERT INTO @myselection
  SELECT TOP 100
  CASE WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName ELSE DocumentNamePath END,
  StatisticsObjectCulture ,
  SUM(HitsCount) AS Count
  FROM Analytics_Statistics
  INNER JOIN Analytics_YearHits ON Analytics_YearHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND View_CMS_Tree_Joined.DocumentCulture = Analytics_Statistics.StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectCulture, DocumentNamePath, StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC
  
INSERT INTO @countedAll
  SELECT
  SUM(HitsCount) AS Count
  FROM Analytics_Statistics
  INNER JOIN Analytics_YearHits ON Analytics_YearHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND View_CMS_Tree_Joined.DocumentCulture = Analytics_Statistics.StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)   
 SELECT DocumentNamePath AS ''{$reports_exitpage_Year.path_header$}'',
 StatisticsObjectCulture AS   ''{$general.culture$}'',
 Count AS ''{$reports_exitpage_Year.hits_header$}'',
 CAST (CAST((100*Count)/(SELECT Counted FROM  @countedAll) as decimal(10,2)) AS NVARCHAR(10))+''%'' AS ''{$reports_exitpage_Year.hits_percent_header$}''
 FROM @myselection', 0, 568, N'<customdata><querynorecordtext></querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '1eb41744-e311-4678-bd1b-dcc3042af638', '20110621 11:09:36')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (388, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''month'');
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
DECLARE @myselection TABLE (
   DocumentNamePath varchar(400),
   StatisticsObjectCulture varchar(400),
   Count float   
);
DECLARE @countedAll TABLE (
   Counted float   
);
INSERT INTO @myselection 
  SELECT TOP 100 
  CASE WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName ELSE DocumentNamePath END, 
  StatisticsObjectCulture , 
  SUM(HitsCount) AS Count 
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_MonthHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND View_CMS_Tree_Joined.DocumentCulture = Analytics_Statistics.StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) 
  AND (StatisticsCode=@CodeName) 
  AND (HitsStartTime >= @FromDate) 
  AND (HitsEndTime <= @ToDate) 
  GROUP BY StatisticsObjectCulture, DocumentNamePath, StatisticsObjectName 
  ORDER BY SUM(HitsCount) DESC
  
INSERT INTO @countedAll 
  SELECT
  SUM(HitsCount) AS Count 
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_MonthHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND View_CMS_Tree_Joined.DocumentCulture = Analytics_Statistics.StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) 
  AND (StatisticsCode=@CodeName) 
  AND (HitsStartTime >= @FromDate) 
  AND (HitsEndTime <= @ToDate)   
 SELECT DocumentNamePath AS ''{$reports_exitpage_month.path_header$}'', 
 StatisticsObjectCulture AS   ''{$general.culture$}'', 
 Count AS ''{$reports_exitpage_month.hits_header$}'', 
 CAST (CAST((100*Count)/(SELECT Counted FROM  @countedAll) as decimal(10,2)) AS NVARCHAR(10))+''%'' AS ''{$reports_exitpage_month.hits_percent_header$}'' 
 FROM @myselection', 0, 569, N'<customdata><pagesize>10</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '715d5ab1-ddff-46e9-b47e-667c30d26568', '20110621 11:09:33')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (389, N'table', N'table', N'SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''week'');
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week'');
DECLARE @myselection TABLE (
   DocumentNamePath varchar(400),
   StatisticsObjectCulture varchar(400),
   Count float   
);
DECLARE @countedAll TABLE (
   Counted float   
);
INSERT INTO @myselection
  SELECT TOP 100
  CASE WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName ELSE DocumentNamePath END,
  StatisticsObjectCulture ,
  SUM(HitsCount) AS Count
  FROM Analytics_Statistics
  INNER JOIN Analytics_WeekHits ON Analytics_WeekHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND View_CMS_Tree_Joined.DocumentCulture = Analytics_Statistics.StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectCulture, DocumentNamePath, StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC
  
INSERT INTO @countedAll
  SELECT
  SUM(HitsCount) AS Count
  FROM Analytics_Statistics
  INNER JOIN Analytics_WeekHits ON Analytics_WeekHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND View_CMS_Tree_Joined.DocumentCulture = Analytics_Statistics.StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)   
 SELECT DocumentNamePath AS ''{$reports_exitpage_Week.path_header$}'',
 StatisticsObjectCulture AS   ''{$general.culture$}'',
 Count AS ''{$reports_exitpage_Week.hits_header$}'',
CAST (CAST((100*Count)/(SELECT Counted FROM  @countedAll) as decimal(10,2)) AS NVARCHAR(10))+''%'' AS ''{$reports_exitpage_Week.hits_percent_header$}''
 FROM @myselection', 0, 570, N'<customdata><querynorecordtext></querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'a6d7c9bb-8a2a-42f2-abd3-78f9fa873bde', '20110621 11:09:33')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (390, N'TableDayAggViewsCulture', N'table', N'SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day'');
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
DECLARE @myselection TABLE (
   DocumentNamePath varchar(400),
   StatisticsObjectCulture varchar(400),
   Count float   
);
DECLARE @countedAll TABLE (
   Counted float   
);
INSERT INTO @myselection 
  SELECT TOP 100 
  CASE WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName ELSE DocumentNamePath END AS ''{$reports_pageviews_day.path_header$}'',  
  StatisticsObjectCulture AS ''{$general.culture$}'', 
  SUM(HitsCount) AS ''{$reports_pageviews_day.hits_header$}'' 
  FROM Analytics_Statistics 
    INNER JOIN Analytics_DayHits ON Analytics_DayHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
    LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND Analytics_Statistics.StatisticsObjectCulture = View_CMS_Tree_Joined.DocumentCulture
    WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) 
  AND (StatisticsCode=@CodeName) 
  AND (HitsStartTime >= @FromDate) 
  AND (HitsEndTime <= @ToDate) 
  GROUP BY StatisticsObjectCulture, DocumentNamePath, StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC
  
INSERT INTO @countedAll 
  SELECT
  SUM(HitsCount) AS ''{$reports_pageviews_day.hits_header$}'' 
  FROM Analytics_Statistics 
    INNER JOIN Analytics_DayHits ON Analytics_DayHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
    LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND Analytics_Statistics.StatisticsObjectCulture = View_CMS_Tree_Joined.DocumentCulture
    WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) 
  AND (StatisticsCode=@CodeName) 
  AND (HitsStartTime >= @FromDate) 
  AND (HitsEndTime <= @ToDate)   
  
SELECT DocumentNamePath AS ''{$reports_exitpage_day.path_header$}'',  
StatisticsObjectCulture AS ''{$general.culture$}'', 
Count AS ''{$reports_exitpage_day.hits_header$}'',
CAST (CAST((100*Count)/(SELECT Counted FROM  @countedAll) as decimal(10,2)) AS NVARCHAR(10))+''%'' 
as ''{$reports_exitpage_day.hits_percent_header$}'' 
FROM @myselection', 0, 571, N'<customdata><pagesize>10</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '927188a8-c7ee-45c5-a1d5-cecb33a6b373', '20110621 11:09:33')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (391, N'table', N'table', N'SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''hour'');
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
DECLARE @myselection TABLE (
   DocumentNamePath varchar(400),
   StatisticsObjectCulture varchar(400),
   Count float   
);
DECLARE @countedAll TABLE (
   Counted float   
);
INSERT INTO @myselection 
  SELECT TOP 100 
  CASE WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName ELSE DocumentNamePath END, 
  StatisticsObjectCulture, SUM(HitsCount) 
  FROM Analytics_Statistics
    INNER JOIN Analytics_HourHits ON Analytics_HourHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
    LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND View_CMS_Tree_Joined.DocumentCulture = Analytics_Statistics.StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) 
  AND (StatisticsCode=@CodeName) 
  AND (HitsStartTime >= @FromDate) 
  AND (HitsEndTime <= @ToDate) 
  GROUP BY StatisticsObjectCulture, DocumentNamePath, StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC, StatisticsObjectCulture
INSERT INTO @countedAll 
  SELECT
  SUM(HitsCount) 
  FROM Analytics_Statistics
    INNER JOIN Analytics_HourHits ON Analytics_HourHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
    LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND View_CMS_Tree_Joined.DocumentCulture = Analytics_Statistics.StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) 
  AND (StatisticsCode=@CodeName) 
  AND (HitsStartTime >= @FromDate) 
  AND (HitsEndTime <= @ToDate)   
  
 SELECT DocumentNamePath AS ''{$reports_exitpage_hour.path_header$}'', 
 StatisticsObjectCulture AS ''{$general.culture$}'', 
 Count AS ''{$reports_exitpage_hour.hits_header$}'',
 CAST (CAST((100*Count)/(SELECT Counted FROM  @countedAll) as decimal(10,2)) AS NVARCHAR(10))+''%'' AS ''{$reports_exitpage_hour.hits_percent_header$}'' 
 FROM @myselection', 0, 572, N'<customdata><pagesize>10</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '19b4ca56-2c39-4d25-a265-856e7611ee00', '20110621 11:09:33')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (392, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''year'');
DECLARE @myselection TABLE (
   DocumentNamePath varchar(400),
   StatisticsObjectCulture varchar(400),
   Count float   
);
DECLARE @countedAll TABLE (
   Counted float   
);
INSERT INTO @myselection
  SELECT TOP 100
  CASE WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName ELSE DocumentNamePath END,
  StatisticsObjectCulture ,
  SUM(HitsCount) AS Count
  FROM Analytics_Statistics
  INNER JOIN Analytics_YearHits ON Analytics_YearHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND View_CMS_Tree_Joined.DocumentCulture = Analytics_Statistics.StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectCulture, DocumentNamePath, StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC
  
INSERT INTO @countedAll
  SELECT
  SUM(HitsCount) AS Counted
  FROM Analytics_Statistics
  INNER JOIN Analytics_YearHits ON Analytics_YearHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND View_CMS_Tree_Joined.DocumentCulture = Analytics_Statistics.StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)
 SELECT DocumentNamePath AS ''{$reports_landingpage_Year.path_header$}'',
 StatisticsObjectCulture AS   ''{$general.culture$}'',
 Count AS ''{$reports_landingpage_Year.hits_header$}'',
 CAST (CAST((100*Count)/(SELECT Counted FROM  @countedAll) as decimal(10,2)) AS NVARCHAR(10))+''%'' AS ''{$reports_landingpage_Year.hits_percent_header$}''
 FROM @myselection', 0, 573, N'<customdata><querynorecordtext></querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'e1bc97f9-1000-4201-a495-69d2caa44be7', '20110621 11:09:33')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (393, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''month'');
DECLARE @myselection TABLE (
   DocumentNamePath varchar(400),
   StatisticsObjectCulture varchar(400),
   Count float   
);
DECLARE @countedAll TABLE (
   Counted float   
);
INSERT INTO @myselection 
  SELECT TOP 100 
  CASE WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName ELSE DocumentNamePath END, 
  StatisticsObjectCulture , 
  SUM(HitsCount) AS Count 
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_MonthHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND View_CMS_Tree_Joined.DocumentCulture = Analytics_Statistics.StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) 
  AND (StatisticsCode=@CodeName) 
  AND (HitsStartTime >= @FromDate) 
  AND (HitsEndTime <= @ToDate) 
  GROUP BY StatisticsObjectCulture, DocumentNamePath, StatisticsObjectName 
  ORDER BY SUM(HitsCount) DESC
  
INSERT INTO @countedAll 
  SELECT
  SUM(HitsCount) AS Counted 
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_MonthHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND View_CMS_Tree_Joined.DocumentCulture = Analytics_Statistics.StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) 
  AND (StatisticsCode=@CodeName) 
  AND (HitsStartTime >= @FromDate) 
  AND (HitsEndTime <= @ToDate) 
 SELECT DocumentNamePath AS ''{$reports_landingpage_month.path_header$}'', 
 StatisticsObjectCulture AS   ''{$general.culture$}'', 
 Count AS ''{$reports_pageviews_month.hits_header$}'', 
 CAST (CAST((100*Count)/(SELECT Counted FROM  @countedAll) as decimal(10,2)) AS NVARCHAR(10))+''%'' AS ''{$reports_landingpage_month.hits_percent_header$}'' 
 FROM @myselection', 0, 574, N'<customdata><pagesize>10</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'b1baf31a-cb76-4695-885f-8ca64506589c', '20110621 11:09:33')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (394, N'TableYearPageViewsCulture', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''week'');
DECLARE @myselection TABLE (
   DocumentNamePath varchar(400),
   StatisticsObjectCulture varchar(400),
   Count float   
);
DECLARE @countedAll TABLE (
   Counted float   
);
INSERT INTO @myselection
  SELECT TOP 100
  CASE WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName ELSE DocumentNamePath END,
  StatisticsObjectCulture ,
  SUM(HitsCount) AS Count
  FROM Analytics_Statistics
  INNER JOIN Analytics_WeekHits ON Analytics_WeekHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND View_CMS_Tree_Joined.DocumentCulture = Analytics_Statistics.StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectCulture, DocumentNamePath, StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC
  
INSERT INTO @countedAll
  SELECT
  SUM(HitsCount) AS Counted
  FROM Analytics_Statistics
  INNER JOIN Analytics_WeekHits ON Analytics_WeekHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND View_CMS_Tree_Joined.DocumentCulture = Analytics_Statistics.StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)
 SELECT DocumentNamePath AS ''{$reports_landingpage_Week.path_header$}'',
 StatisticsObjectCulture AS   ''{$general.culture$}'',
 Count AS ''{$reports_landingpage_Week.hits_header$}'',
 CAST (CAST((100*Count)/(SELECT Counted FROM  @countedAll) as decimal(10,2)) AS NVARCHAR(10))+''%'' AS ''{$reports_landingpage_Week.hits_percent_header$}''
 FROM @myselection', 0, 575, N'<customdata><querynorecordtext></querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '35edb982-5c2d-4e20-a539-7a3d24825538', '20110621 11:09:33')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (395, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day'');
DECLARE @myselection TABLE (
   DocumentNamePath varchar(400),
   StatisticsObjectCulture varchar(400),
   Count float   
);
DECLARE @countedAll TABLE (
   Counted float   
);
INSERT INTO @myselection 
  SELECT TOP 100 
  CASE WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName ELSE DocumentNamePath END AS ''{$reports_pageviews_day.path_header$}'',  
  StatisticsObjectCulture AS ''{$general.culture$}'', 
  SUM(HitsCount) AS ''{$reports_landingpage_day.hits_header$}'' 
  FROM Analytics_Statistics 
    INNER JOIN Analytics_DayHits ON Analytics_DayHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
    LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND Analytics_Statistics.StatisticsObjectCulture = View_CMS_Tree_Joined.DocumentCulture
    WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) 
  AND (StatisticsCode=@CodeName) 
  AND (HitsStartTime >= @FromDate) 
  AND (HitsEndTime <= @ToDate) 
  GROUP BY StatisticsObjectCulture, DocumentNamePath, StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC
  
INSERT INTO @countedAll 
  SELECT
  SUM(HitsCount) AS ''{$reports_landingpage_day.hits_header$}'' 
  FROM Analytics_Statistics 
    INNER JOIN Analytics_DayHits ON Analytics_DayHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
    LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND Analytics_Statistics.StatisticsObjectCulture = View_CMS_Tree_Joined.DocumentCulture
    WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) 
  AND (StatisticsCode=@CodeName) 
  AND (HitsStartTime >= @FromDate) 
  AND (HitsEndTime <= @ToDate) 
SELECT DocumentNamePath AS ''{$reports_landingpage_day.path_header$}'',  
StatisticsObjectCulture AS ''{$general.culture$}'', 
Count AS ''{$reports_landingpage_day.hits_header$}'',
CAST (CAST((100*Count)/(SELECT Counted FROM  @countedAll) as decimal(10,2)) AS NVARCHAR(10))+''%'' AS ''{$reports_landingpage_day.hits_percent_header$}'' 
FROM @myselection', 0, 576, N'<customdata><pagesize>10</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'ede7838d-108e-47af-ae7f-5ae9cd134f89', '20110621 11:09:33')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (396, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''hour'');
DECLARE @myselection TABLE (
   DocumentNamePath varchar(400),
   StatisticsObjectCulture varchar(400),
   Count float   
);
DECLARE @countedAll TABLE (
   Counted float   
);
INSERT INTO @myselection 
  SELECT TOP 100 
  CASE WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName ELSE DocumentNamePath END, 
  StatisticsObjectCulture, SUM(HitsCount) 
  FROM Analytics_Statistics
    INNER JOIN Analytics_HourHits ON Analytics_HourHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
    LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND View_CMS_Tree_Joined.DocumentCulture = Analytics_Statistics.StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) 
  AND (StatisticsCode=@CodeName) 
  AND (HitsStartTime >= @FromDate) 
  AND (HitsEndTime <= @ToDate) 
  GROUP BY StatisticsObjectCulture, DocumentNamePath, StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC, StatisticsObjectCulture
  
INSERT INTO @countedAll 
  SELECT
  SUM(HitsCount) 
  FROM Analytics_Statistics
    INNER JOIN Analytics_HourHits ON Analytics_HourHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
    LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND View_CMS_Tree_Joined.DocumentCulture = Analytics_Statistics.StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) 
  AND (StatisticsCode=@CodeName) 
  AND (HitsStartTime >= @FromDate) 
  AND (HitsEndTime <= @ToDate)   
 SELECT DocumentNamePath AS ''{$reports_landingpage_hour.path_header$}'', 
 StatisticsObjectCulture AS ''{$general.culture$}'', 
 Count AS ''{$reports_landingpage_hour.hits_header$}'',
 CAST (CAST((100*Count)/(SELECT Counted FROM  @countedAll) as decimal(10,2)) AS NVARCHAR(10))+''%'' AS ''{$reports_landingpage_hour.hits_percent_header$}'' 
 FROM @myselection', 0, 577, N'<customdata><pagesize>10</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '9d8324e7-dfde-4633-b12a-ae14577b6f65', '20110621 11:09:34')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (397, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''year'');
SELECT TOP 100
StatisticsObjectName  AS ''{$reports_onsitesearchkeyword.name_header$}'',
 SUM(HitsCount) AS ''{$reports_onsitesearchkeyword.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_YearHits ON Analytics_Statistics.StatisticsID = Analytics_YearHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(3)) AS VARCHAR)+''%'' AS ''{$reports_onsitesearchkeyword.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_YearHits ON Analytics_Statistics.StatisticsID = Analytics_YearHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 578, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'fa45e458-7dc1-4c86-9df2-83b1808d5352', '20110621 11:09:34')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (398, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''week'');
SELECT TOP 100
StatisticsObjectName  AS ''{$reports_onsitesearchkeyword.name_header$}'',
 SUM(HitsCount) AS ''{$reports_onsitesearchkeyword.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_WeekHits ON Analytics_Statistics.StatisticsID = Analytics_WeekHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(3)) AS VARCHAR)+''%'' AS ''{$reports_onsitesearchkeyword.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_WeekHits ON Analytics_Statistics.StatisticsID = Analytics_WeekHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 579, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '64c46652-2883-4729-8d7e-ca08c83a11a4', '20110621 11:09:35')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (399, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''month'');
SELECT TOP 100
StatisticsObjectName  AS ''{$reports_onsitesearchkeyword.name_header$}'',
 SUM(HitsCount) AS ''{$reports_onsitesearchkeyword.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_Statistics.StatisticsID = Analytics_MonthHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(3)) AS VARCHAR)+''%'' AS ''{$reports_onsitesearchkeyword.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_Statistics.StatisticsID = Analytics_MonthHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 580, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '422c771c-5400-4f50-8fa2-5055cc87b9de', '20110621 11:09:35')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (400, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''hour'');
SELECT TOP 100
StatisticsObjectName  AS ''{$reports_onsitesearchkeyword.name_header$}'',
 SUM(HitsCount) AS ''{$reports_onsitesearchkeyword.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_HourHits ON Analytics_Statistics.StatisticsID = Analytics_HourHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(3)) AS VARCHAR)+''%'' AS ''{$reports_onsitesearchkeyword.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_HourHits ON Analytics_Statistics.StatisticsID = Analytics_HourHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 581, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '6d253012-50ed-40b9-a8ed-8a2715d6f32e', '20110621 11:09:35')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (401, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day'');
SELECT TOP 100
StatisticsObjectName  AS ''{$reports_onsitesearchkeyword.name_header$}'',
 SUM(HitsCount) AS ''{$reports_onsitesearchkeyword.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN Analytics_DayHits ON Analytics_Statistics.StatisticsID = Analytics_DayHits.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(3)) AS VARCHAR)+''%'' AS ''{$reports_onsitesearchkeyword.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN Analytics_DayHits ON Analytics_Statistics.StatisticsID = Analytics_DayHits.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 582, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '2a96b6eb-5372-482c-b3b5-f868c3994760', '20110621 11:09:35')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (405, N'TrafficSources', N'Traffic sources', N'DECLARE @Sum DECIMAL (10,2);
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''{%Interval|(default)year%}'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''{%Interval|(default)year%}'');
SELECT @Sum = 
SUM (HitsCount) FROM Analytics_Statistics 
LEFT JOIN {%AnalyticsTable|(default)Analytics_YearHits%} ON HitsStatisticsID = StatisticsID
WHERE 
((StatisticsCode = ''referringsite_direct'') OR (StatisticsCode = ''referringsite_search'')  OR (StatisticsCode = ''referringsite_referring'')  
OR (StatisticsCode = ''referringsite_inbound'')) 
AND ((HitsStartTime >=@FromDate) AND (HitsEndTime <=@ToDate) AND (StatisticsSiteID = @CMSContextCurrentSiteID)
AND (StatisticsObjectID = @NodeID) AND (StatisticsObjectCulture = @CultureCode))
SELECT ''{''+''$analytics_codename.'' + StatisticsCode + ''$}'' AS ''{$reporting.alltraficsources.columntitle$}'',SUM(HitsCount) AS ''{$general.views$}'' ,
CAST (CAST (SUM(HitsCount)/@Sum*100 AS DECIMAL(10,2)) AS NVARCHAR (20))+''%'' AS ''{$general.percent$}''
FROM Analytics_Statistics 
LEFT JOIN {%AnalyticsTable|(default)Analytics_YearHits%} ON HitsStatisticsID = StatisticsID
WHERE
((StatisticsCode = ''referringsite_direct'') OR (StatisticsCode = ''referringsite_search'')  OR (StatisticsCode = ''referringsite_referring'')OR (StatisticsCode = ''referringsite_inbound''))
 AND 
((HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate) AND (StatisticsSiteID = @CMSContextCurrentSiteID) AND (StatisticsObjectID = @NodeID) 
   AND (StatisticsObjectCulture = @CultureCode))
GROUP BY StatisticsCode', 0, 584, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'e9761837-0f7f-42eb-871d-dea5ad211206', '20110621 11:09:33')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (406, N'SearchEngines', N'Search engines', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''{%Interval|(default)Year%}'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''{%Interval|(default)Year%}'');
SELECT TOP 100
StatisticsObjectName  AS ''{$reports_searchengine.name_header$}'',
 SUM(HitsCount) AS ''{$reports_searchengine.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN {%AnalyticsTable|(default)Analytics_YearHits%} ON Analytics_Statistics.StatisticsID = {%AnalyticsTable|(default)Analytics_YearHits%}.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=''referringsite_search'')
  AND (HitsStartTime >= @FromDate)
  AND (StatisticsObjectID = @NodeID)
  AND (StatisticsObjectCulture= @CultureCode)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(3)) AS VARCHAR)+''%'' AS ''{$reports_searchengine.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN {%AnalyticsTable|(default)Analytics_YearHits%} ON Analytics_Statistics.StatisticsID = {%AnalyticsTable|(default)Analytics_YearHits%}.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=''referringsite_search'')
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate) AND (StatisticsObjectID = @NodeID)
  AND (StatisticsObjectCulture= @CultureCode)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 584, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '30ac278e-08a7-4082-a66b-49daed8378b5', '20110621 11:09:33')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (407, N'SearchKeywords', N'Search keywords', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''{%Interval|(default)Year%}'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''{%Interval|(default)Year%}'');
SELECT TOP 100
StatisticsObjectName  AS ''{$reports_searchkeyword.name_header$}'',
 SUM(HitsCount) AS ''{$reports_searchkeyword.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN {%AnalyticsTable|(default)Analytics_YearHits%} ON Analytics_Statistics.StatisticsID = {%AnalyticsTable|(default)Analytics_YearHits%}.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=''searchkeyword'')
  AND (HitsStartTime >= @FromDate)
  AND (StatisticsObjectID = @NodeID)
  AND (StatisticsObjectCulture= @CultureCode)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(3)) AS VARCHAR)+''%'' AS ''{$reports_searchkeyword.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN {%AnalyticsTable|(default)Analytics_YearHits%} ON Analytics_Statistics.StatisticsID = {%AnalyticsTable|(default)Analytics_YearHits%}.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=''searchkeyword'')
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate) AND (StatisticsObjectID = @NodeID)
  AND (StatisticsObjectCulture= @CultureCode)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 584, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'f73f438b-2b54-4d19-9328-9851bb929f57', '20110621 11:09:33')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (423, N'ReferringSites', N'Referring sites', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''{%Interval|(default)Year%}'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''{%Interval|(default)Year%}'');
SELECT TOP 100
StatisticsObjectName  AS ''{$general.sites$}'',
 SUM(HitsCount) AS ''{$reports_searchengine.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN {%AnalyticsTable|(default)Analytics_YearHits%} ON Analytics_Statistics.StatisticsID = {%AnalyticsTable|(default)Analytics_YearHits%}.HitsStatisticsID
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=''referringsite_referring'')
  AND (HitsStartTime >= @FromDate)
  AND (StatisticsObjectID = @NodeID)
  AND (StatisticsObjectCulture= @CultureCode)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(3)) AS VARCHAR)+''%'' AS ''{$reports_searchengine.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN {%AnalyticsTable|(default)Analytics_YearHits%} ON Analytics_Statistics.StatisticsID = {%AnalyticsTable|(default)Analytics_YearHits%}.HitsStatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=''referringsite_referring'')
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate) AND (StatisticsObjectID = @NodeID)
  AND (StatisticsObjectCulture= @CultureCode)
  GROUP BY StatisticsObjectName
  ORDER BY SUM(HitsCount) DESC', 0, 584, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '5cf4c63d-093a-4078-9d49-f82157d311c1', '20110621 11:09:33')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (424, N'LocalPages', N'Local pages', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''{%Interval|(default)Year%}'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''{%Interval|(default)Year%}'');
SELECT TOP 100
Tree.NodeAliasPath AS ''{$general.pages$}'',
 SUM(HitsCount) AS ''{$reports_searchkeyword.hits_header$}'',
 CAST(CAST(100*CAST(SUM(HitsCount) AS float)/NULLIF((
  SELECT SUM(HitsCount)
  FROM Analytics_Statistics
  INNER JOIN {%AnalyticsTable|(default)Analytics_YearHits%} ON Analytics_Statistics.StatisticsID = {%AnalyticsTable|(default)Analytics_YearHits%}.HitsStatisticsID
   RIGHT JOIN View_CMS_Tree_Joined AS Tree ON Analytics_Statistics.StatisticsObjectName = Tree.NodeID AND 
  Analytics_Statistics.StatisticsObjectCulture=Tree.DocumentCulture
  WHERE (StatisticsSiteID=@CMSContextCurrentSiteID)
  AND (StatisticsCode=''referringsite_local'')
  AND (HitsStartTime >= @FromDate)
  AND (StatisticsObjectID = @NodeID)
  AND (StatisticsObjectCulture= @CultureCode)
  AND (HitsEndTime <= @ToDate)), 0) AS decimal(3)) AS VARCHAR)+''%'' AS ''{$reports_searchkeyword.percent_header$}''
  FROM Analytics_Statistics
  INNER JOIN {%AnalyticsTable|(default)Analytics_YearHits%} ON Analytics_Statistics.StatisticsID = {%AnalyticsTable|(default)Analytics_YearHits%}.HitsStatisticsID
  RIGHT JOIN View_CMS_Tree_Joined AS Tree ON Analytics_Statistics.StatisticsObjectName = Tree.NodeID AND 
  Analytics_Statistics.StatisticsObjectCulture=Tree.DocumentCulture  
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=''referringsite_local'')
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate) AND (StatisticsObjectID = @NodeId)
  AND (StatisticsObjectCulture= @CultureCode)
  GROUP BY Tree.NodeAliasPath
  ORDER BY SUM(HitsCount) DESC', 0, 584, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', '1c6cbb77-12dd-41aa-b09f-5abefa676d3a', '20111005 10:36:01')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (436, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''week'');
SELECT TOP 100 CampaignDisplayName AS ''{$reports_campaign.name_header$}'', SUM(HitsCount) AS ''{$reports_campaign.hits_header$}'' FROM
Analytics_WeekHits, Analytics_Statistics 
INNER JOIN Analytics_Campaign ON StatisticsObjectName = CampaignName
WHERE (@CampaignName ='''' OR @CampaignName = CampaignName) AND
(StatisticsSiteID = @CMSContextCurrentSiteID) AND (StatisticsCode=@CodeName) AND (StatisticsID = HitsStatisticsID) AND 
(HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate) GROUP BY CampaignDisplayName ORDER BY SUM(HitsCount) DESC', 0, 609, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'a851c6cc-6318-40cb-a773-fea0e9f592ce', '20110621 11:09:31')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (437, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day'');
SELECT TOP 100 CampaignDisplayName AS ''{$reports_campaign.name_header$}'', SUM(HitsCount) AS ''{$reports_campaign.hits_header$}'' FROM
Analytics_DayHits, Analytics_Statistics 
INNER JOIN Analytics_Campaign ON StatisticsObjectName = CampaignName
WHERE (@CampaignName ='''' OR @CampaignName = CampaignName) AND
(StatisticsSiteID = @CMSContextCurrentSiteID) AND (StatisticsCode=@CodeName) AND (StatisticsID = HitsStatisticsID) AND 
(HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate) GROUP BY CampaignDisplayName ORDER BY SUM(HitsCount) DESC', 0, 610, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'eccf2a1b-c26f-4f12-b07b-6737876e18f2', '20110621 11:09:31')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (438, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''month'');
SELECT TOP 100 CampaignDisplayName AS ''{$reports_campaign.name_header$}'', SUM(HitsCount) AS ''{$reports_campaign.hits_header$}'' FROM
Analytics_MonthHits, Analytics_Statistics 
INNER JOIN Analytics_Campaign ON StatisticsObjectName = CampaignName
WHERE (@CampaignName ='''' OR @CampaignName = CampaignName) AND
(StatisticsSiteID = @CMSContextCurrentSiteID) AND (StatisticsCode=@CodeName) AND (StatisticsID = HitsStatisticsID) AND 
(HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate) GROUP BY CampaignDisplayName ORDER BY SUM(HitsCount) DESC', 0, 611, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '700ecdf9-4bfe-426b-bcd3-c539e4c5aedd', '20110621 11:09:31')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (439, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''hour'');
SELECT TOP 100 CampaignDisplayName AS ''{$reports_campaign.name_header$}'', SUM(HitsCount) AS ''{$reports_campaign.hits_header$}'' FROM
Analytics_HourHits, Analytics_Statistics 
INNER JOIN Analytics_Campaign ON StatisticsObjectName = CampaignName
WHERE (@CampaignName ='''' OR @CampaignName = CampaignName) AND
(StatisticsSiteID = @CMSContextCurrentSiteID) AND (StatisticsCode=@CodeName) AND (StatisticsID = HitsStatisticsID) AND 
(HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate) GROUP BY CampaignDisplayName ORDER BY SUM(HitsCount) DESC', 0, 612, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '3fe4a961-5945-41c2-a849-93f73d4c3233', '20110621 11:09:31')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (440, N'table_1', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''week'');
SELECT TOP 100 CampaignDisplayName AS ''{$reports_campaign.name_header$}'', SUM(HitsCount) AS ''{$reports_campaign.hits_header$}'' FROM
Analytics_WeekHits, Analytics_Statistics 
INNER JOIN Analytics_Campaign ON StatisticsObjectName = CampaignName
WHERE (@CampaignName ='''' OR @CampaignName = CampaignName) AND
(StatisticsSiteID = @CMSContextCurrentSiteID) AND (StatisticsCode=@CodeName) AND (StatisticsID = HitsStatisticsID) AND 
(HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate) GROUP BY CampaignDisplayName ORDER BY SUM(HitsCount) DESC', 0, 615, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '5ce40b8f-a054-413b-ac36-b4a93335a18e', '20110621 11:09:31')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (441, N'table_2', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''month'');
SELECT TOP 100 CampaignDisplayName AS ''{$reports_campaign.name_header$}'', SUM(HitsCount) AS ''{$reports_campaign.hits_header$}'' FROM
Analytics_MonthHits, Analytics_Statistics 
INNER JOIN Analytics_Campaign ON StatisticsObjectName = CampaignName
WHERE (@CampaignName ='''' OR @CampaignName = CampaignName) AND
(StatisticsSiteID = @CMSContextCurrentSiteID) AND (StatisticsCode=@CodeName) AND (StatisticsID = HitsStatisticsID) AND 
(HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate) GROUP BY CampaignDisplayName ORDER BY SUM(HitsCount) DESC', 0, 616, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'f27b6f3c-e496-454f-8d24-ed59f1642b84', '20110621 11:09:31')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (442, N'table_3', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''hour'');
SELECT TOP 100 CampaignDisplayName AS ''{$reports_campaign.name_header$}'', SUM(HitsCount) AS ''{$reports_campaign.hits_header$}'' FROM
Analytics_HourHits, Analytics_Statistics 
INNER JOIN Analytics_Campaign ON StatisticsObjectName = CampaignName
WHERE (@CampaignName ='''' OR @CampaignName = CampaignName) AND
(StatisticsSiteID = @CMSContextCurrentSiteID) AND (StatisticsCode=@CodeName) AND (StatisticsID = HitsStatisticsID) AND 
(HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate) GROUP BY CampaignDisplayName ORDER BY SUM(HitsCount) DESC', 0, 617, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '88114fa5-7712-466c-9e1d-938e28506c2e', '20110621 11:09:31')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (443, N'table_4', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day'');
SELECT TOP 100 CampaignDisplayName AS ''{$reports_campaign.name_header$}'', SUM(HitsCount) AS ''{$reports_campaign.hits_header$}'' FROM
Analytics_DayHits, Analytics_Statistics 
INNER JOIN Analytics_Campaign ON StatisticsObjectName = CampaignName
WHERE (@CampaignName ='''' OR @CampaignName = CampaignName) AND
(StatisticsSiteID = @CMSContextCurrentSiteID) AND (StatisticsCode=@CodeName) AND (StatisticsID = HitsStatisticsID) AND 
(HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate) GROUP BY CampaignDisplayName ORDER BY SUM(HitsCount) DESC', 0, 618, N'<customdata><querynorecordtext>No data found</querynorecordtext><exportenabled>true</exportenabled><skinid>ReportGridAnalytics</skinid><pagesize>10</pagesize><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '92a4d5fc-25e5-4fd9-8741-ae10c2b07a80', '20110621 11:09:31')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (444, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
 SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''year'');  
 
  SELECT CampaignDisplayName AS ''Campaign'',
  CASE
  WHEN @Goal=''value'' THEN SUM(HitsValue)
  ELSE SUM(HitsCount)
  END    
  AS ''{%ColumnName|(default)Hits%}''
  
  FROM Analytics_Statistics
  INNER JOIN Analytics_YearHits ON Analytics_YearHits.HitsStatisticsID = Analytics_Statistics.StatisticsID    
  INNER JOIN Analytics_Campaign ON (@Goal <> ''view'' AND StatisticsCode LIKE @Codename AND SUBSTRING(StatisticsCode, 16,LEN(StatisticsCode)) = CampaignName)
        OR (@Goal=''view'' AND StatisticsObjectName = CampaignName) AND StatisticsSiteID = CampaignSiteID
  
  WHERE (StatisticsSiteID = @SiteID OR @SiteID = 0) AND
  (StatisticsCode LIKE @CodeName)   
  AND (@ConversionName IS NULL OR @ConversionName IN ('''',StatisticsObjectName))
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY CampaignName,CampaignDisplayName
  ORDER BY {%ColumnName|(default)Hits%} DESC', 0, 619, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', '3d8055e1-b10b-436d-a462-f511a797836c', '20111026 13:51:03')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (445, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
 SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day'');  
 
  SELECT CampaignDisplayName AS ''Campaign'',
  CASE
  WHEN @Goal=''value'' THEN SUM(HitsValue)
  ELSE SUM(HitsCount)
  END    
  AS ''{%ColumnName|(default)Hits%}''
  
  FROM Analytics_Statistics
  INNER JOIN Analytics_DayHits ON Analytics_DayHits.HitsStatisticsID = Analytics_Statistics.StatisticsID    
  INNER JOIN Analytics_Campaign ON (@Goal <> ''view'' AND StatisticsCode LIKE @Codename AND SUBSTRING(StatisticsCode, 16,LEN(StatisticsCode)) = CampaignName)
        OR (@Goal=''view'' AND StatisticsObjectName = CampaignName) AND StatisticsSiteID = CampaignSiteID
  
  WHERE (StatisticsSiteID = @SiteID OR @SiteID = 0) AND
  (StatisticsCode LIKE @CodeName)   
  AND (@ConversionName IS NULL OR @ConversionName IN ('''',StatisticsObjectName))
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY CampaignName,CampaignDisplayName
  ORDER BY {%ColumnName|(default)Hits%} DESC', 0, 622, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'c04dc73f-4f2e-44d9-b035-478c420d3e7d', '20110927 11:10:20')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (448, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week'');
 SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''week'');  
 
  SELECT CampaignDisplayName AS ''Campaign'',
  CASE
  WHEN @Goal=''value'' THEN SUM(HitsValue)
  ELSE SUM(HitsCount)
  END    
  AS ''{%ColumnName|(default)Hits%}''
  
  FROM Analytics_Statistics
  INNER JOIN Analytics_WeekHits ON Analytics_WeekHits.HitsStatisticsID = Analytics_Statistics.StatisticsID    
  INNER JOIN Analytics_Campaign ON (@Goal <> ''view'' AND StatisticsCode LIKE @Codename AND SUBSTRING(StatisticsCode, 16,LEN(StatisticsCode)) = CampaignName)
        OR (@Goal=''view'' AND StatisticsObjectName = CampaignName) AND StatisticsSiteID = CampaignSiteID
  
  WHERE (StatisticsSiteID = @SiteID OR @SiteID = 0) AND
  (StatisticsCode LIKE @CodeName)   
  AND (@ConversionName IS NULL OR @ConversionName IN ('''',StatisticsObjectName))
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY CampaignName,CampaignDisplayName
  ORDER BY {%ColumnName|(default)Hits%} DESC', 0, 621, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '81423bf6-dfcc-4da9-95f1-a0a5806fd8b1', '20110927 11:10:20')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (449, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
 SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''month'');  
 
  SELECT CampaignDisplayName AS ''Campaign'',
  CASE
  WHEN @Goal=''value'' THEN SUM(HitsValue)
  ELSE SUM(HitsCount)
  END    
  AS ''{%ColumnName|(default)Hits%}''
  
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_MonthHits.HitsStatisticsID = Analytics_Statistics.StatisticsID    
  INNER JOIN Analytics_Campaign ON (@Goal <> ''view'' AND StatisticsCode LIKE @Codename AND SUBSTRING(StatisticsCode, 16,LEN(StatisticsCode)) = CampaignName)
        OR (@Goal=''view'' AND StatisticsObjectName = CampaignName) AND StatisticsSiteID = CampaignSiteID
  
  WHERE (StatisticsSiteID = @SiteID OR @SiteID = 0) AND
  (StatisticsCode LIKE @CodeName)   
  AND (@ConversionName IS NULL OR @ConversionName IN ('''',StatisticsObjectName))
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY CampaignName,CampaignDisplayName
  ORDER BY {%ColumnName|(default)Hits%} DESC', 0, 620, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'a63b4be6-cc26-4d92-a50d-8454c51cd5f9', '20110927 11:10:20')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (450, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
 SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''hour'');  
 
  SELECT CampaignDisplayName AS ''Campaign'',
  CASE
  WHEN @Goal=''value'' THEN SUM(HitsValue)
  ELSE SUM(HitsCount)
  END    
  AS ''{%ColumnName|(default)Hits%}''
  
  FROM Analytics_Statistics
  INNER JOIN Analytics_HourHits ON Analytics_HourHits.HitsStatisticsID = Analytics_Statistics.StatisticsID    
  INNER JOIN Analytics_Campaign ON (@Goal <> ''view'' AND StatisticsCode LIKE @Codename AND SUBSTRING(StatisticsCode, 16,LEN(StatisticsCode)) = CampaignName)
        OR (@Goal=''view'' AND StatisticsObjectName = CampaignName) AND StatisticsSiteID = CampaignSiteID
  
  WHERE (StatisticsSiteID = @SiteID OR @SiteID = 0) AND
  (StatisticsCode LIKE @CodeName)   
  AND (@ConversionName IS NULL OR @ConversionName IN ('''',StatisticsObjectName))
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
  GROUP BY CampaignName,CampaignDisplayName
  ORDER BY {%ColumnName|(default)Hits%} DESC', 0, 623, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '93025ce1-5b96-4d0c-a3ee-32704df3d0f9', '20110927 11:10:20')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (489, N'table', N'table', N'SELECT
    [CampaignDisplayName] as ''{$campaignselect.itemname$}'',
    SUM(HitsCount)  as ''{$campaign.goalcurrvalue$}'',
    ISNULL(CAST (GoalValueMin AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsCount) / NULLIF (GoalValueMin,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.redflagvalue$}'',
    ISNULL(CAST (GoalValue AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsCount) / NULLIF (GoalValue,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.goalvalue$}''
FROM Analytics_DayHits
    LEFT JOIN Analytics_Statistics ON HitsStatisticsID = [StatisticsID]
    LEFT JOIN Analytics_Campaign AS YY ON CampaignName = [StatisticsObjectName]
    LEFT JOIN
        (SELECT CampaignID,
            CASE
                WHEN CampaignGoalVisitorsPercent = 0  
                    THEN CampaignGoalVisitorsMin
                    ELSE (CAST (CampaignImpressions AS DECIMAL (15,0)) /100) * CampaignGoalVisitorsMin
                END  AS GoalValueMin,
            CASE    
                WHEN CampaignGoalVisitorsPercent = 0
                    THEN CampaignGoalVisitors
                    ELSE (CAST (CampaignImpressions AS DECIMAL (15,0)) /100) * CampaignGoalVisitors
                END  AS GoalValue
        FROM Analytics_Campaign)
        AS XX ON XX.CampaignID = YY.CampaignID
WHERE CampaignSiteID = @CMSContextCurrentSiteID AND StatisticsSiteID = @CMSContextCurrentSiteID AND (@CampaignName = '''' OR @CampaignName = CampaignName ) AND StatisticsCode = ''campaign'' AND (YY.CampaignGoalVisitorsMin IS NOT NULL OR YY.CampaignGoalVisitors IS NOT NULL)
GROUP BY StatisticsObjectName,CampaignDisplayName, CampaignGoalVisitors, CampaignGoalVisitorsMin, GoalValueMin, GoalValue', 0, 688, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '32486d5f-5a4f-400b-96af-e0a8da9b9056', '20110721 14:50:12')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (491, N'table', N'table', N'SELECT
    [CampaignDisplayName] as ''{$campaignselect.itemname$}'',
    SUM(HitsCount)  as ''{$campaign.goalcurrvalue$}'',
    ISNULL(CAST (GoalValueMin AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsCount) / NULLIF (GoalValueMin,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.redflagvalue$}'',
    ISNULL(CAST (GoalValue AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsCount) / NULLIF (GoalValue,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.goalvalue$}''
FROM Analytics_HourHits
    LEFT JOIN Analytics_Statistics ON HitsStatisticsID = [StatisticsID]
    LEFT JOIN Analytics_Campaign AS YY ON CampaignName = [StatisticsObjectName]
    LEFT JOIN
        (SELECT CampaignID,
            CASE
                WHEN CampaignGoalVisitorsPercent = 0  
                    THEN CampaignGoalVisitorsMin
                    ELSE (CAST (CampaignImpressions AS DECIMAL (15,0)) /100) * CampaignGoalVisitorsMin
                END  AS GoalValueMin,
            CASE    
                WHEN CampaignGoalVisitorsPercent = 0
                    THEN CampaignGoalVisitors
                    ELSE (CAST (CampaignImpressions AS DECIMAL (15,0)) /100) * CampaignGoalVisitors
                END  AS GoalValue
        FROM Analytics_Campaign)
        AS XX ON XX.CampaignID = YY.CampaignID
WHERE CampaignSiteID = @CMSContextCurrentSiteID AND StatisticsSiteID = @CMSContextCurrentSiteID AND (@CampaignName = '''' OR @CampaignName = CampaignName ) AND StatisticsCode = ''campaign'' AND (YY.CampaignGoalVisitorsMin IS NOT NULL OR YY.CampaignGoalVisitors IS NOT NULL)
GROUP BY StatisticsObjectName,CampaignDisplayName, CampaignGoalVisitors, CampaignGoalVisitorsMin, GoalValueMin, GoalValue', 0, 690, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', '00664276-2c62-4cc9-887c-5bb43a0973fd', '20110721 14:50:13')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (492, N'table', N'table', N'SELECT
    [CampaignDisplayName] as ''{$campaignselect.itemname$}'',
    SUM(HitsCount)  as ''{$campaign.goalcurrvalue$}'',
    ISNULL(CAST (GoalValueMin AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsCount) / NULLIF (GoalValueMin,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.redflagvalue$}'',
    ISNULL(CAST (GoalValue AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsCount) / NULLIF (GoalValue,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.goalvalue$}''
FROM Analytics_WeekHits
    LEFT JOIN Analytics_Statistics ON HitsStatisticsID = [StatisticsID]
    LEFT JOIN Analytics_Campaign AS YY ON CampaignName = [StatisticsObjectName]
    LEFT JOIN
        (SELECT CampaignID,
            CASE
                WHEN CampaignGoalVisitorsPercent = 0  
                    THEN CampaignGoalVisitorsMin
                    ELSE (CAST (CampaignImpressions AS DECIMAL (15,0)) /100) * CampaignGoalVisitorsMin
                END  AS GoalValueMin,
            CASE    
                WHEN CampaignGoalVisitorsPercent = 0
                    THEN CampaignGoalVisitors
                    ELSE (CAST (CampaignImpressions AS DECIMAL (15,0)) /100) * CampaignGoalVisitors
                END  AS GoalValue
        FROM Analytics_Campaign)
        AS XX ON XX.CampaignID = YY.CampaignID
WHERE CampaignSiteID = @CMSContextCurrentSiteID AND StatisticsSiteID = @CMSContextCurrentSiteID AND (@CampaignName = '''' OR @CampaignName = CampaignName ) AND StatisticsCode = ''campaign'' AND (YY.CampaignGoalVisitorsMin IS NOT NULL OR YY.CampaignGoalVisitors IS NOT NULL)
GROUP BY StatisticsObjectName,CampaignDisplayName, CampaignGoalVisitors, CampaignGoalVisitorsMin, GoalValueMin, GoalValue', 0, 691, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', '3ad7da1d-74df-4107-bfec-7bccf4f9fdbf', '20110721 14:50:12')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (493, N'table', N'table', N'SELECT
    [CampaignDisplayName] as ''{$campaignselect.itemname$}'',
    SUM(HitsCount)  as ''{$campaign.goalcurrvalue$}'',
    ISNULL(CAST (GoalValueMin AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsCount) / NULLIF (GoalValueMin,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.redflagvalue$}'',
    ISNULL(CAST (GoalValue AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsCount) / NULLIF (GoalValue,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.goalvalue$}''
FROM Analytics_MonthHits
    LEFT JOIN Analytics_Statistics ON HitsStatisticsID = [StatisticsID]
    LEFT JOIN Analytics_Campaign AS YY ON CampaignName = [StatisticsObjectName]
    LEFT JOIN
        (SELECT CampaignID,
            CASE
                WHEN CampaignGoalVisitorsPercent = 0  
                    THEN CampaignGoalVisitorsMin
                    ELSE (CAST (CampaignImpressions AS DECIMAL (15,0)) /100) * CampaignGoalVisitorsMin
                END  AS GoalValueMin,
            CASE    
                WHEN CampaignGoalVisitorsPercent = 0
                    THEN CampaignGoalVisitors
                    ELSE (CAST (CampaignImpressions AS DECIMAL (15,0)) /100) * CampaignGoalVisitors
                END  AS GoalValue
        FROM Analytics_Campaign)
        AS XX ON XX.CampaignID = YY.CampaignID
WHERE CampaignSiteID = @CMSContextCurrentSiteID AND StatisticsSiteID = @CMSContextCurrentSiteID AND (@CampaignName = '''' OR @CampaignName = CampaignName ) AND StatisticsCode = ''campaign'' AND (YY.CampaignGoalVisitorsMin IS NOT NULL OR YY.CampaignGoalVisitors IS NOT NULL)
GROUP BY StatisticsObjectName,CampaignDisplayName, CampaignGoalVisitors, CampaignGoalVisitorsMin, GoalValueMin, GoalValue', 0, 692, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', '855f4d5e-9832-4107-a952-4704db3b7b99', '20110721 14:50:12')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (494, N'table', N'table', N'SELECT
    [CampaignDisplayName] as ''{$campaignselect.itemname$}'',
    SUM(HitsCount)  as ''{$campaign.goalcurrvalue$}'',
    ISNULL(CAST (GoalValueMin AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsCount) / NULLIF (GoalValueMin,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.redflagvalue$}'',
    ISNULL(CAST (GoalValue AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsCount) / NULLIF (GoalValue,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.goalvalue$}''
FROM Analytics_YearHits
    LEFT JOIN Analytics_Statistics ON HitsStatisticsID = [StatisticsID]
    LEFT JOIN Analytics_Campaign AS YY ON CampaignName = [StatisticsObjectName]
    LEFT JOIN
        (SELECT CampaignID,
            CASE
                WHEN CampaignGoalVisitorsPercent = 0  
                    THEN CampaignGoalVisitorsMin
                    ELSE (CAST (CampaignImpressions AS DECIMAL (15,0)) /100) * CampaignGoalVisitorsMin
                END  AS GoalValueMin,
            CASE    
                WHEN CampaignGoalVisitorsPercent = 0
                    THEN CampaignGoalVisitors
                    ELSE (CAST (CampaignImpressions AS DECIMAL (15,0)) /100) * CampaignGoalVisitors
                END  AS GoalValue
        FROM Analytics_Campaign)
        AS XX ON XX.CampaignID = YY.CampaignID
WHERE CampaignSiteID = @CMSContextCurrentSiteID AND StatisticsSiteID = @CMSContextCurrentSiteID AND (@CampaignName = '''' OR @CampaignName = CampaignName ) AND StatisticsCode = ''campaign'' AND (YY.CampaignGoalVisitorsMin IS NOT NULL OR YY.CampaignGoalVisitors IS NOT NULL)
GROUP BY StatisticsObjectName,CampaignDisplayName, CampaignGoalVisitors, CampaignGoalVisitorsMin, GoalValueMin, GoalValue', 0, 693, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', '5b4bacf7-889f-4c38-8951-fca62d0a852d', '20110721 14:50:12')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (495, N'table', N'table', N'SELECT
    [CampaignDisplayName] as ''{$campaignselect.itemname$}'',
    SUM(HitsCount)  as ''{$campaign.goalcurrvalue$}'',
    ISNULL(CAST (GoalValueMin AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsCount) / NULLIF (GoalValueMin,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.redflagvalue$}'',
    ISNULL(CAST (GoalValue AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsCount) / NULLIF (GoalValue,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.goalvalue$}''
   
FROM Analytics_DayHits
    LEFT JOIN Analytics_Statistics ON HitsStatisticsID = [StatisticsID]
    LEFT JOIN Analytics_Campaign AS YY ON CampaignName = SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode))
    LEFT JOIN
        (SELECT CampaignID,
            CASE
                WHEN CampaignGoalConversionsPercent = 0  
                    THEN CampaignGoalConversionsMin
                    ELSE (CAST (CampaignImpressions AS DECIMAL (15,0)) /100) * CampaignGoalConversionsMin
                END  AS GoalValueMin,
            CASE    
                WHEN CampaignGoalConversionsPercent = 0
                    THEN CampaignGoalConversions
                    ELSE (CAST (CampaignImpressions AS DECIMAL (15,0)) /100) * CampaignGoalConversions
                END  AS GoalValue
        FROM Analytics_Campaign)
        AS XX ON XX.CampaignID = YY.CampaignID
WHERE CampaignSiteID = @CMSContextCurrentSiteID AND StatisticsSiteID = @CMSContextCurrentSiteID AND ((@CampaignName = '''' AND StatisticsCode LIKE N''campconversion;%'') OR (@CampaignName = CampaignName AND StatisticsCode = N''campconversion;'' + @CampaignName )) AND  (YY.CampaignGoalConversionsMin IS NOT NULL OR YY.CampaignGoalConversions IS NOT NULL)
GROUP BY CampaignDisplayName, CampaignGoalVisitors, CampaignGoalVisitorsMin, GoalValueMin, GoalValue', 0, 694, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '5337cdb3-d7cd-4452-81b7-a616fe839eb1', '20110721 14:48:04')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (496, N'table', N'table', N'SELECT
    [CampaignDisplayName] as ''{$campaignselect.itemname$}'',
    SUM(HitsCount)  as ''{$campaign.goalcurrvalue$}'',
    ISNULL(CAST (GoalValueMin AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsCount) / NULLIF (GoalValueMin,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.redflagvalue$}'',
    ISNULL(CAST (GoalValue AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsCount) / NULLIF (GoalValue,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.goalvalue$}''
   
FROM Analytics_HourHits
    LEFT JOIN Analytics_Statistics ON HitsStatisticsID = [StatisticsID]
    LEFT JOIN Analytics_Campaign AS YY ON CampaignName = SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode))
    LEFT JOIN
        (SELECT CampaignID,
            CASE
                WHEN CampaignGoalConversionsPercent = 0  
                    THEN CampaignGoalConversionsMin
                    ELSE (CAST (CampaignImpressions AS DECIMAL (15,0)) /100) * CampaignGoalConversionsMin
                END  AS GoalValueMin,
            CASE    
                WHEN CampaignGoalConversionsPercent = 0
                    THEN CampaignGoalConversions
                    ELSE (CAST (CampaignImpressions AS DECIMAL (15,0)) /100) * CampaignGoalConversions
                END  AS GoalValue
        FROM Analytics_Campaign)
        AS XX ON XX.CampaignID = YY.CampaignID
WHERE CampaignSiteID = @CMSContextCurrentSiteID AND StatisticsSiteID = @CMSContextCurrentSiteID AND ((@CampaignName = '''' AND StatisticsCode LIKE N''campconversion;%'') OR (@CampaignName = CampaignName AND StatisticsCode = N''campconversion;'' + @CampaignName )) AND  (YY.CampaignGoalConversionsMin IS NOT NULL OR YY.CampaignGoalConversions IS NOT NULL)
GROUP BY CampaignDisplayName, CampaignGoalVisitors, CampaignGoalVisitorsMin, GoalValueMin, GoalValue', 0, 695, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '07505fda-8e2a-4ad2-8adc-d9f8422dd3c8', '20110721 14:48:00')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (497, N'table', N'table', N'SELECT
    [CampaignDisplayName] as ''{$campaignselect.itemname$}'',
    SUM(HitsCount)  as ''{$campaign.goalcurrvalue$}'',
    ISNULL(CAST (GoalValueMin AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsCount) / NULLIF (GoalValueMin,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.redflagvalue$}'',
    ISNULL(CAST (GoalValue AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsCount) / NULLIF (GoalValue,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.goalvalue$}''
   
FROM Analytics_WeekHits
    LEFT JOIN Analytics_Statistics ON HitsStatisticsID = [StatisticsID]
    LEFT JOIN Analytics_Campaign AS YY ON CampaignName = SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode))
    LEFT JOIN
        (SELECT CampaignID,
            CASE
                WHEN CampaignGoalConversionsPercent = 0  
                    THEN CampaignGoalConversionsMin
                    ELSE (CAST (CampaignImpressions AS DECIMAL (15,0)) /100) * CampaignGoalConversionsMin
                END  AS GoalValueMin,
            CASE    
                WHEN CampaignGoalConversionsPercent = 0
                    THEN CampaignGoalConversions
                    ELSE (CAST (CampaignImpressions AS DECIMAL (15,0)) /100) * CampaignGoalConversions
                END  AS GoalValue
        FROM Analytics_Campaign)
        AS XX ON XX.CampaignID = YY.CampaignID
WHERE CampaignSiteID = @CMSContextCurrentSiteID AND StatisticsSiteID = @CMSContextCurrentSiteID AND ((@CampaignName = '''' AND StatisticsCode LIKE N''campconversion;%'') OR (@CampaignName = CampaignName AND StatisticsCode = N''campconversion;'' + @CampaignName )) AND  (YY.CampaignGoalConversionsMin IS NOT NULL OR YY.CampaignGoalConversions IS NOT NULL)
GROUP BY CampaignDisplayName, CampaignGoalVisitors, CampaignGoalVisitorsMin, GoalValueMin, GoalValue', 0, 696, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext></querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', '58994cea-3bb3-4ae6-b196-03220d6b6ca5', '20110804 12:17:45')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (498, N'table', N'table', N'SELECT
    [CampaignDisplayName] as ''{$campaignselect.itemname$}'',
    SUM(HitsCount)  as ''{$campaign.goalcurrvalue$}'',
    ISNULL(CAST (GoalValueMin AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsCount) / NULLIF (GoalValueMin,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.redflagvalue$}'',
    ISNULL(CAST (GoalValue AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsCount) / NULLIF (GoalValue,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.goalvalue$}''
   
FROM Analytics_MonthHits
    LEFT JOIN Analytics_Statistics ON HitsStatisticsID = [StatisticsID]
    LEFT JOIN Analytics_Campaign AS YY ON CampaignName = SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode))
    LEFT JOIN
        (SELECT CampaignID,
            CASE
                WHEN CampaignGoalConversionsPercent = 0  
                    THEN CampaignGoalConversionsMin
                    ELSE (CAST (CampaignImpressions AS DECIMAL (15,0)) /100) * CampaignGoalConversionsMin
                END  AS GoalValueMin,
            CASE    
                WHEN CampaignGoalConversionsPercent = 0
                    THEN CampaignGoalConversions
                    ELSE (CAST (CampaignImpressions AS DECIMAL (15,0)) /100) * CampaignGoalConversions
                END  AS GoalValue
        FROM Analytics_Campaign)
        AS XX ON XX.CampaignID = YY.CampaignID
WHERE CampaignSiteID = @CMSContextCurrentSiteID AND StatisticsSiteID = @CMSContextCurrentSiteID AND ((@CampaignName = '''' AND StatisticsCode LIKE N''campconversion;%'') OR (@CampaignName = CampaignName AND StatisticsCode = N''campconversion;'' + @CampaignName )) AND  (YY.CampaignGoalConversionsMin IS NOT NULL OR YY.CampaignGoalConversions IS NOT NULL)
GROUP BY CampaignDisplayName, CampaignGoalVisitors, CampaignGoalVisitorsMin, GoalValueMin, GoalValue', 0, 697, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '5ad99969-1afe-4be7-b5a3-6d3ae057c874', '20110721 14:48:00')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (499, N'table', N'table', N'SELECT
    [CampaignDisplayName] as ''{$campaignselect.itemname$}'',
    SUM(HitsCount)  as ''{$campaign.goalcurrvalue$}'',
    ISNULL(CAST (GoalValueMin AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsCount) / NULLIF (GoalValueMin,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.redflagvalue$}'',
    ISNULL(CAST (GoalValue AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsCount) / NULLIF (GoalValue,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.goalvalue$}''
   
FROM Analytics_YearHits
    LEFT JOIN Analytics_Statistics ON HitsStatisticsID = [StatisticsID]
    LEFT JOIN Analytics_Campaign AS YY ON CampaignName = SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode))
    LEFT JOIN
        (SELECT CampaignID,
            CASE
                WHEN CampaignGoalConversionsPercent = 0  
                    THEN CampaignGoalConversionsMin
                    ELSE (CAST (CampaignImpressions AS DECIMAL (15,0)) /100) * CampaignGoalConversionsMin
                END  AS GoalValueMin,
            CASE    
                WHEN CampaignGoalConversionsPercent = 0
                    THEN CampaignGoalConversions
                    ELSE (CAST (CampaignImpressions AS DECIMAL (15,0)) /100) * CampaignGoalConversions
                END  AS GoalValue
        FROM Analytics_Campaign)
        AS XX ON XX.CampaignID = YY.CampaignID
WHERE CampaignSiteID = @CMSContextCurrentSiteID AND StatisticsSiteID = @CMSContextCurrentSiteID AND ((@CampaignName = '''' AND StatisticsCode LIKE N''campconversion;%'') OR (@CampaignName = CampaignName AND StatisticsCode = N''campconversion;'' + @CampaignName )) AND  (YY.CampaignGoalConversionsMin IS NOT NULL OR YY.CampaignGoalConversions IS NOT NULL)
GROUP BY CampaignDisplayName, CampaignGoalVisitors, CampaignGoalVisitorsMin, GoalValueMin, GoalValue', 0, 698, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext></querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '2a33d814-422b-461a-9036-566932d0dd7d', '20110804 12:18:11')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (500, N'table', N'table', N'SELECT
    [CampaignDisplayName] as ''{$campaignselect.itemname$}'',
    SUM(HitsValue)  as ''{$campaign.goalcurrvalue$}'',
    ISNULL(CAST (GoalValueMin AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsValue) / NULLIF (GoalValueMin,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.redflagvalue$}'',
    ISNULL(CAST (GoalValue AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsValue) / NULLIF (GoalValue,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.goalvalue$}''
FROM Analytics_DayHits
    LEFT JOIN Analytics_Statistics ON HitsStatisticsID = [StatisticsID]
    LEFT JOIN Analytics_Campaign AS YY ON CampaignName = SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode))
    LEFT JOIN
        (SELECT CampaignID,
            CASE
                WHEN CampaignGoalValuePercent = 0  
                    THEN CampaignGoalValueMin
                    ELSE (CAST (CampaignTotalCost AS DECIMAL (15,0)) /100) * CampaignGoalValueMin
                END  AS GoalValueMin,
            CASE    
                WHEN CampaignGoalValuePercent = 0
                    THEN CampaignGoalValue
                    ELSE (CAST (CampaignTotalCost AS DECIMAL (15,0)) /100) * CampaignGoalValue
                END  AS GoalValue
        FROM Analytics_Campaign)
        AS XX ON XX.CampaignID = YY.CampaignID
WHERE CampaignSiteID = @CMSContextCurrentSiteID AND StatisticsSiteID = @CMSContextCurrentSiteID AND ((@CampaignName = '''' AND StatisticsCode LIKE N''campconversion;%'') OR (@CampaignName = CampaignName AND StatisticsCode = N''campconversion;'' + @CampaignName )) AND  (YY.CampaignGoalValueMin IS NOT NULL OR YY.CampaignGoalValue IS NOT NULL)
GROUP BY CampaignDisplayName, CampaignGoalVisitors, CampaignGoalVisitorsMin, GoalValueMin, GoalValue', 0, 699, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '737d54de-7e1d-4844-b530-fd52aea00f0e', '20110721 14:51:19')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (501, N'table', N'table', N'SELECT
    [CampaignDisplayName] as ''{$campaignselect.itemname$}'',
    SUM(HitsValue)  as ''{$campaign.goalcurrvalue$}'',
    ISNULL(CAST (GoalValueMin AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsValue) / NULLIF (GoalValueMin,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.redflagvalue$}'',
    ISNULL(CAST (GoalValue AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsValue) / NULLIF (GoalValue,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.goalvalue$}''
FROM Analytics_HourHits
    LEFT JOIN Analytics_Statistics ON HitsStatisticsID = [StatisticsID]
    LEFT JOIN Analytics_Campaign AS YY ON CampaignName = SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode))
    LEFT JOIN
        (SELECT CampaignID,
            CASE
                WHEN CampaignGoalValuePercent = 0  
                    THEN CampaignGoalValueMin
                    ELSE (CAST (CampaignTotalCost AS DECIMAL (15,0)) /100) * CampaignGoalValueMin
                END  AS GoalValueMin,
            CASE    
                WHEN CampaignGoalValuePercent = 0
                    THEN CampaignGoalValue
                    ELSE (CAST (CampaignTotalCost AS DECIMAL (15,0)) /100) * CampaignGoalValue
                END  AS GoalValue
        FROM Analytics_Campaign)
        AS XX ON XX.CampaignID = YY.CampaignID
WHERE CampaignSiteID = @CMSContextCurrentSiteID AND StatisticsSiteID = @CMSContextCurrentSiteID AND ((@CampaignName = '''' AND StatisticsCode LIKE N''campconversion;%'') OR (@CampaignName = CampaignName AND StatisticsCode = N''campconversion;'' + @CampaignName )) AND  (YY.CampaignGoalValueMin IS NOT NULL OR YY.CampaignGoalValue IS NOT NULL)
GROUP BY CampaignDisplayName, CampaignGoalVisitors, CampaignGoalVisitorsMin, GoalValueMin, GoalValue', 0, 700, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', 'bc671afd-bc1f-49ad-819d-f2c3b73edd77', '20110721 14:51:19')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (502, N'table', N'table', N'SELECT
    [CampaignDisplayName] as ''{$campaignselect.itemname$}'',
    SUM(HitsValue)  as ''{$campaign.goalcurrvalue$}'',
    ISNULL(CAST (GoalValueMin AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsValue) / NULLIF (GoalValueMin,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.redflagvalue$}'',
    ISNULL(CAST (GoalValue AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsValue) / NULLIF (GoalValue,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.goalvalue$}''
FROM Analytics_WeekHits
    LEFT JOIN Analytics_Statistics ON HitsStatisticsID = [StatisticsID]
    LEFT JOIN Analytics_Campaign AS YY ON CampaignName = SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode))
    LEFT JOIN
        (SELECT CampaignID,
            CASE
                WHEN CampaignGoalValuePercent = 0  
                    THEN CampaignGoalValueMin
                    ELSE (CAST (CampaignTotalCost AS DECIMAL (15,0)) /100) * CampaignGoalValueMin
                END  AS GoalValueMin,
            CASE    
                WHEN CampaignGoalValuePercent = 0
                    THEN CampaignGoalValue
                    ELSE (CAST (CampaignTotalCost AS DECIMAL (15,0)) /100) * CampaignGoalValue
                END  AS GoalValue
        FROM Analytics_Campaign)
        AS XX ON XX.CampaignID = YY.CampaignID
WHERE CampaignSiteID = @CMSContextCurrentSiteID AND StatisticsSiteID = @CMSContextCurrentSiteID AND ((@CampaignName = '''' AND StatisticsCode LIKE N''campconversion;%'') OR (@CampaignName = CampaignName AND StatisticsCode = N''campconversion;'' + @CampaignName )) AND  (YY.CampaignGoalValueMin IS NOT NULL OR YY.CampaignGoalValue IS NOT NULL)
GROUP BY CampaignDisplayName, CampaignGoalVisitors, CampaignGoalVisitorsMin, GoalValueMin, GoalValue', 0, 701, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', '6b4e4ce0-e4f8-434d-ae39-0d86e1fc6f79', '20110721 14:51:19')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (503, N'table', N'table', N'SELECT
    [CampaignDisplayName] as ''{$campaignselect.itemname$}'',
    SUM(HitsValue)  as ''{$campaign.goalcurrvalue$}'',
    ISNULL(CAST (GoalValueMin AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsValue) / NULLIF (GoalValueMin,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.redflagvalue$}'',
    ISNULL(CAST (GoalValue AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsValue) / NULLIF (GoalValue,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.goalvalue$}''
FROM Analytics_MonthHits
    LEFT JOIN Analytics_Statistics ON HitsStatisticsID = [StatisticsID]
    LEFT JOIN Analytics_Campaign AS YY ON CampaignName = SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode))
    LEFT JOIN
        (SELECT CampaignID,
            CASE
                WHEN CampaignGoalValuePercent = 0  
                    THEN CampaignGoalValueMin
                    ELSE (CAST (CampaignTotalCost AS DECIMAL (15,0)) /100) * CampaignGoalValueMin
                END  AS GoalValueMin,
            CASE    
                WHEN CampaignGoalValuePercent = 0
                    THEN CampaignGoalValue
                    ELSE (CAST (CampaignTotalCost AS DECIMAL (15,0)) /100) * CampaignGoalValue
                END  AS GoalValue
        FROM Analytics_Campaign)
        AS XX ON XX.CampaignID = YY.CampaignID
WHERE CampaignSiteID = @CMSContextCurrentSiteID AND StatisticsSiteID = @CMSContextCurrentSiteID AND ((@CampaignName = '''' AND StatisticsCode LIKE N''campconversion;%'') OR (@CampaignName = CampaignName AND StatisticsCode = N''campconversion;'' + @CampaignName )) AND  (YY.CampaignGoalValueMin IS NOT NULL OR YY.CampaignGoalValue IS NOT NULL)
GROUP BY CampaignDisplayName, CampaignGoalVisitors, CampaignGoalVisitorsMin, GoalValueMin, GoalValue', 0, 702, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', '03fd77d3-dce9-4262-b974-f12f3c0bc8b1', '20110721 14:51:19')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (504, N'table', N'table', N'SELECT
    [CampaignDisplayName] as ''{$campaignselect.itemname$}'',
    SUM(HitsValue)  as ''{$campaign.goalcurrvalue$}'',
    ISNULL(CAST (GoalValueMin AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsValue) / NULLIF (GoalValueMin,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.redflagvalue$}'',
    ISNULL(CAST (GoalValue AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsValue) / NULLIF (GoalValue,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.goalvalue$}''
FROM Analytics_YearHits
    LEFT JOIN Analytics_Statistics ON HitsStatisticsID = [StatisticsID]
    LEFT JOIN Analytics_Campaign AS YY ON CampaignName = SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode))
    LEFT JOIN
        (SELECT CampaignID,
            CASE
                WHEN CampaignGoalValuePercent = 0  
                    THEN CampaignGoalValueMin
                    ELSE (CAST (CampaignTotalCost AS DECIMAL (15,0)) /100) * CampaignGoalValueMin
                END  AS GoalValueMin,
            CASE    
                WHEN CampaignGoalValuePercent = 0
                    THEN CampaignGoalValue
                    ELSE (CAST (CampaignTotalCost AS DECIMAL (15,0)) /100) * CampaignGoalValue
                END  AS GoalValue
        FROM Analytics_Campaign)
        AS XX ON XX.CampaignID = YY.CampaignID
WHERE CampaignSiteID = @CMSContextCurrentSiteID AND StatisticsSiteID = @CMSContextCurrentSiteID AND ((@CampaignName = '''' AND StatisticsCode LIKE N''campconversion;%'') OR (@CampaignName = CampaignName AND StatisticsCode = N''campconversion;'' + @CampaignName )) AND  (YY.CampaignGoalValueMin IS NOT NULL OR YY.CampaignGoalValue IS NOT NULL)
GROUP BY CampaignDisplayName, CampaignGoalVisitors, CampaignGoalVisitorsMin, GoalValueMin, GoalValue', 0, 703, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', '573de2ec-bb95-4bc8-94a4-9b5fcac18f0d', '20110721 14:51:19')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (505, N'table', N'table', N'SELECT CampaignDisplayName AS ''{$campaignselect.itemname$}'',
  CAST (SUM(Y.Value)/SUM(X.Visits) AS DECIMAL (15,2)) AS ''{$campaign.goalcurrvalue$}'',    
    ISNULL(CAST (GoalValueMin AS NVARCHAR (20))+'' ('' + CAST (CAST (CAST (SUM(Y.Value)/SUM(X.Visits) AS DECIMAL (15,0)) / NULLIF (GoalValueMin,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.redflagvalue$}'',
    ISNULL(CAST (GoalValue AS NVARCHAR (20))+'' ('' + CAST (CAST (CAST (SUM(Y.Value)/SUM(X.Visits) AS DECIMAL (15,0)) / NULLIF (GoalValue,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.goalvalue$}''
 FROM Analytics_Statistics
 -- Visit
 LEFT JOIN (SELECT HitsStatisticsID, SUM(HitsCount) AS Visits FROM Analytics_DayHits GROUP BY HitsStatisticsID) AS X ON StatisticsCode=''campaign'' AND StatisticsSiteID = @CMSContextCurrentSiteID
  AND X.HitsStatisticsID = StatisticsID
 
 -- Value
 LEFT JOIN (SELECT HitsStatisticsID, SUM(HitsValue) AS Value FROM Analytics_DayHits GROUP BY HitsStatisticsID) AS Y ON StatisticsCode LIKE''campconversion;%'' AND StatisticsSiteID = @CMSContextCurrentSiteID
  AND Y.HitsStatisticsID = StatisticsID
 -- Campaign and its goal    
 LEFT JOIN
        (SELECT CampaignDisplayName,CampaignName,CampaignSiteID,
            CASE
                WHEN CampaignGoalPerVisitorPercent = 0  
                    THEN CampaignGoalPerVisitorMin
                    ELSE ((CAST (CampaignImpressions AS DECIMAL (15,0)) /NULLIF(CampaignTotalCost,0)) * (CAST(CampaignGoalPerVisitorMin AS DECIMAL (15,0)) /100))
                END  AS GoalValueMin,
            CASE    
                WHEN CampaignGoalPerVisitorPercent = 0
                    THEN CampaignGoalPerVisitor
                    ELSE ((CAST (CampaignImpressions AS DECIMAL (15,0)) /NULLIF(CampaignTotalCost,0))  * (CAST(CampaignGoalPerVisitor AS DECIMAL(15,0)) /100))
                END  AS GoalValue
        FROM Analytics_Campaign)
        AS XX ON (XX.CampaignName = SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode)) OR (StatisticsCode =''campaign'' AND statisticsObjectName =CampaignName))
      AND CampaignSiteID = StatisticsSiteID
 WHERE @CampaignName ='''' OR @CampaignName=CampaignName AND StatisticsSiteID = @CMSContextCurrentSiteID      
 GROUP BY CampaignDisplayName,GoalValueMin,GoalValue
 HAVING  SUM(X.Visits) IS NOT NULL OR SUM(Y.Value) IS NOT NULL', 0, 704, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', '802beca6-f973-4035-aac3-278493013348', '20110721 14:54:57')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (506, N'table', N'table', N'SELECT CampaignDisplayName AS ''{$campaignselect.itemname$}'',
  CAST (SUM(Y.Value)/SUM(X.Visits) AS DECIMAL (15,2)) AS ''{$campaign.goalcurrvalue$}'',    
    ISNULL(CAST (GoalValueMin AS NVARCHAR (20))+'' ('' + CAST (CAST (CAST (SUM(Y.Value)/SUM(X.Visits) AS DECIMAL (15,0)) / NULLIF (GoalValueMin,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.redflagvalue$}'',
    ISNULL(CAST (GoalValue AS NVARCHAR (20))+'' ('' + CAST (CAST (CAST (SUM(Y.Value)/SUM(X.Visits) AS DECIMAL (15,0)) / NULLIF (GoalValue,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.goalvalue$}''
 FROM Analytics_Statistics
 -- Visit
 LEFT JOIN (SELECT HitsStatisticsID, SUM(HitsCount) AS Visits FROM Analytics_HourHits GROUP BY HitsStatisticsID) AS X ON StatisticsCode=''campaign'' AND StatisticsSiteID = @CMSContextCurrentSiteID
  AND X.HitsStatisticsID = StatisticsID
 
 -- Value
 LEFT JOIN (SELECT HitsStatisticsID, SUM(HitsValue) AS Value FROM Analytics_HourHits GROUP BY HitsStatisticsID) AS Y ON StatisticsCode LIKE''campconversion;%'' AND StatisticsSiteID = @CMSContextCurrentSiteID
  AND Y.HitsStatisticsID = StatisticsID
 -- Campaign and its goal    
 LEFT JOIN
        (SELECT CampaignDisplayName,CampaignName,CampaignSiteID,
            CASE
                WHEN CampaignGoalPerVisitorPercent = 0  
                    THEN CampaignGoalPerVisitorMin
                    ELSE ((CAST (CampaignImpressions AS DECIMAL (15,0)) /NULLIF(CampaignTotalCost,0)) * (CAST(CampaignGoalPerVisitorMin AS DECIMAL (15,0)) /100))
                END  AS GoalValueMin,
            CASE    
                WHEN CampaignGoalPerVisitorPercent = 0
                    THEN CampaignGoalPerVisitor
                    ELSE ((CAST (CampaignImpressions AS DECIMAL (15,0)) /NULLIF(CampaignTotalCost,0))  * (CAST(CampaignGoalPerVisitor AS DECIMAL(15,0)) /100))
                END  AS GoalValue
        FROM Analytics_Campaign)
        AS XX ON (XX.CampaignName = SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode)) OR (StatisticsCode =''campaign'' AND statisticsObjectName =CampaignName))
      AND CampaignSiteID = StatisticsSiteID
 WHERE @CampaignName ='''' OR @CampaignName=CampaignName AND StatisticsSiteID = @CMSContextCurrentSiteID      
 GROUP BY CampaignDisplayName,GoalValueMin,GoalValue
 HAVING  SUM(X.Visits) IS NOT NULL OR SUM(Y.Value) IS NOT NULL', 0, 705, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', '01e57407-b28b-4f32-a234-b8a8350b66f6', '20110721 14:55:01')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (507, N'table', N'table', N'SELECT CampaignDisplayName AS ''{$campaignselect.itemname$}'',
  CAST (SUM(Y.Value)/SUM(X.Visits) AS DECIMAL (15,2)) AS ''{$campaign.goalcurrvalue$}'',    
    ISNULL(CAST (GoalValueMin AS NVARCHAR (20))+'' ('' + CAST (CAST (CAST (SUM(Y.Value)/SUM(X.Visits) AS DECIMAL (15,0)) / NULLIF (GoalValueMin,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.redflagvalue$}'',
    ISNULL(CAST (GoalValue AS NVARCHAR (20))+'' ('' + CAST (CAST (CAST (SUM(Y.Value)/SUM(X.Visits) AS DECIMAL (15,0)) / NULLIF (GoalValue,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.goalvalue$}''
 FROM Analytics_Statistics
 -- Visit
 LEFT JOIN (SELECT HitsStatisticsID, SUM(HitsCount) AS Visits FROM Analytics_WeekHits GROUP BY HitsStatisticsID) AS X ON StatisticsCode=''campaign'' AND StatisticsSiteID = @CMSContextCurrentSiteID
  AND X.HitsStatisticsID = StatisticsID
 
 -- Value
 LEFT JOIN (SELECT HitsStatisticsID, SUM(HitsValue) AS Value FROM Analytics_WeekHits GROUP BY HitsStatisticsID) AS Y ON StatisticsCode LIKE''campconversion;%'' AND StatisticsSiteID = @CMSContextCurrentSiteID
  AND Y.HitsStatisticsID = StatisticsID
 -- Campaign and its goal    
 LEFT JOIN
        (SELECT CampaignDisplayName,CampaignName,CampaignSiteID,
            CASE
                WHEN CampaignGoalPerVisitorPercent = 0  
                    THEN CampaignGoalPerVisitorMin
                    ELSE ((CAST (CampaignImpressions AS DECIMAL (15,0)) /NULLIF(CampaignTotalCost,0)) * (CAST(CampaignGoalPerVisitorMin AS DECIMAL (15,0)) /100))
                END  AS GoalValueMin,
            CASE    
                WHEN CampaignGoalPerVisitorPercent = 0
                    THEN CampaignGoalPerVisitor
                    ELSE ((CAST (CampaignImpressions AS DECIMAL (15,0)) /NULLIF(CampaignTotalCost,0))  * (CAST(CampaignGoalPerVisitor AS DECIMAL(15,0)) /100))
                END  AS GoalValue
        FROM Analytics_Campaign)
        AS XX ON (XX.CampaignName = SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode)) OR (StatisticsCode =''campaign'' AND statisticsObjectName =CampaignName))
      AND CampaignSiteID = StatisticsSiteID
 WHERE @CampaignName ='''' OR @CampaignName=CampaignName AND StatisticsSiteID = @CMSContextCurrentSiteID      
 GROUP BY CampaignDisplayName,GoalValueMin,GoalValue
 HAVING  SUM(X.Visits) IS NOT NULL OR SUM(Y.Value) IS NOT NULL', 0, 706, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', '943cf535-831f-434e-bf6c-3ae55a20c5ca', '20110721 14:55:01')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (508, N'table', N'table', N'SELECT CampaignDisplayName AS ''{$campaignselect.itemname$}'',
  CAST (SUM(Y.Value)/SUM(X.Visits) AS DECIMAL (15,2)) AS ''{$campaign.goalcurrvalue$}'',    
    ISNULL(CAST (GoalValueMin AS NVARCHAR (20))+'' ('' + CAST (CAST (CAST (SUM(Y.Value)/SUM(X.Visits) AS DECIMAL (15,0)) / NULLIF (GoalValueMin,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.redflagvalue$}'',
    ISNULL(CAST (GoalValue AS NVARCHAR (20))+'' ('' + CAST (CAST (CAST (SUM(Y.Value)/SUM(X.Visits) AS DECIMAL (15,0)) / NULLIF (GoalValue,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.goalvalue$}''
 FROM Analytics_Statistics
 -- Visit
 LEFT JOIN (SELECT HitsStatisticsID, SUM(HitsCount) AS Visits FROM Analytics_MonthHits GROUP BY HitsStatisticsID) AS X ON StatisticsCode=''campaign'' AND StatisticsSiteID = @CMSContextCurrentSiteID
  AND X.HitsStatisticsID = StatisticsID
 
 -- Value
 LEFT JOIN (SELECT HitsStatisticsID, SUM(HitsValue) AS Value FROM Analytics_MonthHits GROUP BY HitsStatisticsID) AS Y ON StatisticsCode LIKE''campconversion;%'' AND StatisticsSiteID = @CMSContextCurrentSiteID
  AND Y.HitsStatisticsID = StatisticsID
 -- Campaign and its goal    
 LEFT JOIN
        (SELECT CampaignDisplayName,CampaignName,CampaignSiteID,
            CASE
                WHEN CampaignGoalPerVisitorPercent = 0  
                    THEN CampaignGoalPerVisitorMin
                    ELSE ((CAST (CampaignImpressions AS DECIMAL (15,0)) /NULLIF(CampaignTotalCost,0)) * (CAST(CampaignGoalPerVisitorMin AS DECIMAL (15,0)) /100))
                END  AS GoalValueMin,
            CASE    
                WHEN CampaignGoalPerVisitorPercent = 0
                    THEN CampaignGoalPerVisitor
                    ELSE ((CAST (CampaignImpressions AS DECIMAL (15,0)) /NULLIF(CampaignTotalCost,0))  * (CAST(CampaignGoalPerVisitor AS DECIMAL(15,0)) /100))
                END  AS GoalValue
        FROM Analytics_Campaign)
        AS XX ON (XX.CampaignName = SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode)) OR (StatisticsCode =''campaign'' AND statisticsObjectName =CampaignName))
      AND CampaignSiteID = StatisticsSiteID
 WHERE @CampaignName ='''' OR @CampaignName=CampaignName AND StatisticsSiteID = @CMSContextCurrentSiteID      
 GROUP BY CampaignDisplayName,GoalValueMin,GoalValue
 HAVING  SUM(X.Visits) IS NOT NULL OR SUM(Y.Value) IS NOT NULL', 0, 707, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '21d347cb-c423-4436-a716-edbdd8e4ed26', '20110721 14:54:57')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (509, N'table', N'table', N'SELECT CampaignDisplayName AS ''{$campaignselect.itemname$}'',
  CAST (SUM(Y.Value)/SUM(X.Visits) AS DECIMAL (15,2)) AS ''{$campaign.goalcurrvalue$}'',    
    ISNULL(CAST (GoalValueMin AS NVARCHAR (20))+'' ('' + CAST (CAST (CAST (SUM(Y.Value)/SUM(X.Visits) AS DECIMAL (15,0)) / NULLIF (GoalValueMin,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.redflagvalue$}'',
    ISNULL(CAST (GoalValue AS NVARCHAR (20))+'' ('' + CAST (CAST (CAST (SUM(Y.Value)/SUM(X.Visits) AS DECIMAL (15,0)) / NULLIF (GoalValue,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.goalvalue$}''
 FROM Analytics_Statistics
 -- Visit
 LEFT JOIN (SELECT HitsStatisticsID, SUM(HitsCount) AS Visits FROM Analytics_YearHits GROUP BY HitsStatisticsID) AS X ON StatisticsCode=''campaign'' AND StatisticsSiteID = @CMSContextCurrentSiteID
  AND X.HitsStatisticsID = StatisticsID
 
 -- Value
 LEFT JOIN (SELECT HitsStatisticsID, SUM(HitsValue) AS Value FROM Analytics_YearHits GROUP BY HitsStatisticsID) AS Y ON StatisticsCode LIKE''campconversion;%'' AND StatisticsSiteID = @CMSContextCurrentSiteID
  AND Y.HitsStatisticsID = StatisticsID
 -- Campaign and its goal    
 LEFT JOIN
        (SELECT CampaignDisplayName,CampaignName,CampaignSiteID,
            CASE
                WHEN CampaignGoalPerVisitorPercent = 0  
                    THEN CampaignGoalPerVisitorMin
                    ELSE ((CAST (CampaignImpressions AS DECIMAL (15,0)) /NULLIF(CampaignTotalCost,0)) * (CAST(CampaignGoalPerVisitorMin AS DECIMAL (15,0)) /100))
                END  AS GoalValueMin,
            CASE    
                WHEN CampaignGoalPerVisitorPercent = 0
                    THEN CampaignGoalPerVisitor
                    ELSE ((CAST (CampaignImpressions AS DECIMAL (15,0)) /NULLIF(CampaignTotalCost,0))  * (CAST(CampaignGoalPerVisitor AS DECIMAL(15,0)) /100))
                END  AS GoalValue
        FROM Analytics_Campaign)
        AS XX ON (XX.CampaignName = SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode)) OR (StatisticsCode =''campaign'' AND statisticsObjectName =CampaignName))
      AND CampaignSiteID = StatisticsSiteID
 WHERE @CampaignName ='''' OR @CampaignName=CampaignName AND StatisticsSiteID = @CMSContextCurrentSiteID      
 GROUP BY CampaignDisplayName,GoalValueMin,GoalValue
 HAVING  SUM(X.Visits) IS NOT NULL OR SUM(Y.Value) IS NOT NULL', 0, 708, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', 'dd913b4f-6320-42d1-a2bf-d6cc04e1cc36', '20110721 14:55:01')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (510, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day''); 
 SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day''); 
SELECT CampaignDisplayName AS '' '',ISNULL(SUM (Views.Hits),0) AS ''{$analytics_codename.visits$}'', ISNULL(SUM(Conversions.Hits),0) AS ''{$conversion.conversion.list$}'',
    ISNULL(CAST (CAST (CAST (SUM(Conversions.Hits) AS DECIMAL (15,2)) / SUM (Views.Hits) * 100 AS DECIMAL(15,1)) AS NVARCHAR(20))+''%'',''0.0%'') AS ''{$abtesting.conversionrate$}'',
  ISNULL(SUM (Conversions.Value),0) AS ''{$conversions.value$}'',   
  ISNULL(ROUND (SUM (Conversions.Value)  / NULLIF (SUM(Views.Hits),0), 1),0) AS ''{$conversions.valuepervisit$}'',
  ISNULL(CampaignTotalCost,0) AS ''{$campaign.totalcost$}'', 
  CAST (CAST (ISNULL(SUM(Conversions.Value) / NULLIF(CampaignTotalCost,0),0)*100 AS DECIMAL(15,1)) AS NVARCHAR(15))+''%'' AS ''{$campaign.roi$}''
  
 FROM Analytics_Campaign 
  LEFT JOIN Analytics_Statistics ON CampaignName = SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode)) OR ((StatisticsObjectName = CampaignName) AND StatisticsCode=''campaign'')
-- Visits
LEFT JOIN (SELECT SUM(HitsCount) AS Hits,HitsStatisticsID AS HitsStatisticsID  FROM Analytics_DayHits 
	WHERE (@FromDate IS NULL OR @FromDate <= HitsStartTime) AND (@ToDate IS NULL OR @ToDate >= HitsStartTime)
   GROUP BY HitsStatisticsID) AS Views ON Views.HitsStatisticsID = Analytics_Statistics.StatisticsID AND StatisticsCode = ''campaign''
-- Conversion count, conversion value
LEFT JOIN (SELECT SUM(HitsCount) AS Hits,SUM(HitsValue) AS Value,HitsStatisticsID AS HitsStatisticsID  FROM Analytics_DayHits 
	WHERE (@FromDate IS NULL OR @FromDate <= HitsStartTime) AND (@ToDate IS NULL OR @ToDate >= HitsStartTime)
   GROUP BY HitsStatisticsID) AS Conversions ON Conversions.HitsStatisticsID = Analytics_Statistics.StatisticsID AND StatisticsCode LIKE ''campconversion;%''
WHERE CampaignSiteID = @CMSContextCurrentSiteID
GROUP BY CampaignDisplayName, CampaignTotalCost', 0, 709, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '81af2b08-ab67-489d-b4a9-4116e9a63358', '20110920 16:37:22')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (516, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day''); 
 SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day''); 
SELECT  
 CampaignDisplayName AS '' '',
 ISNULL(ConversionDisplayName,Conversions.ConversionName +'' ({$recyclebin.deleted$})'') AS ''{$Conversion.name$}'',ISNULL(SUM (Visits.Count),0) AS ''{$analytics_codename.visits$}'',
 ISNULL(SUM(Conversions.Hits),0) AS ''{$conversion.conversion.list$}'',
 ISNULL(CAST (CAST (CAST (SUM(Conversions.Hits) AS DECIMAL (15,2)) / SUM (Visits.Count) * 100 AS DECIMAL(15,1)) AS NVARCHAR(20))+''%'',''0.0%'') AS ''{$abtesting.conversionrate$}'',
 ISNULL(SUM (Conversions.Value),0) AS ''{$conversions.value$}'', 
 ISNULL(ROUND (SUM (Conversions.Value)  / NULLIF (SUM(Visits.Count),0), 1),0) AS ''{$conversions.valuepervisit$}''    
  
 FROM Analytics_Campaign
    -- Visits
    LEFT JOIN (SELECT SUM(HitsCount) AS Count, StatisticsObjectName AS CampaignName FROM
				Analytics_DayHits JOIN Analytics_Statistics ON HitsStatisticsID = StatisticsID
				WHERE (@FromDate IS NULL OR @FromDate <= HitsStartTime) AND (@ToDate IS NULL OR @ToDate >= HitsStartTime)
				AND StatisticsSiteID = @CMSContextCurrentSiteID AND  
				StatisticsCode = ''campaign'' AND StatisticsObjectName = @CampaignName
				GROUP BY StatisticsObjectName
				) AS Visits
	ON Visits.CampaignName = Analytics_Campaign.CampaignName
								  
    --- Conversion count, conversion value
	LEFT JOIN (SELECT SUM(HitsCount) AS Hits,SUM(HitsValue) AS Value, StatisticsObjectName  AS ConversionName,
				SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode)) AS CampaignName 	FROM Analytics_DayHits 
				JOIN Analytics_Statistics ON StatisticsID = HitsStatisticsID
				WHERE (@FromDate IS NULL OR @FromDate <= HitsStartTime) AND (@ToDate IS NULL OR @ToDate >= HitsStartTime)
				AND StatisticsCode LIKE ''campconversion;%'' AND StatisticsSiteID = @CMSContextCurrentSiteID
				GROUP BY StatisticsObjectName,SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode))) AS Conversions 
	ON Conversions.CampaignName = Analytics_Campaign.CampaignName 
 LEFT JOIN Analytics_Conversion ON Conversions.ConversionName = Analytics_Conversion.ConversionName AND 
			Analytics_Conversion.ConversionSiteID = @CMSContextCurrentSiteID
WHERE CampaignSiteID = @CMSContextCurrentSiteID AND Analytics_Campaign.CampaignName = @CampaignName 
GROUP BY CampaignDisplayName,ConversionDisplayName,Conversions.ConversionName', 0, 715, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', '9dab3cd8-c9d1-40f1-95fd-19ad62e0f654', '20110712 19:11:11')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (517, N'table', N'table', N'SELECT
    [CampaignDisplayName] as ''{$campaignselect.itemname$}'',
    SUM(HitsCount)  as ''{$campaign.goalcurrvalue$}'',
    ISNULL(CAST (GoalValueMin AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsCount) / NULLIF (GoalValueMin,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.redflagvalue$}'',
    ISNULL(CAST (GoalValue AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsCount) / NULLIF (GoalValue,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.goalvalue$}''
   
FROM Analytics_YearHits
    LEFT JOIN Analytics_Statistics ON HitsStatisticsID = [StatisticsID]
    LEFT JOIN Analytics_Campaign AS YY ON CampaignName = SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode))
    LEFT JOIN
        (SELECT CampaignID,
            CASE
                WHEN CampaignGoalConversionsPercent = 0  
                    THEN CampaignGoalConversionsMin
                    ELSE (CAST (CampaignImpressions AS DECIMAL (15,0)) /100) * CampaignGoalConversionsMin
                END  AS GoalValueMin,
            CASE    
                WHEN CampaignGoalConversionsPercent = 0
                    THEN CampaignGoalConversions
                    ELSE (CAST (CampaignImpressions AS DECIMAL (15,0)) /100) * CampaignGoalConversions
                END  AS GoalValue
        FROM Analytics_Campaign)
        AS XX ON XX.CampaignID = YY.CampaignID
WHERE CampaignSiteID = @CMSContextCurrentSiteID AND StatisticsSiteID = @CMSContextCurrentSiteID AND ((@CampaignName = '''' AND StatisticsCode LIKE N''campconversion;%'') OR (@CampaignName = CampaignName AND StatisticsCode = N''campconversion;'' + @CampaignName )) AND  (YY.CampaignGoalConversionsMin IS NOT NULL OR YY.CampaignGoalConversions IS NOT NULL)
GROUP BY CampaignDisplayName, CampaignGoalVisitors, CampaignGoalVisitorsMin, GoalValueMin, GoalValue', 0, 717, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', '49a542c6-3cb3-418a-b1be-ba81d0726d08', '20110721 14:48:03')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (518, N'table', N'table', N'SELECT
    [CampaignDisplayName] as ''{$campaignselect.itemname$}'',
    SUM(HitsCount)  as ''{$campaign.goalcurrvalue$}'',
    ISNULL(CAST (GoalValueMin AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsCount) / NULLIF (GoalValueMin,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.redflagvalue$}'',
    ISNULL(CAST (GoalValue AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsCount) / NULLIF (GoalValue,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.goalvalue$}''
   
FROM Analytics_MonthHits
    LEFT JOIN Analytics_Statistics ON HitsStatisticsID = [StatisticsID]
    LEFT JOIN Analytics_Campaign AS YY ON CampaignName = SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode))
    LEFT JOIN
        (SELECT CampaignID,
            CASE
                WHEN CampaignGoalConversionsPercent = 0  
                    THEN CampaignGoalConversionsMin
                    ELSE (CAST (CampaignImpressions AS DECIMAL (15,0)) /100) * CampaignGoalConversionsMin
                END  AS GoalValueMin,
            CASE    
                WHEN CampaignGoalConversionsPercent = 0
                    THEN CampaignGoalConversions
                    ELSE (CAST (CampaignImpressions AS DECIMAL (15,0)) /100) * CampaignGoalConversions
                END  AS GoalValue
        FROM Analytics_Campaign)
        AS XX ON XX.CampaignID = YY.CampaignID
WHERE CampaignSiteID = @CMSContextCurrentSiteID AND StatisticsSiteID = @CMSContextCurrentSiteID AND ((@CampaignName = '''' AND StatisticsCode LIKE N''campconversion;%'') OR (@CampaignName = CampaignName AND StatisticsCode = N''campconversion;'' + @CampaignName )) AND  (YY.CampaignGoalConversionsMin IS NOT NULL OR YY.CampaignGoalConversions IS NOT NULL)
GROUP BY CampaignDisplayName, CampaignGoalVisitors, CampaignGoalVisitorsMin, GoalValueMin, GoalValue', 0, 718, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', '6aee0910-1513-4392-bc8d-87f0181c7160', '20110721 14:48:03')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (519, N'table', N'table', N'SELECT
    [CampaignDisplayName] as ''{$campaignselect.itemname$}'',
    SUM(HitsCount)  as ''{$campaign.goalcurrvalue$}'',
    ISNULL(CAST (GoalValueMin AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsCount) / NULLIF (GoalValueMin,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.redflagvalue$}'',
    ISNULL(CAST (GoalValue AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsCount) / NULLIF (GoalValue,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.goalvalue$}''
   
FROM Analytics_WeekHits
    LEFT JOIN Analytics_Statistics ON HitsStatisticsID = [StatisticsID]
    LEFT JOIN Analytics_Campaign AS YY ON CampaignName = SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode))
    LEFT JOIN
        (SELECT CampaignID,
            CASE
                WHEN CampaignGoalConversionsPercent = 0  
                    THEN CampaignGoalConversionsMin
                    ELSE (CAST (CampaignImpressions AS DECIMAL (15,0)) /100) * CampaignGoalConversionsMin
                END  AS GoalValueMin,
            CASE    
                WHEN CampaignGoalConversionsPercent = 0
                    THEN CampaignGoalConversions
                    ELSE (CAST (CampaignImpressions AS DECIMAL (15,0)) /100) * CampaignGoalConversions
                END  AS GoalValue
        FROM Analytics_Campaign)
        AS XX ON XX.CampaignID = YY.CampaignID
WHERE CampaignSiteID = @CMSContextCurrentSiteID AND StatisticsSiteID = @CMSContextCurrentSiteID AND ((@CampaignName = '''' AND StatisticsCode LIKE N''campconversion;%'') OR (@CampaignName = CampaignName AND StatisticsCode = N''campconversion;'' + @CampaignName )) AND  (YY.CampaignGoalConversionsMin IS NOT NULL OR YY.CampaignGoalConversions IS NOT NULL)
GROUP BY CampaignDisplayName, CampaignGoalVisitors, CampaignGoalVisitorsMin, GoalValueMin, GoalValue', 0, 719, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', '9caaa065-ad65-42e3-beb1-23ee9e45a121', '20110721 14:48:03')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (520, N'table', N'table', N'SELECT
    [CampaignDisplayName] as ''{$campaignselect.itemname$}'',
    SUM(HitsCount)  as ''{$campaign.goalcurrvalue$}'',
    ISNULL(CAST (GoalValueMin AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsCount) / NULLIF (GoalValueMin,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.redflagvalue$}'',
    ISNULL(CAST (GoalValue AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsCount) / NULLIF (GoalValue,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.goalvalue$}''
   
FROM Analytics_HourHits
    LEFT JOIN Analytics_Statistics ON HitsStatisticsID = [StatisticsID]
    LEFT JOIN Analytics_Campaign AS YY ON CampaignName = SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode))
    LEFT JOIN
        (SELECT CampaignID,
            CASE
                WHEN CampaignGoalConversionsPercent = 0  
                    THEN CampaignGoalConversionsMin
                    ELSE (CAST (CampaignImpressions AS DECIMAL (15,0)) /100) * CampaignGoalConversionsMin
                END  AS GoalValueMin,
            CASE    
                WHEN CampaignGoalConversionsPercent = 0
                    THEN CampaignGoalConversions
                    ELSE (CAST (CampaignImpressions AS DECIMAL (15,0)) /100) * CampaignGoalConversions
                END  AS GoalValue
        FROM Analytics_Campaign)
        AS XX ON XX.CampaignID = YY.CampaignID
WHERE CampaignSiteID = @CMSContextCurrentSiteID AND StatisticsSiteID = @CMSContextCurrentSiteID AND ((@CampaignName = '''' AND StatisticsCode LIKE N''campconversion;%'') OR (@CampaignName = CampaignName AND StatisticsCode = N''campconversion;'' + @CampaignName )) AND  (YY.CampaignGoalConversionsMin IS NOT NULL OR YY.CampaignGoalConversions IS NOT NULL)
GROUP BY CampaignDisplayName, CampaignGoalVisitors, CampaignGoalVisitorsMin, GoalValueMin, GoalValue', 0, 720, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', 'b5eeb927-0885-4be2-ae68-22c7efcc929c', '20110721 14:48:04')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (521, N'table', N'table', N'SELECT
    [CampaignDisplayName] as ''{$campaignselect.itemname$}'',
    SUM(HitsCount)  as ''{$campaign.goalcurrvalue$}'',
    ISNULL(CAST (GoalValueMin AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsCount) / NULLIF (GoalValueMin,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.redflagvalue$}'',
    ISNULL(CAST (GoalValue AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsCount) / NULLIF (GoalValue,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.goalvalue$}''
   
FROM Analytics_DayHits
    LEFT JOIN Analytics_Statistics ON HitsStatisticsID = [StatisticsID]
    LEFT JOIN Analytics_Campaign AS YY ON CampaignName = SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode))
    LEFT JOIN
        (SELECT CampaignID,
            CASE
                WHEN CampaignGoalConversionsPercent = 0  
                    THEN CampaignGoalConversionsMin
                    ELSE (CAST (CampaignImpressions AS DECIMAL (15,0)) /100) * CampaignGoalConversionsMin
                END  AS GoalValueMin,
            CASE    
                WHEN CampaignGoalConversionsPercent = 0
                    THEN CampaignGoalConversions
                    ELSE (CAST (CampaignImpressions AS DECIMAL (15,0)) /100) * CampaignGoalConversions
                END  AS GoalValue
        FROM Analytics_Campaign)
        AS XX ON XX.CampaignID = YY.CampaignID
WHERE CampaignSiteID = @CMSContextCurrentSiteID AND StatisticsSiteID = @CMSContextCurrentSiteID AND ((@CampaignName = '''' AND StatisticsCode LIKE N''campconversion;%'') OR (@CampaignName = CampaignName AND StatisticsCode = N''campconversion;'' + @CampaignName )) AND  (YY.CampaignGoalConversionsMin IS NOT NULL OR YY.CampaignGoalConversions IS NOT NULL)
GROUP BY CampaignDisplayName, CampaignGoalVisitors, CampaignGoalVisitorsMin, GoalValueMin, GoalValue', 0, 721, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'f2bf8c81-6729-4b1a-b2a7-66be4a590cac', '20110721 14:48:00')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (525, N'table', N'table', N'SELECT
    [CampaignDisplayName] as ''{$campaignselect.itemname$}'',
    SUM(HitsCount)  as ''{$campaign.goalcurrvalue$}'',
    ISNULL(CAST (GoalValueMin AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsCount) / NULLIF (GoalValueMin,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.redflagvalue$}'',
    ISNULL(CAST (GoalValue AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsCount) / NULLIF (GoalValue,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.goalvalue$}''
FROM Analytics_YearHits
    LEFT JOIN Analytics_Statistics ON HitsStatisticsID = [StatisticsID]
    LEFT JOIN Analytics_Campaign AS YY ON CampaignName = [StatisticsObjectName]
    LEFT JOIN
        (SELECT CampaignID,
            CASE
                WHEN CampaignGoalVisitorsPercent = 0  
                    THEN CampaignGoalVisitorsMin
                    ELSE (CAST (CampaignImpressions AS DECIMAL (15,0)) /100) * CampaignGoalVisitorsMin
                END  AS GoalValueMin,
            CASE    
                WHEN CampaignGoalVisitorsPercent = 0
                    THEN CampaignGoalVisitors
                    ELSE (CAST (CampaignImpressions AS DECIMAL (15,0)) /100) * CampaignGoalVisitors
                END  AS GoalValue
        FROM Analytics_Campaign)
        AS XX ON XX.CampaignID = YY.CampaignID
WHERE CampaignSiteID = @CMSContextCurrentSiteID AND StatisticsSiteID = @CMSContextCurrentSiteID AND (@CampaignName = '''' OR @CampaignName = CampaignName ) AND StatisticsCode = ''campaign'' AND (YY.CampaignGoalVisitorsMin IS NOT NULL OR YY.CampaignGoalVisitors IS NOT NULL)
GROUP BY StatisticsObjectName,CampaignDisplayName, CampaignGoalVisitors, CampaignGoalVisitorsMin, GoalValueMin, GoalValue', 0, 725, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'b8dd895f-9c88-4477-886d-e3521d28a1ec', '20110721 14:50:38')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (526, N'table', N'table', N'SELECT
    [CampaignDisplayName] as ''{$campaignselect.itemname$}'',
    SUM(HitsCount)  as ''{$campaign.goalcurrvalue$}'',
    ISNULL(CAST (GoalValueMin AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsCount) / NULLIF (GoalValueMin,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.redflagvalue$}'',
    ISNULL(CAST (GoalValue AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsCount) / NULLIF (GoalValue,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.goalvalue$}''
FROM Analytics_WeekHits
    LEFT JOIN Analytics_Statistics ON HitsStatisticsID = [StatisticsID]
    LEFT JOIN Analytics_Campaign AS YY ON CampaignName = [StatisticsObjectName]
    LEFT JOIN
        (SELECT CampaignID,
            CASE
                WHEN CampaignGoalVisitorsPercent = 0  
                    THEN CampaignGoalVisitorsMin
                    ELSE (CAST (CampaignImpressions AS DECIMAL (15,0)) /100) * CampaignGoalVisitorsMin
                END  AS GoalValueMin,
            CASE    
                WHEN CampaignGoalVisitorsPercent = 0
                    THEN CampaignGoalVisitors
                    ELSE (CAST (CampaignImpressions AS DECIMAL (15,0)) /100) * CampaignGoalVisitors
                END  AS GoalValue
        FROM Analytics_Campaign)
        AS XX ON XX.CampaignID = YY.CampaignID
WHERE CampaignSiteID = @CMSContextCurrentSiteID AND StatisticsSiteID = @CMSContextCurrentSiteID AND (@CampaignName = '''' OR @CampaignName = CampaignName ) AND StatisticsCode = ''campaign'' AND (YY.CampaignGoalVisitorsMin IS NOT NULL OR YY.CampaignGoalVisitors IS NOT NULL)
GROUP BY StatisticsObjectName,CampaignDisplayName, CampaignGoalVisitors, CampaignGoalVisitorsMin, GoalValueMin, GoalValue', 0, 726, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'c666788c-25c4-44d1-a09e-2068be6b8b98', '20110721 14:50:38')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (527, N'table', N'table', N'SELECT
    [CampaignDisplayName] as ''{$campaignselect.itemname$}'',
    SUM(HitsCount)  as ''{$campaign.goalcurrvalue$}'',
    ISNULL(CAST (GoalValueMin AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsCount) / NULLIF (GoalValueMin,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.redflagvalue$}'',
    ISNULL(CAST (GoalValue AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsCount) / NULLIF (GoalValue,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.goalvalue$}''
FROM Analytics_MonthHits
    LEFT JOIN Analytics_Statistics ON HitsStatisticsID = [StatisticsID]
    LEFT JOIN Analytics_Campaign AS YY ON CampaignName = [StatisticsObjectName]
    LEFT JOIN
        (SELECT CampaignID,
            CASE
                WHEN CampaignGoalVisitorsPercent = 0  
                    THEN CampaignGoalVisitorsMin
                    ELSE (CAST (CampaignImpressions AS DECIMAL (15,0)) /100) * CampaignGoalVisitorsMin
                END  AS GoalValueMin,
            CASE    
                WHEN CampaignGoalVisitorsPercent = 0
                    THEN CampaignGoalVisitors
                    ELSE (CAST (CampaignImpressions AS DECIMAL (15,0)) /100) * CampaignGoalVisitors
                END  AS GoalValue
        FROM Analytics_Campaign)
        AS XX ON XX.CampaignID = YY.CampaignID
WHERE CampaignSiteID = @CMSContextCurrentSiteID AND StatisticsSiteID = @CMSContextCurrentSiteID AND (@CampaignName = '''' OR @CampaignName = CampaignName ) AND StatisticsCode = ''campaign'' AND (YY.CampaignGoalVisitorsMin IS NOT NULL OR YY.CampaignGoalVisitors IS NOT NULL)
GROUP BY StatisticsObjectName,CampaignDisplayName, CampaignGoalVisitors, CampaignGoalVisitorsMin, GoalValueMin, GoalValue', 0, 727, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'fef4a8f1-cd86-4174-91d8-8aff03f4314a', '20110721 14:50:38')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (528, N'table', N'table', N'SELECT
    [CampaignDisplayName] as ''{$campaignselect.itemname$}'',
    SUM(HitsCount)  as ''{$campaign.goalcurrvalue$}'',
    ISNULL(CAST (GoalValueMin AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsCount) / NULLIF (GoalValueMin,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.redflagvalue$}'',
    ISNULL(CAST (GoalValue AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsCount) / NULLIF (GoalValue,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.goalvalue$}''
FROM Analytics_HourHits
    LEFT JOIN Analytics_Statistics ON HitsStatisticsID = [StatisticsID]
    LEFT JOIN Analytics_Campaign AS YY ON CampaignName = [StatisticsObjectName]
    LEFT JOIN
        (SELECT CampaignID,
            CASE
                WHEN CampaignGoalVisitorsPercent = 0  
                    THEN CampaignGoalVisitorsMin
                    ELSE (CAST (CampaignImpressions AS DECIMAL (15,0)) /100) * CampaignGoalVisitorsMin
                END  AS GoalValueMin,
            CASE    
                WHEN CampaignGoalVisitorsPercent = 0
                    THEN CampaignGoalVisitors
                    ELSE (CAST (CampaignImpressions AS DECIMAL (15,0)) /100) * CampaignGoalVisitors
                END  AS GoalValue
        FROM Analytics_Campaign)
        AS XX ON XX.CampaignID = YY.CampaignID
WHERE CampaignSiteID = @CMSContextCurrentSiteID AND StatisticsSiteID = @CMSContextCurrentSiteID AND (@CampaignName = '''' OR @CampaignName = CampaignName ) AND StatisticsCode = ''campaign'' AND (YY.CampaignGoalVisitorsMin IS NOT NULL OR YY.CampaignGoalVisitors IS NOT NULL)
GROUP BY StatisticsObjectName,CampaignDisplayName, CampaignGoalVisitors, CampaignGoalVisitorsMin, GoalValueMin, GoalValue', 0, 728, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '914dbace-0510-4a63-9ada-edb483e12b0e', '20110721 14:50:38')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (529, N'table', N'table', N'SELECT
    [CampaignDisplayName] as ''{$campaignselect.itemname$}'',
    SUM(HitsCount)  as ''{$campaign.goalcurrvalue$}'',
    ISNULL(CAST (GoalValueMin AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsCount) / NULLIF (GoalValueMin,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.redflagvalue$}'',
    ISNULL(CAST (GoalValue AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsCount) / NULLIF (GoalValue,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.goalvalue$}''
FROM Analytics_DayHits
    LEFT JOIN Analytics_Statistics ON HitsStatisticsID = [StatisticsID]
    LEFT JOIN Analytics_Campaign AS YY ON CampaignName = [StatisticsObjectName]
    LEFT JOIN
        (SELECT CampaignID,
            CASE
                WHEN CampaignGoalVisitorsPercent = 0  
                    THEN CampaignGoalVisitorsMin
                    ELSE (CAST (CampaignImpressions AS DECIMAL (15,0)) /100) * CampaignGoalVisitorsMin
                END  AS GoalValueMin,
            CASE    
                WHEN CampaignGoalVisitorsPercent = 0
                    THEN CampaignGoalVisitors
                    ELSE (CAST (CampaignImpressions AS DECIMAL (15,0)) /100) * CampaignGoalVisitors
                END  AS GoalValue
        FROM Analytics_Campaign)
        AS XX ON XX.CampaignID = YY.CampaignID
WHERE CampaignSiteID = @CMSContextCurrentSiteID AND StatisticsSiteID = @CMSContextCurrentSiteID AND (@CampaignName = '''' OR @CampaignName = CampaignName ) AND StatisticsCode = ''campaign'' AND (YY.CampaignGoalVisitorsMin IS NOT NULL OR YY.CampaignGoalVisitors IS NOT NULL)
GROUP BY StatisticsObjectName,CampaignDisplayName, CampaignGoalVisitors, CampaignGoalVisitorsMin, GoalValueMin, GoalValue', 0, 729, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', '6c200810-1c5c-426a-8fb7-51b7ac00c668', '20110721 14:50:38')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (530, N'table', N'table', N'SELECT
    [CampaignDisplayName] as ''{$campaignselect.itemname$}'',
    SUM(HitsValue)  as ''{$campaign.goalcurrvalue$}'',
    ISNULL(CAST (GoalValueMin AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsValue) / NULLIF (GoalValueMin,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.redflagvalue$}'',
    ISNULL(CAST (GoalValue AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsValue) / NULLIF (GoalValue,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.goalvalue$}''
FROM Analytics_YearHits
    LEFT JOIN Analytics_Statistics ON HitsStatisticsID = [StatisticsID]
    LEFT JOIN Analytics_Campaign AS YY ON CampaignName = SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode))
    LEFT JOIN
        (SELECT CampaignID,
            CASE
                WHEN CampaignGoalValuePercent = 0  
                    THEN CampaignGoalValueMin
                    ELSE (CAST (CampaignTotalCost AS DECIMAL (15,0)) /100) * CampaignGoalValueMin
                END  AS GoalValueMin,
            CASE    
                WHEN CampaignGoalValuePercent = 0
                    THEN CampaignGoalValue
                    ELSE (CAST (CampaignTotalCost AS DECIMAL (15,0)) /100) * CampaignGoalValue
                END  AS GoalValue
        FROM Analytics_Campaign)
        AS XX ON XX.CampaignID = YY.CampaignID
WHERE CampaignSiteID = @CMSContextCurrentSiteID AND StatisticsSiteID = @CMSContextCurrentSiteID AND ((@CampaignName = '''' AND StatisticsCode LIKE N''campconversion;%'') OR (@CampaignName = CampaignName AND StatisticsCode = N''campconversion;'' + @CampaignName )) AND  (YY.CampaignGoalValueMin IS NOT NULL OR YY.CampaignGoalValue IS NOT NULL)
GROUP BY CampaignDisplayName, CampaignGoalVisitors, CampaignGoalVisitorsMin, GoalValueMin, GoalValue', 0, 730, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '519d4164-ced8-4b46-bf56-0f92a1da696f', '20110721 14:51:22')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (531, N'table', N'table', N'SELECT
    [CampaignDisplayName] as ''{$campaignselect.itemname$}'',
    SUM(HitsValue)  as ''{$campaign.goalcurrvalue$}'',
    ISNULL(CAST (GoalValueMin AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsValue) / NULLIF (GoalValueMin,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.redflagvalue$}'',
    ISNULL(CAST (GoalValue AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsValue) / NULLIF (GoalValue,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.goalvalue$}''
FROM Analytics_WeekHits
    LEFT JOIN Analytics_Statistics ON HitsStatisticsID = [StatisticsID]
    LEFT JOIN Analytics_Campaign AS YY ON CampaignName = SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode))
    LEFT JOIN
        (SELECT CampaignID,
            CASE
                WHEN CampaignGoalValuePercent = 0  
                    THEN CampaignGoalValueMin
                    ELSE (CAST (CampaignTotalCost AS DECIMAL (15,0)) /100) * CampaignGoalValueMin
                END  AS GoalValueMin,
            CASE    
                WHEN CampaignGoalValuePercent = 0
                    THEN CampaignGoalValue
                    ELSE (CAST (CampaignTotalCost AS DECIMAL (15,0)) /100) * CampaignGoalValue
                END  AS GoalValue
        FROM Analytics_Campaign)
        AS XX ON XX.CampaignID = YY.CampaignID
WHERE CampaignSiteID = @CMSContextCurrentSiteID AND StatisticsSiteID = @CMSContextCurrentSiteID AND ((@CampaignName = '''' AND StatisticsCode LIKE N''campconversion;%'') OR (@CampaignName = CampaignName AND StatisticsCode = N''campconversion;'' + @CampaignName )) AND  (YY.CampaignGoalValueMin IS NOT NULL OR YY.CampaignGoalValue IS NOT NULL)
GROUP BY CampaignDisplayName, CampaignGoalVisitors, CampaignGoalVisitorsMin, GoalValueMin, GoalValue', 0, 731, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'b09120b4-0a9a-44d9-80a6-219c38000e5a', '20110721 14:51:23')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (532, N'table', N'table', N'SELECT
    [CampaignDisplayName] as ''{$campaignselect.itemname$}'',
    SUM(HitsValue)  as ''{$campaign.goalcurrvalue$}'',
    ISNULL(CAST (GoalValueMin AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsValue) / NULLIF (GoalValueMin,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.redflagvalue$}'',
    ISNULL(CAST (GoalValue AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsValue) / NULLIF (GoalValue,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.goalvalue$}''
FROM Analytics_MonthHits
    LEFT JOIN Analytics_Statistics ON HitsStatisticsID = [StatisticsID]
    LEFT JOIN Analytics_Campaign AS YY ON CampaignName = SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode))
    LEFT JOIN
        (SELECT CampaignID,
            CASE
                WHEN CampaignGoalValuePercent = 0  
                    THEN CampaignGoalValueMin
                    ELSE (CAST (CampaignTotalCost AS DECIMAL (15,0)) /100) * CampaignGoalValueMin
                END  AS GoalValueMin,
            CASE    
                WHEN CampaignGoalValuePercent = 0
                    THEN CampaignGoalValue
                    ELSE (CAST (CampaignTotalCost AS DECIMAL (15,0)) /100) * CampaignGoalValue
                END  AS GoalValue
        FROM Analytics_Campaign)
        AS XX ON XX.CampaignID = YY.CampaignID
WHERE CampaignSiteID = @CMSContextCurrentSiteID AND StatisticsSiteID = @CMSContextCurrentSiteID AND ((@CampaignName = '''' AND StatisticsCode LIKE N''campconversion;%'') OR (@CampaignName = CampaignName AND StatisticsCode = N''campconversion;'' + @CampaignName )) AND  (YY.CampaignGoalValueMin IS NOT NULL OR YY.CampaignGoalValue IS NOT NULL)
GROUP BY CampaignDisplayName, CampaignGoalVisitors, CampaignGoalVisitorsMin, GoalValueMin, GoalValue', 0, 732, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '0486ede0-f8c3-423c-aa0d-5614f120afe5', '20110721 14:51:23')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (533, N'table', N'table', N'SELECT
    [CampaignDisplayName] as ''{$campaignselect.itemname$}'',
    SUM(HitsValue)  as ''{$campaign.goalcurrvalue$}'',
    ISNULL(CAST (GoalValueMin AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsValue) / NULLIF (GoalValueMin,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.redflagvalue$}'',
    ISNULL(CAST (GoalValue AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsValue) / NULLIF (GoalValue,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.goalvalue$}''
FROM Analytics_HourHits
    LEFT JOIN Analytics_Statistics ON HitsStatisticsID = [StatisticsID]
    LEFT JOIN Analytics_Campaign AS YY ON CampaignName = SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode))
    LEFT JOIN
        (SELECT CampaignID,
            CASE
                WHEN CampaignGoalValuePercent = 0  
                    THEN CampaignGoalValueMin
                    ELSE (CAST (CampaignTotalCost AS DECIMAL (15,0)) /100) * CampaignGoalValueMin
                END  AS GoalValueMin,
            CASE    
                WHEN CampaignGoalValuePercent = 0
                    THEN CampaignGoalValue
                    ELSE (CAST (CampaignTotalCost AS DECIMAL (15,0)) /100) * CampaignGoalValue
                END  AS GoalValue
        FROM Analytics_Campaign)
        AS XX ON XX.CampaignID = YY.CampaignID
WHERE CampaignSiteID = @CMSContextCurrentSiteID AND StatisticsSiteID = @CMSContextCurrentSiteID AND ((@CampaignName = '''' AND StatisticsCode LIKE N''campconversion;%'') OR (@CampaignName = CampaignName AND StatisticsCode = N''campconversion;'' + @CampaignName )) AND  (YY.CampaignGoalValueMin IS NOT NULL OR YY.CampaignGoalValue IS NOT NULL)
GROUP BY CampaignDisplayName, CampaignGoalVisitors, CampaignGoalVisitorsMin, GoalValueMin, GoalValue', 0, 733, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '9a6da8c8-f15d-42c9-b0a1-647230910729', '20110721 14:51:23')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (534, N'table', N'table', N'SELECT
    [CampaignDisplayName] as ''{$campaignselect.itemname$}'',
    SUM(HitsValue)  as ''{$campaign.goalcurrvalue$}'',
    ISNULL(CAST (GoalValueMin AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsValue) / NULLIF (GoalValueMin,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.redflagvalue$}'',
    ISNULL(CAST (GoalValue AS NVARCHAR (20))+'' ('' + CAST (CAST (SUM(HitsValue) / NULLIF (GoalValue,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.goalvalue$}''
FROM Analytics_DayHits
    LEFT JOIN Analytics_Statistics ON HitsStatisticsID = [StatisticsID]
    LEFT JOIN Analytics_Campaign AS YY ON CampaignName = SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode))
    LEFT JOIN
        (SELECT CampaignID,
            CASE
                WHEN CampaignGoalValuePercent = 0  
                    THEN CampaignGoalValueMin
                    ELSE (CAST (CampaignTotalCost AS DECIMAL (15,0)) /100) * CampaignGoalValueMin
                END  AS GoalValueMin,
            CASE    
                WHEN CampaignGoalValuePercent = 0
                    THEN CampaignGoalValue
                    ELSE (CAST (CampaignTotalCost AS DECIMAL (15,0)) /100) * CampaignGoalValue
                END  AS GoalValue
        FROM Analytics_Campaign)
        AS XX ON XX.CampaignID = YY.CampaignID
WHERE CampaignSiteID = @CMSContextCurrentSiteID AND StatisticsSiteID = @CMSContextCurrentSiteID AND ((@CampaignName = '''' AND StatisticsCode LIKE N''campconversion;%'') OR (@CampaignName = CampaignName AND StatisticsCode = N''campconversion;'' + @CampaignName )) AND  (YY.CampaignGoalValueMin IS NOT NULL OR YY.CampaignGoalValue IS NOT NULL)
GROUP BY CampaignDisplayName, CampaignGoalVisitors, CampaignGoalVisitorsMin, GoalValueMin, GoalValue', 0, 734, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', '897b07d0-ff28-4835-9639-c6860678916f', '20110721 14:51:23')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (535, N'table', N'table', N'SELECT CampaignDisplayName AS ''{$campaignselect.itemname$}'',
  CAST (SUM(Y.Value)/SUM(X.Visits) AS DECIMAL (15,2)) AS ''{$campaign.goalcurrvalue$}'',    
    ISNULL(CAST (GoalValueMin AS NVARCHAR (20))+'' ('' + CAST (CAST (CAST (SUM(Y.Value)/SUM(X.Visits) AS DECIMAL (15,0)) / NULLIF (GoalValueMin,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.redflagvalue$}'',
    ISNULL(CAST (GoalValue AS NVARCHAR (20))+'' ('' + CAST (CAST (CAST (SUM(Y.Value)/SUM(X.Visits) AS DECIMAL (15,0)) / NULLIF (GoalValue,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.goalvalue$}''
 FROM Analytics_Statistics
 -- Visit
 LEFT JOIN (SELECT HitsStatisticsID, SUM(HitsCount) AS Visits FROM Analytics_YearHits GROUP BY HitsStatisticsID) AS X ON StatisticsCode=''campaign'' AND StatisticsSiteID = @CMSContextCurrentSiteID
  AND X.HitsStatisticsID = StatisticsID
 
 -- Value
 LEFT JOIN (SELECT HitsStatisticsID, SUM(HitsValue) AS Value FROM Analytics_YearHits GROUP BY HitsStatisticsID) AS Y ON StatisticsCode LIKE''campconversion;%'' AND StatisticsSiteID = @CMSContextCurrentSiteID
  AND Y.HitsStatisticsID = StatisticsID
 -- Campaign and its goal    
 LEFT JOIN
        (SELECT CampaignDisplayName,CampaignName,CampaignSiteID,
            CASE
                WHEN CampaignGoalPerVisitorPercent = 0  
                    THEN CampaignGoalPerVisitorMin
                    ELSE ((CAST (CampaignImpressions AS DECIMAL (15,0)) /NULLIF(CampaignTotalCost,0)) * (CAST(CampaignGoalPerVisitorMin AS DECIMAL (15,0)) /100))
                END  AS GoalValueMin,
            CASE    
                WHEN CampaignGoalPerVisitorPercent = 0
                    THEN CampaignGoalPerVisitor
                    ELSE ((CAST (CampaignImpressions AS DECIMAL (15,0)) /NULLIF(CampaignTotalCost,0))  * (CAST(CampaignGoalPerVisitor AS DECIMAL(15,0)) /100))
                END  AS GoalValue
        FROM Analytics_Campaign)
        AS XX ON (XX.CampaignName = SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode)) OR (StatisticsCode =''campaign'' AND statisticsObjectName =CampaignName))
      AND CampaignSiteID = StatisticsSiteID
 WHERE @CampaignName ='''' OR @CampaignName=CampaignName AND StatisticsSiteID = @CMSContextCurrentSiteID      
 GROUP BY CampaignDisplayName,GoalValueMin,GoalValue
 HAVING  SUM(X.Visits) IS NOT NULL OR SUM(Y.Value) IS NOT NULL', 0, 735, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', '2ed56811-eb6c-45e5-be82-8212dfbbd769', '20110721 14:54:57')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (536, N'table', N'table', N'SELECT CampaignDisplayName AS ''{$campaignselect.itemname$}'',
  CAST (SUM(Y.Value)/SUM(X.Visits) AS DECIMAL (15,2)) AS ''{$campaign.goalcurrvalue$}'',    
    ISNULL(CAST (GoalValueMin AS NVARCHAR (20))+'' ('' + CAST (CAST (CAST (SUM(Y.Value)/SUM(X.Visits) AS DECIMAL (15,0)) / NULLIF (GoalValueMin,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.redflagvalue$}'',
    ISNULL(CAST (GoalValue AS NVARCHAR (20))+'' ('' + CAST (CAST (CAST (SUM(Y.Value)/SUM(X.Visits) AS DECIMAL (15,0)) / NULLIF (GoalValue,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.goalvalue$}''
 FROM Analytics_Statistics
 -- Visit
 LEFT JOIN (SELECT HitsStatisticsID, SUM(HitsCount) AS Visits FROM Analytics_WeekHits GROUP BY HitsStatisticsID) AS X ON StatisticsCode=''campaign'' AND StatisticsSiteID = @CMSContextCurrentSiteID
  AND X.HitsStatisticsID = StatisticsID
 
 -- Value
 LEFT JOIN (SELECT HitsStatisticsID, SUM(HitsValue) AS Value FROM Analytics_WeekHits GROUP BY HitsStatisticsID) AS Y ON StatisticsCode LIKE''campconversion;%'' AND StatisticsSiteID = @CMSContextCurrentSiteID
  AND Y.HitsStatisticsID = StatisticsID
 -- Campaign and its goal    
 LEFT JOIN
        (SELECT CampaignDisplayName,CampaignName,CampaignSiteID,
            CASE
                WHEN CampaignGoalPerVisitorPercent = 0  
                    THEN CampaignGoalPerVisitorMin
                    ELSE ((CAST (CampaignImpressions AS DECIMAL (15,0)) /NULLIF(CampaignTotalCost,0)) * (CAST(CampaignGoalPerVisitorMin AS DECIMAL (15,0)) /100))
                END  AS GoalValueMin,
            CASE    
                WHEN CampaignGoalPerVisitorPercent = 0
                    THEN CampaignGoalPerVisitor
                    ELSE ((CAST (CampaignImpressions AS DECIMAL (15,0)) /NULLIF(CampaignTotalCost,0))  * (CAST(CampaignGoalPerVisitor AS DECIMAL(15,0)) /100))
                END  AS GoalValue
        FROM Analytics_Campaign)
        AS XX ON (XX.CampaignName = SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode)) OR (StatisticsCode =''campaign'' AND statisticsObjectName =CampaignName))
      AND CampaignSiteID = StatisticsSiteID
 WHERE @CampaignName ='''' OR @CampaignName=CampaignName AND StatisticsSiteID = @CMSContextCurrentSiteID      
 GROUP BY CampaignDisplayName,GoalValueMin,GoalValue
 HAVING  SUM(X.Visits) IS NOT NULL OR SUM(Y.Value) IS NOT NULL', 0, 736, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', '368131c7-85f5-4801-b668-25ff0f0f4dc2', '20110721 14:54:57')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (538, N'table', N'table', N'SELECT CampaignDisplayName AS ''{$campaignselect.itemname$}'',
  CAST (SUM(Y.Value)/SUM(X.Visits) AS DECIMAL (15,2)) AS ''{$campaign.goalcurrvalue$}'',    
    ISNULL(CAST (GoalValueMin AS NVARCHAR (20))+'' ('' + CAST (CAST (CAST (SUM(Y.Value)/SUM(X.Visits) AS DECIMAL (15,0)) / NULLIF (GoalValueMin,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.redflagvalue$}'',
    ISNULL(CAST (GoalValue AS NVARCHAR (20))+'' ('' + CAST (CAST (CAST (SUM(Y.Value)/SUM(X.Visits) AS DECIMAL (15,0)) / NULLIF (GoalValue,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.goalvalue$}''
 FROM Analytics_Statistics
 -- Visit
 LEFT JOIN (SELECT HitsStatisticsID, SUM(HitsCount) AS Visits FROM Analytics_DayHits GROUP BY HitsStatisticsID) AS X ON StatisticsCode=''campaign'' AND StatisticsSiteID = @CMSContextCurrentSiteID
  AND X.HitsStatisticsID = StatisticsID
 
 -- Value
 LEFT JOIN (SELECT HitsStatisticsID, SUM(HitsValue) AS Value FROM Analytics_DayHits GROUP BY HitsStatisticsID) AS Y ON StatisticsCode LIKE''campconversion;%'' AND StatisticsSiteID = @CMSContextCurrentSiteID
  AND Y.HitsStatisticsID = StatisticsID
 -- Campaign and its goal    
 LEFT JOIN
        (SELECT CampaignDisplayName,CampaignName,CampaignSiteID,
            CASE
                WHEN CampaignGoalPerVisitorPercent = 0  
                    THEN CampaignGoalPerVisitorMin
                    ELSE ((CAST (CampaignImpressions AS DECIMAL (15,0)) /NULLIF(CampaignTotalCost,0)) * (CAST(CampaignGoalPerVisitorMin AS DECIMAL (15,0)) /100))
                END  AS GoalValueMin,
            CASE    
                WHEN CampaignGoalPerVisitorPercent = 0
                    THEN CampaignGoalPerVisitor
                    ELSE ((CAST (CampaignImpressions AS DECIMAL (15,0)) /NULLIF(CampaignTotalCost,0))  * (CAST(CampaignGoalPerVisitor AS DECIMAL(15,0)) /100))
                END  AS GoalValue
        FROM Analytics_Campaign)
        AS XX ON (XX.CampaignName = SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode)) OR (StatisticsCode =''campaign'' AND statisticsObjectName =CampaignName))
      AND CampaignSiteID = StatisticsSiteID
 WHERE @CampaignName ='''' OR @CampaignName=CampaignName AND StatisticsSiteID = @CMSContextCurrentSiteID      
 GROUP BY CampaignDisplayName,GoalValueMin,GoalValue
 HAVING  SUM(X.Visits) IS NOT NULL OR SUM(Y.Value) IS NOT NULL', 0, 738, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', '9b3f3a91-94b1-4dee-8e14-2e27fa7f4e7e', '20110721 14:55:01')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (539, N'table', N'table', N'SELECT CampaignDisplayName AS ''{$campaignselect.itemname$}'',
  CAST (SUM(Y.Value)/SUM(X.Visits) AS DECIMAL (15,2)) AS ''{$campaign.goalcurrvalue$}'',    
    ISNULL(CAST (GoalValueMin AS NVARCHAR (20))+'' ('' + CAST (CAST (CAST (SUM(Y.Value)/SUM(X.Visits) AS DECIMAL (15,0)) / NULLIF (GoalValueMin,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.redflagvalue$}'',
    ISNULL(CAST (GoalValue AS NVARCHAR (20))+'' ('' + CAST (CAST (CAST (SUM(Y.Value)/SUM(X.Visits) AS DECIMAL (15,0)) / NULLIF (GoalValue,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.goalvalue$}''
 FROM Analytics_Statistics
 -- Visit
 LEFT JOIN (SELECT HitsStatisticsID, SUM(HitsCount) AS Visits FROM Analytics_MonthHits GROUP BY HitsStatisticsID) AS X ON StatisticsCode=''campaign'' AND StatisticsSiteID = @CMSContextCurrentSiteID
  AND X.HitsStatisticsID = StatisticsID
 
 -- Value
 LEFT JOIN (SELECT HitsStatisticsID, SUM(HitsValue) AS Value FROM Analytics_MonthHits GROUP BY HitsStatisticsID) AS Y ON StatisticsCode LIKE''campconversion;%'' AND StatisticsSiteID = @CMSContextCurrentSiteID
  AND Y.HitsStatisticsID = StatisticsID
 -- Campaign and its goal    
 LEFT JOIN
        (SELECT CampaignDisplayName,CampaignName,CampaignSiteID,
            CASE
                WHEN CampaignGoalPerVisitorPercent = 0  
                    THEN CampaignGoalPerVisitorMin
                    ELSE ((CAST (CampaignImpressions AS DECIMAL (15,0)) /NULLIF(CampaignTotalCost,0)) * (CAST(CampaignGoalPerVisitorMin AS DECIMAL (15,0)) /100))
                END  AS GoalValueMin,
            CASE    
                WHEN CampaignGoalPerVisitorPercent = 0
                    THEN CampaignGoalPerVisitor
                    ELSE ((CAST (CampaignImpressions AS DECIMAL (15,0)) /NULLIF(CampaignTotalCost,0))  * (CAST(CampaignGoalPerVisitor AS DECIMAL(15,0)) /100))
                END  AS GoalValue
        FROM Analytics_Campaign)
        AS XX ON (XX.CampaignName = SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode)) OR (StatisticsCode =''campaign'' AND statisticsObjectName =CampaignName))
      AND CampaignSiteID = StatisticsSiteID
 WHERE @CampaignName ='''' OR @CampaignName=CampaignName AND StatisticsSiteID = @CMSContextCurrentSiteID      
 GROUP BY CampaignDisplayName,GoalValueMin,GoalValue
 HAVING  SUM(X.Visits) IS NOT NULL OR SUM(Y.Value) IS NOT NULL', 0, 739, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'fd7c82f4-8438-408e-9615-3a22d72e08d7', '20110721 14:55:01')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (540, N'table', N'table', N'SELECT CampaignDisplayName AS ''{$campaignselect.itemname$}'',
  CAST (SUM(Y.Value)/SUM(X.Visits) AS DECIMAL (15,2)) AS ''{$campaign.goalcurrvalue$}'',    
    ISNULL(CAST (GoalValueMin AS NVARCHAR (20))+'' ('' + CAST (CAST (CAST (SUM(Y.Value)/SUM(X.Visits) AS DECIMAL (15,0)) / NULLIF (GoalValueMin,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.redflagvalue$}'',
    ISNULL(CAST (GoalValue AS NVARCHAR (20))+'' ('' + CAST (CAST (CAST (SUM(Y.Value)/SUM(X.Visits) AS DECIMAL (15,0)) / NULLIF (GoalValue,0) * 100 AS DECIMAL(15,0)) AS NVARCHAR(10)) +''%)'',0) AS ''{$reports_goals.goalvalue$}''
 FROM Analytics_Statistics
 -- Visit
 LEFT JOIN (SELECT HitsStatisticsID, SUM(HitsCount) AS Visits FROM Analytics_HourHits GROUP BY HitsStatisticsID) AS X ON StatisticsCode=''campaign'' AND StatisticsSiteID = @CMSContextCurrentSiteID
  AND X.HitsStatisticsID = StatisticsID
 
 -- Value
 LEFT JOIN (SELECT HitsStatisticsID, SUM(HitsValue) AS Value FROM Analytics_HourHits GROUP BY HitsStatisticsID) AS Y ON StatisticsCode LIKE''campconversion;%'' AND StatisticsSiteID = @CMSContextCurrentSiteID
  AND Y.HitsStatisticsID = StatisticsID
 -- Campaign and its goal    
 LEFT JOIN
        (SELECT CampaignDisplayName,CampaignName,CampaignSiteID,
            CASE
                WHEN CampaignGoalPerVisitorPercent = 0  
                    THEN CampaignGoalPerVisitorMin
                    ELSE ((CAST (CampaignImpressions AS DECIMAL (15,0)) /NULLIF(CampaignTotalCost,0)) * (CAST(CampaignGoalPerVisitorMin AS DECIMAL (15,0)) /100))
                END  AS GoalValueMin,
            CASE    
                WHEN CampaignGoalPerVisitorPercent = 0
                    THEN CampaignGoalPerVisitor
                    ELSE ((CAST (CampaignImpressions AS DECIMAL (15,0)) /NULLIF(CampaignTotalCost,0))  * (CAST(CampaignGoalPerVisitor AS DECIMAL(15,0)) /100))
                END  AS GoalValue
        FROM Analytics_Campaign)
        AS XX ON (XX.CampaignName = SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode)) OR (StatisticsCode =''campaign'' AND statisticsObjectName =CampaignName))
      AND CampaignSiteID = StatisticsSiteID
 WHERE @CampaignName ='''' OR @CampaignName=CampaignName AND StatisticsSiteID = @CMSContextCurrentSiteID      
 GROUP BY CampaignDisplayName,GoalValueMin,GoalValue
 HAVING  SUM(X.Visits) IS NOT NULL OR SUM(Y.Value) IS NOT NULL', 0, 740, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', 'f3a25f8d-163b-4999-9ffc-c3f935693bf8', '20110721 14:54:57')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (542, N'TableEcommDonations', N'TableEcommDonations', N'SELECT
COM_OrderItem.OrderItemSKUName AS ''{$com.donationsreport.skuname$}'',
COM_OrderItem.OrderItemTotalPriceInMainCurrency AS ''{$com.donationsreport.donationamount$}'',    
COM_OrderItem.OrderItemIsPrivate AS ''{$com.donationsreport.donationisprivate$}'',    
(COM_Customer.CustomerFirstName + '' '' + COM_Customer.CustomerLastName) AS ''{$com.donationsreport.donor$}'',  
COM_Customer.CustomerCompany AS ''{$com.donationsreport.donorcompany$}'',
CONVERT(char(26), COM_Order.OrderDate, 101) AS ''{$com.donationsreport.orderdate$}''
FROM COM_OrderItem
JOIN COM_SKU ON COM_SKU.SKUID = COM_OrderItem.OrderItemSKUID
JOIN COM_Order ON COM_Order.OrderID = COM_OrderItem.OrderItemOrderID
JOIN COM_Customer ON COM_Customer.CustomerID = COM_Order.OrderCustomerID
WHERE (COM_Order.OrderSiteID = @CMSContextCurrentSiteID)
AND (COM_Order.OrderIsPaid = 1)
AND (COM_SKU.SKUProductType = ''DONATION'')
-- Donation name
AND (@DonationName IS NULL OR COM_OrderItem.OrderItemSKUName LIKE (''%'' + @DonationName + ''%''))
-- Donation amount
AND (@DonationAmountFrom IS NULL OR COM_OrderItem.OrderItemTotalPriceInMainCurrency >= @DonationAmountFrom)
AND (@DonationAmountTo IS NULL OR COM_OrderItem.OrderItemTotalPriceInMainCurrency <= @DonationAmountTo)
-- Donation is private
AND (@DonationIsPrivate < 0 OR CAST(ISNULL(COM_OrderItem.OrderItemIsPrivate, 0) AS NVARCHAR) = @DonationIsPrivate)
-- Donor
AND (@Donor IS NULL OR COM_Customer.CustomerFirstName LIKE (''%'' + @Donor + ''%'') OR COM_Customer.CustomerLastName LIKE (''%'' + @Donor + ''%''))
-- Donor company
AND (@DonorCompany IS NULL OR COM_Customer.CustomerCompany LIKE (''%'' + @DonorCompany + ''%''))
-- Order date
AND (@OrderDateFrom IS NULL OR (YEAR(COM_Order.OrderDate) >= YEAR(@OrderDateFrom) AND MONTH(COM_Order.OrderDate) >= MONTH(@OrderDateFrom) AND DAY(COM_Order.OrderDate) >= DAY(@OrderDateFrom)))
AND (@OrderDateTo IS NULL OR (YEAR(COM_Order.OrderDate) <= YEAR(@OrderDateTo) AND MONTH(COM_Order.OrderDate) <= MONTH(@OrderDateTo) AND DAY(COM_Order.OrderDate) <= DAY(@OrderDateTo)))
ORDER BY COM_OrderItem.OrderItemTotalPriceInMainCurrency DESC', 0, 741, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '739d8e39-8cb3-4d16-9324-650698660cad', '20110922 16:09:58')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (543, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''year'');
SELECT X.Name AS ''{$mvtcombination.name$}'',ISNULL (SUM(Y.Hits),0) AS ''{$om.selectedperiod$}'',
ISNULL(SUM(X.Hits),0) AS ''{$om.total$}''
  FROM 
(
SELECT 
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END   
    AS Name,MVTCombinationName, ISNULL(SUM (HitsCount),0) AS Hits,StatisticsObjectCulture
 FROM Analytics_Statistics
 INNER JOIN OM_MVTCombination ON 
  MVTCombinationPageTemplateID IN ( SELECT DocumentPageTemplateID FROM View_CMS_Tree_Joined WHERE NodeSiteID = @CMSContextCurrentSiteID
        AND NodeAliasPath IN(SELECT MVTestPage FROM OM_MVTest WHERE MVTestName = @MVTestName AND MVTestSiteID = @CMSContextCurrentSiteID)                        
        AND DocumentCulture = StatisticsObjectCulture)   
  LEFT JOIN Analytics_YearHits ON StatisticsID = HitsStatisticsID
 WHERE   (StatisticsSiteID = @CMSContextCurrentSiteID ) AND StatisticsCode LIKE ''mvtconversion;%'' 
 AND  @MVTestName = SUBSTRING(StatisticsCode, 15, CHARINDEX('';'',StatisticsCode,15)-15) AND
 MVTCombinationName = (SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)))
 AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY MVTCombinationName, StatisticsObjectCulture,
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END
) AS X 
LEFT JOIN (SELECT SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)) AS MVTCombinationName, SUM(HitsCount) AS Hits,
StatisticsObjectCulture
 FROM Analytics_Statistics JOIN Analytics_YearHits ON HitsStatisticsID = StatisticsID 
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND StatisticsCode LIKE ''mvtconversion;%''
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)   
  AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
  GROUP BY SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)), StatisticsObjectCulture
 )
 AS Y ON X.MVTCombinationName = Y.MVTCombinationName AND X.StatisticsObjectCulture = Y.StatisticsObjectCulture
 
 GROUP BY X.Name
 ORDER BY [{$om.total$}] Desc', 0, 663, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '32161bfc-a721-4c07-8c09-eb711ee40011', '20110908 08:54:53')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (544, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''week'');
SELECT X.Name AS ''{$mvtcombination.name$}'',ISNULL (SUM(Y.Hits),0) AS ''{$om.selectedperiod$}'',ISNULL(SUM(X.Hits),0) AS ''{$om.total$}''  FROM 
(
SELECT 
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END   
    AS Name,MVTCombinationName, ISNULL(SUM (HitsCount),0) AS Hits,StatisticsObjectCulture
 FROM Analytics_Statistics
 INNER JOIN OM_MVTCombination ON 
  MVTCombinationPageTemplateID IN ( SELECT DocumentPageTemplateID FROM View_CMS_Tree_Joined WHERE NodeSiteID = @CMSContextCurrentSiteID
        AND NodeAliasPath IN(SELECT MVTestPage FROM OM_MVTest WHERE MVTestName = @MVTestName AND MVTestSiteID = @CMSContextCurrentSiteID)                        
        AND DocumentCulture = StatisticsObjectCulture)   
  LEFT JOIN Analytics_WeekHits ON StatisticsID = HitsStatisticsID
 WHERE   (StatisticsSiteID = @CMSContextCurrentSiteID ) AND StatisticsCode LIKE ''mvtconversion;%'' 
 AND  @MVTestName = SUBSTRING(StatisticsCode, 15, CHARINDEX('';'',StatisticsCode,15)-15) AND
 MVTCombinationName = (SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)))
 AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY MVTCombinationName, StatisticsObjectCulture,
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END
) AS X 
LEFT JOIN (SELECT SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)) AS MVTCombinationName, SUM(HitsCount) AS Hits,
StatisticsObjectCulture
 FROM Analytics_Statistics JOIN Analytics_WeekHits ON HitsStatisticsID = StatisticsID 
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND StatisticsCode LIKE ''mvtconversion;%''
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)   
  AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
  GROUP BY SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)), StatisticsObjectCulture
 )
 AS Y ON X.MVTCombinationName = Y.MVTCombinationName AND X.StatisticsObjectCulture = Y.StatisticsObjectCulture
 
 
 GROUP BY X.Name
 ORDER BY [{$om.total$}] Desc', 0, 664, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '66b1a977-72bf-452f-b553-b288065e2b3c', '20110908 08:54:54')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (545, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''year'');
SELECT X.Name AS ''{$mvtcombination.name$}'',ISNULL (SUM(Y.Hits),0) AS ''{$om.selectedperiod$}'',
ISNULL(SUM(X.Hits),0) AS ''{$om.total$}''
  FROM 
(
SELECT 
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END   
    AS Name,MVTCombinationName, ISNULL(SUM (HitsCount),0) AS Hits,StatisticsObjectCulture
 FROM Analytics_Statistics
 INNER JOIN OM_MVTCombination ON 
  MVTCombinationPageTemplateID IN ( SELECT DocumentPageTemplateID FROM View_CMS_Tree_Joined WHERE NodeSiteID = @CMSContextCurrentSiteID
        AND NodeAliasPath IN(SELECT MVTestPage FROM OM_MVTest WHERE MVTestName = @MVTestName AND MVTestSiteID = @CMSContextCurrentSiteID)                        
        AND DocumentCulture = StatisticsObjectCulture)   
  LEFT JOIN Analytics_YearHits ON StatisticsID = HitsStatisticsID
 WHERE   (StatisticsSiteID = @CMSContextCurrentSiteID ) AND StatisticsCode LIKE ''mvtconversion;%'' 
 AND  @MVTestName = SUBSTRING(StatisticsCode, 15, CHARINDEX('';'',StatisticsCode,15)-15) AND
 MVTCombinationName = (SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)))
 AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY MVTCombinationName, StatisticsObjectCulture,
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END
) AS X 
LEFT JOIN (SELECT SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)) AS MVTCombinationName, SUM(HitsCount) AS Hits,
StatisticsObjectCulture
 FROM Analytics_Statistics JOIN Analytics_YearHits ON HitsStatisticsID = StatisticsID 
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND StatisticsCode LIKE ''mvtconversion;%''
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)   
  AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
  GROUP BY SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)), StatisticsObjectCulture
 )
 AS Y ON X.MVTCombinationName = Y.MVTCombinationName AND X.StatisticsObjectCulture = Y.StatisticsObjectCulture
 
 GROUP BY X.Name
 ORDER BY [{$om.total$}] Desc', 0, 665, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'd1ea1daf-3d30-4e07-a47a-3148e928b7c3', '20110908 08:54:54')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (546, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''hour'');
SELECT X.Name AS ''{$mvtcombination.name$}'',ISNULL (SUM(Y.Hits),0) AS ''{$om.selectedperiod$}'',ISNULL(SUM(X.Hits),0) AS ''{$om.total$}''  FROM 
(
SELECT 
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END   
    AS Name,MVTCombinationName, ISNULL(SUM (HitsCount),0) AS Hits,StatisticsObjectCulture
 FROM Analytics_Statistics
 INNER JOIN OM_MVTCombination ON 
  MVTCombinationPageTemplateID IN ( SELECT DocumentPageTemplateID FROM View_CMS_Tree_Joined WHERE NodeSiteID = @CMSContextCurrentSiteID
        AND NodeAliasPath IN(SELECT MVTestPage FROM OM_MVTest WHERE MVTestName = @MVTestName AND MVTestSiteID = @CMSContextCurrentSiteID)                        
        AND DocumentCulture = StatisticsObjectCulture)   
  LEFT JOIN Analytics_HourHits ON StatisticsID = HitsStatisticsID
 WHERE   (StatisticsSiteID = @CMSContextCurrentSiteID ) AND StatisticsCode LIKE ''mvtconversion;%'' 
 AND  @MVTestName = SUBSTRING(StatisticsCode, 15, CHARINDEX('';'',StatisticsCode,15)-15) AND
 MVTCombinationName = (SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)))
 AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY MVTCombinationName, StatisticsObjectCulture,
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END
) AS X 
LEFT JOIN (SELECT SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)) AS MVTCombinationName, SUM(HitsCount) AS Hits,
StatisticsObjectCulture
 FROM Analytics_Statistics JOIN Analytics_HourHits ON HitsStatisticsID = StatisticsID 
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND StatisticsCode LIKE ''mvtconversion;%''
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)   
  AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
  GROUP BY SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)), StatisticsObjectCulture
 )
 AS Y ON X.MVTCombinationName = Y.MVTCombinationName AND X.StatisticsObjectCulture = Y.StatisticsObjectCulture
 
 
 GROUP BY X.Name
 ORDER BY [{$om.total$}] Desc', 0, 666, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'ccc50348-7ca6-4e89-84e1-08a3dd838f14', '20110908 08:54:54')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (547, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day'');
SELECT X.Name AS ''{$mvtcombination.name$}'',ISNULL (SUM(Y.Hits),0) AS ''{$om.selectedperiod$}'',ISNULL(SUM(X.Hits),0) AS ''{$om.total$}''  FROM 
(
SELECT 
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END   
    AS Name,MVTCombinationName, ISNULL(SUM (HitsCount),0) AS Hits,StatisticsObjectCulture
 FROM Analytics_Statistics
 INNER JOIN OM_MVTCombination ON 
  MVTCombinationPageTemplateID IN ( SELECT DocumentPageTemplateID FROM View_CMS_Tree_Joined WHERE NodeSiteID = @CMSContextCurrentSiteID
        AND NodeAliasPath IN(SELECT MVTestPage FROM OM_MVTest WHERE MVTestName = @MVTestName AND MVTestSiteID = @CMSContextCurrentSiteID)                        
        AND DocumentCulture = StatisticsObjectCulture)   
  LEFT JOIN Analytics_DayHits ON StatisticsID = HitsStatisticsID
 WHERE   (StatisticsSiteID = @CMSContextCurrentSiteID ) AND StatisticsCode LIKE ''mvtconversion;%'' 
 AND  @MVTestName = SUBSTRING(StatisticsCode, 15, CHARINDEX('';'',StatisticsCode,15)-15) AND
 MVTCombinationName = (SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)))
 AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY MVTCombinationName, StatisticsObjectCulture,
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END
) AS X 
LEFT JOIN (SELECT SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)) AS MVTCombinationName, SUM(HitsCount) AS Hits,
StatisticsObjectCulture
 FROM Analytics_Statistics JOIN Analytics_DayHits ON HitsStatisticsID = StatisticsID 
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND StatisticsCode LIKE ''mvtconversion;%''
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)   
  AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
  GROUP BY SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)), StatisticsObjectCulture
 )
 AS Y ON X.MVTCombinationName = Y.MVTCombinationName AND X.StatisticsObjectCulture = Y.StatisticsObjectCulture
 
 
 GROUP BY X.Name
 ORDER BY [{$om.total$}] Desc', 0, 668, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '1a1f5108-30ed-4e04-9d55-21b88b93682e', '20110908 08:54:54')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (548, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''year'');
SELECT X.Name AS ''{$mvtcombination.name$}'',ISNULL (SUM(Y.Hits),0) AS ''{$om.selectedperiod$}'',ISNULL(SUM(X.Hits),0) AS ''{$om.total$}''  FROM 
(
SELECT 
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END   
    AS Name,MVTCombinationName, ISNULL(SUM (HitsValue),0) AS Hits,StatisticsObjectCulture
 FROM Analytics_Statistics
 INNER JOIN OM_MVTCombination ON 
  MVTCombinationPageTemplateID IN ( SELECT DocumentPageTemplateID FROM View_CMS_Tree_Joined WHERE NodeSiteID = @CMSContextCurrentSiteID
        AND NodeAliasPath IN(SELECT MVTestPage FROM OM_MVTest WHERE MVTestName = @MVTestName AND MVTestSiteID = @CMSContextCurrentSiteID)                        
        AND DocumentCulture = StatisticsObjectCulture)   
  LEFT JOIN Analytics_YearHits ON StatisticsID = HitsStatisticsID
 WHERE   (StatisticsSiteID = @CMSContextCurrentSiteID ) AND StatisticsCode LIKE ''mvtconversion;%'' 
 AND  @MVTestName = SUBSTRING(StatisticsCode, 15, CHARINDEX('';'',StatisticsCode,15)-15) AND
 MVTCombinationName = (SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)))
 AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY MVTCombinationName, StatisticsObjectCulture,
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END
) AS X 
LEFT JOIN (SELECT SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)) AS MVTCombinationName, SUM(HitsValue) AS Hits,
StatisticsObjectCulture
 FROM Analytics_Statistics JOIN Analytics_YearHits ON HitsStatisticsID = StatisticsID 
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND StatisticsCode LIKE ''mvtconversion;%''
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)   
  AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
  GROUP BY SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)), StatisticsObjectCulture
 )
 AS Y ON X.MVTCombinationName = Y.MVTCombinationName AND X.StatisticsObjectCulture = Y.StatisticsObjectCulture
 
 
 GROUP BY X.Name
 ORDER BY {$om.total$} Desc', 0, 669, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'cd689d46-6197-4bd0-9db3-fc36000bab12', '20110908 08:53:32')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (549, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''week'');
SELECT X.Name AS ''{$mvtcombination.name$}'',ISNULL (SUM(Y.Hits),0) AS ''{$om.selectedperiod$}'',ISNULL(SUM(X.Hits),0) AS ''{$om.total$}''  FROM 
(
SELECT 
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END   
    AS Name,MVTCombinationName, ISNULL(SUM (HitsValue),0) AS Hits,StatisticsObjectCulture
 FROM Analytics_Statistics
 INNER JOIN OM_MVTCombination ON 
  MVTCombinationPageTemplateID IN ( SELECT DocumentPageTemplateID FROM View_CMS_Tree_Joined WHERE NodeSiteID = @CMSContextCurrentSiteID
        AND NodeAliasPath IN(SELECT MVTestPage FROM OM_MVTest WHERE MVTestName = @MVTestName AND MVTestSiteID = @CMSContextCurrentSiteID)                        
        AND DocumentCulture = StatisticsObjectCulture)   
  LEFT JOIN Analytics_WeekHits ON StatisticsID = HitsStatisticsID
 WHERE   (StatisticsSiteID = @CMSContextCurrentSiteID ) AND StatisticsCode LIKE ''mvtconversion;%'' 
 AND  @MVTestName = SUBSTRING(StatisticsCode, 15, CHARINDEX('';'',StatisticsCode,15)-15) AND
 MVTCombinationName = (SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)))
 AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY MVTCombinationName, StatisticsObjectCulture,
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END
) AS X 
LEFT JOIN (SELECT SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)) AS MVTCombinationName, SUM(HitsValue) AS Hits,
StatisticsObjectCulture
 FROM Analytics_Statistics JOIN Analytics_WeekHits ON HitsStatisticsID = StatisticsID 
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND StatisticsCode LIKE ''mvtconversion;%''
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)   
  AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
  GROUP BY SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)), StatisticsObjectCulture
 )
 AS Y ON X.MVTCombinationName = Y.MVTCombinationName AND X.StatisticsObjectCulture = Y.StatisticsObjectCulture
 
 
 GROUP BY X.Name
 ORDER BY [{$om.total$}] Desc', 0, 673, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'e2151c94-214b-48fb-94ce-5eaaf8e6b7e8', '20110908 08:53:32')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (550, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''year'');
SELECT X.Name AS ''{$mvtcombination.name$}'',ISNULL (SUM(Y.Hits),0) AS ''{$om.selectedperiod$}'',
ISNULL(SUM(X.Hits),0) AS ''{$om.total$}''
  FROM 
(
SELECT 
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END   
    AS Name,MVTCombinationName, ISNULL(SUM (HitsValue),0) AS Hits,StatisticsObjectCulture
 FROM Analytics_Statistics
 INNER JOIN OM_MVTCombination ON 
  MVTCombinationPageTemplateID IN ( SELECT DocumentPageTemplateID FROM View_CMS_Tree_Joined WHERE NodeSiteID = @CMSContextCurrentSiteID
        AND NodeAliasPath IN(SELECT MVTestPage FROM OM_MVTest WHERE MVTestName = @MVTestName AND MVTestSiteID = @CMSContextCurrentSiteID)                        
        AND DocumentCulture = StatisticsObjectCulture)   
  LEFT JOIN Analytics_YearHits ON StatisticsID = HitsStatisticsID
 WHERE   (StatisticsSiteID = @CMSContextCurrentSiteID ) AND StatisticsCode LIKE ''mvtconversion;%'' 
 AND  @MVTestName = SUBSTRING(StatisticsCode, 15, CHARINDEX('';'',StatisticsCode,15)-15) AND
 MVTCombinationName = (SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)))
 AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY MVTCombinationName, StatisticsObjectCulture,
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END
) AS X 
LEFT JOIN (SELECT SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)) AS MVTCombinationName, SUM(HitsValue) AS Hits,
StatisticsObjectCulture
 FROM Analytics_Statistics JOIN Analytics_YearHits ON HitsStatisticsID = StatisticsID 
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND StatisticsCode LIKE ''mvtconversion;%''
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)   
  AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
  GROUP BY SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)), StatisticsObjectCulture
 )
 AS Y ON X.MVTCombinationName = Y.MVTCombinationName AND X.StatisticsObjectCulture = Y.StatisticsObjectCulture
 
 GROUP BY X.Name
 ORDER BY [{$om.total$}] Desc', 0, 672, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '314e9589-2213-4b90-86ab-5b84a27bfaa8', '20110908 08:53:32')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (551, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''hour'');
SELECT X.Name AS ''{$mvtcombination.name$}'',ISNULL (SUM(Y.Hits),0) AS ''{$om.selectedperiod$}'',ISNULL(SUM(X.Hits),0) AS ''{$om.total$}''  FROM 
(
SELECT 
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END   
    AS Name,MVTCombinationName, ISNULL(SUM (HitsValue),0) AS Hits,StatisticsObjectCulture
 FROM Analytics_Statistics
 INNER JOIN OM_MVTCombination ON 
  MVTCombinationPageTemplateID IN ( SELECT DocumentPageTemplateID FROM View_CMS_Tree_Joined WHERE NodeSiteID = @CMSContextCurrentSiteID
        AND NodeAliasPath IN(SELECT MVTestPage FROM OM_MVTest WHERE MVTestName = @MVTestName AND MVTestSiteID = @CMSContextCurrentSiteID)                        
        AND DocumentCulture = StatisticsObjectCulture)   
  LEFT JOIN Analytics_HourHits ON StatisticsID = HitsStatisticsID
 WHERE   (StatisticsSiteID = @CMSContextCurrentSiteID ) AND StatisticsCode LIKE ''mvtconversion;%'' 
 AND  @MVTestName = SUBSTRING(StatisticsCode, 15, CHARINDEX('';'',StatisticsCode,15)-15) AND
 MVTCombinationName = (SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)))
 AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY MVTCombinationName, StatisticsObjectCulture,
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END
) AS X 
LEFT JOIN (SELECT SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)) AS MVTCombinationName, SUM(HitsValue) AS Hits,
StatisticsObjectCulture
 FROM Analytics_Statistics JOIN Analytics_HourHits ON HitsStatisticsID = StatisticsID 
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND StatisticsCode LIKE ''mvtconversion;%''
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)   
  AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
  GROUP BY SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)), StatisticsObjectCulture
 )
 AS Y ON X.MVTCombinationName = Y.MVTCombinationName AND X.StatisticsObjectCulture = Y.StatisticsObjectCulture
 
 
 GROUP BY X.Name
 ORDER BY [{$om.total$}] Desc', 0, 671, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'bd7a29cd-8701-4975-a65d-87576f932017', '20110908 08:53:32')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (552, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day'');
SELECT X.Name AS ''{$mvtcombination.name$}'',ISNULL (SUM(Y.Hits),0) AS ''{$om.selectedperiod$}'',ISNULL(SUM(X.Hits),0) AS ''{$om.total$}''  FROM 
(
SELECT 
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END   
    AS Name,MVTCombinationName, ISNULL(SUM (HitsValue),0) AS Hits,StatisticsObjectCulture
 FROM Analytics_Statistics
 INNER JOIN OM_MVTCombination ON 
  MVTCombinationPageTemplateID IN ( SELECT DocumentPageTemplateID FROM View_CMS_Tree_Joined WHERE NodeSiteID = @CMSContextCurrentSiteID
        AND NodeAliasPath IN(SELECT MVTestPage FROM OM_MVTest WHERE MVTestName = @MVTestName AND MVTestSiteID = @CMSContextCurrentSiteID)                        
        AND DocumentCulture = StatisticsObjectCulture)   
  LEFT JOIN Analytics_DayHits ON StatisticsID = HitsStatisticsID
 WHERE   (StatisticsSiteID = @CMSContextCurrentSiteID ) AND StatisticsCode LIKE ''mvtconversion;%'' 
 AND  @MVTestName = SUBSTRING(StatisticsCode, 15, CHARINDEX('';'',StatisticsCode,15)-15) AND
 MVTCombinationName = (SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)))
 AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY MVTCombinationName, StatisticsObjectCulture,
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END
) AS X 
LEFT JOIN (SELECT SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)) AS MVTCombinationName, SUM(HitsValue) AS Hits,
StatisticsObjectCulture
 FROM Analytics_Statistics JOIN Analytics_DayHits ON HitsStatisticsID = StatisticsID 
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND StatisticsCode LIKE ''mvtconversion;%''
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)   
  AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
  GROUP BY SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)), StatisticsObjectCulture
 )
 AS Y ON X.MVTCombinationName = Y.MVTCombinationName AND X.StatisticsObjectCulture = Y.StatisticsObjectCulture
 
 
 GROUP BY X.Name
 ORDER BY [{$om.total$}] Desc', 0, 670, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'd30682fc-df01-48fa-bfc1-75585c8fb517', '20110908 08:53:32')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (553, N'table', N'table', N'EXEC Proc_Analytics_RemoveTempTable 
 CREATE TABLE #AnalyticsTempTable (  
  Hits DECIMAL (15,2),
  Name NVARCHAR(300) COLLATE database_default,  
  Culture NVARCHAR(300) COLLATE database_default,   
  Page NVARCHAR(300) COLLATE database_default,
  DisplayName NVARCHAR(300) COLLATE database_default     
);
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
SET @ToDate = {%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''year'');
INSERT INTO #AnalyticsTempTable (DisplayName,Hits,Name,Page,Culture)
SELECT X.Name AS DisplayName, CAST (ISNULL(X.Hits,0) AS DECIMAL (15,2)) AS Hits, X.MVTCombinationName AS Name,X.MVTestPage AS Page,X.MVTestCulture AS Culture
FROM
(
SELECT
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END   
    AS Name,MVTCombinationName, ISNULL(SUM (HitsCount),0) AS Hits ,MVTestPage, MVTestCulture
 FROM Analytics_Statistics
 INNER JOIN OM_MVTCombination ON
  MVTCombinationPageTemplateID IN ( SELECT DocumentPageTemplateID FROM View_CMS_Tree_Joined WHERE NodeSiteID = @CMSContextCurrentSiteID
        AND NodeAliasPath IN(SELECT MVTestPage FROM OM_MVTest WHERE MVTestName = @MVTestName AND MVTestSiteID = @CMSContextCurrentSiteID)                        
        AND DocumentCulture = StatisticsObjectCulture)         
        AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
  LEFT JOIN Analytics_YearHits ON StatisticsID = HitsStatisticsID  
  LEFT JOIN OM_MVTest ON MVTestName = @MVTestName AND MVTestSiteID = @CMSContextCurrentSiteID
 WHERE   (StatisticsSiteID = @CMSContextCurrentSiteID ) AND StatisticsCode LIKE ''mvtconversion;%'' AND
 @MVTestName = SUBSTRING(StatisticsCode, 15, CHARINDEX('';'',StatisticsCode,15)-15) AND
 MVTCombinationName = (SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)))
 AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
 
 GROUP BY MVTCombinationName, MVTestPage, MVTestCulture,
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END
) AS X
 
  UPDATE #AnalyticsTempTable SET Hits = Hits/
     (
   SELECT CAST (SUM (HitsCount) AS DECIMAL (15,2)) FROM Analytics_Statistics
   JOIN Analytics_YearHits ON Analytics_Statistics.StatisticsID = Analytics_YearHits.HitsStatisticsID
   WHERE Analytics_Statistics.StatisticsCode = ''pageviews''  
   AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
   AND Analytics_Statistics.StatisticsObjectID IN
   (SELECT NodeID FROM View_CMS_Tree_Joined WHERE NodeAliasPath = Page AND NodeSiteID = @CMSContextCurrentSiteID)
    AND (StatisticsObjectCulture = Culture OR Culture IS NULL)    
   )
   *100    
   
   SELECT DisplayName AS ''{$mvtcombination.name$}'',
   CAST (CAST (ISNULL (Hits,0) AS DECIMAL (15,1)) AS NVARCHAR(15)) + ''%'' AS ''{$om.selectedperiod$}'' 
   FROM #AnalyticsTempTable
   ORDER BY Hits DESC
  
 EXEC Proc_Analytics_RemoveTempTable', 0, 674, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '03b25306-c49e-49ed-a9ad-04441dc381f8', '20110901 14:02:53')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (554, N'table', N'table', N'EXEC Proc_Analytics_RemoveTempTable 
 CREATE TABLE #AnalyticsTempTable (  
  Hits DECIMAL (15,2),
  Name NVARCHAR(300) COLLATE database_default,  
  Culture NVARCHAR(300) COLLATE database_default,   
  Page NVARCHAR(300) COLLATE database_default,
  DisplayName NVARCHAR(300) COLLATE database_default     
);
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week'');
SET @ToDate = {%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''week'');
INSERT INTO #AnalyticsTempTable (DisplayName,Hits,Name,Page,Culture)
SELECT X.Name AS DisplayName, CAST (ISNULL(X.Hits,0) AS DECIMAL (15,2)) AS Hits, X.MVTCombinationName AS Name,X.MVTestPage AS Page,X.MVTestCulture AS Culture
FROM
(
SELECT
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END   
    AS Name,MVTCombinationName, ISNULL(SUM (HitsCount),0) AS Hits ,MVTestPage, MVTestCulture
 FROM Analytics_Statistics
 INNER JOIN OM_MVTCombination ON
  MVTCombinationPageTemplateID IN ( SELECT DocumentPageTemplateID FROM View_CMS_Tree_Joined WHERE NodeSiteID = @CMSContextCurrentSiteID
        AND NodeAliasPath IN(SELECT MVTestPage FROM OM_MVTest WHERE MVTestName = @MVTestName AND MVTestSiteID = @CMSContextCurrentSiteID)                        
        AND DocumentCulture = StatisticsObjectCulture)         
        AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
  LEFT JOIN Analytics_WeekHits ON StatisticsID = HitsStatisticsID  
  LEFT JOIN OM_MVTest ON MVTestName = @MVTestName AND MVTestSiteID = @CMSContextCurrentSiteID
 WHERE   (StatisticsSiteID = @CMSContextCurrentSiteID ) AND StatisticsCode LIKE ''mvtconversion;%'' AND
 @MVTestName = SUBSTRING(StatisticsCode, 15, CHARINDEX('';'',StatisticsCode,15)-15) AND
 MVTCombinationName = (SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)))
 AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
 
 GROUP BY MVTCombinationName, MVTestPage, MVTestCulture,
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END
) AS X
 
  UPDATE #AnalyticsTempTable SET Hits = Hits/
     (
   SELECT CAST (SUM (HitsCount) AS DECIMAL (15,2)) FROM Analytics_Statistics
   JOIN Analytics_WeekHits ON Analytics_Statistics.StatisticsID = Analytics_WeekHits.HitsStatisticsID
   WHERE Analytics_Statistics.StatisticsCode = ''pageviews''  
   AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
   AND Analytics_Statistics.StatisticsObjectID IN
   (SELECT NodeID FROM View_CMS_Tree_Joined WHERE NodeAliasPath = Page AND NodeSiteID = @CMSContextCurrentSiteID)
    AND (StatisticsObjectCulture = Culture OR Culture IS NULL)    
   )
   *100    
   
   SELECT DisplayName AS ''{$mvtcombination.name$}'',
   CAST (CAST (ISNULL (Hits,0) AS DECIMAL (15,1)) AS NVARCHAR(15)) + ''%'' AS ''{$om.selectedperiod$}'' 
   FROM #AnalyticsTempTable
   ORDER BY Hits DESC
  
 EXEC Proc_Analytics_RemoveTempTable', 0, 675, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'bbde24a8-646a-4f94-9c10-f0a276622f76', '20110901 14:02:53')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (555, N'table', N'table', N'EXEC Proc_Analytics_RemoveTempTable 
 CREATE TABLE #AnalyticsTempTable (  
  Hits DECIMAL (15,2),
  Name NVARCHAR(300) COLLATE database_default,  
  Culture NVARCHAR(300) COLLATE database_default,   
  Page NVARCHAR(300) COLLATE database_default,
  DisplayName NVARCHAR(300) COLLATE database_default     
);
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
SET @ToDate = {%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''month'');
INSERT INTO #AnalyticsTempTable (DisplayName,Hits,Name,Page,Culture)
SELECT X.Name AS DisplayName, CAST (ISNULL(X.Hits,0) AS DECIMAL (15,2)) AS Hits, X.MVTCombinationName AS Name,X.MVTestPage AS Page,X.MVTestCulture AS Culture
FROM
(
SELECT
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END   
    AS Name,MVTCombinationName, ISNULL(SUM (HitsCount),0) AS Hits ,MVTestPage, MVTestCulture
 FROM Analytics_Statistics
 INNER JOIN OM_MVTCombination ON
  MVTCombinationPageTemplateID IN ( SELECT DocumentPageTemplateID FROM View_CMS_Tree_Joined WHERE NodeSiteID = @CMSContextCurrentSiteID
        AND NodeAliasPath IN(SELECT MVTestPage FROM OM_MVTest WHERE MVTestName = @MVTestName AND MVTestSiteID = @CMSContextCurrentSiteID)                        
        AND DocumentCulture = StatisticsObjectCulture)         
        AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
  LEFT JOIN Analytics_MonthHits ON StatisticsID = HitsStatisticsID  
  LEFT JOIN OM_MVTest ON MVTestName = @MVTestName AND MVTestSiteID = @CMSContextCurrentSiteID
 WHERE   (StatisticsSiteID = @CMSContextCurrentSiteID ) AND StatisticsCode LIKE ''mvtconversion;%'' AND
 @MVTestName = SUBSTRING(StatisticsCode, 15, CHARINDEX('';'',StatisticsCode,15)-15) AND
 MVTCombinationName = (SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)))
 AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
 
 GROUP BY MVTCombinationName, MVTestPage, MVTestCulture,
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END
) AS X
 
  UPDATE #AnalyticsTempTable SET Hits = Hits/
     (
   SELECT CAST (SUM (HitsCount) AS DECIMAL (15,2)) FROM Analytics_Statistics
   JOIN Analytics_MonthHits ON Analytics_Statistics.StatisticsID = Analytics_MonthHits.HitsStatisticsID
   WHERE Analytics_Statistics.StatisticsCode = ''pageviews''  
   AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
   AND Analytics_Statistics.StatisticsObjectID IN
   (SELECT NodeID FROM View_CMS_Tree_Joined WHERE NodeAliasPath = Page AND NodeSiteID = @CMSContextCurrentSiteID)
    AND (StatisticsObjectCulture = Culture OR Culture IS NULL)    
   )
   *100    
   
   SELECT DisplayName AS ''{$mvtcombination.name$}'',
   CAST (CAST (ISNULL (Hits,0) AS DECIMAL (15,1)) AS NVARCHAR(15)) + ''%'' AS ''{$om.selectedperiod$}'' 
   FROM #AnalyticsTempTable
   ORDER BY Hits DESC
  
 EXEC Proc_Analytics_RemoveTempTable', 0, 676, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'ef81763b-d1a7-43e7-a14a-77d8b2e1fe53', '20110901 14:02:53')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (556, N'table', N'table', N'EXEC Proc_Analytics_RemoveTempTable 
 CREATE TABLE #AnalyticsTempTable (  
  Hits DECIMAL (15,2),
  Name NVARCHAR(300) COLLATE database_default,  
  Culture NVARCHAR(300) COLLATE database_default,   
  Page NVARCHAR(300) COLLATE database_default,
  DisplayName NVARCHAR(300) COLLATE database_default     
);
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
SET @ToDate = {%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''hour'');
INSERT INTO #AnalyticsTempTable (DisplayName,Hits,Name,Page,Culture)
SELECT X.Name AS DisplayName, CAST (ISNULL(X.Hits,0) AS DECIMAL (15,2)) AS Hits, X.MVTCombinationName AS Name,X.MVTestPage AS Page,X.MVTestCulture AS Culture
FROM
(
SELECT
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END   
    AS Name,MVTCombinationName, ISNULL(SUM (HitsCount),0) AS Hits ,MVTestPage, MVTestCulture
 FROM Analytics_Statistics
 INNER JOIN OM_MVTCombination ON
  MVTCombinationPageTemplateID IN ( SELECT DocumentPageTemplateID FROM View_CMS_Tree_Joined WHERE NodeSiteID = @CMSContextCurrentSiteID
        AND NodeAliasPath IN(SELECT MVTestPage FROM OM_MVTest WHERE MVTestName = @MVTestName AND MVTestSiteID = @CMSContextCurrentSiteID)                        
        AND DocumentCulture = StatisticsObjectCulture)         
        AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
  LEFT JOIN Analytics_HourHits ON StatisticsID = HitsStatisticsID  
  LEFT JOIN OM_MVTest ON MVTestName = @MVTestName AND MVTestSiteID = @CMSContextCurrentSiteID
 WHERE   (StatisticsSiteID = @CMSContextCurrentSiteID ) AND StatisticsCode LIKE ''mvtconversion;%'' AND
 @MVTestName = SUBSTRING(StatisticsCode, 15, CHARINDEX('';'',StatisticsCode,15)-15) AND
 MVTCombinationName = (SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)))
 AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
 
 GROUP BY MVTCombinationName, MVTestPage, MVTestCulture,
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END
) AS X
 
  UPDATE #AnalyticsTempTable SET Hits = Hits/
     (
   SELECT CAST (SUM (HitsCount) AS DECIMAL (15,2)) FROM Analytics_Statistics
   JOIN Analytics_HourHits ON Analytics_Statistics.StatisticsID = Analytics_HourHits.HitsStatisticsID
   WHERE Analytics_Statistics.StatisticsCode = ''pageviews''  
   AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
   AND Analytics_Statistics.StatisticsObjectID IN
   (SELECT NodeID FROM View_CMS_Tree_Joined WHERE NodeAliasPath = Page AND NodeSiteID = @CMSContextCurrentSiteID)
    AND (StatisticsObjectCulture = Culture OR Culture IS NULL)    
   )
   *100    
   
   SELECT DisplayName AS ''{$mvtcombination.name$}'',
   CAST (CAST (ISNULL (Hits,0) AS DECIMAL (15,1)) AS NVARCHAR(15)) + ''%'' AS ''{$om.selectedperiod$}'' 
   FROM #AnalyticsTempTable
   ORDER BY Hits DESC
  
 EXEC Proc_Analytics_RemoveTempTable', 0, 677, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '21784c11-b91f-4cc1-88c3-8119ef9552de', '20110901 14:02:53')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (557, N'table', N'table', N'EXEC Proc_Analytics_RemoveTempTable 
 CREATE TABLE #AnalyticsTempTable (  
  Hits DECIMAL (15,2),
  Name NVARCHAR(300) COLLATE database_default,  
  Culture NVARCHAR(300) COLLATE database_default,   
  Page NVARCHAR(300) COLLATE database_default,
  DisplayName NVARCHAR(300) COLLATE database_default     
);
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
SET @ToDate = {%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day'');
INSERT INTO #AnalyticsTempTable (DisplayName,Hits,Name,Page,Culture)
SELECT X.Name AS DisplayName, CAST (ISNULL(X.Hits,0) AS DECIMAL (15,2)) AS Hits, X.MVTCombinationName AS Name,X.MVTestPage AS Page,X.MVTestCulture AS Culture
FROM
(
SELECT
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END   
    AS Name,MVTCombinationName, ISNULL(SUM (HitsCount),0) AS Hits ,MVTestPage, MVTestCulture
 FROM Analytics_Statistics
 INNER JOIN OM_MVTCombination ON
  MVTCombinationPageTemplateID IN ( SELECT DocumentPageTemplateID FROM View_CMS_Tree_Joined WHERE NodeSiteID = @CMSContextCurrentSiteID
        AND NodeAliasPath IN(SELECT MVTestPage FROM OM_MVTest WHERE MVTestName = @MVTestName AND MVTestSiteID = @CMSContextCurrentSiteID)                        
        AND DocumentCulture = StatisticsObjectCulture)         
        AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
  LEFT JOIN Analytics_DayHits ON StatisticsID = HitsStatisticsID  
  LEFT JOIN OM_MVTest ON MVTestName = @MVTestName AND MVTestSiteID = @CMSContextCurrentSiteID
 WHERE   (StatisticsSiteID = @CMSContextCurrentSiteID ) AND StatisticsCode LIKE ''mvtconversion;%'' AND
 @MVTestName = SUBSTRING(StatisticsCode, 15, CHARINDEX('';'',StatisticsCode,15)-15) AND
 MVTCombinationName = (SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)))
 AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
 
 GROUP BY MVTCombinationName, MVTestPage, MVTestCulture,
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END
) AS X
 
  UPDATE #AnalyticsTempTable SET Hits = Hits/
     (
   SELECT CAST (SUM (HitsCount) AS DECIMAL (15,2)) FROM Analytics_Statistics
   JOIN Analytics_DayHits ON Analytics_Statistics.StatisticsID = Analytics_DayHits.HitsStatisticsID
   WHERE Analytics_Statistics.StatisticsCode = ''pageviews''  
   AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
   AND Analytics_Statistics.StatisticsObjectID IN
   (SELECT NodeID FROM View_CMS_Tree_Joined WHERE NodeAliasPath = Page AND NodeSiteID = @CMSContextCurrentSiteID)
    AND (StatisticsObjectCulture = Culture OR Culture IS NULL)    
   )
   *100    
   
   SELECT DisplayName AS ''{$mvtcombination.name$}'',
   CAST (CAST (ISNULL (Hits,0) AS DECIMAL (15,1)) AS NVARCHAR(15)) + ''%'' AS ''{$om.selectedperiod$}'' 
   FROM #AnalyticsTempTable
   ORDER BY Hits DESC
  
 EXEC Proc_Analytics_RemoveTempTable', 0, 678, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'df7c242b-4c45-48c2-a171-bf040143e844', '20110901 14:02:53')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (558, N'table', N'table', N'SET @FromDate = {%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
SET @ToDate = {%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''year'');
SELECT X.Name AS ''{$om.variant.tabletitle$}'',ISNULL (Y.Hits,0) AS ''{$om.selectedperiod$}'',
ISNULL(X.Hits,0) AS ''{$om.total$}''  FROM
(
SELECT ABVariantDisplayName AS Name, ISNULL(SUM (HitsCount),0) AS Hits,ABVariantName FROM Analytics_Statistics
  LEFT JOIN OM_ABVariant ON ABVariantSiteID = @CMSContextCurrentSiteID AND
    ABVariantName = SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))
  LEFT JOIN Analytics_YearHits ON StatisticsID = HitsSTatisticsID    
 WHERE   StatisticsSiteID = @CMSContextCurrentSiteID AND StatisticsCode LIKE ''abconversion;%'' AND
 @TestName = SUBSTRING(StatisticsCode, 14, CHARINDEX('';'',StatisticsCode,14)-14) AND
 ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY ABVariantDisplayName,ABVariantName
)
 AS X
LEFT JOIN (SELECT
  SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode)) AS ABVariantName, SUM(HitsCount) AS Hits FROM Analytics_Statistics
  LEFT JOIN Analytics_YearHits ON HitsStatisticsID = StatisticsID
  
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND StatisticsCode LIKE ''abconversion;%''
    AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
    AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
     
  GROUP BY SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))
)
AS Y ON X.ABVariantName = Y.ABVariantName
 
ORDER BY X.Hits Desc', 0, 429, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', '5fb8d096-7887-4d79-a9f9-556e3fc892c8', '20110809 09:55:38')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (559, N'table', N'table', N'SET @FromDate = {%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week'');
SET @ToDate = {%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''week'');
SELECT X.Name AS ''{$om.variant.tabletitle$}'',ISNULL (Y.Hits,0) AS ''{$om.selectedperiod$}'',
ISNULL(X.Hits,0) AS ''{$om.total$}''  FROM
(
SELECT ABVariantDisplayName AS Name, ISNULL(SUM (HitsCount),0) AS Hits,ABVariantName FROM Analytics_Statistics
  LEFT JOIN OM_ABVariant ON ABVariantSiteID = @CMSContextCurrentSiteID AND
    ABVariantName = SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))
  LEFT JOIN Analytics_WeekHits ON StatisticsID = HitsSTatisticsID    
 WHERE   StatisticsSiteID = @CMSContextCurrentSiteID AND StatisticsCode LIKE ''abconversion;%'' AND
 @TestName = SUBSTRING(StatisticsCode, 14, CHARINDEX('';'',StatisticsCode,14)-14) AND
 ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY ABVariantDisplayName,ABVariantName
)
 AS X
LEFT JOIN (SELECT
  SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode)) AS ABVariantName, SUM(HitsCount) AS Hits FROM Analytics_Statistics
  LEFT JOIN Analytics_WeekHits ON HitsStatisticsID = StatisticsID
  
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND StatisticsCode LIKE ''abconversion;%''
    AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
    AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
     
  GROUP BY SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))
)
AS Y ON X.ABVariantName = Y.ABVariantName
 
ORDER BY X.Hits Desc', 0, 432, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'f214eaa6-c963-4ad8-b415-40735d8565cd', '20110809 09:55:38')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (560, N'table', N'table', N'SET @FromDate = {%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
SET @ToDate = {%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''month'');
SELECT X.Name AS ''{$om.variant.tabletitle$}'',ISNULL (Y.Hits,0) AS ''{$om.selectedperiod$}'',
ISNULL(X.Hits,0) AS ''{$om.total$}''  FROM
(
SELECT ABVariantDisplayName AS Name, ISNULL(SUM (HitsCount),0) AS Hits,ABVariantName FROM Analytics_Statistics
  LEFT JOIN OM_ABVariant ON ABVariantSiteID = @CMSContextCurrentSiteID AND
    ABVariantName = SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))
  LEFT JOIN Analytics_MonthHits ON StatisticsID = HitsSTatisticsID    
 WHERE   StatisticsSiteID = @CMSContextCurrentSiteID AND StatisticsCode LIKE ''abconversion;%'' AND
 @TestName = SUBSTRING(StatisticsCode, 14, CHARINDEX('';'',StatisticsCode,14)-14) AND
 ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY ABVariantDisplayName,ABVariantName
)
 AS X
LEFT JOIN (SELECT
  SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode)) AS ABVariantName, SUM(HitsCount) AS Hits FROM Analytics_Statistics
  LEFT JOIN Analytics_MonthHits ON HitsStatisticsID = StatisticsID
  
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND StatisticsCode LIKE ''abconversion;%''
    AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
    AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
     
  GROUP BY SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))
)
AS Y ON X.ABVariantName = Y.ABVariantName
 
ORDER BY X.Hits Desc', 0, 431, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'd887e5ae-ef38-4941-9a3c-8c0a622e04fd', '20110809 09:55:38')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (561, N'table', N'table', N'SET @FromDate = {%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
SET @ToDate = {%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''hour'');
SELECT X.Name AS ''{$om.variant.tabletitle$}'',ISNULL (Y.Hits,0) AS ''{$om.selectedperiod$}'',
ISNULL(X.Hits,0) AS ''{$om.total$}''  FROM
(
SELECT ABVariantDisplayName AS Name, ISNULL(SUM (HitsCount),0) AS Hits,ABVariantName FROM Analytics_Statistics
  LEFT JOIN OM_ABVariant ON ABVariantSiteID = @CMSContextCurrentSiteID AND
    ABVariantName = SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))
  LEFT JOIN Analytics_HourHits ON StatisticsID = HitsSTatisticsID    
 WHERE   StatisticsSiteID = @CMSContextCurrentSiteID AND StatisticsCode LIKE ''abconversion;%'' AND
 @TestName = SUBSTRING(StatisticsCode, 14, CHARINDEX('';'',StatisticsCode,14)-14) AND
 ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY ABVariantDisplayName,ABVariantName
)
 AS X
LEFT JOIN (SELECT
  SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode)) AS ABVariantName, SUM(HitsCount) AS Hits FROM Analytics_Statistics
  LEFT JOIN Analytics_HourHits ON HitsStatisticsID = StatisticsID
  
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND StatisticsCode LIKE ''abconversion;%''
    AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
    AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
     
  GROUP BY SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))
)
AS Y ON X.ABVariantName = Y.ABVariantName
 
ORDER BY X.Hits Desc', 0, 433, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '1f715a33-7857-47c2-9d9d-b358720c07a1', '20110809 09:55:38')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (562, N'table', N'table', N'SET @FromDate = {%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
SET @ToDate = {%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day'');
SELECT X.Name AS ''{$om.variant.tabletitle$}'',ISNULL (Y.Hits,0) AS ''{$om.selectedperiod$}'',
ISNULL(X.Hits,0) AS ''{$om.total$}''  FROM
(
SELECT ABVariantDisplayName AS Name, ISNULL(SUM (HitsCount),0) AS Hits,ABVariantName FROM Analytics_Statistics
  LEFT JOIN OM_ABVariant ON ABVariantSiteID = @CMSContextCurrentSiteID AND
    ABVariantName = SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))
  LEFT JOIN Analytics_DayHits ON StatisticsID = HitsSTatisticsID    
 WHERE   StatisticsSiteID = @CMSContextCurrentSiteID AND StatisticsCode LIKE ''abconversion;%'' AND
 @TestName = SUBSTRING(StatisticsCode, 14, CHARINDEX('';'',StatisticsCode,14)-14) AND
 ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY ABVariantDisplayName,ABVariantName
)
 AS X
LEFT JOIN (SELECT
  SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode)) AS ABVariantName, SUM(HitsCount) AS Hits FROM Analytics_Statistics
  LEFT JOIN Analytics_DayHits ON HitsStatisticsID = StatisticsID
  
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND StatisticsCode LIKE ''abconversion;%''
    AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
    AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
     
  GROUP BY SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))
)
AS Y ON X.ABVariantName = Y.ABVariantName
 
ORDER BY X.Hits Desc', 0, 434, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '7b06c7c2-357f-4387-b652-5cc04207f2db', '20110809 09:55:38')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (563, N'table', N'table', N'SET @FromDate = {%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
SET @ToDate = {%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day'');
SELECT X.Name AS ''{$om.variant.tabletitle$}'',ISNULL (Y.Hits,0) AS ''{$om.selectedperiod$}'',
ISNULL(X.Hits,0) AS ''{$om.total$}''  FROM
(
SELECT ABVariantDisplayName AS Name, ISNULL(SUM (HitsValue),0) AS Hits,ABVariantName FROM Analytics_Statistics
  LEFT JOIN OM_ABVariant ON ABVariantSiteID = @CMSContextCurrentSiteID AND
    ABVariantName = SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))
  LEFT JOIN Analytics_DayHits ON StatisticsID = HitsSTatisticsID    
 WHERE   StatisticsSiteID = @CMSContextCurrentSiteID AND StatisticsCode LIKE ''abconversion;%'' AND
 @TestName = SUBSTRING(StatisticsCode, 14, CHARINDEX('';'',StatisticsCode,14)-14) AND
 ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY ABVariantDisplayName,ABVariantName
)
 AS X
LEFT JOIN (SELECT
  SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode)) AS ABVariantName, SUM(HitsValue) AS Hits FROM Analytics_Statistics
  LEFT JOIN Analytics_DayHits ON HitsStatisticsID = StatisticsID
  
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND StatisticsCode LIKE ''abconversion;%''
    AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
    AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
     
  GROUP BY SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))
)
AS Y ON X.ABVariantName = Y.ABVariantName
 
ORDER BY X.Hits Desc', 0, 440, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'ca18bb54-9c89-496e-9c16-d7dca4880b0d', '20110809 09:51:36')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (564, N'table', N'table', N'SET @FromDate = {%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
SET @ToDate = {%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''month'');
SELECT X.Name AS ''{$om.variant.tabletitle$}'',ISNULL (Y.Hits,0) AS ''{$om.selectedperiod$}'',
ISNULL(X.Hits,0) AS ''{$om.total$}''  FROM
(
SELECT ABVariantDisplayName AS Name, ISNULL(SUM (HitsValue),0) AS Hits,ABVariantName FROM Analytics_Statistics
  LEFT JOIN OM_ABVariant ON ABVariantSiteID = @CMSContextCurrentSiteID AND
    ABVariantName = SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))
  LEFT JOIN Analytics_MonthHits ON StatisticsID = HitsSTatisticsID    
 WHERE   StatisticsSiteID = @CMSContextCurrentSiteID AND StatisticsCode LIKE ''abconversion;%'' AND
 @TestName = SUBSTRING(StatisticsCode, 14, CHARINDEX('';'',StatisticsCode,14)-14) AND
 ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY ABVariantDisplayName,ABVariantName
)
 AS X
LEFT JOIN (SELECT
  SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode)) AS ABVariantName, SUM(HitsValue) AS Hits FROM Analytics_Statistics
  LEFT JOIN Analytics_MonthHits ON HitsStatisticsID = StatisticsID
  
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND StatisticsCode LIKE ''abconversion;%''
    AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
    AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
     
  GROUP BY SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))
)
AS Y ON X.ABVariantName = Y.ABVariantName
 
ORDER BY X.Hits Desc', 0, 438, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '8766c510-cdac-4d42-a40a-c51563d343f3', '20110809 09:51:36')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (565, N'table', N'table', N'SET @FromDate = {%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week'');
SET @ToDate = {%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''week'');
SELECT X.Name AS ''{$om.variant.tabletitle$}'',ISNULL (Y.Hits,0) AS ''{$om.selectedperiod$}'',
ISNULL(X.Hits,0) AS ''{$om.total$}''  FROM
(
SELECT ABVariantDisplayName AS Name, ISNULL(SUM (HitsValue),0) AS Hits,ABVariantName FROM Analytics_Statistics
  LEFT JOIN OM_ABVariant ON ABVariantSiteID = @CMSContextCurrentSiteID AND
    ABVariantName = SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))
  LEFT JOIN Analytics_WeekHits ON StatisticsID = HitsSTatisticsID    
 WHERE   StatisticsSiteID = @CMSContextCurrentSiteID AND StatisticsCode LIKE ''abconversion;%'' AND
 @TestName = SUBSTRING(StatisticsCode, 14, CHARINDEX('';'',StatisticsCode,14)-14) AND
 ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY ABVariantDisplayName,ABVariantName
)
 AS X
LEFT JOIN (SELECT
  SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode)) AS ABVariantName, SUM(HitsValue) AS Hits FROM Analytics_Statistics
  LEFT JOIN Analytics_WeekHits ON HitsStatisticsID = StatisticsID
  
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND StatisticsCode LIKE ''abconversion;%''
    AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
    AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
     
  GROUP BY SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))
)
AS Y ON X.ABVariantName = Y.ABVariantName
 
ORDER BY X.Hits Desc', 0, 439, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'a2fca359-0eec-4ca1-aea8-419844e8ed2b', '20110809 09:51:36')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (566, N'table', N'table', N'SET @FromDate = {%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
SET @ToDate = {%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''year'');
SELECT X.Name AS ''{$om.variant.tabletitle$}'',ISNULL (Y.Hits,0) AS ''{$om.selectedperiod$}'',
ISNULL(X.Hits,0) AS ''{$om.total$}''  FROM
(
SELECT ABVariantDisplayName AS Name, ISNULL(SUM (HitsValue),0) AS Hits,ABVariantName FROM Analytics_Statistics
  LEFT JOIN OM_ABVariant ON ABVariantSiteID = @CMSContextCurrentSiteID AND
    ABVariantName = SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))
  LEFT JOIN Analytics_YearHits ON StatisticsID = HitsSTatisticsID    
 WHERE   StatisticsSiteID = @CMSContextCurrentSiteID AND StatisticsCode LIKE ''abconversion;%'' AND
 @TestName = SUBSTRING(StatisticsCode, 14, CHARINDEX('';'',StatisticsCode,14)-14) AND
 ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY ABVariantDisplayName,ABVariantName
)
 AS X
LEFT JOIN (SELECT
  SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode)) AS ABVariantName, SUM(HitsValue) AS Hits FROM Analytics_Statistics
  LEFT JOIN Analytics_YearHits ON HitsStatisticsID = StatisticsID
  
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND StatisticsCode LIKE ''abconversion;%''
    AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
    AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
     
  GROUP BY SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))
)
AS Y ON X.ABVariantName = Y.ABVariantName
 
ORDER BY X.Hits Desc', 0, 437, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', 'b5d99d2e-f1d4-40d8-9e57-6cfa0189cec2', '20110809 09:51:36')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (567, N'table', N'table', N'SET @FromDate = {%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
SET @ToDate = {%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''hour'');
SELECT X.Name AS ''{$om.variant.tabletitle$}'',ISNULL (Y.Hits,0) AS ''{$om.selectedperiod$}'',
ISNULL(X.Hits,0) AS ''{$om.total$}''  FROM
(
SELECT ABVariantDisplayName AS Name, ISNULL(SUM (HitsValue),0) AS Hits,ABVariantName FROM Analytics_Statistics
  LEFT JOIN OM_ABVariant ON ABVariantSiteID = @CMSContextCurrentSiteID AND
    ABVariantName = SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))
  LEFT JOIN Analytics_HourHits ON StatisticsID = HitsSTatisticsID    
 WHERE   StatisticsSiteID = @CMSContextCurrentSiteID AND StatisticsCode LIKE ''abconversion;%'' AND
 @TestName = SUBSTRING(StatisticsCode, 14, CHARINDEX('';'',StatisticsCode,14)-14) AND
 ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY ABVariantDisplayName,ABVariantName
)
 AS X
LEFT JOIN (SELECT
  SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode)) AS ABVariantName, SUM(HitsValue) AS Hits FROM Analytics_Statistics
  LEFT JOIN Analytics_HourHits ON HitsStatisticsID = StatisticsID
  
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND StatisticsCode LIKE ''abconversion;%''
    AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
    AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
     
  GROUP BY SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))
)
AS Y ON X.ABVariantName = Y.ABVariantName
 
ORDER BY X.Hits Desc', 0, 441, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '5e38095a-f853-48bf-8126-32e81aad7d44', '20110809 09:51:36')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (568, N'table', N'table', N'EXEC Proc_Analytics_RemoveTempTable 
 CREATE TABLE #AnalyticsTempTable (  
  Hits DECIMAL(15,2),
  Name NVARCHAR(300) COLLATE database_default,  
  Culture NVARCHAR(300) COLLATE database_default,   
  Page NVARCHAR(300) COLLATE database_default   
);
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''year'');
INSERT INTO #AnalyticsTempTable (Name,Hits,Page,Culture)
SELECT X.Name AS Name,CAST (ISNULL (X.Hits,0) AS DECIMAL (15,2)) AS Hits, X.Page AS Page,X.Culture AS Culture  FROM
(
SELECT ABVariantDisplayName AS Name, ISNULL(SUM (HitsCount),0) AS Hits,ABVariantName,ABVariantPath AS Page, ABTestCulture AS Culture FROM Analytics_Statistics
  LEFT JOIN OM_ABVariant ON ABVariantSiteID = @CMSContextCurrentSiteID AND
    ABVariantName = SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))         
  LEFT JOIN Analytics_YearHits ON StatisticsID = HitsSTatisticsID    
  LEFT JOIN OM_ABTest ON ABTestSiteID = @CMSContextCurrentSiteID AND ABTestName = @TestName AND ABVariantTestID = ABTestID
 WHERE   StatisticsSiteID = @CMSContextCurrentSiteID AND StatisticsCode LIKE ''abconversion;%'' AND
 @TestName = SUBSTRING(StatisticsCode, 14, CHARINDEX('';'',StatisticsCode,14)-14)
 AND HitsStartTime >= @FromDate AND HitsEndTime <= @ToDate   
 AND  ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY ABVariantDisplayName,ABVariantName,ABVariantPath, ABTestCulture
)
 AS X
ORDER BY X.Hits Desc
 UPDATE #AnalyticsTempTable SET Hits = ISNULL (Hits/
 (
   SELECT CAST (SUM (HitsCount) AS DECIMAL (10,2)) FROM Analytics_Statistics
   JOIN Analytics_YearHits ON Analytics_Statistics.StatisticsID = Analytics_YearHits.HitsStatisticsID
   WHERE Analytics_Statistics.StatisticsCode = ''pageviews''  
   AND HitsStartTime >= @FromDate AND HitsEndTime <= @ToDate   
   AND Analytics_Statistics.StatisticsObjectID IN
  (SELECT NodeID FROM View_CMS_Tree_Joined WHERE NodeAliasPath = Page AND NodeSiteID = @CMSContextCurrentSiteID AND (DocumentCulture = StatisticsObjectCulture OR 
StatisticsObjectCulture IS NULL))
     /*culture */
     AND (StatisticsObjectCulture IS NULL OR StatisticsObjectCulture = Culture OR Culture IS NULL)    
   )
  *100  ,0)
  
SELECT Name AS ''{$om.variant.tabletitle$}'',
   CAST (CAST (ISNULL (Hits,0) AS DECIMAL (15,1)) AS NVARCHAR(15)) + ''%'' AS ''{$om.selectedperiod$}''  
 FROM #AnalyticsTempTable ORDER BY Hits DESC
EXEC Proc_Analytics_RemoveTempTable', 0, 443, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', '6170155e-341e-4fa4-9ff6-136b7edc6577', '20110915 09:41:14')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (569, N'table', N'table', N'EXEC Proc_Analytics_RemoveTempTable 
 CREATE TABLE #AnalyticsTempTable (  
  Hits DECIMAL(15,2),
  Name NVARCHAR(300) COLLATE database_default,  
  Culture NVARCHAR(300) COLLATE database_default,   
  Page NVARCHAR(300) COLLATE database_default   
);
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''week'');
INSERT INTO #AnalyticsTempTable (Name,Hits,Page,Culture)
SELECT X.Name AS Name,CAST (ISNULL (X.Hits,0) AS DECIMAL (15,2)) AS Hits, X.Page AS Page,X.Culture AS Culture  FROM
(
SELECT ABVariantDisplayName AS Name, ISNULL(SUM (HitsCount),0) AS Hits,ABVariantName,ABVariantPath AS Page, ABTestCulture AS Culture FROM Analytics_Statistics
  LEFT JOIN OM_ABVariant ON ABVariantSiteID = @CMSContextCurrentSiteID AND
    ABVariantName = SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))         
  LEFT JOIN Analytics_WeekHits ON StatisticsID = HitsSTatisticsID    
  LEFT JOIN OM_ABTest ON ABTestSiteID = @CMSContextCurrentSiteID AND ABTestName = @TestName AND ABVariantTestID = ABTestID
 WHERE   StatisticsSiteID = @CMSContextCurrentSiteID AND StatisticsCode LIKE ''abconversion;%'' AND
 @TestName = SUBSTRING(StatisticsCode, 14, CHARINDEX('';'',StatisticsCode,14)-14)
 AND HitsStartTime >= @FromDate AND HitsEndTime <= @ToDate   
 AND  ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY ABVariantDisplayName,ABVariantName,ABVariantPath, ABTestCulture
)
 AS X
ORDER BY X.Hits Desc
 UPDATE #AnalyticsTempTable SET Hits = ISNULL (Hits/
 (
   SELECT CAST (SUM (HitsCount) AS DECIMAL (10,2)) FROM Analytics_Statistics
   JOIN Analytics_WeekHits ON Analytics_Statistics.StatisticsID = Analytics_WeekHits.HitsStatisticsID
   WHERE Analytics_Statistics.StatisticsCode = ''pageviews''  
   AND HitsStartTime >= @FromDate AND HitsEndTime <= @ToDate   
   AND Analytics_Statistics.StatisticsObjectID IN
  (SELECT NodeID FROM View_CMS_Tree_Joined WHERE NodeAliasPath = Page AND NodeSiteID = @CMSContextCurrentSiteID AND (DocumentCulture = StatisticsObjectCulture OR 
StatisticsObjectCulture IS NULL))
     /*culture */
     AND (StatisticsObjectCulture IS NULL OR StatisticsObjectCulture = Culture OR Culture IS NULL)    
   )
  *100  ,0)
  
SELECT Name AS ''{$om.variant.tabletitle$}'',
   CAST (CAST (ISNULL (Hits,0) AS DECIMAL (15,1)) AS NVARCHAR(15)) + ''%'' AS ''{$om.selectedperiod$}''  
 FROM #AnalyticsTempTable ORDER BY Hits DESC
EXEC Proc_Analytics_RemoveTempTable', 0, 445, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', 'eb90b678-c683-42d6-90db-4b0f8e2e0a6d', '20110901 13:55:14')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (570, N'table', N'table', N'EXEC Proc_Analytics_RemoveTempTable 
 CREATE TABLE #AnalyticsTempTable (  
  Hits DECIMAL(15,2),
  Name NVARCHAR(300) COLLATE database_default,  
  Culture NVARCHAR(300) COLLATE database_default,   
  Page NVARCHAR(300) COLLATE database_default   
);
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''month'');
INSERT INTO #AnalyticsTempTable (Name,Hits,Page,Culture)
SELECT X.Name AS Name,CAST (ISNULL (X.Hits,0) AS DECIMAL (15,2)) AS Hits, X.Page AS Page,X.Culture AS Culture  FROM
(
SELECT ABVariantDisplayName AS Name, ISNULL(SUM (HitsCount),0) AS Hits,ABVariantName,ABVariantPath AS Page, ABTestCulture AS Culture FROM Analytics_Statistics
  LEFT JOIN OM_ABVariant ON ABVariantSiteID = @CMSContextCurrentSiteID AND
    ABVariantName = SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))         
  LEFT JOIN Analytics_MonthHits ON StatisticsID = HitsSTatisticsID    
  LEFT JOIN OM_ABTest ON ABTestSiteID = @CMSContextCurrentSiteID AND ABTestName = @TestName AND ABVariantTestID = ABTestID
 WHERE   StatisticsSiteID = @CMSContextCurrentSiteID AND StatisticsCode LIKE ''abconversion;%'' AND
 @TestName = SUBSTRING(StatisticsCode, 14, CHARINDEX('';'',StatisticsCode,14)-14)
 AND HitsStartTime >= @FromDate AND HitsEndTime <= @ToDate   
 AND  ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY ABVariantDisplayName,ABVariantName,ABVariantPath, ABTestCulture
)
 AS X
ORDER BY X.Hits Desc
 UPDATE #AnalyticsTempTable SET Hits = ISNULL (Hits/
 (
   SELECT CAST (SUM (HitsCount) AS DECIMAL (10,2)) FROM Analytics_Statistics
   JOIN Analytics_MonthHits ON Analytics_Statistics.StatisticsID = Analytics_MonthHits.HitsStatisticsID
   WHERE Analytics_Statistics.StatisticsCode = ''pageviews''  
   AND HitsStartTime >= @FromDate AND HitsEndTime <= @ToDate   
   AND Analytics_Statistics.StatisticsObjectID IN
  (SELECT NodeID FROM View_CMS_Tree_Joined WHERE NodeAliasPath = Page AND NodeSiteID = @CMSContextCurrentSiteID AND (DocumentCulture = StatisticsObjectCulture OR 
StatisticsObjectCulture IS NULL))
     /*culture */
     AND (StatisticsObjectCulture IS NULL OR StatisticsObjectCulture = Culture OR Culture IS NULL)    
   )
  *100  ,0)
  
SELECT Name AS ''{$om.variant.tabletitle$}'',
   CAST (CAST (ISNULL (Hits,0) AS DECIMAL (15,1)) AS NVARCHAR(15)) + ''%'' AS ''{$om.selectedperiod$}''  
 FROM #AnalyticsTempTable ORDER BY Hits DESC
EXEC Proc_Analytics_RemoveTempTable', 0, 444, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', '82733e5b-6e8b-42f2-b248-3689431e0a68', '20110901 13:55:14')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (571, N'table', N'table', N'EXEC Proc_Analytics_RemoveTempTable 
 CREATE TABLE #AnalyticsTempTable (  
  Hits DECIMAL(15,2),
  Name NVARCHAR(300) COLLATE database_default,  
  Culture NVARCHAR(300) COLLATE database_default,   
  Page NVARCHAR(300) COLLATE database_default   
);
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''hour'');
INSERT INTO #AnalyticsTempTable (Name,Hits,Page,Culture)
SELECT X.Name AS Name,CAST (ISNULL (X.Hits,0) AS DECIMAL (15,2)) AS Hits, X.Page AS Page,X.Culture AS Culture  FROM
(
SELECT ABVariantDisplayName AS Name, ISNULL(SUM (HitsCount),0) AS Hits,ABVariantName,ABVariantPath AS Page, ABTestCulture AS Culture FROM Analytics_Statistics
  LEFT JOIN OM_ABVariant ON ABVariantSiteID = @CMSContextCurrentSiteID AND
    ABVariantName = SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))         
  LEFT JOIN Analytics_HourHits ON StatisticsID = HitsSTatisticsID    
  LEFT JOIN OM_ABTest ON ABTestSiteID = @CMSContextCurrentSiteID AND ABTestName = @TestName AND ABVariantTestID = ABTestID
 WHERE   StatisticsSiteID = @CMSContextCurrentSiteID AND StatisticsCode LIKE ''abconversion;%'' AND
 @TestName = SUBSTRING(StatisticsCode, 14, CHARINDEX('';'',StatisticsCode,14)-14)
 AND HitsStartTime >= @FromDate AND HitsEndTime <= @ToDate   
 AND  ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY ABVariantDisplayName,ABVariantName,ABVariantPath, ABTestCulture
)
 AS X
ORDER BY X.Hits Desc
 UPDATE #AnalyticsTempTable SET Hits = ISNULL (Hits/
 (
   SELECT CAST (SUM (HitsCount) AS DECIMAL (10,2)) FROM Analytics_Statistics
   JOIN Analytics_HourHits ON Analytics_Statistics.StatisticsID = Analytics_HourHits.HitsStatisticsID
   WHERE Analytics_Statistics.StatisticsCode = ''pageviews''  
   AND HitsStartTime >= @FromDate AND HitsEndTime <= @ToDate   
   AND Analytics_Statistics.StatisticsObjectID IN
  (SELECT NodeID FROM View_CMS_Tree_Joined WHERE NodeAliasPath = Page AND NodeSiteID = @CMSContextCurrentSiteID AND (DocumentCulture = StatisticsObjectCulture OR 
StatisticsObjectCulture IS NULL))
     /*culture */
     AND (StatisticsObjectCulture IS NULL OR StatisticsObjectCulture = Culture OR Culture IS NULL)    
   )
  *100  ,0)
  
SELECT Name AS ''{$om.variant.tabletitle$}'',
   CAST (CAST (ISNULL (Hits,0) AS DECIMAL (15,1)) AS NVARCHAR(15)) + ''%'' AS ''{$om.selectedperiod$}''  
 FROM #AnalyticsTempTable ORDER BY Hits DESC
EXEC Proc_Analytics_RemoveTempTable', 0, 447, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', '519e9b0b-0287-426c-a24d-d06adc911b29', '20110901 13:55:14')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (572, N'table', N'table', N'EXEC Proc_Analytics_RemoveTempTable 
 CREATE TABLE #AnalyticsTempTable (  
  Hits DECIMAL(15,2),
  Name NVARCHAR(300) COLLATE database_default,  
  Culture NVARCHAR(300) COLLATE database_default,   
  Page NVARCHAR(300) COLLATE database_default   
);
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day'');
INSERT INTO #AnalyticsTempTable (Name,Hits,Page,Culture)
SELECT X.Name AS Name,CAST (ISNULL (X.Hits,0) AS DECIMAL (15,2)) AS Hits, X.Page AS Page,X.Culture AS Culture  FROM
(
SELECT ABVariantDisplayName AS Name, ISNULL(SUM (HitsCount),0) AS Hits,ABVariantName,ABVariantPath AS Page, ABTestCulture AS Culture FROM Analytics_Statistics
  LEFT JOIN OM_ABVariant ON ABVariantSiteID = @CMSContextCurrentSiteID AND
    ABVariantName = SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))         
  LEFT JOIN Analytics_DayHits ON StatisticsID = HitsSTatisticsID    
  LEFT JOIN OM_ABTest ON ABTestSiteID = @CMSContextCurrentSiteID AND ABTestName = @TestName AND ABVariantTestID = ABTestID
 WHERE   StatisticsSiteID = @CMSContextCurrentSiteID AND StatisticsCode LIKE ''abconversion;%'' AND
 @TestName = SUBSTRING(StatisticsCode, 14, CHARINDEX('';'',StatisticsCode,14)-14)
 AND HitsStartTime >= @FromDate AND HitsEndTime <= @ToDate   
 AND  ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY ABVariantDisplayName,ABVariantName,ABVariantPath, ABTestCulture
)
 AS X
ORDER BY X.Hits Desc
 UPDATE #AnalyticsTempTable SET Hits = ISNULL (Hits/
 (
   SELECT CAST (SUM (HitsCount) AS DECIMAL (10,2)) FROM Analytics_Statistics
   JOIN Analytics_DayHits ON Analytics_Statistics.StatisticsID = Analytics_DayHits.HitsStatisticsID
   WHERE Analytics_Statistics.StatisticsCode = ''pageviews''  
   AND HitsStartTime >= @FromDate AND HitsEndTime <= @ToDate   
   AND Analytics_Statistics.StatisticsObjectID IN
  (SELECT NodeID FROM View_CMS_Tree_Joined WHERE NodeAliasPath = Page AND NodeSiteID = @CMSContextCurrentSiteID AND (DocumentCulture = StatisticsObjectCulture OR 
StatisticsObjectCulture IS NULL))
     /*culture */
     AND (StatisticsObjectCulture IS NULL OR StatisticsObjectCulture = Culture OR Culture IS NULL)    
   )
  *100  ,0)
  
SELECT Name AS ''{$om.variant.tabletitle$}'',
   CAST (CAST (ISNULL (Hits,0) AS DECIMAL (15,1)) AS NVARCHAR(15)) + ''%'' AS ''{$om.selectedperiod$}''  
 FROM #AnalyticsTempTable ORDER BY Hits DESC
EXEC Proc_Analytics_RemoveTempTable', 0, 446, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', '639182ae-112b-4ce9-be1c-646f1e432b13', '20110901 13:55:14')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (580, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''year'');
SELECT TOP 100 StatisticsObjectName AS ''{$reports_countries.name_header$}'', SUM(HitsCount) AS ''{$reports_countries.hits_header$}'' FROM 
Analytics_Statistics, Analytics_YearHits WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND (StatisticsCode=@CodeName) AND (StatisticsID = HitsStatisticsID) AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate) GROUP BY StatisticsObjectName ORDER BY SUM(HitsCount) DESC', 0, 749, N'<customdata><querynorecordtext></querynorecordtext><exportenabled>True</exportenabled><pagesize>10</pagesize><enablepaging>False</enablepaging><skinid>ReportGridAnalytics</skinid><pagemode>1</pagemode></customdata>', '255b0e32-1ab5-4b2c-b358-37c8fcf42b3f', '20110818 16:18:56')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (581, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
  SELECT DATEPART(YEAR,[Date]) AS ''{$ecommerce.report_year$}'', COUNT(OrderDate) AS ''{$ecommerce.report_number$}''  FROM
  {%DatabaseSchema%}.Func_Analytics_EnsureDates (@FromDate,@ToDate,''year'') AS Dates
  LEFT JOIN COM_Order  ON   
    ([Date] <= OrderDate  AND  DATEADD(YEAR,1,[Date]) > OrderDate) AND
    OrderSiteID = @CMSContextCurrentSiteID
  GROUP BY [Date]
ORDER BY [Date] DESC', 0, 752, N'<customdata><pagesize>10</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext></querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '5f0ffe41-93ba-4e5e-b2ed-24eeb431da44', '20111003 07:21:36')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (582, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
  SELECT DATENAME(MONTH,[Date])+'', ''+ DATENAME(YEAR, [Date])  AS ''{$ecommerce.report_month$}'', COUNT(OrderDate) AS ''{$ecommerce.report_number$}''  FROM
  {%DatabaseSchema%}.Func_Analytics_EnsureDates (@FromDate,@ToDate,''month'') AS Dates
  LEFT JOIN COM_Order  ON   
    ([Date] <= OrderDate  AND  DATEADD(MONTH,1,[Date]) > OrderDate) AND
    OrderSiteID = @CMSContextCurrentSiteID
  GROUP BY [Date]
ORDER BY [Date] DESC', 0, 753, N'<customdata><pagesize>10</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext></querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', '0a09d488-a5e2-4a9c-b4c9-5a8bc91023e1', '20111003 07:21:01')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (583, N'table', N'table', N'SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
  SELECT CAST(DATEPART(MONTH,[Date]) AS NVARCHAR(2)) +''/''+ DATENAME(DAY,[Date])+''/''+DATENAME(YEAR,[Date]) AS ''{$ecommerce.report_day$}'', COUNT(OrderDate) AS ''{$ecommerce.report_number$}'' FROM 
  {%DatabaseSchema%}.Func_Analytics_EnsureDates (@FromDate,@ToDate,''day'') AS Dates
  LEFT JOIN COM_Order  ON   
    ([Date] <= OrderDate  AND  DATEADD(DAY,1,[Date]) > OrderDate) AND 
    OrderSiteID = @CMSContextCurrentSiteID
  GROUP BY [Date]
ORDER BY [Date] DESC', 0, 754, N'<customdata><pagesize>10</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext></querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', '2c56c9a0-1048-46e0-bbd5-c9ace9b35be9', '20111003 07:18:49')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (587, N'TopNContacts', N'Top N Contacts', N'SELECT TOP (@SelectTopN) 
	SUM(OM_ScoreContactRule.Value) as [{$om.score$}],
	ContactFirstName as [{$om.contact.firstname$}], 
	ContactLastName as [{$om.contact.lastname$}], 
        ContactEmail as [{$general.email$}],
        ContactIsAnonymous as [{$om.contact.isanonymous$}],
        ContactStatusName as [{$om.contactstatus$}],
        FullName as [{$om.contact.owner$}]
FROM OM_Contact
LEFT JOIN OM_ContactStatus ON OM_Contact.ContactStatusID = OM_ContactStatus.ContactStatusID
LEFT JOIN CMS_User ON CMS_User.UserID = OM_Contact.ContactOwnerUserID
INNER JOIN OM_ScoreContactRule ON OM_Contact.ContactID = OM_ScoreContactRule.ContactID
INNER JOIN OM_Score ON OM_ScoreContactRule.ScoreID = OM_Score.ScoreID
INNER JOIN OM_Rule ON OM_ScoreContactRule.RuleID = OM_Rule.RuleID
WHERE
  OM_Score.ScoreEnabled = 1
  AND
  (@ScoreID = -1 OR OM_Score.ScoreID = @ScoreID)
  AND
  OM_Score.ScoreSiteID = @CMSContextCurrentSiteID
GROUP BY OM_Contact.ContactID, ContactFirstName, ContactLastName, ContactIsAnonymous, ContactEmail, ContactStatusName, FullName
ORDER BY [{$om.score$}] DESC', 0, 757, N'<customdata><pagesize>15</pagesize><skinid></skinid><querynorecordtext>No data found</querynorecordtext><pagemode>1</pagemode><enablepaging>True</enablepaging><exportenabled>True</exportenabled></customdata>', '0a00506e-8929-4f8f-81bf-ed23f3dc786b', '20110907 12:25:26')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (588, N'ContactsWithScoreHigherThanN', N'Contacts with score higher than N', N'SELECT
	SUM(OM_ScoreContactRule.Value) as [{$om.score$}],
	ContactFirstName as [{$om.contact.firstname$}], 
	ContactLastName as [{$om.contact.lastname$}], 
        ContactEmail as [{$general.email$}],
        ContactIsAnonymous as [{$om.contact.isanonymous$}],
        ContactStatusName as [{$om.contactstatus$}],
        FullName as [{$om.contact.owner$}]
FROM OM_Contact
LEFT JOIN OM_ContactStatus ON OM_Contact.ContactStatusID = OM_ContactStatus.ContactStatusID
LEFT JOIN CMS_User ON CMS_User.UserID = OM_Contact.ContactOwnerUserID
INNER JOIN OM_ScoreContactRule ON OM_Contact.ContactID = OM_ScoreContactRule.ContactID
INNER JOIN OM_Score ON OM_ScoreContactRule.ScoreID = OM_Score.ScoreID
INNER JOIN OM_Rule ON OM_ScoreContactRule.RuleID = OM_Rule.RuleID
WHERE
  OM_Score.ScoreEnabled = 1
  AND
  (@ScoreID = -1 OR OM_Score.ScoreID = @ScoreID)
  AND
  OM_Score.ScoreSiteID = @CMSContextCurrentSiteID
GROUP BY OM_Contact.ContactID, ContactFirstName, ContactLastName, ContactIsAnonymous, ContactEmail, ContactStatusName, FullName
HAVING SUM(OM_ScoreContactRule.Value) >= @MinimalScore
ORDER BY [{$om.score$}] DESC', 0, 758, N'<customdata><pagesize>15</pagesize><skinid></skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>True</enablepaging><pagemode>1</pagemode></customdata>', '741b6c81-1266-43ba-a180-b80b32d069e1', '20110919 17:49:19')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (589, N'TopPctContacts', N'Top % Contacts', N'SELECT TOP (@SelectTopNPct) PERCENT 
	SUM(OM_ScoreContactRule.Value) as [{$om.score$}],
	ContactFirstName as [{$om.contact.firstname$}], 
	ContactLastName as [{$om.contact.lastname$}], 
        ContactEmail as [{$general.email$}],
        ContactIsAnonymous as [{$om.contact.isanonymous$}],
        ContactStatusName as [{$om.contactstatus$}],
        FullName as [{$om.contact.owner$}]
FROM OM_Contact
LEFT JOIN OM_ContactStatus ON OM_Contact.ContactStatusID = OM_ContactStatus.ContactStatusID
LEFT JOIN CMS_User ON CMS_User.UserID = OM_Contact.ContactOwnerUserID
INNER JOIN OM_ScoreContactRule ON OM_Contact.ContactID = OM_ScoreContactRule.ContactID
INNER JOIN OM_Score ON OM_ScoreContactRule.ScoreID = OM_Score.ScoreID
INNER JOIN OM_Rule ON OM_ScoreContactRule.RuleID = OM_Rule.RuleID
WHERE
  OM_Score.ScoreEnabled = 1
  AND
  (@ScoreID = -1 OR OM_Score.ScoreID = @ScoreID)
  AND
  OM_Score.ScoreSiteID = @CMSContextCurrentSiteID
GROUP BY OM_Contact.ContactID, ContactFirstName, ContactLastName, ContactIsAnonymous, ContactEmail, ContactStatusName, FullName
ORDER BY [{$om.score$}] DESC', 0, 759, N'<customdata><pagesize>15</pagesize><skinid></skinid><querynorecordtext>No data found</querynorecordtext><pagemode>1</pagemode><enablepaging>False</enablepaging><exportenabled>True</exportenabled></customdata>', 'ceb31edd-8103-4a30-9dd4-3accde62c4e0', '20110907 12:27:47')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (590, N'DocumentAliasesTable', N'Document aliases table', N'SELECT NodeName, NodeAliasPath, AliasUrlPath, AliasCulture, AliasExtensions  
FROM CMS_DocumentAlias LEFT JOIN CMS_Tree 
ON AliasNodeID = NodeID 
WHERE NodeSiteID = {% CMSContext.CurrentSiteID |(user)administrator|(hash)2cbb3933d6fe028c10b72ff08f995085e8fe7902d8c14db671019a1c750b0874%}
ORDER BY NodeAliasPath', 0, 760, N'<customdata><pagesize>20</pagesize><skinid></skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>True</enablepaging><pagemode>1</pagemode></customdata>', 'ead70440-74db-4375-a13d-dfd0caa4a543', '20110907 13:17:08')
INSERT INTO [Reporting_ReportTable] ([TableID], [TableName], [TableDisplayName], [TableQuery], [TableQueryIsStoredProcedure], [TableReportID], [TableSettings], [TableGUID], [TableLastModified]) VALUES (592, N'table', N'table', N'DECLARE @PaveViews TABLE
(
  DocumentNamePath NVARCHAR(500),
  Pageviews INT,
  Percents DECIMAL(10,2)  
)
DECLARE @Sum DECIMAL;
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day'');
SELECT @Sum =   
  SUM(HitsCount) 
  FROM Analytics_Statistics
  INNER JOIN Analytics_DayHits ON Analytics_DayHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  LEFT JOIN View_CMS_Tree_Joined ON View_CMS_Tree_Joined.NodeID = Analytics_Statistics.StatisticsObjectID AND DocumentCulture = StatisticsObjectCulture
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND (StatisticsCode=@CodeName)
  AND (HitsStartTime >= @FromDate)
 AND (HitsEndTime <= @ToDate)
INSERT INTO @PaveViews (DocumentNamePath,pageViews,percents)
SELECT TOP 10
 CASE
  WHEN DocumentNamePath LIKE '''' OR DocumentNamePath IS NULL THEN StatisticsObjectName
  ELSE DocumentNamePath
 END , SUM(HitsCount), (SUM(HitsCount)/@Sum)*100
 FROM Analytics_Statistics
 INNER JOIN Analytics_DayHits ON Analytics_DayHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
 LEFT JOIN View_CMS_Tree_Joined ON Analytics_Statistics.StatisticsObjectID = View_CMS_Tree_Joined.NodeID AND DocumentCulture = StatisticsObjectCulture
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
 AND (StatisticsCode=@CodeName)
 GROUP BY DocumentNamePath, StatisticsObjectName
 ORDER BY SUM(HitsCount) DESC
 
  
 SELECT DocumentNamePath AS ''{$reports_landingpage_Day.path_header$}'',pageviews AS  ''{$reports_landingpage_Day.hits_header$}'',
   CAST (Percents AS NVARCHAR(10))+''%'' AS ''{$reports_landingpage_Day.hits_percent_header$}''
   FROM @PaveViews ORDER BY PageViews DESC', 0, 761, N'<customdata><pagesize>15</pagesize><skinid>ReportGridAnalytics</skinid><querynorecordtext>No data found</querynorecordtext><exportenabled>True</exportenabled><enablepaging>False</enablepaging><pagemode>1</pagemode></customdata>', '24dd4953-f8fa-4c83-a4ce-00504c72b6b0', '20110920 12:29:27')
SET IDENTITY_INSERT [Reporting_ReportTable] OFF
