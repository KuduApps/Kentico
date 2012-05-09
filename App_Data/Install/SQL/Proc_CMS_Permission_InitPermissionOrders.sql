CREATE PROCEDURE [Proc_CMS_Permission_InitPermissionOrders]
	@PermissionID int
AS
BEGIN
	DECLARE @ClassID int;
	DECLARE @ResourceID int;
	
	SET @ClassID = (SELECT TOP 1 ISNULL([ClassID],0) FROM CMS_Permission WHERE ([PermissionID] = @PermissionID));
	SET @ResourceID = (SELECT TOP 1 ISNULL([ResourceID],0) FROM CMS_Permission WHERE ([PermissionID] = @PermissionID));
		
	/* Declare the selection table */
	DECLARE @permissionTable TABLE 
	(
		PermissionID int NOT NULL,
		PermissionOrder int NULL
	);
	
	/* Get the steps list */
	IF @ClassID > 0 
		INSERT INTO @permissionTable SELECT [PermissionID], [PermissionOrder] FROM [CMS_Permission] WHERE [ClassID] = @ClassID
	ELSE IF @ResourceID > 0
		INSERT INTO @permissionTable SELECT [PermissionID], [PermissionOrder] FROM [CMS_Permission] WHERE [ResourceID] = @ResourceID
	
	/* Declare the cursor to loop through the table */
	DECLARE @permissionCursor CURSOR;
    SET @permissionCursor = CURSOR FOR SELECT [PermissionID], [PermissionOrder] FROM @permissionTable ORDER BY [PermissionOrder] ASC, [PermissionID] ASC;
    
	/* Assign the numbers to the steps */
	DECLARE @currentIndex int
	DECLARE @currentPermissionOrder int;
	DECLARE @currentPermissionId int;
	SET @currentIndex = 1;
	
	/* Loop through the table */
	OPEN @permissionCursor
	FETCH NEXT FROM @permissionCursor INTO @currentPermissionId, @currentPermissionOrder;
	WHILE @@FETCH_STATUS = 0
	BEGIN
		/* Set the step index if different */
		IF @currentPermissionOrder IS NULL OR @currentPermissionOrder <> @currentIndex
			UPDATE [CMS_Permission] SET [PermissionOrder] = @currentIndex WHERE [PermissionID] = @currentPermissionId;
						
		/* Get next record */
		SET @currentIndex = @currentIndex + 1;
		FETCH NEXT FROM @permissionCursor INTO @currentPermissionId, @currentPermissionOrder;
	END
	CLOSE @permissionCursor;
	DEALLOCATE @permissionCursor;
END
