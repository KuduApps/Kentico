CREATE PROCEDURE [Proc_CMS_SiteDomainAlias_Delete]
	@SiteName nvarchar(100),
	@SiteDomainAliasName nvarchar(400)
AS
BEGIN
DECLARE @SiteID int;
SET @SiteID = ( SELECT SiteID FROM CMS_Site WHERE SiteName = @SiteName );
DELETE FROM CMS_SiteDomainAlias 
	WHERE	SiteID = @SiteID AND
			SiteDomainAliasName = @SiteDomainAliasName;
END
