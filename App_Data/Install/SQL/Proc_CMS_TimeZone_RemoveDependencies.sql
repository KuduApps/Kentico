CREATE PROCEDURE [Proc_CMS_TimeZone_RemoveDependencies]
	@ID int
AS
BEGIN
	UPDATE CMS_UserSettings SET UserTimeZoneID = NULL WHERE UserTimeZoneID = @ID
END
