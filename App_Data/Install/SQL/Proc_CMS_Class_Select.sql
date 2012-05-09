CREATE PROCEDURE [Proc_CMS_Class_Select] 
	@ClassID int
AS
BEGIN
	SELECT * FROM [CMS_Class] WHERE [ClassID]=@ClassID
END
