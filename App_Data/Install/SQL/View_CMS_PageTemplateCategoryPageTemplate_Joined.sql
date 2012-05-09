CREATE VIEW [View_CMS_PageTemplateCategoryPageTemplate_Joined]
AS
SELECT     CategoryID AS ObjectID, CategoryName AS CodeName, CategoryDisplayName AS DisplayName, CategoryParentID AS ParentID, CategoryGUID AS GUID, 
                      CategoryLastModified AS LastModified, CategoryImagePath, CategoryPath AS ObjectPath, CategoryOrder, CategoryLevel AS ObjectLevel, CategoryChildCount, 
                      CategoryTemplateChildCount, ISNULL(CategoryChildCount, 0) + ISNULL(CategoryTemplateChildCount, 0) AS CompleteChildCount, 
                      'pagetemplatecategory' AS ObjectType, NULL AS Parameter, 0 AS 'PageTemplateForAllPages', NULL AS 'PageTemplateType'
FROM         CMS_PageTemplateCategory
UNION ALL
(SELECT     PageTemplateID AS ObjectID, PageTemplateCodeName AS CodeName, PageTemplateDisplayName AS DisplayName, PageTemplateCategoryID AS CategoryID, 
                        PageTemplateGUID AS GUID, PageTemplateLastModified AS LastModified, NULL AS CategoryImagePath, 
                        CMS_PageTemplateCategory.CategoryPath + '/' + PageTemplateCodeName AS ObjectPath, NULL AS CategoryOrder, 
                        CMS_PageTemplateCategory.CategoryLevel + 1 AS ObjectLevel, 0 AS CategoryChildCount, 0 AS CategoryTemplateChildCount, 0 AS CompleteChildCount, 
                        'pagetemplate' AS ObjectType, PageTemplateIsReusable AS Parameter, PageTemplateForAllPages AS 'PageTemplateForAllPages', PageTemplateType
 FROM         CMS_PageTemplate LEFT JOIN
                        CMS_PageTemplateCategory ON CMS_PageTemplate.PageTemplateCategoryID = CMS_PageTemplateCategory.CategoryID)
GO
