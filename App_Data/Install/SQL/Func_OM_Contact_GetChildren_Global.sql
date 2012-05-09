CREATE FUNCTION [Func_OM_Contact_GetChildren_Global] 
(
	@currentContactId int,
	@includeParent int
)
RETURNS @result TABLE
(
	ContactID int
)
AS
BEGIN
	-- Get all contacts from first level
	DECLARE @contacts TABLE 
	( 
		contactid INT
	)
	INSERT INTO @contacts SELECT ContactID FROM OM_Contact WHERE ContactGlobalContactID = @currentContactId ORDER BY contactid
   
	-- Iterate through first level contacts
	DECLARE @contactid int;
	DECLARE @contactSiteID int;
	SELECT TOP 1 @contactid = contactid FROM @contacts
	WHILE (@contactid IS NOT NULL)
	BEGIN
	   SELECT @contactSiteID = ContactSiteID FROM OM_Contact WHERE ContactID = @contactid;
	   -- Is it global contact?
	   IF @contactSiteID IS NULL
	      -- process global contact
	     INSERT INTO @result SELECT * FROM Func_OM_Contact_GetChildren_Global(@contactid, 1);
	   ELSE 
         -- process site contact
	     INSERT INTO @result SELECT * FROM Func_OM_Contact_GetChildren(@contactid, 1);
	   SET @contactid = (SELECT TOP 1 contactid FROM @contacts WHERE contactid > @contactid)
	END
	-- Include parent contact ID in result
	IF @includeParent = 1    
	BEGIN
		INSERT INTO @result SELECT @currentContactId
	END
    
	RETURN 
END
