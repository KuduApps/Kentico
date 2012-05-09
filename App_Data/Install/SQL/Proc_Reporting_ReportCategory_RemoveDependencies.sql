-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_Reporting_ReportCategory_RemoveDependencies] 
	@ID int
AS
BEGIN
	DELETE FROM Reporting_ReportValue WHERE ValueReportID IN (
		SELECT ReportID FROM Reporting_Report WHERE ReportCategoryID=@ID);
	DELETE FROM Reporting_ReportTable WHERE TableReportID IN (
		SELECT ReportID FROM Reporting_Report WHERE ReportCategoryID=@ID);
	DELETE FROM Reporting_ReportGraph WHERE GraphReportID IN (
		SELECT ReportID FROM Reporting_Report WHERE ReportCategoryID=@ID);
	-- SavedReport
	DELETE FROM Reporting_SavedGraph WHERE SavedGraphSavedReportID IN (
		SELECT SavedReportID FROM Reporting_SavedReport WHERE SavedReportReportID IN (
			SELECT ReportID FROM Reporting_Report WHERE ReportCategoryID=@ID)
		);
	DELETE FROM Reporting_SavedReport WHERE SavedReportReportID IN (
		SELECT ReportID FROM Reporting_Report WHERE ReportCategoryID=@ID);
	-- Report
	DELETE FROM Reporting_Report WHERE ReportCategoryID=@ID;
END
