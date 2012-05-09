CREATE VIEW [View_Boards_BoardMessage_Joined]
AS
SELECT     Board_Board.BoardID, Board_Board.BoardName, Board_Board.BoardDisplayName, Board_Board.BoardDescription, 
                      Board_Board.BoardOpenedFrom, Board_Board.BoardOpened, Board_Board.BoardOpenedTo, Board_Board.BoardEnabled, 
                      Board_Board.BoardModerated, Board_Board.BoardAccess, Board_Board.BoardUseCaptcha, Board_Board.BoardLastModified, 
                      Board_Board.BoardMessages, Board_Board.BoardDocumentID, Board_Board.BoardGUID, Board_Board.BoardUserID, 
                      Board_Board.BoardGroupID, Board_Board.BoardLastMessageTime, Board_Board.BoardLastMessageUserName, 
                      Board_Board.BoardUnsubscriptionURL, Board_Board.BoardRequireEmails, Board_Board.BoardSiteID, 
                      Board_Board.BoardEnableSubscriptions, Board_Board.BoardBaseURL, Board_Message.MessageID, 
                      Board_Message.MessageUserName, Board_Message.MessageText, Board_Message.MessageEmail, 
                      Board_Message.MessageURL, Board_Message.MessageIsSpam, Board_Message.MessageBoardID, 
                      Board_Message.MessageApproved, Board_Message.MessageUserID, Board_Message.MessageApprovedByUserID, 
                      Board_Message.MessageUserInfo, Board_Message.MessageAvatarGUID, Board_Message.MessageInserted, 
                      Board_Message.MessageLastModified, Board_Message.MessageGUID, Board_Message.MessageRatingValue, 
                      Community_Group.GroupID, Community_Group.GroupGUID, Community_Group.GroupLastModified, Community_Group.GroupSiteID, 
                      Community_Group.GroupDisplayName, Community_Group.GroupName, Community_Group.GroupDescription, 
                      Community_Group.GroupNodeGUID, Community_Group.GroupApproveMembers, Community_Group.GroupAccess, 
                      Community_Group.GroupCreatedByUserID, Community_Group.GroupApprovedByUserID, Community_Group.GroupAvatarID, 
                      Community_Group.GroupApproved, Community_Group.GroupCreatedWhen, Community_Group.GroupSendJoinLeaveNotification, 
                      Community_Group.GroupSendWaitingForApprovalNotification, Community_Group.GroupSecurity
FROM         Board_Board INNER JOIN
                      Board_Message ON Board_Board.BoardID = Board_Message.MessageBoardID LEFT OUTER JOIN
                      Community_Group ON Board_Board.BoardGroupID = Community_Group.GroupID
GO
