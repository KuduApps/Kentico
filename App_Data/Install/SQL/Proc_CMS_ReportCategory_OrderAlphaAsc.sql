--DECLARE @classId int;
--SET @classId = (SELECT TOP 1 ClassID FROM CMS_Class WHERE ClassName = 'reporting.reportcategory')
--IF @classId <>0 BEGIN
--INSERT [CMS_Query] ([QueryName], [QueryTypeID], [QueryText], [QueryRequiresTransaction], [ClassID], [QueryIsLocked], 
--[QueryLastModified], [QueryGUID], [QueryLoadGeneration], [QueryIsCustom]) VALUES ('OrderAlphabetically', 
--1, 'Proc_CMS_ReportCategory_OrderAlphaAsc', 0, @classId, 0, getDate(), newId(), 0, 0)
--end
--go 
CREATE PROCEDURE [Proc_CMS_ReportCategory_OrderAlphaAsc] 
	@CategoryParentID int
AS
BEGIN
	
	/* Declare the selection table */
	DECLARE @categoriesTable TABLE (
		CategoryID int,
		CategoryOrder int,
		CategoryDisplayName	nvarchar(200)
	);
	
	/* Get the nodes list */
	INSERT INTO @categoriesTable SELECT CategoryID, CategoryOrder, CategoryDisplayName FROM Reporting_ReportCategory WHERE CategoryParentID = @CategoryParentID;
	
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
			UPDATE Reporting_ReportCategory SET CategoryOrder = @currentIndex WHERE CategoryID = @currentCategoryId;
		/* Get next record */
		SET @currentIndex = @currentIndex + 1;
		FETCH NEXT FROM @categoryCursor INTO @currentCategoryId, @currentCategoryOrder;
	END
	CLOSE @categoryCursor;
	DEALLOCATE @categoryCursor;
	RETURN @currentIndex;
	
END
