CREATE PROCEDURE [Proc_PM_ProjectTask_MoveDown]
@ProjectTaskID int,
    @OrderColumn int
        -- OrderColumn: ProjectOrder = 1,
        --              UserOrder = 2
AS
BEGIN
DECLARE @maxTaskOrder int
	DECLARE @assigneeId int
	DECLARE @projectId int
	
	SET @projectId = (SELECT TOP 1 ProjectTaskProjectID FROM PM_ProjectTask WHERE ProjectTaskID = @ProjectTaskID);
	SET @assigneeId = (SELECT TOP 1 ProjectTaskAssignedToUserID FROM PM_ProjectTask WHERE ProjectTaskID = @ProjectTaskID);
	
	IF @OrderColumn = 1 -- Project order
	BEGIN
		SET @maxTaskOrder = (SELECT TOP 1 ProjectTaskProjectOrder FROM PM_ProjectTask WHERE ProjectTaskProjectID = @projectId ORDER BY ProjectTaskProjectOrder DESC);
		/* Move the next step(s) up */
		UPDATE PM_ProjectTask SET ProjectTaskProjectOrder = ProjectTaskProjectOrder - 1 WHERE ProjectTaskProjectOrder = (SELECT ProjectTaskProjectOrder FROM PM_ProjectTask WHERE ProjectTaskID=@ProjectTaskID) + 1 
		/* Move the current step down */
		UPDATE PM_ProjectTask SET ProjectTaskProjectOrder = ProjectTaskProjectOrder + 1 WHERE ProjectTaskID = @ProjectTaskID AND ProjectTaskProjectOrder < @maxTaskOrder
	END
	ELSE -- User order
	BEGIN
		/* Move the next step(s) up */
		-- get next taskId
		DECLARE @nextTaskId int;
		SET @nextTaskId = (
			SELECT ProjectTaskID
			FROM PM_ProjectTask
			WHERE
				ProjectTaskUserOrder = (SELECT ProjectTaskUserOrder FROM PM_ProjectTask WHERE ProjectTaskID=@ProjectTaskID) + 1
				AND ProjectTaskAssignedToUserID = @assigneeId
		);		
		-- move the next task up
		IF (@nextTaskId IS NOT NULL)
		BEGIN
			UPDATE PM_ProjectTask
			SET ProjectTaskUserOrder = ProjectTaskUserOrder - 1
			WHERE
				ProjectTaskID = @nextTaskId;
			/* Move the current step down */
			UPDATE PM_ProjectTask
			SET ProjectTaskUserOrder = ProjectTaskUserOrder + 1
			WHERE
				ProjectTaskID = @ProjectTaskID
		END
	END
END
