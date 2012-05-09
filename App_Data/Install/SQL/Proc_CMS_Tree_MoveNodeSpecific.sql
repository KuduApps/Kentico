CREATE PROCEDURE [Proc_CMS_Tree_MoveNodeSpecific]
	@NodeID int,
	@NewOrder int
AS
BEGIN
	/* Move the next node(s) up */
	UPDATE CMS_Tree SET NodeOrder = NodeOrder + 1 WHERE NodeOrder >= @NewOrder AND NodeParentID = (SELECT NodeParentID FROM CMS_Tree WHERE NodeID = @NodeID)
	
	/* Set current node order */
	UPDATE CMS_Tree SET NodeOrder = @NewOrder WHERE NodeID = @NodeID
END
