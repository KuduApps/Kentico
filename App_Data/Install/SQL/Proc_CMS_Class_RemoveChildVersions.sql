CREATE PROCEDURE [Proc_CMS_Class_RemoveChildVersions]
@ID int
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    -- CMS_Query
    DELETE FROM [CMS_ObjectVersionHistory] WHERE [VersionObjectType] = N'cms.query' AND [VersionObjectID] IN ( SELECT [QueryID] FROM [CMS_Query] WHERE ClassID = @ID);
    -- CMS_Transformation
    DELETE FROM [CMS_ObjectVersionHistory] WHERE [VersionObjectType] = N'cms.transformation' AND [VersionObjectID] IN (SELECT [TransformationID] FROM [CMS_Transformation] WHERE [TransformationClassID] = @ID);    
    -- CMS Alternative forms
    DELETE FROM [CMS_ObjectVersionHistory] WHERE [VersionObjectType] = N'alternativeform.form' AND [VersionObjectID] IN (SELECT [FormID] FROM [CMS_AlternativeForm] WHERE [FormClassID] = @ID);
    
    COMMIT TRANSACTION;
END
