CREATE PROCEDURE [Proc_CMS_SettingsCategory_RemoveDependencies] 
	@IDPath varchar(450),
	@SettingsCategoryID int
AS
BEGIN
	SET NOCOUNT ON;
	
	-- Remove all CMS_SettingsKeys
	DELETE FROM [CMS_SettingsKey] WHERE ([KeyCategoryID] = @SettingsCategoryID)
	-- Remove references within CMS_SettingsCategory table
	UPDATE [CMS_SettingsCategory] SET [CategoryParentID] = NULL WHERE (([CategoryIDPath] LIKE N'' + @IDPath + '/%') OR ([CategoryIDPath] LIKE @IDPath))
	
	-- Remove all CMS_SettingsKeys for the current node children
	DELETE FROM [CMS_SettingsKey] 
	WHERE [KeyCategoryID] IN
	(
		SELECT [CategoryID] FROM [CMS_SettingsCategory]  
        WHERE (([CategoryIDPath] LIKE N'' + @IDPath + '/%') OR ([CategoryIDPath] LIKE @IDPath))
    )   
    
	-- Remove [CMS_SettingsCategory] dependencies
	DELETE FROM [CMS_SettingsCategory] 
    WHERE [CategoryID] IN 
    (
        SELECT [CategoryID] FROM [CMS_SettingsCategory]  
        WHERE (([CategoryIDPath] LIKE N'' + @IDPath + '/%') OR ([CategoryIDPath] LIKE @IDPath))
    )
END
