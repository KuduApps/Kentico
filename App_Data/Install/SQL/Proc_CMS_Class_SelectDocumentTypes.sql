CREATE PROCEDURE [Proc_CMS_Class_SelectDocumentTypes] 
AS
BEGIN
	SELECT * FROM [CMS_Class] WHERE [ClassIsDocumentType]=1 ORDER BY [ClassDisplayName]
END
