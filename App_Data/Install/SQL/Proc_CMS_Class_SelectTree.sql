CREATE PROCEDURE [Proc_CMS_Class_SelectTree]
	@ClassID int
AS
BEGIN
	SELECT * FROM [CMS_Tree] WHERE NodeClassID=@ClassID;
END
