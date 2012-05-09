CREATE PROCEDURE [Proc_CMS_SettingsKey_MoveKeyUp]
	@KeyName nvarchar(100)
AS
BEGIN
    DECLARE @KeyCategoryID int
	SET @KeyCategoryID = (SELECT TOP 1 KeyCategoryID FROM CMS_SettingsKey WHERE KeyName = @KeyName AND SiteID IS NULL);
	/* Move the previous key down */
	UPDATE CMS_SettingsKey SET KeyOrder = KeyOrder + 1 WHERE KeyOrder = ((SELECT KeyOrder FROM CMS_SettingsKey WHERE KeyName = @KeyName AND SiteID IS NULL) - 1 ) AND KeyCategoryID = @KeyCategoryID
	/* Move the current key up */
	UPDATE CMS_SettingsKey SET KeyOrder = KeyOrder - 1 WHERE KeyName = @KeyName AND KeyOrder > 1
END
