-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_PM_ProjectTaskPriority_MoveUp] 
	@TaskPriorityID int
AS
BEGIN
    /* Move the previous step(s) down */
	UPDATE PM_ProjectTaskPriority SET TaskPriorityOrder = TaskPriorityOrder + 1 WHERE TaskPriorityOrder = (SELECT TaskPriorityOrder FROM PM_ProjectTaskPriority WHERE TaskPriorityID = @TaskPriorityID) - 1 
	/* Move the current step up */
	UPDATE PM_ProjectTaskPriority SET TaskPriorityOrder = TaskPriorityOrder - 1 WHERE TaskPriorityID = @TaskPriorityID AND TaskPriorityOrder > 1
END
