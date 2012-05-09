CREATE PROCEDURE [Proc_Forums_Forum_MoveNodeUp]
	@ForumID int,
	@ForumGroupID int
AS
BEGIN
	/* Move the next node(s) up */
	UPDATE Forums_Forum SET ForumOrder = ForumOrder + 1 WHERE ForumOrder = (SELECT ForumOrder FROM Forums_Forum WHERE ForumID = @ForumID) - 1 AND ForumGroupID = @ForumGroupID
	/* Move the current node down */
	UPDATE Forums_Forum SET ForumOrder = ForumOrder - 1 WHERE ForumID = @ForumID
END
