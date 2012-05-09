----------------------------------------------------------------------------------
--   Convert MyDesk UIElements   -------------------------------------------------
--
--   Description: Converts the UIElements for MyDesk into a new default layout.
--	              User-defined elements are grouped in the section "Custom"
----------------------------------------------------------------------------------

DECLARE @siteId INT;
SET @siteId = ##SITEID##; 

DECLARE @myDeskResourceId int;
DECLARE @myDeskElementId int;
DECLARE @myDeskIDPath nvarchar(450);
DECLARE @myDeskCustomElementId int;
DECLARE @myDeskCustomElementName nvarchar(200);
DECLARE @tempId int;
DECLARE @order int;

-- Declare a table where will be stored default UIElements for MyDesk
DECLARE @defaultUIElementsTable TABLE (
	ElementName nvarchar(200),
	ElementParentID int,
	ElementLevel int,
	ElementOrder int
);

-- Set default values
SET @myDeskCustomElementName = 'MyDeskCustom'; -- codename of the custom folder
SET @myDeskResourceId = (SELECT ResourceID FROM CMS_Resource WHERE ResourceName = 'CMS.MyDesk');
SET @myDeskCustomElementId = (SELECT ElementID FROM CMS_UIElement WHERE ElementName = @myDeskCustomElementName AND ElementResourceID = @myDeskResourceId);
SET @myDeskElementId = (SELECT ElementID FROM CMS_UIElement WHERE ElementName = 'cmsmydesk' AND ElementResourceID = @myDeskResourceId);
-- Create path in format "/00001234/00002555"
SET @myDeskIdPath = '/' + REPLICATE('0', 8 - DATALENGTH(CAST(@myDeskElementId as varchar))) + CAST(@myDeskElementId as varchar);

---------------------------------------------------
--   Create "Custom" UIElement   ------------------
---------------------------------------------------
-- create a new "Custom" record if does not exist
IF @myDeskCustomElementId IS NULL
BEGIN
	DECLARE @myDeskCustomElementsCount int;
	SET @myDeskCustomElementsCount = (SELECT COUNT(*)
		FROM CMS_UIElement
		WHERE
			CMS_UIElement.ElementResourceID = @myDeskResourceId AND
			ElementName NOT IN (SELECT ElementName FROM @defaultUIElementsTable));
		
	-- Create "Custom" folder only if there are custom elements
	IF (@myDeskCustomElementsCount > 0)
	BEGIN
		DECLARE @myDeskCustomElementOrder int;
		SET @myDeskCustomElementOrder = (SELECT COUNT(*) FROM CMS_UIElement WHERE ElementParentID = @myDeskElementID) + 1;

		INSERT INTO [CMS_UIElement]( [ElementDisplayName],   [ElementName],            [ElementCaption],       [ElementTargetURL], [ElementResourceID], [ElementParentID], [ElementChildCount], [ElementOrder],             [ElementLevel], [ElementIDPath], [ElementIconPath], [ElementIsCustom], [ElementLastModified], [ElementGUID], [ElementSize], [ElementDescription],               [ElementFromVersion])
						 VALUES    ( '{$mydesk.ui.custom$}', @myDeskCustomElementName, '{$mydesk.ui.custom$}', '',                 @myDeskResourceID,   @myDeskElementID,  0,                   @myDeskCustomElementsCount, 1,              @myDeskIDPath,   '',                0,                 GETDATE(),             NEWID(),       0,             '{$mydesk.ui.custom.description$}', '6.0');
				   
		-- Update CustomElementID
		SET @myDeskCustomElementId = (SELECT SCOPE_IDENTITY());
	END
END

-- Create a temporary table containing default element structure for MyDesk
-- Define MyDesk (root)
INSERT INTO @defaultUIElementsTable (ElementName, ElementParentID, ElementLevel, ElementOrder) VALUES ( 'cmsmydesk', 0, 0, 0);
-- Define root folders
SET @tempId = @myDeskElementId;
INSERT INTO @defaultUIElementsTable (ElementName, ElementParentID, ElementLevel, ElementOrder) VALUES ( 'MyDeskDashBoard', @tempId, 1, 1);
INSERT INTO @defaultUIElementsTable (ElementName, ElementParentID, ElementLevel, ElementOrder) VALUES ( 'MyDeskDocuments', @tempId, 1, 2);
INSERT INTO @defaultUIElementsTable (ElementName, ElementParentID, ElementLevel, ElementOrder) VALUES ( 'MyDeskMyData', @tempId, 1, 3);
INSERT INTO @defaultUIElementsTable (ElementName, ElementParentID, ElementLevel, ElementOrder) VALUES ( @myDeskCustomElementName, @tempId, 1, 4);
-- Set folder Dashboard
SET @tempId = (SELECT ElementID FROM CMS_UIElement WHERE ElementResourceID = @myDeskResourceId AND ElementName = 'MyDeskDashBoard');
INSERT INTO @defaultUIElementsTable (ElementName, ElementParentID, ElementLevel, ElementOrder) VALUES ( 'MyDeskDashBoardItem', @tempId, 2, 1);
-- Set folder Documents
SET @tempId = (SELECT ElementID FROM CMS_UIElement WHERE ElementResourceID = @myDeskResourceId AND ElementName = 'MyDeskDocuments');
INSERT INTO @defaultUIElementsTable (ElementName, ElementParentID, ElementLevel, ElementOrder) VALUES ( 'RecentDocs', @tempId, 2, 1);
INSERT INTO @defaultUIElementsTable (ElementName, ElementParentID, ElementLevel, ElementOrder) VALUES ( 'OutdatedDocs', @tempId, 2, 2);
INSERT INTO @defaultUIElementsTable (ElementName, ElementParentID, ElementLevel, ElementOrder) VALUES ( 'WaitingDocs', @tempId, 2, 3);
INSERT INTO @defaultUIElementsTable (ElementName, ElementParentID, ElementLevel, ElementOrder) VALUES ( 'CheckedOutDocs', @tempId, 2, 4);
INSERT INTO @defaultUIElementsTable (ElementName, ElementParentID, ElementLevel, ElementOrder) VALUES ( 'MyDocuments', @tempId, 2, 5);
-- Set folder My Data
SET @tempId = (SELECT ElementID FROM CMS_UIElement WHERE ElementResourceID = @myDeskResourceId AND ElementName = 'MyDeskMyData');
INSERT INTO @defaultUIElementsTable (ElementName, ElementParentID, ElementLevel, ElementOrder) VALUES ( 'MyProfile', @tempId, 2, 1);
INSERT INTO @defaultUIElementsTable (ElementName, ElementParentID, ElementLevel, ElementOrder) VALUES ( 'MyBlogs', @tempId, 2, 2);
INSERT INTO @defaultUIElementsTable (ElementName, ElementParentID, ElementLevel, ElementOrder) VALUES ( 'MyFriends', @tempId, 2, 3);
INSERT INTO @defaultUIElementsTable (ElementName, ElementParentID, ElementLevel, ElementOrder) VALUES ( 'MyMessages', @tempId, 2, 4);
INSERT INTO @defaultUIElementsTable (ElementName, ElementParentID, ElementLevel, ElementOrder) VALUES ( 'MyProjects', @tempId, 2, 5);
INSERT INTO @defaultUIElementsTable (ElementName, ElementParentID, ElementLevel, ElementOrder) VALUES ( 'MyRecycleBin', @tempId, 2, 6);
-- Set folder Account
SET @tempId = (SELECT ElementID FROM CMS_UIElement WHERE ElementResourceID = @myDeskResourceId AND ElementName = 'MyProfile');
INSERT INTO @defaultUIElementsTable (ElementName, ElementParentID, ElementLevel, ElementOrder) VALUES ( 'MyProfile.Details', @tempId, 3, 1);
INSERT INTO @defaultUIElementsTable (ElementName, ElementParentID, ElementLevel, ElementOrder) VALUES ( 'MyProfile.ChangePassword', @tempId, 3, 2);
INSERT INTO @defaultUIElementsTable (ElementName, ElementParentID, ElementLevel, ElementOrder) VALUES ( 'MyProfile.Notifications', @tempId, 3, 3);
INSERT INTO @defaultUIElementsTable (ElementName, ElementParentID, ElementLevel, ElementOrder) VALUES ( 'MyProfile.Subscriptions', @tempId, 3, 4);
INSERT INTO @defaultUIElementsTable (ElementName, ElementParentID, ElementLevel, ElementOrder) VALUES ( 'MyProfile.Categories', @tempId, 3, 5);
-- Set Folder Recycle Bin
SET @tempId = (SELECT ElementID FROM CMS_UIElement WHERE ElementResourceID = @myDeskResourceId AND ElementName = 'MyRecycleBin');
INSERT INTO @defaultUIElementsTable (ElementName, ElementParentID, ElementLevel, ElementOrder) VALUES ( 'MyRecycleBin.Documents', @tempId, 3, 1);
INSERT INTO @defaultUIElementsTable (ElementName, ElementParentID, ElementLevel, ElementOrder) VALUES ( 'MyRecycleBin.Objects', @tempId, 3, 2);

-------------------------------------------------------------------------------------------------
--   Update all MyDesk UIElements (ElementIconPath, ElementTargetUrl, ElementParentID ...)  -----
-------------------------------------------------------------------------------------------------
DECLARE @cursorMyDesk CURSOR;
-- Set the cursor to loop through all MyDesk UIElements
SET @cursorMyDesk = CURSOR FOR
	SELECT * FROM (
		-- Select default MyDesk elements (Dashboard, My data, My Documents ...), get ElementLevel and ElementOrder from the table @defaultUIElementsTable
		SELECT CMS_UIElement.ElementID,
			CMS_UIElement.ElementName,
			DefaultElTable.ElementParentID,
			CMS_UIElement.ElementTargetUrl,
			CMS_UIElement.ElementIconPath,
			DefaultElTable.ElementLevel,
			DefaultElTable.ElementOrder
		FROM @defaultUIElementsTable AS DefaultElTable
			INNER JOIN CMS_UIElement ON DefaultElTable.ElementName = CMS_UIElement.ElementName
		WHERE
			CMS_UIElement.ElementResourceID = @myDeskResourceId AND
			CMS_UIElement.ElementID <> @myDeskElementId -- exclude MyDesk root UIElement (cmsmydesk)
		UNION
		-- Select all custom MyDesk UIElements
		SELECT ElementID, ElementName, @myDeskCustomElementId, ElementTargetUrl, ElementIconPath, 0, ElementOrder -- ElementLevel = 0 -> will be proceeded first
		FROM CMS_UIElement
		WHERE
			CMS_UIElement.ElementResourceID = @myDeskResourceId AND
			ElementName NOT IN (SELECT ElementName FROM @defaultUIElementsTable)
	) AS MyDeskUIElements
	ORDER BY ElementLevel, ElementParentID, ElementOrder;

-- UIElement order 
SET @order = 1;

-- Declare temporary variables for the cursor
DECLARE @currentElementId int;
DECLARE @currentElementName nvarchar(200);
DECLARE @currentElementParentId int;
DECLARE @currentElementTargetUrl nvarchar(450);
DECLARE @currentElementIconPath nvarchar(200);
DECLARE @currentElementLevel int;
DECLARE @currentElementOrder int;
DECLARE @currentElementIdPath nvarchar(450);
DECLARE @lastParentId int;

SET @lastParentId = @myDeskElementId;
-- Loop through all MyDesk UIElements and update its columns
OPEN @cursorMyDesk
FETCH NEXT FROM @cursorMyDesk INTO @currentElementId, @currentElementName, @currentElementParentId, @currentElementTargetUrl, @currentElementIconPath, @currentElementLevel, @currentElementOrder;
WHILE @@FETCH_STATUS = 0
BEGIN
	-- change ElementIconPath
	SET @currentElementIconPath = REPLACE(@currentElementIconPath, 'list.png', 'module.png');
	-- change ElementTargetUrl
	SET @currentElementTargetUrl = REPLACE(@currentElementTargetUrl, '~/CMSDesk/MyDesk/', '~/CMSModules/MyDesk/');
	-- Set parent to "MyDeskCustom" if the default parent has been deleted
	IF (@currentElementParentId IS NULL) BEGIN SET @currentElementParentId = @myDeskCustomElementId END;	
	-- rebuild ElementIdPath in format "/00001234/00002555"
	SET @currentElementIdPath = (SELECT ElementIDPath FROM CMS_UIElement WHERE ElementID = @currentElementParentId) + '/' + REPLICATE('0', 8 - DATALENGTH(CAST(@currentElementId as varchar))) + CAST(@currentElementId as varchar);
	-- Reset order if changing folder
	IF (@lastParentId <> @currentElementParentId) BEGIN SET @order = 1 END;
	
	-- Update UIElement
	--IF (@currentElementParentId <> @customElementId)
	IF (@currentElementName IN (SELECT ElementName FROM @defaultUIElementsTable))
	BEGIN
		-- is NOT a custom UIElement, update all chaged fields
		UPDATE CMS_UIElement SET 
			ElementIconPath = @currentElementIconPath,
			ElementTargetUrl = @currentElementTargetUrl,
			ElementParentID = @currentElementParentId,
			ElementLevel = @currentElementLevel,
			ElementOrder = @order,
			ElementIDPath = @currentElementIdPath
		WHERE 
			ElementID = @currentElementId;
	END
	ELSE
	BEGIN
		-- IS a custom UIElement, update only some fields
		UPDATE CMS_UIElement SET 
			ElementIDPath = @currentElementIdPath,
			ElementOrder = @order,
			ElementParentID = @myDeskCustomElementId,
			ElementChildCount = 0,
			ElementIsCustom = 1,
			ElementLevel = 2,
			ElementSize = 1 -- small icons
		WHERE 
			ElementID = @currentElementId;
	END
	
	-- prepare temp variables for the next step	
	SET @lastParentId = @currentElementParentId;
	SET @order = @order + 1;
	
	FETCH NEXT FROM @cursorMyDesk INTO @currentElementId, @currentElementName, @currentElementParentId, @currentElementTargetUrl, @currentElementIconPath, @currentElementLevel, @currentElementOrder;
END
CLOSE @cursorMyDesk;
DEALLOCATE @cursorMyDesk;

------------------------------------------
---   Update ElementChildCount   ---------
------------------------------------------
SET @cursorMyDesk = CURSOR FOR
	SELECT ElementID
	FROM @defaultUIElementsTable AS DefaultElTable
		INNER JOIN CMS_UIElement ON DefaultElTable.ElementName = CMS_UIElement.ElementName
	WHERE
		CMS_UIElement.ElementResourceID = @myDeskResourceId AND
		CMS_UIElement.ElementID <> @myDeskElementId; -- exclude MyDesk root UIElement (cmsmydesk)
		
OPEN @cursorMyDesk
FETCH NEXT FROM @cursorMyDesk INTO @currentElementId
WHILE @@FETCH_STATUS = 0
BEGIN		
	-- Update ElementChildCount
	UPDATE CMS_UIElement SET
		ElementChildCount = (SELECT COUNT(*) FROM CMS_UIElement WHERE ElementParentID = @currentElementId)
	WHERE
		ElementID = @currentElementId;

	FETCH NEXT FROM @cursorMyDesk INTO @currentElementId;
END
CLOSE @cursorMyDesk;
DEALLOCATE @cursorMyDesk;

-----------------------------------------------
---   Update MyDesk category permissions   ----
-----------------------------------------------
DECLARE @currentElementRoleId int;
DECLARE @cursorRoleIdElementId CURSOR;

-- Get all elements that are a second level element
SET @cursorMyDesk = CURSOR FOR
	SELECT ElementID
	FROM CMS_UIElement
	WHERE
		ElementResourceID = @myDeskResourceId AND
		ElementLevel = 2
	ORDER BY ElementLevel DESC;
		
OPEN @cursorMyDesk
FETCH NEXT FROM @cursorMyDesk INTO @currentElementId
WHILE @@FETCH_STATUS = 0
BEGIN	
	-- Get all roles for the element, get ElementParentID
	SET @cursorRoleIdElementId = CURSOR FOR
		SELECT CMS_UIElement.ElementParentID, CMS_RoleUIElement.RoleID
		FROM CMS_RoleUIElement
		INNER JOIN CMS_Role ON CMS_RoleUIElement.RoleID = CMS_Role.RoleID
		INNER JOIN CMS_UIElement ON CMS_RoleUIElement.ElementID = CMS_UIElement.ElementID
		WHERE
			CMS_RoleUIElement.ElementID = @currentElementId AND
			CMS_Role.SiteID = -- include this siteId condition only if siteId > 0
				(CASE WHEN @siteId > 0
					THEN @siteId
					ELSE CMS_Role.SiteID
				END);

	OPEN @cursorRoleIdElementId
	FETCH NEXT FROM @cursorRoleIdElementId INTO @currentElementId, @currentElementRoleId;
	WHILE @@FETCH_STATUS = 0
	BEGIN
		-- insert a new permission (same as the child's one) for the parent element
		IF NOT EXISTS (SELECT ElementID FROM CMS_RoleUIElement WHERE ElementID = @currentElementId AND RoleID = @currentElementRoleId)
		BEGIN
			INSERT INTO CMS_RoleUIElement (ElementID, RoleID) VALUES ( @currentElementId, @currentElementRoleId);
		END
		FETCH NEXT FROM @cursorRoleIdElementId INTO @currentElementId, @currentElementRoleId;
	END
	CLOSE @cursorRoleIdElementId;
	DEALLOCATE @cursorRoleIdElementId;	
	
	FETCH NEXT FROM @cursorMyDesk INTO @currentElementId;
END
CLOSE @cursorMyDesk;
DEALLOCATE @cursorMyDesk;

---------------------------------------------------
-- END: Convert MyDesk UIElements  ----------------
---------------------------------------------------
