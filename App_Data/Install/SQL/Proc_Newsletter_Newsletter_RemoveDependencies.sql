-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_Newsletter_Newsletter_RemoveDependencies]
	@ID int
AS
BEGIN
	  -- SET NOCOUNT ON added to prevent extra result sets from
  -- interfering with SELECT statements.
  SET NOCOUNT ON;
  
  BEGIN TRANSACTION;
  
  -- Newsletter_Emails
    DELETE FROM Newsletter_Emails WHERE EmailNewsletterIssueID IN
    (SELECT IssueID FROM Newsletter_NewsletterIssue WHERE IssueNewsletterID = @ID);
  
  -- Newsletter_SubscriberLink
  DELETE FROM Newsletter_SubscriberLink WHERE LinkID IN
    (SELECT LinkID FROM Newsletter_Link WHERE LinkIssueID IN   
      (SELECT IssueID FROM Newsletter_NewsletterIssue WHERE IssueNewsletterID = @ID));
    
    -- Newsletter_Link
    DELETE FROM Newsletter_Link WHERE LinkIssueID IN
    (SELECT IssueID FROM Newsletter_NewsletterIssue WHERE IssueNewsletterID = @ID);     
  
  -- Newsletter_OpenedEmail
  DELETE FROM Newsletter_OpenedEmail WHERE IssueID IN
    (SELECT IssueID FROM Newsletter_NewsletterIssue WHERE IssueNewsletterID = @ID);
    
  -- Newsletter_NewsletterIssue
  DELETE FROM Newsletter_NewsletterIssue WHERE IssueNewsletterID = @ID;
  
  -- Newsletter_SubscriberNewsletter
  DELETE FROM Newsletter_SubscriberNewsletter WHERE NewsletterID = @ID;
  
  COMMIT TRANSACTION;
END
