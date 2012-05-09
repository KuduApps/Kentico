-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_PM_ProjectTaskPriority_MoveDown] 
    @TaskPriorityID int
AS
BEGIN
	DECLARE @MaxTaskPriorityOrder int
	SET @MaxTaskPriorityOrder = (SELECT TOP 1 TaskPriorityOrder FROM PM_ProjectTaskPriority ORDER BY TaskPriorityOrder DESC);
	/* Move the next step(s) up */
	UPDATE PM_ProjectTaskPriority SET TaskPriorityOrder = TaskPriorityOrder - 1 WHERE TaskPriorityOrder = (SELECT TaskPriorityOrder FROM PM_ProjectTaskPriority WHERE TaskPriorityID=@TaskPriorityID) + 1 
	/* Move the current step down */
	UPDATE PM_ProjectTaskPriority SET TaskPriorityOrder = TaskPriorityOrder + 1 WHERE TaskPriorityID = @TaskPriorityID AND TaskPriorityOrder < @MaxTaskPriorityOrder
END
