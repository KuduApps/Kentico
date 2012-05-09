CREATE PROCEDURE [Proc_OM_ContactGroupMember_UpdateMembersForAccount]
	@accountID int,
	@groupID int
AS
BEGIN
	SET NOCOUNT ON;
	
	-- Variables
	DECLARE @tempTable TABLE (
		ContactID int NOT NULL
	);	
	DECLARE @contactGroups TABLE (
		ContactGroupID int NOT NULL
	);	
	DECLARE @contactID int;
	DECLARE @currentGroupID int;
	
	-- Loop through all contact groups of current account
	IF (@groupID = 0) OR (@groupID IS NULL)
	BEGIN
		-- Get all contact groups where account is present
		INSERT INTO @contactGroups SELECT ContactGroupMemberContactGroupID FROM OM_ContactGroupMember WHERE ContactGroupMemberType = 1 
		AND ContactGroupMemberRelatedID = @accountID 
		SELECT TOP 1 @currentGroupID = ContactGroupID FROM @contactGroups
	END
	-- Or use single contact group
	ELSE
		SET @currentGroupID = @groupID;
	WHILE ((@currentGroupID > 0) AND (@currentGroupID IS NOT NULL))
	BEGIN	
		-- Update all contacts which already exist under the group and mark them as added from account
		UPDATE OM_ContactGroupMember SET ContactGroupMemberFromAccount = 1 WHERE ContactGroupMemberType = 0 AND ContactGroupMemberContactGroupID = @currentGroupID
			AND ContactGroupMemberRelatedID IN (SELECT ContactID FROM OM_AccountContact WHERE AccountID = @accountID)
		
		-- Get all contacts from current account which don't already exist as member of the group
		INSERT INTO @tempTable SELECT ContactID FROM OM_AccountContact WHERE AccountID = @accountID 
			AND ContactID NOT IN (SELECT ContactGroupMemberRelatedID FROM OM_ContactGroupMember 
			WHERE ContactGroupMemberContactGroupID = @currentGroupID  AND ContactGroupMemberType = 0)
			
		-- Loop through all contacts of added account and insert them into the group
		WHILE ((SELECT Count(*) FROM @tempTable) > 0)
		BEGIN
			SELECT TOP 1 @contactID = ContactID From @tempTable;
		
			-- Insert new contact as a contact group member
			INSERT INTO OM_ContactGroupMember 
			(ContactGroupMemberContactGroupID, ContactGroupMemberRelatedID, ContactGroupMemberType, ContactGroupMemberFromAccount) VALUES
			(@currentGroupID, @contactID, 0, 1)
			
			-- Remove record from temporary table
			DELETE @tempTable WHERE ContactID = @contactID
		END
			
		-- Go to next contact group
		DELETE FROM @contactGroups WHERE ContactGroupID = @currentGroupID;
		SET @currentGroupID = 0;
		SELECT TOP 1 @currentGroupID = ContactGroupID FROM @contactGroups;
	END
END
