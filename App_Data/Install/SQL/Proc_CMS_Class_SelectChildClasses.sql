CREATE PROCEDURE [Proc_CMS_Class_SelectChildClasses] 
	@ParentClassID int
AS
BEGIN
	SELECT * FROM CMS_Class 
	WHERE ClassID IN (SELECT ChildClassID FROM CMS_AllowedChildClasses WHERE ParentClassID=@ParentClassID)
END
