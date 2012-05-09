-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_Reporting_Report_RemoveDependencies]
	@ID int
AS
BEGIN
	DELETE FROM Reporting_ReportValue WHERE ValueReportID=@ID;
	DELETE FROM Reporting_ReportTable WHERE TableReportID=@ID;
	DELETE FROM Reporting_ReportGraph WHERE GraphReportID=@ID;
	-- SavedReport
	DELETE FROM Reporting_SavedGraph WHERE SavedGraphSavedReportID IN (
		SELECT SavedReportID FROM Reporting_SavedReport WHERE SavedReportReportID=@ID);
	DELETE FROM Reporting_SavedReport WHERE SavedReportReportID=@ID;
END
