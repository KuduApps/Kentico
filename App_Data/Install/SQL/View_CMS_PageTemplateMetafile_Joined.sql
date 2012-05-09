CREATE VIEW [View_CMS_PageTemplateMetafile_Joined]
AS
SELECT     CMS_PageTemplate.PageTemplateID, CMS_PageTemplate.PageTemplateDisplayName, CMS_PageTemplate.PageTemplateCodeName, 
                      CMS_PageTemplate.PageTemplateDescription, CMS_PageTemplate.PageTemplateFile, CMS_PageTemplate.PageTemplateIsPortal, 
                      CMS_PageTemplate.PageTemplateCategoryID, CMS_PageTemplate.PageTemplateLayoutID, CMS_PageTemplate.PageTemplateWebParts, 
                      CMS_PageTemplate.PageTemplateIsReusable, CMS_PageTemplate.PageTemplateShowAsMasterTemplate, 
                      CMS_PageTemplate.PageTemplateInheritPageLevels, CMS_PageTemplate.PageTemplateLayout, 
                      CMS_PageTemplate.PageTemplateLayoutCheckedOutFileName, CMS_PageTemplate.PageTemplateLayoutCheckedOutByUserID, 
                      CMS_PageTemplate.PageTemplateLayoutCheckedOutMachineName, CMS_PageTemplate.PageTemplateVersionGUID, 
                      CMS_PageTemplate.PageTemplateHeader, CMS_PageTemplate.PageTemplateGUID, CMS_PageTemplate.PageTemplateLastModified, 
                      CMS_PageTemplate.PageTemplateSiteID, CMS_PageTemplate.PageTemplateForAllPages, TemplatesMetaFiles.MetaFileGUID, 
                      CMS_PageTemplate.PageTemplateType, CMS_PageTemplate.PageTemplateLayoutType
FROM         CMS_PageTemplate LEFT OUTER JOIN
                          (SELECT     MetaFileGUID, MetaFileObjectID
                            FROM          CMS_MetaFile
                            WHERE      (MetaFileObjectType = 'cms.pagetemplate')) AS TemplatesMetaFiles ON 
                      CMS_PageTemplate.PageTemplateID = TemplatesMetaFiles.MetaFileObjectID
GO
