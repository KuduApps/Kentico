-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_PM_ProjectStatus_InitOrders]
AS
BEGIN
	/* Declare the selection table */
	DECLARE @statusTable TABLE (
		StatusID int NOT NULL,
		StatusOrder int NULL
	);
	
	/* Get the steps list */
	INSERT INTO @statusTable SELECT StatusID, StatusOrder FROM PM_ProjectStatus
	
	/* Declare the cursor to loop through the table */
	DECLARE @cursor CURSOR;
    SET @cursor = CURSOR FOR SELECT StatusID, StatusOrder FROM @statusTable ORDER BY StatusOrder ASC, StatusID ASC;
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
			UPDATE PM_ProjectStatus SET StatusOrder = @currentIndex WHERE StatusID = @currentId;
		/* Get next record */
		SET @currentIndex = @currentIndex + 1;
		FETCH NEXT FROM @cursor INTO @currentId, @currentOrder;
	END
	CLOSE @cursor;
	DEALLOCATE @cursor;
END
