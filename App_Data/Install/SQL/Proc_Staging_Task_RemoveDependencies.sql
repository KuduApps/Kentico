-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_Staging_Task_RemoveDependencies]
	@ID int
AS
BEGIN
    -- Staging_SyncLog
	DELETE FROM Staging_SyncLog WHERE SyncLogTaskID = @ID;
	-- Staging_Synchronization
	DELETE FROM Staging_Synchronization WITH(HOLDLOCK) WHERE SynchronizationTaskID = @ID;
END
