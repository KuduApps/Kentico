-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_CMS_SettingsKey_GetDataByKeyName_Global] 
	-- Add the parameters for the stored procedure here
	@KeyName nvarchar(100)
AS
BEGIN
    -- Insert statements for procedure here
	SELECT * FROM CMS_SettingsKey WHERE CMS_SettingsKey.KeyName = @KeyName AND 	CMS_SettingsKey.SiteID IS NULL
END
