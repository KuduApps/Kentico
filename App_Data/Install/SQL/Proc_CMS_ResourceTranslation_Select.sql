CREATE PROCEDURE [Proc_CMS_ResourceTranslation_Select]
	@ID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM CMS_ResourceTranslation WHERE TranslationID = @ID
END
