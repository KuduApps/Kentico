CREATE PROCEDURE [Proc_CMS_SettingsCategory_MoveCategoryDown]
	@CategoryID int
AS
BEGIN
	DECLARE @MaxCategoryOrder int
	SET @MaxCategoryOrder = (SELECT TOP 1 [CategoryOrder] FROM [CMS_SettingsCategory] ORDER BY [CategoryOrder] DESC);
	
	/* Move elements only within same branch and within the same type (see IsGroup */
    DECLARE @ParentCategoryID int;
    DECLARE @IsGroup bit;
    
    SELECT TOP 1 @ParentCategoryID = ISNULL([CategoryParentID], 0)
				,@IsGroup = ISNULL([CategoryIsGroup], 0)
	FROM [CMS_SettingsCategory] 
	WHERE [CategoryID] = @CategoryID
	
	/* Move the next step(s) up */
	UPDATE [CMS_SettingsCategory] 
	SET [CategoryOrder] = [CategoryOrder] - 1 
	WHERE	([CategoryParentID] = @ParentCategoryID)
		AND ([CategoryIsGroup] = @IsGroup)
		AND ([CategoryOrder] = (SELECT [CategoryOrder] FROM [CMS_SettingsCategory] WHERE [CategoryID] = @CategoryID) + 1)
	
	/* Move the current step down */
	UPDATE [CMS_SettingsCategory] 
	SET	[CategoryOrder] = [CategoryOrder] + 1 
	WHERE	([CategoryID] = @CategoryID) 
		AND ([CategoryOrder] < @MaxCategoryOrder)
	
END
