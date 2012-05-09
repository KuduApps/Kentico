CREATE PROCEDURE [Proc_COM_ProductOption_InitOptionOrders]
	@SkuID int,
    @OptionCategoryID int
AS
BEGIN
	/* Declare the selection table */
	DECLARE @optionTable TABLE (
		SKUID int NOT NULL,
		SKUOrder int NULL
	);
	
	/* Get the steps list */
	INSERT INTO @optionTable SELECT SKUID, SKUOrder FROM COM_SKU WHERE SKUOptionCategoryID = @OptionCategoryID
	
	/* Declare the cursor to loop through the table */
	DECLARE @optionCursor CURSOR;
    SET @optionCursor = CURSOR FOR SELECT SKUID, SKUOrder FROM @optionTable ORDER BY SKUOrder ASC, SKUID ASC;
	/* Assign the numbers to the steps */
	DECLARE @currentIndex int, @currentOptionOrder int;
	SET @currentIndex = 1;
	DECLARE @currentOptionId int;
	
	/* Loop through the table */
	OPEN @optionCursor
	FETCH NEXT FROM @optionCursor INTO @currentOptionId, @currentOptionOrder;
	WHILE @@FETCH_STATUS = 0
	BEGIN
		/* Set the step index if different */
		IF @currentOptionOrder IS NULL OR @currentOptionOrder <> @currentIndex
			UPDATE COM_SKU SET SKUOrder = @currentIndex WHERE SKUID = @currentOptionId;
		/* Get next record */
		SET @currentIndex = @currentIndex + 1;
		FETCH NEXT FROM @optionCursor INTO @currentOptionId, @currentOptionOrder;
	END
	CLOSE @optionCursor;
	DEALLOCATE @optionCursor;
END
