CREATE PROCEDURE [Proc_CMS_Avatar_RemoveDependencies]
@ID int
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
  -- interfering with SELECT statements.
  SET NOCOUNT ON;
  UPDATE [CMS_UserSettings] SET UserAvatarID=NULL WHERE UserAvatarID=@ID;
  UPDATE [Community_Group] SET GroupAvatarID=NULL WHERE GroupAvatarID=@ID;
END
