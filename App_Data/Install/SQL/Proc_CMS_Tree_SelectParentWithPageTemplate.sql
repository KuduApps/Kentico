CREATE PROCEDURE [Proc_CMS_Tree_SelectParentWithPageTemplate]
	@SiteID int,
	@CultureCode nvarchar(20),
	@NodeAliasPath nvarchar(450)
AS
BEGIN
	SELECT TOP 1 * FROM View_CMS_Tree_Joined INNER JOIN CMS_PageTemplate ON View_CMS_Tree_Joined.DocumentPageTemplateID=CMS_PageTemplate.PageTemplateID WHERE
	(NodeSiteID=@SiteID AND DocumentCulture=@CultureCode AND DocumentPageTemplateID IS NOT NULL AND (NodeAliasPath='/' OR @NodeAliasPath LIKE NodeAliasPath+'/%')) ORDER BY NodeAliasPath DESC
END
