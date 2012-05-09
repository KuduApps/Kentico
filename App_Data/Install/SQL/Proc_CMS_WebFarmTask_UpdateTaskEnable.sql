CREATE PROCEDURE [Proc_CMS_WebFarmTask_UpdateTaskEnable] 
	@TaskID int,
	@TaskEnabled bit
AS
BEGIN
	UPDATE CMS_WebFarmTask SET TaskEnabled = @TaskEnabled WHERE TaskID = @TaskID;
END
