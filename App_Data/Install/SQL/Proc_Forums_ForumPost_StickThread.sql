CREATE PROCEDURE [Proc_Forums_ForumPost_StickThread]
@PostForumID int,
@PostID int
AS
BEGIN
	-- Increment  post order for other posts
	UPDATE Forums_ForumPost SET PostStickOrder = (PostStickOrder + 1) WHERE PostForumID = @PostForumID AND PostStickOrder > 0
	-- SET  order for new sticky thread
	UPDATE Forums_ForumPost SET PostStickOrder = 1 WHERE PostId = @PostID
END
