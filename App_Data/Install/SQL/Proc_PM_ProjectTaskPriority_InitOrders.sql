-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_PM_ProjectTaskPriority_InitOrders]
AS
BEGIN
	/* Declare the selection table */
	DECLARE @priorityTable TABLE (
		TaskPriorityID int NOT NULL,
		TaskPriorityOrder int NULL
	);
	
	/* Get the steps list */
	INSERT INTO @priorityTable SELECT TaskPriorityID, TaskPriorityOrder FROM PM_ProjectTaskPriority
	
	/* Declare the cursor to loop through the table */
	DECLARE @cursor CURSOR;
    SET @cursor = CURSOR FOR SELECT TaskPriorityID, TaskPriorityOrder FROM @priorityTable ORDER BY TaskPriorityOrder ASC, TaskPriorityID ASC;
	/* Assign the numbers to the steps */
	DECLARE @currentIndex int, @currentOrder int;
	SET @currentIndex = 1;
	DECLARE @currentId int;
	
	/* Loop through the table */
	OPEN @cursor
	FETCH NEXT FROM @cursor INTO @currentId, @currentOrder;
	WHILE @@FETCH_STATUS = 0
	BEGIN
		/* Set the step index if different */
		IF @currentOrder IS NULL OR @currentOrder <> @currentIndex
			UPDATE PM_ProjectTaskPriority SET TaskPriorityOrder = @currentIndex WHERE TaskPriorityID = @currentId;
		/* Get next record */
		SET @currentIndex = @currentIndex + 1;
		FETCH NEXT FROM @cursor INTO @currentId, @currentOrder;
	END
	CLOSE @cursor;
	DEALLOCATE @cursor;
END
