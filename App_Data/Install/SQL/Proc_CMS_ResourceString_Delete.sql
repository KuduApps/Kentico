CREATE PROCEDURE [Proc_CMS_ResourceString_Delete]
	@ID int	
AS
BEGIN
	SET NOCOUNT ON;   
	
	DELETE FROM CMS_ResourceTranslation WHERE TranslationStringID = @ID
	DELETE FROM CMS_ResourceString WHERE StringID = @ID
END
