-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_CMS_Class_InsertChildClass]
	@ParentClassID int = NULL,
	@ChildClassID int = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    INSERT INTO CMS_AllowedChildClasses (
		ParentClassID,
		ChildClassID
	)
	VALUES (
		@ParentClassID,
		@ChildClassID
	)
END
