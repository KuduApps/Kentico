CREATE PROCEDURE [Proc_Board_Board_DeleteDocumentBoards] 
	@ID int
AS
BEGIN
	-- Remove board dependecies			
	DELETE FROM Board_Message WHERE MessageBoardID IN (SELECT BoardID FROM Board_Board WHERE BoardDocumentID = @ID);
	DELETE FROM Board_Moderator WHERE BoardID IN (SELECT BoardID FROM Board_Board WHERE BoardDocumentID = @ID);
	DELETE FROM Board_Role WHERE BoardID IN (SELECT BoardID FROM Board_Board WHERE BoardDocumentID = @ID);
	DELETE FROM Board_Subscription WHERE SubscriptionBoardID IN (SELECT BoardID FROM Board_Board WHERE BoardDocumentID = @ID);
				
	-- Delete the board itself
	DELETE FROM Board_Board WHERE BoardID IN (SELECT BoardID FROM Board_Board WHERE BoardDocumentID = @ID);		
END
