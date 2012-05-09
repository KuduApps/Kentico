CREATE VIEW [View_Newsletter_Subscriptions_Joined]
AS
SELECT     Newsletter_Subscriber.SubscriberID, Newsletter_Subscriber.SubscriberFullName, Newsletter_Subscriber.SubscriberEmail, CMS_User.Email, 
                      Newsletter_SubscriberNewsletter.SubscriptionApproved, Newsletter_SubscriberNewsletter.NewsletterID, Newsletter_Subscriber.SubscriberType, 
                      Newsletter_Subscriber.SubscriberBounces, Newsletter_Newsletter.NewsletterDisplayName
FROM         Newsletter_Subscriber INNER JOIN
                      Newsletter_SubscriberNewsletter ON Newsletter_Subscriber.SubscriberID = Newsletter_SubscriberNewsletter.SubscriberID INNER JOIN
                      Newsletter_Newsletter ON Newsletter_Newsletter.NewsletterID = Newsletter_SubscriberNewsletter.NewsletterID LEFT OUTER JOIN
                      CMS_User ON Newsletter_Subscriber.SubscriberRelatedID = CMS_User.UserID AND Newsletter_Subscriber.SubscriberType = 'cms.user'
GO
