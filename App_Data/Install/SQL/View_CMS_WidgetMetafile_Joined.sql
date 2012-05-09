CREATE VIEW [View_CMS_WidgetMetafile_Joined]
AS
SELECT     CMS_Widget.WidgetID, CMS_Widget.WidgetWebPartID, CMS_Widget.WidgetDisplayName, CMS_Widget.WidgetName, 
                      CMS_Widget.WidgetDescription, CMS_Widget.WidgetCategoryID, CMS_Widget.WidgetProperties, CMS_Widget.WidgetSecurity, 
                      CMS_Widget.WidgetForGroup,CMS_Widget.WidgetForInline, CMS_Widget.WidgetForEditor, CMS_Widget.WidgetForUser, CMS_Widget.WidgetForDashboard, CMS_Widget.WidgetGUID, 
                      CMS_Widget.WidgetLastModified, CMS_Widget.WidgetIsEnabled, WidgetsMetaFiles.MetaFileGUID
FROM         CMS_Widget LEFT OUTER JOIN
                          (SELECT     MetaFileGUID, MetaFileObjectID
                            FROM          CMS_MetaFile
                            WHERE      (MetaFileObjectType = 'cms.widget')) AS WidgetsMetaFiles ON CMS_Widget.WidgetID = WidgetsMetaFiles.MetaFileObjectID
GO
