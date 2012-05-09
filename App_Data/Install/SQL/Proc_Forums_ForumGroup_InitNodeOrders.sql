CREATE PROCEDURE [Proc_Forums_ForumGroup_InitNodeOrders]
	@SiteID int,
	@GroupGroupID int
AS
BEGIN
	/* Declare the selection table */
	DECLARE @nodesTable TABLE (
		GroupID int,
		GroupOrder int,
		GroupName nvarchar(200)
	);
	
	/* Get the nodes list */
	IF @GroupGroupID = 0
		INSERT INTO @nodesTable SELECT GroupID, GroupOrder, GroupName FROM Forums_ForumGroup WHERE GroupSiteID = @SiteID AND GroupGroupID IS NULL;
	ELSE
		INSERT INTO @nodesTable SELECT GroupID, GroupOrder, GroupName FROM Forums_ForumGroup WHERE GroupSiteID = @SiteID AND GroupGroupID = @GroupGroupID;
	/* Declare the cursor to loop through the table */
	DECLARE @nodeCursor CURSOR;
    SET @nodeCursor = CURSOR FOR SELECT GroupID, GroupOrder FROM @nodesTable ORDER BY GroupOrder ASC, GroupName ASC;
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
			UPDATE Forums_ForumGroup SET GroupOrder = @currentIndex WHERE GroupID = @currentNodeId;
		/* Get next record */
		SET @currentIndex = @currentIndex + 1;
		FETCH NEXT FROM @nodeCursor INTO @currentNodeId, @currentNodeOrder;
	END
	CLOSE @nodeCursor;
	DEALLOCATE @nodeCursor;
END
