-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_Media_Library_RemoveDependencies]
	-- Add the parameters for the stored procedure here
	@ID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	BEGIN TRANSACTION;
    -- Insert statements for procedure here
	DELETE FROM Media_File WHERE FileLibraryID = @ID;
	DELETE FROM Media_LibraryRolePermission WHERE LibraryID = @ID;
	
	COMMIT TRANSACTION;
END
