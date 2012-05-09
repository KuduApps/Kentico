CREATE PROCEDURE [Proc_OM_Rule_RemoveDependencies]
		@ID int
AS
BEGIN
	SET NOCOUNT ON;
	DELETE FROM OM_ScoreContactRule WHERE RuleID = @ID;
END
