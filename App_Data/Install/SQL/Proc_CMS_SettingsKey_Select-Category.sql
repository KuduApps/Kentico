CREATE PROCEDURE [Proc_CMS_SettingsKey_Select-Category]
	@CategoryID int
AS
BEGIN
SELECT * FROM CMS_SettingsKey, CMS_SettingsCategory
	WHERE	CategoryID = KeyCategoryID AND
			CategoryID = @CategoryID AND
			SiteID is null AND
			(KeyIsHidden IS NULL OR KeyIsHidden = 0)
END
