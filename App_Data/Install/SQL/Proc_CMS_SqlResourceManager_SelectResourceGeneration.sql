CREATE PROCEDURE [Proc_CMS_SqlResourceManager_SelectResourceGeneration]	
	@StringLoadGeneration int
AS
BEGIN
	SELECT CMS_ResourceTranslation.TranslationText, CMS_ResourceString.StringKey, CMS_UICulture.UICultureCode
	FROM CMS_ResourceTranslation 
		INNER JOIN CMS_ResourceString ON CMS_ResourceTranslation.TranslationStringID = CMS_ResourceString.StringID 
		INNER JOIN CMS_UICulture ON CMS_ResourceTranslation.TranslationUICultureID = CMS_UICulture.UICultureID
	WHERE CMS_ResourceString.StringLoadGeneration = @StringLoadGeneration
END
