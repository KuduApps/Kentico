-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_PM_ProjectTaskStatus_MoveDown] 
    @TaskStatusID int
AS
BEGIN
	DECLARE @MaxTaskStatusOrder int
	SET @MaxTaskStatusOrder = (SELECT TOP 1 TaskStatusOrder FROM PM_ProjectTaskStatus ORDER BY TaskStatusOrder DESC);
	/* Move the next step(s) up */
	UPDATE PM_ProjectTaskStatus SET TaskStatusOrder = TaskStatusOrder - 1 WHERE TaskStatusOrder = (SELECT TaskStatusOrder FROM PM_ProjectTaskStatus WHERE TaskStatusID=@TaskStatusID) + 1 
	/* Move the current step down */
	UPDATE PM_ProjectTaskStatus SET TaskStatusOrder = TaskStatusOrder + 1 WHERE TaskStatusID = @TaskStatusID AND TaskStatusOrder < @MaxTaskStatusOrder
END
