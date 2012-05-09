CREATE PROCEDURE [Proc_CMS_VersionHistory_DestroyDocumentHistory]
	@DocumentID int
AS
BEGIN
	UPDATE CMS_Document SET DocumentPublishedVersionHistoryID = NULL, DocumentCheckedOutVersionHistoryID = NULL, DocumentCheckedOutByUserID = NULL WHERE DocumentID = @DocumentID; 
	DELETE FROM CMS_WorkflowHistory WHERE VersionHistoryID IN (SELECT VersionHistoryID FROM CMS_VersionHistory WHERE DocumentID = @DocumentID);
	DELETE FROM CMS_VersionHistory WHERE DocumentID = @DocumentID;
END
