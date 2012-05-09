CREATE PROCEDURE [Proc_CMS_SettingsKey_MoveKeyDown]
	@KeyName nvarchar(100)
AS
BEGIN
	DECLARE @CategoryID int
	SET @CategoryID = (SELECT TOP 1 KeyCategoryID FROM CMS_SettingsKey WHERE KeyName = @KeyName AND SiteID IS NULL);
	DECLARE @MaxKeyOrder int
	SET @MaxKeyOrder = (SELECT TOP 1 KeyOrder FROM CMS_SettingsKey WHERE KeyCategoryID = @CategoryID ORDER BY KeyOrder DESC);
	/* Move the next step(s) up */
	UPDATE CMS_SettingsKey SET KeyOrder = KeyOrder - 1 WHERE KeyOrder = ((SELECT KeyOrder FROM CMS_SettingsKey WHERE KeyName = @KeyName AND SiteID IS NULL) + 1 ) AND KeyCategoryID = @CategoryID
	/* Move the current step down */
	UPDATE CMS_SettingsKey SET KeyOrder = KeyOrder + 1 WHERE KeyName = @KeyName AND KeyOrder < @MaxKeyOrder
END
