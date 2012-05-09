CREATE PROCEDURE [Proc_Forums_Forum_UpdateForumCounts]
	@ID int,
	@ThreadLength int
AS
BEGIN
	-- Get number of posts
    DECLARE @Threads int;
	DECLARE @AbsoluteThreads int;
	DECLARE @Posts int;
	DECLARE @AbsolutePosts int;
	
	SET @Threads = (SELECT COUNT(*) FROM [Forums_ForumPost] WHERE ([PostForumID] = @ID)AND(PostApproved=1)AND(PostLevel = 0));
	SET @AbsoluteThreads = (SELECT COUNT(*) FROM [Forums_ForumPost] WHERE ([PostForumID] = @ID)AND(PostLevel = 0));
	SET @Posts = (SELECT COUNT(*) FROM [Forums_ForumPost] AS temp WHERE ([PostForumID] = @ID)AND(PostApproved=1) AND ((SELECT  PostId FROM Forums_ForumPost WHERE PostIDPath = SUBSTRING(temp.PostIDPath, 0, @ThreadLength) AND PostApproved = 1) IS NOT NULL));
	SET @AbsolutePosts = (SELECT COUNT(*) FROM [Forums_ForumPost] AS temp WHERE ([PostForumID] = @ID) AND ((SELECT  PostId FROM Forums_ForumPost WHERE PostIDPath = SUBSTRING(temp.PostIDPath, 0, @ThreadLength)) IS NOT NULL));
	-- Get last post
	DECLARE @LastPostTime datetime;
	DECLARE @LastPostUserName nvarchar(200);
	SELECT TOP 1 @LastPostTime = PostTime, @LastPostUserName = PostUserName
	FROM [Forums_ForumPost] AS temp
	WHERE
		([PostForumID] = @ID) AND
		(PostApproved=1) AND
		(
			(
				SELECT PostId
				FROM Forums_ForumPost
				WHERE
					PostIDPath = SUBSTRING(temp.PostIDPath, 0, @ThreadLength) AND
					PostApproved = 1
			)
			IS NOT NULL
		)
	ORDER BY PostTime DESC;
	
	-- Get last absolute post
	DECLARE @LastAbsolutePostTime datetime;
	DECLARE @LastAbsolutePostUserName nvarchar(200);
	SELECT TOP 1 @LastAbsolutePostTime = PostTime, @LastAbsolutePostUserName = PostUserName
	FROM [Forums_ForumPost] AS temp
	WHERE
		([PostForumID] = @ID) AND
		(
			(
				SELECT PostId
				FROM Forums_ForumPost
				WHERE
					PostIDPath = SUBSTRING(temp.PostIDPath, 0, @ThreadLength)
			)
			IS NOT NULL
		)
	ORDER BY PostTime DESC;
	-- Update the forum
	UPDATE [Forums_Forum] SET  
		[ForumThreads] = @Threads,
		[ForumPosts] = @Posts, 
		[ForumLastPostTime] = @LastPostTime,
		[ForumLastPostUserName] = @LastPostUserName,
		[ForumThreadsAbsolute] = @AbsoluteThreads,
		[ForumPostsAbsolute] = @AbsolutePosts, 
		[ForumLastPostTimeAbsolute] = @LastAbsolutePostTime,
		[ForumLastPostUserNameAbsolute] = @LastAbsolutePostUserName
	WHERE ForumID = @ID
END
