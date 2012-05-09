CREATE PROCEDURE [Proc_OM_ContactGroupMember_RemoveContactsFromAccount]
	@where nvarchar(max)
AS
BEGIN
SET NOCOUNT ON;
	-- Variables
	DECLARE @tempTable TABLE (
		ContactID int NOT NULL,
		AccountID int NOT NULL
	);	
	DECLARE @contactID int;
	DECLARE	@accountID int;
	DECLARE @sqlQuery NVARCHAR(MAX);
	
	SET @sqlQuery = 'SELECT ContactID, AccountID FROM OM_AccountContact WHERE ' + @where
	INSERT INTO @tempTable EXEC(@sqlQuery)
	-- Loop through all contacts which are being deleted
	WHILE (SELECT Count(*) FROM @tempTable) > 0
	BEGIN
		SELECT TOP 1 @contactID = ContactID, @accountID = AccountID From @tempTable;
		
		-- Delete dynamically added contact from all groups
		DELETE FROM OM_ContactGroupMember WHERE ContactGroupMemberType = 0 AND ContactGroupMemberRelatedID = @contactID
		-- Contacts must be added only as member of account to be deleted
		AND (ContactGroupMemberFromAccount = 1 AND (ContactGroupMemberFromCondition = 0 OR ContactGroupMemberFromCondition IS NULL) AND (ContactGroupMemberFromManual = 0 OR ContactGroupMemberFromManual IS NULL))
		-- And is not member of different account 
			 AND @contactID NOT IN (SELECT ContactID FROM OM_AccountContact	WHERE AccountID <> @accountID AND 
		-- Which would be member of current group			
			 AccountID IN (SELECT OtherAccounts.ContactGroupMemberRelatedID FROM OM_ContactGroupMember AS OtherAccounts
				WHERE OtherAccounts.ContactGroupMemberType = 1 AND OtherAccounts.ContactGroupMemberContactGroupID = ContactGroupMemberContactGroupID ))
			
		-- Update contacts which are added 
		UPDATE OM_ContactGroupMember SET ContactGroupMemberFromAccount = 0 
		WHERE ContactGroupMemberType = 0 AND ContactGroupMemberRelatedID = @contactID
		-- Contacts must not be added only as member of account to be deleted
		AND (ContactGroupMemberFromAccount = 1 AND (ContactGroupMemberFromCondition = 1 OR ContactGroupMemberFromManual = 1))
		-- And is not member of different account 
			 AND @contactID NOT IN (SELECT ContactID FROM OM_AccountContact	WHERE AccountID <> @accountID AND 
		-- Which would be member of current group			
			 AccountID IN (SELECT OtherAccounts.ContactGroupMemberRelatedID FROM OM_ContactGroupMember AS OtherAccounts
				WHERE OtherAccounts.ContactGroupMemberType = 1 AND OtherAccounts.ContactGroupMemberContactGroupID = ContactGroupMemberContactGroupID ))
			
		-- Remove record from temporary table
		DELETE @tempTable WHERE ContactID = @contactID
	END
END
