CREATE PROCEDURE [Proc_CMS_Attachment_MoveAttachmentDown]
	@AttachmentGUID uniqueidentifier,
	@DocumentID int
AS
BEGIN
	/* Get Attachment ID */
	DECLARE @AttachmentID int;
	IF @DocumentID > 0 
		SET @AttachmentID = (SELECT TOP 1 AttachmentID FROM CMS_Attachment WHERE (AttachmentGUID = @AttachmentGUID) AND (AttachmentDocumentID=@DocumentID));
	ELSE
		SET @AttachmentID = (SELECT TOP 1 AttachmentID FROM CMS_Attachment WHERE (AttachmentGUID = @AttachmentGUID) AND (AttachmentDocumentID IS NULL));
	/* Get ID of group */
	DECLARE @AttachmentGroupGUID uniqueidentifier;
	IF @DocumentID > 0 
		SET @AttachmentGroupGUID = (SELECT TOP 1 AttachmentGroupGUID FROM CMS_Attachment WHERE AttachmentID = @AttachmentID);
	ELSE
		SET @AttachmentGroupGUID = (SELECT TOP 1 AttachmentGroupGUID FROM CMS_Attachment WHERE AttachmentID = @AttachmentID);
	
	/* Get GUID of form */
	DECLARE @AttachmentFormGUID uniqueidentifier;
	IF @DocumentID > 0 
		SET @AttachmentFormGUID = (SELECT TOP 1 AttachmentFormGUID FROM CMS_Attachment WHERE AttachmentID = @AttachmentID);
	ELSE
		SET @AttachmentFormGUID = (SELECT TOP 1 AttachmentFormGUID FROM CMS_Attachment WHERE AttachmentID = @AttachmentID);
	
	DECLARE @MaxAttachmentOrder int	
	IF @AttachmentFormGUID IS NOT NULL
		IF @AttachmentGroupGUID IS NOT NULL
			SET @MaxAttachmentOrder = (SELECT TOP 1 AttachmentOrder FROM CMS_Attachment WHERE AttachmentFormGUID = @AttachmentFormGUID AND AttachmentGroupGUID = @AttachmentGroupGUID ORDER BY AttachmentOrder DESC);
		ELSE
			SET @MaxAttachmentOrder = (SELECT TOP 1 AttachmentOrder FROM CMS_Attachment WHERE AttachmentFormGUID = @AttachmentFormGUID AND AttachmentGroupGUID IS NULL ORDER BY AttachmentOrder DESC);	
	ELSE	
		IF @AttachmentGroupGUID IS NOT NULL
			SET @MaxAttachmentOrder = (SELECT TOP 1 AttachmentOrder FROM CMS_Attachment WHERE AttachmentDocumentID = @DocumentID AND AttachmentGroupGUID = @AttachmentGroupGUID ORDER BY AttachmentOrder DESC);
		ELSE
			SET @MaxAttachmentOrder = (SELECT TOP 1 AttachmentOrder FROM CMS_Attachment WHERE AttachmentDocumentID = @DocumentID AND AttachmentGroupGUID IS NULL ORDER BY AttachmentOrder DESC);		
	
	/* Move the next attachment(s) up */	
	IF @AttachmentFormGUID IS NOT NULL
		IF @AttachmentGroupGUID IS NOT NULL
			UPDATE CMS_Attachment SET AttachmentOrder = AttachmentOrder - 1 WHERE AttachmentOrder = ((SELECT AttachmentOrder FROM CMS_Attachment WHERE AttachmentID = @AttachmentID) + 1 ) AND AttachmentFormGUID = @AttachmentFormGUID AND AttachmentGroupGUID = @AttachmentGroupGUID
		ELSE
			UPDATE CMS_Attachment SET AttachmentOrder = AttachmentOrder - 1 WHERE AttachmentOrder = ((SELECT AttachmentOrder FROM CMS_Attachment WHERE AttachmentID = @AttachmentID) + 1 ) AND AttachmentFormGUID = @AttachmentFormGUID AND AttachmentGroupGUID IS NULL
	ELSE
		IF @AttachmentGroupGUID IS NOT NULL
			UPDATE CMS_Attachment SET AttachmentOrder = AttachmentOrder - 1 WHERE AttachmentOrder = ((SELECT AttachmentOrder FROM CMS_Attachment WHERE AttachmentID = @AttachmentID) + 1 ) AND AttachmentDocumentID = @DocumentID AND AttachmentGroupGUID = @AttachmentGroupGUID
		ELSE
			UPDATE CMS_Attachment SET AttachmentOrder = AttachmentOrder - 1 WHERE AttachmentOrder = ((SELECT AttachmentOrder FROM CMS_Attachment WHERE AttachmentID = @AttachmentID) + 1 ) AND AttachmentDocumentID = @DocumentID AND AttachmentGroupGUID IS NULL
		
	/* Move the current attachment down */
	UPDATE CMS_Attachment SET AttachmentOrder = AttachmentOrder + 1 WHERE (AttachmentID = @AttachmentID) AND (AttachmentOrder < @MaxAttachmentOrder)
END
