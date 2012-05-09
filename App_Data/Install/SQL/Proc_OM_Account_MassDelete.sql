CREATE PROCEDURE [Proc_OM_Account_MassDelete]
@where nvarchar(max),
	@deleteSubsidiaries bit
AS
BEGIN
SET NOCOUNT ON;
    -- Variables
	DECLARE @DeletedAccounts TABLE (
		AccountID int NOT NULL,
		AccountSiteID int NULL
	);	
	DECLARE @DeletedMergedAccounts TABLE (
		AccountID int NOT NULL
	);		
	DECLARE @SubsidiaryAccounts TABLE (
		AccountID int NOT NULL,
		AccountSiteID int NULL
	);
	DECLARE @MergedSubsidiaryAccounts TABLE (
		AccountID int NOT NULL
	);
	DECLARE @currentAccountID int;
	DECLARE @currentAccountSiteID int;
	DECLARE @currentDeletedAccountID int;
	DECLARE @currentSubsidiaryAccountID int;
	DECLARE @currentSubsidiaryAccountSiteID int;
	DECLARE @currentMergedSubsidiaryAccountID int;
	DECLARE @sqlQuery NVARCHAR(MAX);
	-- Get TOP 1000 of deleted accounts
	SET @sqlQuery = 'SELECT TOP 1000 AccountID, AccountSiteID FROM OM_Account WHERE ' + @where;
	INSERT INTO @DeletedAccounts EXEC(@sqlQuery);
	-- Process first batch of records
	WHILE ((SELECT Count(*) FROM @DeletedAccounts) > 0)
	BEGIN			
		-- Loop through records
		WHILE ((SELECT Count(*) FROM @DeletedAccounts) > 0)
		BEGIN		
			SELECT TOP 1 @currentAccountID = AccountID, @currentAccountSiteID = AccountSiteID FROM @DeletedAccounts;
			-- Get merged accounts
			IF @currentAccountSiteID > 0
				INSERT INTO @DeletedMergedAccounts SELECT AccountID FROM Func_OM_Account_GetChildren(@currentAccountID, 1)
			ELSE
				INSERT INTO @DeletedMergedAccounts SELECT AccountID FROM Func_OM_Account_GetChildren_Global(@currentAccountID, 1)
			
			-- Delete merged and parent records
			WHILE ((SELECT Count(*) FROM @DeletedMergedAccounts) > 0)
			BEGIN
				SELECT TOP 1 @currentDeletedAccountID = AccountID FROM @DeletedMergedAccounts;
				
				-- Delete Subsidiary accounts
				IF @deleteSubsidiaries > 0
				BEGIN
					INSERT INTO @SubsidiaryAccounts SELECT AccountID, AccountSiteID FROM OM_Account WHERE AccountSubsidiaryOfID = @currentDeletedAccountID;
					
					WHILE ((SELECT Count(*) FROM @SubsidiaryAccounts) > 0)
					BEGIN		
						SELECT TOP 1 @currentSubsidiaryAccountID = AccountID, @currentSubsidiaryAccountSiteID = AccountSiteID FROM @SubsidiaryAccounts;
						
						-- Get merged accounts into subsidiary accounts
						IF @currentSubsidiaryAccountSiteID > 0
							INSERT INTO @MergedSubsidiaryAccounts SELECT AccountID FROM Func_OM_Account_GetChildren(@currentSubsidiaryAccountID, 1)
						ELSE
							INSERT INTO @MergedSubsidiaryAccounts SELECT AccountID FROM Func_OM_Account_GetChildren_Global(@currentSubsidiaryAccountID, 1)
							
						-- Delete merged subsidiary accounts
						WHILE ((SELECT Count(*) FROM @MergedSubsidiaryAccounts) > 0)
						BEGIN
							SELECT TOP 1 @currentMergedSubsidiaryAccountID = AccountID FROM @MergedSubsidiaryAccounts;
							
							-- Remove contacts from contact groups which are added via the account
							SET @where = 'AccountID = ' + CAST(@currentMergedSubsidiaryAccountID AS nvarchar(50));
							EXEC Proc_OM_ContactGroupMember_RemoveContactsFromAccount @where
							-- Remove dependency
							EXEC Proc_OM_Account_RemoveDependencies @currentMergedSubsidiaryAccountID
							-- Delete record
							DELETE FROM OM_Account WHERE AccountID = @currentMergedSubsidiaryAccountID;
						
							DELETE FROM @MergedSubsidiaryAccounts WHERE AccountID = @currentMergedSubsidiaryAccountID;
						END
						
						DELETE FROM @SubsidiaryAccounts WHERE AccountID = @currentSubsidiaryAccountID;
					END
				END
				-- Remove contacts from contact groups which are added via the account
				SET @where = 'AccountID = ' + CAST(@currentDeletedAccountID AS nvarchar(50));
				EXEC Proc_OM_ContactGroupMember_RemoveContactsFromAccount @where
				-- Remove dependency
				EXEC Proc_OM_Account_RemoveDependencies @currentDeletedAccountID
				-- Delete record
				DELETE FROM OM_Account WHERE AccountID = @currentDeletedAccountID;
				DELETE FROM @DeletedMergedAccounts WHERE AccountID = @currentDeletedAccountID;
			END
			
			DELETE FROM @DeletedAccounts WHERE AccountID = @currentAccountID;
		END
		
		-- Get next batch of a
		INSERT INTO @DeletedAccounts EXEC(@sqlQuery);
	END
END
