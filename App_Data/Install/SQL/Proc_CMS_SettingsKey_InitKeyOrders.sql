CREATE PROCEDURE [Proc_CMS_SettingsKey_InitKeyOrders] 
	@KeyName nvarchar(100)
AS
BEGIN
	DECLARE @KeyCategoryID int
	SET @KeyCategoryID = (SELECT TOP 1 KeyCategoryID FROM CMS_SettingsKey WHERE KeyName = @KeyName AND SiteID IS NULL);
	
	/* Declare the selection table */
	DECLARE @keyTable TABLE (
		KeyName nvarchar(100) NOT NULL,
		KeyOrder int NULL,
		KeyDisplayName nvarchar(200) NOT NULL
	);
	
	/* Get the all keys list */
	INSERT INTO @keyTable SELECT KeyName, KeyOrder, KeyDisplayName FROM CMS_SettingsKey WHERE SiteID IS NULL AND KeyCategoryID = @KeyCategoryID
	
	/* Declare the cursor to loop through the table */
	DECLARE @keyCursor CURSOR;
    SET @keyCursor = CURSOR FOR SELECT KeyName, KeyOrder FROM @keyTable ORDER BY KeyOrder, KeyDisplayName;
	/* Assign the numbers to the keys */
	DECLARE @currentIndex int, @currentKeyOrder int;
	SET @currentIndex = 1;
	DECLARE @currentKeyName nvarchar(100);
	
	/* Loop through the table */
	OPEN @keyCursor
	FETCH NEXT FROM @keyCursor INTO @currentKeyName, @currentKeyOrder;
	WHILE @@FETCH_STATUS = 0
	BEGIN
		/* Set the keys index if different */
		IF @currentKeyOrder IS NULL OR @currentKeyOrder <> @currentIndex
			UPDATE CMS_SettingsKey SET KeyOrder = @currentIndex WHERE KeyName = @currentKeyName;
		/* Get next record */
		SET @currentIndex = @currentIndex + 1;
		FETCH NEXT FROM @keyCursor INTO @currentKeyName, @currentKeyOrder;
	END
	CLOSE @keyCursor;
	DEALLOCATE @keyCursor;
END
