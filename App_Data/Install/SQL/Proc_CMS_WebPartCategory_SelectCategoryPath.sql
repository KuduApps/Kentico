CREATE PROCEDURE [Proc_CMS_WebPartCategory_SelectCategoryPath]
    @CategoryID int
AS
BEGIN
    /* Declare the table for results */
    DECLARE @categoryTable TABLE (
        CategoryID int,
        CategoryDisplayName nvarchar(100),
        CategoryParentID int,
        CategoryName nvarchar(100),
        CategoryGUID uniqueidentifier,
        CategoryLastModified datetime
    );
    WHILE @CategoryID > 0
    BEGIN
        /* Add current category */
        INSERT INTO @categoryTable SELECT CategoryID, CategoryDisplayName, CategoryParentID, CategoryName, CategoryGUID, CategoryLastModified FROM CMS_WebPartCategory WHERE CategoryID = @CategoryID
        /* Get parent ID */
        SET @CategoryID = (SELECT CategoryParentID FROM CMS_WebPartCategory WHERE CategoryID = @CategoryID)
    END
    /* Return the result table */
    SELECT * FROM @categoryTable ORDER BY CategoryID ASC
END
