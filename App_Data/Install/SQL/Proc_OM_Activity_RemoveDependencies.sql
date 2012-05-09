CREATE PROCEDURE [Proc_OM_Activity_RemoveDependencies]
@ID int
AS
BEGIN
-- Delete all relations
  DELETE FROM OM_PageVisit WHERE PageVisitActivityID = @ID
  DELETE FROM OM_Search WHERE SearchActivityID = @ID
END
