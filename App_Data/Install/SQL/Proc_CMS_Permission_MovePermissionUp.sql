CREATE PROCEDURE [Proc_CMS_Permission_MovePermissionUp]
	@PermissionID int
AS
BEGIN
    /* Move elements only within same branch */
	DECLARE @ClassID int;
	DECLARE @ResourceID int;
	
	SET @ClassID = (SELECT TOP 1 ISNULL([ClassID],0) FROM CMS_Permission WHERE ([PermissionID] = @PermissionID));
	SET @ResourceID = (SELECT TOP 1 ISNULL([ResourceID],0) FROM CMS_Permission WHERE ([PermissionID] = @PermissionID));
	    
    IF @ClassID > 0 
		BEGIN
			/* Move the previous step(s) down */
			UPDATE [CMS_Permission] 
				SET [PermissionOrder] = [PermissionOrder] + 1 
			WHERE	([ClassID] = @ClassID) 
				AND	([PermissionOrder] = (SELECT [PermissionOrder] FROM [CMS_Permission] WHERE [PermissionID] = @PermissionID) - 1)
				
			/* Move the current step up */
			UPDATE [CMS_Permission] 
				SET	[PermissionOrder] = [PermissionOrder] - 1 
			WHERE	([PermissionID] = @PermissionID) 
				AND (PermissionOrder > 1)				
		END
	ELSE IF @ResourceID > 0
		BEGIN
			/* Move the previous step(s) down */
			UPDATE [CMS_Permission] 
				SET [PermissionOrder] = [PermissionOrder] + 1 
			WHERE	([ResourceID] = @ResourceID) 
				AND	([PermissionOrder] = (SELECT [PermissionOrder] FROM [CMS_Permission] WHERE [PermissionID] = @PermissionID) - 1)
				
			/* Move the current step up */
			UPDATE [CMS_Permission] 
				SET	[PermissionOrder] = [PermissionOrder] - 1 
			WHERE	([PermissionID] = @PermissionID) 
				AND (PermissionOrder > 1)		
		END
END
