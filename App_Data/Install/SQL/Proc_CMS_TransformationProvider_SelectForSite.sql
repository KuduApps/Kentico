CREATE PROCEDURE [Proc_CMS_TransformationProvider_SelectForSite]
@TransformationName nvarchar(100),
@TransformationClassName nvarchar(100)
AS
BEGIN
	SELECT * FROM CMS_Transformation WHERE TransformationName = @TransformationName AND TransformationClassID = (SELECT [ClassID] FROM CMS_Class Where ClassName =  @TransformationClassName)
END
