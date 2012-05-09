CREATE PROCEDURE [Proc_CMS_ResourceTranslation_Update]
	@TranslationID int,	
	@TranslationStringID int,
	@TranslationUICultureID int,
	@TranslationText ntext
AS
BEGIN
	SET NOCOUNT ON;   
	UPDATE [CMS_ResourceTranslation]
	SET
		TranslationStringID = @TranslationStringID,
		TranslationUICultureID = @TranslationUICultureID,
		TranslationText = @TranslationText
	WHERE
		TranslationID = @TranslationID
END
