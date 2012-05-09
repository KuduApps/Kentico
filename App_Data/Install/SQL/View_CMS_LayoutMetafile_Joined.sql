CREATE VIEW [View_CMS_LayoutMetafile_Joined]
AS
SELECT     CMS_Layout.LayoutID, CMS_Layout.LayoutCodeName, CMS_Layout.LayoutDisplayName, CMS_Layout.LayoutDescription, 
                      LayoutFiles.MetaFileGUID
FROM         CMS_Layout LEFT OUTER JOIN
                          (SELECT     MetaFileGUID, MetaFileObjectID
                            FROM          CMS_MetaFile
                            WHERE      (MetaFileObjectType = 'cms.layout')) AS LayoutFiles ON CMS_Layout.LayoutID = LayoutFiles.MetaFileObjectID
GO
