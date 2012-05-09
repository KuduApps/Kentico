CREATE PROCEDURE [Proc_OM_Contact_RemoveDependencies]
@ID int
AS
BEGIN
SET NOCOUNT ON;
	/* Remove all references */
	UPDATE OM_Contact SET [ContactGlobalContactID] = NULL WHERE [ContactGlobalContactID] = @ID;
	UPDATE OM_Contact SET [ContactMergedWithContactID] = NULL WHERE [ContactMergedWithContactID] = @ID;
	UPDATE OM_Account SET [AccountPrimaryContactID] = NULL WHERE [AccountPrimaryContactID] = @ID;
	UPDATE OM_Account SET [AccountSecondaryContactID] = NULL WHERE [AccountSecondaryContactID] = @ID;
	
	/* Remove all relations */
	DELETE FROM OM_AccountContact WHERE [ContactID] = @ID;
	DELETE FROM OM_ContactGroupMember WHERE ContactGroupMemberType=0 AND (ContactGroupMemberRelatedID = @ID)
	DELETE FROM [OM_Membership] WHERE [OriginalContactID] =@ID OR [ActiveContactID] = @ID;
	DELETE FROM [OM_IP] WHERE [IPActiveContactID] = @ID OR [IPOriginalContactID] = @ID;
	DELETE FROM [OM_UserAgent] WHERE [UserAgentActiveContactID] = @ID OR [UserAgentOriginalContactID] = @ID;
	DELETE FROM [OM_ScoreContactRule] WHERE [ContactID] = @ID;
	
	/* Delete relations from depending activity */
	DELETE FROM [OM_PageVisit] WHERE [PageVisitActivityID] IN (SELECT [ActivityID] FROM OM_Activity WHERE [ActivityActiveContactID] = @ID OR [ActivityOriginalContactID] = @ID);
	DELETE FROM [OM_Search] WHERE [SearchActivityID] IN (SELECT [ActivityID] FROM OM_Activity WHERE [ActivityActiveContactID] = @ID OR [ActivityOriginalContactID] = @ID);
	DELETE FROM [OM_Activity] WHERE [ActivityActiveContactID] = @ID OR [ActivityOriginalContactID] = @ID
END
