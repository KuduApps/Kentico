CREATE PROCEDURE [Proc_CMS_Class_SelectChildClassesForSite] 
	@ParentClassID int,
	@SiteID int
AS
BEGIN
	SELECT * FROM CMS_Class 
	WHERE ClassID IN (SELECT ChildClassID FROM CMS_AllowedChildClasses WHERE ParentClassID=@ParentClassID)
	AND ClassID IN (SELECT ClassID FROM CMS_ClassSite WHERE SiteID = @SiteID)
END
