-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_PM_ProjectTaskStatus_MoveUp] 
	@TaskStatusID int
AS
BEGIN
    /* Move the previous step(s) down */
	UPDATE PM_ProjectTaskStatus SET TaskStatusOrder = TaskStatusOrder + 1 WHERE TaskStatusOrder = (SELECT TaskStatusOrder FROM PM_ProjectTaskStatus WHERE TaskStatusID = @TaskStatusID) - 1 
	/* Move the current step up */
	UPDATE PM_ProjectTaskStatus SET TaskStatusOrder = TaskStatusOrder - 1 WHERE TaskStatusID = @TaskStatusID AND TaskStatusOrder > 1
END
