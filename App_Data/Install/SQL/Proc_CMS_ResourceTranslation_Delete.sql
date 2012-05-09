CREATE PROCEDURE [Proc_CMS_ResourceTranslation_Delete]
	@ID int	
AS
BEGIN
	SET NOCOUNT ON;   
	
	DELETE FROM CMS_ResourceTranslation WHERE TranslationID = @ID
END
