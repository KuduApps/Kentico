CREATE PROCEDURE [Proc_CMS_SettingsCategory_SelectByName] 
	@CategoryDisplayName nvarchar(200)
AS
BEGIN
	SELECT * FROM CMS_SettingsCategory WHERE CategoryDisplayName = @CategoryDisplayName ORDER BY 'CategoryDisplayName'
END
