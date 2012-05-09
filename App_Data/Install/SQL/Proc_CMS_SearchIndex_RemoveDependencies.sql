-- =============================================
-- Author:        <Author,,Name>
-- Create date: <Create Date,,>
-- Description:    <Description,,>
-- =============================================
CREATE PROCEDURE [Proc_CMS_SearchIndex_RemoveDependencies] 
    @ID int
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
        
    DELETE FROM CMS_SearchIndexCulture WHERE IndexID = @ID;    
    DELETE FROM CMS_SearchIndexSite WHERE IndexID = @ID;
    
    COMMIT TRANSACTION;
END
