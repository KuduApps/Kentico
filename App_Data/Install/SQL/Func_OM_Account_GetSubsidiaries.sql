CREATE FUNCTION [Func_OM_Account_GetSubsidiaries]
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
    -- Recursively find all children of current account
    WITH Recursion(AccountID)
    AS
    (
        SELECT AccountID
        FROM OM_Account a
        WHERE a.AccountSubsidiaryOfID = @currentAccountId
        UNION ALL
        SELECT a.AccountID
        FROM OM_Account a INNER JOIN Recursion r ON a.AccountSubsidiaryOfID = r.AccountID
        WHERE a.AccountID <> @currentAccountId
    )
    INSERT INTO @result SELECT AccountID FROM Recursion 
	-- Include parent account ID in result
	IF @includeParent = 1
      INSERT INTO @result VALUES (@currentAccountId)
	RETURN 
END
