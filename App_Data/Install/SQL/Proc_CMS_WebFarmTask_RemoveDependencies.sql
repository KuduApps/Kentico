-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_CMS_WebFarmTask_RemoveDependencies]
	@ID int
AS
BEGIN
	
	-- CMS_WebFarmServerTask
    DELETE FROM CMS_WebFarmServerTask WHERE TaskID = @ID;
	
	DELETE FROM CMS_WebFarmTask WHERE TaskEnabled = 1 AND (TaskIsAnonymous IS NULL OR TaskIsAnonymous = 0) AND TaskID NOT IN (SELECT TaskID FROM CMS_WebFarmServerTask);
END
