CREATE PROCEDURE [Proc_CMS_ResourceTranslation_SelectByStringIDAndCultureID]
	@StringID int,
	@UICultureID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM CMS_ResourceTranslation WHERE TranslationStringID = @StringID AND TranslationUICultureID = @UICultureID
END
