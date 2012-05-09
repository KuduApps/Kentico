CREATE PROCEDURE [Proc_CMS_SettingsKey_SelectByKeyName] 
	@KeyName nvarchar(100),
	@SiteID int
AS
BEGIN
    -- Insert statements for procedure here
	SELECT * FROM CMS_SettingsKey WHERE KeyName = @KeyName AND ((SiteID IS NULL AND @SiteID = 0) OR (SiteID = @SiteID))
END
