CREATE PROCEDURE [Proc_CMS_Site_Insert]
	@SiteID int,
	@SiteName nvarchar(100),
    @SiteDisplayName nvarchar(200),
    @SiteDescription ntext,
    @SiteStatus varchar(20),
	@SiteDomainName nvarchar(400)
AS
BEGIN
INSERT INTO [CMS_Site] (
	[SiteName],
	[SiteDisplayName],
	[SiteDescription],
	[SiteStatus],
	[SiteDomainName]
)
VALUES (
	@SiteName,
	@SiteDisplayName,
	@SiteDescription,
	@SiteStatus,
	@SiteDomainName
)
SELECT SCOPE_IDENTITY()
END
