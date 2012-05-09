CREATE PROCEDURE [Proc_Newsletter_Issue_AddSentEmails]
	@IssueID INT,
	@Emails INT,
	@MailoutTime DATETIME
AS
BEGIN
	SET NOCOUNT ON;
	
	BEGIN TRANSACTION
	
	DECLARE @SentEmails AS INT
	SET @SentEmails = (SELECT IssueSentEmails FROM [Newsletter_NewsletterIssue] WHERE IssueID = @IssueID)
	SET @SentEmails = @SentEmails + @Emails;
	UPDATE [Newsletter_NewsletterIssue] SET IssueSentEmails = @SentEmails, IssueMailoutTime = @MailoutTime WHERE IssueID = @IssueID		
	
	COMMIT TRANSACTION    
END
