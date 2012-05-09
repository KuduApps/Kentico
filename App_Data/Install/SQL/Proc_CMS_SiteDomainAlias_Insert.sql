CREATE PROCEDURE [Proc_CMS_SiteDomainAlias_Insert]
	@SiteName nvarchar(100),
	@SiteDomainAliasName nvarchar(400)
AS
BEGIN
DECLARE @SiteID int;
IF ( @SiteName IS NOT NULL )
	SET @SiteID = ( SELECT SiteID FROM CMS_Site WHERE SiteName = @SiteName )
ELSE
	SET @SiteID = NULL
INSERT INTO CMS_SiteDomainAlias (
        [SiteDomainAliasName],
		[SiteID]
	)
    VALUES (
        @SiteDomainAliasName,
        @SiteID
	)
END
