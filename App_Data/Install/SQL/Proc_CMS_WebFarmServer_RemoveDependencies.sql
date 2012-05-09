CREATE PROCEDURE [Proc_CMS_WebFarmServer_RemoveDependencies]
	@ID int
AS
BEGIN
    DELETE FROM CMS_WebFarmServerTask WHERE ServerID = @ID;
	DELETE FROM CMS_WebFarmTask WHERE TaskEnabled = 1 AND TaskID NOT IN (SELECT TaskID FROM CMS_WebFarmServerTask);
END
