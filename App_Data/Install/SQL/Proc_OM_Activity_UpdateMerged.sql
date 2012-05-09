CREATE PROCEDURE [Proc_OM_Activity_UpdateMerged]
 @bottomActivityID int,
 @topActivityID int
AS
BEGIN
	SET NOCOUNT ON;
	-- Variables
	DECLARE @templateTable TABLE (
		ContactID int NOT NULL
	);	
	DECLARE @currentContactID int;
	DECLARE @parentContactID int;
			
	 -- Get all merged contacts whose activities were processed in last activities update
	INSERT INTO @templateTable SELECT ContactID FROM OM_Contact WHERE ContactMergedWithContactID IS NOT NULL AND ContactID IN
	(SELECT ActivityActiveContactID FROM OM_Activity WHERE ActivityID >= @bottomActivityID AND ActivityID <= @topActivityID)
	 -- Loop through all merged contacts
	WHILE (SELECT Count(*) FROM @templateTable) > 0
	BEGIN
		SELECT TOP 1 @currentContactID = ContactID From @templateTable;
		-- Recursively find parent contact of current merged contact    
		WITH Recursion(ContactID, ContactMergedWithContactID)
		AS
		(
			SELECT ContactID, ContactMergedWithContactID
			FROM OM_Contact c
			WHERE c.ContactID = @currentContactID
			UNION ALL
			SELECT c.ContactID, c.ContactMergedWithContactID
			FROM OM_Contact c INNER JOIN Recursion r ON r.ContactMergedWithContactID = c.ContactID
		)
		SELECT TOP 1 @parentContactID = ContactID
		FROM Recursion 
		WHERE ContactMergedWithContactID IS NULL
		
		-- Update all activity records to point to parent contact
		UPDATE OM_Activity
		SET ActivityActiveContactID=@parentContactID
		WHERE ActivityOriginalContactID=@currentContactID
		-- Remove record from temporary table
		DELETE @templateTable WHERE ContactID = @currentContactID
	END
END
