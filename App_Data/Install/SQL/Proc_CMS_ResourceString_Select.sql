CREATE PROCEDURE [Proc_CMS_ResourceString_Select]
	@ID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM CMS_ResourceString WHERE StringID = @ID
END
