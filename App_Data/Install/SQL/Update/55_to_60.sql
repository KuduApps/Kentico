/* 
 * This script assigns the newly created UIElements to all roles (except of generic roles)
 */

DECLARE @siteId INT; SET @siteId = ##SITEID##; 
 
/* Declare the table of newly added UI Elements */
DECLARE @elementTable TABLE (
	ElementID INT NOT NULL
);

INSERT INTO @elementTable SELECT ElementID FROM CMS_UIElement WHERE ElementName IN ('New.LinkExistingDocument', 'New', 'New.SelectTemplate', 'SelectTemplate.UseTemplate', 'SelectTemplate.InheritFromParent', 'SelectTemplate.CreateBlank') AND ElementResourceID IN (SELECT ResourceID FROM CMS_Resource WHERE ResourceName = 'CMS.Content')
			
DECLARE @elementID INT; 

DECLARE @elementCursor CURSOR;
SET @elementCursor = CURSOR FOR SELECT ElementID FROM @elementTable

OPEN @elementCursor
FETCH NEXT FROM @elementCursor INTO @elementID;
WHILE @@FETCH_STATUS = 0
BEGIN
	IF @elementID IS NOT NULL BEGIN
	
		/* Declare the selection table */
		DECLARE @permissionTable TABLE (
			RoleID INT NOT NULL
		);
			
		/* Get the list of roles */
		DELETE FROM @permissionTable;
		INSERT INTO @permissionTable SELECT RoleID FROM CMS_Role 
			WHERE RoleName NOT IN ('cmsopenidusers', 'CMSFacebookUsers', 'CMSLiveIDUsers', '_everyone_', '_authenticated_', '_notauthenticated_') AND
			      RoleID IN (SELECT RoleID FROM CMS_Role WHERE SiteID = @siteId)
			
		/* Declare the cursor to loop through the table */
		DECLARE @cursor CURSOR;
		SET @cursor = CURSOR FOR SELECT RoleID FROM @permissionTable

		DECLARE @currentRoleID int;

		/* Loop through the table */
		OPEN @cursor
		FETCH NEXT FROM @cursor INTO @currentRoleID;
		WHILE @@FETCH_STATUS = 0
		BEGIN
			IF NOT EXISTS (SELECT 1 FROM CMS_RoleUIElement WHERE RoleID = @currentRoleID AND ElementID = @elementID) BEGIN
				INSERT INTO CMS_RoleUIElement (RoleID, ElementID) VALUES (@currentRoleID, @elementID);
			END
			FETCH NEXT FROM @cursor INTO @currentRoleID;
		END
		CLOSE @cursor;
		DEALLOCATE @cursor;
	END
	
FETCH NEXT FROM @elementCursor INTO @elementID;
END
CLOSE @elementCursor;
DEALLOCATE @elementCursor; 


/* 
 * This script assigns the new 'EcommerceGlobalModify' permission to all site roles having 'EcommerceModify' permission.
 */

/* Get ID of ecommerce module */
DECLARE @ecommerceResourceID int;
SET @ecommerceResourceID = (SELECT ResourceID FROM CMS_Resource WHERE ResourceName = 'CMS.Ecommerce');

/* Get 'EcommerceModify' permission ID */
DECLARE @ecommModifyPermissionID int;
SET @ecommModifyPermissionID = (SELECT [PermissionID] FROM CMS_Permission WHERE PermissionName = 'EcommerceModify' AND ResourceID = @EcommerceResourceID);

/* Get 'EcommerceGlobalModify' permission ID */
DECLARE @ecomGlobalModifyPermissionID int;
SET @ecomGlobalModifyPermissionID = (SELECT [PermissionID] FROM CMS_Permission WHERE PermissionName = 'EcommerceGlobalModify' AND ResourceID = @EcommerceResourceID);

/* Assign 'EcommerceGlobalModify' permission to site roles having 'EcommerceModify' */
INSERT INTO CMS_RolePermission(RoleID, PermissionID) (
	SELECT RoleID, @ecomGlobalModifyPermissionID
	FROM CMS_RolePermission
	WHERE PermissionID = @ecommModifyPermissionID AND
		RoleID NOT IN (SELECT RoleID FROM CMS_RolePermission WHERE PermissionID = @ecomGlobalModifyPermissionID) AND
		RoleID IN (SELECT [RoleID] FROM CMS_Role WHERE SiteID = @siteId)
)

/* Get 'ConfigurationModify' permission ID */
DECLARE @ecommConfModifyPermissionID int;
SET @ecommConfModifyPermissionID = (SELECT [PermissionID] FROM CMS_Permission WHERE PermissionName = 'ConfigurationModify' AND ResourceID = @EcommerceResourceID);

/* Get 'ConfigurationGlobalModify' permission ID */
DECLARE @ecomGlobalConfModifyPermissionID int;
SET @ecomGlobalConfModifyPermissionID = (SELECT [PermissionID] FROM CMS_Permission WHERE PermissionName = 'ConfigurationGlobalModify' AND ResourceID = @EcommerceResourceID);

/* Assign 'ConfigurationGlobalModify' permission to site roles having 'ConfigurationModify' */
INSERT INTO CMS_RolePermission(RoleID, PermissionID) (
	SELECT RoleID, @ecomGlobalConfModifyPermissionID
	FROM CMS_RolePermission
	WHERE PermissionID = @ecommConfModifyPermissionID AND
		RoleID NOT IN (SELECT RoleID FROM CMS_RolePermission WHERE PermissionID = @ecomGlobalConfModifyPermissionID) AND
		RoleID IN (SELECT [RoleID] FROM CMS_Role WHERE SiteID = @siteId)
)

/* 
 * This script assigns the new 'GlobalModify' (and 'GlobalRead') permission for polls to all site roles having 'Modify' (and 'GlobalModify') polls permission.
 */

/* Get ID of polls module */
DECLARE @pollsResourceID int;
SET @pollsResourceID = (SELECT ResourceID FROM CMS_Resource WHERE ResourceName = 'CMS.Polls');

/* Get 'Modify' permission ID */
DECLARE @pollsModifyPermissionID int;
SET @pollsModifyPermissionID = (SELECT [PermissionID] FROM CMS_Permission WHERE PermissionName = 'Modify' AND ResourceID = @pollsResourceID);

/* Get 'GlobalModify' permission ID */
DECLARE @pollsGlobalModifyPermissionID int;
SET @pollsGlobalModifyPermissionID = (SELECT [PermissionID] FROM CMS_Permission WHERE PermissionName = 'GlobalModify' AND ResourceID = @pollsResourceID);

/* Assign 'GlobalModify' permission to site roles having 'Modify' */
INSERT INTO CMS_RolePermission(RoleID, PermissionID) (
	SELECT RoleID, @pollsGlobalModifyPermissionID
	FROM CMS_RolePermission
	WHERE PermissionID = @pollsModifyPermissionID AND
		RoleID NOT IN (SELECT RoleID FROM CMS_RolePermission WHERE PermissionID = @pollsGlobalModifyPermissionID) AND
		RoleID IN (SELECT [RoleID] FROM CMS_Role WHERE SiteID = @siteId)
)

/* Get 'Read' permission ID */
DECLARE @pollsReadPermissionID int;
SET @pollsReadPermissionID = (SELECT [PermissionID] FROM CMS_Permission WHERE PermissionName = 'Read' AND ResourceID = @pollsResourceID);

/* Get 'GlobalRead' permission ID */
DECLARE @pollsGlobalReadPermissionID int;
SET @pollsGlobalReadPermissionID = (SELECT [PermissionID] FROM CMS_Permission WHERE PermissionName = 'GlobalRead' AND ResourceID = @pollsResourceID);

/* Assign 'GlobalRead' permission to site roles having 'Read' */
INSERT INTO CMS_RolePermission(RoleID, PermissionID) (
	SELECT RoleID, @pollsGlobalReadPermissionID
	FROM CMS_RolePermission
	WHERE PermissionID = @pollsReadPermissionID AND
		RoleID NOT IN (SELECT RoleID FROM CMS_RolePermission WHERE PermissionID = @pollsGlobalReadPermissionID) AND
		RoleID IN (SELECT [RoleID] FROM CMS_Role WHERE SiteID = @siteId)
)
