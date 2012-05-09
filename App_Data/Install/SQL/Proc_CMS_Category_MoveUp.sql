CREATE PROCEDURE [Proc_CMS_Category_MoveUp]
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
    
	/* Move the previous categories down */
	UPDATE CMS_Category SET CategoryOrder = CategoryOrder + 1 WHERE ISNULL(CategoryParentID, 0) = @ParentCategoryID AND ISNULL(CategorySiteID, 0) = @CategorySiteID AND ISNULL(CategoryUserID, 0) = @CategoryUserID AND CategoryOrder = (SELECT CategoryOrder FROM CMS_Category WHERE CategoryID = @ID) - 1
	/* Move the current step up */
	UPDATE CMS_Category SET CategoryOrder = CategoryOrder - 1 WHERE CategoryID = @ID AND CategoryOrder > 1
END
