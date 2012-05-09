CREATE PROCEDURE [Proc_CMS_Class_SelectCustomTablesForSite]
	@siteId int	
AS
BEGIN
	SELECT * FROM CMS_Class
	WHERE (ClassIsCustomTable = 1) AND (ClassID IN
       (SELECT ClassID FROM CMS_ClassSite WHERE SiteID = @siteId))
END
