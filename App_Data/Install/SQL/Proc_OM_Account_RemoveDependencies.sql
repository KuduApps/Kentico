CREATE PROCEDURE [Proc_OM_Account_RemoveDependencies]
	@ID int
AS
BEGIN
	SET NOCOUNT ON;
	
	/* Remove all references */
	UPDATE OM_Account SET AccountGlobalAccountID = NULL WHERE AccountGlobalAccountID =  @ID;
	UPDATE OM_Account SET AccountSubsidiaryOfID = NULL WHERE AccountSubsidiaryOfID =  @ID;
	UPDATE OM_Account SET AccountMergedWithAccountID = NULL WHERE AccountMergedWithAccountID =  @ID;
	
	/* Remove all relations */
	DELETE FROM OM_AccountContact WHERE AccountID = @ID;
	DELETE FROM OM_ContactGroupMember WHERE ContactGroupMemberType=1 AND ContactGroupMemberRelatedID = @ID
END
