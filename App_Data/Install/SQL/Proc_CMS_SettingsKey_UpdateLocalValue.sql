CREATE PROCEDURE [Proc_CMS_SettingsKey_UpdateLocalValue]
	@SiteName nvarchar(100),
	@KeyName nvarchar(100),
	@KeyValue ntext
AS
BEGIN
DECLARE @SiteID int;
SET @SiteID = ( SELECT SiteID FROM CMS_Site WHERE SiteName = @SiteName );
UPDATE CMS_SettingsKey 
	SET [KeyValue] = @KeyValue
	WHERE	SiteID = @SiteID AND
			KeyName = @KeyName;
END
