CREATE PROCEDURE [Proc_CMS_WidgetCategory_OrderAlphaAsc] 
	@WidgetCategoryParentID int
AS
BEGIN
	
	/* Declare the selection table */
	DECLARE @categoriesTable TABLE (
		WidgetCategoryID int,
		WidgetCategoryOrder int,
		WidgetCategoryDisplayName	nvarchar(100)
	);
	
	/* Get the nodes list */
	INSERT INTO @categoriesTable SELECT WidgetCategoryID, WidgetCategoryOrder, WidgetCategoryDisplayName FROM CMS_WidgetCategory WHERE WidgetCategoryParentID = @WidgetCategoryParentID;
	
	/* Declare the cursor to loop through the table */
	DECLARE @categoryCursor CURSOR;
    SET @categoryCursor = CURSOR FOR SELECT WidgetCategoryID, WidgetCategoryOrder FROM @categoriesTable ORDER BY WidgetCategoryDisplayName ASC, WidgetCategoryOrder ASC;
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
			UPDATE CMS_WidgetCategory SET WidgetCategoryOrder = @currentIndex WHERE WidgetCategoryID = @currentCategoryId;
		/* Get next record */
		SET @currentIndex = @currentIndex + 1;
		FETCH NEXT FROM @categoryCursor INTO @currentCategoryId, @currentCategoryOrder;
	END
	CLOSE @categoryCursor;
	DEALLOCATE @categoryCursor;
	RETURN @currentIndex;
	
END
