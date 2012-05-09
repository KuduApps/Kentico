CREATE VIEW [View_CMS_WebPartMetafile_Joined]
AS
SELECT     CMS_WebPart.WebPartID, CMS_WebPart.WebPartName, CMS_WebPart.WebPartDisplayName, CMS_WebPart.WebPartDescription, 
                      CMS_WebPart.WebPartFileName, CMS_WebPart.WebPartProperties, CMS_WebPart.WebPartCategoryID, 
                      CMS_WebPart.WebPartParentID, CMS_WebPart.WebPartDocumentation, CMS_WebPart.WebPartGUID, 
                      CMS_WebPart.WebPartLastModified, CMS_WebPart.WebPartType, CMS_WebPart.WebPartLoadGeneration, 
                      CMS_WebPart.WebPartLastSelection, WebpartsMetaFiles.MetaFileGUID
FROM         CMS_WebPart LEFT JOIN (
SELECT CMS_MetaFile.MetaFileGUID, MetaFileObjectID FROM CMS_MetaFile 
	WHERE     (CMS_MetaFile.MetaFileObjectType = 'cms.webpart')
) AS WebpartsMetaFiles
ON CMS_WebPart.WebPartID = WebpartsMetaFiles.MetaFileObjectID
GO
