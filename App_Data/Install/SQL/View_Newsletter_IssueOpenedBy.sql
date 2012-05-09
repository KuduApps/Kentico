CREATE VIEW [View_Newsletter_IssueOpenedBy]
AS
SELECT  Newsletter_OpenedEmail.IssueID, View_NewsletterSubscriberUserRole_Joined.SubscriberID, 
        View_NewsletterSubscriberUserRole_Joined.SubscriberFullName, 
        ISNULL(View_NewsletterSubscriberUserRole_Joined.SubscriberEmail, View_NewsletterSubscriberUserRole_Joined.Email) AS SubscriberEmail, 
        Newsletter_OpenedEmail.OpenedWhen, Newsletter_NewsletterIssue.IssueSiteID AS SiteID
FROM    Newsletter_OpenedEmail INNER JOIN Newsletter_NewsletterIssue ON Newsletter_OpenedEmail.IssueID = Newsletter_NewsletterIssue.IssueID 
	LEFT OUTER JOIN View_NewsletterSubscriberUserRole_Joined ON Newsletter_OpenedEmail.SubscriberID = View_NewsletterSubscriberUserRole_Joined.SubscriberID
GO
