CREATE PROCEDURE [Proc_CMS_SettingsKey_SelectByKeyID]
@KeyID int
AS
BEGIN
	SELECT * FROM CMS_SettingsKey WHERE KeyID = @KeyID
END
