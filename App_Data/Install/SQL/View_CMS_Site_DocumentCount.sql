CREATE VIEW [View_CMS_Site_DocumentCount]
AS
SELECT     SiteID, SiteName, SiteDisplayName, SiteDescription, SiteStatus, SiteDomainName, SiteDefaultStylesheetID, SiteDefaultVisitorCulture, SiteDefaultEditorStylesheet, 
                      SiteGUID, SiteLastModified,
                          (SELECT     COUNT(*) AS Documents
                            FROM          CMS_Tree
                            WHERE      (NodeSiteID = CMS_Site.SiteID)) AS Documents, SiteIsOffline
FROM         CMS_Site
GO
