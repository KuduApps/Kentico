-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_PM_Project_RemoveDependencies]
	@ID int
AS
BEGIN
	DELETE FROM [PM_ProjectTask] WHERE ProjectTaskProjectID = @ID;
	DELETE FROM [PM_ProjectRolePermission] WHERE ProjectID = @ID;
END
