CREATE PROCEDURE [Proc_Forums_ForumGroup_MoveNodeUp]
	@GroupID int,
	@GroupSiteID int,
	@GroupGroupID int
AS
BEGIN
	/* Move the next node(s) up */
	IF @GroupGroupID = 0	
		UPDATE Forums_ForumGroup SET GroupOrder = GroupOrder + 1 WHERE GroupOrder = (SELECT GroupOrder FROM Forums_ForumGroup WHERE GroupID = @GroupID) - 1 AND GroupSiteID = @GroupSiteID
	ELSE
		UPDATE Forums_ForumGroup SET GroupOrder = GroupOrder + 1 WHERE GroupOrder = (SELECT GroupOrder FROM Forums_ForumGroup WHERE GroupID = @GroupID) - 1 AND GroupGroupID = @GroupGroupID
	/* Move the current node down */			
	UPDATE Forums_ForumGroup SET GroupOrder = GroupOrder - 1 WHERE GroupID = @GroupID
	
END
