CREATE PROCEDURE [Proc_CMS_AttachmentHistory_MoveAttachmentUp]
	@AttachmentGUID uniqueidentifier,
	@VersionHistoryID int
AS
BEGIN
	/* Get Attachment ID */
	DECLARE @AttachmentHistoryID int;
	SET @AttachmentHistoryID = (SELECT TOP 1 AttachmentHistoryID FROM CMS_AttachmentHistory WHERE (AttachmentGUID = @AttachmentGUID) AND (AttachmentHistoryID IN (SELECT AttachmentHistoryID FROM CMS_VersionAttachment WHERE VersionHistoryID=@VersionHistoryID)));
	/* Get attachment's document ID */
	DECLARE @AttachmentDocumentID int;
	SET @AttachmentDocumentID = (SELECT TOP 1 AttachmentDocumentID FROM CMS_AttachmentHistory WHERE AttachmentHistoryID = @AttachmentHistoryID);
	
	/* Get ID of group */
	DECLARE @AttachmentGroupGUID uniqueidentifier;
	SET @AttachmentGroupGUID = (SELECT TOP 1 AttachmentGroupGUID FROM CMS_AttachmentHistory WHERE AttachmentHistoryID = @AttachmentHistoryID);
	
	/* Move the previous attachment down */
	IF @AttachmentGroupGUID IS NOT NULL
		UPDATE CMS_AttachmentHistory SET AttachmentOrder = AttachmentOrder + 1 WHERE AttachmentOrder = ((SELECT AttachmentOrder FROM CMS_AttachmentHistory WHERE AttachmentHistoryID = @AttachmentHistoryID) - 1 ) AND AttachmentDocumentID = @AttachmentDocumentID AND AttachmentGroupGUID = @AttachmentGroupGUID
	ELSE 
		UPDATE CMS_AttachmentHistory SET AttachmentOrder = AttachmentOrder + 1 WHERE AttachmentOrder = ((SELECT AttachmentOrder FROM CMS_AttachmentHistory WHERE AttachmentHistoryID = @AttachmentHistoryID) - 1 ) AND AttachmentDocumentID = @AttachmentDocumentID AND AttachmentGroupGUID IS NULL
		
	/* Move the current attachment up */
	UPDATE CMS_AttachmentHistory SET AttachmentOrder = AttachmentOrder - 1 WHERE AttachmentHistoryID = @AttachmentHistoryID AND AttachmentOrder > 1
END
