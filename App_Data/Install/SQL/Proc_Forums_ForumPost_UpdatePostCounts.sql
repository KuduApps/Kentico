CREATE PROCEDURE [Proc_Forums_ForumPost_UpdatePostCounts]
	@PostID int
AS
BEGIN
	UPDATE [Forums_ForumPost]
	   SET [PostThreadPosts] = 
	   (SELECT COUNT(*) FROM 
			(SELECT * FROM [Forums_ForumPost] WHERE PostApproved = 1) AS T 
		WHERE  T.PostParentID = [Forums_ForumPost].PostId),
		
		[PostThreadPostsAbsolute] =
	   (SELECT COUNT(*) FROM 
			(SELECT * FROM [Forums_ForumPost]) AS T2 
		WHERE  T2.PostParentID = [Forums_ForumPost].PostId)
	WHERE PostId = @PostID
END
