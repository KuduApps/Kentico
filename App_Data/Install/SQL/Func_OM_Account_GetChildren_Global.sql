CREATE FUNCTION [Func_OM_Account_GetChildren_Global] 
(
	@currentAccountId int,
	@includeParent int
)
RETURNS @result TABLE
(
	AccountID int
)
AS
BEGIN
	-- Get all contacts from first level
	DECLARE @accounts TABLE 
	( 
		accountid INT
	)
	INSERT INTO @accounts SELECT AccountID FROM OM_Account WHERE AccountGlobalAccountID = @currentAccountId ORDER BY accountid
   
	-- Iterate through first level contacts
	DECLARE @accountid int;
	DECLARE @accountSiteID int;
	SELECT TOP 1 @accountid = accountid FROM @accounts
	WHILE (@accountid IS NOT NULL)
	BEGIN
	   SELECT @accountSiteID = AccountSiteID FROM OM_Account WHERE AccountID = @accountid;
	   -- Is it global account?
	   IF @accountSiteID IS NULL
	      -- process global account
	     INSERT INTO @result SELECT * FROM Func_OM_Account_GetChildren_Global(@accountid, 1);
	   ELSE 
         -- process site account
	     INSERT INTO @result SELECT * FROM Func_OM_Account_GetChildren(@accountid, 1);
	   SET @accountid = (SELECT TOP 1 accountid FROM @accounts WHERE accountid > @accountid)
	END
	-- Include parent contact ID in result
	IF @includeParent = 1    
	BEGIN
		INSERT INTO @result SELECT @currentAccountId
	END
    
	RETURN 
END
