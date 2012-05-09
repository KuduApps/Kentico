-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_CMS_Email_DeleteAll]
    @Status int,
    @SiteID int,
    @OnlyFailed bit
AS
BEGIN
    BEGIN TRANSACTION
	SET NOCOUNT ON;
    
    DECLARE @emails TABLE ( EmailID int not null ) 
    IF @OnlyFailed = 1
      BEGIN
        IF @SiteID = 0         -- Get global e-mails only (SiteID = 0 or SiteID is null)
          INSERT INTO @emails SELECT EmailID FROM CMS_Email WHERE EmailLastSendResult IS NOT NULL AND (EmailSiteID = @SiteID OR EmailSiteID IS NULL)
        ELSE IF @SiteID = -1   -- Get all e-mails (ignore SiteID)
          INSERT INTO @emails SELECT EmailID FROM CMS_Email WHERE EmailLastSendResult IS NOT NULL
        ELSE                   -- Get all e-mails attached to site
          INSERT INTO @emails SELECT EmailID FROM CMS_Email WHERE EmailLastSendResult IS NOT NULL AND EmailSiteID = @SiteID
      END
    ELSE
      BEGIN
        IF @SiteID = 0         -- Get global e-mails only (SiteID = 0 or SiteID is null)
          INSERT INTO @emails SELECT EmailID FROM CMS_Email WHERE EmailStatus = @Status AND (EmailSiteID = @SiteID OR EmailSiteID IS NULL)
        ELSE IF @SiteID = -1   -- Get all e-mails (ignore SiteID)
          INSERT INTO @emails SELECT EmailID FROM CMS_Email WHERE EmailStatus = @Status
        ELSE                   -- Get all e-mails attached to site
          INSERT INTO @emails SELECT EmailID FROM CMS_Email WHERE EmailStatus = @Status AND EmailSiteID = @SiteID
      END
    -- Delete attachment binding 
    DELETE FROM [CMS_AttachmentForEmail] WHERE EmailID IN (SELECT EmailID FROM @emails)
	-- Delete user binding
	DELETE FROM [CMS_EmailUser] WHERE EmailID IN (SELECT EmailID FROM @emails)
    -- Delete all attachments that have no bindings
    DELETE FROM CMS_EmailAttachment WHERE AttachmentID NOT IN (SELECT DISTINCT AttachmentID FROM CMS_AttachmentForEmail)
    -- Delete e-mails
    DELETE FROM [CMS_Email] WHERE EmailID IN (SELECT EmailID FROM @emails)
    COMMIT TRANSACTION;
END
