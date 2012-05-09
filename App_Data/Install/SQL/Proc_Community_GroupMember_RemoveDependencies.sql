CREATE PROCEDURE [Proc_Community_GroupMember_RemoveDependencies] 
	@GroupID int,
	@UserID int
AS
BEGIN
	-- Remove from forum group moderators
	DELETE FROM Forums_ForumModerators WHERE UserID=@UserID AND ForumID IN 
	(SELECT ForumID FROM Forums_Forum WHERE ForumGroupID IN 
    (SELECT GroupID FROM Forums_ForumGroup WHERE GroupGroupID = @GroupID))
END
