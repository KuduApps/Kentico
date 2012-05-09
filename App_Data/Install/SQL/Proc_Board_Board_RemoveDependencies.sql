-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_Board_Board_RemoveDependencies] 
	@ID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	BEGIN TRANSACTION;
	-- Board dependencies
    DELETE FROM Board_Message WHERE MessageBoardID = @ID;
	DELETE FROM Board_Moderator WHERE BoardID = @ID;
	DELETE FROM Board_Role WHERE BoardID = @ID;
	DELETE FROM Board_Subscription WHERE SubscriptionBoardID = @ID;
	COMMIT TRANSACTION;
END
