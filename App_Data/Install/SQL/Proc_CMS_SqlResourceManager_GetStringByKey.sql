CREATE PROCEDURE [Proc_CMS_SqlResourceManager_GetStringByKey]
	@stringKey nvarchar(200),
	@cultureCode nvarchar(50)
AS
BEGIN
SELECT [TranslationText] FROM CMS_ResourceTranslation 
WHERE TranslationUICultureID = 
(SELECT [UICultureID] FROM [CMS_UICulture] WHERE UICultureCode = @cultureCode)  
AND
TranslationStringID = (SELECT TOP 1 [StringID] FROM [CMS_ResourceString] WHERE StringKey = @stringKey)
END
