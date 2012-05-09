CREATE PROCEDURE [Proc_CMS_QueryProvider_SelectAll]
AS
BEGIN
	SELECT CMS_Query.*, CMS_Class.ClassName 
    FROM CMS_Query
    LEFT JOIN CMS_Class ON CMS_Class.ClassID = CMS_Query.ClassID
    ORDER BY CMS_Class.ClassName, CMS_Query.QueryName
END
