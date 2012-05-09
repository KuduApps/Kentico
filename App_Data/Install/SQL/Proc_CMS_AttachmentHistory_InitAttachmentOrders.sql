CREATE PROCEDURE [Proc_CMS_AttachmentHistory_InitAttachmentOrders]
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
	
	/* Declare the selection table */
	DECLARE @attachmentTable TABLE (
		AttachmentHistoryID int NOT NULL,
		AttachmentName nvarchar(255) NOT NULL,
		AttachmentOrder int NULL
		
	);
	
	/* Get the grouped or unsorted attachments */
	IF @AttachmentGroupGUID IS NOT NULL
		INSERT INTO @attachmentTable SELECT AttachmentHistoryID, AttachmentName,  AttachmentOrder FROM CMS_AttachmentHistory WHERE  AttachmentGroupGUID = @AttachmentGroupGUID AND AttachmentDocumentID=@AttachmentDocumentID
	ELSE 
		INSERT INTO @attachmentTable SELECT AttachmentHistoryID, AttachmentName,  AttachmentOrder FROM CMS_AttachmentHistory WHERE  AttachmentGroupGUID IS NULL AND AttachmentDocumentID=@AttachmentDocumentID AND AttachmentIsUnsorted=1
	
	
	
	
	/* Declare the cursor to loop through the table */
	DECLARE @attachmentCursor CURSOR;
    SET @attachmentCursor = CURSOR FOR SELECT AttachmentHistoryID, AttachmentName, AttachmentOrder FROM @attachmentTable ORDER BY AttachmentOrder, AttachmentName, AttachmentHistoryID;
    
	/* Assign the numbers to the attachments */
	DECLARE @currentIndex int, @currentAttachmentOrder int, @currentAttachmentHistoryID int;
	SET @currentIndex = 1;
	
	DECLARE @currentAttachmentName nvarchar(255);
	
	/* Loop through the table */
	OPEN @attachmentCursor
	FETCH NEXT FROM @attachmentCursor INTO @currentAttachmentHistoryID,@currentAttachmentName, @currentAttachmentOrder;
	WHILE @@FETCH_STATUS = 0
	BEGIN
		/* Set the attachments' index if different */
		IF @currentAttachmentOrder IS NULL OR @currentAttachmentOrder <> @currentIndex
			UPDATE CMS_AttachmentHistory SET AttachmentOrder = @currentIndex WHERE AttachmentHistoryID = @currentAttachmentHistoryID;
		/* Get next record */
		SET @currentIndex = @currentIndex + 1;
		FETCH NEXT FROM @attachmentCursor INTO @currentAttachmentHistoryID,@currentAttachmentName, @currentAttachmentOrder;
	END
	CLOSE @attachmentCursor;
	DEALLOCATE @attachmentCursor;
	
END
