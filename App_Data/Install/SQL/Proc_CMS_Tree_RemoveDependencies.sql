CREATE PROCEDURE [Proc_CMS_Tree_RemoveDependencies]
    @NodeID int,
    @NodeGUID uniqueidentifier,
    @NodeSiteID int
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    UPDATE CMS_EventLog SET NodeID = NULL WHERE NodeID = @NodeID; 
    DELETE FROM CMS_Relationship WHERE RightNodeID = @NodeID OR LeftNodeID = @NodeID;
    UPDATE CMS_Tree SET NodeACLID = NULL WHERE NodeACLID IN (SELECT ACLID FROM CMS_ACL WHERE ACLOwnerNodeID = @NodeID)
    
    DELETE FROM CMS_ACLItem WHERE ACLID IN (SELECT ACLID FROM CMS_ACL WHERE ACLOwnerNodeID = @NodeID)
    DELETE FROM CMS_ACL WHERE ACLOwnerNodeID = @NodeID
    
    DELETE FROM CMS_DocumentAlias WHERE AliasNodeID = @NodeID    
    UPDATE Community_Group SET GroupNodeGUID = NULL WHERE GroupSiteID = @NodeSiteID AND GroupNodeGUID = @NodeGUID
    -- Project Management
    UPDATE [PM_Project] SET ProjectNodeID = NULL WHERE ProjectNodeID = @NodeID;
    
    COMMIT TRANSACTION;
END
