CREATE PROCEDURE [Proc_Forums_Forum_InitNodeOrders]
	@GroupForumID int
AS
BEGIN
	/* Declare the selection table */
	DECLARE @nodesTable TABLE (
		ForumID int,
		ForumOrder int,
		ForumName nvarchar(200)
	);
	
	/* Get the nodes list */
	INSERT INTO @nodesTable SELECT ForumID, ForumOrder, ForumName FROM Forums_Forum WHERE ForumGroupID = @GroupForumID;
	
	/* Declare the cursor to loop through the table */
	DECLARE @nodeCursor CURSOR;
    SET @nodeCursor = CURSOR FOR SELECT ForumID, ForumOrder FROM @nodesTable ORDER BY ForumOrder ASC, ForumName ASC;
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
			UPDATE Forums_Forum SET ForumOrder = @currentIndex WHERE ForumID = @currentNodeId;
		/* Get next record */
		SET @currentIndex = @currentIndex + 1;
		FETCH NEXT FROM @nodeCursor INTO @currentNodeId, @currentNodeOrder;
	END
	CLOSE @nodeCursor;
	DEALLOCATE @nodeCursor;
END
