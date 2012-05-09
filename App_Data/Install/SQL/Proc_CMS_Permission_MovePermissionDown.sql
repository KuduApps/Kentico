CREATE PROCEDURE [Proc_CMS_Permission_MovePermissionDown]
	@PermissionID int
AS
BEGIN
    /* Move elements only within same branch */
	DECLARE @ClassID int;
	DECLARE @ResourceID int;
	
	SET @ClassID = (SELECT TOP 1 ISNULL([ClassID],0) FROM CMS_Permission WHERE ([PermissionID] = @PermissionID));
	SET @ResourceID = (SELECT TOP 1 ISNULL([ResourceID],0) FROM CMS_Permission WHERE ([PermissionID] = @PermissionID));
	
	DECLARE @MaxPermissionOrder int
	SET @MaxPermissionOrder = (SELECT TOP 1 [PermissionOrder] FROM [CMS_Permission] ORDER BY [PermissionOrder] DESC);	
	
	IF @ClassID > 0 
		BEGIN
			/* Move the next step(s) up */
			UPDATE [CMS_Permission] 
			SET [PermissionOrder] = [PermissionOrder] - 1 
			WHERE	([ClassID] = @ClassID)
				AND ([PermissionOrder] = (SELECT [PermissionOrder] FROM [CMS_Permission] WHERE [PermissionID] = @PermissionID) + 1)
		END
	ELSE IF @ResourceID > 0
		BEGIN
			/* Move the next step(s) up */
			UPDATE [CMS_Permission] 
			SET [PermissionOrder] = [PermissionOrder] - 1 
			WHERE	([ResourceID] = @ResourceID)
				AND ([PermissionOrder] = (SELECT [PermissionOrder] FROM [CMS_Permission] WHERE [PermissionID] = @PermissionID) + 1)
		END
		
	/* Move the current step down */
	UPDATE [CMS_Permission] 
	SET	[PermissionOrder] = [PermissionOrder] + 1 
	WHERE	([PermissionID] = @PermissionID) 
		AND ([PermissionOrder] < @MaxPermissionOrder)			
END
