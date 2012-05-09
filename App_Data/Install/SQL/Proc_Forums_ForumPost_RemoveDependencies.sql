CREATE PROCEDURE [Proc_Forums_ForumPost_RemoveDependencies] 
	@Path varchar(450)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	-- Remove dependencies to the subscribers
	DELETE FROM [Forums_ForumSubscription] 
	WHERE SubscriptionPostID IN 
		(
			SELECT PostId FROM Forums_ForumPost 
			WHERE ((PostIDPath LIKE N''+@Path+'/%') OR (PostIDPath LIKE @Path))
		);
	-- Remove dependencies to the post attachments
	DELETE FROM [Forums_Attachment] 
	WHERE AttachmentPostID IN 
		(
			SELECT PostId FROM Forums_ForumPost 
			WHERE ((PostIDPath LIKE N''+@Path+'/%') OR (PostIDPath LIKE @Path))
		);
	--  Remove dependencies to the user favorites
	DELETE FROM [Forums_UserFavorites] WHERE PostId IN
		(
			SELECT PostId FROM Forums_ForumPost 
			WHERE ((PostIDPath LIKE N''+@Path+'/%') OR (PostIDPath LIKE @Path))
		);		
	-- Remove references within the table
	UPDATE Forums_ForumPost SET PostParentID=NULL WHERE (PostIDPath LIKE N''+@Path+'/%');
	-- Delete all subposts
	DELETE FROM Forums_ForumPost WHERE (PostIDPath LIKE N''+@Path+'/%');
END
