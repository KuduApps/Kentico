CREATE PROCEDURE [Proc_CMS_SettingsCategory_Select]
AS
BEGIN
  SELECT * FROM CMS_SettingsCategory ORDER BY CategoryOrder, CategoryDisplayName;
END
