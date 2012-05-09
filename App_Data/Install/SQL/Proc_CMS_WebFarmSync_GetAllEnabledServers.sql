CREATE PROCEDURE [Proc_CMS_WebFarmSync_GetAllEnabledServers]
AS
BEGIN
	SELECT * FROM CMS_WebFarmServer WHERE ServerEnabled = 1
END
