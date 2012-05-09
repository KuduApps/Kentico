CREATE PROCEDURE [Proc_Reporting_Report_RemoveChildVersions]
@ID int
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;
    
    -- Report value
    DELETE FROM [CMS_ObjectVersionHistory] WHERE [VersionObjectType] = N'reporting.reportvalue' AND [VersionObjectID] IN (
        SELECT [ValueID] FROM [Reporting_ReportValue] WHERE [ValueReportID] = @ID
    );
    
     -- Report table
    DELETE FROM [CMS_ObjectVersionHistory] WHERE [VersionObjectType] = N'reporting.reporttable' AND [VersionObjectID] IN (
        SELECT [TableID] FROM [Reporting_ReportTable] WHERE [TableReportID] = @ID
    );
    
     -- Report graph
    DELETE FROM [CMS_ObjectVersionHistory] WHERE [VersionObjectType] = N'reporting.reportgraph' AND [VersionObjectID] IN (
        SELECT [GraphID] FROM [Reporting_ReportGraph] WHERE [GraphReportID] = @ID
    );
END
