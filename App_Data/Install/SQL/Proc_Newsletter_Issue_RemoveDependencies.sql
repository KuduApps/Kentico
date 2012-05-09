-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_Newsletter_Issue_RemoveDependencies]
	@ID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	BEGIN TRANSACTION;
	
	-- Newsletter_Emails
    DELETE FROM Newsletter_Emails WHERE EmailNewsletterIssueID = @ID;
    
    -- Newsletter_SubscriberLink
    DELETE FROM Newsletter_SubscriberLink WHERE LinkID IN
		(SELECT LinkID FROM Newsletter_Link WHERE LinkIssueID = @ID);	    
    
    -- Newsletter_Link
    DELETE FROM Newsletter_Link WHERE LinkIssueID = @ID;
    
    -- Newsletter_OpenedEmail
	DELETE FROM Newsletter_OpenedEmail WHERE IssueID = @ID;
	
	COMMIT TRANSACTION;
END
