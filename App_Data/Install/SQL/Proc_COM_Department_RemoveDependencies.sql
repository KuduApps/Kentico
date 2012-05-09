-- =============================================
-- Author:        <Author,,Name>
-- Create date: <Create Date,,>
-- Description:    <Description,,>
-- =============================================
CREATE PROCEDURE [Proc_COM_Department_RemoveDependencies]
    @ID int
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    -- COM_UserDepartment
    DELETE FROM COM_UserDepartment WHERE DepartmentID=@ID;
    DELETE FROM COM_DiscountLevelDepartment WHERE DepartmentID=@ID;
    DELETE FROM COM_DepartmentTaxClass WHERE DepartmentID=@ID;
    UPDATE CMS_Class SET ClassSKUDefaultDepartmentID = NULL WHERE ClassSKUDefaultDepartmentID = @ID;
    COMMIT TRANSACTION;
END
