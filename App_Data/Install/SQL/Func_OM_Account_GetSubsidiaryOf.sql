CREATE FUNCTION [Func_OM_Account_GetSubsidiaryOf]
(
	@currentAccountId int,
	@includeParent int
)
RETURNS @result TABLE
(
	AccountSubsidiaryOfID int
)
AS
BEGIN
    -- Recursively find all parents of current account
    WITH Recursion(AccountSubsidiaryOfID)
    AS
    (
        SELECT AccountSubsidiaryOfID
        FROM OM_Account a
        WHERE a.AccountID = @currentAccountId
        AND AccountSubsidiaryOfID IS NOT NULL
        UNION ALL
        SELECT a.AccountSubsidiaryOfID
        FROM OM_Account a INNER JOIN Recursion r ON a.AccountID = r.AccountSubsidiaryOfID
        WHERE a.AccountSubsidiaryOfID <> @currentAccountId
    )
    INSERT INTO @result SELECT AccountSubsidiaryOfID FROM Recursion 
	-- Include parent account ID in result
	IF @includeParent = 1
      INSERT INTO @result VALUES (@currentAccountId)
	RETURN 
END
