CREATE PROCEDURE [Proc_CMS_Tree_MoveNodeAlphabetical]
	@NodeID int
AS
BEGIN
	/* Declare the selection table */
	DECLARE @nodesTable TABLE (
		NodeID int,
		NodeOrder int,
		NodeName nvarchar(100),
		NodeAlias nvarchar(50)
	);
	
	/* Get parent node ID */
	DECLARE @NodeParentID int, @NodeName nvarchar(100);
	SELECT @NodeParentID = NodeParentID, @NodeName = NodeName FROM CMS_Tree WHERE NodeID = @NodeID;
	/* Get the nodes list */
	INSERT INTO @nodesTable SELECT NodeID, NodeOrder, NodeName, NodeAlias FROM CMS_Tree WHERE NodeParentID = @NodeParentID AND NodeID <> @NodeID;
	
	/* Declare the cursor to loop through the table */
	DECLARE @nodeCursor CURSOR;
    SET @nodeCursor = CURSOR FOR SELECT NodeID, NodeOrder, NodeName FROM @nodesTable ORDER BY NodeOrder ASC, NodeName ASC, NodeAlias ASC;
	/* Assign the numbers to the nodes */
	DECLARE @currentIndex int, @currentNodeOrder int, @currentNodeName nvarchar(100);
	SET @currentIndex = 1;
	DECLARE @currentNodeId int;
	DECLARE @insertIndex int;
	DECLARE @nodePlaced bit;
	SET @nodePlaced = 0;
	SET @insertIndex = -1;
	
	/* Loop through the table */
	OPEN @nodeCursor
	FETCH NEXT FROM @nodeCursor INTO @currentNodeId, @currentNodeOrder, @currentNodeName;
	WHILE @@FETCH_STATUS = 0
	BEGIN
		/* If current node should be next, place it */
		IF @nodePlaced = 0 AND @currentNodeName > @NodeName
		BEGIN
			
			UPDATE CMS_Tree SET NodeOrder = @currentIndex WHERE NodeID = @NodeID;
  			SET @insertIndex = @currentIndex;
			SET @nodePlaced = 1;
			SET @currentIndex = @currentIndex + 1;
		END
		/* Set the Node index if different */
		IF @currentNodeOrder IS NULL OR @currentNodeOrder <> @currentIndex
		BEGIN	
			UPDATE CMS_Tree SET NodeOrder = @currentIndex WHERE NodeID = @currentNodeId;
		END
		/* Get next record */
		SET @currentIndex = @currentIndex + 1;
		FETCH NEXT FROM @nodeCursor INTO @currentNodeId, @currentNodeOrder, @currentNodeName;
	END
	
	/* If node wasn't placed, add this node to the end*/
	IF @nodePlaced = 0
	BEGIN
		UPDATE CMS_Tree SET NodeOrder = @currentIndex WHERE NodeID = @NodeID;
	END	
	CLOSE @nodeCursor;
	DEALLOCATE @nodeCursor;
	
	IF  (@insertIndex = -1)
	BEGIN
		SELECT @currentIndex AS NodeOrder;
	END
	ELSE BEGIN
		SELECT @insertIndex AS NodeOrder;
	END
END
