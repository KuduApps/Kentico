CREATE PROCEDURE [Proc_CMS_UIElement_UpdateAfterImport] 
	@siteId INT,
	@packageVersion NVARCHAR(16),
	@topLevelElements bit
AS
BEGIN
	/* Declare the table of UI Elements */
	DECLARE @elementTable TABLE (
		ElementID INT NOT NULL,
		ElementParentID INT,
		ElementResourceID INT
	);
	/* Get UI elements newer than imported package */
	INSERT INTO @elementTable 
		SELECT ElementID, ElementParentID, ElementResourceID
		FROM CMS_UIElement 
		WHERE ElementFromVersion > @packageVersion AND ((@topLevelElements = 1 AND ElementLevel = 1) OR (@topLevelElements = 0 AND ElementLevel > 1))
		ORDER BY ElementLevel
	DECLARE @elementID INT; 
	DECLARE @elementParentID INT; 
	DECLARE @elementResourceID INT; 
	DECLARE @elementCursor CURSOR;
	SET @elementCursor = CURSOR FOR SELECT ElementID, ElementParentID, ElementResourceID FROM @elementTable
	OPEN @elementCursor
	FETCH NEXT FROM @elementCursor INTO @elementID, @elementParentID, @elementResourceID;
	WHILE @@FETCH_STATUS = 0
	BEGIN
		IF @elementID IS NOT NULL BEGIN
		
			DECLARE @readPermissionID int;
		
			/* Declare the selection table */
			DECLARE @rolesTable TABLE (
				RoleID INT NOT NULL
			);
			
			DELETE FROM @rolesTable;
			
			IF @topLevelElements = 1 BEGIN
				/* Get 'Read' permission ID for current element's resource */
				SET @readPermissionID = (SELECT [PermissionID] FROM CMS_Permission WHERE PermissionName = 'Read' AND ResourceID = @elementResourceID);
				
				/* When module does not have 'Read' permission */
				IF @readPermissionID IS NULL BEGIN
					/* Get the list of site roles */
					INSERT INTO @rolesTable SELECT RoleID FROM CMS_Role 
					WHERE SiteID = @siteId AND
						  RoleID NOT IN (SELECT RoleID FROM CMS_RoleUIElement WHERE ElementID = @elementID) AND
						  RoleName NOT IN ('cmsopenidusers', 'CMSFacebookUsers', 'CMSLiveIDUsers', '_everyone_', '_authenticated_', '_notauthenticated_')
				END
				ELSE BEGIN
					/* Get the list of site roles having 'Read' permission for element's resource and NOT having current element assigned */
					INSERT INTO @rolesTable SELECT RoleID FROM CMS_RolePermission
					WHERE RoleID IN (SELECT RoleID FROM CMS_Role WHERE SiteID = @siteId AND RoleName NOT IN ('cmsopenidusers', 'CMSFacebookUsers', 'CMSLiveIDUsers', '_everyone_', '_authenticated_', '_notauthenticated_')) AND
						  PermissionID = @readPermissionID AND
						  RoleID NOT IN (SELECT RoleID FROM CMS_RoleUIElement WHERE ElementID = @elementID) 
				END				
			END
			ELSE BEGIN
				/* Get the list of site roles having current element's parent assigned and NOT having CURRENT element assigned */
				INSERT INTO @rolesTable SELECT RoleID FROM CMS_RoleUIElement 
				WHERE RoleID IN (SELECT RoleID FROM CMS_Role WHERE SiteID = @siteId AND RoleName NOT IN ('cmsopenidusers', 'CMSFacebookUsers', 'CMSLiveIDUsers', '_everyone_', '_authenticated_', '_notauthenticated_')) AND
					  ElementID = @elementParentID AND
					  RoleID NOT IN (SELECT RoleID FROM CMS_RoleUIElement WHERE ElementID = @elementID)
			END
			
			/* Declare the cursor to loop through the roles */
			DECLARE @cursor CURSOR;
			SET @cursor = CURSOR FOR SELECT RoleID FROM @rolesTable;
			DECLARE @currentRoleID int;
			/* Loop through the table and assign elements to roles */
			OPEN @cursor
			FETCH NEXT FROM @cursor INTO @currentRoleID;
			WHILE @@FETCH_STATUS = 0
			BEGIN
				INSERT INTO CMS_RoleUIElement (RoleID, ElementID) VALUES (@currentRoleID, @elementID);
				FETCH NEXT FROM @cursor INTO @currentRoleID;
			END
			CLOSE @cursor;
			DEALLOCATE @cursor;
		END
		FETCH NEXT FROM @elementCursor INTO @elementID, @elementParentID, @elementResourceID;
	END
	CLOSE @elementCursor;
	DEALLOCATE @elementCursor; 
END
