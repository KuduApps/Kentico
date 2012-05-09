CREATE PROCEDURE [Proc_CMS_WebFarmSync_SetServerTasks]
	@ServerId int,
	@TaskId int
AS
BEGIN
	INSERT INTO [CMS_WebFarmServerTask] ( [ServerID], [TaskID] ) VALUES (  @ServerId, @TaskId)
END
