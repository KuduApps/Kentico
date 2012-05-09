-- =============================================
-- Author:        <Author,,Name>
-- Create date: <Create Date,,>
-- Description:    <Description,,>
-- =============================================
CREATE PROCEDURE [Proc_CMS_Culture_RemoveDependencies] 
    @ID int
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    -- Delete dependencies from CMS_UserCulture
    DELETE FROM CMS_UserCulture WHERE CultureID = @ID;
    -- CMS_SiteCulture
    DELETE FROM CMS_SiteCulture WHERE CultureID = @ID;
    -- BadWords_WordCulture
    DELETE FROM BadWords_WordCulture WHERE CultureID = @ID;   
    -- Search index
    DELETE FROM [CMS_SearchIndexCulture] WHERE IndexCultureID = @ID;
    
    -- CMS Page template scopes
    DELETE FROM [CMS_PageTemplateScope] WHERE PageTemplateScopeCultureID = @ID;
    
    COMMIT TRANSACTION;
END
