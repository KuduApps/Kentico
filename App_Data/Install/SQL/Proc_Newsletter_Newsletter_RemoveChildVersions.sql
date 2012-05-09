CREATE PROCEDURE [Proc_Newsletter_Newsletter_RemoveChildVersions]
@ID int
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
 SET NOCOUNT ON;
 
 -- Newsletter_NewsletterIssue
  DELETE FROM [CMS_ObjectVersionHistory] WHERE [VersionObjectType] = N'newsletter.issue' AND [VersionObjectID] IN (
      SELECT [IssueID] FROM [Newsletter_NewsletterIssue] WHERE [IssueNewsletterID] = @ID
  );
END
