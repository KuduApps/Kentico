CREATE PROCEDURE [Proc_CMS_SiteDomainAlias_Select]
	@SiteName varchar(100)
AS
BEGIN
DECLARE @SiteID int;
IF ( @SiteName IS NOT NULL )
	SET @SiteID = ( SELECT SiteID FROM CMS_Site WHERE SiteName = @SiteName )
ELSE
	SET @SiteID = NULL
SELECT * 
	FROM CMS_SiteDomainAlias
	WHERE SiteID = @SiteID;
END
