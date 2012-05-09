CREATE PROCEDURE [Proc_CMS_WebFarm_DeleteTask]
	@ServerId int,
	@TaskId int
AS
BEGIN
DECLARE @ExistingTaskID int
IF (@ServerId <> 0)
BEGIN
	BEGIN TRANSACTION
		-- Removes task/server binding
		DELETE FROM [CMS_WebFarmServerTask] WHERE [TaskID]=@TaskId AND [ServerID]=@ServerId
	COMMIT TRANSACTION
		-- Returns id of existing taks
		SET @ExistingTaskID = (SELECT TOP 1 TaskID FROM CMS_WebFarmServerTask WITH (NOLOCK) WHERE [TaskID]=@TaskId)
		--  Removes all unasigned tasks
		IF @ExistingTaskID IS NULL
		DELETE FROM CMS_WebFarmTask WHERE [TaskID]=@TaskId
END
ELSE
BEGIN
	DELETE FROM CMS_WebFarmTask WHERE [TaskID]=@TaskId
END
END
