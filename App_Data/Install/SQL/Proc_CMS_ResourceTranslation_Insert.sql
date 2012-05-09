CREATE PROCEDURE [Proc_CMS_ResourceTranslation_Insert]
	@TranslationID int,	
	@TranslationStringID int,
	@TranslationUICultureID int,
	@TranslationText ntext
AS
BEGIN
	SET NOCOUNT ON;   
	INSERT INTO [CMS_ResourceTranslation] (
		[TranslationStringID],
		[TranslationUICultureID],
		[TranslationText]
	)
	VALUES (
		@TranslationStringID,
		@TranslationUICultureID,
		@TranslationText
	)
	SELECT SCOPE_IDENTITY()
END
