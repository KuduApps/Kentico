CREATE VIEW [View_Newsletter_Issue_Joined]
AS
SELECT Issue.IssueID,
	   Issue.IssueNewsletterID,
       Issue.IssueSubject,
       Issue.IssueMailoutTime,
       Issue.IssueSentEmails,
       ISNULL(Issue.IssueOpenedEmails, 0) AS IssueOpenedEmails,
       Issue.IssueUnsubscribed, 
       ISNULL(LinkCount, 0) AS LinkCount,
       ISNULL(Issue.IssueBounces, 0) IssueBounces 
  FROM Newsletter_NewsletterIssue AS Issue LEFT OUTER JOIN  
     (SELECT LinkIssueID, COUNT(LinkIssueID) AS LinkCount FROM Newsletter_Link GROUP BY LinkIssueID) AS Links 
      ON Issue.IssueID = Links.LinkIssueID
GO
