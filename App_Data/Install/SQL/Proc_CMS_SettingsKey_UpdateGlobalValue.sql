CREATE PROCEDURE [Proc_CMS_SettingsKey_UpdateGlobalValue]
	@KeyName nvarchar(100),
	@KeyValue ntext
AS
BEGIN
UPDATE CMS_SettingsKey 
	SET [KeyValue] = @KeyValue
	WHERE	KeyName = @KeyName AND
			SiteID is null;
END
