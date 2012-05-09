CREATE PROCEDURE [Proc_Newsletter_Link_Log]
@LinkGUID uniqueidentifier,
@SubscriberGUID uniqueidentifier,	    
@SiteID int
AS
BEGIN
SET NOCOUNT ON;
  BEGIN TRANSACTION
  DECLARE @LinkID AS int
  SET @LinkID = (SELECT LinkID 
                   FROM Newsletter_Link INNER JOIN Newsletter_NewsletterIssue   
                     ON Newsletter_Link.LinkIssueID = Newsletter_NewsletterIssue.IssueID
                  WHERE Newsletter_NewsletterIssue.IssueSiteID = @SiteID AND Newsletter_Link.LinkGUID = @LinkGUID)
  DECLARE @SubscriberID AS int
  SET @SubscriberID = (SELECT SubscriberID FROM Newsletter_Subscriber WHERE SubscriberGUID = @SubscriberGUID AND SubscriberSiteID = @SiteID)
  
  DECLARE @SubscriberType AS nvarchar(30)
  SET @SubscriberType = (SELECT SubscriberType FROM Newsletter_Subscriber WHERE SubscriberID = @SubscriberID)
  IF (NOT @LinkID IS NULL AND NOT @SubscriberID IS NULL)
    BEGIN
      -- Increment click counters
      DECLARE @LinkTotalClicks AS int      
      SET @LinkTotalClicks = (SELECT COALESCE(LinkTotalClicks, 0) FROM Newsletter_Link WHERE LinkID = @LinkID)
      SET @LinkTotalClicks = @LinkTotalClicks + 1      
      UPDATE Newsletter_Link SET LinkTotalClicks = @LinkTotalClicks WHERE LinkID = @LinkID
      -- Do not increment clicks for contact group subscriber, clicks are obtained from activities
      IF (NOT @SubscriberType LIKE 'om.contactgroup' OR @SubscriberType IS NULL)
	BEGIN
	  DECLARE @Clicks AS int
	  SET @Clicks = (SELECT Clicks FROM Newsletter_SubscriberLink WHERE LinkID = @LinkID AND SubscriberID = @SubscriberID)
	  IF (NOT @Clicks IS NULL)
		BEGIN
		  SET @Clicks = @Clicks + 1
		  UPDATE Newsletter_SubscriberLink SET Clicks = @Clicks WHERE LinkID = @LinkID AND SubscriberID = @SubscriberID
		END
	  ELSE
		INSERT INTO Newsletter_SubscriberLink VALUES (@SubscriberID, @LinkID, 1)
	  -- If opened e-mail tracking is enabled, log another open e-mail
      	  DECLARE @IssueID AS int
      	  SET @IssueID = (SELECT LinkIssueID FROM Newsletter_Link WHERE LinkID = @LinkID)           
      
      	  DECLARE @OpenEmailTrackingEnabled AS bit
      	  SET @OpenEmailTrackingEnabled = (SELECT NewsletterTrackOpenEmails FROM Newsletter_Newsletter WHERE NewsletterID = 
									   (SELECT IssueNewsletterID FROM Newsletter_NewsletterIssue WHERE IssueID = @IssueID))
      
      	  IF (@OpenEmailTrackingEnabled = 1)
		EXEC Proc_Newsletter_OpenedEmail_Log_Internal @SubscriberID, @IssueID
                
	END
      -- Return URL of the link
      SELECT LinkTarget FROM Newsletter_Link WHERE LinkID = @LinkID
            
    END
  COMMIT TRANSACTION
END
