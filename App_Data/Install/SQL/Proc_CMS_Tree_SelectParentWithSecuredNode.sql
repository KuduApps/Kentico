CREATE PROCEDURE [Proc_CMS_Tree_SelectParentWithSecuredNode]
	@SiteID int,
	@NodeAliasPath nvarchar(450)
AS
BEGIN
SELECT TOP 1 * FROM CMS_Tree WHERE
	(NodeSiteID=@SiteID AND IsSecuredNode IS NOT NULL AND (NodeAliasPath='/' OR @NodeAliasPath LIKE NodeAliasPath+'/%')) ORDER BY NodeAliasPath DESC
END
