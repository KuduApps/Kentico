CREATE PROCEDURE [Proc_CMS_Email_DeleteArchived]
@SiteID int,
	@ExpirationDate datetime,
	@BatchSize int
AS
BEGIN
SET NOCOUNT ON;
-- Code for archived email status
DECLARE @Archived AS int
SET @Archived = 3
	
-- Declare table for emails
DECLARE @Emails TABLE (EmailID int NOT NULL, PRIMARY KEY(EmailID))
BEGIN
    
	SET ROWCOUNT @BatchSize
	-- Get global e-mails
        IF ((@SiteID = 0) OR (@SiteID IS NULL))
		INSERT INTO @Emails 
                	SELECT EmailID FROM CMS_Email 
				WHERE (EmailLastSendAttempt <= @ExpirationDate) AND (EmailSiteID IS NULL OR EmailSiteID = 0) AND (EmailStatus = @Archived)
				
	-- Get all e-mails attached to the site			
	ELSE                   
		INSERT INTO @Emails 
			SELECT EmailID FROM CMS_Email 
				WHERE (EmailLastSendAttempt <= @ExpirationDate) AND (EmailSiteID = @SiteID) AND (EmailStatus = @Archived)
	SET ROWCOUNT 0
END
BEGIN TRANSACTION
          
-- Delete attachment binding 
DELETE FROM [CMS_AttachmentForEmail] WHERE EmailID IN (SELECT EmailID FROM @Emails)
    
-- Delete user binding
DELETE FROM [CMS_EmailUser] WHERE EmailID IN (SELECT EmailID FROM @Emails)
	
-- Delete all attachments that have no bindings
DELETE FROM CMS_EmailAttachment WHERE AttachmentID NOT IN (SELECT DISTINCT AttachmentID FROM CMS_AttachmentForEmail)
    
-- Delete e-mails
DELETE FROM [CMS_Email] WHERE EmailID IN (SELECT EmailID FROM @Emails)
      
COMMIT TRANSACTION;
END
