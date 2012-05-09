CREATE PROCEDURE [Proc_Forums_ForumPost_MoveUpStickyThread]
@PostForumID int,
@PostID int
AS
BEGIN
	DECLARE @order int;
	DECLARE @maxOrder int;
	SET @order = (SELECT PostStickOrder FROM Forums_ForumPost WHERE PostId = @PostID)
	SET @maxOrder = (SELECT TOP 1 PostStickOrder FROM Forums_ForumPost WHERE PostForumID = @PostForumID ORDER BY PostStickOrder DESC)
	
	IF (@order <> @maxOrder) BEGIN
	/* Declare the cursor to loop through the table */
	DECLARE @postCursor CURSOR;
    SET @postCursor = CURSOR FOR SELECT PostId, PostStickOrder FROM Forums_ForumPost WHERE PostForumID = @PostForumID AND PostStickOrder > 0 ORDER BY PostStickOrder;
	/* Assign the numbers to the nodes */
	DECLARE @currentPostId int, @currentStickOrder int, @added int;
	SET @added = 0;
	
	/* Loop through the table */
	OPEN @postCursor
	FETCH NEXT FROM @postCursor INTO @currentPostId, @currentStickOrder;
	WHILE @@FETCH_STATUS = 0
	BEGIN
		IF ((@added = 0)AND(@currentStickOrder = (@order + 1))) BEGIN
			UPDATE Forums_ForumPost SET PostStickOrder = @order WHERE PostId = @currentPostId 
			UPDATE Forums_ForumPost SET PostStickOrder = @currentStickOrder WHERE PostId = @PostID 
			SET @added = 1; 
		END
		FETCH NEXT FROM @postCursor INTO @currentPostId, @currentStickOrder;
	END
	CLOSE @postCursor;
	DEALLOCATE @postCursor;
	END	
END
