CREATE VIEW [View_CMS_DocumentAlias_Joined]
AS
SELECT     CMS_Tree.NodeAliasPath, CMS_DocumentAlias.AliasNodeID, CMS_DocumentAlias.AliasCulture, CMS_DocumentAlias.AliasURLPath, 
                      CMS_DocumentAlias.AliasSiteID
FROM         CMS_DocumentAlias INNER JOIN
                      CMS_Tree ON CMS_DocumentAlias.AliasNodeID = CMS_Tree.NodeID
GO
