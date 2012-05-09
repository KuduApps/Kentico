CREATE PROCEDURE [Proc_CMS_Tree_MoveNodeLast]
	@NodeID int 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	-- Set node as last one (high node order)
	UPDATE CMS_Tree SET NodeOrder = 1000000 WHERE NodeID = @NodeID
	-- Init the orders
	DECLARE @NodeParentID int;
	SELECT @NodeParentID = NodeParentID FROM CMS_Tree WHERE NodeID = @NodeID;
	EXECUTE Proc_CMS_Tree_InitNodeOrders @NodeParentID;
	-- Get new node order
	SELECT NodeOrder FROM CMS_Tree WHERE NodeID = @NodeID
END
