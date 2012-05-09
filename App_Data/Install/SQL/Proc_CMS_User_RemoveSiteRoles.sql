-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_CMS_User_RemoveSiteRoles]
	@UserID int,
	@SiteID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DELETE FROM [CMS_UserRole] WHERE
		UserID=@UserID AND
		RoleID IN (
			SELECT RoleID FROM [CMS_Role] INNER JOIN [CMS_Site] 
			ON [CMS_Role].[SiteID]=[CMS_Site].[SiteID]
			WHERE [CMS_Role].[SiteID]=@SiteID
		)
END
