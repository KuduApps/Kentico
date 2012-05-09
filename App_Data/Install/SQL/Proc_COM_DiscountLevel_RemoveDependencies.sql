-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_COM_DiscountLevel_RemoveDependencies]
	@ID int
AS
BEGIN
    UPDATE CMS_UserSite SET UserDiscountLevelID = NULL WHERE UserDiscountLevelID = @ID;
	DELETE FROM COM_DiscountLevelDepartment WHERE DiscountLevelID = @ID;
	UPDATE COM_Customer SET CustomerDiscountLevelID = NULL WHERE CustomerDiscountLevelID = @ID;
END
