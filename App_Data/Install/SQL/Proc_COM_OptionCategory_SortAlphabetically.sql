CREATE PROCEDURE [Proc_COM_OptionCategory_SortAlphabetically]
@CategoryID int	
AS
BEGIN
	/* Declare the selection table */
	DECLARE @orderTable TABLE (
		SKUID int,
		SKUName nvarchar(450),
		SKUOptionCategoryID int,
		SKUOrder int);	
	/* Get the nodes list */
	INSERT INTO @orderTable SELECT SKUID,SKUName,SKUOptionCategoryID,SKUOrder FROM COM_SKU WHERE SKUOptionCategoryID = @CategoryID;
	/* Declare the cursor to loop through the table */
	DECLARE @orderCursor CURSOR;  
	SET @orderCursor = CURSOR FOR SELECT SKUID, SKUOrder FROM @orderTable ORDER BY SKUName ASC, SKUOrder ASC;
	/* Assign the numbers to the nodes */
	DECLARE @currentIndex int, @currentOrder int;
	SET @currentIndex = 1;
	DECLARE @currentId int;	
	/* Loop through the table */
	OPEN @orderCursor
	FETCH NEXT FROM @orderCursor INTO @currentId,@currentOrder;
	WHILE @@FETCH_STATUS = 0
	BEGIN
		/* Set the Node index if different */
		IF @currentOrder IS NULL OR @currentOrder <> @currentIndex
			UPDATE COM_SKU SET SKUOrder = @currentIndex WHERE SKUID = @currentId;
		/* Get next record */
		SET @currentIndex = @currentIndex + 1;
		FETCH NEXT FROM @orderCursor INTO @currentId, @currentOrder;
	END
	CLOSE @orderCursor;
	DEALLOCATE @orderCursor;
	RETURN @currentIndex;
END
