CREATE PROCEDURE [Proc_Community_Group_RemoveDependencies]
@ID int
AS
BEGIN
SET NOCOUNT ON;
    -- Get AvatarID from current group
    DECLARE @AvatarID int;
    SELECT @AvatarID = GroupAvatarID FROM [Community_Group] WHERE GroupID=@ID
    IF (@AvatarID > 0)
    BEGIN
      DECLARE @AvatarIsCustom bit;
      SELECT @AvatarIsCustom = AvatarIsCustom FROM [CMS_Avatar]
      IF (@AvatarIsCustom=1)
      BEGIN
      UPDATE [Community_Group] SET GroupAvatarID=NULL WHERE GroupAvatarID=@AvatarID;
        DELETE FROM [CMS_Avatar] WHERE AvatarID=@AvatarID
      END
    END
  -- Members
  DELETE FROM [Community_GroupMember] WHERE MemberGroupID = @ID
  -- Group role permission
  DELETE FROM [Community_GroupRolePermission] WHERE GroupID = @ID
  -- Forums
  UPDATE [Forums_ForumPost] SET PostParentID=NULL WHERE PostForumID IN (
      SELECT ForumID FROM [Forums_Forum] WHERE ForumGroupID IN (
      SELECT GroupID FROM [Forums_ForumGroup] WHERE GroupGroupID = @ID
    ));
  DELETE FROM [Forums_ForumSubscription] WHERE SubscriptionPostID IN (
      SELECT PostID FROM Forums_ForumPost WHERE (PostForumID  IN (
      SELECT ForumID FROM [Forums_Forum] WHERE ForumGroupID IN (
      SELECT GroupID FROM [Forums_ForumGroup] WHERE GroupGroupID = @ID
    ))));
  DELETE FROM [Forums_UserFavorites] WHERE PostID IN (
      SELECT PostID FROM [Forums_ForumPost] WHERE ( PostForumID IN (  
      SELECT ForumID FROM [Forums_Forum] WHERE ForumGroupID IN (
      SELECT GroupID FROM [Forums_ForumGroup] WHERE GroupGroupID = @ID
    ))));
  DELETE FROM [Forums_UserFavorites] WHERE ForumID IN (
      SELECT ForumID FROM [Forums_Forum] WHERE ForumGroupID IN (
      SELECT GroupID FROM [Forums_ForumGroup] WHERE GroupGroupID = @ID
    ));
  DELETE FROM [Forums_Attachment] WHERE AttachmentPostID IN (
      SELECT PostID FROM [Forums_ForumPost] WHERE ( PostForumID IN (  
      SELECT ForumID FROM [Forums_Forum] WHERE ForumGroupID IN (
      SELECT GroupID FROM [Forums_ForumGroup] WHERE GroupGroupID = @ID
    ))));  
  DELETE FROM [Forums_ForumPost] WHERE PostForumID IN (
      SELECT ForumID FROM [Forums_Forum] WHERE ForumGroupID IN (
      SELECT GroupID FROM [Forums_ForumGroup] WHERE GroupGroupID = @ID
    ));
  DELETE FROM [Forums_ForumRoles ] WHERE ForumID IN (
      SELECT ForumID FROM [Forums_Forum] WHERE ForumGroupID IN (
      SELECT GroupID FROM [Forums_ForumGroup] WHERE GroupGroupID = @ID
    ));
  DELETE FROM [Forums_ForumModerators] WHERE ForumID IN (
      SELECT ForumID FROM [Forums_Forum] WHERE ForumGroupID IN (
      SELECT GroupID FROM [Forums_ForumGroup] WHERE GroupGroupID = @ID
    ));
  DELETE FROM [Forums_ForumSubscription] WHERE SubscriptionForumID IN (
      SELECT ForumID FROM [Forums_Forum] WHERE ForumGroupID IN (
      SELECT GroupID FROM [Forums_ForumGroup] WHERE GroupGroupID = @ID
    ));
  DELETE FROM [Forums_Forum] WHERE ForumGroupID IN (
      SELECT GroupID FROM [Forums_ForumGroup] WHERE GroupGroupID = @ID
    );
  DELETE FROM Forums_ForumGroup WHERE GroupGroupID = @ID;
  -- Polls
  DELETE FROM [Polls_PollRoles] WHERE PollID IN (
      SELECT PollID FROM [Polls_Poll] WHERE PollGroupID = @ID
    );
  DELETE FROM [Polls_PollSite] WHERE PollID IN (
      SELECT PollID FROM [Polls_Poll] WHERE PollGroupID = @ID
    );
  DELETE FROM [Polls_PollAnswer] WHERE AnswerPollID IN (
      SELECT PollID FROM [Polls_Poll] WHERE PollGroupID = @ID
    );
  DELETE FROM [Polls_Poll] WHERE PollGroupID = @ID;
  -- Roles
  DELETE FROM [CMS_UserRole] WHERE RoleID IN (
      SELECT RoleID FROM [CMS_Role] WHERE RoleGroupID = @ID
    );
  DELETE FROM [CMS_RolePermission] WHERE RoleID IN (
      SELECT RoleID FROM [CMS_Role] WHERE RoleGroupID = @ID
    );
  DELETE FROM [CMS_ACLItem] WHERE RoleID IN (
      SELECT RoleID FROM [CMS_Role] WHERE RoleGroupID = @ID
    );
  DELETE FROM [CMS_WorkflowStepRoles] WHERE RoleID IN (
      SELECT RoleID FROM [CMS_Role] WHERE RoleGroupID = @ID
    );
    DELETE FROM [Forums_ForumRoles] WHERE RoleID IN (
      SELECT RoleID FROM [CMS_Role] WHERE RoleGroupID = @ID
    );
  DELETE FROM [Polls_PollRoles] WHERE RoleID IN (
      SELECT RoleID FROM [CMS_Role] WHERE RoleGroupID = @ID
    );
  DELETE FROM [Board_Role] WHERE RoleID IN (
      SELECT RoleID FROM [CMS_Role] WHERE RoleGroupID = @ID
    );
    
  DELETE FROM [CMS_RoleUIElement] WHERE RoleID IN (
    SELECT RoleID FROM [CMS_Role] WHERE RoleGroupID = @ID
  );
  DELETE FROM [CMS_Role] WHERE RoleGroupID = @ID;
  -- Group message boards
    DELETE FROM Board_Message WHERE MessageBoardID IN ( SELECT BoardID FROM Board_Board WHERE BoardGroupID = @ID );
  DELETE FROM Board_Moderator WHERE BoardID IN ( SELECT BoardID FROM Board_Board WHERE BoardGroupID = @ID );
  DELETE FROM Board_Role WHERE BoardID IN ( SELECT BoardID FROM Board_Board WHERE BoardGroupID = @ID );
  DELETE FROM Board_Subscription WHERE SubscriptionBoardID IN (SELECT BoardID FROM Board_Board WHERE BoardGroupID = @ID);
  DELETE FROM Board_Board WHERE BoardGroupID = @ID;
  -- Group invitation
  DELETE FROM Community_Invitation WHERE InvitationGroupID = @ID;
  -- Remove refrences to the tree node
  UPDATE CMS_Tree SET NodeGroupID = NULL WHERE NodeGroupID = @ID;
  
  -- Project Management
  DELETE FROM [PM_ProjectRolePermission] WHERE ProjectID IN (SELECT ProjectID FROM [PM_Project] WHERE ProjectGroupID = @ID);
  DELETE FROM [PM_ProjectTask] WHERE ProjectTaskProjectID IN (SELECT ProjectID FROM [PM_Project] WHERE ProjectGroupID = @ID);
  DELETE FROM [PM_Project] WHERE ProjectGroupID = @ID;
END
