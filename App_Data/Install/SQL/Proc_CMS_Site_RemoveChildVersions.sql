CREATE PROCEDURE [Proc_CMS_Site_RemoveChildVersions]
@ID int
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
 SET NOCOUNT ON;
 
 -- removes ObjectVersionHistory of site objects
 DELETE FROM [CMS_ObjectVersionHistory] WHERE VersionObjectSiteID = @ID
END
