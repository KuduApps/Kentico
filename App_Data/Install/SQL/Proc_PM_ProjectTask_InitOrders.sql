CREATE PROCEDURE [Proc_PM_ProjectTask_InitOrders]
	@ItemID int = 0,
	@OrderColumn int
        -- OrderColumn: ProjectOrder = 1,
        --              UserOrder = 2  	
AS
BEGIN
	/* Declare the selection table */
	DECLARE @nodesTable TABLE (
		TaskID int,
		TaskOrder int,
		TaskPriorityOrder int,
		TaskDeadline datetime
	);
	
	/* Get the nodes list */
	IF @OrderColumn = 1 -- Project order
		INSERT INTO @nodesTable 
			SELECT ProjectTaskID, ProjectTaskProjectOrder, TaskPriorityOrder, ProjectTaskDeadline
			FROM PM_ProjectTask INNER JOIN PM_ProjectTaskPriority ON PM_ProjectTask.ProjectTaskPriorityID = PM_ProjectTaskPriority.TaskPriorityID
			WHERE ProjectTaskProjectID = @ItemID;
	ELSE -- User order
		INSERT INTO @nodesTable 
			SELECT ProjectTaskID, ProjectTaskUserOrder, TaskPriorityOrder, ProjectTaskDeadline
			FROM PM_ProjectTask INNER JOIN PM_ProjectTaskPriority ON PM_ProjectTask.ProjectTaskPriorityID = PM_ProjectTaskPriority.TaskPriorityID
			WHERE ProjectTaskAssignedToUserID = @ItemID;	
	
	/* Declare the cursor to loop through the table */
	DECLARE @nodeCursor CURSOR;
	
	-- sorting arrows
	SET @nodeCursor = CURSOR FOR SELECT TaskID, TaskOrder FROM @nodesTable ORDER BY TaskOrder ASC, TaskPriorityOrder ASC, TaskDeadline DESC;
		
	/* Assign the numbers to the nodes */
	DECLARE @currentIndex int, @currentNodeOrder int;
	SET @currentIndex = 1;
	DECLARE @currentNodeId int;
	
	/* Loop through the table */
	OPEN @nodeCursor
	FETCH NEXT FROM @nodeCursor INTO @currentNodeId, @currentNodeOrder;
	WHILE @@FETCH_STATUS = 0
	BEGIN
		/* Set the Node index if different */
		IF @currentNodeOrder IS NULL OR @currentNodeOrder <> @currentIndex
		BEGIN
			IF @OrderColumn = 1 -- Project order
				UPDATE PM_ProjectTask SET ProjectTaskProjectOrder = @currentIndex WHERE ProjectTaskID = @currentNodeId;
			ELSE -- User order
				UPDATE PM_ProjectTask SET ProjectTaskUserOrder = @currentIndex WHERE ProjectTaskID = @currentNodeId;
		END
		/* Get next record */
		SET @currentIndex = @currentIndex + 1;
		FETCH NEXT FROM @nodeCursor INTO @currentNodeId, @currentNodeOrder;
	END
	CLOSE @nodeCursor;
	DEALLOCATE @nodeCursor;
END
