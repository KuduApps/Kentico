CREATE PROCEDURE [Proc_CMS_Class_SelectByTableName] 
	@TableName nvarchar(100)
AS
BEGIN
	SELECT * FROM [CMS_Class] WHERE [ClassTableName]=@TableName
END
