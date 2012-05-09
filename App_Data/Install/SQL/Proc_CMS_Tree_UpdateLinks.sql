CREATE PROCEDURE [Proc_CMS_Tree_UpdateLinks]
	@NodeID int,
	@NodeName nvarchar(100),
	@NodeSKUID int
AS
BEGIN
	SET NOCOUNT ON;
    UPDATE CMS_Tree SET 
		NodeName = @NodeName, 
		NodeSKUID = @NodeSKUID
	WHERE NodeID = @NodeID OR NodeLinkedNodeID = @NodeID
END
