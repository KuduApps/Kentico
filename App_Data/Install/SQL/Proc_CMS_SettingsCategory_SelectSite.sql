CREATE PROCEDURE [Proc_CMS_SettingsCategory_SelectSite]
	@SiteID int
AS
BEGIN
SELECT DISTINCT CMS_SettingsCategory.* FROM CMS_SettingsKey, CMS_SettingsCategory
	WHERE	CategoryID = KeyCategoryID AND
			SiteID = SiteID ORDER BY CategoryOrder, CategoryDisplayName;
END
