CREATE PROCEDURE [Proc_CMS_WebPartCategory_OrderAlphaAsc] 
	@CategoryParentID int
AS
BEGIN
	
	/* Declare the selection table */
	DECLARE @categoriesTable TABLE (
		CategoryID int,
		CategoryOrder int,
		CategoryDisplayName	nvarchar(100)
	);
	
	/* Get the nodes list */
	INSERT INTO @categoriesTable SELECT CategoryID, CategoryOrder, CategoryDisplayName FROM CMS_WebPartCategory WHERE CategoryParentID = @CategoryParentID;
	
	/* Declare the cursor to loop through the table */
	DECLARE @categoryCursor CURSOR;
    SET @categoryCursor = CURSOR FOR SELECT CategoryID, CategoryOrder FROM @categoriesTable ORDER BY CategoryDisplayName ASC, CategoryOrder ASC;
	/* Assign the numbers to the categories */
	DECLARE @currentIndex int, @currentCategoryOrder int;
	SET @currentIndex = 1;
	DECLARE @currentCategoryId int;
	
	/* Loop through the table */
	OPEN @categoryCursor
	FETCH NEXT FROM @categoryCursor INTO @currentCategoryId, @currentCategoryOrder;
	WHILE @@FETCH_STATUS = 0
	BEGIN
		/* Set the Node index if different */
		IF @currentCategoryOrder IS NULL OR @currentCategoryOrder <> @currentIndex
			UPDATE CMS_WebPartCategory SET CategoryOrder = @currentIndex WHERE CategoryID = @currentCategoryId;
		/* Get next record */
		SET @currentIndex = @currentIndex + 1;
		FETCH NEXT FROM @categoryCursor INTO @currentCategoryId, @currentCategoryOrder;
	END
	CLOSE @categoryCursor;
	DEALLOCATE @categoryCursor;
	RETURN @currentIndex;
	
END
