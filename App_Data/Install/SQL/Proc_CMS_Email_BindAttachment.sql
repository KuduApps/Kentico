CREATE PROCEDURE [Proc_CMS_Email_BindAttachment] 
    @EmailID int,
    @Name nvarchar(255),
    @Ext nvarchar(50),
    @Size int,
    @MimeType nvarchar(100),
    @Bin image,
	@ContentID nvarchar(255),
	@GUID uniqueidentifier,
	@LastModified datetime,
	@SiteID int
AS
BEGIN
    BEGIN TRANSACTION
	SET NOCOUNT ON;
    DECLARE @AttID int;
    
    SET @AttID = 0;
	/* Search for attachment with specified parameters */
    SELECT @AttID = AttachmentID FROM CMS_EmailAttachment WHERE
    AttachmentName = @Name AND AttachmentExtension = @Ext AND
    AttachmentSize = @Size AND AttachmentMimeType = @MimeType AND
	(AttachmentContentID = @ContentID OR (AttachmentContentID IS NULL AND @ContentID IS NULL)) AND
	AttachmentGUID = @GUID AND AttachmentLastModified = @LastModified AND
	(AttachmentSiteID = @SiteID OR (AttachmentSiteID IS NULL AND @SiteID IS NULL))
    IF @AttID > 0
	  BEGIN
		/* Bind attachment to e-mail */
		DECLARE @Count int;
		SELECT @Count = COUNT(EmailID) FROM [CMS_AttachmentForEmail] WHERE EmailID = @EmailID AND AttachmentID = @AttID
		IF @Count = 0
		  BEGIN
			INSERT INTO [CMS_AttachmentForEmail] ([EmailID], [AttachmentID]) VALUES (@EmailID, @AttID)
		  END;
	  END;
    ELSE
      BEGIN
		/* Create record for attachment */
        INSERT INTO [CMS_EmailAttachment] ([AttachmentName],[AttachmentExtension],[AttachmentSize],
            [AttachmentMimeType], [AttachmentBinary], [AttachmentGUID], [AttachmentLastModified], [AttachmentContentID],
			[AttachmentSiteID])
        VALUES (@Name, @Ext, @Size, @MimeType, @Bin, @GUID, @LastModified, @ContentID, @SiteID)
        SET @AttID = SCOPE_IDENTITY();
        /* Bind attachment to e-mail */
        INSERT INTO [CMS_AttachmentForEmail] ([EmailID], [AttachmentID]) VALUES (@EmailID, @AttID);
      END;
	SELECT * FROM CMS_EmailAttachment WHERE AttachmentID = @AttID;
    COMMIT TRANSACTION;
END
