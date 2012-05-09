CREATE PROCEDURE [Proc_Forums_ForumGroup_RemoveDependencies] 
    @ID int
AS
BEGIN
UPDATE [Forums_ForumPost] SET PostParentID=NULL WHERE PostForumID IN (
        SELECT ForumID FROM [Forums_Forum] WHERE ForumGroupID = @ID
    );
DELETE FROM [Forums_ForumSubscription] WHERE SubscriptionPostID IN (
        SELECT PostId FROM Forums_ForumPost WHERE (PostForumID  IN (
        SELECT ForumID FROM [Forums_Forum] WHERE ForumGroupID = @ID
    )));
DELETE FROM [Forums_Attachment] WHERE AttachmentPostID IN (
        SELECT PostId FROM Forums_ForumPost WHERE (PostForumID  IN (
        SELECT ForumID FROM [Forums_Forum] WHERE ForumGroupID = @ID
    )));
DELETE FROM [Forums_UserFavorites] WHERE PostID IN (
        SELECT PostId FROM Forums_ForumPost WHERE (PostForumID  IN (
        SELECT ForumID FROM [Forums_Forum] WHERE ForumGroupID = @ID
    )));
DELETE FROM [Forums_UserFavorites] WHERE ForumID IN (
    SELECT ForumID FROM [Forums_Forum] WHERE ForumGroupID = @ID
    );
DELETE FROM [Forums_ForumPost] WHERE PostForumID IN (
        SELECT ForumID FROM [Forums_Forum] WHERE ForumGroupID = @ID
    );
DELETE FROM [Forums_ForumRoles ] WHERE ForumID IN (
        SELECT ForumID FROM [Forums_Forum] WHERE ForumGroupID = @ID
    );
DELETE FROM [Forums_ForumModerators] WHERE ForumID IN (
        SELECT ForumID FROM [Forums_Forum] WHERE ForumGroupID = @ID
    );
DELETE FROM [Forums_ForumSubscription] WHERE SubscriptionForumID IN (
        SELECT ForumID FROM [Forums_Forum] WHERE ForumGroupID = @ID
    );
DELETE FROM [Forums_Forum] WHERE ForumGroupID = @ID;
END
