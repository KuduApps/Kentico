-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_Polls_Poll_RemoveDependencies]
	@ID int
AS
BEGIN
  DELETE FROM Polls_PollRoles WHERE PollID=@ID;
  DELETE FROM Polls_PollSite WHERE PollID=@ID;
  DELETE FROM Polls_PollAnswer WHERE AnswerPollID=@ID;
END
