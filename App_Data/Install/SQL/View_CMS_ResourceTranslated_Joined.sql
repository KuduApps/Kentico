CREATE VIEW [View_CMS_ResourceTranslated_Joined]
AS
SELECT     CMS_ResourceString.StringID, CMS_ResourceString.StringKey, CMS_ResourceTranslation.TranslationText, CMS_UICulture.UICultureID, 
                      CMS_UICulture.UICultureName, CMS_UICulture.UICultureCode
FROM         CMS_ResourceString CROSS JOIN
                      CMS_UICulture LEFT OUTER JOIN
                      CMS_ResourceTranslation ON CMS_ResourceString.StringID = CMS_ResourceTranslation.TranslationStringID AND 
                      CMS_ResourceTranslation.TranslationUICultureID = CMS_UICulture.UICultureID
GO
