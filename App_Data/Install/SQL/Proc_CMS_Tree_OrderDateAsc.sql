CREATE PROCEDURE [Proc_CMS_Tree_OrderDateAsc]
	@NodeParentID int
AS
BEGIN
	/* Declare the selection table */
	DECLARE @nodesTable TABLE (
		NodeID int,
		NodeOrder int,
		DocumentModifiedWhen datetime
	);
	
	/* Get the nodes list */
	INSERT INTO @nodesTable SELECT NodeID, NodeOrder, DocumentModifiedWhen FROM View_CMS_Tree_Joined WHERE NodeParentID = @NodeParentID;
	
	/* Declare the cursor to loop through the table */
	DECLARE @nodeCursor CURSOR;
    SET @nodeCursor = CURSOR FOR SELECT NodeID, NodeOrder FROM @nodesTable ORDER BY DocumentModifiedWhen ASC, NodeOrder ASC;
	/* Assign the numbers to the nodes */
	DECLARE @currentIndex int, @currentNodeOrder int;
	SET @currentIndex = 1;
	DECLARE @currentNodeId int;
	
	/* Loop through the table */
	OPEN @nodeCursor
	FETCH NEXT FROM @nodeCursor INTO @currentNodeId, @currentNodeOrder;
	WHILE @@FETCH_STATUS = 0
	BEGIN
		/* Set the Node index if different */
		IF @currentNodeOrder IS NULL OR @currentNodeOrder <> @currentIndex
			UPDATE CMS_Tree SET NodeOrder = @currentIndex WHERE NodeID = @currentNodeId;
		/* Get next record */
		SET @currentIndex = @currentIndex + 1;
		FETCH NEXT FROM @nodeCursor INTO @currentNodeId, @currentNodeOrder;
	END
	CLOSE @nodeCursor;
	DEALLOCATE @nodeCursor;
	RETURN @currentIndex;
END
