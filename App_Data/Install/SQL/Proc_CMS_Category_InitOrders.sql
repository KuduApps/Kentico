CREATE PROCEDURE [Proc_CMS_Category_InitOrders]
	@ID int
AS
BEGIN
    /* Move categories only within same branch */
    DECLARE @ParentCategoryID int;
    SET @ParentCategoryID = (SELECT TOP 1 ISNULL(CategoryParentID, 0) FROM CMS_Category WHERE CategoryID = @ID);
    /* Move categories only within same site */
    DECLARE @CategorySiteID int;
    SET @CategorySiteID = (SELECT TOP 1 ISNULL(CategorySiteID, 0) FROM CMS_Category WHERE CategoryID = @ID);
    /* Move categories only within same user */
    DECLARE @CategoryUserID int;
    SET @CategoryUserID = (SELECT TOP 1 ISNULL(CategoryUserID, 0) FROM CMS_Category WHERE CategoryID = @ID);
    
	/* Declare the selection table */
	DECLARE @tempTable TABLE (
		CategoryID int NOT NULL,
		CategoryOrder int NULL
	);
	
	/* Get the steps list */
	INSERT INTO @tempTable SELECT CategoryID, CategoryOrder FROM CMS_Category 
	WHERE ISNULL(CategoryParentID, 0) = @ParentCategoryID 
	AND ISNULL(CategorySiteID, 0) = @CategorySiteID
	AND ISNULL(CategoryUserID, 0) = @CategoryUserID
	
	/* Declare the cursor to loop through the table */
	DECLARE @cursor CURSOR;
    SET @cursor = CURSOR FOR SELECT CategoryID, CategoryOrder FROM @tempTable ORDER BY CategoryOrder ASC, CategoryID ASC;
    
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
			UPDATE CMS_Category SET CategoryOrder = @currentIndex WHERE CategoryID = @currentId;
			
		/* Get next record */
		SET @currentIndex = @currentIndex + 1;
		FETCH NEXT FROM @cursor INTO @currentId, @currentOrder;
	END
	CLOSE @cursor;
	DEALLOCATE @cursor;
END
