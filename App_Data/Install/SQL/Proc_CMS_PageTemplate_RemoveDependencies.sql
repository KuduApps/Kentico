-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_CMS_PageTemplate_RemoveDependencies]
	@ID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	BEGIN TRANSACTION;
	-- CMS_Document
    UPDATE [CMS_Document] SET DocumentPageTemplateID = NULL WHERE DocumentPageTemplateID = @ID; 
	-- CMS_Class
    UPDATE [CMS_Class] SET ClassDefaultPageTemplateID = NULL WHERE ClassDefaultPageTemplateID = @ID; 
	-- CMS_PageTemplateSite
	DELETE FROM CMS_PageTemplateSite WHERE PageTemplateID = @ID;
	
	-- CMS Page template scopes
    DELETE FROM [CMS_PageTemplateScope] WHERE PageTemplateScopeTemplateID = @ID;
    
    -- Online Marketing
    DELETE FROM OM_MVTCombinationVariation WHERE MVTCombinationID IN ( SELECT MVTCombinationID FROM OM_MVTCombination WHERE MVTCombinationPageTemplateID = @ID);
    DELETE FROM OM_MVTCombinationVariation WHERE MVTVariantID IN ( SELECT MVTVariantID FROM OM_MVTVariant WHERE MVTVariantPageTemplateID = @ID);
	DELETE FROM OM_MVTCombination WHERE MVTCombinationPageTemplateID = @ID;
	DELETE FROM OM_MVTVariant WHERE MVTVariantPageTemplateID = @ID;    
	
	COMMIT TRANSACTION;
END
