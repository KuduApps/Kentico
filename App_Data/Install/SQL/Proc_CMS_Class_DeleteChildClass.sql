-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_CMS_Class_DeleteChildClass]
	@ParentClassID int = 0,
	@ChildClassID int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    DELETE FROM CMS_AllowedChildClasses WHERE ParentClassID=@ParentClassID AND ChildClassID=@ChildClassID
END
