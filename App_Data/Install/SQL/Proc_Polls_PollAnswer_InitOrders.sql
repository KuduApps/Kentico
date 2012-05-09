-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_Polls_PollAnswer_InitOrders]
	@ID int
AS
BEGIN
	/* Declare the selection table */
	DECLARE @pollanswerTable TABLE (
		AnswerID int NOT NULL,
		AnswerOrder int NULL
	);
	
	/* Get the steps list */
	INSERT INTO @pollanswerTable SELECT AnswerID, AnswerOrder FROM Polls_PollAnswer WHERE AnswerPollID=@ID
	
	/* Declare the cursor to loop through the table */
	DECLARE @cursor CURSOR;
    SET @cursor = CURSOR FOR SELECT AnswerID, AnswerOrder FROM @pollanswerTable ORDER BY AnswerOrder ASC, AnswerID ASC;
	/* Assign the numbers to the steps */
	DECLARE @currentIndex int, @currentOrder int;
	SET @currentIndex = 1;
	DECLARE @currentId int;
	
	/* Loop through the table */
	OPEN @cursor
	FETCH NEXT FROM @cursor INTO @currentId, @currentOrder;
	WHILE @@FETCH_STATUS = 0
	BEGIN
		/* Set the step index if different */
		IF @currentOrder IS NULL OR @currentOrder <> @currentIndex
			UPDATE Polls_PollAnswer SET AnswerOrder = @currentIndex WHERE AnswerID = @currentId;
		/* Get next record */
		SET @currentIndex = @currentIndex + 1;
		FETCH NEXT FROM @cursor INTO @currentId, @currentOrder;
	END
	CLOSE @cursor;
	DEALLOCATE @cursor;
END
