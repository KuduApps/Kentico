-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_Media_File_UpdateFilePath]
	@OriginalPath nvarchar(450),
	@NewPath nvarchar(450),
	@LibraryID int,
    @OriginalPathLength int
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRANSACTION
		UPDATE [Media_File] 
		SET [FilePath] = @NewPath + Substring(FilePath, @OriginalPathLength + 1, Len(FilePath) ) 
		FROM [Media_File] 
		WHERE ([FilePath] LIKE (@OriginalPath + '/%')) AND (FileLibraryID = @LibraryID);
		
	COMMIT TRANSACTION
END
