CREATE PROCEDURE [Proc_CMS_Class_GetClassByName]
@ClassName nvarchar(100)
AS
BEGIN
	SELECT * FROM [CMS_Class] WHERE ClassName = @ClassName
END
