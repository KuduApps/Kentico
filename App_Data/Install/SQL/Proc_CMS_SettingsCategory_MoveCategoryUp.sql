CREATE PROCEDURE [Proc_CMS_SettingsCategory_MoveCategoryUp]
	@CategoryID int
AS
BEGIN
    /* Move elements only within same branch and within the same type*/
    DECLARE @ParentCategoryID int;
    DECLARE @IsGroup bit;
    
    SELECT TOP 1 @ParentCategoryID = ISNULL([CategoryParentID], 0)
				,@IsGroup = ISNULL([CategoryIsGroup], 0)
	FROM [CMS_SettingsCategory] 
	WHERE [CategoryID] = @CategoryID    
    
    /* Move the previous step(s) down */
	UPDATE [CMS_SettingsCategory] 
		SET [CategoryOrder] = [CategoryOrder] + 1 
	WHERE	([CategoryParentID] = @ParentCategoryID) 
		AND ([CategoryIsGroup] = @IsGroup)
		AND	([CategoryOrder] = (SELECT [CategoryOrder] FROM [CMS_SettingsCategory] WHERE [CategoryID] = @CategoryID) - 1)
		
	/* Move the current step up */
	UPDATE [CMS_SettingsCategory] 
		SET	[CategoryOrder] = [CategoryOrder] - 1 
	WHERE	([CategoryID] = @CategoryID) 
		AND (CategoryOrder > 1)
END
