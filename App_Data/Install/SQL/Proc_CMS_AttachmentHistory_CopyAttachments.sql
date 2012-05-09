CREATE PROCEDURE  [Proc_CMS_AttachmentHistory_CopyAttachments] 
	@SourceVersionHistoryID int,
	@TargetVersionHistoryID int
AS
BEGIN
	/* Declare the cursor to loop through the table */
	DECLARE @bindingCursor CURSOR;
    SET @bindingCursor = CURSOR FOR SELECT AttachmentHistoryID FROM CMS_VersionAttachment WHERE VersionHistoryID = @SourceVersionHistoryID;
	DECLARE @currentAttachmentHistoryID int
	OPEN @bindingCursor;
	FETCH NEXT FROM @bindingCursor INTO @currentAttachmentHistoryID;
	WHILE @@FETCH_STATUS = 0
	BEGIN
		/* Insert the binding */
		INSERT INTO CMS_VersionAttachment (VersionHistoryID, AttachmentHistoryID) VALUES (@TargetVersionHistoryID, @currentAttachmentHistoryID);
	
		/* Get next record */
		FETCH NEXT FROM @bindingCursor INTO @currentAttachmentHistoryID;
	END	
END
