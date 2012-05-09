CREATE PROCEDURE [Proc_CMS_Class_SelectGeneration]
	@ClassLoadGeneration int
AS
BEGIN
	SELECT * FROM CMS_Class WHERE ClassLoadGeneration = @ClassLoadGeneration
END
