CREATE PROCEDURE [Proc_CMS_Class_GetQueryByName]
@QueryName nvarchar(100),
@ClassName nvarchar(100)
AS
BEGIN
SELECT CMS_Query.*, CMS_Class.ClassName 
    FROM CMS_Query
    LEFT JOIN CMS_Class ON CMS_Query.ClassID = CMS_Class.ClassID WHERE CMS_Class.ClassName = @ClassName AND  QueryName = @QueryName  
END
