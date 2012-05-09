CREATE PROCEDURE [Proc_CMS_UIElement_InitOrders]
	@ID int
AS
BEGIN
    /* Move elements only within same branch */
    DECLARE @ParentElementID int;
    SET @ParentElementID = (SELECT TOP 1 ElementParentID FROM CMS_UIElement WHERE ElementID = @ID);
    
	/* Declare the selection table */
	DECLARE @tempTable TABLE (
		ElementID int NOT NULL,
		ElementOrder int NULL
	);
	
	/* Get the steps list */
	INSERT INTO @tempTable SELECT ElementID, ElementOrder FROM CMS_UIElement WHERE ElementParentID = @ParentElementID
	
	/* Declare the cursor to loop through the table */
	DECLARE @cursor CURSOR;
    SET @cursor = CURSOR FOR SELECT ElementID, ElementOrder FROM @tempTable ORDER BY ElementOrder ASC, ElementID ASC;
    
	/* Assign the numbers to the steps */
	DECLARE @currentIndex int, @currentOrder int, @currentId int;
	SET @currentIndex = 1;
	
	/* Loop through the table */
	OPEN @cursor
	FETCH NEXT FROM @cursor INTO @currentId, @currentOrder;
	WHILE @@FETCH_STATUS = 0
	BEGIN
		/* Set the step index if different */
		IF @currentOrder IS NULL OR @currentOrder <> @currentIndex
			UPDATE CMS_UIElement SET ElementOrder = @currentIndex WHERE ElementID = @currentId;
			
		/* Get next record */
		SET @currentIndex = @currentIndex + 1;
		FETCH NEXT FROM @cursor INTO @currentId, @currentOrder;
	END
	CLOSE @cursor;
	DEALLOCATE @cursor;
END
