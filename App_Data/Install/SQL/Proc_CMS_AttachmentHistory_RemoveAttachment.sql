CREATE PROCEDURE [Proc_CMS_AttachmentHistory_RemoveAttachment] 
	@VersionHistoryID int,
	@AttachmentGUID uniqueidentifier
AS
BEGIN
	/* Get the AttachmentHistoryID */
	DECLARE @AttachmentHistoryID int	
	SELECT @AttachmentHistoryID = AttachmentHistoryID FROM CMS_AttachmentHistory WHERE AttachmentGUID = @AttachmentGUID AND AttachmentHistoryID IN (SELECT AttachmentHistoryID FROM CMS_VersionAttachment WHERE VersionHistoryID = @VersionHistoryID)
	/* Remove the binding */
	DELETE FROM CMS_VersionAttachment WHERE VersionHistoryID = @VersionHistoryID AND AttachmentHistoryID = @AttachmentHistoryID
	/* Remove the attachment if no more bindings exist */
	IF ( NOT EXISTS (SELECT VersionHistoryID FROM CMS_VersionAttachment WHERE AttachmentHistoryID = @AttachmentHistoryID) )
	BEGIN
		DELETE FROM CMS_AttachmentHistory WHERE AttachmentHistoryID = @AttachmentHistoryID
	END
END
