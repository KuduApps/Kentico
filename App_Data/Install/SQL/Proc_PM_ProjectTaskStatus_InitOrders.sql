-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_PM_ProjectTaskStatus_InitOrders]
AS
BEGIN
	/* Declare the selection table */
	DECLARE @statusTable TABLE (
		TaskStatusID int NOT NULL,
		TaskStatusOrder int NULL
	);
	
	/* Get the steps list */
	INSERT INTO @statusTable SELECT TaskStatusID, TaskStatusOrder FROM PM_ProjectTaskStatus
	
	/* Declare the cursor to loop through the table */
	DECLARE @cursor CURSOR;
    SET @cursor = CURSOR FOR SELECT TaskStatusID, TaskStatusOrder FROM @statusTable ORDER BY TaskStatusOrder ASC, TaskStatusID ASC;
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
			UPDATE PM_ProjectTaskStatus SET TaskStatusOrder = @currentIndex WHERE TaskStatusID = @currentId;
		/* Get next record */
		SET @currentIndex = @currentIndex + 1;
		FETCH NEXT FROM @cursor INTO @currentId, @currentOrder;
	END
	CLOSE @cursor;
	DEALLOCATE @cursor;
END
