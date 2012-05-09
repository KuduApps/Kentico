CREATE PROCEDURE [Proc_OM_AccountContact_AccountsIntoContact]
	@contactID int,
	@where nvarchar(max),
	@roleID int
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @insertAccounts TABLE (
		AccountID int NOT NULL
	);	
	DECLARE @updateAccounts TABLE (
		AccountID int NOT NULL
	);		
	DECLARE @currentAccountID int;	
	DECLARE @sqlQuery NVARCHAR(MAX);
	
	-- Fill in table of records to be created
	SET @sqlQuery = 'SELECT AccountID FROM OM_Account WHERE ' + @where + ' AND AccountID NOT IN (SELECT AccountID FROM OM_AccountContact WHERE ContactID = '+CAST(@contactID as nvarchar(50)) +')';
	INSERT INTO @insertAccounts EXEC(@sqlQuery);
	
	-- Fill in table of records to be updated
	SET @sqlQuery = 'SELECT AccountID FROM OM_Account WHERE ' + @where + ' AND AccountID IN (SELECT AccountID FROM OM_AccountContact WHERE ContactID = '+CAST(@contactID as nvarchar(50)) +')';
	INSERT INTO @updateAccounts EXEC(@sqlQuery);
	
	-- Loop through records and insert them
	WHILE ((SELECT Count(*) FROM @insertAccounts) > 0)
	BEGIN		
		SELECT TOP 1 @currentAccountID = AccountID FROM @insertAccounts;
		IF @roleID > 0
			INSERT INTO OM_AccountContact (ContactRoleID, AccountID, ContactID) VALUES (@roleID,@currentAccountID,@contactID);
		ELSE
			INSERT INTO OM_AccountContact (AccountID, ContactID) VALUES (@currentAccountID,@contactID)
			
		-- Update contact groups
		EXEC Proc_OM_ContactGroupMember_AddContactIntoAccount @currentAccountID, @contactID;			
		DELETE FROM @insertAccounts WHERE AccountID = @currentAccountID;
	END
	
	-- Update roles for other part of records
	IF @roleID > 0
		UPDATE OM_AccountContact SET ContactRoleID = @roleID WHERE ContactID = @contactID AND AccountID IN (SELECT AccountID FROM @updateAccounts);
	ELSE
		UPDATE OM_AccountContact SET ContactRoleID = NULL WHERE ContactID = @contactID AND AccountID IN (SELECT AccountID FROM @updateAccounts);
END
