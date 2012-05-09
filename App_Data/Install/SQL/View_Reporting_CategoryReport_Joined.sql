CREATE VIEW [View_Reporting_CategoryReport_Joined]
AS
(SELECT CategoryID AS ObjectID, CategoryCodeName AS CodeName, 
CategoryDisplayName AS DisplayName, CategoryParentID AS ParentID,
CategoryGUID AS GUID, CategoryLastModified AS LastModified      
      ,CategoryImagePath
      ,CategoryPath AS ObjectPath
      ,CategoryOrder
      ,CategoryLevel AS ObjectLevel
      ,CategoryChildCount
      ,CategoryReportChildCount
      ,ISNULL(CategoryChildCount,0) + ISNULL(CategoryReportChildCount,0) AS CompleteChildCount
      ,NULL AS ReportLayout
      ,NULL AS ReportParameters
      ,NULL AS ReportAccess
      ,'reportcategory' AS ObjectType     
FROM         Reporting_ReportCategory)
UNION ALL
(
SELECT ReportID AS ObjectID, ReportName AS CodeName, 
ReportDisplayName AS DisplayName, ReportCategoryID AS ParentID,
ReportGUID AS GUID, ReportLastModified AS LastModified
	  ,NULL AS CategoryImagePath
      ,Reporting_ReportCategory.CategoryPath + '/' + ReportName AS ObjectPath
      ,NULL AS CategoryOrder
      ,Reporting_ReportCategory.CategoryLevel + 1 AS ObjectLevel
      ,0 AS CategoryChildCount
      ,0 AS CategoryWebPartChildCount 
      ,0 AS CompleteChildCount
      ,ReportLayout
      ,ReportParameters
      ,ReportAccess 
      ,'report' AS ObjectType  
FROM  Reporting_Report LEFT JOIN Reporting_ReportCategory ON Reporting_Report.ReportCategoryID = Reporting_ReportCategory.CategoryID
)
GO
