CREATE PROCEDURE [Proc_CMS_Attachment_InitAttachmentOrders]
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
	SET @AttachmentGroupGUID = (SELECT TOP 1 AttachmentGroupGUID FROM CMS_Attachment WHERE AttachmentID = @AttachmentID);
	
	/* Get GUID of form */
	DECLARE @AttachmentFormGUID uniqueidentifier;
	SET @AttachmentFormGUID = (SELECT TOP 1 AttachmentFormGUID FROM CMS_Attachment WHERE AttachmentID = @AttachmentID);
	
	/* Declare the selection table */
	DECLARE @attachmentTable TABLE (
		AttachmentID int NOT NULL,
		AttachmentName nvarchar(255) NOT NULL,
		AttachmentOrder int NULL		
	);
	
	/* Get the grouped, unsorted or temporary attachments */
	IF @AttachmentFormGUID IS NOT NULL
		IF @AttachmentGroupGUID IS NOT NULL
			INSERT INTO @attachmentTable SELECT AttachmentID, AttachmentName,  AttachmentOrder FROM CMS_Attachment WHERE  AttachmentGroupGUID = @AttachmentGroupGUID AND AttachmentFormGUID=@AttachmentFormGUID
		ELSE 
			INSERT INTO @attachmentTable SELECT AttachmentID, AttachmentName,  AttachmentOrder FROM CMS_Attachment WHERE  AttachmentGroupGUID IS NULL AND AttachmentFormGUID=@AttachmentFormGUID AND AttachmentIsUnsorted=1	
	ELSE
		IF @AttachmentGroupGUID IS NOT NULL
			INSERT INTO @attachmentTable SELECT AttachmentID, AttachmentName,  AttachmentOrder FROM CMS_Attachment WHERE  AttachmentGroupGUID = @AttachmentGroupGUID AND AttachmentDocumentID=@DocumentID
		ELSE 
			INSERT INTO @attachmentTable SELECT AttachmentID, AttachmentName,  AttachmentOrder FROM CMS_Attachment WHERE  AttachmentGroupGUID IS NULL AND AttachmentDocumentID=@DocumentID AND AttachmentIsUnsorted=1
		
	/* Declare the cursor to loop through the table */
	DECLARE @attachmentCursor CURSOR;
    SET @attachmentCursor = CURSOR FOR SELECT AttachmentID, AttachmentName, AttachmentOrder FROM @attachmentTable ORDER BY AttachmentOrder, AttachmentName, AttachmentID;
    
	/* Assign the numbers to the attachments */
	DECLARE @currentIndex int, @currentAttachmentOrder int, @currentAttachmentID int;
	SET @currentIndex = 1;
	
	DECLARE @currentAttachmentName nvarchar(255);
	
	/* Loop through the table */
	OPEN @attachmentCursor
	FETCH NEXT FROM @attachmentCursor INTO @currentAttachmentID, @currentAttachmentName, @currentAttachmentOrder;
	WHILE @@FETCH_STATUS = 0
	BEGIN
		/* Set the attachments' index if different */
		IF @currentAttachmentOrder IS NULL OR @currentAttachmentOrder <> @currentIndex
			UPDATE CMS_Attachment SET AttachmentOrder = @currentIndex WHERE AttachmentID = @currentAttachmentID;
		/* Get next record */
		SET @currentIndex = @currentIndex + 1;
		FETCH NEXT FROM @attachmentCursor INTO @currentAttachmentID, @currentAttachmentName, @currentAttachmentOrder;
	END
	CLOSE @attachmentCursor;
	DEALLOCATE @attachmentCursor;
	
END
