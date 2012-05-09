CREATE PROCEDURE [Proc_CMS_VersionHistory_RemoveDependencies]
	@VersionHistoryID int
AS
BEGIN
	SET NOCOUNT ON;
	/* Update the documents */
	UPDATE CMS_Document SET DocumentCheckedOutVersionHistoryID = NULL, DocumentCheckedOutByUserID = NULL, DocumentCheckedOutWhen = NULL WHERE DocumentCheckedOutVersionHistoryID = @VersionHistoryID; 
	UPDATE CMS_Document SET DocumentPublishedVersionHistoryID = NULL WHERE DocumentPublishedVersionHistoryID = @VersionHistoryID; 
	/* Clear the workflow history */
	DELETE FROM CMS_WorkflowHistory WHERE VersionHistoryID = @VersionHistoryID; 
	/* Clear the attachment history - delete all the attachments that are connected to  DELETE FROM CMS_AttachmentHistory WHERE AttachmentHistoryID IN (SELECT AttachmentHistoryID FROM CMS_VersionAttachment WHERE VersionHistoryID = @VersionHistoryID) AND AttachmentHistoryID IN (SELECT AttachmentHistoryID FROM CMS_VersionAttachment GROUP BY AttachmentHistoryID HAVING COUNT(VersionHistoryID) <= 1)*/  
	DELETE FROM CMS_AttachmentHistory WHERE AttachmentHistoryID IN (SELECT AttachmentHistoryID FROM CMS_VersionAttachment WITH (NOLOCK) WHERE VersionHistoryID = @VersionHistoryID GROUP BY AttachmentHistoryID HAVING COUNT(VersionHistoryID) <= 1 )
END
