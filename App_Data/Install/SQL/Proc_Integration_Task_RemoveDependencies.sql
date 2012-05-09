CREATE PROCEDURE [Proc_Integration_Task_RemoveDependencies]
	@ID int
AS
BEGIN
	-- Integration_SyncLog
	DELETE FROM [Integration_SyncLog] WHERE SyncLogSynchronizationID IN (SELECT SynchronizationID FROM [Integration_Synchronization] WHERE SynchronizationTaskID = @ID);
    -- Integration_Synchronization
	DELETE FROM [Integration_Synchronization] WITH(HOLDLOCK) WHERE SynchronizationTaskID = @ID;
END
