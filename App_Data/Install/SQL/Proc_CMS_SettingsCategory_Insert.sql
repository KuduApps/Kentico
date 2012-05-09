CREATE PROCEDURE [Proc_CMS_SettingsCategory_Insert] 
	@CategoryDisplayName nvarchar(200)
AS
BEGIN
	INSERT INTO CMS_SettingsCategory (CategoryDisplayName) VALUES (@CategoryDisplayName)
END
