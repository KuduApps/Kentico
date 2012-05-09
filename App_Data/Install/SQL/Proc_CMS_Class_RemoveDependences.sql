-- =============================================
-- Author:        <Author,,Name>
-- Create date: <Create Date,,>
-- Description:    <Description,,>
-- =============================================
CREATE PROCEDURE [Proc_CMS_Class_RemoveDependences]
    @ClassID int
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    -- CMS_Query
    DELETE FROM [CMS_Query] WHERE ClassID=@ClassID;
    -- CMS_Transformation
    DELETE FROM [CMS_Transformation] WHERE TransformationClassID=@ClassID;
    -- CMS_ClassSite
    DELETE FROM [CMS_ClassSite] WHERE ClassID=@ClassID;
    -- CMS_RolePermission and CMS_Permission
    DELETE FROM [CMS_RolePermission] WHERE PermissionID IN
    (
        SELECT PermissionID FROM CMS_Permission WHERE ClassID=@ClassID
    );
    DELETE FROM [CMS_Permission] WHERE ClassID=@ClassID;
    -- CMS_AllowedChildClassses
    DELETE FROM [CMS_AllowedChildClasses] WHERE ParentClassID=@ClassID OR ChildClassID=@ClassID;
    -- CMS_WorkflowScope
    DELETE FROM [CMS_WorkflowScope] WHERE ScopeClassID = @ClassID;
    -- CMS_Form
    DELETE FROM [CMS_Form] WHERE FormClassID=@ClassID;
    -- CMS Alternative forms
    DELETE FROM [CMS_AlternativeForm] WHERE FormClassID = @ClassID;
    
    -- CMS Page template scopes
    DELETE FROM [CMS_PageTemplateScope] WHERE PageTemplateScopeClassID = @ClassID;
    
    COMMIT TRANSACTION;
END
