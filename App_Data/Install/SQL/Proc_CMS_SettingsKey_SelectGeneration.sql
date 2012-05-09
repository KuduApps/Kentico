CREATE PROCEDURE [Proc_CMS_SettingsKey_SelectGeneration]
	@KeyLoadGeneration int,
	@SiteName nvarchar(100)
AS
IF (@SiteName = '') BEGIN
	SELECT KeyName, KeyValue FROM CMS_SettingsKey WHERE KeyLoadGeneration = @KeyLoadGeneration AND SiteID IS NULL
END ELSE BEGIN
	SELECT KeyName, KeyValue FROM CMS_SettingsKey WHERE KeyLoadGeneration = @KeyLoadGeneration AND SiteID = (SELECT TOP 1 SiteID FROM CMS_Site WHERE SiteName = @SiteName)
END
