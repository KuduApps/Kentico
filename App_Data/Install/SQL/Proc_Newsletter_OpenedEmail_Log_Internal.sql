-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_Newsletter_OpenedEmail_Log_Internal]
	@SubscriberID int,
    @IssueID int 
AS
BEGIN
	SET NOCOUNT ON;
	IF (NOT @SubscriberID IS NULL AND NOT @IssueID IS NULL)
		BEGIN
			DECLARE @OpenedWhen AS datetime
			SET @OpenedWhen = (SELECT OpenedWhen FROM Newsletter_OpenedEmail WHERE (IssueID = @IssueID AND SubscriberID = @SubscriberID))
			IF (@OpenedWhen IS NULL)
				BEGIN
					INSERT INTO Newsletter_OpenedEmail(SubscriberID, IssueID, OpenedWhen) VALUES (@SubscriberID, @IssueID, CURRENT_TIMESTAMP)
					DECLARE @IssueOpenedEmails AS int
					SET @IssueOpenedEmails = (SELECT COALESCE(IssueOpenedEmails, 0) FROM Newsletter_NewsletterIssue WHERE (IssueID = @IssueID))
					SET @IssueOpenedEmails = @IssueOpenedEmails + 1
					
					UPDATE Newsletter_NewsletterIssue SET IssueOpenedEmails = @IssueOpenedEmails WHERE (IssueID = @IssueID)									
				END							
		END
END
