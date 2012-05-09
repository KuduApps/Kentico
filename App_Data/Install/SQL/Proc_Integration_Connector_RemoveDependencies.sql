CREATE PROCEDURE [Proc_Integration_Connector_RemoveDependencies]
@ID int
AS
BEGIN
  -- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
  SET NOCOUNT ON;
  BEGIN TRANSACTION;
  -- Integration_SyncLog
  DELETE FROM [Integration_SyncLog] WHERE SyncLogSynchronizationID IN (SELECT SynchronizationID FROM [Integration_Synchronization] WHERE SynchronizationConnectorID = @ID);
  -- Integration_Synchronization
  DELETE FROM [Integration_Synchronization] WHERE SynchronizationConnectorID = @ID;
  -- Integration_Task - delete tasks which don't have any synchronization binding
  DELETE FROM [Integration_Task] WHERE (TaskID NOT IN (SELECT DISTINCT SynchronizationTaskID FROM Integration_Synchronization));
  COMMIT TRANSACTION;
END
