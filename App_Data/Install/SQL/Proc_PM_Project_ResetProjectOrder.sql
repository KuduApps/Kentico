-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_PM_Project_ResetProjectOrder]
	@ProjectID int
AS
BEGIN
	UPDATE PM_ProjectTask
	SET ProjectTaskProjectOrder = 0
	WHERE ProjectTaskProjectID = @ProjectID;
END
