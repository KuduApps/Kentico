CREATE PROCEDURE [Proc_Newsletter_Issue_Unsubscribe]	
	@IssueID INT
AS
BEGIN
	SET NOCOUNT ON;
	
	BEGIN TRANSACTION
	
	DECLARE @Unsubscribed AS INT
	SET @Unsubscribed = (SELECT IssueUnsubscribed FROM [Newsletter_NewsletterIssue] WHERE IssueID = @IssueID)	
	SET @Unsubscribed = @Unsubscribed + 1
	UPDATE Newsletter_NewsletterIssue SET IssueUnsubscribed = @Unsubscribed WHERE IssueID = @IssueID			
		
	COMMIT TRANSACTION    
END
