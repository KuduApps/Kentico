CREATE PROCEDURE [Proc_Forums_Forum_RemoveDependencies] 
    @ID int
AS
BEGIN
UPDATE Forums_ForumPost SET PostParentID=NULL WHERE (PostForumID =@ID);
DELETE FROM [Forums_ForumSubscription] WHERE SubscriptionPostID IN (SELECT PostId FROM Forums_ForumPost WHERE (PostForumID =@ID));
DELETE FROM [Forums_Attachment] WHERE AttachmentPostID IN (SELECT PostId FROM Forums_ForumPost WHERE (PostForumID =@ID));
DELETE FROM [Forums_UserFavorites] WHERE PostId IN (SELECT PostId FROM Forums_ForumPost WHERE (PostForumID =@ID));
DELETE FROM [Forums_UserFavorites] WHERE ForumID = @ID; 
DELETE FROM [Forums_ForumPost] WHERE (PostForumID =@ID);
DELETE FROM [Forums_ForumRoles ] WHERE ForumID = @ID;
DELETE FROM [Forums_ForumModerators ] WHERE ForumID = @ID;
DELETE FROM [Forums_ForumSubscription] WHERE SubscriptionForumID = @ID;
END
