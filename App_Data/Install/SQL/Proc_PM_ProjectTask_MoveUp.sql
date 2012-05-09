-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_PM_ProjectTask_MoveUp] 
    @ProjectTaskID int,
    @OrderColumn int
        -- OrderColumn: ProjectOrder = 1,
        --              UserOrder = 2    
AS
BEGIN
	DECLARE @projectId int;
	DECLARE @assigneeId int;
	
	SET @projectId = (SELECT TOP 1 ProjectTaskProjectID FROM PM_ProjectTask WHERE ProjectTaskID = @ProjectTaskID);
	SET @assigneeId = (SELECT TOP 1 ProjectTaskAssignedToUserID FROM PM_ProjectTask WHERE ProjectTaskID = @ProjectTaskID);
	IF @OrderColumn = 1 -- Project order
	BEGIN
		/* Move the previous step(s) down */
		UPDATE PM_ProjectTask 
		SET ProjectTaskProjectOrder = ProjectTaskProjectOrder + 1 
		WHERE
			ProjectTaskProjectID = @projectId
			AND ProjectTaskProjectOrder = (SELECT ProjectTaskProjectOrder FROM PM_ProjectTask WHERE ProjectTaskID = @ProjectTaskID) - 1 
		/* Move the current step up */
		UPDATE PM_ProjectTask SET ProjectTaskProjectOrder = ProjectTaskProjectOrder - 1 WHERE ProjectTaskID = @ProjectTaskID AND ProjectTaskProjectOrder > 1
	END
	ELSE -- User order
	BEGIN	
		/* Move the previous step(s) down */
		UPDATE PM_ProjectTask
		SET ProjectTaskUserOrder = ProjectTaskUserOrder + 1
		WHERE
			ProjectTaskAssignedToUserID = @assigneeId
			AND ProjectTaskUserOrder = (SELECT ProjectTaskUserOrder FROM PM_ProjectTask WHERE ProjectTaskID = @ProjectTaskID) - 1 
		/* Move the current step up */
		UPDATE PM_ProjectTask SET ProjectTaskUserOrder = ProjectTaskUserOrder - 1 WHERE ProjectTaskID = @ProjectTaskID AND ProjectTaskUserOrder > 1
	END	
END
