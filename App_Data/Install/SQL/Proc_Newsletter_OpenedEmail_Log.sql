CREATE PROCEDURE [Proc_Newsletter_OpenedEmail_Log]
@SubscriberGUID uniqueidentifier,
    @IssueGUID uniqueidentifier,
    @SiteID int
AS
BEGIN
SET NOCOUNT ON;
BEGIN TRANSACTION
DECLARE @SubscriberID AS int
SET @SubscriberID = (SELECT SubscriberID FROM Newsletter_Subscriber WHERE SubscriberGUID = @SubscriberGUID AND SubscriberSiteID = @SiteID)
DECLARE @IssueID AS int
SET @IssueID = (SELECT IssueID FROM Newsletter_NewsletterIssue WHERE IssueGUID = @IssueGUID AND IssueSiteID = @SiteID)
EXEC Proc_Newsletter_OpenedEmail_Log_Internal @SubscriberID, @IssueID
SELECT @IssueID
		
COMMIT TRANSACTION
END
