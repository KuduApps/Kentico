-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_CMS_RelationshipName_RemoveDependencies]
	@ID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	BEGIN TRANSACTION;
    
	DELETE FROM [CMS_RelationshipNameSite] WHERE RelationshipNameID = @ID;
	DELETE FROM [CMS_Relationship] WHERE RelationshipNameID = @ID;
	DELETE FROM [CMS_ObjectRelationship] WHERE RelationshipNameID = @ID;
    DELETE FROM [CMS_Relationship] WHERE RelationshipNameID = @ID;
	COMMIT TRANSACTION;
END
