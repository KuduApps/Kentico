DECLARE @siteId INT; SET @siteId = ##SITEID##;

DECLARE @tempTable TABLE (
	ModuleName VARCHAR(50) NOT NULL,
    PermissionName VARCHAR(50) NOT NULL,
    ElemModuleName VARCHAR(50) NOT NULL,
    ElemName VARCHAR(50) NOT NULL,
    AllRoles BIT
);

DECLARE @tempResourceElement TABLE (
	  ResourceID INT NOT NULL,
    ElementID INT NOT NULL
);

-- Insert data into @tempResourceElement table from import 

##PARAMETER##

-- Migrate permissions of CMS.UserInterface module to UI elements
DECLARE @key NVARCHAR(50); 
DECLARE @allroles BIT; SET @allroles = 1;

SET @key = (SELECT TOP 1 KeyValue FROM CMS_SettingsKey WHERE KeyName = 'CMSPersonalizeUserInterface' AND SiteID = @siteId);
IF @key IS NULL BEGIN
	SET @key = (SELECT TOP 1 KeyValue FROM CMS_SettingsKey WHERE KeyName = 'CMSPersonalizeUserInterface' AND SiteID IS NULL);
END

IF (@key = 'true') OR (@key = '1') BEGIN
	SET @allroles = 0;
END

INSERT INTO @tempTable VALUES ('CMS.UserInterface', 'PropertiesTab', 'CMS.Content', 'Properties', @allroles);
INSERT INTO @tempTable VALUES ('CMS.UserInterface', 'PropertiesGeneral', 'CMS.Content', 'Properties.General', @allroles);
INSERT INTO @tempTable VALUES ('CMS.UserInterface', 'PropertiesGeneral', 'CMS.Content', 'General.Design', @allroles);
INSERT INTO @tempTable VALUES ('CMS.UserInterface', 'PropertiesGeneral', 'CMS.Content', 'General.OtherProperties', @allroles);
INSERT INTO @tempTable VALUES ('CMS.UserInterface', 'PropertiesGeneral', 'CMS.Content', 'General.Owner', @allroles);
INSERT INTO @tempTable VALUES ('CMS.UserInterface', 'PropertiesGeneral', 'CMS.Content', 'General.Cache', @allroles);
INSERT INTO @tempTable VALUES ('CMS.UserInterface', 'PropertiesGeneral', 'CMS.Content', 'General.Advanced', @allroles);
INSERT INTO @tempTable VALUES ('CMS.UserInterface', 'PropertiesURLs', 'CMS.Content', 'Properties.URLs', @allroles);
INSERT INTO @tempTable VALUES ('CMS.UserInterface', 'PropertiesURLs', 'CMS.Content', 'URLs.Path', @allroles);
INSERT INTO @tempTable VALUES ('CMS.UserInterface', 'PropertiesURLs', 'CMS.Content', 'URLs.ExtendedProperties', @allroles);
INSERT INTO @tempTable VALUES ('CMS.UserInterface', 'PropertiesURLs', 'CMS.Content', 'URLs.Aliases', @allroles);
INSERT INTO @tempTable VALUES ('CMS.UserInterface', 'PropertiesTemplate', 'CMS.Content', 'Properties.Template', @allroles);
INSERT INTO @tempTable VALUES ('CMS.UserInterface', 'PropertiesTemplate', 'CMS.Content', 'Template.SaveAsNew', @allroles);
INSERT INTO @tempTable VALUES ('CMS.UserInterface', 'PropertiesTemplate', 'CMS.Content', 'Template.Inherit', @allroles);
INSERT INTO @tempTable VALUES ('CMS.UserInterface', 'PropertiesTemplate', 'CMS.Content', 'Template.CloneAdHoc', @allroles);
INSERT INTO @tempTable VALUES ('CMS.UserInterface', 'PropertiesTemplate', 'CMS.Content', 'Template.EditProperties', @allroles);
INSERT INTO @tempTable VALUES ('CMS.UserInterface', 'PropertiesTemplate', 'CMS.Content', 'Template.InheritContent', @allroles);
INSERT INTO @tempTable VALUES ('CMS.UserInterface', 'PropertiesTemplate', 'CMS.Content', 'Template.ModifySharedTemplates', @allroles);
INSERT INTO @tempTable VALUES ('CMS.UserInterface', 'PropertiesMetadata', 'CMS.Content', 'Properties.Metadata', @allroles);
INSERT INTO @tempTable VALUES ('CMS.UserInterface', 'PropertiesMetadata', 'CMS.Content', 'Metadata.Page', @allroles);
INSERT INTO @tempTable VALUES ('CMS.UserInterface', 'PropertiesMetadata', 'CMS.Content', 'Metadata.Tags', @allroles);
INSERT INTO @tempTable VALUES ('CMS.UserInterface', 'PropertiesCategories', 'CMS.Content', 'Properties.Categories', @allroles);
INSERT INTO @tempTable VALUES ('CMS.UserInterface', 'PropertiesMenu', 'CMS.Content', 'Properties.Menu', @allroles);
INSERT INTO @tempTable VALUES ('CMS.UserInterface', 'PropertiesMenu', 'CMS.Content', 'Menu.BasicProperties', @allroles);
INSERT INTO @tempTable VALUES ('CMS.UserInterface', 'PropertiesMenu', 'CMS.Content', 'Menu.Actions', @allroles);
INSERT INTO @tempTable VALUES ('CMS.UserInterface', 'PropertiesMenu', 'CMS.Content', 'Menu.Design', @allroles);
INSERT INTO @tempTable VALUES ('CMS.UserInterface', 'PropertiesWorkflow', 'CMS.Content', 'Properties.Workflow', @allroles);
INSERT INTO @tempTable VALUES ('CMS.UserInterface', 'PropertiesVersions', 'CMS.Content', 'Properties.Versions', @allroles);
INSERT INTO @tempTable VALUES ('CMS.UserInterface', 'PropertiesRelatedDocs', 'CMS.Content', 'Properties.RelatedDocs', @allroles);
INSERT INTO @tempTable VALUES ('CMS.UserInterface', 'PropertiesLinkedDocs', 'CMS.Content', 'Properties.LinkedDocs', @allroles);
INSERT INTO @tempTable VALUES ('CMS.UserInterface', 'PropertiesSecurity', 'CMS.Content', 'Properties.Security', @allroles);
INSERT INTO @tempTable VALUES ('CMS.UserInterface', 'PropertiesSecurity', 'CMS.Content', 'Security.Permissions', @allroles);
INSERT INTO @tempTable VALUES ('CMS.UserInterface', 'PropertiesSecurity', 'CMS.Content', 'Security.Authentication', @allroles);
INSERT INTO @tempTable VALUES ('CMS.UserInterface', 'PropertiesSecurity', 'CMS.Content', 'Security.SSL', @allroles);
INSERT INTO @tempTable VALUES ('CMS.UserInterface', 'PropertiesAttachments', 'CMS.Content', 'Properties.Attachments', @allroles);
INSERT INTO @tempTable VALUES ('CMS.UserInterface', 'PropertiesLanguages', 'CMS.Content', 'Properties.Languages', @allroles);


-- Migrate other module permissions to UI Elements

INSERT INTO @tempTable VALUES ('CMS.Content', 'ExploreTree', 'CMS.Desk', 'Content', 0);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.Desk', 'MyDesk', 1);
INSERT INTO @tempTable VALUES ('CMS.Tools', 'Open', 'CMS.Desk', 'Tools', 0);
INSERT INTO @tempTable VALUES ('CMS.Administration', 'Read', 'CMS.Desk', 'Administration', 0);
INSERT INTO @tempTable VALUES ('CMS.Content', 'Read', 'CMS.Content', 'Page', 0);
INSERT INTO @tempTable VALUES ('CMS.Content', 'Design', 'CMS.Content', 'Design', 0);
INSERT INTO @tempTable VALUES ('CMS.Content', 'Design', 'CMS.Content', 'Design.EditLayout', 0);
INSERT INTO @tempTable VALUES ('CMS.Content', 'Design', 'CMS.Content', 'Design.EditTemplateProperties', 0);
INSERT INTO @tempTable VALUES ('CMS.Content', 'Design', 'CMS.Content', 'Design.CloneAdHoc', 0);
INSERT INTO @tempTable VALUES ('CMS.Content', 'Design', 'CMS.Content', 'Design.ModifySharedTemplates', 0);
INSERT INTO @tempTable VALUES ('CMS.Content', 'Design', 'CMS.Content', 'Design.WebPartZoneProperties', 0);
INSERT INTO @tempTable VALUES ('CMS.Content', 'Design', 'CMS.Content', 'Design.WebPartProperties', 0);
INSERT INTO @tempTable VALUES ('CMS.Content', 'Design', 'CMS.Content', 'Design.AddWebParts', 0);
INSERT INTO @tempTable VALUES ('CMS.Content', 'Design', 'CMS.Content', 'Design.RemoveWebParts', 0);
INSERT INTO @tempTable VALUES ('CMS.Content', 'Design', 'CMS.Content', 'WebPartProperties.Code', 0);
INSERT INTO @tempTable VALUES ('CMS.Content', 'Design', 'CMS.Content', 'WebPartProperties.Bindings', 0);
INSERT INTO @tempTable VALUES ('CMS.Content', 'Design', 'CMS.Content', 'WebPartProperties.General', 0);
INSERT INTO @tempTable VALUES ('CMS.Content', 'Design', 'CMS.Content', 'WebPartProperties.Layout', 0);
INSERT INTO @tempTable VALUES ('CMS.Content', 'Design', 'CMS.Content', 'WebPartProperties.EditTransformations', 0);
INSERT INTO @tempTable VALUES ('CMS.Content', 'Read',   'CMS.Content', 'Form', 0);
INSERT INTO @tempTable VALUES ('CMS.Content', 'Design', 'CMS.Content', 'MasterPage', 0);
INSERT INTO @tempTable VALUES ('CMS.Ecommerce', 'Display', 'CMS.Content', 'Product', 0);

INSERT INTO @tempTable VALUES ('-', '-', 'CMS.MyDesk', 'RecentDocs', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.MyDesk', 'OutdatedDocs', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.MyDesk', 'WaitingDocs', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.MyDesk', 'CheckedOutDocs', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.MyDesk', 'MyRecycleBin', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.MyDesk', 'MyDocuments', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.MyDesk', 'MyProfile', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.MyDesk', 'MyProfile.Details', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.MyDesk', 'MyProfile.ChangePassword', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.MyDesk', 'MyProfile.Notifications', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.MyDesk', 'MyProfile.Subscriptions', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.MyDesk', 'MyBlogs', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.MyDesk', 'MyMessages', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.MyDesk', 'MyFriends', 1);

INSERT INTO @tempTable VALUES ('-', '-', 'CMS.MediaDialog', 'AttachmentsTab', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.MediaDialog', 'ContentTab', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.MediaDialog', 'MediaLibrariesTab', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.MediaDialog', 'WebTab', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.MediaDialog', 'AnchorTab', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.MediaDialog', 'EmailTab', 1);

INSERT INTO @tempTable VALUES ('CMS.AbuseReport', 'Display', 'CMS.Tools', 'AbuseReport', 0);
INSERT INTO @tempTable VALUES ('CMS.Form', 'Display', 'CMS.Tools', 'Form', 0);
INSERT INTO @tempTable VALUES ('CMS.Blog', 'Display', 'CMS.Tools', 'Blog', 0);
INSERT INTO @tempTable VALUES ('CMS.EventManager', 'Display', 'CMS.Tools', 'EventManager', 0);
INSERT INTO @tempTable VALUES ('CMS.Staging', 'Display', 'CMS.Tools', 'Staging', 0);
INSERT INTO @tempTable VALUES ('CMS.CustomTables', 'Display', 'CMS.Tools', 'CustomTables', 0);
INSERT INTO @tempTable VALUES ('CMS.Ecommerce', 'Display', 'CMS.Tools', 'Ecommerce', 0);
INSERT INTO @tempTable VALUES ('CMS.FileImport', 'Display', 'CMS.Tools', 'FileImport', 0);
INSERT INTO @tempTable VALUES ('CMS.Forums', 'Display', 'CMS.Tools', 'Forums', 0);
INSERT INTO @tempTable VALUES ('CMS.Groups', 'Display', 'CMS.Tools', 'Groups', 0);
INSERT INTO @tempTable VALUES ('CMS.MediaLibrary', 'Display', 'CMS.Tools', 'MediaLibrary', 0);
INSERT INTO @tempTable VALUES ('CMS.MessageBoards', 'Display', 'CMS.Tools', 'MessageBoards', 0);
INSERT INTO @tempTable VALUES ('CMS.Newsletter', 'Display', 'CMS.Tools', 'Newsletter', 0);
INSERT INTO @tempTable VALUES ('CMS.Polls', 'Display', 'CMS.Tools', 'Polls', 0);
INSERT INTO @tempTable VALUES ('CMS.Reporting', 'Display', 'CMS.Tools', 'Reporting', 0);
INSERT INTO @tempTable VALUES ('CMS.WebAnalytics', 'Display', 'CMS.Tools', 'WebAnalytics', 0);

INSERT INTO @tempTable VALUES ('CMS.BannedIP', 'Display', 'CMS.Administration', 'BannedIP', 0);
INSERT INTO @tempTable VALUES ('CMS.EventLog', 'Display', 'CMS.Administration', 'EventLog', 0);
INSERT INTO @tempTable VALUES ('CMS.Permissions', 'Display', 'CMS.Administration', 'Permissions', 0);
INSERT INTO @tempTable VALUES ('CMS.Roles', 'Display', 'CMS.Administration', 'Roles', 0);
INSERT INTO @tempTable VALUES ('CMS.ScheduledTasks', 'Display', 'CMS.Administration', 'ScheduledTasks', 0);
INSERT INTO @tempTable VALUES ('CMS.Administration', 'Read', 'CMS.Administration', 'UIPersonalization', 0);
INSERT INTO @tempTable VALUES ('CMS.Users', 'Display', 'CMS.Administration', 'Users', 0);

INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'Group.ViewModes', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'Source', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'Preview', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'Group.Clipboard', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'Cut', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'Copy', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'Paste', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'PasteText', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'PasteWord', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'Print', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'Group.SearchHistory', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'Undo', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'Redo', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'Find', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'Replace', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'SelectAll', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'RemoveFormat', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'Group.Formatting', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'Bold', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'Italic', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'Underline', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'StrikeThrough', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'Subscript', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'Superscript', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'Group.ListsIndents', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'OrderedList', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'UnorderedList', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'Outdent', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'Indent', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'Group.Justify', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'JustifyLeft', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'JustifyCenter', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'JustifyRight', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'JustifyFull', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'Group.Links', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'InsertLink', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'Unlink', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'Anchor', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'Group.ImagesMedia', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'InsertImageOrMedia', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'QuicklyInsertImage', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'InsertYouTubeVideo', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'Group.HTMLElements', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'Table', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'Rule', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'Smiley', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'SpecialChar', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'PageBreak', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'Group.Components', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'InsertPolls', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'InsertRating', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'InsertBizForms', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'InsertInlineControls', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'Group.Style', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'Style', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'FontName', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'FontSize', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'FontFormat', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'Group.Colors', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'TextColor', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'BGColor', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'Group.Display', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'FitWindow', 1);
INSERT INTO @tempTable VALUES ('-', '-', 'CMS.WYSIWYGEditor', 'ShowBlocks', 1);


DECLARE @cursorTemp CURSOR;
SET @cursorTemp = CURSOR FOR SELECT * FROM @tempTable

DECLARE @resourceName VARCHAR(50);
DECLARE @permissionName VARCHAR(50);
DECLARE @elementResourceName VARCHAR(50);
DECLARE @elementName VARCHAR(50);
DECLARE @forAllRoles BIT;

OPEN @cursorTemp
FETCH NEXT FROM @cursorTemp INTO @resourceName, @permissionName, @elementResourceName, @elementName, @forAllRoles;
WHILE @@FETCH_STATUS = 0
BEGIN
    
    /* Get the element ID */
	DECLARE @elementID INT; SET  @elementID = (SELECT TOP 1 ElementID FROM CMS_UIElement WHERE ElementName = @elementName AND ElementResourceID IN (SELECT ResourceID FROM CMS_Resource WHERE ResourceName = @elementResourceName))

    IF @elementID IS NOT NULL BEGIN
		/* Declare the selection table */
		DECLARE @permissionTable TABLE (
			RoleID INT NOT NULL
		);
			
		/* Get the permissions */
		DELETE FROM @permissionTable;
		IF @forAllRoles = 0 BEGIN
			INSERT INTO @permissionTable SELECT DISTINCT RoleID FROM CMS_RolePermission 
				 WHERE PermissionID IN 
					  (SELECT PermissionID FROM CMS_Permission 
					   WHERE PermissionName = @permissionName AND ResourceID IN 
						  (SELECT ResourceID FROM CMS_Resource WHERE ResourceName = @resourceName)) 
				 AND RoleID IN (SELECT RoleID FROM CMS_Role WHERE SiteID = @siteId)
					
		END ELSE BEGIN
			INSERT INTO @permissionTable SELECT RoleID FROM CMS_Role 
			    WHERE RoleName NOT IN ('CMSLiveIDUsers', '_everyone_', '_authenticated_', '_notauthenticated_') 
			    AND RoleID IN (SELECT RoleID FROM CMS_Role WHERE SiteID = @siteId)
		END
			
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

    FETCH NEXT FROM @cursorTemp INTO @resourceName, @permissionName, @elementResourceName, @elementName, @forAllRoles;
END
CLOSE @cursorTemp;
DEALLOCATE @cursorTemp;

-- Process @tempResourceElement table

DECLARE @cursorResourceElement CURSOR;
SET @cursorResourceElement = CURSOR FOR SELECT * FROM @tempResourceElement

DECLARE @resourceId INT;
DECLARE @uiElementId INT;

OPEN @cursorResourceElement
FETCH NEXT FROM @cursorResourceElement INTO @resourceId, @uiElementId;
WHILE @@FETCH_STATUS = 0
BEGIN

	/* Declare the selection table */
	DECLARE @displayPermissionTable TABLE (
		RoleID INT NOT NULL
	);
	DELETE FROM @displayPermissionTable;	
  
  DECLARE @hasDisplay INT; 
  SET  @hasDisplay = (SELECT TOP 1 PermissionID FROM CMS_Permission 
			    WHERE PermissionName = 'Display' AND ResourceID = @resourceId);

  IF @hasDisplay IS NOT NULL BEGIN
  	/* Get the roles for 'Display' permission */
  	INSERT INTO @displayPermissionTable SELECT RoleID FROM CMS_RolePermission 
  			    WHERE PermissionID IN (SELECT PermissionID FROM CMS_Permission 
  			    WHERE PermissionName = 'Display' AND ResourceID = @resourceId);
			    
	END ELSE BEGIN
  	/* Get the all roles */
  	INSERT INTO @displayPermissionTable SELECT RoleID FROM CMS_Role 
  			    WHERE RoleName NOT IN ('CMSLiveIDUsers', '_everyone_', '_authenticated_', '_notauthenticated_');
	END
	
	/* Declare the cursor to loop through the table */
	DECLARE @cursorRole CURSOR;
	SET @cursorRole = CURSOR FOR SELECT RoleID FROM @displayPermissionTable

	DECLARE @currentDisplayRoleID int;

	/* Loop through the table */
	OPEN @cursorRole
	FETCH NEXT FROM @cursorRole INTO @currentDisplayRoleID;
	WHILE @@FETCH_STATUS = 0
	BEGIN
	    IF NOT EXISTS (SELECT 1 FROM CMS_RoleUIElement WHERE RoleID = @currentDisplayRoleID AND ElementID = @uiElementId) BEGIN
		    INSERT INTO CMS_RoleUIElement (RoleID, ElementID) VALUES (@currentDisplayRoleID, @uiElementId);
		END
		FETCH NEXT FROM @cursorRole INTO @currentDisplayRoleID;
	END
	CLOSE @cursorRole;
	DEALLOCATE @cursorRole;	    
	
	FETCH NEXT FROM @cursorResourceElement INTO @resourceId, @uiElementId;
END
CLOSE @cursorResourceElement;
DEALLOCATE @cursorResourceElement; 


-- Remove Display permissions from all cms modules and all its dependencies
DELETE FROM Forums_ForumRoles				WHERE PermissionID IN (SELECT PermissionID FROM CMS_Permission WHERE ResourceID IN (SELECT ResourceID FROM CMS_Resource WHERE ResourceName LIKE 'cms.%') AND PermissionName = 'display')
DELETE FROM CMS_RolePermission				WHERE PermissionID IN (SELECT PermissionID FROM CMS_Permission WHERE ResourceID IN (SELECT ResourceID FROM CMS_Resource WHERE ResourceName LIKE 'cms.%') AND PermissionName = 'display')
DELETE FROM Community_GroupRolePermission	WHERE PermissionID IN (SELECT PermissionID FROM CMS_Permission WHERE ResourceID IN (SELECT ResourceID FROM CMS_Resource WHERE ResourceName LIKE 'cms.%') AND PermissionName = 'display')
DELETE FROM Media_LibraryRolePermission		WHERE PermissionID IN (SELECT PermissionID FROM CMS_Permission WHERE ResourceID IN (SELECT ResourceID FROM CMS_Resource WHERE ResourceName LIKE 'cms.%') AND PermissionName = 'display')
DELETE FROM CMS_WidgetRole					WHERE PermissionID IN (SELECT PermissionID FROM CMS_Permission WHERE ResourceID IN (SELECT ResourceID FROM CMS_Resource WHERE ResourceName LIKE 'cms.%') AND PermissionName = 'display')
DELETE FROM CMS_Permission					WHERE PermissionID IN (SELECT PermissionID FROM CMS_Permission WHERE ResourceID IN (SELECT ResourceID FROM CMS_Resource WHERE ResourceName LIKE 'cms.%') AND PermissionName = 'display')


-- Remove Open permission from Tools module and all its dependencies
DECLARE @permissionId INT;

SET @permissionId = (SELECT PermissionID FROM CMS_Permission WHERE ResourceID IN (SELECT ResourceID FROM CMS_Resource WHERE ResourceName = 'cms.tools') AND PermissionName = 'open'); 
IF @permissionId IS NOT NULL BEGIN
	EXEC Proc_CMS_Permission_RemoveDependencies @permissionId;
	DELETE FROM CMS_Permission WHERE PermissionID = @permissionId;
END


-- Remove CMS.UserInterface module and all its dependencies
SET @resourceId = (SELECT ResourceID FROM CMS_Resource WHERE ResourceName = 'cms.userinterface');
IF @resourceId IS NOT NULL BEGIN
	EXEC Proc_CMS_Resource_RemoveDependencies @resourceId;
	DELETE FROM CMS_Resource WHERE ResourceID = @resourceId;
END

-- Change category of the CMSPersonalizeUserInterface key and change its description
DECLARE @securtiyCat INT; SET @securtiyCat = (SELECT TOP 1 CategoryID FROM CMS_SettingsCategory WHERE CategoryName = 'CMS.Security');
UPDATE CMS_SettingsKey SET KeyCategoryID = @securtiyCat, KeyOrder = 11, KeyDisplayName = 'Enable UI personalization', KeyDescription = 'Indicates if UI personalization is enabled: If checked, user can see only such UI elements which are allowed according to his UI profile. If unchecked, user can see all UI elements.' WHERE KeyName = 'CMSPersonalizeUserInterface';
EXEC Proc_CMS_SettingsKey_InitKeyOrders @KeyName = 'CMSPersonalizeUserInterface';
