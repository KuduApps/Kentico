CREATE PROCEDURE [Proc_CMS_SettingsKey_GetDataByKeyName] 
	-- Add the parameters for the stored procedure here
	@KeyName nvarchar(100),
	@SiteName nvarchar(100)
AS
BEGIN
    -- Insert statements for procedure here
	SELECT * FROM CMS_SettingsKey WHERE CMS_SettingsKey.KeyName = @KeyName AND 	CMS_SettingsKey.SiteID = (SELECT [SiteID] FROM [CMS_Site] WHERE SiteName = @SiteName)
END
