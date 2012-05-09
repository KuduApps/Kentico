CREATE PROCEDURE [Proc_Forums_ForumPost_UpdateThreadCounts]
	@Path nvarchar(450)
AS
BEGIN
	-- Get number of posts
    DECLARE @Posts int;
	DECLARE @AbsolutePosts int;
	DECLARE @IsThreadApproved bit;
	
	SET @IsThreadApproved = (SELECT CASE WHEN (SELECT TOP 1 PostId FROM [Forums_ForumPost] WHERE ([PostIDPath] = @Path) AND (PostApproved = 1)) IS NOT NULL THEN 1 ELSE 0 END);
	SET @Posts = (SELECT COUNT(*) FROM [Forums_ForumPost] WHERE ([PostIDPath] LIKE @Path+'%') AND (PostApproved = 1) AND (@IsThreadApproved = 1));
	SET @AbsolutePosts = (SELECT COUNT(*) FROM [Forums_ForumPost] WHERE ([PostIDPath] LIKE @Path+'%'));
	-- Get last post
	DECLARE @LastPostTime datetime;
	DECLARE @LastPostUserName nvarchar(200);
	
	SELECT TOP 1 @LastPostTime = PostTime, @LastPostUserName = PostUserName
	FROM [Forums_ForumPost]
	WHERE ([PostIDPath] LIKE @Path+'%')
		AND (@IsThreadApproved = 1)
		AND (PostApproved = 1)
	ORDER BY PostTime DESC
	
	-- Get last absolute post
	DECLARE @LastAbsolutePostTime datetime;
	DECLARE @LastAbsolutePostUserName nvarchar(200);
	
	SELECT TOP 1 @LastAbsolutePostTime = PostTime, @LastAbsolutePostUserName = PostUserName
	FROM [Forums_ForumPost] WHERE ([PostIDPath] LIKE @Path+'%')
	ORDER BY PostTime DESC
	
	
	-- Update forum
	UPDATE [Forums_ForumPost] SET  
		[PostThreadPosts] = @Posts,
		[PostThreadLastPostTime] = @LastPostTime,
		[PostThreadLastPostUserName] = @LastPostUserName,
		[PostThreadPostsAbsolute] = @AbsolutePosts,
		[PostThreadLastPostTimeAbsolute] = @LastAbsolutePostTime,
		[PostThreadLastPostUserNameAbsolute] = @LastAbsolutePostUserName
	WHERE ([PostIDPath] = @Path AND PostParentID IS NULL)
END
