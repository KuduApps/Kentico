-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_Polls_PollAnswer_MoveUp] 
	@PollID int,
    @AnswerID int
AS
BEGIN
    /* Move the previous step(s) down */
	UPDATE Polls_PollAnswer SET AnswerOrder = AnswerOrder + 1 WHERE AnswerPollID = @PollID AND AnswerOrder = (SELECT AnswerOrder FROM Polls_PollAnswer WHERE AnswerPollID = @PollID AND AnswerID = @AnswerID) - 1 
	/* Move the current step up */
	UPDATE Polls_PollAnswer SET AnswerOrder = AnswerOrder - 1 WHERE AnswerPollID = @PollID AND AnswerID = @AnswerID AND AnswerOrder > 1
END
