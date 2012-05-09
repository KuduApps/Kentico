CREATE PROCEDURE [Proc_CMS_SettingsKey_Select-Category_Site]
	@CategoryID int,
	@SiteID int
AS
BEGIN
SELECT * FROM CMS_SettingsKey, CMS_SettingsCategory
	WHERE	CategoryID = KeyCategoryID AND
			CategoryID = @CategoryID AND
			SiteID = @SiteID AND
			(KeyIsHidden IS NULL OR KeyIsHidden = 0);
END
