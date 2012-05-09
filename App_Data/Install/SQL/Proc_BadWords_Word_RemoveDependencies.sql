CREATE PROCEDURE [Proc_BadWords_Word_RemoveDependencies]
@ID int
AS
BEGIN
DELETE FROM [BadWords_WordCulture] WHERE WordID = @ID;
END
