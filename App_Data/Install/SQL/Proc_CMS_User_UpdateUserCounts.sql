CREATE PROCEDURE [Proc_CMS_User_UpdateUserCounts]
	@UserID int,
	@Points int,
	@Type int
AS
BEGIN
	IF (@UserID != 0) BEGIN
		-- Forum post
		IF (@Type = 0) 
			UPDATE CMS_UserSettings SET UserForumPosts =(SELECT COUNT(*) FROM Forums_ForumPost WHERE PostUserID = @UserID AND PostApproved = 1) WHERE UserSettingsUserID = @UserID
		-- Message board post
		IF (@Type = 1)  
			UPDATE CMS_UserSettings SET UserMessageBoardPosts =(SELECT COUNT(*) FROM Board_Message WHERE MessageUserID = @UserID AND MessageApproved = 1) WHERE UserSettingsUserID = @UserID
		-- Blog comment post
		IF (@Type = 2)  
			UPDATE CMS_UserSettings SET UserBlogComments =(SELECT COUNT(*) FROM Blog_Comment WHERE CommentUserID = @UserID AND CommentApproved = 1) WHERE UserSettingsUserID = @UserID
		-- Blog post
		IF (@Type = 3)  
			UPDATE CMS_UserSettings SET UserBlogPosts = (SELECT COUNT(*) FROM View_CMS_Tree_Joined WHERE NodeOwner = @UserID AND ClassName = 'CMS.BlogPost' AND Published = 1) WHERE UserSettingsUserID = @UserID
	END
	
	IF (@UserID = 0) BEGIN
		-- Forum post
		IF (@Type = 0) 
			UPDATE CMS_UserSettings SET UserForumPosts = (SELECT COUNT(*) FROM Forums_ForumPost WHERE PostApproved = 1 AND [Forums_ForumPost].PostUserID = [CMS_UserSettings].UserSettingsUserID) 
		-- Message board post
		IF (@Type = 1)  
			UPDATE CMS_UserSettings SET UserMessageBoardPosts =(SELECT COUNT(*) FROM Board_Message WHERE MessageApproved = 1 AND [Board_Message].MessageUserID = [CMS_UserSettings].UserSettingsUserID) 
		-- Blog comment post
		IF (@Type = 2)  
			UPDATE CMS_UserSettings SET UserBlogComments =(SELECT COUNT(*) FROM Blog_Comment WHERE CommentApproved = 1 AND [Blog_Comment].CommentUserID = [CMS_UserSettings].UserSettingsUserID)
		-- Blog post
		IF (@Type = 3)  
			UPDATE CMS_UserSettings SET UserBlogPosts = (SELECT COUNT(*) FROM View_CMS_Tree_Joined WHERE ClassName = 'CMS.BlogPost' AND Published = 1 AND [View_CMS_Tree_Joined].NodeOwner = [CMS_UserSettings].UserSettingsUserID)
	END
	-- Update all counts for all users
	IF ((@Type = 4) AND (@UserID = 0))
	BEGIN
		UPDATE CMS_UserSettings SET UserForumPosts = (SELECT COUNT(*) FROM Forums_ForumPost WHERE PostApproved = 1 AND [Forums_ForumPost].PostUserID = [CMS_UserSettings].UserSettingsUserID) 
		UPDATE CMS_UserSettings SET UserMessageBoardPosts =(SELECT COUNT(*) FROM Board_Message WHERE MessageApproved = 1 AND [Board_Message].MessageUserID = [CMS_UserSettings].UserSettingsUserID) 
		UPDATE CMS_UserSettings SET UserBlogComments =(SELECT COUNT(*) FROM Blog_Comment WHERE CommentApproved = 1 AND [Blog_Comment].CommentUserID = [CMS_UserSettings].UserSettingsUserID)
		UPDATE CMS_UserSettings SET UserBlogPosts = (SELECT COUNT(*) FROM View_CMS_Tree_Joined WHERE ClassName = 'CMS.BlogPost' AND Published = 1 AND [View_CMS_Tree_Joined].NodeOwner = [CMS_UserSettings].UserSettingsUserID)
	END
	ELSE IF ((@Type = 4) AND (@UserID != 0))
	BEGIN
		UPDATE CMS_UserSettings SET UserForumPosts =(SELECT COUNT(*) FROM Forums_ForumPost WHERE PostUserID = @UserID AND PostApproved = 1) WHERE UserSettingsUserID = @UserID
		UPDATE CMS_UserSettings SET UserMessageBoardPosts =(SELECT COUNT(*) FROM Board_Message WHERE MessageUserID = @UserID AND MessageApproved = 1) WHERE UserSettingsUserID = @UserID
		UPDATE CMS_UserSettings SET UserBlogComments =(SELECT COUNT(*) FROM Blog_Comment WHERE CommentUserID = @UserID AND CommentApproved = 1) WHERE UserSettingsUserID = @UserID
		UPDATE CMS_UserSettings SET UserBlogPosts = (SELECT COUNT(*) FROM View_CMS_Tree_Joined WHERE NodeOwner = @UserID AND ClassName = 'CMS.BlogPost' AND Published = 1) WHERE UserSettingsUserID = @UserID
	END
	-- Update activity points
	IF (@Type < 4)
	BEGIN
		UPDATE CMS_UserSettings SET UserActivityPoints = ISNULL(UserActivityPoints, 0) + @Points WHERE UserSettingsUserID = @UserID;
		-- Activity points cannot be negative
		IF ((SELECT TOP 1 UserActivityPoints FROM CMS_UserSettings WHERE UserSettingsUserID = @UserID)  < 0)
			UPDATE CMS_UserSettings SET UserActivityPoints =0 WHERE UserSettingsUserID = @UserID;	
	END
END
