-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_EventManager_RemoveDependencies]
	@NodeID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
BEGIN TRANSACTION
    DELETE FROM Events_Attendee WHERE AttendeeEventNodeID = @NodeID;
COMMIT TRANSACTION;
END
