-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_Polls_PollAnswer_MoveDown] 
	@PollID int,
    @AnswerID int
AS
BEGIN
	DECLARE @MaxAnswerOrder int
	SET @MaxAnswerOrder = (SELECT TOP 1 AnswerOrder FROM Polls_PollAnswer WHERE AnswerPollID = @PollID ORDER BY AnswerOrder DESC);
	/* Move the next step(s) up */
	UPDATE Polls_PollAnswer SET AnswerOrder = AnswerOrder - 1 WHERE AnswerOrder = (SELECT AnswerOrder FROM Polls_PollAnswer WHERE AnswerPollID=@PollID AND AnswerID = @AnswerID) + 1 
	/* Move the current step down */
	UPDATE Polls_PollAnswer SET AnswerOrder = AnswerOrder + 1 WHERE AnswerPollID = @PollID AND AnswerID = @AnswerID AND AnswerOrder < @MaxAnswerOrder
END
