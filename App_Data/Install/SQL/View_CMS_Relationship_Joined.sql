CREATE VIEW [View_CMS_Relationship_Joined]
AS
SELECT     LeftTree.NodeID AS LeftNodeID, LeftTree.NodeGUID AS LeftNodeGUID, LeftTree.NodeName AS LeftNodeName, 
                      CMS_RelationshipName.RelationshipName, CMS_RelationshipName.RelationshipNameID, RightTree.NodeID AS RightNodeID, 
                      RightTree.NodeGUID AS RightNodeGUID, RightTree.NodeName AS RightNodeName, CMS_RelationshipName.RelationshipDisplayName, CMS_Relationship.RelationshipCustomData
FROM         CMS_Relationship INNER JOIN
                      CMS_Tree AS LeftTree ON CMS_Relationship.LeftNodeID = LeftTree.NodeID INNER JOIN
                      CMS_Tree AS RightTree ON CMS_Relationship.RightNodeID = RightTree.NodeID INNER JOIN
                      CMS_RelationshipName ON CMS_Relationship.RelationshipNameID = CMS_RelationshipName.RelationshipNameID
GO
