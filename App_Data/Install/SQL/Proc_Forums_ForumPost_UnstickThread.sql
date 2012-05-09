CREATE PROCEDURE [Proc_Forums_ForumPost_UnstickThread]
@PostForumID int,
@PostID int
AS
BEGIN
	DECLARE @order int;
	SET @order = (SELECT PostStickOrder FROM Forums_ForumPost WHERE PostId = @PostID)
	-- SET  order for new sticky thread
	UPDATE Forums_ForumPost SET PostStickOrder = 0 WHERE PostId = @PostID
	-- Increment  post order for other posts
	UPDATE Forums_ForumPost SET PostStickOrder = (PostStickOrder - 1) WHERE PostForumID = @PostForumID AND PostStickOrder > @order
END
