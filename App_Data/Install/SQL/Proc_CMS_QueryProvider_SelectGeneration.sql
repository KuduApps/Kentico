CREATE PROCEDURE [Proc_CMS_QueryProvider_SelectGeneration]
	@QueryLoadGeneration int
AS
BEGIN
	SELECT CMS_Query.*, CMS_Class.ClassName 
    FROM CMS_Query
    LEFT JOIN CMS_Class ON CMS_Class.ClassID = CMS_Query.ClassID
    WHERE QueryLoadGeneration = @QueryLoadGeneration
END
