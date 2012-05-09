CREATE PROCEDURE [Proc_Reporting_ReportCategory_RemoveChildVersions]
@ID int
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;
    
    -- Report value
    DELETE FROM [CMS_ObjectVersionHistory] WHERE [VersionObjectType] = N'reporting.reportvalue' AND [VersionObjectID] IN (
        SELECT [ValueID] FROM [Reporting_ReportValue] WHERE [ValueReportID] IN ( 
            SELECT [ReportID] FROM [Reporting_Report] WHERE [ReportCategoryID] = @ID
        )
    );
    
    -- Report table
    DELETE FROM [CMS_ObjectVersionHistory] WHERE [VersionObjectType] = N'reporting.reporttable' AND [VersionObjectID] IN (
        SELECT [TableID] FROM [Reporting_ReportTable] WHERE [TableReportID] IN (
            SELECT [ReportID] FROM [Reporting_Report] WHERE [ReportCategoryID] = @ID
        )
    );
    
    -- Report graph
    DELETE FROM [CMS_ObjectVersionHistory] WHERE [VersionObjectType] = N'reporting.reportgraph' AND [VersionObjectID] IN (
        SELECT [GraphID] FROM [Reporting_ReportGraph] WHERE [GraphReportID] IN (
            SELECT [ReportID] FROM [Reporting_Report] WHERE [ReportCategoryID] = @ID
        )
    );
     
   -- Report
   DELETE FROM [CMS_ObjectVersionHistory] WHERE [VersionObjectType] = N'reporting.report' AND [VersionObjectID] IN (
       SELECT [ReportID] FROM [Reporting_Report] WHERE [ReportCategoryID] = @ID
   );
END
