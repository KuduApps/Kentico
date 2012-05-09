-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_Reporting_SavedReports_RemoveDependencies]
	@ID int
AS
BEGIN
	DELETE FROM Reporting_SavedGraph WHERE SavedGraphSavedReportID IN (SELECT SavedReportID FROM Reporting_SavedReport WHERE [SavedReportID] = @ID)	
END
