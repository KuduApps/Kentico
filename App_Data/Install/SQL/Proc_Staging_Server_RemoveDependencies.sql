CREATE PROCEDURE [Proc_Staging_Server_RemoveDependencies]
@ID int
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
  -- interfering with SELECT statements.
  SET NOCOUNT ON;
  BEGIN TRANSACTION;
  -- Staging_SyncLog
    DELETE FROM Staging_SyncLog WHERE SyncLogServerID = @ID;
  -- Staging_Synchronization
  DELETE FROM Staging_Synchronization WHERE SynchronizationServerID = @ID;
  -- Staging_Task - delete tasks which don't have any synchronization binding
  DELETE FROM Staging_Task WHERE TaskID NOT IN (SELECT DISTINCT SynchronizationTaskID FROM Staging_Synchronization);
  COMMIT TRANSACTION;
END
