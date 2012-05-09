-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_Newsletter_Emails_CreateMails]
	@NewsletterIssueID int,
	@NewsletterID int,
	@SiteID int,
	@MonitoringEnabled bit,
	@BounceLimit int,
	@Now datetime
AS
BEGIN
    SET NOCOUNT ON;
	BEGIN TRANSACTION
        
    IF (@MonitoringEnabled = 1)
		BEGIN		   			
				
			-- Create newsletter email queue items for approved and active subscribers
			INSERT INTO Newsletter_Emails (EmailNewsletterIssueID, EmailSubscriberID, EmailSiteID, EmailGUID, EmailLastModified)
				 SELECT @NewsletterIssueID, Subscriptions.SubscriberID, @SiteID, NEWID(), @Now
				   FROM Newsletter_SubscriberNewsletter AS Subscriptions 
				        LEFT OUTER JOIN
				        Newsletter_Subscriber AS Subscribers ON Subscriptions.SubscriberID = Subscribers.SubscriberID
			      WHERE NewsletterID = @NewsletterID AND 
				       (Subscriptions.SubscriptionApproved = 1 OR Subscriptions.SubscriptionApproved IS NULL) AND	
					   (((Subscribers.SubscriberType = 'cms.user' OR Subscribers.SubscriberType IS NULL) AND
					     (Subscribers.SubscriberBounces IS NULL OR Subscribers.SubscriberBounces < @BounceLimit OR @BounceLimit <= 0)) OR
					    (Subscribers.SubscriberType = 'cms.role') OR (Subscribers.SubscriberType = 'om.contactgroup'))
			   ORDER BY Subscriptions.SubscriberID
		END
	ELSE
		BEGIN
		
			-- Create newsletter email queue items for approved subscribers
			INSERT INTO Newsletter_Emails (EmailNewsletterIssueID, EmailSubscriberID, EmailSiteID, EmailGUID, EmailLastModified)
				 SELECT @NewsletterIssueID, Subscriptions.SubscriberID, @SiteID, NEWID(), @Now
				   FROM Newsletter_SubscriberNewsletter AS Subscriptions 				       
			      WHERE NewsletterID = @NewsletterID AND 
				       (Subscriptions.SubscriptionApproved = 1 OR Subscriptions.SubscriptionApproved IS NULL)
			   ORDER BY Subscriptions.SubscriberID
		END
	COMMIT TRANSACTION;
END
