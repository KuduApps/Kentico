CREATE PROCEDURE [Proc_Staging_Synchronization_DeleteOlderGlobalTasks]
	@TaskID int,
	@ServerName nvarchar(100),
	@ExcludeServerID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	BEGIN TRANSACTION;
	
	DELETE FROM Staging_Synchronization 
	WHERE SynchronizationTaskID = @TaskID 
	AND SynchronizationServerID IN (SELECT ServerID FROM Staging_Server WHERE ServerName = @ServerName)
	AND SynchronizationServerID <> @ExcludeServerID
	
	COMMIT TRANSACTION;
END
