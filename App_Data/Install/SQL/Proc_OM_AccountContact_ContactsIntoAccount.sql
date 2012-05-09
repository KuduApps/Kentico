CREATE PROCEDURE [Proc_OM_AccountContact_ContactsIntoAccount]
	@accountID int,
	@where nvarchar(max),
	@roleID int
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @insertContacts TABLE (
		ContactID int NOT NULL
	);	
	DECLARE @updateContacts TABLE (
		ContactID int NOT NULL
	);		
	DECLARE @currentContactID int;	
	DECLARE @sqlQuery NVARCHAR(MAX);
	
	-- Fill in table of records to be created
	SET @sqlQuery = 'SELECT ContactID FROM OM_Contact WHERE ' + @where + ' AND ContactID NOT IN (SELECT ContactID FROM OM_AccountContact WHERE AccountID = '+CAST(@accountID as nvarchar(50)) +')';
	INSERT INTO @insertContacts EXEC(@sqlQuery);
	
	-- Fill in table of records to be updated
	SET @sqlQuery = 'SELECT ContactID FROM OM_Contact WHERE ' + @where + ' AND ContactID IN (SELECT ContactID FROM OM_AccountContact WHERE AccountID = '+CAST(@accountID as nvarchar(50)) +')';
	INSERT INTO @updateContacts EXEC(@sqlQuery);
	
	-- Loop through records and insert them
	WHILE ((SELECT Count(*) FROM @insertContacts) > 0)
	BEGIN		
		SELECT TOP 1 @currentContactID = ContactID FROM @insertContacts;
		IF @roleID > 0
			INSERT INTO OM_AccountContact (ContactRoleID, AccountID, ContactID) VALUES (@roleID,@accountID,@currentContactID);
		ELSE
			INSERT INTO OM_AccountContact (AccountID, ContactID) VALUES (@accountID,@currentContactID)
			
		-- Update contact groups
		EXEC Proc_OM_ContactGroupMember_AddContactIntoAccount @accountID, @currentContactID;			
		DELETE FROM @insertContacts WHERE ContactID = @currentContactID;
	END
	-- Update roles for other part of records
	IF @roleID > 0
		UPDATE OM_AccountContact SET ContactRoleID = @roleID WHERE AccountID = @accountID AND ContactID IN (SELECT ContactID FROM @updateContacts);
	ELSE
		UPDATE OM_AccountContact SET ContactRoleID = NULL WHERE AccountID = @accountID AND ContactID IN (SELECT ContactID FROM @updateContacts);
END
