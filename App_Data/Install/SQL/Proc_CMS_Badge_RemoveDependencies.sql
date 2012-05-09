CREATE PROCEDURE [Proc_CMS_Badge_RemoveDependencies]
-- Add the parameters for the stored procedure here
	@ID int
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
  -- interfering with SELECT statements.
  SET NOCOUNT ON;
  UPDATE [CMS_UserSettings] SET UserBadgeID=NULL WHERE UserBadgeID=@ID;
END
