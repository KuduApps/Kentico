CREATE PROCEDURE [Proc_Integration_Synchronization_RemoveDependencies]
@ID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	SET NOCOUNT ON;
	DELETE FROM [Integration_SyncLog] WHERE SyncLogSynchronizationID = @ID
END
