CREATE VIEW [View_Poll_AnswerCount]
AS
SELECT     PollID, PollCodeName, PollDisplayName, PollTitle, PollOpenFrom, PollOpenTo, PollAllowMultipleAnswers, PollQuestion, PollAccess, PollResponseMessage, 
                      PollGUID, PollLastModified, PollGroupID, PollSiteID,
                          (SELECT     SUM(AnswerCount) AS AnswerCount
                            FROM          Polls_PollAnswer
                            WHERE      (AnswerPollID = Polls_Poll.PollID)) AS AnswerCount
FROM         Polls_Poll
GO
