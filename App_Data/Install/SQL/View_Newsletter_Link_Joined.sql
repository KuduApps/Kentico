CREATE VIEW [View_Newsletter_Link_Joined]
AS
SELECT Newsletter_Link.LinkID,       
       Newsletter_Link.LinkTarget, 
       Newsletter_Link.LinkDescription,
       Newsletter_Link.LinkOutdated,
       ISNULL(Clicks, 0) AS UniqueClicks,
       ISNULL(Newsletter_Link.LinkTotalClicks, 0) AS TotalClicks,               
       (ISNULL(Clicks, 0) / NULLIF(IssueSentEmails, 0)) * 100 AS ClickRate,
       Newsletter_Link.LinkIssueID AS IssueID,
       Issues.IssueSiteID AS SiteID
  FROM Newsletter_Link
	   LEFT OUTER JOIN
       (SELECT LinkID, CAST(COUNT(Clicks) AS float) AS Clicks FROM Newsletter_SubscriberLink GROUP BY LinkID) AS Links ON Links.LinkID = Newsletter_Link.LinkID
       LEFT OUTER JOIN
       (SELECT IssueID, IssueSentEmails, IssueSiteID FROM Newsletter_NewsletterIssue) AS Issues ON Issues.IssueID = Newsletter_Link.LinkIssueID
GO
