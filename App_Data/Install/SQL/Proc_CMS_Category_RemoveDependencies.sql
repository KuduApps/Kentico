-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_CMS_Category_RemoveDependencies]
	@ID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	BEGIN TRANSACTION;
	
	DECLARE @IDPath nvarchar(1500);
	SET @IDPath = (SELECT TOP 1 CategoryIDPath FROM CMS_Category WHERE CategoryID = @ID);
	
	-- Remove CMS_DocumentCategory dependencies
    DELETE FROM CMS_DocumentCategory 
        WHERE (CategoryID = @ID) OR
            CategoryID IN (
            SELECT CategoryID FROM CMS_Category 
            WHERE (CategoryIDPath LIKE N'' + @IDPath + '/%')
        )
	
	-- Remove references within CMS_Category table
	UPDATE CMS_Category SET CategoryParentID = NULL WHERE ((CategoryIDPath LIKE N'' + @IDPath + '/%') OR (CategoryID = @ID))
	
	-- Remove categories
	DELETE FROM CMS_Category WHERE ((CategoryIDPath LIKE N'' + @IDPath + '/%') OR (CategoryID = @ID))
    
	COMMIT TRANSACTION;
END
