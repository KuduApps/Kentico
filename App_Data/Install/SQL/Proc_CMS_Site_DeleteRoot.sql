CREATE PROCEDURE [Proc_CMS_Site_DeleteRoot]
    @SiteID int
AS
BEGIN
    -- Removes VersionHistory dependences from VersionAttachment
    DELETE FROM [CMS_VersionAttachment] WHERE VersionHistoryID IN (
        SELECT VersionHistoryID FROM [CMS_VersionHistory] WHERE NodeSiteID = @SiteID
    );
    -- Removes AttachmentHistory
    DELETE FROM CMS_AttachmentHistory WHERE 
        AttachmentDocumentID IN (SELECT DocumentID FROM View_CMS_Tree_Joined WHERE NodeSiteID=@SiteID);
    -- Delete complete site workflow history
    DELETE FROM CMS_WorkflowHistory WHERE 
        VersionHistoryID IN (SELECT VersionHistoryID FROM CMS_VersionHistory WHERE NodeSiteID=@SiteID);
    -- Update the documents
    UPDATE CMS_Document SET DocumentCheckedOutVersionHistoryID = NULL, DocumentCheckedOutByUserID = NULL, DocumentCheckedOutWhen = NULL WHERE 
        DocumentCheckedOutVersionHistoryID IN (SELECT VersionHistoryID FROM CMS_VersionHistory WHERE NodeSiteID=@SiteID);
    UPDATE CMS_Document SET DocumentPublishedVersionHistoryID = NULL WHERE
        DocumentPublishedVersionHistoryID IN (SELECT VersionHistoryID FROM CMS_VersionHistory WHERE NodeSiteID=@SiteID);
    -- Delete complete version history
    DELETE FROM CMS_VersionHistory WHERE NodeSiteID=@SiteID;
    -- Delete bindings between document and tag
    DELETE FROM CMS_DocumentTag WHERE
        DocumentID IN (SELECT DocumentID FROM View_CMS_Tree_Joined WHERE NodeSiteID=@SiteID);
    -- Delete bindings between document and category
    DELETE FROM CMS_DocumentCategory WHERE
        DocumentID IN (SELECT DocumentID FROM View_CMS_Tree_Joined WHERE NodeSiteID=@SiteID);
    -- Delete the document data
    DELETE FROM CMS_Document WHERE 
        DocumentNodeID IN (SELECT NodeID FROM CMS_Tree WHERE NodeSiteID=@SiteID);
    -- Delete root eventlog data
    DELETE FROM CMS_EventLog WHERE NodeID IN (SELECT NodeID FROM CMS_Tree WHERE NodeSiteID = @SiteID);
    -- Delete ACL
    UPDATE CMS_Tree SET NodeACLID = NULL WHERE NodeSiteID = @SiteID
    
    DELETE FROM CMS_ACLItem WHERE ACLID IN (SELECT ACLID FROM CMS_ACL WHERE ACLOwnerNodeID IN (SELECT NodeID FROM CMS_Tree WHERE NodeSiteID = @SiteID))
    DELETE FROM CMS_ACL WHERE ACLOwnerNodeID IN (SELECT NodeID FROM CMS_Tree WHERE NodeSiteID = @SiteID)
    -- Delete relationships
    DELETE FROM CMS_Relationship WHERE LeftNodeID IN (SELECT NodeID FROM CMS_Tree WHERE NodeSiteID = @SiteID) OR RightNodeID IN (SELECT NodeID FROM CMS_Tree WHERE NodeSiteID = @SiteID)
   
    -- Delete document aliases
	DELETE FROM CMS_DocumentAlias WHERE AliasNodeID IN (SELECT NodeID FROM CMS_Tree WHERE NodeSiteID = @SiteID)
	DELETE FROM CMS_DocumentAlias WHERE AliasSiteID = @SiteID
	-- Delete the node data
    DELETE FROM CMS_Tree WHERE NodeSiteID = @SiteID;
END
