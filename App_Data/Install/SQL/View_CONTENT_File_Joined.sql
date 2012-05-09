CREATE VIEW [View_CONTENT_File_Joined] AS SELECT View_CMS_Tree_Joined.*, CONTENT_File.* FROM View_CMS_Tree_Joined INNER JOIN CONTENT_File ON View_CMS_Tree_Joined.DocumentForeignKeyValue = CONTENT_File.[FileID] WHERE (ClassName = 'CMS.File')
GO
