CREATE PROCEDURE [Proc_OM_ContactGroupMember_RemoveAccountContacts]
	@where nvarchar(max),
	@contactGroupID int
AS
BEGIN
	SET NOCOUNT ON;
	-- Variables
	DECLARE @tempTable TABLE (
		AccountID int NOT NULL
	);	
	DECLARE @accountID int;
	DECLARE @sqlQuery NVARCHAR(MAX)
	
	SET @sqlQuery = 'SELECT ContactGroupMemberRelatedID FROM OM_ContactGroupMember WHERE ' + @where
	INSERT INTO @tempTable EXEC(@sqlQuery)
	-- Loop through all accounts which are being deleted
	WHILE (SELECT Count(*) FROM @tempTable) > 0
	BEGIN
		SELECT TOP 1 @accountID = AccountID From @tempTable;
	
		-- Delete contacts from current group
		DELETE FROM OM_ContactGroupMember WHERE ContactGroupMemberType = 0 AND ContactGroupMemberContactGroupID = @contactGroupID 
		-- Which aren't added in any other way, only as an account member
		AND (ContactGroupMemberFromAccount = 1 AND (ContactGroupMemberFromCondition = 0 OR ContactGroupMemberFromCondition IS NULL) AND (ContactGroupMemberFromManual = 0 OR ContactGroupMemberFromManual IS NULL))
		-- Which are members of selected accounts
		AND ContactGroupMemberRelatedID IN (SELECT ParentAccount.ContactID FROM OM_AccountContact AS ParentAccount 
			WHERE ParentAccount.AccountID IN (SELECT TempTable.AccountID FROM @tempTable AS TempTable)
		-- These accounts must be member of current group
			AND ParentAccount.AccountID IN (SELECT ContactGroupMemberRelatedID FROM OM_ContactGroupMember WHERE ContactGroupMemberContactGroupID = @contactGroupID AND ContactGroupMemberType = 1)
		-- Don't remove contacts from other accounts
			AND ParentAccount.ContactID NOT IN (SELECT OtherAccounts.ContactID FROM OM_AccountContact AS OtherAccounts WHERE OtherAccounts.AccountID 
		-- Which are not part of selected accounts
			NOT IN (SELECT TempTable.AccountID FROM @tempTable AS TempTable) 
		-- And yet are members of current contact group
				AND OtherAccounts.AccountID IN (SELECT ContactGroupMemberRelatedID FROM OM_ContactGroupMember WHERE ContactGroupMemberContactGroupID = @contactGroupID AND ContactGroupMemberType = 1)))
				
		-- Update contacts which are added in other way such as manually or from dynamic condition
		UPDATE OM_ContactGroupMember SET ContactGroupMemberFromAccount = 0 WHERE ContactGroupMemberType = 0 AND ContactGroupMemberContactGroupID = @contactGroupID 
		-- Limit condition only to those added from account
		AND (ContactGroupMemberFromAccount = 1 AND (ContactGroupMemberFromCondition = 1 OR ContactGroupMemberFromManual = 1))
		-- Which are members of selected accounts
		AND ContactGroupMemberRelatedID IN (SELECT ParentAccount.ContactID FROM OM_AccountContact AS ParentAccount 
			WHERE ParentAccount.AccountID IN (SELECT TempTable.AccountID FROM @tempTable AS TempTable)
		-- These accounts must be member of current group
			AND ParentAccount.AccountID IN (SELECT ContactGroupMemberRelatedID FROM OM_ContactGroupMember WHERE ContactGroupMemberContactGroupID = @contactGroupID AND ContactGroupMemberType = 1)
		-- Don't change contacts related to other accounts
			AND ParentAccount.ContactID NOT IN (SELECT OtherAccounts.ContactID FROM OM_AccountContact AS OtherAccounts WHERE OtherAccounts.AccountID 
		-- Which are not part of selected accounts
			NOT IN (SELECT TempTable.AccountID FROM @tempTable AS TempTable) 
		-- And yet are members of current contact group
				AND OtherAccounts.AccountID IN (SELECT ContactGroupMemberRelatedID FROM OM_ContactGroupMember WHERE ContactGroupMemberContactGroupID = @contactGroupID AND ContactGroupMemberType = 1)))		
				
		-- Remove record from temporary table
		DELETE @tempTable WHERE AccountID = @accountID
	END
END
