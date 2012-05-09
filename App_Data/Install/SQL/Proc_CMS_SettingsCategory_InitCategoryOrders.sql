CREATE PROCEDURE [Proc_CMS_SettingsCategory_InitCategoryOrders]
	@CategoryID int
AS
BEGIN
    /* Move elements only within same branch and within the same category type (meaning IsGroup) */
    DECLARE @ParentCategoryID int;
    DECLARE @IsGroup bit;
    
    SELECT TOP 1 @ParentCategoryID = ISNULL([CategoryParentID], 0)
				,@IsGroup = ISNULL([CategoryIsGroup], 0)
	FROM [CMS_SettingsCategory] 
	WHERE [CategoryID] = @CategoryID
    
    
	/* Declare the selection table */
	DECLARE @categoryTable TABLE (
		CategoryID int NOT NULL,
		CategoryOrder int NULL
	);
	
	/* Get the steps list */
	INSERT INTO @categoryTable SELECT [CategoryID], [CategoryOrder] FROM [CMS_SettingsCategory] WHERE ([CategoryParentID] = @ParentCategoryID) AND ([CategoryIsGroup] = @IsGroup)
	
	/* Declare the cursor to loop through the table */
	DECLARE @categoryCursor CURSOR;
    SET @categoryCursor = CURSOR FOR SELECT [CategoryID], [CategoryOrder] FROM @categoryTable ORDER BY [CategoryOrder] ASC, [CategoryID] ASC;
    
	/* Assign the numbers to the steps */
	DECLARE @currentIndex int, @currentCategoryOrder int;
	SET @currentIndex = 1;
	DECLARE @currentCategoryId int;
	
	/* Loop through the table */
	OPEN @categoryCursor
	FETCH NEXT FROM @categoryCursor INTO @currentCategoryId, @currentCategoryOrder;
	WHILE @@FETCH_STATUS = 0
	BEGIN
		/* Set the step index if different */
		IF @currentCategoryOrder IS NULL OR @currentCategoryOrder <> @currentIndex
			UPDATE [CMS_SettingsCategory] SET [CategoryOrder] = @currentIndex WHERE CategoryID = @currentCategoryId;
						
		/* Get next record */
		SET @currentIndex = @currentIndex + 1;
		FETCH NEXT FROM @categoryCursor INTO @currentCategoryId, @currentCategoryOrder;
	END
	CLOSE @categoryCursor;
	DEALLOCATE @categoryCursor;
END
