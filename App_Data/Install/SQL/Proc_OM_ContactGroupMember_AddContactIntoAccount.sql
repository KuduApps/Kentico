CREATE PROCEDURE [Proc_OM_ContactGroupMember_AddContactIntoAccount]
	@accountID int,
	@contactID int
AS
BEGIN
	SET NOCOUNT ON;
	-- Variables
	DECLARE @tempTable TABLE (
		ContactGroupID int NOT NULL
	);	
	DECLARE @contactGroupID int;
	
	-- Update all existing contacts in ContactGroupMember table where account is present
	UPDATE OM_ContactGroupMember SET ContactGroupMemberFromAccount = 1 WHERE ContactGroupMemberType = 0 AND ContactGroupMemberRelatedID = @contactID
		AND ContactGroupMemberContactGroupID IN (SELECT ContactGroupMemberContactGroupID FROM OM_ContactGroupMember WHERE  ContactGroupMemberType = 1 AND ContactGroupMemberRelatedID = @accountID)
	-- Get all contact groups where current account is member and contact is not already specified there
	INSERT INTO @tempTable SELECT ContactGroupMemberContactGroupID FROM OM_ContactGroupMember 
		WHERE ContactGroupMemberRelatedID = @accountID AND ContactGroupMemberType = 1 AND ContactGroupMemberContactGroupID NOT IN
			(SELECT ContactGroupMemberContactGroupID FROM OM_ContactGroupMember WHERE ContactGroupMemberRelatedID = @contactID AND ContactGroupMemberType = 0)			
	
	-- Loop through all contacts of added account and insert them
	WHILE (SELECT Count(*) FROM @tempTable) > 0
	BEGIN
		SELECT TOP 1 @contactGroupID = ContactGroupID From @tempTable;
	
		-- Insert new contact as a contact group member
		INSERT INTO OM_ContactGroupMember 
		(ContactGroupMemberContactGroupID, ContactGroupMemberRelatedID, ContactGroupMemberType, ContactGroupMemberFromAccount) VALUES
		(@contactGroupID, @contactID, 0, 1)
		
		-- Remove record from temporary table
		DELETE @tempTable WHERE ContactGroupID = @contactGroupID
	END			
END
