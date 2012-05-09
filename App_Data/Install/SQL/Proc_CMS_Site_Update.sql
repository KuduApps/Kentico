CREATE PROCEDURE [Proc_CMS_Site_Update]
	@SiteID int,
	@SiteName nvarchar(100),
    @SiteDisplayName nvarchar(200),
    @SiteDescription ntext,
    @SiteStatus varchar(20),
	@SiteDomainName nvarchar(400)
AS
BEGIN
UPDATE [CMS_Site] 
SET 
	SiteName = @SiteName,
	SiteDisplayName = @SiteDisplayName,
	SiteDescription = @SiteDescription,
	SiteStatus = @SiteStatus,
	SiteDomainName = @SiteDomainName
WHERE
	SiteID = @SiteID
END
