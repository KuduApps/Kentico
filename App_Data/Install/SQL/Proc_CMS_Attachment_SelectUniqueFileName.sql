CREATE PROCEDURE [Proc_CMS_Attachment_SelectUniqueFileName]
	@AttachmentFormGUID uniqueidentifier,
	@AttachmentName nvarchar(255),
	@AttachmentExtension nvarchar(50),
	@CurrentAttachmentID int,
	@SelectFromVersionHistory bit,
	@AttachmentDocumentID int
AS
BEGIN
	IF @AttachmentFormGUID IS NOT NULL
		SELECT DISTINCT AttachmentID, AttachmentName FROM CMS_Attachment WHERE AttachmentFormGUID = @AttachmentFormGUID AND AttachmentName = @AttachmentName AND AttachmentExtension = @AttachmentExtension AND AttachmentID <> @CurrentAttachmentID AND AttachmentDocumentID IS NULL
	ELSE	
		IF @SelectFromVersionHistory = 1
			BEGIN
				DECLARE @VersionHistoryID int;
				SET @VersionHistoryID = (SELECT TOP 1 VersionHistoryID FROM CMS_AttachmentHistory INNER JOIN CMS_VersionAttachment ON CMS_AttachmentHistory.AttachmentHistoryID = CMS_VersionAttachment.AttachmentHistoryID WHERE CMS_AttachmentHistory.AttachmentHistoryID = @CurrentAttachmentID);
				SELECT DISTINCT CMS_VersionAttachment.AttachmentHistoryID, AttachmentName FROM CMS_AttachmentHistory INNER JOIN CMS_VersionAttachment ON CMS_AttachmentHistory.AttachmentHistoryID = CMS_VersionAttachment.AttachmentHistoryID WHERE AttachmentName = @AttachmentName AND AttachmentExtension = @AttachmentExtension AND CMS_AttachmentHistory.AttachmentHistoryID <> @CurrentAttachmentID AND AttachmentDocumentID = @AttachmentDocumentID AND CMS_VersionAttachment.VersionHistoryID=@VersionHistoryID
			END
		ELSE
			SELECT DISTINCT AttachmentID, AttachmentName FROM CMS_Attachment WHERE AttachmentName = @AttachmentName AND AttachmentExtension = @AttachmentExtension AND AttachmentID <> @CurrentAttachmentID AND AttachmentDocumentID = @AttachmentDocumentID
END
