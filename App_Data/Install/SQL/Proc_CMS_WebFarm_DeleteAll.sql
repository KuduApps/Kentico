CREATE PROCEDURE [Proc_CMS_WebFarm_DeleteAll] 
AS
BEGIN
	-- Remove all task - server binding
	DELETE FROM [CMS_WebFarmServerTask];
	-- Remove all unasigned tasks
	DELETE FROM CMS_WebFarmTask WHERE TaskEnabled = 1 AND TaskID NOT IN (SELECT TaskID FROM CMS_WebFarmServerTask);
END
