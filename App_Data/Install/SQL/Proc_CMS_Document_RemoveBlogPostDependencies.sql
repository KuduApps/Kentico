-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_CMS_Document_RemoveBlogPostDependencies]
	@DocumentID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DELETE FROM Blog_Comment WHERE CommentPostDocumentID = @DocumentID; 
	DELETE FROM Blog_PostSubscription WHERE SubscriptionPostDocumentID = @DocumentID;
END
