CREATE PROCEDURE [Proc_Board_Message_UpdateCounts]
	@boardID int
AS
BEGIN
	/* Get number of messages */
    DECLARE @Messages int;
	SET @Messages = (SELECT COUNT(*) FROM [Board_Message] WHERE ([MessageBoardID] =  @boardID)AND([MessageApproved] = 1));
	/* Get last message */
	DECLARE @lastMessage TABLE (
		MessageInserted datetime,
		MessageUserName nvarchar(250)
	);
	INSERT INTO @lastMessage SELECT TOP 1 [MessageInserted], [MessageUserName] FROM [Board_Message] WHERE ([MessageBoardID] =  @boardID) AND ([MessageApproved] = 1) ORDER BY [MessageInserted] DESC;
	DECLARE @LastMessageTime datetime;
	DECLARE @LastMessageUserName nvarchar(250);
	
	SET @LastMessageTime = (SELECT TOP 1 MessageInserted FROM @lastMessage);
	SET @LastMessageUserName = (SELECT TOP 1 MessageUserName FROM @lastMessage);
	
	/* Update the board*/
	UPDATE [Board_Board] SET [BoardMessages] = @Messages, [BoardLastMessageTime] = @LastMessageTime, [BoardLastMessageUserName] = @LastMessageUserName WHERE ([BoardID] = @boardID)
END
