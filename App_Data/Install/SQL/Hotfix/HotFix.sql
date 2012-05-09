/********************* INSERT RESOURCE STRINGS ***********************/
DECLARE @stringId AS int;
DECLARE @cultureId AS int;
DECLARE @cultureCode AS nvarchar(50);


-- Change value of this parameter if your default UI culture is not 'en-US' (see details in hotfix instructions)
SET @cultureCode = 'en-US';

-- Get culture ID
SET @cultureID = (SELECT UICultureID FROM [CMS_UICulture] WHERE UICultureCode=@cultureCode);
IF  @cultureID IS NULL
BEGIN
  SET @cultureID = (SELECT UICultureID FROM [CMS_UICulture] WHERE UICultureCode='en-US');
END

-- Integration multiple actions - all tasks dropdown option
SET @stringId = (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID WHERE StringKey = N'integration.alltasks' AND TranslationUICultureID = @cultureID);
IF  @stringId IS NULL
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('integration.alltasks'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'All tasks')
END

-- Integration multiple actions - selected tasks dropdown option
SET @stringId = (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID WHERE StringKey = N'integration.selectedtasks' AND TranslationUICultureID = @cultureID);
IF  @stringId IS NULL
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('integration.selectedtasks'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'Selected tasks')
END

-- Integration multiple actions - skipping task due to unavailable connector
SET @stringId = (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID WHERE StringKey = N'synchronization.skippingunavailable' AND TranslationUICultureID = @cultureID);
IF  @stringId IS NULL
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('synchronization.skippingunavailable'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'Skipping ''{0}'' task - failed to load associated connector.')
END

-- Integration multiple actions - skipping task due to disabled connector
SET @stringId = (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID WHERE StringKey = N'synchronization.skippingdisabled' AND TranslationUICultureID = @cultureID);
IF  @stringId IS NULL
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('synchronization.skippingdisabled'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'Skipping ''{0}'' task - associated connector is disabled.')
END

-- Integration multiple actions - no tasks selected
SET @stringId = (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID WHERE StringKey = N'synchronization.selecttasks' AND TranslationUICultureID = @cultureID);
IF  @stringId IS NULL
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('synchronization.selecttasks'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'You have to select some tasks first.')
END

-- Activity - blog
SET @stringId = (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID WHERE StringKey = N'activityitemdetail.blogcomment' AND TranslationUICultureID = @cultureID);
IF  @stringId IS NULL
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('activityitemdetail.blogcomment'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'Blog')
END

-- Activity - forums
SET @stringId = (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID WHERE StringKey = N'om.activitydetails.forumpostsubject' AND TranslationUICultureID = @cultureID);
IF  @stringId IS NULL
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('om.activitydetails.forumpostsubject'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'Post subject')
END

-- Activity - new settings for file extensions
SET @stringId = (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID WHERE StringKey = N'settingskey.cmsactivitytrackedextensions' AND TranslationUICultureID = @cultureID);
IF  @stringId IS NULL
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('settingskey.cmsactivitytrackedextensions'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'Track file downloads (cms.file) for these extensions')
END

-- Activity - new settings for file extensions (description)
SET @stringId = (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID WHERE StringKey = N'settingskey.cmsactivitytrackedextensions.description' AND TranslationUICultureID = @cultureID);
IF  @stringId IS NULL
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('settingskey.cmsactivitytrackedextensions.description'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'File types (extensions) that will be tracked as activity (<strong>separated by semicolon without dot</strong>). <strong>Please note:</strong> If no value is specified, all types of files will be tracked.')
END

-- MVT tests
SET @stringId = (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID WHERE StringKey = N'analytics_codename.mvtest' AND TranslationUICultureID = @cultureID);
IF  @stringId IS NULL
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('analytics_codename.mvtest'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'MVT tests')
END

-- Dashborad no template
SET @stringId = (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID WHERE StringKey = N'dashboard.notemplate' AND TranslationUICultureID = @cultureID);
IF  @stringId IS NULL
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('dashboard.notemplate'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'Page template for this dashboard was not found.')
END

-- Conversion not on current site
SET @stringId = (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID WHERE StringKey = N'conversion.currentsite' AND TranslationUICultureID = @cultureID);
IF  @stringId IS NULL
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('conversion.currentsite'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'Conversion does not belong to current site.')
END


-- Tools - web analytics - visitors
SET @stringId = (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID WHERE StringKey = N'tools.ui.webanalyticsviewvisits' AND TranslationUICultureID = @cultureID);
IF  @stringId IS NULL
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('tools.ui.webanalyticsviewvisits'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'View statistics of visitors')
END

-- Hier. footer
SET @stringId = (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID WHERE StringKey = N'hiertransf.footer' AND TranslationUICultureID = @cultureID);
IF  @stringId IS NULL
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('hiertransf.footer'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'Footer transformation')
END


-- Hier. footer
SET @stringId = (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID WHERE StringKey = N'hiertransf.header' AND TranslationUICultureID = @cultureID);
IF  @stringId IS NULL
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('hiertransf.header'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'Header transformation')
END

-- Check if resource string exists
SET @stringId = (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID WHERE StringKey = N'activityitemdetail.blogpostsubscription' AND TranslationUICultureID = @cultureID);
IF  @stringId IS NULL
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('activityitemdetail.blogpostsubscription'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'Blog')
END

-- Check if resource string exists
SET @stringId = (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID WHERE StringKey = N'objecttype.om_membershipuser' AND TranslationUICultureID = @cultureID);
IF  @stringId IS NULL
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('objecttype.om_membershipuser'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'User membership')
END

-- Check if resource string exists
SET @stringId = (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID WHERE StringKey = N'validation.link.resultinvalidwarning' AND TranslationUICultureID = @cultureID);
IF  @stringId IS NULL
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('validation.link.resultinvalidwarning'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'The following links may require your attention.')
END

-- Check if resource string exists
SET @stringId = (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID WHERE StringKey = N'analyt.settings.deleterinprogress' AND TranslationUICultureID = @cultureID);
IF  @stringId IS NULL
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('analyt.settings.deleterinprogress'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'Deletion of data is in progress.')
END

-- Check if resource string exists
SET @stringId = (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID WHERE StringKey = N'importobjects.selectallconfirm' AND TranslationUICultureID = @cultureID);
IF  @stringId IS NULL
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('importobjects.selectallconfirm'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'Are you sure you want to select all objects? Definitions of objects, such as Inline controls, Form controls, Web parts, Widgets and others, as well as files belonging to these objects, will be overwritten. You can lose your custom changes made to these objects.')
END

-- Custom activities - select site
SET @stringId = (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID WHERE StringKey = N'om.choosesite' AND TranslationUICultureID = @cultureID);
IF  @stringId IS NULL
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('om.choosesite'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'Please choose a particular site to create a new item.')
END

-- Custom activities - create a custom activity
SET @stringId = (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID WHERE StringKey = N'om.activities.nocustomactivity' AND TranslationUICultureID = @cultureID);
IF  @stringId IS NULL
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('om.activities.nocustomactivity'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'Please create a custom activity type to create a new item.')
END


-- Online marketing - settings key translation
IF (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID
 WHERE StringKey = N'settingskey.cmsdeleteinactivecontacts' AND TranslationUICultureID = @cultureID) IS NULL
   
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('settingskey.cmsdeleteinactivecontacts'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'Delete inactive contacts')
END


-- Online marketing - settings key translation
IF (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID
 WHERE StringKey = N'settingskey.cmsdeleteinactivecontacts.description' AND TranslationUICultureID = @cultureID) IS NULL
   
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('settingskey.cmsdeleteinactivecontacts.description'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'Indicates if old contacts from current site can be deleted automatically using scheduled task.')
END

IF (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID
 WHERE StringKey = N'settingskey.cmslastactivityolderthan' AND TranslationUICultureID = @cultureID) IS NULL
   
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('settingskey.cmslastactivityolderthan'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'Last activity older than (days)')
END


IF (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID
 WHERE StringKey = N'settingskey.lastactivitydescription' AND TranslationUICultureID = @cultureID) IS NULL
   
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('settingskey.lastactivitydescription'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'Contacts whose activities will be older than specified number of days will be deleted. Value represents number of days.')
END

IF (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID
 WHERE StringKey = N'settingskey.contactcreatedbefore' AND TranslationUICultureID = @cultureID) IS NULL
   
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('settingskey.contactcreatedbefore'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'Contact created before (days)')
END

IF (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID
 WHERE StringKey = N'settingskey.contactcreatedbefore.description' AND TranslationUICultureID = @cultureID) IS NULL
   
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('settingskey.contactcreatedbefore.description'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'Contacts which were created before specified number of days will be deleted. Value represents number of days.')
END

IF (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID
 WHERE StringKey = N'settingskey.contactlastlogon' AND TranslationUICultureID = @cultureID) IS NULL
   
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('settingskey.contactlastlogon'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'Contact last logon before (days)')
END

IF (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID
 WHERE StringKey = N'settingskey.contactlastlogon.description' AND TranslationUICultureID = @cultureID) IS NULL
   
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('settingskey.contactlastlogon.description'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'Applicable to non anonymous contacts with relation to a user. Contacts whose last logon is older than specified number of days will be deleted. Value represents number of days.')
END

IF (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID
 WHERE StringKey = N'settingskey.contactlastmodified' AND TranslationUICultureID = @cultureID) IS NULL
   
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('settingskey.contactlastmodified'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'Contact last modified before (days)')
END

IF (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID
 WHERE StringKey = N'settingskey.contactlastmodified.description' AND TranslationUICultureID = @cultureID) IS NULL
   
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('settingskey.contactlastmodified.description'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'Contacts which were modified (e.g. contact address changed) before specified number of days will be deleted. Value represents number of days.')
END

IF (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID
 WHERE StringKey = N'settingskey.contactmergedwhen' AND TranslationUICultureID = @cultureID) IS NULL
   
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('settingskey.contactmergedwhen'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'Contact merged before (days)')
END

IF (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID
 WHERE StringKey = N'settingskey.contactmergedwhen.description' AND TranslationUICultureID = @cultureID) IS NULL
   
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('settingskey.contactmergedwhen.description'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'Contacts which were merged before specified number of days will be deleted. Value represents number of days.')
END

IF (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID
 WHERE StringKey = N'settingskey.contactmergedintosite' AND TranslationUICultureID = @cultureID) IS NULL
   
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('settingskey.contactmergedintosite'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'Merged into site contact only')
END

IF (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID
 WHERE StringKey = N'settingskey.contactmergedintosite.description' AND TranslationUICultureID = @cultureID) IS NULL
   
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('settingskey.contactmergedintosite.description'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'Only contacts which are merged into another site contact will be deleted.')
END

IF (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID
 WHERE StringKey = N'settingskey.mergedintoglobal' AND TranslationUICultureID = @cultureID) IS NULL
   
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('settingskey.mergedintoglobal'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'Merged into global contact only')
END

IF (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID
 WHERE StringKey = N'settingskey.mergedintoglobal.description' AND TranslationUICultureID = @cultureID) IS NULL
   
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('settingskey.mergedintoglobal.description'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'Only contacts which are merged into global contact will be deleted.')
END



IF (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID
 WHERE StringKey = N'settingskey.contactisanonymous' AND TranslationUICultureID = @cultureID) IS NULL
   
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('settingskey.contactisanonymous'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'Contact is anonymous')
END


IF (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID
 WHERE StringKey = N'settingskey.contactisanonymousdescription' AND TranslationUICultureID = @cultureID) IS NULL
   
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('settingskey.contactisanonymousdescription'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'Contacts which are/aren''t related to a user will be deleted.')
END

IF (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID
 WHERE StringKey = N'om.contact.deleteall' AND TranslationUICultureID = @cultureID) IS NULL
   
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('om.contact.deleteall'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'Delete all')
END

IF (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID
 WHERE StringKey = N'om.contact.onlymerged' AND TranslationUICultureID = @cultureID) IS NULL
   
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('om.contact.onlymerged'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'Delete only merged')
END

IF (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID
 WHERE StringKey = N'om.contact.deletenotmerged' AND TranslationUICultureID = @cultureID) IS NULL
   
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('om.contact.deletenotmerged'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'Delete only not merged')
END

IF (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID
 WHERE StringKey = N'om.contact.doesntmatter' AND TranslationUICultureID = @cultureID) IS NULL
   
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('om.contact.doesntmatter'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'Doesn''t matter')
END

IF (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID
 WHERE StringKey = N'om.contact.notanonymous' AND TranslationUICultureID = @cultureID) IS NULL
   
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('om.contact.notanonymous'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'Is not anonymous')
END

-- Campaign selector no licence error
SET @stringId = (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID WHERE StringKey = N'campaignselector.nolicence' AND TranslationUICultureID = @cultureID);
IF  @stringId IS NULL
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('campaignselector.nolicence'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'You don''t have valid licence for campaigns.')
END


-- Conversion selector no licence error
SET @stringId = (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID WHERE StringKey = N'conversionselector.nolicence' AND TranslationUICultureID = @cultureID);
IF  @stringId IS NULL
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('conversionselector.nolicence'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'You don''t have valid licence for conversions.')
END


-- Conversion selector no licence error
SET @stringId = (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID WHERE StringKey = N'transformationtype.jquery' AND TranslationUICultureID = @cultureID);
IF  @stringId IS NULL
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('transformationtype.jquery'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'jQuery')
END



-- Task already running info
SET @stringId = (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID WHERE StringKey = N'ScheduledTask.TaskAlreadyrunning' AND TranslationUICultureID = @cultureID);
IF  @stringId IS NULL
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('ScheduledTask.TaskAlreadyrunning'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'This task is already running. Repeat the action when it''s finished.')
END


-- Invalid folder name
SET @stringId = (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID WHERE StringKey = N'media.invalidfoldername' AND TranslationUICultureID = @cultureID);
IF  @stringId IS NULL
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('media.invalidfoldername'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'Please enter a valid folder name. A valid folder name does not contain special characters such as "<", ">", ":", """, "/", "\", "|", "?" or "*", is not a reserved device name such as COM1 or LPT1, and is not too long.')
END

-- No silverlight for file import
SET @stringId = (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID WHERE StringKey = N'fileimport.nosilverlight' AND TranslationUICultureID = @cultureID);
IF  @stringId IS NULL
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('fileimport.nosilverlight'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'This feature is available only if Silverlight plug-in is <strong>installed and enabled</strong> in your browser.')
END

-- Not allowed to modify culture version
SET @stringId = (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID WHERE StringKey = N'content.notallowedtomodifycultureversion' AND TranslationUICultureID = @cultureID);
IF  @stringId IS NULL
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('content.notallowedtomodifycultureversion'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'You are not allowed to modify ''{0}'' culture version of the ''{1}'' document.')
END

-- Not monitored
SET @stringId = (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID WHERE StringKey = N'om.contact.isnotmonitored' AND TranslationUICultureID = @cultureID);
IF  @stringId IS NULL
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('om.contact.isnotmonitored'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'Not monitored')
END

-- Number is out of range
SET @stringId = (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID WHERE StringKey = N'general.outofrange' AND TranslationUICultureID = @cultureID);
IF  @stringId IS NULL
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('general.outofrange'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'Value is out of range ({0}..{1})')
END

-- Hotfix object import message
SET @stringId = (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID WHERE StringKey = N'importobjects.warningobjecthotfixversion' AND TranslationUICultureID = @cultureID);
IF  @stringId IS NULL
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('importobjects.warningobjecthotfixversion'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'<strong>Please note: </strong>You are about to import package from older hotfix version. This package includes versions of files for these objects which may not be compatible with the new hotfix version. All items which are in conflict with existing items were automatically deselected. Please select carefully items which you want to be imported.')
END

-- Hotfix import message
SET @stringId = (SELECT StringID FROM [CMS_ResourceString] LEFT JOIN [CMS_ResourceTranslation] ON StringID=TranslationStringID WHERE StringKey = N'importobjects.warninghotfixversion' AND TranslationUICultureID = @cultureID);
IF  @stringId IS NULL
BEGIN
-- Insert resource string
INSERT INTO [CMS_ResourceString]
           ([StringKey]
           ,[StringIsCustom]
           ,[StringLoadGeneration])
     VALUES
           ('importobjects.warninghotfixversion'
           ,0
           ,0)

SET @stringId = scope_identity();

-- Insert translation
INSERT INTO [CMS_ResourceTranslation]
           ([TranslationStringID]
           ,[TranslationUICultureID]
           ,[TranslationText])
     VALUES
           (@stringId
           ,@cultureId
           ,'<strong>Please note: </strong>You are about to import package from older hotfix version. This package may include versions of files for objects <strong>Inline controls</strong>, <strong>Form controls</strong>, <strong>Web parts</strong>, <strong>Widgets</strong> and others, which may not be compatible with the new hotfix version. Please check the objects lists and select carefully the items which you want to be imported. Also make sure that if you want to import the physical files, the import settings <strong>Import files</strong> is selected.')
END

GO
/*********************  END INSERT RESOURCE STRINGS ***********************/

/********************* CHANGE ROLE MEMBERSHIP QUERY ***********************/

DECLARE @HOTFIXVERSION INT;
SET @HOTFIXVERSION =  (SELECT KeyValue FROM [CMS_SettingsKey] WHERE KeyName = N'CMSHotfixVersion')
IF @HOTFIXVERSION  < 5
BEGIN
UPDATE CMS_Query SET QueryText = 
'
SELECT DISTINCT ##TOPN## Y.RoleID,Y.RoleName,Y.SiteID,Y.UserID,Y.ValidTo,Y.RoleGroupID FROM
(
   SELECT DISTINCT  * FROM
   (
     SELECT CMS_Role.RoleID,CMS_Role.RoleName,CMS_Role.SiteID,CMS_UserRole.UserID,CMS_UserRole.ValidTo,CMS_Role.RoleGroupID,''user'' AS ''RoleType'' FROM CMS_Role
     INNER JOIN CMS_UserRole ON CMS_UserRole.RoleID = CMS_Role.RoleID  
     UNION ALL 
     SELECT CMS_Role.RoleID,CMS_Role.RoleName,CMS_Role.SiteID,CMS_MembershipUser.UserID,CMS_MembershipUser.ValidTo,CMS_Role.RoleGroupID,''membership'' AS ''RoleType'' FROM CMS_Role
     INNER JOIN CMS_MembershipRole ON CMS_MembershipRole.RoleID = CMS_Role.RoleID
     INNER JOIN CMS_MembershipUser ON CMS_MembershipUser.MembershipID= CMS_MembershipRole.MembershipID
     UNION ALL
     SELECT CMS_Role.RoleID,CMS_Role.RoleName,CMS_Role.SiteID,@UserID,NULL,CMS_Role.RoleGroupID,''user'' AS ''RoleType'' FROM CMS_Role  WHERE RoleName IN (##GENERICROLES##) 
   ) AS  X 
  WHERE ##WHERE## AND X.UserID = @UserID) 
  AS Y 
 ORDER BY ##ORDERBY##
'
WHERE QueryName = 'SelectMembershipUserRoles' AND ClassID IN (SELECT ClassID From CMS_Class WHERE ClassName ='CMS.User')
END

IF @HOTFIXVERSION < 10
BEGIN
UPDATE CMS_Query SET QueryText = 'SELECT ##TOPN## ##COLUMNS## FROM CMS_DocumentAlias
	LEFT JOIN Analytics_Campaign ON AliasCampaign = CampaignName AND CampaignSiteID = AliasSiteID
	WHERE ##WHERE## ORDER BY ##ORDERBY##'
	
	WHERE 
	QueryName= 'selectall' AND ClassID IN (SELECT ClassID FROM CMS_Class WHERE ClassName = 'CMS.DocumentAlias')
END

/********************* END ROLE MEMBERSHIP QUERY **************************/


/********************** UPDATE FORUM POSTS GRAPH *****************************/

IF @HOTFIXVERSION < 15
BEGIN
UPDATE Reporting_ReportGraph SET GraphQuery = '
SELECT Year(PostTime) AS ''Year'', COUNT(*) as ''Number of posts'' FROM Forums_ForumPost 
WHERE PostForumID IN (SELECT ForumID FROM Forums_Forum WHERE ForumGroupID IN 
(SELECT GroupID FROM Forums_ForumGroup WHERE GroupSiteID = @CMSContextCurrentSiteID)) AND 
{%DatabaseSchema%}.Func_Analytics_DateTrim(DateAdd(YEAR, -1 * @LastXyears, GetDate()),''year'') < PostTime
GROUP BY  Year(PostTime) ORDER BY Year(PostTime)
'
WHERE GraphName = 'ForumPostsByYearGraph' AND GraphReportID IN (SELECT ReportID FROM Reporting_Report WHERE ReportName = 'NewPostsByYear')


UPDATE Reporting_ReportGraph SET GraphQuery = '
SELECT   Cast(Year(PostTime) AS nvarchar(20))+'' '' + Cast( DatePart(week,PostTime) AS nvarchar(20)) AS ''Month'', COUNT(*) as ''Number of posts'' FROM Forums_ForumPost 
WHERE PostForumID IN (SELECT ForumID FROM Forums_Forum WHERE ForumGroupID IN (SELECT GroupID FROM Forums_ForumGroup WHERE GroupSiteID = @CMSContextCurrentSiteID)) AND 
{%DatabaseSchema%}.Func_Analytics_DateTrim(DateAdd(week, -1 * @LastXweeks, GetDate()),''week'') < PostTime
GROUP BY  Year(PostTime), DatePart(week,PostTime) ORDER BY Year(PostTime), DatePart(week,PostTime)
'
WHERE GraphName = 'NewPostsByWeekGraph' AND GraphReportID IN (SELECT ReportID FROM Reporting_Report WHERE ReportName = 'NewPostsByWeek')


UPDATE Reporting_ReportGraph SET GraphQuery = '
SELECT  Cast(Year(PostTime) AS nvarchar(20))+'' '' + Cast( Month(PostTime) AS nvarchar(20)) AS ''Month'', COUNT(*) as ''Number of posts'' FROM Forums_ForumPost 
WHERE PostForumID IN (SELECT ForumID FROM Forums_Forum WHERE ForumGroupID IN (SELECT GroupID FROM Forums_ForumGroup WHERE GroupSiteID = @CMSContextCurrentSiteID)) 
AND {%DatabaseSchema%}.Func_Analytics_DateTrim(DateAdd(month, -1 * @LastXmonths, GetDate()),''month'') < PostTime
GROUP BY  Year(PostTime), Month(PostTime) ORDER BY Year(PostTime), Month(PostTime)
'
WHERE GraphName = 'ForumPostsByMonthGraph' AND GraphReportID IN (SELECT ReportID FROM Reporting_Report WHERE ReportName = 'NewPostsByMonth')


UPDATE Reporting_ReportGraph SET GraphQuery = '
SELECT   Cast(Year(PostTime) AS nvarchar(20))+'' '' + Cast( Month(PostTime) AS nvarchar(20))+'' '' + Cast( Day(PostTime) AS nvarchar(20)) AS ''Day'', COUNT(*) as ''Number of posts'' FROM Forums_ForumPost 
WHERE PostForumID IN (SELECT ForumID FROM Forums_Forum WHERE ForumGroupID IN (SELECT GroupID FROM Forums_ForumGroup WHERE GroupSiteID = @CMSContextCurrentSiteID)) AND
{%DatabaseSchema%}.Func_Analytics_DateTrim(DateAdd(day, -1 * @LastXDays, GetDate()),''day'') < PostTime
GROUP BY  Year(PostTime), Month(PostTime), Day(PostTime) ORDER BY Year(PostTime), Month(PostTime), Day(PostTime)
'
WHERE GraphName = 'NewPostsByDayGraph' AND GraphReportID IN (SELECT ReportID FROM Reporting_Report WHERE ReportName = 'NewPostsByDay')

END

/*********************** END UPDATE FORUM POSTS ***********************************************/


/********************* UPDATE VIEWS ***********************/
-- Declare helper variables
DECLARE @definition NVARCHAR(MAX);
DECLARE @viewName NVARCHAR(128);
-- Declare cursor
DECLARE updateViewCursor CURSOR FAST_FORWARD FOR 
-- Select all corrupted views
SELECT [TABLE_NAME]	FROM [INFORMATION_SCHEMA].[VIEWS] WHERE [TABLE_NAME] IN (
	'View_PageInfo_Blank',
	'View_Forums_GroupForumPost_Joined',
	'View_CMS_EventLog_Joined',
	'View_Document_Attachment',
	'View_CMS_Tree_Joined_Attachments',
	'View_CMS_Tree_Joined_Versions_Attachments',
	'View_CMS_ACLItem_ItemsAndOperators',
	'View_CMS_WidgetCategoryWidget_Joined',
	'View_CMS_WebPartCategoryWebpart_Joined',
	'View_CMS_WidgetMetafile_Joined',
	'View_CMS_SiteRoleResourceUIElement_Joined',
	'View_CMS_PageTemplateMetafile_Joined',
	'View_CMS_PageTemplateCategoryPageTemplate_Joined',
	'View_CMS_LayoutMetafile_Joined',
	'View_CMS_ResourceTranslated_Joined',
	'View_BookingSystem_Joined',
	'View_Boards_BoardMessage_Joined',
	'View_Community_Friend_RequestedFriends',
	'View_Community_Friend_Friends',
	'View_Poll_AnswerCount',
	'View_CMS_Site_DocumentCount',
	'View_CMS_RoleResourcePermission_Joined',
	'View_PM_ProjectStatus_Joined',
	'View_PM_ProjectTaskStatus_Joined',
	'View_OM_Account_Joined',
	'View_OM_AccountContact_ContactJoined',
	'View_OM_AccountContact_AccountJoined',
	'View_Integration_Task_Joined',
	'View_OM_ContactGroupMember_ContactJoined',
	'View_OM_ContactGroupMember_AccountJoined',
	'View_CMS_DocumentAlias_Joined'
	);

OPEN updateViewCursor 

FETCH NEXT FROM updateViewCursor INTO @viewName
WHILE @@FETCH_STATUS = 0
BEGIN
	-- Get view definition
	SELECT @definition = COALESCE(@definition,'') + definition FROM sys.sql_modules WHERE OBJECT_NAME(object_id) = @viewName;
	-- Change CREATE to ALTER
	SET @definition = REPLACE(@definition, 'CREATE VIEW', 'ALTER VIEW');
	-- Remove dbo schema
	SET @definition = REPLACE(@definition, '[dbo].', '');
	SET @definition = REPLACE(@definition, 'dbo.', '');
	
	-- Refresh view
	EXEC (@definition);
	-- Reset view definition
	SET @definition = '';
	-- Continue with next view
	FETCH NEXT FROM updateViewCursor INTO @viewName
END

CLOSE updateViewCursor 
DEALLOCATE updateViewCursor 


-- Remove View_Newsletter_Link_Joined
IF EXISTS (SELECT 1 FROM [INFORMATION_SCHEMA].[VIEWS] WHERE [TABLE_NAME] = 'View_Newsletter_Link_Joined')
DROP VIEW [View_Newsletter_Link_Joined]
GO

/********************* END UPDATE VIEWS ***********************/


/********************* UPDATE CONTACT MANAGEMENT ***********************/

ALTER PROCEDURE Proc_OM_Contact_MoveRelations
	@mergeIntoContactID int,
	@mergeFromContactID int
AS
BEGIN
	SET NOCOUNT ON;
		-- Move Activities into parent contact
    	UPDATE OM_Activity SET ActivityActiveContactID = @mergeIntoContactID WHERE ActivityActiveContactID = @mergeFromContactID 
    	-- Move memberships 
    	UPDATE OM_Membership SET ActiveContactID = @mergeIntoContactID WHERE ActiveContactID = @mergeFromContactID 
    	-- IPs
    	UPDATE OM_IP SET IPActiveContactID = @mergeIntoContactID WHERE IPActiveContactID = @mergeFromContactID 
    	-- User agents
    	UPDATE OM_UserAgent SET UserAgentActiveContactID = @mergeIntoContactID WHERE UserAgentActiveContactID = @mergeFromContactID
END
GO


IF OBJECT_ID('Func_OM_Contact_GetChildren_Multiple') IS NOT NULL
BEGIN
DROP FUNCTION Func_OM_Contact_GetChildren_Multiple
END
GO

CREATE FUNCTION [Func_OM_Contact_GetChildren_Multiple]
(
    @contactIDs nvarchar(max),
    @includeParent int
)
 
RETURNS @result TABLE
(
    ContactID int
)
AS
BEGIN
    DECLARE @parsed TABLE (ContactID int);
    INSERT INTO @parsed SELECT * FROM Func_Selection_ParseIDs(@contactIDs);
    
    -- Recursively find all children of current contact
    WITH Recursion(ContactID)
    AS
    (
        SELECT c.ContactID
        FROM OM_Contact c, @parsed p
        WHERE c.ContactMergedWithContactID = p.ContactID OR c.ContactGlobalContactID = p.ContactID
        UNION ALL
        SELECT c.ContactID
        FROM OM_Contact c INNER JOIN Recursion r ON c.ContactMergedWithContactID = r.ContactID
        UNION ALL
        SELECT c.ContactID
        FROM OM_Contact c INNER JOIN Recursion r ON c.ContactGlobalContactID = r.ContactID
    )
    INSERT INTO @result SELECT DISTINCT ContactID FROM Recursion 

    -- Include parent contact IDs in result
    IF @includeParent = 1
      INSERT INTO @result SELECT DISTINCT * FROM @parsed
      
    RETURN 
END
GO


ALTER FUNCTION [Func_OM_Contact_GetChildren] 
(
	@currentContactId int,
	@includeParent int
)
RETURNS @result TABLE
(
	ContactID int
)
AS
BEGIN
    -- Recursively find all children of current contact
    WITH Recursion(ContactID)
    AS
    (
        SELECT ContactID
        FROM OM_Contact c
        WHERE c.ContactMergedWithContactID = @currentContactId OR c.ContactGlobalContactID = @currentContactId
        UNION ALL
        SELECT c.ContactID
        FROM OM_Contact c INNER JOIN Recursion r ON c.ContactMergedWithContactID = r.ContactID
        UNION ALL
        SELECT c.ContactID
        FROM OM_Contact c INNER JOIN Recursion r ON c.ContactGlobalContactID = r.ContactID
    )
    INSERT INTO @result SELECT DISTINCT ContactID FROM Recursion 
	-- Include parent contact ID in result
	IF @includeParent = 1
      INSERT INTO @result VALUES (@currentContactId)
	RETURN 
END
GO


ALTER PROCEDURE [Proc_OM_Contact_MassDelete]
	@where nvarchar(max),
	@batchLimit int
AS
BEGIN
	SET NOCOUNT ON;
	-- Variables
	DECLARE @DeletedContacts TABLE (
		ContactID int NOT NULL
	);	
	DECLARE @currentContactSiteID int;
	DECLARE @currentDeletedContactID int;
	DECLARE @sqlQuery NVARCHAR(MAX);
	DECLARE @listStr NVARCHAR(MAX);
	DECLARE @top NVARCHAR(20);
	
	-- Limit processed batch
	IF ((@batchLimit IS NOT NULL) OR (@batchLimit > 0))
		SET @top = 'TOP ' + CAST(@batchLimit AS NVARCHAR(10));
	ELSE 
		SET @top = 'TOP 1000';
	
	SET @sqlQuery = 'SELECT ' + @top + ' ContactID FROM OM_Contact WHERE ' + @where;
	INSERT INTO @DeletedContacts EXEC(@sqlQuery);
	
	-- Process first batch of records
	WHILE ((SELECT Count(*) FROM @DeletedContacts) > 0)
	BEGIN			
		-- Loop through records
		IF ((SELECT Count(*) FROM @DeletedContacts) > 0)
		BEGIN	
			-- Add merged contacts to deleted list
			SELECT @listStr = COALESCE(@listStr+',' ,'') + CAST(ContactID AS nvarchar(10)) FROM @DeletedContacts;
			INSERT INTO @DeletedContacts SELECT * FROM Func_OM_Contact_GetChildren_Multiple(@listStr, 1);
			
			/* Remove all references */
			UPDATE o SET o.AccountPrimaryContactID = NULL FROM OM_Account o INNER JOIN @DeletedContacts d ON o.AccountPrimaryContactID = d.ContactID;
			UPDATE o SET o.AccountSecondaryContactID = NULL FROM OM_Account o INNER JOIN @DeletedContacts d ON o.AccountSecondaryContactID = d.ContactID;
			/* Remove all relations */
			DELETE o FROM OM_AccountContact o INNER JOIN @DeletedContacts d ON o.ContactID = d.ContactID;
			DELETE o FROM OM_ContactGroupMember o LEFT JOIN @DeletedContacts d ON o.ContactGroupMemberRelatedID = d.ContactID WHERE o.ContactGroupMemberType=0;
			DELETE o FROM OM_Membership o LEFT JOIN @DeletedContacts do ON o.OriginalContactID = do.ContactID LEFT JOIN @DeletedContacts da ON o.ActiveContactID = da.ContactID WHERE do.ContactID IS NOT NULL OR da.ContactID IS NOT NULL;
			DELETE o FROM OM_IP o LEFT JOIN @DeletedContacts do ON o.IPOriginalContactID = do.ContactID LEFT JOIN @DeletedContacts da ON o.IPActiveContactID = da.ContactID WHERE do.ContactID IS NOT NULL OR da.ContactID IS NOT NULL;
			DELETE o FROM OM_UserAgent o LEFT JOIN @DeletedContacts do ON o.UserAgentOriginalContactID = do.ContactID LEFT JOIN @DeletedContacts da ON o.UserAgentActiveContactID = da.ContactID WHERE do.ContactID IS NOT NULL OR da.ContactID IS NOT NULL;
			DELETE o FROM OM_ScoreContactRule o INNER JOIN @DeletedContacts d ON o.ContactID = d.ContactID;
			/* Delete relations from depending activity */
			DELETE o FROM OM_PageVisit o INNER JOIN OM_Activity a ON o.PageVisitActivityID = a.ActivityID LEFT JOIN @DeletedContacts do ON a.ActivityOriginalContactID = do.ContactID LEFT JOIN @DeletedContacts da ON a.ActivityActiveContactID = da.ContactID WHERE (do.ContactID IS NOT NULL OR da.ContactID IS NOT NULL) AND (a.ActivityType = 'pagevisit' OR a.ActivityType = 'landingpage');
			DELETE o FROM OM_Search o INNER JOIN OM_Activity a ON o.SearchActivityID = a.ActivityID LEFT JOIN @DeletedContacts do ON a.ActivityOriginalContactID = do.ContactID LEFT JOIN @DeletedContacts da ON a.ActivityActiveContactID = da.ContactID WHERE (do.ContactID IS NOT NULL OR da.ContactID IS NOT NULL) AND (a.ActivityType = 'internalsearch' OR a.ActivityType = 'externalsearch');
			DELETE o FROM OM_Activity o LEFT JOIN @DeletedContacts do ON o.ActivityOriginalContactID = do.ContactID LEFT JOIN @DeletedContacts da ON o.ActivityActiveContactID = da.ContactID WHERE do.ContactID IS NOT NULL OR da.ContactID IS NOT NULL;
			
			-- Delete merged and parent records
			DELETE o FROM OM_Contact o INNER JOIN @DeletedContacts d ON o.ContactID = d.ContactID;
			DELETE FROM @DeletedContacts
		END
		
		-- Get next batch
		IF (@batchLimit IS  NULL)
			INSERT INTO @DeletedContacts EXEC(@sqlQuery);
	END
END
GO


UPDATE CMS_Query
SET QueryText = 
'SELECT ActiveContactID AS ContactID FROM OM_Membership m WHERE m.RelatedID=@RelatedID AND m.MemberType=@MemberType AND EXISTS(SELECT ContactID FROM OM_Contact WHERE ContactID=m.ActiveContactID AND ContactSiteID=@SiteID AND ##WHERE##) ORDER BY ActiveContactID' 
WHERE QueryName = N'selectbyrelatedidandtype' AND ClassID = (SELECT ClassID FROM CMS_Class WHERE ClassName=N'OM.Membership')
GO

UPDATE CMS_Query
SET QueryText = 
'SELECT ##TOPN## ##COLUMNS## FROM (
SELECT OM_Activity.ActivityID, OM_Activity.ActivityTitle, OM_Activity.ActivityType,
OM_Activity.ActivityCreated, OM_Activity.ActivitySiteID,OM_Activity.ActivityIPAddress,
OM_Contact.ContactID, OM_Contact.ContactMergedWithContactID,
ISNULL(OM_Contact.ContactFirstName,'''') +
CASE OM_Contact.ContactFirstName
  WHEN '''' THEN ''''
  WHEN NULL THEN ''''
  ELSE '' ''
END
+ ISNULL(OM_Contact.ContactMiddleName,'''') +
CASE OM_Contact.ContactMiddleName
  WHEN '''' THEN ''''
  WHEN NULL THEN ''''
  ELSE '' ''
END
+ ISNULL(OM_Contact.ContactLastName, '''') AS ContactFullNameJoined
FROM OM_Activity
LEFT JOIN OM_Contact ON OM_Activity.ActivityActiveContactID = OM_Contact.ContactID
) as tab WHERE ##WHERE## ORDER BY ##ORDERBY##' 
WHERE QueryName = N'selectactivitylist' AND ClassID = (SELECT ClassID FROM CMS_Class WHERE ClassName=N'OM.Activity')
GO

UPDATE CMS_Query
SET QueryText = 
'SELECT ##TOPN## ##COLUMNS## FROM (
SELECT ContactID, MembershipID, ActiveContactID, OriginalContactID, CustomerFirstName, CustomerLastName, CustomerEmail, CustomerCompany, 
ContactMergedWithContactID, OM_Contact.ContactSiteID,
ISNULL(OM_Contact.ContactFirstName,'''') +
CASE OM_Contact.ContactFirstName
  WHEN '''' THEN ''''
  WHEN NULL THEN ''''
  ELSE '' ''
END
+ ISNULL(OM_Contact.ContactMiddleName,'''') +
CASE OM_Contact.ContactMiddleName
  WHEN '''' THEN ''''
  WHEN NULL THEN ''''
  ELSE '' ''
END
+ ISNULL(OM_Contact.ContactLastName, '''') AS ContactFullNameJoined
FROM OM_Membership
	INNER JOIN OM_Contact ON OM_Membership.OriginalContactID=OM_Contact.ContactID
	INNER JOIN COM_Customer ON OM_Membership.RelatedID=COM_Customer.CustomerID
WHERE ActiveContactID=@ContactID AND MemberType=1) as tab
WHERE ##WHERE## ORDER BY ##ORDERBY##' 
WHERE QueryName = N'selectcustomers' AND ClassID = (SELECT ClassID FROM CMS_Class WHERE ClassName=N'OM.Membership')
GO

UPDATE CMS_Query
SET QueryText = 
'SELECT ##TOPN## ##COLUMNS## FROM (
SELECT ContactID, MembershipID, ActiveContactID, OriginalContactID, ContactMergedWithContactID, CustomerCompany, 
CustomerFirstName, CustomerLastName, CustomerEmail, OM_Contact.ContactSiteID,
ISNULL(OM_Contact.ContactFirstName,'''') +
CASE OM_Contact.ContactFirstName
  WHEN '''' THEN ''''
  WHEN NULL THEN ''''
  ELSE '' ''
END
+ ISNULL(OM_Contact.ContactMiddleName,'''') +
CASE OM_Contact.ContactMiddleName
  WHEN '''' THEN ''''
  WHEN NULL THEN ''''
  ELSE '' ''
END
+ ISNULL(OM_Contact.ContactLastName, '''') AS ContactFullNameJoined
FROM OM_Membership
INNER JOIN OM_Contact ON OM_Membership.OriginalContactID=OM_Contact.ContactID
INNER JOIN COM_Customer ON OM_Membership.RelatedID=COM_Customer.CustomerID
WHERE MemberType=1 AND OriginalContactID IN (SELECT * FROM Func_OM_Contact_GetChildren(@ContactID, 1))) as tab
WHERE ##WHERE## ORDER BY ##ORDERBY##' 
WHERE QueryName = N'selectmergedcustomers' AND ClassID = (SELECT ClassID FROM CMS_Class WHERE ClassName=N'OM.Membership')
GO

UPDATE CMS_Query
SET QueryText = 
'SELECT ##TOPN## ##COLUMNS## FROM (
SELECT ContactID, MembershipID, ActiveContactID, OriginalContactID, CustomerFirstName, CustomerLastName, CustomerEmail, CustomerCompany,
ContactMergedWithContactID, OM_Contact.ContactSiteID, ISNULL(OM_Contact.ContactFirstName,'''') +
CASE OM_Contact.ContactFirstName
  WHEN '''' THEN ''''
  WHEN NULL THEN ''''
  ELSE '' ''
END
+ ISNULL(OM_Contact.ContactMiddleName,'''') +
CASE OM_Contact.ContactMiddleName
  WHEN '''' THEN ''''
  WHEN NULL THEN ''''
  ELSE '' ''
END
+ ISNULL(OM_Contact.ContactLastName, '''') AS ContactFullNameJoined
FROM OM_Membership
INNER JOIN OM_Contact ON OM_Membership.OriginalContactID=OM_Contact.ContactID
INNER JOIN COM_Customer ON OM_Membership.RelatedID=COM_Customer.CustomerID
WHERE ContactID IN (SELECT * FROM Func_OM_Contact_GetChildren_Global(@ContactID, 1)) AND MemberType=1) as tab
WHERE ##WHERE## ORDER BY ##ORDERBY##' 
WHERE QueryName = N'selectglobalcustomers' AND ClassID = (SELECT ClassID FROM CMS_Class WHERE ClassName=N'OM.Membership')
GO

UPDATE CMS_Query
SET QueryText = 
'SELECT ##TOPN## ##COLUMNS## FROM (
SELECT OM_Activity.ActivityID, OM_Activity.ActivityTitle, OM_Activity.ActivityType,
OM_Activity.ActivityCreated, OM_Activity.ActivitySiteID,OM_Activity.ActivityIPAddress,
OM_Contact.ContactID, OM_Contact.ContactMergedWithContactID,
ISNULL(OM_Contact.ContactFirstName,'''') +
CASE OM_Contact.ContactFirstName
  WHEN '''' THEN ''''
  WHEN NULL THEN ''''
  ELSE '' ''
END
+ ISNULL(OM_Contact.ContactMiddleName,'''') +
CASE OM_Contact.ContactMiddleName
  WHEN '''' THEN ''''
  WHEN NULL THEN ''''
  ELSE '' ''
END
+ ISNULL(OM_Contact.ContactLastName, '''') AS ContactFullNameJoined
FROM OM_Activity
INNER JOIN OM_Contact ON OM_Activity.ActivityActiveContactID = OM_Contact.ContactID WHERE ContactID IN (SELECT * FROM Func_OM_Contact_GetChildren(@ContactID, 1))
) as tab WHERE ##WHERE## ORDER BY ##ORDERBY##'
WHERE QueryName = N'selectcontactactivitygloballist' AND ClassID = (SELECT ClassID FROM CMS_Class WHERE ClassName=N'OM.Activity')
GO

UPDATE CMS_Query
SET QueryText = 
'SELECT ##TOPN## ##COLUMNS## FROM
(SELECT IPID, IPActiveContactID, IPOriginalContactID, IPAddress, IPCreated, ContactSiteID,
       ContactMergedWithContactID, ContactID, ISNULL(ContactFirstName, '''') + CASE ContactFirstName WHEN '''' THEN '''' WHEN NULL
       THEN '''' ELSE '' '' END + ISNULL(ContactMiddleName, '''') + CASE ContactMiddleName WHEN '''' THEN '''' WHEN NULL
       THEN '''' ELSE '' '' END + ISNULL(ContactLastName, '''') AS ContactFullName
FROM OM_IP LEFT JOIN OM_Contact ON OM_IP.IPOriginalContactID = OM_Contact.ContactID WHERE ContactID IN (SELECT * FROM Func_OM_Contact_GetChildren(@ContactID, 1))
) as tab WHERE ##WHERE## ORDER BY ##ORDERBY##'
WHERE QueryName = N'selectglobalips' AND ClassID = (SELECT ClassID FROM CMS_Class WHERE ClassName=N'OM.IP')
GO

UPDATE CMS_Query
SET QueryText = 
'SELECT ##TOPN## ##COLUMNS## FROM (
SELECT ContactID, MembershipID, ActiveContactID, OriginalContactID,ContactMergedWithContactID,
RelatedID, OM_Contact.ContactSiteID,
ISNULL(OM_Contact.ContactFirstName,'''') +
CASE OM_Contact.ContactFirstName
  WHEN '''' THEN ''''
  WHEN NULL THEN ''''
  ELSE '' ''
END
+ ISNULL(OM_Contact.ContactMiddleName,'''') +
CASE OM_Contact.ContactMiddleName
  WHEN '''' THEN ''''
  WHEN NULL THEN ''''
  ELSE '' ''
END
+ ISNULL(OM_Contact.ContactLastName, '''') AS ContactFullNameJoined
FROM OM_Membership
INNER JOIN OM_Contact ON OM_Membership.OriginalContactID=OM_Contact.ContactID
WHERE ContactID IN (SELECT * FROM Func_OM_Contact_GetChildren(@ContactID,1)) AND MemberType=0
) as tab WHERE ##WHERE## ORDER BY ##ORDERBY##'
WHERE QueryName = N'selectglobalusers' AND ClassID = (SELECT ClassID FROM CMS_Class WHERE ClassName=N'OM.Membership')
GO

UPDATE CMS_Query
SET QueryText = 
'SELECT ##TOPN## ##COLUMNS## FROM (
SELECT ContactID, MembershipID, ActiveContactID, OriginalContactID, RelatedID,
ContactMergedWithContactID, OM_Contact.ContactSiteID, ISNULL(OM_Contact.ContactFirstName,'''') +
CASE OM_Contact.ContactFirstName
  WHEN '''' THEN ''''
  WHEN NULL THEN ''''
  ELSE '' ''
END
+ ISNULL(OM_Contact.ContactMiddleName,'''') +
CASE OM_Contact.ContactMiddleName
  WHEN '''' THEN ''''
  WHEN NULL THEN ''''
  ELSE '' ''
END
+ ISNULL(OM_Contact.ContactLastName, '''') AS ContactFullNameJoined
FROM OM_Membership
INNER JOIN OM_Contact ON OM_Membership.OriginalContactID=OM_Contact.ContactID
WHERE ContactID IN (SELECT * FROM Func_OM_Contact_GetChildren(@ContactID, 1)) AND MemberType=1) as tab
WHERE ##WHERE## ORDER BY ##ORDERBY##'
WHERE QueryName = N'selectglobalcustomers' AND ClassID = (SELECT ClassID FROM CMS_Class WHERE ClassName=N'OM.Membership')
GO

UPDATE CMS_Query
SET QueryText = 
'SELECT ##TOPN## ##COLUMNS## FROM (
SELECT ContactID, MembershipID, ActiveContactID, OriginalContactID, RelatedID, ContactMergedWithContactID, OM_Contact.ContactSiteID,
ISNULL(OM_Contact.ContactFirstName,'''') +
CASE OM_Contact.ContactFirstName
  WHEN '''' THEN ''''
  WHEN NULL THEN ''''
  ELSE '' ''
END
+ ISNULL(OM_Contact.ContactMiddleName,'''') +
CASE OM_Contact.ContactMiddleName
  WHEN '''' THEN ''''
  WHEN NULL THEN ''''
  ELSE '' ''
END
+ ISNULL(OM_Contact.ContactLastName, '''') AS ContactFullNameJoined
FROM OM_Membership
INNER JOIN OM_Contact ON OM_Membership.OriginalContactID=OM_Contact.ContactID
WHERE ContactID IN (SELECT * FROM Func_OM_Contact_GetChildren(@ContactID,1)) AND MemberType=2) as tab WHERE ##WHERE## ORDER BY ##ORDERBY##'
WHERE QueryName = N'selectglobalsubscribers' AND ClassID = (SELECT ClassID FROM CMS_Class WHERE ClassName=N'OM.Membership')
GO

UPDATE CMS_Query
SET QueryText = 
'DECLARE @list TABLE (ActivityID INT, RN INT);
 INSERT INTO @list (ActivityID, RN) SELECT ActivityID, ROW_NUMBER() OVER (ORDER BY ##ORDERBY##) AS RN FROM
(
SELECT OM_Activity.ActivityID, OM_Activity.ActivityTitle, OM_Activity.ActivityType,
OM_Activity.ActivityCreated, OM_Activity.ActivitySiteID,OM_Activity.ActivityIPAddress,
OM_Contact.ContactID, OM_Contact.ContactMergedWithContactID,
ISNULL(OM_Contact.ContactFirstName,'''') +
CASE OM_Contact.ContactFirstName
  WHEN '''' THEN ''''
  WHEN NULL THEN ''''
  ELSE '' ''
END
+ ISNULL(OM_Contact.ContactMiddleName,'''') +
CASE OM_Contact.ContactMiddleName
  WHEN '''' THEN ''''
  WHEN NULL THEN ''''
  ELSE '' ''
END
+ ISNULL(OM_Contact.ContactLastName, '''') AS ContactFullNameJoined
FROM OM_Activity
INNER JOIN OM_Contact ON OM_Activity.ActivityActiveContactID = OM_Contact.ContactID WHERE ContactID IN (SELECT * FROM Func_OM_Contact_GetChildren(@ContactID, 1))
) as tab

WHERE ##WHERE##;
DECLARE @num INT; SET @num = (SELECT RN FROM @list WHERE ActivityID = @ActivityID);
SELECT ActivityID, RN, @num AS BASE_RN FROM @list WHERE RN IN (@num - 1, @num + 1)'
WHERE QueryName = N'selectpreviousnextglobalcontact' AND ClassID = (SELECT ClassID FROM CMS_Class WHERE ClassName=N'OM.Activity')
GO



UPDATE OM_ActivityType SET [ActivityTypeDetailFormControl]='selectdocument' WHERE ActivityTypeName='blogpostsubscription' AND ActivityTypeDetailFormControl IS NULL
UPDATE OM_ActivityType SET [ActivityTypeDetailFormControl]='selectdocument' WHERE ActivityTypeName='blogcomment' AND ActivityTypeDetailFormControl IS NULL
GO

DECLARE @categoryID int;
DECLARE @parentCategoryID int;
DECLARE @parentPath nvarchar(450);
DECLARE @CategoriesPadding nvarchar(450);
DECLARE @completePath nvarchar(450);

SELECT @parentCategoryID=CategoryID, @parentPath=CategoryIDPath FROM CMS_SettingsCategory WHERE CategoryName LIKE 'CMS.OnlineMarketing';

-- Add new settings keys for deleting inactive contacts
IF (SELECT TOP 1 CategoryName FROM CMS_SettingsCategory WHERE CategoryName LIKE 'DeleteInactiveContacts') IS NULL
BEGIN
	INSERT INTO CMS_SettingsCategory
	(CategoryDisplayName, CategoryOrder, CategoryName,  CategoryParentID,
	CategoryLevel, CategoryChildCount, CategoryIsGroup, CategoryIsCustom)
	VALUES ( '{$settingskey.cmsdeleteinactivecontacts$}', 7, 'DeleteInactiveContacts', @parentCategoryID, 2, 0, 1, 1);	

SET @categoryID = scope_identity();
SET @CategoriesPadding = REPLICATE('0', 8);
SET @completePath =  @parentPath + '/' + RIGHT(@CategoriesPadding + CAST(@categoryID AS nvarchar(8)), 8)

UPDATE CMS_SettingsCategory 
SET CategoryIDPath=@completePath
WHERE CategoryID=@categoryID;

END;


IF (SELECT TOP 1 KeyName FROM CMS_SettingsKey WHERE KeyName LIKE 'CMSDeleteInactiveContacts') IS NULL
BEGIN

	DECLARE @currentSiteID int;
	DECLARE @counter int;
	DECLARE @rowCount int;
	DECLARE @siteTable TABLE (SiteID INT NOT NULL);


	INSERT INTO @siteTable SELECT SiteID FROM CMS_Site ORDER BY SiteID ASC;
	SET @rowCount = (SELECT COUNT(SiteID) FROM @siteTable);
	SET @counter = 0;
	SET @currentSiteID = (SELECT TOP 1 SiteID FROM @siteTable ORDER BY SiteID ASC);


	INSERT INTO CMS_SettingsKey
	(KeyName, KeyDisplayName, KeyDescription, KeyType, KeyCategoryID, KeyGUID, KeyLastModified, KeyOrder,  KeyValue)
	VALUES ('CMSDeleteInactiveContacts', '{$settingskey.cmsdeleteinactivecontacts$}', '{$settingskey.cmsdeleteinactivecontacts.description$}',
	'boolean', @categoryID, NEWID(), GETDATE(),1, NULL);

	WHILE @counter < @rowCount
	BEGIN
		INSERT INTO CMS_SettingsKey
		(KeyName, KeyDisplayName, KeyDescription, KeyType, KeyCategoryID, KeyGUID, KeyLastModified, KeyOrder,SiteID)
		VALUES ('CMSDeleteInactiveContacts', '{$settingskey.cmsdeleteinactivecontacts$}', '{$settingskey.cmsdeleteinactivecontacts.description$}',
		'boolean', @categoryID, NEWID(), GETDATE(),1,@currentSiteID );
		DELETE FROM @siteTable WHERE SiteID = @currentSiteID;
		SET @currentSiteID = (SELECT TOP 1 SiteID FROM @siteTable ORDER BY SiteID ASC);
		SET @counter = @counter + 1    
	END;
END;


IF (SELECT TOP 1 KeyName FROM CMS_SettingsKey WHERE KeyName LIKE 'CMSLastActivityOlderThan') IS NULL
BEGIN

	INSERT INTO @siteTable SELECT SiteID FROM CMS_Site ORDER BY SiteID ASC;
	SET @rowCount = (SELECT COUNT(SiteID) FROM @siteTable);
	SET @counter = 0;
	SET @currentSiteID = (SELECT TOP 1 SiteID FROM @siteTable ORDER BY SiteID ASC);


	INSERT INTO CMS_SettingsKey
	(KeyName, KeyDisplayName, KeyDescription, KeyType, KeyCategoryID, KeyGUID, KeyLastModified, KeyOrder,  KeyValue)
	VALUES ('CMSLastActivityOlderThan', '{$settingskey.cmslastactivityolderthan$}', '{$settingskey.lastactivitydescription$}',
	'int', @categoryID, NEWID(), GETDATE(),2, NULL);

	WHILE @counter < @rowCount
	BEGIN
		INSERT INTO CMS_SettingsKey
		(KeyName, KeyDisplayName, KeyDescription, KeyType, KeyCategoryID, KeyGUID, KeyLastModified, KeyOrder,SiteID)
		VALUES ('CMSLastActivityOlderThan', '{$settingskey.cmslastactivityolderthan$}', '{$settingskey.lastactivitydescription$}',
		'int', @categoryID, NEWID(), GETDATE(),2,@currentSiteID );
		DELETE FROM @siteTable WHERE SiteID = @currentSiteID;
		SET @currentSiteID = (SELECT TOP 1 SiteID FROM @siteTable ORDER BY SiteID ASC);
		SET @counter = @counter + 1    
	END;
END;

IF (SELECT TOP 1 KeyName FROM CMS_SettingsKey WHERE KeyName LIKE 'CMSContactCreatedBefore') IS NULL
BEGIN
	INSERT INTO @siteTable SELECT SiteID FROM CMS_Site ORDER BY SiteID ASC;
	SET @rowCount = (SELECT COUNT(SiteID) FROM @siteTable);
	SET @counter = 0;
	SET @currentSiteID = (SELECT TOP 1 SiteID FROM @siteTable ORDER BY SiteID ASC);


	INSERT INTO CMS_SettingsKey
	(KeyName, KeyDisplayName, KeyDescription, KeyType, KeyCategoryID, KeyGUID, KeyLastModified, KeyOrder,  KeyValue)
	VALUES ('CMSContactCreatedBefore', '{$settingskey.contactcreatedbefore$}', '{$settingskey.contactcreatedbefore.description$}',
	'int', @categoryID, NEWID(), GETDATE(),3, NULL);

	WHILE @counter < @rowCount
	BEGIN
		INSERT INTO CMS_SettingsKey
		(KeyName, KeyDisplayName, KeyDescription, KeyType, KeyCategoryID, KeyGUID, KeyLastModified, KeyOrder,SiteID)
		VALUES ('CMSContactCreatedBefore', '{$settingskey.contactcreatedbefore$}', '{$settingskey.contactcreatedbefore.description$}',
		'int', @categoryID, NEWID(), GETDATE(),3,@currentSiteID );
		DELETE FROM @siteTable WHERE SiteID = @currentSiteID;
		SET @currentSiteID = (SELECT TOP 1 SiteID FROM @siteTable ORDER BY SiteID ASC);
		SET @counter = @counter + 1    
	END;
END;

IF (SELECT TOP 1 KeyName FROM CMS_SettingsKey WHERE KeyName LIKE 'CMSContactLastLogon') IS NULL
BEGIN

	INSERT INTO @siteTable SELECT SiteID FROM CMS_Site ORDER BY SiteID ASC;
	SET @rowCount = (SELECT COUNT(SiteID) FROM @siteTable);
	SET @counter = 0;
	SET @currentSiteID = (SELECT TOP 1 SiteID FROM @siteTable ORDER BY SiteID ASC);


	INSERT INTO CMS_SettingsKey
	(KeyName, KeyDisplayName, KeyDescription, KeyType, KeyCategoryID, KeyGUID, KeyLastModified, KeyOrder,  KeyValue)
	VALUES ('CMSContactLastLogon', '{$settingskey.contactlastlogon$}', '{$settingskey.contactlastlogon.description$}',
	'int', @categoryID, NEWID(), GETDATE(),4, NULL);

	WHILE @counter < @rowCount
	BEGIN
		INSERT INTO CMS_SettingsKey
		(KeyName, KeyDisplayName, KeyDescription, KeyType, KeyCategoryID, KeyGUID, KeyLastModified, KeyOrder,SiteID)
		VALUES ('CMSContactLastLogon', '{$settingskey.contactlastlogon$}', '{$settingskey.contactlastlogon.description$}',
		'int', @categoryID, NEWID(), GETDATE(),4,@currentSiteID );
		DELETE FROM @siteTable WHERE SiteID = @currentSiteID;
		SET @currentSiteID = (SELECT TOP 1 SiteID FROM @siteTable ORDER BY SiteID ASC);
		SET @counter = @counter + 1    
	END;
END;

IF (SELECT TOP 1 KeyName FROM CMS_SettingsKey WHERE KeyName LIKE 'CMSContactLastModified') IS NULL
BEGIN

	INSERT INTO @siteTable SELECT SiteID FROM CMS_Site ORDER BY SiteID ASC;
	SET @rowCount = (SELECT COUNT(SiteID) FROM @siteTable);
	SET @counter = 0;
	SET @currentSiteID = (SELECT TOP 1 SiteID FROM @siteTable ORDER BY SiteID ASC);


	INSERT INTO CMS_SettingsKey
	(KeyName, KeyDisplayName, KeyDescription, KeyType, KeyCategoryID, KeyGUID, KeyLastModified, KeyOrder,  KeyValue)
	VALUES ('CMSContactLastModified', '{$settingskey.contactlastmodified$}', '{$settingskey.contactlastmodified.description$}',
	'int', @categoryID, NEWID(), GETDATE(),5, NULL);

	WHILE @counter < @rowCount
	BEGIN
		INSERT INTO CMS_SettingsKey
		(KeyName, KeyDisplayName, KeyDescription, KeyType, KeyCategoryID, KeyGUID, KeyLastModified, KeyOrder,SiteID)
		VALUES ('CMSContactLastModified', '{$settingskey.contactlastmodified$}', '{$settingskey.contactlastmodified.description$}',
		'int', @categoryID, NEWID(), GETDATE(),5,@currentSiteID );
		DELETE FROM @siteTable WHERE SiteID = @currentSiteID;
		SET @currentSiteID = (SELECT TOP 1 SiteID FROM @siteTable ORDER BY SiteID ASC);
		SET @counter = @counter + 1    
	END;
END;

IF (SELECT TOP 1 KeyName FROM CMS_SettingsKey WHERE KeyName LIKE 'CMSContactMergedWhen') IS NULL
BEGIN

	INSERT INTO @siteTable SELECT SiteID FROM CMS_Site ORDER BY SiteID ASC;
	SET @rowCount = (SELECT COUNT(SiteID) FROM @siteTable);
	SET @counter = 0;
	SET @currentSiteID = (SELECT TOP 1 SiteID FROM @siteTable ORDER BY SiteID ASC);


	INSERT INTO CMS_SettingsKey
	(KeyName, KeyDisplayName, KeyDescription, KeyType, KeyCategoryID, KeyGUID, KeyLastModified, KeyOrder,  KeyValue)
	VALUES ('CMSContactMergedWhen', '{$settingskey.contactmergedwhen$}', '{$settingskey.contactmergedwhen.description$}',
	'int', @categoryID, NEWID(), GETDATE(),6, NULL);

	WHILE @counter < @rowCount
	BEGIN
		INSERT INTO CMS_SettingsKey
		(KeyName, KeyDisplayName, KeyDescription, KeyType, KeyCategoryID, KeyGUID, KeyLastModified, KeyOrder,SiteID)
		VALUES ('CMSContactMergedWhen', '{$settingskey.contactmergedwhen$}', '{$settingskey.contactmergedwhen.description$}',
		'int', @categoryID, NEWID(), GETDATE(),6,@currentSiteID );
		DELETE FROM @siteTable WHERE SiteID = @currentSiteID;
		SET @currentSiteID = (SELECT TOP 1 SiteID FROM @siteTable ORDER BY SiteID ASC);
		SET @counter = @counter + 1    
	END;
END;


IF (SELECT TOP 1 KeyName FROM CMS_SettingsKey WHERE KeyName LIKE 'CMSContactMergedSite') IS NULL
BEGIN

	INSERT INTO @siteTable SELECT SiteID FROM CMS_Site ORDER BY SiteID ASC;
	SET @rowCount = (SELECT COUNT(SiteID) FROM @siteTable);
	SET @counter = 0;
	SET @currentSiteID = (SELECT TOP 1 SiteID FROM @siteTable ORDER BY SiteID ASC);


	INSERT INTO CMS_SettingsKey
	(KeyName, KeyDisplayName, KeyDescription, KeyType, KeyCategoryID, KeyGUID, KeyLastModified, KeyOrder,  KeyValue)
	VALUES ('CMSContactMergedSite', '{$settingskey.contactmergedintosite$}', '{$settingskey.contactmergedintosite.description$}',
	'boolean', @categoryID, NEWID(), GETDATE(),7, NULL);

	WHILE @counter < @rowCount
	BEGIN
		INSERT INTO CMS_SettingsKey
		(KeyName, KeyDisplayName, KeyDescription, KeyType, KeyCategoryID, KeyGUID, KeyLastModified, KeyOrder,SiteID)
		VALUES ('CMSContactMergedSite', '{$settingskey.contactmergedintosite$}', '{$settingskey.contactmergedintosite.description$}',
		'boolean', @categoryID, NEWID(), GETDATE(),7,@currentSiteID);
		DELETE FROM @siteTable WHERE SiteID = @currentSiteID;
		SET @currentSiteID = (SELECT TOP 1 SiteID FROM @siteTable ORDER BY SiteID ASC);
		SET @counter = @counter + 1    
	END;
END;


IF (SELECT TOP 1 KeyName FROM CMS_SettingsKey WHERE KeyName LIKE 'CMSContactMergedGlobal') IS NULL
BEGIN

	INSERT INTO @siteTable SELECT SiteID FROM CMS_Site ORDER BY SiteID ASC;
	SET @rowCount = (SELECT COUNT(SiteID) FROM @siteTable);
	SET @counter = 0;
	SET @currentSiteID = (SELECT TOP 1 SiteID FROM @siteTable ORDER BY SiteID ASC);


	INSERT INTO CMS_SettingsKey
	(KeyName, KeyDisplayName, KeyDescription, KeyType, KeyCategoryID, KeyGUID, KeyLastModified, KeyOrder,  KeyValue)
	VALUES ('CMSContactMergedGlobal', '{$settingskey.mergedintoglobal$}', '{$settingskey.mergedintoglobal.description$}',
	'boolean', @categoryID, NEWID(), GETDATE(),8, NULL);

	WHILE @counter < @rowCount
	BEGIN
		INSERT INTO CMS_SettingsKey
		(KeyName, KeyDisplayName, KeyDescription, KeyType, KeyCategoryID, KeyGUID, KeyLastModified, KeyOrder,SiteID)
		VALUES ('CMSContactMergedGlobal', '{$settingskey.mergedintoglobal$}', '{$settingskey.mergedintoglobal.description$}',
		'boolean', @categoryID, NEWID(), GETDATE(),8,@currentSiteID);
		DELETE FROM @siteTable WHERE SiteID = @currentSiteID;
		SET @currentSiteID = (SELECT TOP 1 SiteID FROM @siteTable ORDER BY SiteID ASC);
		SET @counter = @counter + 1    
	END;
END;


IF (SELECT TOP 1 KeyName FROM CMS_SettingsKey WHERE KeyName LIKE 'CMSContactIsAnonymous') IS NULL
BEGIN

	INSERT INTO @siteTable SELECT SiteID FROM CMS_Site ORDER BY SiteID ASC;
	SET @rowCount = (SELECT COUNT(SiteID) FROM @siteTable);
	SET @counter = 0;
	SET @currentSiteID = (SELECT TOP 1 SiteID FROM @siteTable ORDER BY SiteID ASC);


	INSERT INTO CMS_SettingsKey
	(KeyName, KeyDisplayName, KeyDescription, KeyType, KeyCategoryID, KeyGUID, KeyLastModified, KeyOrder,  KeyValue, KeyEditingControlPath)
	VALUES ('CMSContactIsAnonymous', '{$settingskey.contactisanonymous$}', '{$settingskey.contactisanonymousdescription$}',
	'int', @categoryID, NEWID(), GETDATE(),9, NULL, '~/CMSModules/Settings/FormControls/ContactIsAnonymous.ascx');

	WHILE @counter < @rowCount
	BEGIN
		INSERT INTO CMS_SettingsKey
		(KeyName, KeyDisplayName, KeyDescription, KeyType, KeyCategoryID, KeyGUID, KeyLastModified, KeyOrder,SiteID, KeyEditingControlPath)
		VALUES ('CMSContactIsAnonymous', '{$settingskey.contactisanonymous$}', '{$settingskey.contactisanonymousdescription$}',
		'int', @categoryID, NEWID(), GETDATE(),9,@currentSiteID, '~/CMSModules/Settings/FormControls/ContactIsAnonymous.ascx');
		DELETE FROM @siteTable WHERE SiteID = @currentSiteID;
		SET @currentSiteID = (SELECT TOP 1 SiteID FROM @siteTable ORDER BY SiteID ASC);
		SET @counter = @counter + 1    
	END;
END;
GO

-- Add new settings key for loging file downloads (cms.file) as page visit activity
DECLARE @ActivityCategoryID int;
SELECT @ActivityCategoryID=[CategoryID] FROM [CMS_SettingsCategory] WHERE [CategoryName]='CMS.OnlineMarketing.Activities.General'

IF @ActivityCategoryID IS NOT NULL
BEGIN
  DECLARE @GlobalActivityKey int;
  SELECT @GlobalActivityKey=[KeyID] FROM [CMS_SettingsKey] WHERE [KeyName] = 'CMSActivityTrackedExtensions'  

  IF @GlobalActivityKey IS NULL
  BEGIN
    -- Insert global key
    INSERT INTO CMS_SettingsKey ([KeyName], [KeyDisplayName], [KeyDescription], [KeyValue], [KeyType], [KeyCategoryID], [SiteID], [KeyGUID], [KeyLastModified], [KeyDefaultValue], [KeyOrder])
    SELECT 'CMSActivityTrackedExtensions', '{$settingskey.cmsactivitytrackedextensions$}', '{$settingskey.cmsactivitytrackedextensions.description$}', 'pdf;doc;docx;xls;xlsx;txt', 'string', @ActivityCategoryID, NULL, NEWID(), GETDATE(), 'pdf;doc;docx;xls;xlsx;txt', 100
    INSERT INTO CMS_SettingsKey ([KeyName], [KeyDisplayName], [KeyDescription], [KeyValue], [KeyType], [KeyCategoryID], [SiteID], [KeyGUID], [KeyLastModified], [KeyDefaultValue], [KeyOrder])
    -- Insert site key for all sites
    SELECT 'CMSActivityTrackedExtensions', '{$settingskey.cmsactivitytrackedextensions$}', '{$settingskey.cmsactivitytrackedextensions.description$}', NULL, 'string', @ActivityCategoryID, SiteID, NEWID(), GETDATE(), NULL, 100
    FROM CMS_Site
  END
END
GO

-- Add new scheduled task for deleting inactive contacts
IF (SELECT TaskName FROM CMS_ScheduledTask WHERE TaskName LIKE 'DeleteInactiveContacts') IS NULL
BEGIN
INSERT INTO CMS_ScheduledTask
(TaskName, TaskDisplayName, TaskAssemblyName, TaskClass, TaskInterval, TaskNextRunTime, TaskEnabled, TaskGUID, TaskLastModified, TaskRunInSeparateThread, TaskData)
VALUES ('DeleteInactiveContacts','Delete inactive contacts','CMS.OnlineMarketing','CMS.OnlineMarketing.DeleteInactiveContacts', 'week;11/21/2011 1:57:18 PM;1', DATEADD(day, 7, GETDATE()), 1, NEWID(),GETDATE(),1,'' )
END
GO

-- Changes UserAgentString column to nvarchar(max) and updates class schema and form definition
ALTER TABLE [OM_UserAgent] ALTER COLUMN [UserAgentString] nvarchar(max);
UPDATE [CMS_Class] SET [ClassXmlSchema]='<?xml version="1.0" encoding="utf-8"?><xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata"><xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true"><xs:complexType><xs:choice minOccurs="0" maxOccurs="unbounded"><xs:element name="OM_UserAgent"><xs:complexType><xs:sequence><xs:element name="UserAgentID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" /><xs:element name="UserAgentString"><xs:simpleType><xs:restriction base="xs:string"><xs:maxLength value="2147483647" /></xs:restriction></xs:simpleType></xs:element><xs:element name="UserAgentActiveContactID" type="xs:int" /><xs:element name="UserAgentOriginalContactID" type="xs:int" /><xs:element name="UserAgentCreated" type="xs:dateTime" /></xs:sequence></xs:complexType></xs:element></xs:choice></xs:complexType><xs:unique name="Constraint1" msdata:PrimaryKey="true"><xs:selector xpath=".//OM_UserAgent" /><xs:field xpath="UserAgentID" /></xs:unique></xs:element></xs:schema>',
[ClassFormDefinition]='<form><field column="UserAgentID" fieldcaption="UserAgentID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="e071be4f-6728-4132-b362-ef8381d36d4c" ismacro="false" hasdependingfields="false" dependsonanotherfield="false" translatefield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="UserAgentString" fieldcaption="UserAgentString" visible="true" columntype="longtext" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="487b63a0-d2a3-4871-b377-adc9737c7eaa" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false" translatefield="false"><settings><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><FilterMode>False</FilterMode><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching><IsTextArea>True</IsTextArea><controlname>textareacontrol</controlname></settings></field><field column="UserAgentActiveContactID" fieldcaption="UserAgentActiveContactID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="9e416ad8-c509-4b9a-92b5-276aa9bb4e6f" ismacro="false" hasdependingfields="false" dependsonanotherfield="false" translatefield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserAgentOriginalContactID" fieldcaption="UserAgentOriginalContactID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4ee1b681-4b56-4eda-a877-107be04d84d1" ismacro="false" hasdependingfields="false" dependsonanotherfield="false" translatefield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="UserAgentCreated" fieldcaption="UserAgentCreated" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a1b1df83-8077-4cc3-8013-73f45d43247c" ismacro="false" hasdependingfields="false" dependsonanotherfield="false" translatefield="false"><settings><controlname>calendarcontrol</controlname></settings></field></form>' WHERE [ClassName] = 'OM.UserAgent'
GO


/********************* END UPDATE CONTACT MANAGEMENT ***********************/


/********************* UPDATE MVT ***********************/
UPDATE CMS_Query
   SET QueryText = 'IF (@MVTVariantDocumentID = 0)
BEGIN
  SET @MVTVariantDocumentID = NULL;
END
   
SELECT *
FROM OM_MVTCombination
WHERE
  (MVTCombinationPageTemplateID = @MVTCombinationPageTemplateID)
  AND 
  ((MVTCombinationDocumentID IS NULL) OR (MVTCombinationDocumentID = COALESCE(@MVTVariantDocumentID, MVTCombinationDocumentID)))
  AND 
  MVTCombinationID NOT IN
  (
    SELECT MVTCombinationID
    FROM OM_MVTCombinationVariation
    WHERE
      MVTVariantID IN
      (
        SELECT MVTVariantID
        FROM OM_MVTVariant
        WHERE
          MVTVariantInstanceGUID = @MVTVariantInstanceGUID
      )
  )'
 WHERE QueryName = 'GetCombinationsWithoutWebpart';

UPDATE CMS_Query
   SET QueryText = 'IF (@MVTVariantDocumentID = 0)
BEGIN
  SET @MVTVariantDocumentID = NULL;
END
   
SELECT *
FROM OM_MVTCombination
WHERE
  (MVTCombinationPageTemplateID = @MVTCombinationPageTemplateID)
  AND
  ((MVTCombinationDocumentID IS NULL) OR (MVTCombinationDocumentID = COALESCE(@MVTVariantDocumentID, MVTCombinationDocumentID)))
  AND
  MVTCombinationID NOT IN
  (
    SELECT MVTCombinationID
    FROM OM_MVTCombinationVariation
    WHERE
      MVTVariantID IN
      (
        SELECT MVTVariantID
        FROM OM_MVTVariant
        WHERE
          (MVTVariantZoneID = @MVTVariantZoneID) AND
          (MVTVariantPageTemplateID= @MVTCombinationPageTemplateID)
      )
  )'
 WHERE QueryName = 'GetCombinationsWithoutZone'; 

/********************* END UPDATE MVT ***********************/


/****** UPDATE DELETE WEB ANALYTICS FUNCTION ****************/


UPDATE CMS_Query SET QueryText = 
'
    DECLARE @Year1Start datetime;
    DECLARE @Year1End datetime;
    DECLARE @Year2Start datetime;
    DECLARE @Year2End datetime;
    
    DECLARE @Week1Start datetime;
    DECLARE @Week1End datetime;
    DECLARE @Week2Start datetime;
    DECLARE @Week2End datetime;
    
    DECLARE @Month1Start datetime;
    DECLARE @Month1End datetime;
    DECLARE @Month2Start datetime;
    DECLARE @Month2End datetime;
    		
	-- Trim years
	SET @Year1Start = {%DatabaseSchema%}.Func_Analytics_DateTrim (@From ,''year'');
	SET @Year1End = {%DatabaseSchema%}.Func_Analytics_EndDateTrim (@From ,''year'');
	SET @Year2Start = {%DatabaseSchema%}.Func_Analytics_DateTrim (@To ,''year'');
	SET @Year2End = {%DatabaseSchema%}.Func_Analytics_EndDateTrim (@To ,''year'');	
	
	-- Trim months
	SET @Month1Start = {%DatabaseSchema%}.Func_Analytics_DateTrim (@From ,''month'');
	SET @Month1End = {%DatabaseSchema%}.Func_Analytics_EndDateTrim (@From ,''month'');
	SET @Month2Start = {%DatabaseSchema%}.Func_Analytics_DateTrim (@To ,''month'');
	SET @Month2End = {%DatabaseSchema%}.Func_Analytics_EndDateTrim (@To ,''month'');	
	
	-- Trim week
	SET @Week1Start = {%DatabaseSchema%}.Func_Analytics_DateTrim (@From ,''week'');
	SET @Week1End = {%DatabaseSchema%}.Func_Analytics_EndDateTrim (@From ,''week'');
	SET @Week2Start = {%DatabaseSchema%}.Func_Analytics_DateTrim (@To ,''week'');
	SET @Week2End = {%DatabaseSchema%}.Func_Analytics_EndDateTrim (@To ,''week'');	


SET NOCOUNT ON;
	DECLARE @HitsStatID int;
	DECLARE @Cnt int;
	DECLARE @CntL int;
	DECLARE @CntM int;
	DECLARE @CntR int;
       	DECLARE @ValL int;
	DECLARE @ValM int;
	DECLARE @ValR int;
	DECLARE @hitsID int;
	DECLARE @hitsCount int;
        DECLARE @hitsValue int;
	DECLARE mycursor CURSOR FOR SELECT HitsStatisticsID FROM Analytics_Statistics, Analytics_DayHits
		  WHERE (StatisticsSiteID=@SiteID OR @SiteID = 0) AND  (##WHERE##) AND
		  StatisticsID=HitsStatisticsID AND HitsStartTime >= @From AND
		  @To >= HitsEndTime
	OPEN mycursor;
	FETCH NEXT FROM mycursor INTO @HitsStatID
	WHILE @@FETCH_STATUS = 0
	BEGIN

-- WEEKS
    IF (@Week1End < @To)
    BEGIN
        SET @CntL = 0;
		SELECT @CntL = SUM(HitsCount),@ValL = SUM(HitsValue) FROM Analytics_Statistics, Analytics_DayHits
			  WHERE (##WHERE##) AND StatisticsID=HitsStatisticsID AND
		              HitsStartTime >= @From AND @Week1End >= HitsEndTime AND StatisticsID = @HitsStatID
		GROUP BY StatisticsID, StatisticsObjectName, StatisticsObjectID, StatisticsObjectCulture, HitsStatisticsID
		SET @hitsID = 0;
		SELECT @hitsID = HitsID, @hitsCount = HitsCount,@hitsValue = HitsValue FROM [Analytics_WeekHits]
		WHERE HitsStatisticsID=@HitsStatID AND HitsStartTime>=@Week1Start AND HitsEndTime<=@Week1End;
		IF @hitsID > 0
		BEGIN
			UPDATE [Analytics_WeekHits] SET HitsCount=(@hitsCount-@CntL), HitsValue=(@hitsValue-@ValL) WHERE HitsID=@hitsID;
		END;
    END
    IF (@Week2Start > @From)
    BEGIN
        SET @CntR = 0;
		SELECT @CntR = SUM(HitsCount),@ValR = SUM(HitsValue) FROM Analytics_Statistics, Analytics_DayHits
			  WHERE (##WHERE##) AND StatisticsID=HitsStatisticsID AND
              HitsStartTime >= @Week2Start AND @To >= HitsEndTime AND
              StatisticsID = @HitsStatID
		GROUP BY StatisticsID, StatisticsObjectName, StatisticsObjectID, StatisticsObjectCulture, HitsStatisticsID
		SET @hitsID = 0;
		SELECT @hitsID = HitsID, @hitsCount = HitsCount,@hitsValue = HitsValue FROM [Analytics_WeekHits]
		WHERE HitsStatisticsID=@HitsStatID AND HitsStartTime>=@Week2Start AND HitsEndTime<=@Week2End;
		IF @hitsID > 0
		BEGIN
			UPDATE [Analytics_WeekHits] SET HitsCount=(@hitsCount-@CntR),HitsValue=(@hitsValue-@ValR) WHERE HitsID=@hitsID;
		END;
    END
    IF (@Week1Start <= @From AND @To <= @Week1End)
    BEGIN
        SET @CntM = 0;
		SELECT @CntM = SUM(HitsCount),@ValM = SUM(HitsValue) FROM Analytics_Statistics, Analytics_DayHits
			  WHERE (##WHERE##)  AND StatisticsID=HitsStatisticsID AND
              HitsStartTime >= @From AND @To >= HitsEndTime AND
              StatisticsID = @HitsStatID
		GROUP BY StatisticsID, StatisticsObjectName, StatisticsObjectID, StatisticsObjectCulture, HitsStatisticsID
		SET @hitsID = 0;
		SELECT @hitsID = HitsID, @hitsCount = HitsCount,@hitsValue = HitsValue  FROM [Analytics_WeekHits]
		WHERE HitsStatisticsID=@HitsStatID AND HitsStartTime>=@Week1Start AND HitsEndTime<=@Week1End;
		IF @hitsID > 0
		BEGIN
			UPDATE [Analytics_WeekHits] SET HitsCount=(@hitsCount-@CntM),HitsValue=(@hitsValue-@ValM) WHERE HitsID=@hitsID;
		END;
    END;
-- MONTHS
    IF (@Month1End < @To)
    BEGIN
        SET @CntL = 0;
		SELECT @CntL = SUM(HitsCount),@ValL = SUM(HitsValue) FROM Analytics_Statistics, Analytics_DayHits
			  WHERE (##WHERE##)  AND StatisticsID=HitsStatisticsID AND
              HitsStartTime >= @From AND @Month1End >= HitsEndTime AND
              StatisticsID = @HitsStatID
		GROUP BY StatisticsID, StatisticsObjectName, StatisticsObjectID, StatisticsObjectCulture, HitsStatisticsID
		SET @hitsID = 0;
		SELECT @hitsID = HitsID, @hitsCount = HitsCount,@hitsValue = HitsValue FROM [Analytics_MonthHits]
		WHERE HitsStatisticsID=@HitsStatID AND HitsStartTime>=@Month1Start AND HitsEndTime<=@Month1End;
		IF @hitsID > 0
		BEGIN
			UPDATE [Analytics_MonthHits] SET HitsCount=(@hitsCount-@CntL),HitsValue=(@hitsValue-@ValL) WHERE HitsID=@hitsID;
		END;
    END
    IF (@Month2Start > @From)
    BEGIN
        SET @CntR = 0;
		SELECT @CntR = SUM(HitsCount),@ValR = SUM(HitsValue) FROM Analytics_Statistics, Analytics_DayHits
			  WHERE (##WHERE##)  AND StatisticsID=HitsStatisticsID AND
              HitsStartTime >= @Month2Start AND @To >= HitsEndTime AND
              StatisticsID = @HitsStatID
		GROUP BY StatisticsID, StatisticsObjectName, StatisticsObjectID, StatisticsObjectCulture, HitsStatisticsID
		SET @hitsID = 0;
		SELECT @hitsID = HitsID, @hitsCount = HitsCount,@hitsValue = HitsValue FROM [Analytics_MonthHits]
		WHERE HitsStatisticsID=@HitsStatID AND HitsStartTime>=@Month2Start AND HitsEndTime<=@Month2End;
		IF @hitsID > 0
		BEGIN
			UPDATE [Analytics_MonthHits] SET HitsCount=(@hitsCount-@CntR),HitsValue=(@hitsValue-@ValR) WHERE HitsID=@hitsID;
		END;
    END
    IF (@Month1Start <= @From AND @To <= @Month1End)
    BEGIN
        SET @CntM = 0;
		SELECT @CntM = SUM(HitsCount),@ValM = SUM(HitsValue) FROM Analytics_Statistics, Analytics_DayHits
			  WHERE (##WHERE##)  AND StatisticsID=HitsStatisticsID AND
              HitsStartTime >= @From AND @To >= HitsEndTime AND
              StatisticsID = @HitsStatID
		GROUP BY StatisticsID, StatisticsObjectName, StatisticsObjectID, StatisticsObjectCulture, HitsStatisticsID
		SET @hitsID = 0;
		SELECT @hitsID = HitsID, @hitsCount = HitsCount,@hitsValue = HitsValue FROM [Analytics_MonthHits]
		WHERE HitsStatisticsID=@HitsStatID AND HitsStartTime>=@Month1Start AND HitsEndTime<=@Month1End;
		IF @hitsID > 0
		BEGIN
			UPDATE [Analytics_MonthHits] SET HitsCount=(@hitsCount-@CntM),HitsValue=(@hitsValue-@ValM) WHERE HitsID=@hitsID;
		END;
    END;
-- YEARS
    IF (@Year1End < @To)
    BEGIN
        SET @CntL = 0;
		SELECT @CntL = SUM(HitsCount),@ValL = SUM(HitsValue) FROM Analytics_Statistics, Analytics_DayHits
			  WHERE (##WHERE##)  AND StatisticsID=HitsStatisticsID AND
              HitsStartTime >= @From AND @Year1End >= HitsEndTime AND
              StatisticsID = @HitsStatID
		GROUP BY StatisticsID, StatisticsObjectName, StatisticsObjectID, StatisticsObjectCulture, HitsStatisticsID
		SET @hitsID = 0;
		SELECT @hitsID = HitsID, @hitsCount = HitsCount,@hitsValue = HitsValue FROM [Analytics_YearHits]
		WHERE HitsStatisticsID=@HitsStatID AND HitsStartTime>=@Year1Start AND HitsEndTime<=@Year1End;
		IF @hitsID > 0
		BEGIN
			UPDATE [Analytics_YearHits] SET HitsCount=(@hitsCount-@CntL),HitsValue=(@hitsValue-@ValL) WHERE HitsID=@hitsID;
		END;
    END
    IF (@Year2Start > @From)
    BEGIN
        SET @CntR = 0;
		SELECT @CntR = SUM(HitsCount),@ValR = SUM(HitsValue) FROM Analytics_Statistics, Analytics_DayHits
			  WHERE (##WHERE##)  AND StatisticsID=HitsStatisticsID AND
              HitsStartTime >= @Year2Start AND @To >= HitsEndTime AND
              StatisticsID = @HitsStatID
		GROUP BY StatisticsID, StatisticsObjectName, StatisticsObjectID, StatisticsObjectCulture, HitsStatisticsID
		SET @hitsID = 0;
		SELECT @hitsID = HitsID, @hitsCount = HitsCount,@hitsValue = HitsValue FROM [Analytics_YearHits]
		WHERE HitsStatisticsID=@HitsStatID AND HitsStartTime>=@Year2Start AND HitsEndTime<=@Year2End;
		IF @hitsID > 0
		BEGIN
			UPDATE [Analytics_YearHits] SET HitsCount=(@hitsCount-@CntR),HitsValue=(@hitsValue-@ValR) WHERE HitsID=@hitsID;
		END;
    END
    IF (@Year1Start <= @From AND @To <= @Year1End)
    BEGIN
        SET @CntM = 0;
		SELECT @CntM = SUM(HitsCount),@ValM = SUM(HitsValue) FROM Analytics_Statistics, Analytics_DayHits
			  WHERE (##WHERE##)  AND StatisticsID=HitsStatisticsID AND
              HitsStartTime >= @From AND @To >= HitsEndTime AND
              StatisticsID = @HitsStatID
		GROUP BY StatisticsID, StatisticsObjectName, StatisticsObjectID, StatisticsObjectCulture, HitsStatisticsID
		SET @hitsID = 0;
		SELECT @hitsID = HitsID, @hitsCount = HitsCount,@hitsValue = HitsValue FROM [Analytics_YearHits]
		WHERE HitsStatisticsID=@HitsStatID AND HitsStartTime>=@Year1Start AND HitsEndTime<=@Year1End;
		IF @hitsID > 0
		BEGIN
			UPDATE [Analytics_YearHits] SET HitsCount=(@hitsCount-@CntM),HitsValue=(@hitsValue-@ValM) WHERE HitsID=@hitsID;
		END;
    END;
	    DELETE FROM [Analytics_HourHits] WHERE 
	      HitsStatisticsID=@HitsStatID AND HitsStartTime>=@From AND HitsEndTime<=@To;
	    DELETE FROM [Analytics_DayHits] WHERE 
	      HitsStatisticsID=@HitsStatID AND HitsStartTime>=@From AND HitsEndTime<=@To;
	    IF (@From <= @Week1End AND @Week2Start <= @To)
	    BEGIN
	    DELETE FROM [Analytics_WeekHits] WHERE 
	      HitsStatisticsID=@HitsStatID AND HitsStartTime>=@Week1End AND HitsEndTime<=@Week2Start;
	    END;
	    IF (@From <= @Month1End AND @Month2Start <= @To)
	    BEGIN
	    DELETE FROM [Analytics_MonthHits] WHERE 
	      HitsStatisticsID=@HitsStatID AND HitsStartTime>=@Month1End AND HitsEndTime<=@Month2Start;
	    END;    
	    IF (@From <= @Year1End AND @Year2Start <= @To)
	    BEGIN
	    DELETE FROM [Analytics_YearHits] WHERE 
	      HitsStatisticsID=@HitsStatID AND HitsStartTime>=@Year1End AND HitsEndTime<=@Year2Start;
	    END;    
		FETCH NEXT FROM mycursor INTO @HitsStatID
      END
	DEALLOCATE mycursor;
	-- Delete zero stats
	DELETE FROM [Analytics_HourHits] WHERE HitsCount <= 0
	DELETE FROM [Analytics_DayHits] WHERE HitsCount <= 0
	DELETE FROM [Analytics_MonthHits] WHERE HitsCount <= 0
	DELETE FROM [Analytics_WeekHits] WHERE HitsCount <= 0
	DELETE FROM [Analytics_YearHits] WHERE HitsCount <= 0
	DECLARE @stat TABLE (
	  StatisticsID int
	)
	-- Get stats ID with no stats
	INSERT INTO @stat SELECT StatisticsID FROM (
	(SELECT StatisticsID FROM [Analytics_Statistics] WHERE StatisticsID NOT IN (SELECT HitsStatisticsID FROM [Analytics_HourHits])) UNION
	(SELECT StatisticsID FROM [Analytics_Statistics] WHERE StatisticsID NOT IN (SELECT HitsStatisticsID FROM [Analytics_DayHits])) UNION
	(SELECT StatisticsID FROM [Analytics_Statistics] WHERE StatisticsID NOT IN (SELECT HitsStatisticsID FROM [Analytics_WeekHits])) UNION
	(SELECT StatisticsID FROM [Analytics_Statistics] WHERE StatisticsID NOT IN (SELECT HitsStatisticsID FROM [Analytics_MonthHits])) UNION
	(SELECT StatisticsID FROM [Analytics_Statistics] WHERE StatisticsID NOT IN (SELECT HitsStatisticsID FROM [Analytics_YearHits]))
	) as tab
	-- Remove dependencies
	DELETE FROM [Analytics_HourHits] WHERE HitsStatisticsID IN (SELECT StatisticsID FROM @stat)
	DELETE FROM [Analytics_DayHits] WHERE HitsStatisticsID IN (SELECT StatisticsID FROM @stat)
	DELETE FROM [Analytics_WeekHits] WHERE HitsStatisticsID IN (SELECT StatisticsID FROM @stat)
	DELETE FROM [Analytics_MonthHits] WHERE HitsStatisticsID IN (SELECT StatisticsID FROM @stat)
	DELETE FROM [Analytics_YearHits] WHERE HitsStatisticsID IN (SELECT StatisticsID FROM @stat)
	-- Remove master record
	DELETE FROM [Analytics_Statistics] WHERE StatisticsID IN (SELECT StatisticsID FROM @stat)
'

WHERE QueryNAme = 'removeanalyticsdata'
  AND ClassID IN (SELECT TOP 1 ClassID FROM CMS_Class WHERE ClassName ='analytics.statistics')

/******** END UPDATE DELETE WEB ANALYTICS FUNCTION ******/

/**** UPDATE TOOLS WEB ANALYTICS VISITORS LINK *****/

UPDATE [CMS_UIElement] 
SET ElementTargetURL = '~/CMSModules/WebAnalytics/Tools/default.aspx?node=visitfirst'
where elementname = 'WebAnalytics.ViewVisits'

/**** END UPDATE TOOLS WEB ANALYTICS VISITORS LINK *****/

/******** BEGIN UPDATE POLLS ******/

UPDATE CMS_Query SET [QueryText]='SELECT * FROM Polls_Poll WHERE (PollID IN (SELECT PollID FROM Polls_PollSite WHERE SiteID=@SiteID) OR PollID IN (SELECT PollID FROM Polls_Poll WHERE PollSiteID=@SiteID)) ORDER BY ##ORDERBY##'
WHERE ClassID IN (SELECT ClassID FROM CMS_Class WHERE ClassName LIKE 'polls.poll') AND QueryName='selectallofsite'

/******** END UPDATE POLLS ******/

/******** BEGIN UPDATE FORM CONTROLS ******/

DECLARE @resourceId AS int;

-- Get 'Community' resource ID
SET @resourceId = (SELECT ResourceID FROM [CMS_Resource] WHERE ResourceName = N'CMS.Community');
IF  @resourceId IS NOT NULL
BEGIN
-- Update form controls
UPDATE [CMS_FormUserControl] SET [UserControlResourceID]=@resourceId WHERE UserControlCodeName IN ('DepartmentSectionsManager', 'DepartmentRolesSelector')
END

GO

/******** END UPDATE FORM CONTROLS ******/


/****** UPDATE WRONG ORDER BY STATEMENT IN TABLE QUERIES *******/
UPDATE Reporting_ReportTable SET TableQuery = REPLACE (TableQuery,'ORDER BY ''{$om.total$}''','ORDER BY [{$om.total$}]')
WHERE TableQuery LIKE '%ORDER BY ''%'

UPDATE Reporting_ReportTable SET TableQuery = REPLACE (TableQuery,'ORDER BY ''{%ColumnName|(default)Hits%}''','ORDER BY [{%ColumnName|(default)Hits%}]')
WHERE TableQuery LIKE '%ORDER BY ''%'

/******* END UPDATE WRONG ORDER BY STATEMENT ********/

/****** UPDATE PROCEDURE FOR BACKWARD COMPATIBILITY OF UI ELEMENTS *******/


GO

ALTER PROCEDURE [Proc_CMS_UIElement_UpdateAfterImport] 
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

GO

/****** END PROCEDURE FOR BACKWARD COMPATIBILITY OF UI ELEMENTS *******/


/******** UPDATE WEB ANALYTICS REPORTS ******/

DECLARE @newDefaultSiteID varchar(100);
SET @newDefaultSiteID = '<field column="SiteID" visible="false" defaultvalue="{%CurrentSite.SiteID%}"';

UPDATE Reporting_Report
SET ReportParameters = REPLACE(
							REPLACE(
								ReportParameters,
								'<field column="SiteID" visible="false" columntype',
								@newDefaultSiteID + ' columntype'),
							'<field column="SiteID" visible="false" defaultvalue="0"',
							@newDefaultSiteID)
WHERE
	(ReportName IN (
		'campaigncompare.dayreport',
		'campaigncompare.hourreport',
		'campaigncompare.monthreport',
		'campaigncompare.weekreport',
		'campaigncompare.yearreport')
	)
	
GO

/******** END UPDATE WEB ANALYTICS REPORTS ******/

/******** BEGIN INSERT REMOVE ALL ANALYTICS DATA ********/

DECLARE @AnalyticsClassID INT;
DECLARE @RemoveAllID INT;
DECLARE @QueryRemoveAllAnalytics NVARCHAR(MAX)
SELECT @AnalyticsClassID =  ClassID FROM CMS_Class WHERE ClassName = 'analytics.statistics'
SET @QueryRemoveAllAnalytics = 
'
DELETE FROM Analytics_HourHits  WHERE HitsStatisticsID IN (
SELECT StatisticsID FROM Analytics_Statistics WHERE (##WHERE##) AND (StatisticsSiteID = @SiteID OR @SiteID = 0))    

DELETE FROM Analytics_DayHits  WHERE HitsStatisticsID IN (
SELECT StatisticsID FROM Analytics_Statistics WHERE (##WHERE##) AND (StatisticsSiteID = @SiteID OR @SiteID = 0))    

DELETE FROM Analytics_WeekHits  WHERE  HitsStatisticsID IN (
SELECT StatisticsID FROM Analytics_Statistics WHERE (##WHERE##) AND (StatisticsSiteID = @SiteID OR @SiteID = 0))    

DELETE FROM Analytics_MonthHits  WHERE HitsStatisticsID IN (
SELECT StatisticsID FROM Analytics_Statistics WHERE (##WHERE##) AND (StatisticsSiteID = @SiteID OR @SiteID = 0))    

DELETE FROM Analytics_YearHits  WHERE HitsStatisticsID IN (
SELECT StatisticsID FROM Analytics_Statistics WHERE (##WHERE##) AND (StatisticsSiteID = @SiteID OR @SiteID = 0))    

DELETE FROM Analytics_Statistics WHERE 
    StatisticsID NOT IN (SELECT HitsStatisticsID FROM Analytics_YearHits)   
AND StatisticsID NOT IN (SELECT HitsStatisticsID FROM Analytics_MonthHits)  
AND StatisticsID NOT IN (SELECT HitsStatisticsID FROM Analytics_WeekHits)   
AND StatisticsID NOT IN (SELECT HitsStatisticsID FROM Analytics_DayHits)  
AND StatisticsID NOT IN (SELECT HitsStatisticsID FROM Analytics_HourHits)
'

SELECT @RemoveAllID = QueryID FROM CMS_Query WHERE ClassID = @AnalyticsClassID AND @AnalyticsClassID <> 0 AND QueryName = 'removeAllSiteAnalyticsData'

IF (@RemoveAllID IS NULL)
BEGIN
INSERT INTO CMS_Query (QueryName,QueryTypeID,QueryText,QueryRequiresTransaction,ClassID,QueryIsLocked,QueryLastModified,QueryGUID,
QueryLoadGeneration,QueryIsCustom) VALUES 
('removeAllSiteAnalyticsData',0, @QueryRemoveAllAnalytics
,
0,@AnalyticsClassID,0,getdate(),newid(),0,NULL);
END 
ELSE 
BEGIN
	UPDATE CMS_Query  SET QueryText = @QueryRemoveAllAnalytics WHERE 
	ClassID = @AnalyticsClassID AND @AnalyticsClassID <> 0 AND QueryName = 'removeAllSiteAnalyticsData'
END
GO


/******** END INSERT REMOVE ALL ANALYTICS DATA ********/

/******** BEGIN UPDATE FORM CONTROLS ******/

UPDATE [CMS_FormUserControl] SET [UserControlType]=0 WHERE UserControlCodeName IN ('AllowedExtensionsSelector', 'PasswordStrength')
UPDATE [CMS_FormUserControl] SET [UserControlType]=2 WHERE UserControlCodeName IN ('CategorySelector', 'DateIntervalSelector')
UPDATE [CMS_FormUserControl] SET [UserControlDisplayName]='Date & time filter', [UserControlShowInCustomTables] = 1, [UserControlShowInBizForms] = 1, [UserControlDefaultDataType] = 'DateTime' WHERE UserControlCodeName = N'DateTimeFilter'

GO

/******** END UPDATE FORM CONTROLS ******/

/******** BEGIN UPDATE PROCEDURES ******/


-- Create Proc_CMS_State_RemoveDependencies
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE [ROUTINE_NAME]='Proc_CMS_State_RemoveDependencies' AND [ROUTINE_TYPE]='PROCEDURE')
DROP PROCEDURE [Proc_CMS_State_RemoveDependencies]
GO

CREATE PROCEDURE [Proc_CMS_State_RemoveDependencies]
 @ID int
AS
BEGIN
 BEGIN TRANSACTION;
	-- On-line marketing
	UPDATE OM_Contact SET ContactStateID=NULL WHERE ContactStateID=@ID
	UPDATE OM_Account SET AccountStateID=NULL WHERE AccountStateID=@ID
 COMMIT TRANSACTION;
END
GO


-- Update Proc_Newsletter_Link_Log
ALTER PROCEDURE [Proc_Newsletter_Link_Log]
 @LinkGUID uniqueidentifier,
 @SubscriberGUID uniqueidentifier,	    
 @SiteID int
AS
BEGIN
 SET NOCOUNT ON;

  BEGIN TRANSACTION

  DECLARE @LinkID AS int
  SET @LinkID = (SELECT LinkID 
                   FROM Newsletter_Link INNER JOIN Newsletter_NewsletterIssue   
                     ON Newsletter_Link.LinkIssueID = Newsletter_NewsletterIssue.IssueID
                  WHERE Newsletter_NewsletterIssue.IssueSiteID = @SiteID AND Newsletter_Link.LinkGUID = @LinkGUID)

  DECLARE @SubscriberID AS int
  SET @SubscriberID = (SELECT SubscriberID FROM Newsletter_Subscriber WHERE SubscriberGUID = @SubscriberGUID AND SubscriberSiteID = @SiteID)
  
  DECLARE @SubscriberType AS nvarchar(30)
  SET @SubscriberType = (SELECT SubscriberType FROM Newsletter_Subscriber WHERE SubscriberID = @SubscriberID)

  IF (NOT @LinkID IS NULL AND NOT @SubscriberID IS NULL)
    BEGIN

      -- Increment click counters

      DECLARE @LinkTotalClicks AS int      
      SET @LinkTotalClicks = (SELECT COALESCE(LinkTotalClicks, 0) FROM Newsletter_Link WHERE LinkID = @LinkID)
      SET @LinkTotalClicks = @LinkTotalClicks + 1      
      UPDATE Newsletter_Link SET LinkTotalClicks = @LinkTotalClicks WHERE LinkID = @LinkID

      -- Do not increment clicks for contact group subscriber, clicks are obtained from activities

      IF (NOT @SubscriberType LIKE 'om.contactgroup' OR @SubscriberType IS NULL)
	BEGIN
	  DECLARE @Clicks AS int
	  SET @Clicks = (SELECT Clicks FROM Newsletter_SubscriberLink WHERE LinkID = @LinkID AND SubscriberID = @SubscriberID)

	  IF (NOT @Clicks IS NULL)
		BEGIN
		  SET @Clicks = @Clicks + 1
		  UPDATE Newsletter_SubscriberLink SET Clicks = @Clicks WHERE LinkID = @LinkID AND SubscriberID = @SubscriberID
		END
	  ELSE
		INSERT INTO Newsletter_SubscriberLink VALUES (@SubscriberID, @LinkID, 1)

	  -- If opened e-mail tracking is enabled, log another open e-mail

      	  DECLARE @IssueID AS int
      	  SET @IssueID = (SELECT LinkIssueID FROM Newsletter_Link WHERE LinkID = @LinkID)           
      
      	  DECLARE @OpenEmailTrackingEnabled AS bit
      	  SET @OpenEmailTrackingEnabled = (SELECT NewsletterTrackOpenEmails FROM Newsletter_Newsletter WHERE NewsletterID = 
									   (SELECT IssueNewsletterID FROM Newsletter_NewsletterIssue WHERE IssueID = @IssueID))
      
      	  IF (@OpenEmailTrackingEnabled = 1)
		EXEC Proc_Newsletter_OpenedEmail_Log_Internal @SubscriberID, @IssueID
                
	END

      -- Return URL of the link

      SELECT LinkTarget FROM Newsletter_Link WHERE LinkID = @LinkID
            
    END
  COMMIT TRANSACTION
END
GO

-- Update Proc_CMS_UICulture_RemoveDependencies
ALTER PROCEDURE [Proc_CMS_UICulture_RemoveDependencies]
	@ID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	BEGIN TRANSACTION;
	-- CMS_ResourceTranslation
    DELETE FROM [CMS_ResourceTranslation] WHERE TranslationUICultureID=@ID;
    -- Update Users' preferred culture
    UPDATE CMS_User SET PreferredUICultureCode = null WHERE PreferredUICultureCode IN (SELECT UICultureCode FROM CMS_UICulture WHERE UICultureID = @ID);
    
	COMMIT TRANSACTION;
END
GO

-- Update Proc_CMS_Category_UpdateNamePath
ALTER PROCEDURE [Proc_CMS_Category_UpdateNamePath]
	@OldParentID int,
	@NewParentID int
AS
BEGIN
	DECLARE @OldIdPrefix NVARCHAR(450);
    	DECLARE @OldPrefix NVARCHAR(1500);
	DECLARE @NewPrefix NVARCHAR(1500);
	DECLARE @Name NVARCHAR(250);
	DECLARE @OldLevel INT;
	DECLARE @NewLevel INT;

	SET @OldIdPrefix = (SELECT TOP 1 CategoryIDPath FROM CMS_Category WHERE CategoryID = @OldParentID);
	SET @OldPrefix = (SELECT TOP 1 CategoryNamePath FROM CMS_Category WHERE CategoryID = @OldParentID);
	SET @Name = (SELECT TOP 1 CategoryDisplayName FROM CMS_Category WHERE CategoryID = @OldParentID);
	SET @NewPrefix = (SELECT TOP 1 CategoryNamePath FROM CMS_Category WHERE CategoryID = @NewParentID);
	SET @OldLevel = (SELECT TOP 1 CategoryLevel FROM CMS_Category WHERE CategoryID = @OldParentID);
	SET @NewLevel = (SELECT TOP 1 CategoryLevel FROM CMS_Category WHERE CategoryID = @NewParentID);

    -- UPDATE NAME PATH
	UPDATE CMS_Category SET CategoryLevel = CategoryLevel - @OldLevel + ISNULL(@NewLevel, -1) + 1, CategoryNamePath = ISNULL(@NewPrefix, '') + '/' + @Name + SUBSTRING(CategoryNamePath, LEN(@OldPrefix)+1, LEN(CategoryNamePath)) WHERE CategoryIDPath LIKE @OldIdPrefix + '%'

END
GO

/******** END UPDATE PROCEDURES ******/


/******** BEGIN UPDATE QUERIES ******/

-- Create remove dependencies query for CMS_State
DECLARE @StateClassID int;
SELECT @StateClassID=ClassID FROM CMS_Class WHERE ClassName = 'cms.state'
IF (@StateClassID > 0) AND NOT EXISTS(SELECT QueryID FROM CMS_Query WHERE ClassID=@StateClassID AND QueryName='removedependencies' )
BEGIN
INSERT INTO CMS_Query ([QueryName], [QueryTypeID], [QueryText], [QueryRequiresTransaction], [ClassID], [QueryIsLocked],
  [QueryLastModified], [QueryGUID], [QueryLoadGeneration], [QueryIsCustom] ) 
  VALUES ( 'removedependencies', 1, 'Proc_CMS_State_RemoveDependencies', 0, @StateClassID, 0, GETDATE(), NEWID(), 0, 0)
END

UPDATE [CMS_Query]
   SET [QueryText] = 'SELECT ##TOPN## ##COLUMNS## FROM CMS_VersionHistory LEFT JOIN CMS_Class ON CMS_Class.ClassID = VersionClassID
WHERE (VersionDeletedWhen IS NOT NULL) AND (NodeSiteID=@SiteID OR NodeSiteID IS NULL OR @SiteID = 0) AND ##WHERE## 
ORDER BY ##ORDERBY##'
 WHERE [QueryName] = 'selectrecyclebin' AND [ClassID] IN (SELECT [ClassID] FROM [CMS_Class] WHERE [ClassName] = 'cms.versionhistory')
 
 
-- Update query newsletter.openedemail.selectallissueopeners
 UPDATE [CMS_Query] SET QueryText=
'SELECT ##TOPN## ##COLUMNS## FROM
(SELECT [IssueID]
      ,[SubscriberID]
      ,[SubscriberFullName]
      ,[SubscriberEmail]
      ,[OpenedWhen]
      ,[SiteID]
  FROM [View_Newsletter_IssueOpenedBy]
  WHERE [IssueID]=@IssueID
UNION
SELECT [ActivityItemDetailID] AS [IssueID]
      ,0 AS [SubscriberID]
      ,''Contact ''''''+[ContactFirstName]+'' ''+[ContactLastName]+'''''''' AS [SubscriberFullName]
      ,[ContactEmail] AS [SubscriberEmail]
      ,[ActivityCreated] AS [OpenedWhen]
      ,[ActivitySiteID] AS [SiteID]
  FROM [View_OM_Contact_Activity] AS [Activity1]
  WHERE (ActivityType = ''newsletteropen'' AND ActivityItemDetailID = @IssueID AND ActivityValue LIKE ''contactgroup%''
  AND ActivityCreated = (SELECT MIN(ActivityCreated) FROM [OM_Activity] AS [Activity2] WHERE (ActivityType = ''newsletteropen'' AND ActivityItemDetailID = @IssueID
	AND ActivityValue LIKE ''contactgroup%'' AND [Activity1].ActivityActiveContactID = [Activity2].ActivityActiveContactID)))) AS [OpenedBy]
WHERE ##WHERE## ORDER BY ##ORDERBY##'
WHERE [ClassID] IN (SELECT [ClassID] FROM [CMS_Class] WHERE [ClassName] LIKE 'newsletter.openedemail') AND QueryName='selectallIssueOpeners'


-- Update query newsletter.link.selectstatistics
 UPDATE [CMS_Query] SET QueryText=
'SELECT ##TOPN## ##COLUMNS## FROM 
(
SELECT Newsletter_Link.LinkID,
       Newsletter_Link.LinkTarget,
       Newsletter_Link.LinkDescription,
       Newsletter_Link.LinkOutdated,
       ISNULL(Clicks, 0) AS UniqueClicks,
       ISNULL(Newsletter_Link.LinkTotalClicks, 0) AS TotalClicks,
       (ISNULL(Clicks, 0) / NULLIF(IssueSentEmails, 0)) * 100 AS ClickRate,
       Newsletter_Link.LinkIssueID AS IssueID,
       Issues.IssueSiteID AS SiteID
  FROM Newsletter_Link
	LEFT OUTER JOIN
	(
	-- Select unique clicks for links
	SELECT LinkID, CAST(SUM(Clicks) AS float) AS Clicks FROM 
	(
	 -- Select subscribers'' clicks
	 SELECT LinkID, COUNT(Clicks) AS Clicks FROM Newsletter_SubscriberLink GROUP BY LinkID
		UNION ALL
	 -- Select contacts'' clicks
	 SELECT LinkID, Clicks FROM
	(SELECT COUNT(ActivityActiveContactID) AS Clicks, ActivityURL AS Link, ActivityItemDetailID AS IssueID FROM (SELECT ActivityActiveContactID, ActivityURL, ActivityItemDetailID
	  FROM [OM_Activity]
	  WHERE (ActivityType = ''newsletterclickthrough'') AND (ActivityValue LIKE ''contactgroup%'')
	  GROUP BY ActivityActiveContactID, ActivityURL, ActivityItemDetailID) AS [ContactClickCount]
	  GROUP BY ActivityURL, ActivityItemDetailID) AS [ContactClicks], [Newsletter_Link]
	  WHERE (LinkTarget = [ContactClicks].Link AND LinkIssueID = [ContactClicks].IssueID)
	) AS [AllClicks]
	GROUP BY LinkID

	) AS Links ON Links.LinkID = Newsletter_Link.LinkID
	LEFT OUTER JOIN
        (SELECT IssueID, IssueSentEmails, IssueSiteID FROM Newsletter_NewsletterIssue) AS Issues ON Issues.IssueID = Newsletter_Link.LinkIssueID
) AS [ClickStatistic]
WHERE ##WHERE## ORDER BY ##ORDERBY##'
WHERE [ClassID] IN (SELECT [ClassID] FROM [CMS_Class] WHERE [ClassName] LIKE 'newsletter.link') AND QueryName='selectstatistics'


-- Update query newsletter.subscriberlink.selectAllLinkClickers
 UPDATE [CMS_Query] SET QueryText=
'SELECT ##TOPN## ##COLUMNS## FROM
(SELECT [LinkID]
      ,[SubscriberID]
      ,[SubscriberFullName]
      ,[SubscriberEmail]
      ,[Clicks]
      ,[SiteID]
  FROM [View_Newsletter_LinkClickedBy]
UNION
SELECT [LinkID]
      ,0 AS [SubscriberID]
      ,''Contact ''''''+[ContactFirstName]+'' ''+[ContactLastName]+'''''''' AS [SubscriberFullName]
      ,[ContactEmail] AS [SubscriberEmail]
      ,[Clicks]
      ,[ActivitySiteID] AS [SiteID]
  FROM 
  (
	SELECT [ContactFirstName],[ContactLastName],[ContactEmail],COUNT([ActivityID]) AS [Clicks],[ActivitySiteID],[ActivityURL],[ActivityItemDetailID]
		FROM [View_OM_Contact_Activity]
		WHERE (ActivityType = ''newsletterclickthrough'') AND (ActivityValue LIKE ''contactgroup%'')
		GROUP BY [ContactFirstName],[ContactLastName],[ContactEmail],[ActivitySiteID],[ActivityURL],[ActivityItemID],[ActivityItemDetailID]
  ) AS [ActionClicks], [Newsletter_Link]
  WHERE (LinkTarget=ActivityURL AND LinkIssueID=ActivityItemDetailID)) AS [ClickedBy]
WHERE ##WHERE## ORDER BY ##ORDERBY##'
WHERE [ClassID] IN (SELECT [ClassID] FROM [CMS_Class] WHERE [ClassName] LIKE 'newsletter.subscriberlink') AND QueryName='selectAllLinkClickers'


-- Create new query newsletter.openedemail.selectcontactopens
DECLARE @OpenedEmailClassID int;
SELECT @OpenedEmailClassID=ClassID FROM CMS_Class WHERE ClassName = 'newsletter.openedemail'
IF (@OpenedEmailClassID > 0) AND NOT EXISTS(SELECT QueryID FROM CMS_Query WHERE ClassID=@OpenedEmailClassID AND QueryName='selectcontactopens' )
BEGIN
INSERT INTO CMS_Query ([QueryName], [QueryTypeID], [QueryText], [QueryRequiresTransaction], [ClassID], [QueryIsLocked],
  [QueryLastModified], [QueryGUID], [QueryLoadGeneration], [QueryIsCustom] ) 
  VALUES ( 'selectcontactopens', 0,
'SELECT COUNT(ActivityActiveContactID) FROM (SELECT ActivityActiveContactID
  FROM [OM_Activity]
  WHERE (ActivityType = ''newsletteropen'') AND (ActivityItemDetailID = @IssueID) AND (ActivityValue LIKE ''contactgroup%'')
  GROUP BY ActivityActiveContactID) AS [OpenEmailCount]',
  0, @OpenedEmailClassID, 0, GETDATE(), NEWID(), 2, NULL)
END
GO

/********************* UPDATE PIVOT *******************************************/

ALTER PROCEDURE [Proc_Analytics_Pivot]
@Type nvarchar(5),
@StaticColumns nvarchar(MAX) = ''
AS
BEGIN
	IF  OBJECT_ID('tempdb..#AnalyticsTempTable') IS NULL
	BEGIN
	   RETURN;
    END;

	IF ((SELECT COUNT (*) FROM #AnalyticsTempTable WHERE Name IS NOT NULL) > 0)
	BEGIN
        
		------ Get all columns for PIVOT functions
		DECLARE @Columns NVARCHAR(MAX)
		DECLARE @ColumnsNull NVARCHAR (MAX)

		---- Columns for pivot
		SELECT @Columns = COALESCE(@Columns + ',[' + CAST(Name AS VARCHAR(MAX)) + ']',
			'[' + CAST(Name AS VARCHAR(MAX))+ ']')
			FROM #AnalyticsTempTable
			GROUP BY Name ORDER BY Name

		---- Columns for ISNULL function
		SELECT @ColumnsNull = COALESCE(@ColumnsNull + ',ISNULL([' + CAST(Name AS VARCHAR(MAX)) + '],0) AS '''+CAST(Name AS VARCHAR(MAX))+'''',
			'ISNULL([' + CAST(Name AS VARCHAR(MAX))+ '],0) AS '''+ CAST(Name AS VARCHAR(MAX))+'''')
			FROM #AnalyticsTempTable
			GROUP BY Name ORDER BY Name
			
		------ Pivot function
		DECLARE @Query NVARCHAR(MAX)
		IF (@Type ='week')
		BEGIN
			SET @Query = 'SELECT  CONVERT (NVARCHAR(2),DATEPART(wk,[StartTime]))+''/''+CONVERT (NVARCHAR(4),DATEPART(YEAR,[StartTime])), '+  @Columns + @StaticColumns + ' FROM #AnalyticsTempTable PIVOT ( SUM(Hits) FOR [Name] IN (' + @Columns + ') ) AS p'-- ORDER BY StartTime'
		END
		ELSE
		BEGIN
			SET @Query = 'SELECT  StartTime, '+  @Columns + @StaticColumns + '  FROM #AnalyticsTempTable PIVOT ( SUM(Hits) FOR [Name] IN (' + @Columns + ') ) AS p ORDER BY StartTime'
		END
		
		EXECUTE(@Query)		
		
	END
	ELSE 
    BEGIN 
		IF (@Type ='week')
		BEGIN
			 EXECUTE('SELECT CONVERT (NVARCHAR(2),DATEPART(wk,[StartTime]))+''/''+CONVERT (NVARCHAR(4),DATEPART(YEAR,[StartTime])),Hits '+ @StaticColumns  +' FROM #AnalyticsTempTable');
		END
		ELSE
		BEGIN
			EXECUTE('SELECT StartTime,Hits'+@StaticColumns+' FROM #AnalyticsTempTable')
		END
	END
END
GO

/**********************END UPDATE PIVOT ***************************************/



/******** END UPDATE QUERIES ******/



/******** BEGIN UPDATE INDEXES ******/
IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_OM_Activity_ActivityCreated')
BEGIN
CREATE NONCLUSTERED INDEX [IX_OM_Activity_ActivityCreated] ON [OM_Activity] 
(
	[ActivityCreated] ASC
)
END
GO


IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_OM_Account_AccountCountryID')
BEGIN
CREATE NONCLUSTERED INDEX [IX_OM_Account_AccountCountryID] ON [OM_Account] 
(
	[AccountCountryID]
)
END
GO

IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_OM_Account_AccountGlobalAccountID')
BEGIN
CREATE NONCLUSTERED INDEX [IX_OM_Account_AccountGlobalAccountID] ON [OM_Account] 
(
	[AccountGlobalAccountID]
)
END
GO

IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_OM_Account_AccountMergedWithAccountID')
BEGIN
CREATE NONCLUSTERED INDEX [IX_OM_Account_AccountMergedWithAccountID] ON [OM_Account] 
(
	[AccountMergedWithAccountID]
)
END
GO

IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_OM_Account_AccountOwnerUserID')
BEGIN
CREATE NONCLUSTERED INDEX [IX_OM_Account_AccountOwnerUserID] ON [OM_Account] 
(
	[AccountOwnerUserID]
)
END
GO

IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_OM_Account_AccountPrimaryContactID')
BEGIN
CREATE NONCLUSTERED INDEX [IX_OM_Account_AccountPrimaryContactID] ON [OM_Account] 
(
	[AccountPrimaryContactID]
)
END
GO

IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_OM_Account_AccountSecondaryContactID')
BEGIN
CREATE NONCLUSTERED INDEX [IX_OM_Account_AccountSecondaryContactID] ON [OM_Account] 
(
	[AccountSecondaryContactID]
)
END
GO

IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_OM_Account_AccountSiteID')
BEGIN
CREATE NONCLUSTERED INDEX [IX_OM_Account_AccountSiteID] ON [OM_Account] 
(
	[AccountSiteID]
)
END
GO

IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_OM_Account_AccountStateID')
BEGIN
CREATE NONCLUSTERED INDEX [IX_OM_Account_AccountStateID] ON [OM_Account] 
(
	[AccountStateID]
)
END
GO

IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_OM_Account_AccountStatusID')
BEGIN
CREATE NONCLUSTERED INDEX [IX_OM_Account_AccountStatusID] ON [OM_Account] 
(
	[AccountStatusID]
)
END
GO

IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_OM_Account_AccountSubsidiaryOfID')
BEGIN
CREATE NONCLUSTERED INDEX [IX_OM_Account_AccountSubsidiaryOfID] ON [OM_Account] 
(
	[AccountSubsidiaryOfID]
)
END
GO

IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_OM_AccountContact_AccountID')
BEGIN
CREATE NONCLUSTERED INDEX [IX_OM_AccountContact_AccountID] ON [OM_AccountContact] 
(
	[AccountID]
)
END
GO

IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_OM_AccountContact_ContactID')
BEGIN
CREATE NONCLUSTERED INDEX [IX_OM_AccountContact_ContactID] ON [OM_AccountContact] 
(
	[ContactID]
)
END
GO

IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_OM_AccountContact_ContactRoleID')
BEGIN
CREATE NONCLUSTERED INDEX [IX_OM_AccountContact_ContactRoleID] ON [OM_AccountContact] 
(
	[ContactRoleID]
)
END
GO

IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_OM_AccountStatus_AccountStatusSiteID')
BEGIN
CREATE NONCLUSTERED INDEX [IX_OM_AccountStatus_AccountStatusSiteID] ON [OM_AccountStatus] 
(
	[AccountStatusSiteID]
)
END
GO

IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_OM_Activity_ActivityActiveContactID')
BEGIN
CREATE NONCLUSTERED INDEX [IX_OM_Activity_ActivityActiveContactID] ON [OM_Activity] 
(
	[ActivityActiveContactID]
)
END
GO

IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_OM_Activity_ActivityOriginalContactID')
BEGIN
CREATE NONCLUSTERED INDEX [IX_OM_Activity_ActivityOriginalContactID] ON [OM_Activity] 
(
	[ActivityOriginalContactID]
)
END
GO

IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_OM_Activity_ActivitySiteID')
BEGIN
CREATE NONCLUSTERED INDEX [IX_OM_Activity_ActivitySiteID] ON [OM_Activity] 
(
	[ActivitySiteID]
)
END
GO

IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_OM_Activity_ActivityType')
BEGIN
CREATE NONCLUSTERED INDEX [IX_OM_Activity_ActivityType] ON [OM_Activity] 
(
	[ActivityType]
)
END
GO

IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_OM_Contact_ContactCountryID')
BEGIN
CREATE NONCLUSTERED INDEX [IX_OM_Contact_ContactCountryID] ON [OM_Contact] 
(
	[ContactCountryID]
)
END
GO

IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_OM_Contact_ContactGlobalContactID')
BEGIN
CREATE NONCLUSTERED INDEX [IX_OM_Contact_ContactGlobalContactID] ON [OM_Contact] 
(
	[ContactGlobalContactID]
)
END
GO

IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_OM_Contact_ContactMergedWithContactID')
BEGIN
CREATE NONCLUSTERED INDEX [IX_OM_Contact_ContactMergedWithContactID] ON [OM_Contact] 
(
	[ContactMergedWithContactID]
)
END
GO

IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_OM_Contact_ContactOwnerUserID')
BEGIN
CREATE NONCLUSTERED INDEX [IX_OM_Contact_ContactOwnerUserID] ON [OM_Contact] 
(
	[ContactOwnerUserID]
)
END
GO

IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_OM_Contact_ContactSiteID')
BEGIN
CREATE NONCLUSTERED INDEX [IX_OM_Contact_ContactSiteID] ON [OM_Contact] 
(
	[ContactSiteID]
)
END
GO

IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_OM_Contact_ContactStateID')
BEGIN
CREATE NONCLUSTERED INDEX [IX_OM_Contact_ContactStateID] ON [OM_Contact] 
(
	[ContactStateID]
)
END
GO

IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_OM_Contact_ContactStatusID')
BEGIN
CREATE NONCLUSTERED INDEX [IX_OM_Contact_ContactStatusID] ON [OM_Contact] 
(
	[ContactStatusID]
)
END
GO

IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_OM_ContactGroup_ContactGroupSiteID')
BEGIN
CREATE NONCLUSTERED INDEX [IX_OM_ContactGroup_ContactGroupSiteID] ON [OM_ContactGroup] 
(
	[ContactGroupSiteID]
)
END
GO

IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_OM_ContactGroupMember_ContactGroupMemberContactGroupID')
BEGIN
CREATE NONCLUSTERED INDEX [IX_OM_ContactGroupMember_ContactGroupMemberContactGroupID] ON [OM_ContactGroupMember] 
(
	[ContactGroupMemberContactGroupID]
)
END
GO

IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_OM_ContactGroupMember_ContactGroupMemberRelatedID')
BEGIN
CREATE NONCLUSTERED INDEX [IX_OM_ContactGroupMember_ContactGroupMemberRelatedID] ON [OM_ContactGroupMember] 
(
	[ContactGroupMemberRelatedID]
)
END
GO

IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_OM_ContactRole_ContactRoleSiteID')
BEGIN
CREATE NONCLUSTERED INDEX [IX_OM_ContactRole_ContactRoleSiteID] ON [OM_ContactRole] 
(
	[ContactRoleSiteID]
)
END
GO

IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_OM_ContactStatus_ContactStatusSiteID')
BEGIN
CREATE NONCLUSTERED INDEX [IX_OM_ContactStatus_ContactStatusSiteID] ON [OM_ContactStatus] 
(
	[ContactStatusSiteID]
)
END
GO

IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_OM_IP_IPActiveContactID')
BEGIN
CREATE NONCLUSTERED INDEX [IX_OM_IP_IPActiveContactID] ON [OM_IP] 
(
	[IPActiveContactID]
)
END
GO

IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_OM_IP_IPOriginalContactID')
BEGIN
CREATE NONCLUSTERED INDEX [IX_OM_IP_IPOriginalContactID] ON [OM_IP] 
(
	[IPOriginalContactID]
)
END
GO

IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_OM_Membership_ActiveContactID')
BEGIN
CREATE NONCLUSTERED INDEX [IX_OM_Membership_ActiveContactID] ON [OM_Membership] 
(
	[ActiveContactID]
)
END
GO

IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_OM_Membership_OriginalContactID')
BEGIN
CREATE NONCLUSTERED INDEX [IX_OM_Membership_OriginalContactID] ON [OM_Membership] 
(
	[OriginalContactID]
)
END
GO

IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_OM_Membership_RelatedID')
BEGIN
CREATE NONCLUSTERED INDEX [IX_OM_Membership_RelatedID] ON [OM_Membership] 
(
	[RelatedID]
)
END
GO

IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_OM_PageVisit_PageVisitActivityID')
BEGIN
CREATE NONCLUSTERED INDEX [IX_OM_PageVisit_PageVisitActivityID] ON [OM_PageVisit] 
(
	[PageVisitActivityID]
)
END
GO

IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_OM_Rule_RuleScoreID')
BEGIN
CREATE NONCLUSTERED INDEX [IX_OM_Rule_RuleScoreID] ON [OM_Rule] 
(
	[RuleScoreID]
)
END
GO

IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_OM_Rule_RuleSiteID')
BEGIN
CREATE NONCLUSTERED INDEX [IX_OM_Rule_RuleSiteID] ON [OM_Rule] 
(
	[RuleSiteID]
)
END
GO

IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_OM_Score_ScoreSiteID')
BEGIN
CREATE NONCLUSTERED INDEX [IX_OM_Score_ScoreSiteID] ON [OM_Score] 
(
	[ScoreSiteID]
)
END
GO

IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_OM_Search_SearchActivityID')
BEGIN
CREATE NONCLUSTERED INDEX [IX_OM_Search_SearchActivityID] ON [OM_Search] 
(
	[SearchActivityID]
)
END
GO

IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_OM_UserAgent_UserAgentActiveContactID')
BEGIN
CREATE NONCLUSTERED INDEX [IX_OM_UserAgent_UserAgentActiveContactID] ON [OM_UserAgent] 
(
	[UserAgentActiveContactID]
)
END
GO

IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_OM_UserAgent_UserAgentOriginalContactID')
BEGIN
CREATE NONCLUSTERED INDEX [IX_OM_UserAgent_UserAgentOriginalContactID] ON [OM_UserAgent] 
(
	[UserAgentOriginalContactID]
)
END
GO


/******** END UPDATE INDEXES ******/


/******** UPDATE CAMPAIGN DETAIL QUERIES******/
UPDATE Reporting_ReportTable SET TableQuery = 
'
 SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day''); 
 SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day''); 

SELECT  
 CampaignDisplayName AS '' '',
 ISNULL(ConversionDisplayName,Conversions.ConversionName +'' ({$recyclebin.deleted$})'') AS ''{$Conversion.name$}'',ISNULL(SUM (Visits.Count),0) AS ''{$analytics_codename.visits$}'',
 ISNULL(SUM(Conversions.Hits),0) AS ''{$conversion.conversion.list$}'',
 ISNULL(CAST (CAST (CAST (SUM(Conversions.Hits) AS DECIMAL (15,2)) / SUM (Visits.Count) * 100 AS DECIMAL(15,1)) AS NVARCHAR(20))+''%'',''0.0%'') AS ''{$abtesting.conversionrate$}'',
 ISNULL(SUM (Conversions.Value),0) AS ''{$conversions.value$}'', 
 ISNULL(ROUND (SUM (Conversions.Value)  / NULLIF (SUM(Visits.Count),0), 1),0) AS ''{$conversions.valuepervisit$}''    
  
 FROM Analytics_Campaign
    -- Visits
    LEFT JOIN (SELECT SUM(HitsCount) AS Count, StatisticsObjectName AS CampaignName FROM
				Analytics_DayHits JOIN Analytics_Statistics ON HitsStatisticsID = StatisticsID
				WHERE (@FromDate IS NULL OR @FromDate <= HitsStartTime) AND (@ToDate IS NULL OR @ToDate >= HitsStartTime)
				AND StatisticsSiteID = @CMSContextCurrentSiteID AND  
				StatisticsCode = ''campaign'' AND StatisticsObjectName = @CampaignName
				GROUP BY StatisticsObjectName
				) AS Visits
	ON Visits.CampaignName = Analytics_Campaign.CampaignName
								  
    --- Conversion count, conversion value
	LEFT JOIN (SELECT SUM(HitsCount) AS Hits,SUM(HitsValue) AS Value, StatisticsObjectName  AS ConversionName,
				SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode)) AS CampaignName 	FROM Analytics_DayHits 
				JOIN Analytics_Statistics ON StatisticsID = HitsStatisticsID
				WHERE (@FromDate IS NULL OR @FromDate <= HitsStartTime) AND (@ToDate IS NULL OR @ToDate >= HitsStartTime)
				AND StatisticsCode LIKE ''campconversion;%'' AND StatisticsSiteID = @CMSContextCurrentSiteID
				GROUP BY StatisticsObjectName,SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode))) AS Conversions 
	ON Conversions.CampaignName = Analytics_Campaign.CampaignName 

 LEFT JOIN Analytics_Conversion ON Conversions.ConversionName = Analytics_Conversion.ConversionName AND 
			Analytics_Conversion.ConversionSiteID = @CMSContextCurrentSiteID
WHERE CampaignSiteID = @CMSContextCurrentSiteID AND Analytics_Campaign.CampaignName = @CampaignName 
GROUP BY CampaignDisplayName,ConversionDisplayName,Conversions.ConversionName

'
WHERE TableName = 'table' AND TableReportID IN (SELECT ReportID FROM Reporting_Report WHERE 
ReportName = 'campaigns.singledetails')



UPDATE Reporting_ReportTable SET TableQuery = 
'
 SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day''); 
 SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day''); 

SELECT CampaignDisplayName AS '' '',ISNULL(SUM (Views.Hits),0) AS ''{$analytics_codename.visits$}'', ISNULL(SUM(Conversions.Hits),0) AS ''{$conversion.conversion.list$}'',
    ISNULL(CAST (CAST (CAST (SUM(Conversions.Hits) AS DECIMAL (15,2)) / SUM (Views.Hits) * 100 AS DECIMAL(15,1)) AS NVARCHAR(20))+''%'',''0.0%'') AS ''{$abtesting.conversionrate$}'',
  ISNULL(SUM (Conversions.Value),0) AS ''{$conversions.value$}'',   
  ISNULL(ROUND (SUM (Conversions.Value)  / NULLIF (SUM(Views.Hits),0), 1),0) AS ''{$conversions.valuepervisit$}'',
  ISNULL(CampaignTotalCost,0) AS ''{$campaign.totalcost$}'', 
  CAST (CAST (ISNULL(SUM(Conversions.Value) / NULLIF(CampaignTotalCost,0),0)*100 AS DECIMAL(15,1)) AS NVARCHAR(15))+''%'' AS ''{$campaign.roi$}''
  
 FROM Analytics_Campaign 
  LEFT JOIN Analytics_Statistics ON CampaignName = SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode)) OR ((StatisticsObjectName = CampaignName) AND StatisticsCode=''campaign'')
-- Visits
LEFT JOIN (SELECT SUM(HitsCount) AS Hits,HitsStatisticsID AS HitsStatisticsID  FROM Analytics_DayHits 
	WHERE (@FromDate IS NULL OR @FromDate <= HitsStartTime) AND (@ToDate IS NULL OR @ToDate >= HitsStartTime)
   GROUP BY HitsStatisticsID) AS Views ON Views.HitsStatisticsID = Analytics_Statistics.StatisticsID AND StatisticsCode = ''campaign''
-- Conversion count, conversion value
LEFT JOIN (SELECT SUM(HitsCount) AS Hits,SUM(HitsValue) AS Value,HitsStatisticsID AS HitsStatisticsID  FROM Analytics_DayHits 
	WHERE (@FromDate IS NULL OR @FromDate <= HitsStartTime) AND (@ToDate IS NULL OR @ToDate >= HitsStartTime)
   GROUP BY HitsStatisticsID) AS Conversions ON Conversions.HitsStatisticsID = Analytics_Statistics.StatisticsID AND StatisticsCode LIKE ''campconversion;%''

WHERE CampaignSiteID = @CMSContextCurrentSiteID
GROUP BY CampaignDisplayName, CampaignTotalCost
'
WHERE TableName = 'table' AND TableReportID IN (SELECT ReportID FROM Reporting_Report WHERE 
ReportName = 'campaigns.alldetails')


UPDATE Reporting_Report 
SET ReportParameters =
'<form><field column="FromDate" visible="false" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="false" publicfield="false" spellcheck="true" guid="39390d84-5945-428f-b8a3-3a7d40fb54c8" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false" inheritable="false" translatefield="false" /><field column="ToDate" visible="false" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="false" publicfield="false" spellcheck="true" guid="43fc3003-5999-4c4a-8797-05fe015d60b1" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false" inheritable="false" translatefield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field></form>'
WHERE ReportName = 'campaigns.alldetails'  

UPDATE Reporting_Report
SET ReportParameters = 
'<form><field column="CampaignName" fieldcaption="Campaign name" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="false" columnsize="200" publicfield="false" spellcheck="true" guid="50fb877c-fe24-4ad3-b5b9-00f02e07c65c" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false" inheritable="false" translatefield="false"><settings><AutoCompleteShowOnlyCurrentWordInCompletionListItem>False</AutoCompleteShowOnlyCurrentWordInCompletionListItem><AutoCompleteFirstRowSelected>False</AutoCompleteFirstRowSelected><controlname>textboxcontrol</controlname><FilterMode>False</FilterMode><Trim>False</Trim><AutoCompleteEnableCaching>False</AutoCompleteEnableCaching></settings></field><field column="FromDate" fieldcaption="Campaign name" visible="false" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="false" publicfield="false" spellcheck="true" guid="b1b019c9-8d7b-429e-be81-654bc4d4a73f" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false" inheritable="false" translatefield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ToDate" visible="false" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="false" publicfield="false" spellcheck="true" guid="6b2a0f85-d6eb-46ce-940f-d567c72a7ea2" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false" inheritable="false" translatefield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field></form>'
WHERE ReportName = 'campaigns.singledetails'


GO

/******** END UPDATE CAMPAIGN DETAIL QUERIES******/

/********** UPDATE SELECTBYNAME FOR CMS.ROLE **************/
UPDATE CMS_Query SET QueryText = 
'SELECT CMS_Role.* FROM CMS_Role WHERE ##WHERE## ORDER BY ##ORDERBY##'
where ClassID IN (SELECT ClassID FROM CMS_Class WHERE ClassName ='cms.role') AND QueryName = 'selectbyname'

GO
/*********** END UPDATE SELECTBYNAME ***************/

/******** UPDATE PERMISSION NAMES ******/

-- Document 'Read' permission
UPDATE CMS_Permission SET PermissionDisplayName = '{$permissionnames.read$}', PermissionDescription = '{$permissiondescription.document.read$}'
WHERE ClassID IN (SELECT ClassID FROM CMS_Class WHERE ClassIsDocumentType = 1) AND PermissionName = 'read'

-- Document 'Modify' permission
UPDATE CMS_Permission SET PermissionDisplayName = '{$permissionnames.modify$}', PermissionDescription = '{$permissiondescription.document.modify$}'
WHERE ClassID IN (SELECT ClassID FROM CMS_Class WHERE ClassIsDocumentType = 1) AND PermissionName = 'modify'

-- Document 'CreateSpecific' permission
UPDATE CMS_Permission SET PermissionDisplayName = '{$permissionnames.create$}', PermissionDescription = '{$permissiondescription.document.create$}'
WHERE ClassID IN (SELECT ClassID FROM CMS_Class WHERE ClassIsDocumentType = 1) AND PermissionName = 'createspecific'

-- Document 'Create' permission
UPDATE CMS_Permission SET PermissionDisplayName = '{$permissionnames.createanywhere$}', PermissionDescription = '{$permissiondescription.document.createanywhere$}'
WHERE ClassID IN (SELECT ClassID FROM CMS_Class WHERE ClassIsDocumentType = 1) AND PermissionName = 'create'

-- Document 'Delete' permission
UPDATE CMS_Permission SET PermissionDisplayName = '{$general.delete$}', PermissionDescription = '{$permissiondescription.document.delete$}'
WHERE ClassID IN (SELECT ClassID FROM CMS_Class WHERE ClassIsDocumentType = 1) AND PermissionName = 'delete'

-- Document 'Destroy' permission
UPDATE CMS_Permission SET PermissionDisplayName = '{$permissionnames.destroy$}', PermissionDescription = '{$permissiondescription.document.destroy$}'
WHERE ClassID IN (SELECT ClassID FROM CMS_Class WHERE ClassIsDocumentType = 1) AND PermissionName = 'destroy'

-- Document 'ExploreTree' permission
UPDATE CMS_Permission SET PermissionDisplayName = '{$permissionnames.exploretree$}', PermissionDescription = '{$permissiondescription.document.exploretree$}'
WHERE ClassID IN (SELECT ClassID FROM CMS_Class WHERE ClassIsDocumentType = 1) AND PermissionName = 'exploretree'

-- Document 'ModifyPermissions' permission
UPDATE CMS_Permission SET PermissionDisplayName = '{$permissionnames.modifypermissions$}', PermissionDescription = '{$permissiondescription.document.modifypermissions$}'
WHERE ClassID IN (SELECT ClassID FROM CMS_Class WHERE ClassIsDocumentType = 1) AND PermissionName = 'modifypermissions'


-- Custom table 'Read' permission
UPDATE CMS_Permission SET PermissionDisplayName = '{$permissionnames.read$}', PermissionDescription = '{$permissiondescription.customtable.read$}'
WHERE ClassID IN (SELECT ClassID FROM CMS_Class WHERE ClassIsCustomTable = 1) AND PermissionName = 'read'

-- Custom table 'Modify' permission
UPDATE CMS_Permission SET PermissionDisplayName = '{$permissionnames.modify$}', PermissionDescription = '{$permissiondescription.customtable.modify$}'
WHERE ClassID IN (SELECT ClassID FROM CMS_Class WHERE ClassIsCustomTable = 1) AND PermissionName = 'modify'

-- Custom table 'Create' permission
UPDATE CMS_Permission SET PermissionDisplayName = '{$permissionnames.create$}', PermissionDescription = '{$permissiondescription.customtable.create$}'
WHERE ClassID IN (SELECT ClassID FROM CMS_Class WHERE ClassIsCustomTable = 1) AND PermissionName = 'create'

-- Custom table 'Delete' permission
UPDATE CMS_Permission SET PermissionDisplayName = '{$general.delete$}', PermissionDescription = '{$permissiondescription.customtable.delete$}'
WHERE ClassID IN (SELECT ClassID FROM CMS_Class WHERE ClassIsCustomTable = 1) AND PermissionName = 'delete'

/******** END UPDATE PERMISSION NAMES ******/

/******** UPDATE SCHEDULED TASKS ******/

-- Content synchronization task - assembly name and class --
UPDATE [CMS_ScheduledTask]
SET [TaskAssemblyName] = 'CMS.SynchronizationEngine', [TaskClass] = 'CMS.Synchronization.StagingWorker'
WHERE [TaskAssemblyName] = 'CMS.SyncService' AND [TaskClass] = 'CMS.Staging.StagingWorker'

/******** END UPDATE SCHEDULED TASKS *******/


/*************** START UPDATE CAMPAIGN CONVERSION COUNT AND VALUE QUERIES *********************/

UPDATE Reporting_ReportGraph SET GraphQuery =
'
EXEC Proc_Analytics_RemoveTempTable
CREATE TABLE #AnalyticsTempTable (
  StartTime DATETIME,
  Hits INT,
  Name NVARCHAR(300) COLLATE database_default  
);

 SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year''); 
 
  INSERT INTO #AnalyticsTempTable (StartTime, Hits, Name)        
 SELECT [Date] AS StartTime ,T1.Hits,T1.Name AS Name 
 FROM
  {%DatabaseSchema%}.Func_Analytics_EnsureDates (@FromDate,@ToDate,''year'') AS Dates   
  LEFT JOIN
  (SELECT HitsStartTime AS StartTime,SUM(HitsValue)AS hits,
   CASE
    WHEN @ConversionName = '''' THEN CampaignDisplayName
    ELSE ConversionDisplayName
   END
  AS Name
  FROM Analytics_Statistics
  INNER JOIN Analytics_YearHits ON Analytics_YearHits.HitsStatisticsID = Analytics_Statistics.StatisticsID  
  INNER JOIN Analytics_Conversion ON StatisticsObjectName = ConversionName AND ConversionSiteID = StatisticsSiteID
  INNER JOIN Analytics_Campaign ON SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode)) = @CampaignName  
        AND CampaignName = @CampaignName  AND CampaignSiteID = StatisticsSiteID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND
  (StatisticsCode LIKE @CodeName+'';%'')  AND 
  (@CampaignName IN ('''',CampaignName) OR @CampaignName IS NULL) AND 
  (@ConversionName IN ('''',ConversionName) OR @ConversionName IS NULL)  
  
  GROUP BY HitsStartTime,CampaignName,
     CASE
    WHEN @ConversionName = '''' THEN CampaignDisplayName
    ELSE ConversionDisplayName
     END
  ) AS T1
  ON T1.StartTime = [Date]  
  
 EXEC Proc_Analytics_Pivot ''year''
 EXEC Proc_Analytics_RemoveTempTable
 ' 
 
 WHERE GraphName = 'graph' AND GraphReportID IN
(SELECT ReportID FROM Reporting_Report WHERE ReportName = 'campaignconversionvalue.yearreport') 



UPDATE Reporting_ReportGraph SET GraphQuery =
'
EXEC Proc_Analytics_RemoveTempTable
CREATE TABLE #AnalyticsTempTable (
  StartTime DATETIME,
  Hits INT,
  Name NVARCHAR(300) COLLATE database_default  
);

 SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month''); 
 
  INSERT INTO #AnalyticsTempTable (StartTime, Hits, Name)        
 SELECT [Date] AS StartTime ,T1.Hits,T1.Name AS Name 
 FROM
  {%DatabaseSchema%}.Func_Analytics_EnsureDates (@FromDate,@ToDate,''month'') AS Dates   
  LEFT JOIN
  (SELECT HitsStartTime AS StartTime,SUM(HitsValue)AS hits,
   CASE
    WHEN @ConversionName = '''' THEN CampaignDisplayName
    ELSE ConversionDisplayName
   END
  AS Name
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_MonthHits.HitsStatisticsID = Analytics_Statistics.StatisticsID  
  INNER JOIN Analytics_Conversion ON StatisticsObjectName = ConversionName AND ConversionSiteID = StatisticsSiteID
  INNER JOIN Analytics_Campaign ON SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode)) = @CampaignName  
        AND CampaignName = @CampaignName  AND CampaignSiteID = StatisticsSiteID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND
  (StatisticsCode LIKE @CodeName+'';%'')  AND 
  (@CampaignName IN ('''',CampaignName) OR @CampaignName IS NULL) AND
  (@ConversionName IN ('''',ConversionName) OR @ConversionName IS NULL)  
  
  GROUP BY HitsStartTime,CampaignName,
     CASE
    WHEN @ConversionName = '''' THEN CampaignDisplayName
    ELSE ConversionDisplayName
     END
  ) AS T1
  ON T1.StartTime = [Date]  
  
 EXEC Proc_Analytics_Pivot ''month''
 EXEC Proc_Analytics_RemoveTempTable
' 
 
 WHERE GraphName = 'graph' AND GraphReportID IN
(SELECT ReportID FROM Reporting_Report WHERE ReportName = 'campaignconversionvalue.monthreport') 


UPDATE Reporting_ReportGraph SET GraphQuery =
'
EXEC Proc_Analytics_RemoveTempTable
CREATE TABLE #AnalyticsTempTable (
  StartTime DATETIME,
  Hits INT,
  Name NVARCHAR(300) COLLATE database_default  
);

 SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week''); 
 
  INSERT INTO #AnalyticsTempTable (StartTime, Hits, Name)        
 SELECT [Date] AS StartTime ,T1.Hits,T1.Name AS Name 
 FROM
  {%DatabaseSchema%}.Func_Analytics_EnsureDates (@FromDate,@ToDate,''week'') AS Dates   
  LEFT JOIN
  (SELECT HitsStartTime AS StartTime,SUM(HitsValue)AS hits,
   CASE
    WHEN @ConversionName = '''' THEN CampaignDisplayName
    ELSE ConversionDisplayName
   END
  AS Name
  FROM Analytics_Statistics
  INNER JOIN Analytics_WeekHits ON Analytics_WeekHits.HitsStatisticsID = Analytics_Statistics.StatisticsID  
  INNER JOIN Analytics_Conversion ON StatisticsObjectName = ConversionName AND ConversionSiteID = StatisticsSiteID
  INNER JOIN Analytics_Campaign ON SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode)) = @CampaignName  
        AND CampaignName = @CampaignName  AND CampaignSiteID = StatisticsSiteID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND
  (StatisticsCode LIKE @CodeName+'';%'')  AND 
  (@CampaignName IN ('''',CampaignName) OR @CampaignName IS NULL) AND
  (@ConversionName IN ('''',ConversionName) OR @ConversionName IS NULL)  
  
  GROUP BY HitsStartTime,CampaignName,
     CASE
    WHEN @ConversionName = '''' THEN CampaignDisplayName
    ELSE ConversionDisplayName
     END
  ) AS T1
  ON T1.StartTime = [Date]  
  
 EXEC Proc_Analytics_Pivot ''week''
 EXEC Proc_Analytics_RemoveTempTable
' 
 WHERE GraphName = 'graph' AND GraphReportID IN
(SELECT ReportID FROM Reporting_Report WHERE ReportName = 'campaignconversionvalue.weekreport') 


UPDATE Reporting_ReportGraph SET GraphQuery =
'
EXEC Proc_Analytics_RemoveTempTable
CREATE TABLE #AnalyticsTempTable (
  StartTime DATETIME,
  Hits INT,
  Name NVARCHAR(300) COLLATE database_default  
);

 SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour''); 
 
  INSERT INTO #AnalyticsTempTable (StartTime, Hits, Name)        
 SELECT [Date] AS StartTime ,T1.Hits,T1.Name AS Name 
 FROM
  {%DatabaseSchema%}.Func_Analytics_EnsureDates (@FromDate,@ToDate,''hour'') AS Dates   
  LEFT JOIN
  (SELECT HitsStartTime AS StartTime,SUM(HitsValue)AS hits,
   CASE
    WHEN @ConversionName = '''' THEN CampaignDisplayName
    ELSE ConversionDisplayName
   END
  AS Name
  FROM Analytics_Statistics
  INNER JOIN Analytics_HourHits ON Analytics_HourHits.HitsStatisticsID = Analytics_Statistics.StatisticsID  
  INNER JOIN Analytics_Conversion ON StatisticsObjectName = ConversionName AND ConversionSiteID = StatisticsSiteID
  INNER JOIN Analytics_Campaign ON SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode)) = @CampaignName  
        AND CampaignName = @CampaignName  AND CampaignSiteID = StatisticsSiteID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND
  (StatisticsCode LIKE @CodeName+'';%'')  AND 
  (@CampaignName IN ('''',CampaignName) OR @CampaignName IS NULL) AND
  (@ConversionName IN ('''',ConversionName) OR @ConversionName IS NULL)  
  
  GROUP BY HitsStartTime,CampaignName,
     CASE
    WHEN @ConversionName = '''' THEN CampaignDisplayName
    ELSE ConversionDisplayName
     END
  ) AS T1
  ON T1.StartTime = [Date]  
  
 EXEC Proc_Analytics_Pivot ''hour''
 EXEC Proc_Analytics_RemoveTempTable
' 
 
 WHERE GraphName = 'graph' AND GraphReportID IN
(SELECT ReportID FROM Reporting_Report WHERE ReportName = 'campaignconversionvalue.hourreport') 



UPDATE Reporting_ReportGraph SET GraphQuery =
'
EXEC Proc_Analytics_RemoveTempTable
CREATE TABLE #AnalyticsTempTable (
  StartTime DATETIME,
  Hits INT,
  Name NVARCHAR(300) COLLATE database_default  
);

 SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day''); 
 
  INSERT INTO #AnalyticsTempTable (StartTime, Hits, Name)        
 SELECT [Date] AS StartTime ,T1.Hits,T1.Name AS Name 
 FROM
  {%DatabaseSchema%}.Func_Analytics_EnsureDates (@FromDate,@ToDate,''day'') AS Dates   
  LEFT JOIN
  (SELECT HitsStartTime AS StartTime,SUM(HitsValue)AS hits,
   CASE
    WHEN @ConversionName = '''' THEN CampaignDisplayName
    ELSE ConversionDisplayName
   END
  AS Name
  FROM Analytics_Statistics
  INNER JOIN Analytics_DayHits ON Analytics_DayHits.HitsStatisticsID = Analytics_Statistics.StatisticsID  
  INNER JOIN Analytics_Conversion ON StatisticsObjectName = ConversionName AND ConversionSiteID = StatisticsSiteID
  INNER JOIN Analytics_Campaign ON SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode)) = @CampaignName  
        AND CampaignName = @CampaignName  AND CampaignSiteID = StatisticsSiteID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND
  (StatisticsCode LIKE @CodeName+'';%'')  AND 
  (@CampaignName IN ('''',CampaignName) OR @CampaignName IS NULL) AND
  (@ConversionName IN ('''',ConversionName) OR @ConversionName IS NULL)  
  
  GROUP BY HitsStartTime,CampaignName,
     CASE
    WHEN @ConversionName = '''' THEN CampaignDisplayName
    ELSE ConversionDisplayName
     END
  ) AS T1
  ON T1.StartTime = [Date]  
  
 EXEC Proc_Analytics_Pivot ''day''
 EXEC Proc_Analytics_RemoveTempTable
'
 
 WHERE GraphName = 'graph' AND GraphReportID IN
(SELECT ReportID FROM Reporting_Report WHERE ReportName = 'campaignconversionvalue.dayreport') 

/************************* CONVERSION COUNT **************************************************/


UPDATE Reporting_ReportGraph SET GraphQuery =
'
EXEC Proc_Analytics_RemoveTempTable
CREATE TABLE #AnalyticsTempTable (
  StartTime DATETIME,
  Hits INT,
  Name NVARCHAR(300) COLLATE database_default  
);

 SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year''); 
 
  INSERT INTO #AnalyticsTempTable (StartTime, Hits, Name)        
 SELECT [Date] AS StartTime ,T1.Hits,T1.Name AS Name 
 FROM
  {%DatabaseSchema%}.Func_Analytics_EnsureDates (@FromDate,@ToDate,''year'') AS Dates   
  LEFT JOIN
  (SELECT HitsStartTime AS StartTime,SUM(HitsCount)AS hits,
   CASE
    WHEN @ConversionName = '''' THEN CampaignDisplayName
    ELSE ConversionDisplayName
   END
  AS Name
  FROM Analytics_Statistics
  INNER JOIN Analytics_YearHits ON Analytics_YearHits.HitsStatisticsID = Analytics_Statistics.StatisticsID  
  INNER JOIN Analytics_Conversion ON StatisticsObjectName = ConversionName AND ConversionSiteID = StatisticsSiteID
  INNER JOIN Analytics_Campaign ON SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode)) = @CampaignName  
        AND CampaignName = @CampaignName  AND CampaignSiteID = StatisticsSiteID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND
  (StatisticsCode LIKE @CodeName+'';%'')  AND 
  (@CampaignName IN ('''',CampaignName) OR @CampaignName IS NULL) AND
  (@ConversionName IN ('''',ConversionName) OR @ConversionName IS NULL)  
  
  GROUP BY HitsStartTime,CampaignName,
     CASE
    WHEN @ConversionName = '''' THEN CampaignDisplayName
    ELSE ConversionDisplayName
     END
  ) AS T1
  ON T1.StartTime = [Date]  
  
 EXEC Proc_Analytics_Pivot ''year''
 EXEC Proc_Analytics_RemoveTempTable
'  
 WHERE GraphName = 'graph' AND GraphReportID IN
(SELECT ReportID FROM Reporting_Report WHERE ReportName = 'campaignconversioncount.yearreport') 


UPDATE Reporting_ReportGraph SET GraphQuery =
'
EXEC Proc_Analytics_RemoveTempTable
CREATE TABLE #AnalyticsTempTable (
  StartTime DATETIME,
  Hits INT,
  Name NVARCHAR(300) COLLATE database_default  
);

 SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month''); 
 
  INSERT INTO #AnalyticsTempTable (StartTime, Hits, Name)        
 SELECT [Date] AS StartTime ,T1.Hits,T1.Name AS Name 
 FROM
  {%DatabaseSchema%}.Func_Analytics_EnsureDates (@FromDate,@ToDate,''month'') AS Dates   
  LEFT JOIN
  (SELECT HitsStartTime AS StartTime,SUM(HitsCount)AS hits,
   CASE
    WHEN @ConversionName = '''' THEN CampaignDisplayName
    ELSE ConversionDisplayName
   END
  AS Name
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_MonthHits.HitsStatisticsID = Analytics_Statistics.StatisticsID  
  INNER JOIN Analytics_Conversion ON StatisticsObjectName = ConversionName AND ConversionSiteID = StatisticsSiteID
  INNER JOIN Analytics_Campaign ON SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode)) = @CampaignName  
        AND CampaignName = @CampaignName  AND CampaignSiteID = StatisticsSiteID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND
  (StatisticsCode LIKE @CodeName+'';%'')  AND 
  (@CampaignName IN ('''',CampaignName) OR @CampaignName IS NULL) AND
  (@ConversionName IN ('''',ConversionName) OR @ConversionName IS NULL)  
  
  GROUP BY HitsStartTime,CampaignName,
     CASE
    WHEN @ConversionName = '''' THEN CampaignDisplayName
    ELSE ConversionDisplayName
     END
  ) AS T1
  ON T1.StartTime = [Date]  
  
 EXEC Proc_Analytics_Pivot ''month''
 EXEC Proc_Analytics_RemoveTempTable
'  
 WHERE GraphName = 'graph' AND GraphReportID IN
(SELECT ReportID FROM Reporting_Report WHERE ReportName = 'campaignconversioncount.monthreport') 


UPDATE Reporting_ReportGraph SET GraphQuery =
'
EXEC Proc_Analytics_RemoveTempTable
CREATE TABLE #AnalyticsTempTable (
  StartTime DATETIME,
  Hits INT,
  Name NVARCHAR(300) COLLATE database_default  
);

 SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week''); 
 
  INSERT INTO #AnalyticsTempTable (StartTime, Hits, Name)        
 SELECT [Date] AS StartTime ,T1.Hits,T1.Name AS Name 
 FROM
  {%DatabaseSchema%}.Func_Analytics_EnsureDates (@FromDate,@ToDate,''week'') AS Dates   
  LEFT JOIN
  (SELECT HitsStartTime AS StartTime,SUM(HitsCount)AS hits,
   CASE
    WHEN @ConversionName = '''' THEN CampaignDisplayName
    ELSE ConversionDisplayName
   END
  AS Name
  FROM Analytics_Statistics
  INNER JOIN Analytics_WeekHits ON Analytics_WeekHits.HitsStatisticsID = Analytics_Statistics.StatisticsID  
  INNER JOIN Analytics_Conversion ON StatisticsObjectName = ConversionName AND ConversionSiteID = StatisticsSiteID
  INNER JOIN Analytics_Campaign ON SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode)) = @CampaignName  
        AND CampaignName = @CampaignName  AND CampaignSiteID = StatisticsSiteID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND
  (StatisticsCode LIKE @CodeName+'';%'')  AND 
  (@CampaignName IN ('''',CampaignName) OR @CampaignName IS NULL) AND
  (@ConversionName IN ('''',ConversionName) OR @ConversionName IS NULL)  
  
  GROUP BY HitsStartTime,CampaignName,
     CASE
    WHEN @ConversionName = '''' THEN CampaignDisplayName
    ELSE ConversionDisplayName
     END
  ) AS T1
  ON T1.StartTime = [Date]  
  
 EXEC Proc_Analytics_Pivot ''week''
 EXEC Proc_Analytics_RemoveTempTable
'  
 WHERE GraphName = 'graph' AND GraphReportID IN
(SELECT ReportID FROM Reporting_Report WHERE ReportName = 'campaignconversioncount.weekreport') 


UPDATE Reporting_ReportGraph SET GraphQuery =
'
EXEC Proc_Analytics_RemoveTempTable
CREATE TABLE #AnalyticsTempTable (
  StartTime DATETIME,
  Hits INT,
  Name NVARCHAR(300) COLLATE database_default  
);

 SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day''); 
 
  INSERT INTO #AnalyticsTempTable (StartTime, Hits, Name)        
 SELECT [Date] AS StartTime ,T1.Hits,T1.Name AS Name 
 FROM
  {%DatabaseSchema%}.Func_Analytics_EnsureDates (@FromDate,@ToDate,''day'') AS Dates   
  LEFT JOIN
  (SELECT HitsStartTime AS StartTime,SUM(HitsCount)AS hits,
   CASE
    WHEN @ConversionName = '''' THEN CampaignDisplayName
    ELSE ConversionDisplayName
   END
  AS Name
  FROM Analytics_Statistics
  INNER JOIN Analytics_DayHits ON Analytics_DayHits.HitsStatisticsID = Analytics_Statistics.StatisticsID  
  INNER JOIN Analytics_Conversion ON StatisticsObjectName = ConversionName AND ConversionSiteID = StatisticsSiteID
  INNER JOIN Analytics_Campaign ON SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode)) = @CampaignName  
        AND CampaignName = @CampaignName  AND CampaignSiteID = StatisticsSiteID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND
  (StatisticsCode LIKE @CodeName+'';%'')  AND 
  (@CampaignName IN ('''',CampaignName) OR @CampaignName IS NULL) AND
  (@ConversionName IN ('''',ConversionName) OR @ConversionName IS NULL)  
  
  GROUP BY HitsStartTime,CampaignName,
     CASE
    WHEN @ConversionName = '''' THEN CampaignDisplayName
    ELSE ConversionDisplayName
     END
  ) AS T1
  ON T1.StartTime = [Date]  
  
 EXEC Proc_Analytics_Pivot ''day''
 EXEC Proc_Analytics_RemoveTempTable
'  
 WHERE GraphName = 'graph' AND GraphReportID IN
(SELECT ReportID FROM Reporting_Report WHERE ReportName = 'campaignconversioncount.dayreport') 


UPDATE Reporting_ReportGraph SET GraphQuery =
'
EXEC Proc_Analytics_RemoveTempTable
CREATE TABLE #AnalyticsTempTable (
  StartTime DATETIME,
  Hits INT,
  Name NVARCHAR(300) COLLATE database_default  
);

 SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour''); 
 
  INSERT INTO #AnalyticsTempTable (StartTime, Hits, Name)        
 SELECT [Date] AS StartTime ,T1.Hits,T1.Name AS Name 
 FROM
  {%DatabaseSchema%}.Func_Analytics_EnsureDates (@FromDate,@ToDate,''hour'') AS Dates   
  LEFT JOIN
  (SELECT HitsStartTime AS StartTime,SUM(HitsCount)AS hits,
   CASE
    WHEN @ConversionName = '''' THEN CampaignDisplayName
    ELSE ConversionDisplayName
   END
  AS Name
  FROM Analytics_Statistics
  INNER JOIN Analytics_HourHits ON Analytics_HourHits.HitsStatisticsID = Analytics_Statistics.StatisticsID  
  INNER JOIN Analytics_Conversion ON StatisticsObjectName = ConversionName AND ConversionSiteID = StatisticsSiteID
  INNER JOIN Analytics_Campaign ON SUBSTRING(StatisticsCode, 16, LEN(StatisticsCode)) = @CampaignName  
        AND CampaignName = @CampaignName  AND CampaignSiteID = StatisticsSiteID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND
  (StatisticsCode LIKE @CodeName+'';%'')  AND 
  (@CampaignName IN ('''',CampaignName) OR @CampaignName IS NULL) AND
  (@ConversionName IN ('''',ConversionName) OR @ConversionName IS NULL)  
  
  GROUP BY HitsStartTime,CampaignName,
     CASE
    WHEN @ConversionName = '''' THEN CampaignDisplayName
    ELSE ConversionDisplayName
     END
  ) AS T1
  ON T1.StartTime = [Date]  
  
 EXEC Proc_Analytics_Pivot ''hour''
 EXEC Proc_Analytics_RemoveTempTable
'  
 WHERE GraphName = 'graph' AND GraphReportID IN
(SELECT ReportID FROM Reporting_Report WHERE ReportName = 'campaignconversioncount.hourreport') 


GO

/********************* END UPDATE CAMPAIGN CONVERSIONS COUNT AND VALUES QUERIES ****************/

/******************************* UPDATE MVT,AB TABLE QUERIES ***********************************/


DECLARE @HOTFIXVERSION INT;
SET @HOTFIXVERSION =  (SELECT KeyValue FROM [CMS_SettingsKey] WHERE KeyName = N'CMSHotfixVersion')
IF @HOTFIXVERSION  < 20
BEGIN

UPDATE Reporting_ReportTable SET TableQuery=
'
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''year'');

SELECT X.Name AS ''{$mvtcombination.name$}'',ISNULL (SUM(Y.Hits),0) AS ''{$om.selectedperiod$}'',
ISNULL(SUM(X.Hits),0) AS ''{$om.total$}''
  FROM 
(
SELECT 
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END   
    AS Name,MVTCombinationName, ISNULL(SUM (HitsCount),0) AS Hits,StatisticsObjectCulture
 FROM Analytics_Statistics
 INNER JOIN OM_MVTCombination ON 
  MVTCombinationPageTemplateID IN ( SELECT DocumentPageTemplateID FROM View_CMS_Tree_Joined WHERE NodeSiteID = @CMSContextCurrentSiteID
        AND NodeAliasPath IN(SELECT MVTestPage FROM OM_MVTest WHERE MVTestName = @MVTestName AND MVTestSiteID = @CMSContextCurrentSiteID)                        
        AND DocumentCulture = StatisticsObjectCulture)   
  LEFT JOIN Analytics_YearHits ON StatisticsID = HitsStatisticsID

 WHERE   (StatisticsSiteID = @CMSContextCurrentSiteID ) AND StatisticsCode LIKE ''mvtconversion;%'' 
 AND  @MVTestName = SUBSTRING(StatisticsCode, 15, CHARINDEX('';'',StatisticsCode,15)-15) AND
 MVTCombinationName = (SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)))
 AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY MVTCombinationName, StatisticsObjectCulture,
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END
) AS X 
LEFT JOIN (SELECT SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)) AS MVTCombinationName, SUM(HitsCount) AS Hits,
StatisticsObjectCulture
 FROM Analytics_Statistics JOIN Analytics_YearHits ON HitsStatisticsID = StatisticsID 
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND StatisticsCode LIKE ''mvtconversion;%''
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)   
  AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
  GROUP BY SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)), StatisticsObjectCulture
 )
 AS Y ON X.MVTCombinationName = Y.MVTCombinationName AND X.StatisticsObjectCulture = Y.StatisticsObjectCulture
 
 GROUP BY X.Name
 ORDER BY [{$om.total$}] Desc
'
WHERE 
TableReportID IN (SELECT ReportID FROM Reporting_Report WHERE ReportName= 'mvtestconversioncount.monthreport')
AND TableName = 'table'

UPDATE Reporting_ReportTable SET TableQuery=
'
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''month'');

SELECT X.Name AS ''{$mvtcombination.name$}'',ISNULL (SUM(Y.Hits),0) AS ''{$om.selectedperiod$}'',ISNULL(SUM(X.Hits),0) AS ''{$om.total$}''  FROM 
(
SELECT 
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END   
    AS Name,MVTCombinationName, ISNULL(SUM (HitsCount),0) AS Hits,StatisticsObjectCulture
 FROM Analytics_Statistics
 INNER JOIN OM_MVTCombination ON 
  MVTCombinationPageTemplateID IN ( SELECT DocumentPageTemplateID FROM View_CMS_Tree_Joined WHERE NodeSiteID = @CMSContextCurrentSiteID
        AND NodeAliasPath IN(SELECT MVTestPage FROM OM_MVTest WHERE MVTestName = @MVTestName AND MVTestSiteID = @CMSContextCurrentSiteID)                        
        AND DocumentCulture = StatisticsObjectCulture)   
  LEFT JOIN Analytics_MonthHits ON StatisticsID = HitsStatisticsID

 WHERE   (StatisticsSiteID = @CMSContextCurrentSiteID ) AND StatisticsCode LIKE ''mvtconversion;%'' 
 AND  @MVTestName = SUBSTRING(StatisticsCode, 15, CHARINDEX('';'',StatisticsCode,15)-15) AND
 MVTCombinationName = (SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)))
 AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY MVTCombinationName, StatisticsObjectCulture,
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END
) AS X 
LEFT JOIN (SELECT SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)) AS MVTCombinationName, SUM(HitsCount) AS Hits,
StatisticsObjectCulture
 FROM Analytics_Statistics JOIN Analytics_MonthHits ON HitsStatisticsID = StatisticsID 
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND StatisticsCode LIKE ''mvtconversion;%''
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)   
  AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
  GROUP BY SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)), StatisticsObjectCulture
 )
 AS Y ON X.MVTCombinationName = Y.MVTCombinationName AND X.StatisticsObjectCulture = Y.StatisticsObjectCulture
 
 
 GROUP BY X.Name
 ORDER BY [{$om.total$}] Desc
'
WHERE 
TableReportID IN (SELECT ReportID FROM Reporting_Report WHERE ReportName= 'mvtestconversioncount.weekreport')
AND TableName = 'table'

UPDATE Reporting_ReportTable SET TableQuery=
'
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''week'');

SELECT X.Name AS ''{$mvtcombination.name$}'',ISNULL (SUM(Y.Hits),0) AS ''{$om.selectedperiod$}'',ISNULL(SUM(X.Hits),0) AS ''{$om.total$}''  FROM 
(
SELECT 
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END   
    AS Name,MVTCombinationName, ISNULL(SUM (HitsCount),0) AS Hits,StatisticsObjectCulture
 FROM Analytics_Statistics
 INNER JOIN OM_MVTCombination ON 
  MVTCombinationPageTemplateID IN ( SELECT DocumentPageTemplateID FROM View_CMS_Tree_Joined WHERE NodeSiteID = @CMSContextCurrentSiteID
        AND NodeAliasPath IN(SELECT MVTestPage FROM OM_MVTest WHERE MVTestName = @MVTestName AND MVTestSiteID = @CMSContextCurrentSiteID)                        
        AND DocumentCulture = StatisticsObjectCulture)   
  LEFT JOIN Analytics_WeekHits ON StatisticsID = HitsStatisticsID

 WHERE   (StatisticsSiteID = @CMSContextCurrentSiteID ) AND StatisticsCode LIKE ''mvtconversion;%'' 
 AND  @MVTestName = SUBSTRING(StatisticsCode, 15, CHARINDEX('';'',StatisticsCode,15)-15) AND
 MVTCombinationName = (SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)))
 AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY MVTCombinationName, StatisticsObjectCulture,
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END
) AS X 
LEFT JOIN (SELECT SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)) AS MVTCombinationName, SUM(HitsCount) AS Hits,
StatisticsObjectCulture
 FROM Analytics_Statistics JOIN Analytics_WeekHits ON HitsStatisticsID = StatisticsID 
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND StatisticsCode LIKE ''mvtconversion;%''
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)   
  AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
  GROUP BY SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)), StatisticsObjectCulture
 )
 AS Y ON X.MVTCombinationName = Y.MVTCombinationName AND X.StatisticsObjectCulture = Y.StatisticsObjectCulture
 
 
 GROUP BY X.Name
 ORDER BY [{$om.total$}] Desc
'
WHERE 
TableReportID IN (SELECT ReportID FROM Reporting_Report WHERE ReportName= 'mvtestconversioncount.weekreport')
AND TableName = 'table'

UPDATE Reporting_ReportTable SET TableQuery=
'
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day'');

SELECT X.Name AS ''{$mvtcombination.name$}'',ISNULL (SUM(Y.Hits),0) AS ''{$om.selectedperiod$}'',ISNULL(SUM(X.Hits),0) AS ''{$om.total$}''  FROM 
(
SELECT 
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END   
    AS Name,MVTCombinationName, ISNULL(SUM (HitsCount),0) AS Hits,StatisticsObjectCulture
 FROM Analytics_Statistics
 INNER JOIN OM_MVTCombination ON 
  MVTCombinationPageTemplateID IN ( SELECT DocumentPageTemplateID FROM View_CMS_Tree_Joined WHERE NodeSiteID = @CMSContextCurrentSiteID
        AND NodeAliasPath IN(SELECT MVTestPage FROM OM_MVTest WHERE MVTestName = @MVTestName AND MVTestSiteID = @CMSContextCurrentSiteID)                        
        AND DocumentCulture = StatisticsObjectCulture)   
  LEFT JOIN Analytics_DayHits ON StatisticsID = HitsStatisticsID

 WHERE   (StatisticsSiteID = @CMSContextCurrentSiteID ) AND StatisticsCode LIKE ''mvtconversion;%'' 
 AND  @MVTestName = SUBSTRING(StatisticsCode, 15, CHARINDEX('';'',StatisticsCode,15)-15) AND
 MVTCombinationName = (SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)))
 AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY MVTCombinationName, StatisticsObjectCulture,
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END
) AS X 
LEFT JOIN (SELECT SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)) AS MVTCombinationName, SUM(HitsCount) AS Hits,
StatisticsObjectCulture
 FROM Analytics_Statistics JOIN Analytics_DayHits ON HitsStatisticsID = StatisticsID 
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND StatisticsCode LIKE ''mvtconversion;%''
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)   
  AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
  GROUP BY SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)), StatisticsObjectCulture
 )
 AS Y ON X.MVTCombinationName = Y.MVTCombinationName AND X.StatisticsObjectCulture = Y.StatisticsObjectCulture
 
 
 GROUP BY X.Name
 ORDER BY [{$om.total$}] Desc
'
WHERE 
TableReportID IN (SELECT ReportID FROM Reporting_Report WHERE ReportName= 'mvtestconversioncount.dayreport')
AND TableName = 'table'

UPDATE Reporting_ReportTable SET TableQuery=
'
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''hour'');

SELECT X.Name AS ''{$mvtcombination.name$}'',ISNULL (SUM(Y.Hits),0) AS ''{$om.selectedperiod$}'',ISNULL(SUM(X.Hits),0) AS ''{$om.total$}''  FROM 
(
SELECT 
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END   
    AS Name,MVTCombinationName, ISNULL(SUM (HitsCount),0) AS Hits,StatisticsObjectCulture
 FROM Analytics_Statistics
 INNER JOIN OM_MVTCombination ON 
  MVTCombinationPageTemplateID IN ( SELECT DocumentPageTemplateID FROM View_CMS_Tree_Joined WHERE NodeSiteID = @CMSContextCurrentSiteID
        AND NodeAliasPath IN(SELECT MVTestPage FROM OM_MVTest WHERE MVTestName = @MVTestName AND MVTestSiteID = @CMSContextCurrentSiteID)                        
        AND DocumentCulture = StatisticsObjectCulture)   
  LEFT JOIN Analytics_HourHits ON StatisticsID = HitsStatisticsID

 WHERE   (StatisticsSiteID = @CMSContextCurrentSiteID ) AND StatisticsCode LIKE ''mvtconversion;%'' 
 AND  @MVTestName = SUBSTRING(StatisticsCode, 15, CHARINDEX('';'',StatisticsCode,15)-15) AND
 MVTCombinationName = (SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)))
 AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY MVTCombinationName, StatisticsObjectCulture,
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END
) AS X 
LEFT JOIN (SELECT SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)) AS MVTCombinationName, SUM(HitsCount) AS Hits,
StatisticsObjectCulture
 FROM Analytics_Statistics JOIN Analytics_HourHits ON HitsStatisticsID = StatisticsID 
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND StatisticsCode LIKE ''mvtconversion;%''
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)   
  AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
  GROUP BY SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)), StatisticsObjectCulture
 )
 AS Y ON X.MVTCombinationName = Y.MVTCombinationName AND X.StatisticsObjectCulture = Y.StatisticsObjectCulture
 
 
 GROUP BY X.Name
 ORDER BY [{$om.total$}] Desc
'
WHERE 
TableReportID IN (SELECT ReportID FROM Reporting_Report WHERE ReportName= 'mvtestconversioncount.hourreport')
AND TableName = 'table'


--- MVT VALUE ---
UPDATE Reporting_ReportTable SET TableQuery=
'
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''year'');

SELECT X.Name AS ''{$mvtcombination.name$}'',ISNULL (SUM(Y.Hits),0) AS ''{$om.selectedperiod$}'',
ISNULL(SUM(X.Hits),0) AS ''{$om.total$}''
  FROM 
(
SELECT 
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END   
    AS Name,MVTCombinationName, ISNULL(SUM (HitsValue),0) AS Hits,StatisticsObjectCulture
 FROM Analytics_Statistics
 INNER JOIN OM_MVTCombination ON 
  MVTCombinationPageTemplateID IN ( SELECT DocumentPageTemplateID FROM View_CMS_Tree_Joined WHERE NodeSiteID = @CMSContextCurrentSiteID
        AND NodeAliasPath IN(SELECT MVTestPage FROM OM_MVTest WHERE MVTestName = @MVTestName AND MVTestSiteID = @CMSContextCurrentSiteID)                        
        AND DocumentCulture = StatisticsObjectCulture)   
  LEFT JOIN Analytics_YearHits ON StatisticsID = HitsStatisticsID

 WHERE   (StatisticsSiteID = @CMSContextCurrentSiteID ) AND StatisticsCode LIKE ''mvtconversion;%'' 
 AND  @MVTestName = SUBSTRING(StatisticsCode, 15, CHARINDEX('';'',StatisticsCode,15)-15) AND
 MVTCombinationName = (SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)))
 AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY MVTCombinationName, StatisticsObjectCulture,
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END
) AS X 
LEFT JOIN (SELECT SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)) AS MVTCombinationName, SUM(HitsValue) AS Hits,
StatisticsObjectCulture
 FROM Analytics_Statistics JOIN Analytics_YearHits ON HitsStatisticsID = StatisticsID 
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND StatisticsCode LIKE ''mvtconversion;%''
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)   
  AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
  GROUP BY SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)), StatisticsObjectCulture
 )
 AS Y ON X.MVTCombinationName = Y.MVTCombinationName AND X.StatisticsObjectCulture = Y.StatisticsObjectCulture
 
 GROUP BY X.Name
 ORDER BY [{$om.total$}] Desc
'
WHERE 
TableReportID IN (SELECT ReportID FROM Reporting_Report WHERE ReportName= 'mvtestconversionvalue.monthreport')
AND TableName = 'table'

UPDATE Reporting_ReportTable SET TableQuery=
'
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''month'');

SELECT X.Name AS ''{$mvtcombination.name$}'',ISNULL (SUM(Y.Hits),0) AS ''{$om.selectedperiod$}'',ISNULL(SUM(X.Hits),0) AS ''{$om.total$}''  FROM 
(
SELECT 
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END   
    AS Name,MVTCombinationName, ISNULL(SUM (HitsValue),0) AS Hits,StatisticsObjectCulture
 FROM Analytics_Statistics
 INNER JOIN OM_MVTCombination ON 
  MVTCombinationPageTemplateID IN ( SELECT DocumentPageTemplateID FROM View_CMS_Tree_Joined WHERE NodeSiteID = @CMSContextCurrentSiteID
        AND NodeAliasPath IN(SELECT MVTestPage FROM OM_MVTest WHERE MVTestName = @MVTestName AND MVTestSiteID = @CMSContextCurrentSiteID)                        
        AND DocumentCulture = StatisticsObjectCulture)   
  LEFT JOIN Analytics_MonthHits ON StatisticsID = HitsStatisticsID

 WHERE   (StatisticsSiteID = @CMSContextCurrentSiteID ) AND StatisticsCode LIKE ''mvtconversion;%'' 
 AND  @MVTestName = SUBSTRING(StatisticsCode, 15, CHARINDEX('';'',StatisticsCode,15)-15) AND
 MVTCombinationName = (SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)))
 AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY MVTCombinationName, StatisticsObjectCulture,
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END
) AS X 
LEFT JOIN (SELECT SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)) AS MVTCombinationName, SUM(HitsValue) AS Hits,
StatisticsObjectCulture
 FROM Analytics_Statistics JOIN Analytics_MonthHits ON HitsStatisticsID = StatisticsID 
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND StatisticsCode LIKE ''mvtconversion;%''
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)   
  AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
  GROUP BY SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)), StatisticsObjectCulture
 )
 AS Y ON X.MVTCombinationName = Y.MVTCombinationName AND X.StatisticsObjectCulture = Y.StatisticsObjectCulture
 
 
 GROUP BY X.Name
 ORDER BY [{$om.total$}] Desc
'
WHERE 
TableReportID IN (SELECT ReportID FROM Reporting_Report WHERE ReportName= 'mvtestconversionvalue.weekreport')
AND TableName = 'table'

UPDATE Reporting_ReportTable SET TableQuery=
'
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''week'');

SELECT X.Name AS ''{$mvtcombination.name$}'',ISNULL (SUM(Y.Hits),0) AS ''{$om.selectedperiod$}'',ISNULL(SUM(X.Hits),0) AS ''{$om.total$}''  FROM 
(
SELECT 
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END   
    AS Name,MVTCombinationName, ISNULL(SUM (HitsValue),0) AS Hits,StatisticsObjectCulture
 FROM Analytics_Statistics
 INNER JOIN OM_MVTCombination ON 
  MVTCombinationPageTemplateID IN ( SELECT DocumentPageTemplateID FROM View_CMS_Tree_Joined WHERE NodeSiteID = @CMSContextCurrentSiteID
        AND NodeAliasPath IN(SELECT MVTestPage FROM OM_MVTest WHERE MVTestName = @MVTestName AND MVTestSiteID = @CMSContextCurrentSiteID)                        
        AND DocumentCulture = StatisticsObjectCulture)   
  LEFT JOIN Analytics_WeekHits ON StatisticsID = HitsStatisticsID

 WHERE   (StatisticsSiteID = @CMSContextCurrentSiteID ) AND StatisticsCode LIKE ''mvtconversion;%'' 
 AND  @MVTestName = SUBSTRING(StatisticsCode, 15, CHARINDEX('';'',StatisticsCode,15)-15) AND
 MVTCombinationName = (SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)))
 AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY MVTCombinationName, StatisticsObjectCulture,
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END
) AS X 
LEFT JOIN (SELECT SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)) AS MVTCombinationName, SUM(HitsValue) AS Hits,
StatisticsObjectCulture
 FROM Analytics_Statistics JOIN Analytics_WeekHits ON HitsStatisticsID = StatisticsID 
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND StatisticsCode LIKE ''mvtconversion;%''
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)   
  AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
  GROUP BY SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)), StatisticsObjectCulture
 )
 AS Y ON X.MVTCombinationName = Y.MVTCombinationName AND X.StatisticsObjectCulture = Y.StatisticsObjectCulture
 
 
 GROUP BY X.Name
 ORDER BY [{$om.total$}] Desc
'
WHERE 
TableReportID IN (SELECT ReportID FROM Reporting_Report WHERE ReportName= 'mvtestconversionvalue.weekreport')
AND TableName = 'table'

UPDATE Reporting_ReportTable SET TableQuery=
'
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day'');

SELECT X.Name AS ''{$mvtcombination.name$}'',ISNULL (SUM(Y.Hits),0) AS ''{$om.selectedperiod$}'',ISNULL(SUM(X.Hits),0) AS ''{$om.total$}''  FROM 
(
SELECT 
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END   
    AS Name,MVTCombinationName, ISNULL(SUM (HitsValue),0) AS Hits,StatisticsObjectCulture
 FROM Analytics_Statistics
 INNER JOIN OM_MVTCombination ON 
  MVTCombinationPageTemplateID IN ( SELECT DocumentPageTemplateID FROM View_CMS_Tree_Joined WHERE NodeSiteID = @CMSContextCurrentSiteID
        AND NodeAliasPath IN(SELECT MVTestPage FROM OM_MVTest WHERE MVTestName = @MVTestName AND MVTestSiteID = @CMSContextCurrentSiteID)                        
        AND DocumentCulture = StatisticsObjectCulture)   
  LEFT JOIN Analytics_DayHits ON StatisticsID = HitsStatisticsID

 WHERE   (StatisticsSiteID = @CMSContextCurrentSiteID ) AND StatisticsCode LIKE ''mvtconversion;%'' 
 AND  @MVTestName = SUBSTRING(StatisticsCode, 15, CHARINDEX('';'',StatisticsCode,15)-15) AND
 MVTCombinationName = (SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)))
 AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY MVTCombinationName, StatisticsObjectCulture,
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END
) AS X 
LEFT JOIN (SELECT SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)) AS MVTCombinationName, SUM(HitsValue) AS Hits,
StatisticsObjectCulture
 FROM Analytics_Statistics JOIN Analytics_DayHits ON HitsStatisticsID = StatisticsID 
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND StatisticsCode LIKE ''mvtconversion;%''
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)   
  AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
  GROUP BY SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)), StatisticsObjectCulture
 )
 AS Y ON X.MVTCombinationName = Y.MVTCombinationName AND X.StatisticsObjectCulture = Y.StatisticsObjectCulture
 
 
 GROUP BY X.Name
 ORDER BY [{$om.total$}] Desc
'
WHERE 
TableReportID IN (SELECT ReportID FROM Reporting_Report WHERE ReportName= 'mvtestconversionvalue.dayreport')
AND TableName = 'table'

UPDATE Reporting_ReportTable SET TableQuery=
'
SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''hour'');

SELECT X.Name AS ''{$mvtcombination.name$}'',ISNULL (SUM(Y.Hits),0) AS ''{$om.selectedperiod$}'',ISNULL(SUM(X.Hits),0) AS ''{$om.total$}''  FROM 
(
SELECT 
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END   
    AS Name,MVTCombinationName, ISNULL(SUM (HitsValue),0) AS Hits,StatisticsObjectCulture
 FROM Analytics_Statistics
 INNER JOIN OM_MVTCombination ON 
  MVTCombinationPageTemplateID IN ( SELECT DocumentPageTemplateID FROM View_CMS_Tree_Joined WHERE NodeSiteID = @CMSContextCurrentSiteID
        AND NodeAliasPath IN(SELECT MVTestPage FROM OM_MVTest WHERE MVTestName = @MVTestName AND MVTestSiteID = @CMSContextCurrentSiteID)                        
        AND DocumentCulture = StatisticsObjectCulture)   
  LEFT JOIN Analytics_HourHits ON StatisticsID = HitsStatisticsID

 WHERE   (StatisticsSiteID = @CMSContextCurrentSiteID ) AND StatisticsCode LIKE ''mvtconversion;%'' 
 AND  @MVTestName = SUBSTRING(StatisticsCode, 15, CHARINDEX('';'',StatisticsCode,15)-15) AND
 MVTCombinationName = (SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)))
 AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY MVTCombinationName, StatisticsObjectCulture,
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END
) AS X 
LEFT JOIN (SELECT SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)) AS MVTCombinationName, SUM(HitsValue) AS Hits,
StatisticsObjectCulture
 FROM Analytics_Statistics JOIN Analytics_HourHits ON HitsStatisticsID = StatisticsID 
 WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND StatisticsCode LIKE ''mvtconversion;%''
  AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)   
  AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
  GROUP BY SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)), StatisticsObjectCulture
 )
 AS Y ON X.MVTCombinationName = Y.MVTCombinationName AND X.StatisticsObjectCulture = Y.StatisticsObjectCulture
 
 
 GROUP BY X.Name
 ORDER BY [{$om.total$}] Desc
'
WHERE 
TableReportID IN (SELECT ReportID FROM Reporting_Report WHERE ReportName= 'mvtestconversionvalue.hourreport')
AND TableName = 'table'


-- AB TEST COUNT--
UPDATE Reporting_ReportTable SET TableQuery=
'
SET @FromDate = {%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
SET @ToDate = {%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''year'');

SELECT X.Name AS ''{$om.variant.tabletitle$}'',ISNULL (Y.Hits,0) AS ''{$om.selectedperiod$}'',
ISNULL(X.Hits,0) AS ''{$om.total$}''  FROM
(
SELECT ABVariantDisplayName AS Name, ISNULL(SUM (HitsCount),0) AS Hits,ABVariantName FROM Analytics_Statistics
  LEFT JOIN OM_ABVariant ON ABVariantSiteID = @CMSContextCurrentSiteID AND
    ABVariantName = SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))
  LEFT JOIN Analytics_YearHits ON StatisticsID = HitsSTatisticsID    

 WHERE   StatisticsSiteID = @CMSContextCurrentSiteID AND StatisticsCode LIKE ''abconversion;%'' AND
 @TestName = SUBSTRING(StatisticsCode, 14, CHARINDEX('';'',StatisticsCode,14)-14) AND
 ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY ABVariantDisplayName,ABVariantName
)
 AS X
LEFT JOIN (SELECT
  SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode)) AS ABVariantName, SUM(HitsCount) AS Hits FROM Analytics_Statistics
  LEFT JOIN Analytics_YearHits ON HitsStatisticsID = StatisticsID
  
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND StatisticsCode LIKE ''abconversion;%''
    AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
    AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
     
  GROUP BY SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))
)
AS Y ON X.ABVariantName = Y.ABVariantName

 
ORDER BY X.Hits Desc
'
WHERE 
TableReportID IN (SELECT ReportID FROM Reporting_Report WHERE ReportName= 'abtestconversioncount.yearreport')
AND TableName = 'table'

UPDATE Reporting_ReportTable SET TableQuery=
'
SET @FromDate = {%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
SET @ToDate = {%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''month'');

SELECT X.Name AS ''{$om.variant.tabletitle$}'',ISNULL (Y.Hits,0) AS ''{$om.selectedperiod$}'',
ISNULL(X.Hits,0) AS ''{$om.total$}''  FROM
(
SELECT ABVariantDisplayName AS Name, ISNULL(SUM (HitsCount),0) AS Hits,ABVariantName FROM Analytics_Statistics
  LEFT JOIN OM_ABVariant ON ABVariantSiteID = @CMSContextCurrentSiteID AND
    ABVariantName = SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))
  LEFT JOIN Analytics_MonthHits ON StatisticsID = HitsSTatisticsID    

 WHERE   StatisticsSiteID = @CMSContextCurrentSiteID AND StatisticsCode LIKE ''abconversion;%'' AND
 @TestName = SUBSTRING(StatisticsCode, 14, CHARINDEX('';'',StatisticsCode,14)-14) AND
 ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY ABVariantDisplayName,ABVariantName
)
 AS X
LEFT JOIN (SELECT
  SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode)) AS ABVariantName, SUM(HitsCount) AS Hits FROM Analytics_Statistics
  LEFT JOIN Analytics_MonthHits ON HitsStatisticsID = StatisticsID
  
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND StatisticsCode LIKE ''abconversion;%''
    AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
    AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
     
  GROUP BY SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))
)
AS Y ON X.ABVariantName = Y.ABVariantName

 
ORDER BY X.Hits Desc
'
WHERE 
TableReportID IN (SELECT ReportID FROM Reporting_Report WHERE ReportName= 'abtestconversioncount.monthreport')
AND TableName = 'table'

UPDATE Reporting_ReportTable SET TableQuery=
'
SET @FromDate = {%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week'');
SET @ToDate = {%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''week'');

SELECT X.Name AS ''{$om.variant.tabletitle$}'',ISNULL (Y.Hits,0) AS ''{$om.selectedperiod$}'',
ISNULL(X.Hits,0) AS ''{$om.total$}''  FROM
(
SELECT ABVariantDisplayName AS Name, ISNULL(SUM (HitsCount),0) AS Hits,ABVariantName FROM Analytics_Statistics
  LEFT JOIN OM_ABVariant ON ABVariantSiteID = @CMSContextCurrentSiteID AND
    ABVariantName = SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))
  LEFT JOIN Analytics_WeekHits ON StatisticsID = HitsSTatisticsID    

 WHERE   StatisticsSiteID = @CMSContextCurrentSiteID AND StatisticsCode LIKE ''abconversion;%'' AND
 @TestName = SUBSTRING(StatisticsCode, 14, CHARINDEX('';'',StatisticsCode,14)-14) AND
 ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY ABVariantDisplayName,ABVariantName
)
 AS X
LEFT JOIN (SELECT
  SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode)) AS ABVariantName, SUM(HitsCount) AS Hits FROM Analytics_Statistics
  LEFT JOIN Analytics_WeekHits ON HitsStatisticsID = StatisticsID
  
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND StatisticsCode LIKE ''abconversion;%''
    AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
    AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
     
  GROUP BY SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))
)
AS Y ON X.ABVariantName = Y.ABVariantName

 
ORDER BY X.Hits Desc
'
WHERE 
TableReportID IN (SELECT ReportID FROM Reporting_Report WHERE ReportName= 'abtestconversioncount.weekreport')
AND TableName ='table'

UPDATE Reporting_ReportTable SET TableQuery=
'
SET @FromDate = {%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
SET @ToDate = {%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day'');

SELECT X.Name AS ''{$om.variant.tabletitle$}'',ISNULL (Y.Hits,0) AS ''{$om.selectedperiod$}'',
ISNULL(X.Hits,0) AS ''{$om.total$}''  FROM
(
SELECT ABVariantDisplayName AS Name, ISNULL(SUM (HitsCount),0) AS Hits,ABVariantName FROM Analytics_Statistics
  LEFT JOIN OM_ABVariant ON ABVariantSiteID = @CMSContextCurrentSiteID AND
    ABVariantName = SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))
  LEFT JOIN Analytics_DayHits ON StatisticsID = HitsSTatisticsID    

 WHERE   StatisticsSiteID = @CMSContextCurrentSiteID AND StatisticsCode LIKE ''abconversion;%'' AND
 @TestName = SUBSTRING(StatisticsCode, 14, CHARINDEX('';'',StatisticsCode,14)-14) AND
 ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY ABVariantDisplayName,ABVariantName
)
 AS X
LEFT JOIN (SELECT
  SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode)) AS ABVariantName, SUM(HitsCount) AS Hits FROM Analytics_Statistics
  LEFT JOIN Analytics_DayHits ON HitsStatisticsID = StatisticsID
  
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND StatisticsCode LIKE ''abconversion;%''
    AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
    AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
     
  GROUP BY SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))
)
AS Y ON X.ABVariantName = Y.ABVariantName

 
ORDER BY X.Hits Desc

'
WHERE 
TableReportID IN (SELECT ReportID FROM Reporting_Report WHERE ReportName= 'abtestconversioncount.dayreport')
AND TableName = 'table'

UPDATE Reporting_ReportTable SET TableQuery=
'
SET @FromDate = {%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
SET @ToDate = {%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''hour'');

SELECT X.Name AS ''{$om.variant.tabletitle$}'',ISNULL (Y.Hits,0) AS ''{$om.selectedperiod$}'',
ISNULL(X.Hits,0) AS ''{$om.total$}''  FROM
(
SELECT ABVariantDisplayName AS Name, ISNULL(SUM (HitsCount),0) AS Hits,ABVariantName FROM Analytics_Statistics
  LEFT JOIN OM_ABVariant ON ABVariantSiteID = @CMSContextCurrentSiteID AND
    ABVariantName = SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))
  LEFT JOIN Analytics_HourHits ON StatisticsID = HitsSTatisticsID    

 WHERE   StatisticsSiteID = @CMSContextCurrentSiteID AND StatisticsCode LIKE ''abconversion;%'' AND
 @TestName = SUBSTRING(StatisticsCode, 14, CHARINDEX('';'',StatisticsCode,14)-14) AND
 ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY ABVariantDisplayName,ABVariantName
)
 AS X
LEFT JOIN (SELECT
  SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode)) AS ABVariantName, SUM(HitsCount) AS Hits FROM Analytics_Statistics
  LEFT JOIN Analytics_HourHits ON HitsStatisticsID = StatisticsID
  
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND StatisticsCode LIKE ''abconversion;%''
    AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
    AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
     
  GROUP BY SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))
)
AS Y ON X.ABVariantName = Y.ABVariantName

 
ORDER BY X.Hits Desc
'
WHERE 
TableReportID IN (SELECT ReportID FROM Reporting_Report WHERE ReportName= 'abtestconversioncount.hourreport')
AND TableName = 'table'

-- ABTEST VALUE

UPDATE Reporting_ReportTable SET TableQuery=
'
SET @FromDate = {%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
SET @ToDate = {%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''year'');

SELECT X.Name AS ''{$om.variant.tabletitle$}'',ISNULL (Y.Hits,0) AS ''{$om.selectedperiod$}'',
ISNULL(X.Hits,0) AS ''{$om.total$}''  FROM
(
SELECT ABVariantDisplayName AS Name, ISNULL(SUM (HitsValue),0) AS Hits,ABVariantName FROM Analytics_Statistics
  LEFT JOIN OM_ABVariant ON ABVariantSiteID = @CMSContextCurrentSiteID AND
    ABVariantName = SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))
  LEFT JOIN Analytics_YearHits ON StatisticsID = HitsSTatisticsID    

 WHERE   StatisticsSiteID = @CMSContextCurrentSiteID AND StatisticsCode LIKE ''abconversion;%'' AND
 @TestName = SUBSTRING(StatisticsCode, 14, CHARINDEX('';'',StatisticsCode,14)-14) AND
 ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY ABVariantDisplayName,ABVariantName
)
 AS X
LEFT JOIN (SELECT
  SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode)) AS ABVariantName, SUM(HitsValue) AS Hits FROM Analytics_Statistics
  LEFT JOIN Analytics_YearHits ON HitsStatisticsID = StatisticsID
  
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND StatisticsCode LIKE ''abconversion;%''
    AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
    AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
     
  GROUP BY SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))
)
AS Y ON X.ABVariantName = Y.ABVariantName

 
ORDER BY X.Hits Desc
'
WHERE 
TableReportID IN (SELECT ReportID FROM Reporting_Report WHERE ReportName= 'abtestconversionvalue.yearreport')
AND TableName = 'table'

UPDATE Reporting_ReportTable SET TableQuery=
'
SET @FromDate = {%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
SET @ToDate = {%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''month'');

SELECT X.Name AS ''{$om.variant.tabletitle$}'',ISNULL (Y.Hits,0) AS ''{$om.selectedperiod$}'',
ISNULL(X.Hits,0) AS ''{$om.total$}''  FROM
(
SELECT ABVariantDisplayName AS Name, ISNULL(SUM (HitsValue),0) AS Hits,ABVariantName FROM Analytics_Statistics
  LEFT JOIN OM_ABVariant ON ABVariantSiteID = @CMSContextCurrentSiteID AND
    ABVariantName = SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))
  LEFT JOIN Analytics_MonthHits ON StatisticsID = HitsSTatisticsID    

 WHERE   StatisticsSiteID = @CMSContextCurrentSiteID AND StatisticsCode LIKE ''abconversion;%'' AND
 @TestName = SUBSTRING(StatisticsCode, 14, CHARINDEX('';'',StatisticsCode,14)-14) AND
 ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY ABVariantDisplayName,ABVariantName
)
 AS X
LEFT JOIN (SELECT
  SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode)) AS ABVariantName, SUM(HitsValue) AS Hits FROM Analytics_Statistics
  LEFT JOIN Analytics_MonthHits ON HitsStatisticsID = StatisticsID
  
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND StatisticsCode LIKE ''abconversion;%''
    AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
    AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
     
  GROUP BY SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))
)
AS Y ON X.ABVariantName = Y.ABVariantName

 
ORDER BY X.Hits Desc
'
WHERE 
TableReportID IN (SELECT ReportID FROM Reporting_Report WHERE ReportName= 'abtestconversionvalue.monthreport')
AND TableName = 'table'

UPDATE Reporting_ReportTable SET TableQuery=
'
SET @FromDate = {%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week'');
SET @ToDate = {%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''week'');

SELECT X.Name AS ''{$om.variant.tabletitle$}'',ISNULL (Y.Hits,0) AS ''{$om.selectedperiod$}'',
ISNULL(X.Hits,0) AS ''{$om.total$}''  FROM
(
SELECT ABVariantDisplayName AS Name, ISNULL(SUM (HitsValue),0) AS Hits,ABVariantName FROM Analytics_Statistics
  LEFT JOIN OM_ABVariant ON ABVariantSiteID = @CMSContextCurrentSiteID AND
    ABVariantName = SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))
  LEFT JOIN Analytics_WeekHits ON StatisticsID = HitsSTatisticsID    

 WHERE   StatisticsSiteID = @CMSContextCurrentSiteID AND StatisticsCode LIKE ''abconversion;%'' AND
 @TestName = SUBSTRING(StatisticsCode, 14, CHARINDEX('';'',StatisticsCode,14)-14) AND
 ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY ABVariantDisplayName,ABVariantName
)
 AS X
LEFT JOIN (SELECT
  SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode)) AS ABVariantName, SUM(HitsValue) AS Hits FROM Analytics_Statistics
  LEFT JOIN Analytics_WeekHits ON HitsStatisticsID = StatisticsID
  
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND StatisticsCode LIKE ''abconversion;%''
    AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
    AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
     
  GROUP BY SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))
)
AS Y ON X.ABVariantName = Y.ABVariantName

 
ORDER BY X.Hits Desc
'
WHERE 
TableReportID IN (SELECT ReportID FROM Reporting_Report WHERE ReportName= 'abtestconversionvalue.weekreport')
AND TableName = 'table'

UPDATE Reporting_ReportTable SET TableQuery=
'
SET @FromDate = {%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
SET @ToDate = {%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day'');

SELECT X.Name AS ''{$om.variant.tabletitle$}'',ISNULL (Y.Hits,0) AS ''{$om.selectedperiod$}'',
ISNULL(X.Hits,0) AS ''{$om.total$}''  FROM
(
SELECT ABVariantDisplayName AS Name, ISNULL(SUM (HitsValue),0) AS Hits,ABVariantName FROM Analytics_Statistics
  LEFT JOIN OM_ABVariant ON ABVariantSiteID = @CMSContextCurrentSiteID AND
    ABVariantName = SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))
  LEFT JOIN Analytics_DayHits ON StatisticsID = HitsSTatisticsID    

 WHERE   StatisticsSiteID = @CMSContextCurrentSiteID AND StatisticsCode LIKE ''abconversion;%'' AND
 @TestName = SUBSTRING(StatisticsCode, 14, CHARINDEX('';'',StatisticsCode,14)-14) AND
 ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY ABVariantDisplayName,ABVariantName
)
 AS X
LEFT JOIN (SELECT
  SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode)) AS ABVariantName, SUM(HitsValue) AS Hits FROM Analytics_Statistics
  LEFT JOIN Analytics_DayHits ON HitsStatisticsID = StatisticsID
  
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND StatisticsCode LIKE ''abconversion;%''
    AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
    AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
     
  GROUP BY SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))
)
AS Y ON X.ABVariantName = Y.ABVariantName

 
ORDER BY X.Hits Desc

'
WHERE 
TableReportID IN (SELECT ReportID FROM Reporting_Report WHERE ReportName= 'abtestconversionvalue.dayreport')
AND TableName = 'table'

UPDATE Reporting_ReportTable SET TableQuery=
'
SET @FromDate = {%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
SET @ToDate = {%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''hour'');

SELECT X.Name AS ''{$om.variant.tabletitle$}'',ISNULL (Y.Hits,0) AS ''{$om.selectedperiod$}'',
ISNULL(X.Hits,0) AS ''{$om.total$}''  FROM
(
SELECT ABVariantDisplayName AS Name, ISNULL(SUM (HitsValue),0) AS Hits,ABVariantName FROM Analytics_Statistics
  LEFT JOIN OM_ABVariant ON ABVariantSiteID = @CMSContextCurrentSiteID AND
    ABVariantName = SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))
  LEFT JOIN Analytics_HourHits ON StatisticsID = HitsSTatisticsID    

 WHERE   StatisticsSiteID = @CMSContextCurrentSiteID AND StatisticsCode LIKE ''abconversion;%'' AND
 @TestName = SUBSTRING(StatisticsCode, 14, CHARINDEX('';'',StatisticsCode,14)-14) AND
 ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY ABVariantDisplayName,ABVariantName
)
 AS X
LEFT JOIN (SELECT
  SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode)) AS ABVariantName, SUM(HitsValue) AS Hits FROM Analytics_Statistics
  LEFT JOIN Analytics_HourHits ON HitsStatisticsID = StatisticsID
  
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID) AND StatisticsCode LIKE ''abconversion;%''
    AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
    AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
     
  GROUP BY SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))
)
AS Y ON X.ABVariantName = Y.ABVariantName
 
ORDER BY X.Hits Desc
'
WHERE 
TableReportID IN (SELECT ReportID FROM Reporting_Report WHERE ReportName= 'abtestconversionvalue.hourreport')
AND TableName = 'table'


-- Conversion Rate
UPDATE Reporting_ReportTable SET TableQuery=
'
EXEC Proc_Analytics_RemoveTempTable 
 CREATE TABLE #AnalyticsTempTable (  
  Hits DECIMAL(15,2),
  Name NVARCHAR(300) COLLATE database_default,  
  Culture NVARCHAR(300) COLLATE database_default,   
  Page NVARCHAR(300) COLLATE database_default   
);

SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''year'');

INSERT INTO #AnalyticsTempTable (Name,Hits,Page,Culture)
SELECT X.Name AS Name,CAST (ISNULL (X.Hits,0) AS DECIMAL (15,2)) AS Hits, X.Page AS Page,X.Culture AS Culture  FROM
(
SELECT ABVariantDisplayName AS Name, ISNULL(SUM (HitsCount),0) AS Hits,ABVariantName,ABVariantPath AS Page, ABTestCulture AS Culture FROM Analytics_Statistics
  LEFT JOIN OM_ABVariant ON ABVariantSiteID = @CMSContextCurrentSiteID AND
    ABVariantName = SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))         
  LEFT JOIN Analytics_YearHits ON StatisticsID = HitsSTatisticsID    
  LEFT JOIN OM_ABTest ON ABTestSiteID = @CMSContextCurrentSiteID AND ABTestName = @TestName AND ABVariantTestID = ABTestID

 WHERE   StatisticsSiteID = @CMSContextCurrentSiteID AND StatisticsCode LIKE ''abconversion;%'' AND
 @TestName = SUBSTRING(StatisticsCode, 14, CHARINDEX('';'',StatisticsCode,14)-14)
 AND HitsStartTime >= @FromDate AND HitsEndTime <= @ToDate   
 AND  ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY ABVariantDisplayName,ABVariantName,ABVariantPath, ABTestCulture
)
 AS X

ORDER BY X.Hits Desc

 UPDATE #AnalyticsTempTable SET Hits = ISNULL (Hits/
 (
   SELECT CAST (SUM (HitsCount) AS DECIMAL (10,2)) FROM Analytics_Statistics
   JOIN Analytics_YearHits ON Analytics_Statistics.StatisticsID = Analytics_YearHits.HitsStatisticsID
   WHERE Analytics_Statistics.StatisticsCode = ''pageviews''  
   AND HitsStartTime >= @FromDate AND HitsEndTime <= @ToDate   
   AND Analytics_Statistics.StatisticsObjectID IN
  (SELECT NodeID FROM View_CMS_Tree_Joined WHERE NodeAliasPath = Page AND NodeSiteID = @CMSContextCurrentSiteID AND (DocumentCulture = StatisticsObjectCulture OR StatisticsObjectCulture IS NULL))
     /*culture */
     AND (StatisticsObjectCulture IS NULL OR StatisticsObjectCulture = Culture OR Culture IS NULL)    
   )
  *100  ,0)
  
SELECT Name AS ''{$om.variant.tabletitle$}'',
   CAST (CAST (ISNULL (Hits,0) AS DECIMAL (15,1)) AS NVARCHAR(15)) + ''%'' AS ''{$om.selectedperiod$}''  
 FROM #AnalyticsTempTable ORDER BY Hits DESC

EXEC Proc_Analytics_RemoveTempTable


'
WHERE 
TableReportID IN (SELECT ReportID FROM Reporting_Report WHERE ReportName= 'abtestdetailconversionrate.yearreport')
AND TableName = 'table'

UPDATE Reporting_ReportTable SET TableQuery=
'
EXEC Proc_Analytics_RemoveTempTable 
 CREATE TABLE #AnalyticsTempTable (  
  Hits DECIMAL(15,2),
  Name NVARCHAR(300) COLLATE database_default,  
  Culture NVARCHAR(300) COLLATE database_default,   
  Page NVARCHAR(300) COLLATE database_default   
);

SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''month'');

INSERT INTO #AnalyticsTempTable (Name,Hits,Page,Culture)
SELECT X.Name AS Name,CAST (ISNULL (X.Hits,0) AS DECIMAL (15,2)) AS Hits, X.Page AS Page,X.Culture AS Culture  FROM
(
SELECT ABVariantDisplayName AS Name, ISNULL(SUM (HitsCount),0) AS Hits,ABVariantName,ABVariantPath AS Page, ABTestCulture AS Culture FROM Analytics_Statistics
  LEFT JOIN OM_ABVariant ON ABVariantSiteID = @CMSContextCurrentSiteID AND
    ABVariantName = SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))         
  LEFT JOIN Analytics_MonthHits ON StatisticsID = HitsSTatisticsID    
  LEFT JOIN OM_ABTest ON ABTestSiteID = @CMSContextCurrentSiteID AND ABTestName = @TestName AND ABVariantTestID = ABTestID

 WHERE   StatisticsSiteID = @CMSContextCurrentSiteID AND StatisticsCode LIKE ''abconversion;%'' AND
 @TestName = SUBSTRING(StatisticsCode, 14, CHARINDEX('';'',StatisticsCode,14)-14)
 AND HitsStartTime >= @FromDate AND HitsEndTime <= @ToDate   
 AND  ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY ABVariantDisplayName,ABVariantName,ABVariantPath, ABTestCulture
)
 AS X

ORDER BY X.Hits Desc

 UPDATE #AnalyticsTempTable SET Hits = ISNULL (Hits/
 (
   SELECT CAST (SUM (HitsCount) AS DECIMAL (10,2)) FROM Analytics_Statistics
   JOIN Analytics_MonthHits ON Analytics_Statistics.StatisticsID = Analytics_MonthHits.HitsStatisticsID
   WHERE Analytics_Statistics.StatisticsCode = ''pageviews''  
   AND HitsStartTime >= @FromDate AND HitsEndTime <= @ToDate   
   AND Analytics_Statistics.StatisticsObjectID IN
  (SELECT NodeID FROM View_CMS_Tree_Joined WHERE NodeAliasPath = Page AND NodeSiteID = @CMSContextCurrentSiteID AND (DocumentCulture = StatisticsObjectCulture OR StatisticsObjectCulture IS NULL))
     /*culture */
     AND (StatisticsObjectCulture IS NULL OR StatisticsObjectCulture = Culture OR Culture IS NULL)    
   )
  *100  ,0)
  
SELECT Name AS ''{$om.variant.tabletitle$}'',
   CAST (CAST (ISNULL (Hits,0) AS DECIMAL (15,1)) AS NVARCHAR(15)) + ''%'' AS ''{$om.selectedperiod$}''  
 FROM #AnalyticsTempTable ORDER BY Hits DESC

EXEC Proc_Analytics_RemoveTempTable


'
WHERE 
TableReportID IN (SELECT ReportID FROM Reporting_Report WHERE ReportName= 'abtestdetailconversionrate.monthreport')
AND TableName = 'table'


UPDATE Reporting_ReportTable SET TableQuery=
'
EXEC Proc_Analytics_RemoveTempTable 
 CREATE TABLE #AnalyticsTempTable (  
  Hits DECIMAL(15,2),
  Name NVARCHAR(300) COLLATE database_default,  
  Culture NVARCHAR(300) COLLATE database_default,   
  Page NVARCHAR(300) COLLATE database_default   
);

SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''week'');

INSERT INTO #AnalyticsTempTable (Name,Hits,Page,Culture)
SELECT X.Name AS Name,CAST (ISNULL (X.Hits,0) AS DECIMAL (15,2)) AS Hits, X.Page AS Page,X.Culture AS Culture  FROM
(
SELECT ABVariantDisplayName AS Name, ISNULL(SUM (HitsCount),0) AS Hits,ABVariantName,ABVariantPath AS Page, ABTestCulture AS Culture FROM Analytics_Statistics
  LEFT JOIN OM_ABVariant ON ABVariantSiteID = @CMSContextCurrentSiteID AND
    ABVariantName = SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))         
  LEFT JOIN Analytics_WeekHits ON StatisticsID = HitsSTatisticsID    
  LEFT JOIN OM_ABTest ON ABTestSiteID = @CMSContextCurrentSiteID AND ABTestName = @TestName AND ABVariantTestID = ABTestID

 WHERE   StatisticsSiteID = @CMSContextCurrentSiteID AND StatisticsCode LIKE ''abconversion;%'' AND
 @TestName = SUBSTRING(StatisticsCode, 14, CHARINDEX('';'',StatisticsCode,14)-14)
 AND HitsStartTime >= @FromDate AND HitsEndTime <= @ToDate   
 AND  ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY ABVariantDisplayName,ABVariantName,ABVariantPath, ABTestCulture
)
 AS X

ORDER BY X.Hits Desc

 UPDATE #AnalyticsTempTable SET Hits = ISNULL (Hits/
 (
   SELECT CAST (SUM (HitsCount) AS DECIMAL (10,2)) FROM Analytics_Statistics
   JOIN Analytics_WeekHits ON Analytics_Statistics.StatisticsID = Analytics_WeekHits.HitsStatisticsID
   WHERE Analytics_Statistics.StatisticsCode = ''pageviews''  
   AND HitsStartTime >= @FromDate AND HitsEndTime <= @ToDate   
   AND Analytics_Statistics.StatisticsObjectID IN
  (SELECT NodeID FROM View_CMS_Tree_Joined WHERE NodeAliasPath = Page AND NodeSiteID = @CMSContextCurrentSiteID AND (DocumentCulture = StatisticsObjectCulture OR StatisticsObjectCulture IS NULL))
     /*culture */
     AND (StatisticsObjectCulture IS NULL OR StatisticsObjectCulture = Culture OR Culture IS NULL)    
   )
  *100  ,0)
  
SELECT Name AS ''{$om.variant.tabletitle$}'',
   CAST (CAST (ISNULL (Hits,0) AS DECIMAL (15,1)) AS NVARCHAR(15)) + ''%'' AS ''{$om.selectedperiod$}''  
 FROM #AnalyticsTempTable ORDER BY Hits DESC

EXEC Proc_Analytics_RemoveTempTable


'
WHERE 
TableReportID IN (SELECT ReportID FROM Reporting_Report WHERE ReportName= 'abtestdetailconversionrate.weekreport')
AND TableName = 'table'


UPDATE Reporting_ReportTable SET TableQuery=
'
EXEC Proc_Analytics_RemoveTempTable 
 CREATE TABLE #AnalyticsTempTable (  
  Hits DECIMAL(15,2),
  Name NVARCHAR(300) COLLATE database_default,  
  Culture NVARCHAR(300) COLLATE database_default,   
  Page NVARCHAR(300) COLLATE database_default   
);

SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day'');

INSERT INTO #AnalyticsTempTable (Name,Hits,Page,Culture)
SELECT X.Name AS Name,CAST (ISNULL (X.Hits,0) AS DECIMAL (15,2)) AS Hits, X.Page AS Page,X.Culture AS Culture  FROM
(
SELECT ABVariantDisplayName AS Name, ISNULL(SUM (HitsCount),0) AS Hits,ABVariantName,ABVariantPath AS Page, ABTestCulture AS Culture FROM Analytics_Statistics
  LEFT JOIN OM_ABVariant ON ABVariantSiteID = @CMSContextCurrentSiteID AND
    ABVariantName = SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))         
  LEFT JOIN Analytics_DayHits ON StatisticsID = HitsSTatisticsID    
  LEFT JOIN OM_ABTest ON ABTestSiteID = @CMSContextCurrentSiteID AND ABTestName = @TestName AND ABVariantTestID = ABTestID

 WHERE   StatisticsSiteID = @CMSContextCurrentSiteID AND StatisticsCode LIKE ''abconversion;%'' AND
 @TestName = SUBSTRING(StatisticsCode, 14, CHARINDEX('';'',StatisticsCode,14)-14)
 AND HitsStartTime >= @FromDate AND HitsEndTime <= @ToDate   
 AND  ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY ABVariantDisplayName,ABVariantName,ABVariantPath, ABTestCulture
)
 AS X

ORDER BY X.Hits Desc

 UPDATE #AnalyticsTempTable SET Hits = ISNULL (Hits/
 (
   SELECT CAST (SUM (HitsCount) AS DECIMAL (10,2)) FROM Analytics_Statistics
   JOIN Analytics_DayHits ON Analytics_Statistics.StatisticsID = Analytics_DayHits.HitsStatisticsID
   WHERE Analytics_Statistics.StatisticsCode = ''pageviews''  
   AND HitsStartTime >= @FromDate AND HitsEndTime <= @ToDate   
   AND Analytics_Statistics.StatisticsObjectID IN
  (SELECT NodeID FROM View_CMS_Tree_Joined WHERE NodeAliasPath = Page AND NodeSiteID = @CMSContextCurrentSiteID AND (DocumentCulture = StatisticsObjectCulture OR StatisticsObjectCulture IS NULL))
     /*culture */
     AND (StatisticsObjectCulture IS NULL OR StatisticsObjectCulture = Culture OR Culture IS NULL)    
   )
  *100  ,0)
  
SELECT Name AS ''{$om.variant.tabletitle$}'',
   CAST (CAST (ISNULL (Hits,0) AS DECIMAL (15,1)) AS NVARCHAR(15)) + ''%'' AS ''{$om.selectedperiod$}''  
 FROM #AnalyticsTempTable ORDER BY Hits DESC

EXEC Proc_Analytics_RemoveTempTable


'
WHERE 
TableReportID IN (SELECT ReportID FROM Reporting_Report WHERE ReportName= 'abtestdetailconversionrate.dayreport')
AND TableName = 'table'


UPDATE Reporting_ReportTable SET TableQuery=
'
EXEC Proc_Analytics_RemoveTempTable 
 CREATE TABLE #AnalyticsTempTable (  
  Hits DECIMAL(15,2),
  Name NVARCHAR(300) COLLATE database_default,  
  Culture NVARCHAR(300) COLLATE database_default,   
  Page NVARCHAR(300) COLLATE database_default   
);

SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
SET @ToDate ={%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''hour'');

INSERT INTO #AnalyticsTempTable (Name,Hits,Page,Culture)
SELECT X.Name AS Name,CAST (ISNULL (X.Hits,0) AS DECIMAL (15,2)) AS Hits, X.Page AS Page,X.Culture AS Culture  FROM
(
SELECT ABVariantDisplayName AS Name, ISNULL(SUM (HitsCount),0) AS Hits,ABVariantName,ABVariantPath AS Page, ABTestCulture AS Culture FROM Analytics_Statistics
  LEFT JOIN OM_ABVariant ON ABVariantSiteID = @CMSContextCurrentSiteID AND
    ABVariantName = SUBSTRING(StatisticsCode, CHARINDEX('';'',StatisticsCode,14)+1,LEN (StatisticsCode))         
  LEFT JOIN Analytics_HourHits ON StatisticsID = HitsSTatisticsID    
  LEFT JOIN OM_ABTest ON ABTestSiteID = @CMSContextCurrentSiteID AND ABTestName = @TestName AND ABVariantTestID = ABTestID

 WHERE   StatisticsSiteID = @CMSContextCurrentSiteID AND StatisticsCode LIKE ''abconversion;%'' AND
 @TestName = SUBSTRING(StatisticsCode, 14, CHARINDEX('';'',StatisticsCode,14)-14)
 AND HitsStartTime >= @FromDate AND HitsEndTime <= @ToDate   
 AND  ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
 
 GROUP BY ABVariantDisplayName,ABVariantName,ABVariantPath, ABTestCulture
)
 AS X

ORDER BY X.Hits Desc

 UPDATE #AnalyticsTempTable SET Hits = ISNULL (Hits/
 (
   SELECT CAST (SUM (HitsCount) AS DECIMAL (10,2)) FROM Analytics_Statistics
   JOIN Analytics_HourHits ON Analytics_Statistics.StatisticsID = Analytics_HourHits.HitsStatisticsID
   WHERE Analytics_Statistics.StatisticsCode = ''pageviews''  
   AND HitsStartTime >= @FromDate AND HitsEndTime <= @ToDate   
   AND Analytics_Statistics.StatisticsObjectID IN
  (SELECT NodeID FROM View_CMS_Tree_Joined WHERE NodeAliasPath = Page AND NodeSiteID = @CMSContextCurrentSiteID AND (DocumentCulture = StatisticsObjectCulture OR StatisticsObjectCulture IS NULL))
     /*culture */
     AND (StatisticsObjectCulture IS NULL OR StatisticsObjectCulture = Culture OR Culture IS NULL)    
   )
  *100  ,0)
  
SELECT Name AS ''{$om.variant.tabletitle$}'',
   CAST (CAST (ISNULL (Hits,0) AS DECIMAL (15,1)) AS NVARCHAR(15)) + ''%'' AS ''{$om.selectedperiod$}''  
 FROM #AnalyticsTempTable ORDER BY Hits DESC

EXEC Proc_Analytics_RemoveTempTable

'
WHERE 
TableReportID IN (SELECT ReportID FROM Reporting_Report WHERE ReportName= 'abtestdetailconversionrate.hourreport')
AND TableName = 'table'


-- mvtest rate

UPDATE Reporting_ReportTable SET TableQuery=
'
EXEC Proc_Analytics_RemoveTempTable 
 CREATE TABLE #AnalyticsTempTable (  
  Hits DECIMAL (15,2),
  Name NVARCHAR(300) COLLATE database_default,  
  Culture NVARCHAR(300) COLLATE database_default,   
  Page NVARCHAR(300) COLLATE database_default,
  DisplayName NVARCHAR(300) COLLATE database_default     
);

SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
SET @ToDate = {%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''year'');

INSERT INTO #AnalyticsTempTable (DisplayName,Hits,Name,Page,Culture)
SELECT X.Name AS DisplayName, CAST (ISNULL(X.Hits,0) AS DECIMAL (15,2)) AS Hits, X.MVTCombinationName AS Name,X.MVTestPage AS Page,X.MVTestCulture AS Culture
FROM
(
SELECT
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END   
    AS Name,MVTCombinationName, ISNULL(SUM (HitsCount),0) AS Hits ,MVTestPage, MVTestCulture
 FROM Analytics_Statistics
 INNER JOIN OM_MVTCombination ON
  MVTCombinationPageTemplateID IN ( SELECT DocumentPageTemplateID FROM View_CMS_Tree_Joined WHERE NodeSiteID = @CMSContextCurrentSiteID
        AND NodeAliasPath IN(SELECT MVTestPage FROM OM_MVTest WHERE MVTestName = @MVTestName AND MVTestSiteID = @CMSContextCurrentSiteID)                        
        AND DocumentCulture = StatisticsObjectCulture)         
        AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
  LEFT JOIN Analytics_YearHits ON StatisticsID = HitsStatisticsID  
  LEFT JOIN OM_MVTest ON MVTestName = @MVTestName AND MVTestSiteID = @CMSContextCurrentSiteID

 WHERE   (StatisticsSiteID = @CMSContextCurrentSiteID ) AND StatisticsCode LIKE ''mvtconversion;%'' AND
 @MVTestName = SUBSTRING(StatisticsCode, 15, CHARINDEX('';'',StatisticsCode,15)-15) AND
 MVTCombinationName = (SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)))
 AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
 
 GROUP BY MVTCombinationName, MVTestPage, MVTestCulture,
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END
) AS X
 
  UPDATE #AnalyticsTempTable SET Hits = Hits/
     (
   SELECT CAST (SUM (HitsCount) AS DECIMAL (15,2)) FROM Analytics_Statistics
   JOIN Analytics_YearHits ON Analytics_Statistics.StatisticsID = Analytics_YearHits.HitsStatisticsID
   WHERE Analytics_Statistics.StatisticsCode = ''pageviews''  
   AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
   AND Analytics_Statistics.StatisticsObjectID IN
   (SELECT NodeID FROM View_CMS_Tree_Joined WHERE NodeAliasPath = Page AND NodeSiteID = @CMSContextCurrentSiteID)
    AND (StatisticsObjectCulture = Culture OR Culture IS NULL)    
   )
   *100    
   
   SELECT DisplayName AS ''{$mvtcombination.name$}'',
   CAST (CAST (ISNULL (Hits,0) AS DECIMAL (15,1)) AS NVARCHAR(15)) + ''%'' AS ''{$om.selectedperiod$}'' 
   FROM #AnalyticsTempTable
   ORDER BY Hits DESC
  
 EXEC Proc_Analytics_RemoveTempTable

'
WHERE 
TableReportID IN (SELECT ReportID FROM Reporting_Report WHERE ReportName= 'mvtestconversionrate.yearreport')
AND TableName = 'table'

UPDATE Reporting_ReportTable SET TableQuery=
'
 
EXEC Proc_Analytics_RemoveTempTable 
 CREATE TABLE #AnalyticsTempTable (  
  Hits DECIMAL (15,2),
  Name NVARCHAR(300) COLLATE database_default,  
  Culture NVARCHAR(300) COLLATE database_default,   
  Page NVARCHAR(300) COLLATE database_default,
  DisplayName NVARCHAR(300) COLLATE database_default     
);

SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
SET @ToDate = {%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''month'');

INSERT INTO #AnalyticsTempTable (DisplayName,Hits,Name,Page,Culture)
SELECT X.Name AS DisplayName, CAST (ISNULL(X.Hits,0) AS DECIMAL (15,2)) AS Hits, X.MVTCombinationName AS Name,X.MVTestPage AS Page,X.MVTestCulture AS Culture
FROM
(
SELECT
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END   
    AS Name,MVTCombinationName, ISNULL(SUM (HitsCount),0) AS Hits ,MVTestPage, MVTestCulture
 FROM Analytics_Statistics
 INNER JOIN OM_MVTCombination ON
  MVTCombinationPageTemplateID IN ( SELECT DocumentPageTemplateID FROM View_CMS_Tree_Joined WHERE NodeSiteID = @CMSContextCurrentSiteID
        AND NodeAliasPath IN(SELECT MVTestPage FROM OM_MVTest WHERE MVTestName = @MVTestName AND MVTestSiteID = @CMSContextCurrentSiteID)                        
        AND DocumentCulture = StatisticsObjectCulture)         
        AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
  LEFT JOIN Analytics_MonthHits ON StatisticsID = HitsStatisticsID  
  LEFT JOIN OM_MVTest ON MVTestName = @MVTestName AND MVTestSiteID = @CMSContextCurrentSiteID

 WHERE   (StatisticsSiteID = @CMSContextCurrentSiteID ) AND StatisticsCode LIKE ''mvtconversion;%'' AND
 @MVTestName = SUBSTRING(StatisticsCode, 15, CHARINDEX('';'',StatisticsCode,15)-15) AND
 MVTCombinationName = (SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)))
 AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
 
 GROUP BY MVTCombinationName, MVTestPage, MVTestCulture,
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END
) AS X
 
  UPDATE #AnalyticsTempTable SET Hits = Hits/
     (
   SELECT CAST (SUM (HitsCount) AS DECIMAL (15,2)) FROM Analytics_Statistics
   JOIN Analytics_MonthHits ON Analytics_Statistics.StatisticsID = Analytics_MonthHits.HitsStatisticsID
   WHERE Analytics_Statistics.StatisticsCode = ''pageviews''  
   AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
   AND Analytics_Statistics.StatisticsObjectID IN
   (SELECT NodeID FROM View_CMS_Tree_Joined WHERE NodeAliasPath = Page AND NodeSiteID = @CMSContextCurrentSiteID)
    AND (StatisticsObjectCulture = Culture OR Culture IS NULL)    
   )
   *100    
   
   SELECT DisplayName AS ''{$mvtcombination.name$}'',
   CAST (CAST (ISNULL (Hits,0) AS DECIMAL (15,1)) AS NVARCHAR(15)) + ''%'' AS ''{$om.selectedperiod$}'' 
   FROM #AnalyticsTempTable
   ORDER BY Hits DESC
  
 EXEC Proc_Analytics_RemoveTempTable

'
WHERE 
TableReportID IN (SELECT ReportID FROM Reporting_Report WHERE ReportName= 'mvtestconversionrate.monthreport')
AND TableName = 'table'

UPDATE Reporting_ReportTable SET TableQuery=
'
 EXEC Proc_Analytics_RemoveTempTable 
 CREATE TABLE #AnalyticsTempTable (  
  Hits DECIMAL (15,2),
  Name NVARCHAR(300) COLLATE database_default,  
  Culture NVARCHAR(300) COLLATE database_default,   
  Page NVARCHAR(300) COLLATE database_default,
  DisplayName NVARCHAR(300) COLLATE database_default     
);

SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week'');
SET @ToDate = {%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''week'');

INSERT INTO #AnalyticsTempTable (DisplayName,Hits,Name,Page,Culture)
SELECT X.Name AS DisplayName, CAST (ISNULL(X.Hits,0) AS DECIMAL (15,2)) AS Hits, X.MVTCombinationName AS Name,X.MVTestPage AS Page,X.MVTestCulture AS Culture
FROM
(
SELECT
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END   
    AS Name,MVTCombinationName, ISNULL(SUM (HitsCount),0) AS Hits ,MVTestPage, MVTestCulture
 FROM Analytics_Statistics
 INNER JOIN OM_MVTCombination ON
  MVTCombinationPageTemplateID IN ( SELECT DocumentPageTemplateID FROM View_CMS_Tree_Joined WHERE NodeSiteID = @CMSContextCurrentSiteID
        AND NodeAliasPath IN(SELECT MVTestPage FROM OM_MVTest WHERE MVTestName = @MVTestName AND MVTestSiteID = @CMSContextCurrentSiteID)                        
        AND DocumentCulture = StatisticsObjectCulture)         
        AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
  LEFT JOIN Analytics_WeekHits ON StatisticsID = HitsStatisticsID  
  LEFT JOIN OM_MVTest ON MVTestName = @MVTestName AND MVTestSiteID = @CMSContextCurrentSiteID

 WHERE   (StatisticsSiteID = @CMSContextCurrentSiteID ) AND StatisticsCode LIKE ''mvtconversion;%'' AND
 @MVTestName = SUBSTRING(StatisticsCode, 15, CHARINDEX('';'',StatisticsCode,15)-15) AND
 MVTCombinationName = (SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)))
 AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
 
 GROUP BY MVTCombinationName, MVTestPage, MVTestCulture,
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END
) AS X
 
  UPDATE #AnalyticsTempTable SET Hits = Hits/
     (
   SELECT CAST (SUM (HitsCount) AS DECIMAL (15,2)) FROM Analytics_Statistics
   JOIN Analytics_WeekHits ON Analytics_Statistics.StatisticsID = Analytics_WeekHits.HitsStatisticsID
   WHERE Analytics_Statistics.StatisticsCode = ''pageviews''  
   AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
   AND Analytics_Statistics.StatisticsObjectID IN
   (SELECT NodeID FROM View_CMS_Tree_Joined WHERE NodeAliasPath = Page AND NodeSiteID = @CMSContextCurrentSiteID)
    AND (StatisticsObjectCulture = Culture OR Culture IS NULL)    
   )
   *100    
   
   SELECT DisplayName AS ''{$mvtcombination.name$}'',
   CAST (CAST (ISNULL (Hits,0) AS DECIMAL (15,1)) AS NVARCHAR(15)) + ''%'' AS ''{$om.selectedperiod$}'' 
   FROM #AnalyticsTempTable
   ORDER BY Hits DESC
  
 EXEC Proc_Analytics_RemoveTempTable

'
WHERE 
TableReportID IN (SELECT ReportID FROM Reporting_Report WHERE ReportName= 'mvtestconversionrate.weekreport')
AND TableName = 'table'

UPDATE Reporting_ReportTable SET TableQuery=
'
 EXEC Proc_Analytics_RemoveTempTable 
 CREATE TABLE #AnalyticsTempTable (  
  Hits DECIMAL (15,2),
  Name NVARCHAR(300) COLLATE database_default,  
  Culture NVARCHAR(300) COLLATE database_default,   
  Page NVARCHAR(300) COLLATE database_default,
  DisplayName NVARCHAR(300) COLLATE database_default     
);

SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
SET @ToDate = {%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''day'');

INSERT INTO #AnalyticsTempTable (DisplayName,Hits,Name,Page,Culture)
SELECT X.Name AS DisplayName, CAST (ISNULL(X.Hits,0) AS DECIMAL (15,2)) AS Hits, X.MVTCombinationName AS Name,X.MVTestPage AS Page,X.MVTestCulture AS Culture
FROM
(
SELECT
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END   
    AS Name,MVTCombinationName, ISNULL(SUM (HitsCount),0) AS Hits ,MVTestPage, MVTestCulture
 FROM Analytics_Statistics
 INNER JOIN OM_MVTCombination ON
  MVTCombinationPageTemplateID IN ( SELECT DocumentPageTemplateID FROM View_CMS_Tree_Joined WHERE NodeSiteID = @CMSContextCurrentSiteID
        AND NodeAliasPath IN(SELECT MVTestPage FROM OM_MVTest WHERE MVTestName = @MVTestName AND MVTestSiteID = @CMSContextCurrentSiteID)                        
        AND DocumentCulture = StatisticsObjectCulture)         
        AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
  LEFT JOIN Analytics_DayHits ON StatisticsID = HitsStatisticsID  
  LEFT JOIN OM_MVTest ON MVTestName = @MVTestName AND MVTestSiteID = @CMSContextCurrentSiteID

 WHERE   (StatisticsSiteID = @CMSContextCurrentSiteID ) AND StatisticsCode LIKE ''mvtconversion;%'' AND
 @MVTestName = SUBSTRING(StatisticsCode, 15, CHARINDEX('';'',StatisticsCode,15)-15) AND
 MVTCombinationName = (SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)))
 AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
 
 GROUP BY MVTCombinationName, MVTestPage, MVTestCulture,
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END
) AS X
 
  UPDATE #AnalyticsTempTable SET Hits = Hits/
     (
   SELECT CAST (SUM (HitsCount) AS DECIMAL (15,2)) FROM Analytics_Statistics
   JOIN Analytics_DayHits ON Analytics_Statistics.StatisticsID = Analytics_DayHits.HitsStatisticsID
   WHERE Analytics_Statistics.StatisticsCode = ''pageviews''  
   AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
   AND Analytics_Statistics.StatisticsObjectID IN
   (SELECT NodeID FROM View_CMS_Tree_Joined WHERE NodeAliasPath = Page AND NodeSiteID = @CMSContextCurrentSiteID)
    AND (StatisticsObjectCulture = Culture OR Culture IS NULL)    
   )
   *100    
   
   SELECT DisplayName AS ''{$mvtcombination.name$}'',
   CAST (CAST (ISNULL (Hits,0) AS DECIMAL (15,1)) AS NVARCHAR(15)) + ''%'' AS ''{$om.selectedperiod$}'' 
   FROM #AnalyticsTempTable
   ORDER BY Hits DESC
  
 EXEC Proc_Analytics_RemoveTempTable

'
WHERE 
TableReportID IN (SELECT ReportID FROM Reporting_Report WHERE ReportName= 'mvtestconversionrate.dayreport')
AND TableName = 'table'

UPDATE Reporting_ReportTable SET TableQuery=
'
 EXEC Proc_Analytics_RemoveTempTable 
 CREATE TABLE #AnalyticsTempTable (  
  Hits DECIMAL (15,2),
  Name NVARCHAR(300) COLLATE database_default,  
  Culture NVARCHAR(300) COLLATE database_default,   
  Page NVARCHAR(300) COLLATE database_default,
  DisplayName NVARCHAR(300) COLLATE database_default     
);

SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
SET @ToDate = {%DatabaseSchema%}.Func_Analytics_EndDateTrim(@ToDate,''hour'');

INSERT INTO #AnalyticsTempTable (DisplayName,Hits,Name,Page,Culture)
SELECT X.Name AS DisplayName, CAST (ISNULL(X.Hits,0) AS DECIMAL (15,2)) AS Hits, X.MVTCombinationName AS Name,X.MVTestPage AS Page,X.MVTestCulture AS Culture
FROM
(
SELECT
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END   
    AS Name,MVTCombinationName, ISNULL(SUM (HitsCount),0) AS Hits ,MVTestPage, MVTestCulture
 FROM Analytics_Statistics
 INNER JOIN OM_MVTCombination ON
  MVTCombinationPageTemplateID IN ( SELECT DocumentPageTemplateID FROM View_CMS_Tree_Joined WHERE NodeSiteID = @CMSContextCurrentSiteID
        AND NodeAliasPath IN(SELECT MVTestPage FROM OM_MVTest WHERE MVTestName = @MVTestName AND MVTestSiteID = @CMSContextCurrentSiteID)                        
        AND DocumentCulture = StatisticsObjectCulture)         
        AND ISNULL(@ConversionName,'''') IN ('''',StatisticsObjectName)
  LEFT JOIN Analytics_HourHits ON StatisticsID = HitsStatisticsID  
  LEFT JOIN OM_MVTest ON MVTestName = @MVTestName AND MVTestSiteID = @CMSContextCurrentSiteID

 WHERE   (StatisticsSiteID = @CMSContextCurrentSiteID ) AND StatisticsCode LIKE ''mvtconversion;%'' AND
 @MVTestName = SUBSTRING(StatisticsCode, 15, CHARINDEX('';'',StatisticsCode,15)-15) AND
 MVTCombinationName = (SUBSTRING(StatisticsCode,LEN (''mvtconversion;''+@MVTestName+'';'')+1,LEN (StatisticsCode)))
 AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
 
 GROUP BY MVTCombinationName, MVTestPage, MVTestCulture,
   CASE
      WHEN MVTCombinationCustomName  IS NULL OR MVTCombinationCustomName='''' THEN MVTCombinationName
      ELSE MVTCombinationCustomName
    END
) AS X
 
  UPDATE #AnalyticsTempTable SET Hits = Hits/
     (
   SELECT CAST (SUM (HitsCount) AS DECIMAL (15,2)) FROM Analytics_Statistics
   JOIN Analytics_HourHits ON Analytics_Statistics.StatisticsID = Analytics_HourHits.HitsStatisticsID
   WHERE Analytics_Statistics.StatisticsCode = ''pageviews''  
   AND (HitsStartTime >= @FromDate) AND (HitsEndTime <= @ToDate)
   AND Analytics_Statistics.StatisticsObjectID IN
   (SELECT NodeID FROM View_CMS_Tree_Joined WHERE NodeAliasPath = Page AND NodeSiteID = @CMSContextCurrentSiteID)
    AND (StatisticsObjectCulture = Culture OR Culture IS NULL)    
   )
   *100    
   
   SELECT DisplayName AS ''{$mvtcombination.name$}'',
   CAST (CAST (ISNULL (Hits,0) AS DECIMAL (15,1)) AS NVARCHAR(15)) + ''%'' AS ''{$om.selectedperiod$}'' 
   FROM #AnalyticsTempTable
   ORDER BY Hits DESC
  
 EXEC Proc_Analytics_RemoveTempTable


'
WHERE 
TableReportID IN (SELECT ReportID FROM Reporting_Report WHERE ReportName= 'mvtestconversionrate.hourreport')
AND TableName = 'table'

END

GO

/******************************* END UPDATE MVT,AB TABLE QUERIES********************************/
/********************* FIX ALL TRAFFIC GRAPH FOR ASURE ***********************************/
DECLARE @HOTFIXVERSION INT;
SET @HOTFIXVERSION =  (SELECT KeyValue FROM [CMS_SettingsKey] WHERE KeyName = N'CMSHotfixVersion')
IF @HOTFIXVERSION  < 22
BEGIN
UPDATE Reporting_ReportGraph SET GraphQuery =
'
  EXEC Proc_Analytics_RemoveTempTable
  CREATE TABLE #AnalyticsTempTable (
    StartTime DATETIME,
    Hits INT,
    Name NVARCHAR(300) COLLATE database_default 
  );
  SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''year'');
 
INSERT INTO #AnalyticsTempTable (StartTime, Hits, Name)
 SELECT [Date] AS StartTime ,T1.Hits,''{''+''$analytics_codename.'' + StatisticsCode + ''$}'' AS Name FROM
  {%DatabaseSchema%}.Func_Analytics_EnsureDates (@FromDate,@ToDate,''year'') AS Dates   
  LEFT JOIN
  (SELECT HitsStartTime AS StartTime,SUM(HitsCount) AS hits,StatisticsCode
  FROM Analytics_Statistics
  INNER JOIN Analytics_YearHits ON Analytics_YearHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND((StatisticsCode = @Direct) OR (StatisticsCode = @Reffering)  OR (StatisticsCode = @Search))  
  GROUP BY HitsStartTime,StatisticsCode) AS T1
  ON T1.StartTime = [Date]
  
   EXEC Proc_Analytics_Pivot ''year''
'
WHERE GraphName ='graph' AND GraphReportID IN (SELECT ReportID FROM Reporting_Report WHERE ReportName='alltrafficsources.yearreport')

UPDATE Reporting_ReportGraph SET GraphQuery =
'
  EXEC Proc_Analytics_RemoveTempTable
  CREATE TABLE #AnalyticsTempTable (
    StartTime DATETIME,
    Hits INT,
    Name NVARCHAR(300) COLLATE database_default 
  );
  SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''month'');
 
INSERT INTO #AnalyticsTempTable (StartTime, Hits, Name)
 SELECT [Date] AS StartTime ,T1.Hits,''{''+''$analytics_codename.'' + StatisticsCode + ''$}'' AS Name FROM
  {%DatabaseSchema%}.Func_Analytics_EnsureDates (@FromDate,@ToDate,''month'') AS Dates   
  LEFT JOIN
  (SELECT HitsStartTime AS StartTime,SUM(HitsCount) AS hits,StatisticsCode
  FROM Analytics_Statistics
  INNER JOIN Analytics_MonthHits ON Analytics_MonthHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND((StatisticsCode = @Direct) OR (StatisticsCode = @Reffering)  OR (StatisticsCode = @Search))  
  GROUP BY HitsStartTime,StatisticsCode) AS T1
  ON T1.StartTime = [Date]
  
   EXEC Proc_Analytics_Pivot ''month''
'
WHERE GraphName ='graph' AND GraphReportID IN (SELECT ReportID FROM Reporting_Report WHERE ReportName='alltrafficsources.monthreport')

UPDATE Reporting_ReportGraph SET GraphQuery =
'
  EXEC Proc_Analytics_RemoveTempTable
  CREATE TABLE #AnalyticsTempTable (
    StartTime DATETIME,
    Hits INT,
    Name NVARCHAR(300) COLLATE database_default 
  );
  SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''day'');
 
INSERT INTO #AnalyticsTempTable (StartTime, Hits, Name)
 SELECT [Date] AS StartTime ,T1.Hits,''{''+''$analytics_codename.'' + StatisticsCode + ''$}'' AS Name FROM
  {%DatabaseSchema%}.Func_Analytics_EnsureDates (@FromDate,@ToDate,''day'') AS Dates   
  LEFT JOIN
  (SELECT HitsStartTime AS StartTime,SUM(HitsCount) AS hits,StatisticsCode
  FROM Analytics_Statistics
  INNER JOIN Analytics_DayHits ON Analytics_DayHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND((StatisticsCode = @Direct) OR (StatisticsCode = @Reffering)  OR (StatisticsCode = @Search))  
  GROUP BY HitsStartTime,StatisticsCode) AS T1
  ON T1.StartTime = [Date]
  
   EXEC Proc_Analytics_Pivot ''day''
'
WHERE GraphName ='graph' AND GraphReportID IN (SELECT ReportID FROM Reporting_Report WHERE ReportName='alltrafficsources.dayreport')

UPDATE Reporting_ReportGraph SET GraphQuery =
'
  EXEC Proc_Analytics_RemoveTempTable
  CREATE TABLE #AnalyticsTempTable (
    StartTime DATETIME,
    Hits INT,
    Name NVARCHAR(300) COLLATE database_default 
  );
  SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''week'');
 
INSERT INTO #AnalyticsTempTable (StartTime, Hits, Name)
 SELECT [Date] AS StartTime ,T1.Hits,''{''+''$analytics_codename.'' + StatisticsCode + ''$}'' AS Name FROM
  {%DatabaseSchema%}.Func_Analytics_EnsureDates (@FromDate,@ToDate,''week'') AS Dates   
  LEFT JOIN
  (SELECT HitsStartTime AS StartTime,SUM(HitsCount) AS hits,StatisticsCode
  FROM Analytics_Statistics
  INNER JOIN Analytics_WeekHits ON Analytics_WeekHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND((StatisticsCode = @Direct) OR (StatisticsCode = @Reffering)  OR (StatisticsCode = @Search))  
  GROUP BY HitsStartTime,StatisticsCode) AS T1
  ON T1.StartTime = [Date]
  
   EXEC Proc_Analytics_Pivot ''week''
'
WHERE GraphName ='graph' AND GraphReportID IN (SELECT ReportID FROM Reporting_Report WHERE ReportName='alltrafficsources.weekreport')

UPDATE Reporting_ReportGraph SET GraphQuery =
'
  EXEC Proc_Analytics_RemoveTempTable
  CREATE TABLE #AnalyticsTempTable (
    StartTime DATETIME,
    Hits INT,
    Name NVARCHAR(300) COLLATE database_default 
  );
  SET @FromDate ={%DatabaseSchema%}.Func_Analytics_DateTrim(@FromDate,''hour'');
 
INSERT INTO #AnalyticsTempTable (StartTime, Hits, Name)
 SELECT [Date] AS StartTime ,T1.Hits,''{''+''$analytics_codename.'' + StatisticsCode + ''$}'' AS Name FROM
  {%DatabaseSchema%}.Func_Analytics_EnsureDates (@FromDate,@ToDate,''hour'') AS Dates   
  LEFT JOIN
  (SELECT HitsStartTime AS StartTime,SUM(HitsCount) AS hits,StatisticsCode
  FROM Analytics_Statistics
  INNER JOIN Analytics_HourHits ON Analytics_HourHits.HitsStatisticsID = Analytics_Statistics.StatisticsID
  WHERE (StatisticsSiteID = @CMSContextCurrentSiteID)
  AND((StatisticsCode = @Direct) OR (StatisticsCode = @Reffering)  OR (StatisticsCode = @Search))  
  GROUP BY HitsStartTime,StatisticsCode) AS T1
  ON T1.StartTime = [Date]
  
   EXEC Proc_Analytics_Pivot ''hour''
'
WHERE GraphName ='graph' AND GraphReportID IN (SELECT ReportID FROM Reporting_Report WHERE ReportName='alltrafficsources.hourreport')
END
GO

/**************************** END ALL TRAFFIC FOR ASURE *****************************/




/***************************INSERT INDEX TO USERRROLE *************************************/

DECLARE @HOTFIXVERSION INT;
SET @HOTFIXVERSION =  (SELECT KeyValue FROM [CMS_SettingsKey] WHERE KeyName = N'CMSHotfixVersion')
IF @HOTFIXVERSION < 19
BEGIN
  IF NOT EXISTS
    (SELECT * FROM SYS.INDEXES AS i JOIN SYS.TABLES AS t ON t.object_id = i.object_id
	 	WHERE i.name = N'IX_CMS_UserRole_UserID' AND t.name = N'CMS_UserRole')
  BEGIN 
     CREATE NONCLUSTERED INDEX IX_CMS_UserRole_UserID
       	ON [CMS_UserRole] ([UserID])
	INCLUDE ([RoleID],[ValidTo])
  END
END
GO
/***************************** END INSERT INDEX TO USERROLE *******************************/


/********************* ECOMMERCE - CUSTOMER CHECK DEPENDENCIES UPDATE *********************/
GO
UPDATE CMS_Query SET QueryText = N'SELECT TOP 1 OrderCustomerID FROM [COM_Order] WHERE OrderCustomerID=@ID' WHERE ClassID = (SELECT TOP 1 ClassID FROM CMS_Class WHERE ClassName = 'ecommerce.customer') AND QueryName = 'checkdependencies'
GO
/********************* END ECOMMERCE - CUSTOMER CHECK DEPENDENCIES UPDATE *********************/


/********************* SMART SEARCH - SEARCH ATTACHMENT QUERY *********************/
GO

DECLARE @HOTFIXVERSION INT;
SET @HOTFIXVERSION =  (SELECT KeyValue FROM [CMS_SettingsKey] WHERE KeyName = N'CMSHotfixVersion')
IF @HOTFIXVERSION  < 18
BEGIN

UPDATE CMS_Query SET QueryText = N'SELECT ##TOPN## SUM(RANK) AS [Score], DocumentID, SiteName, NodeID, NodeAliasPath, DocumentCulture, NodeClassID, ClassName, NodeACLID, NodeSiteID, NodeLinkedNodeID, NodeOwner
FROM CMS_Attachment INNER JOIN
CONTAINSTABLE(CMS_Attachment, AttachmentBinary, ##SEARCH## ) AS KEY_TBL ON CMS_Attachment.AttachmentID = KEY_TBL.[KEY] INNER JOIN
View_CMS_Tree_Joined ON View_CMS_Tree_Joined.DocumentID = CMS_Attachment.AttachmentDocumentID
WHERE (Published = 1) AND (##WHERE##) 
GROUP BY DocumentID, SiteName, NodeID, NodeAliasPath, DocumentCulture, NodeClassID, ClassName, NodeACLID, NodeSiteID, NodeLinkedNodeID, NodeOwner ORDER BY ##ORDERBY##' 
WHERE ClassID = (SELECT TOP 1 ClassID FROM CMS_Class WHERE ClassName = N'cms.root') AND QueryName = N'smartsearchattachments'

END

GO
/********************* END SMART SEARCH - SEARCH ATTACHMENT QUERY *********************/


/********************* UPDATE CMS.WEBFARMSERVER OBJECT *********************/

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'ServerLastUpdated' AND Object_ID = Object_ID(N'CMS_WebFarmServer'))
BEGIN 
	ALTER TABLE [CMS_WebFarmServer]
	ADD [ServerLastUpdated] [datetime] NULL	
END

UPDATE CMS_Class SET
ClassXmlSchema = '<?xml version="1.0" encoding="utf-8"?>  <xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">    <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">      <xs:complexType>        <xs:choice minOccurs="0" maxOccurs="unbounded">          <xs:element name="CMS_WebFarmServer">            <xs:complexType>              <xs:sequence>                <xs:element name="ServerID" msdata:ReadOnly="true" msdata:AutoIncrement="true" type="xs:int" />                <xs:element name="ServerDisplayName">                  <xs:simpleType>                    <xs:restriction base="xs:string">                      <xs:maxLength value="200" />                    </xs:restriction>                  </xs:simpleType>                </xs:element>                <xs:element name="ServerName">                  <xs:simpleType>                    <xs:restriction base="xs:string">                      <xs:maxLength value="200" />                    </xs:restriction>                  </xs:simpleType>                </xs:element>                <xs:element name="ServerURL">                  <xs:simpleType>                    <xs:restriction base="xs:string">                      <xs:maxLength value="2000" />                    </xs:restriction>                  </xs:simpleType>                </xs:element>                <xs:element name="ServerGUID" msdata:DataType="System.Guid, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" type="xs:string" />                <xs:element name="ServerLastModified" type="xs:dateTime" />                <xs:element name="ServerEnabled" type="xs:boolean" />                <xs:element name="ServerLastUpdated" type="xs:dateTime" minOccurs="0" />              </xs:sequence>            </xs:complexType>          </xs:element>        </xs:choice>      </xs:complexType>      <xs:unique name="Constraint1" msdata:PrimaryKey="true">        <xs:selector xpath=".//CMS_WebFarmServer" />        <xs:field xpath="ServerID" />      </xs:unique>    </xs:element>  </xs:schema>',
ClassFormDefinition = '<form><field column="ServerID" fieldcaption="ServerID" visible="true" columntype="integer" fieldtype="CustomUserControl" allowempty="false" isPK="true" system="true" publicfield="false" spellcheck="true" guid="aa45d15a-369b-4d52-b349-50a6dd88bf99" ismacro="false" hasdependingfields="false" dependsonanotherfield="false" inheritable="false" translatefield="false"><settings><controlname>labelcontrol</controlname></settings></field><field column="ServerDisplayName" fieldcaption="ServerDisplayName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="0faeba3a-902c-498b-86f1-adc10aecc480" ismacro="false" hasdependingfields="false" dependsonanotherfield="false" inheritable="false" translatefield="true"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ServerName" fieldcaption="ServerName" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="2b918771-1f4f-4b7e-8079-a9bc77580211" ismacro="false" hasdependingfields="false" dependsonanotherfield="false" inheritable="false" translatefield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ServerURL" fieldcaption="ServerURL" visible="true" columntype="text" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="46b24382-3b88-46b8-b1f8-9a04ddbf09d3" ismacro="false" hasdependingfields="false" dependsonanotherfield="false" inheritable="false" translatefield="false"><settings><controlname>textboxcontrol</controlname></settings></field><field column="ServerGUID" fieldcaption="ServerGUID" visible="true" columntype="file" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="4a6c0fb1-566d-4a9f-b780-a6777aab8ac5" ismacro="false" hasdependingfields="false" dependsonanotherfield="false" inheritable="false" translatefield="false"><settings><controlname>unknown</controlname></settings></field><field column="ServerLastModified" fieldcaption="ServerLastModified" visible="true" columntype="datetime" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="08311544-ceb1-4df6-b40a-c7c7eb4b8048" ismacro="false" hasdependingfields="false" dependsonanotherfield="false" inheritable="false" translatefield="false"><settings><controlname>calendarcontrol</controlname></settings></field><field column="ServerEnabled" fieldcaption="ServerEnabled" visible="true" columntype="boolean" fieldtype="CustomUserControl" allowempty="false" isPK="false" system="true" publicfield="false" spellcheck="true" guid="a7318ab5-b31f-4c39-a7e5-6f3fe9dff9c1" ismacro="false" hasdependingfields="false" dependsonanotherfield="false" inheritable="false" translatefield="false"><settings><controlname>checkboxcontrol</controlname></settings></field><field column="ServerLastUpdated" visible="false" columntype="datetime" fieldtype="CustomUserControl" allowempty="true" isPK="false" system="true" publicfield="false" spellcheck="true" guid="bb3c64d7-9e54-42b6-84ad-23d64182f136" visibility="none" ismacro="false" hasdependingfields="false" dependsonanotherfield="false" inheritable="false" translatefield="false"><settings><controlname>dropdownlistcontrol</controlname></settings></field></form>'
WHERE ClassName = 'cms.WebFarmServer'

UPDATE CMS_Query SET
QueryText = 'INSERT INTO CMS_WebFarmServer ([ServerDisplayName], [ServerName], [ServerURL], [ServerGUID], [ServerLastModified], [ServerEnabled], [ServerLastUpdated] ) VALUES ( @ServerDisplayName, @ServerName, @ServerURL, @ServerGUID, @ServerLastModified, @ServerEnabled, @ServerLastUpdated); SELECT SCOPE_IDENTITY() AS [ServerID]'
WHERE QueryName = 'insert' AND ClassID IN (SELECT ClassID FROM CMS_Class WHERE ClassName = 'cms.WebFarmServer')

UPDATE CMS_Query SET
QueryText = 'UPDATE CMS_WebFarmServer SET [ServerDisplayName] = @ServerDisplayName, [ServerName] = @ServerName, [ServerURL] = @ServerURL, [ServerGUID] = @ServerGUID, [ServerLastModified] = @ServerLastModified, [ServerEnabled] = @ServerEnabled, [ServerLastUpdated] = @ServerLastUpdated WHERE [ServerID] = @ServerID'
WHERE QueryName = 'update' AND ClassID IN (SELECT ClassID FROM CMS_Class WHERE ClassName = 'cms.WebFarmServer')

DECLARE @UpdateAllServerID INT;
DECLARE @WebFarmServerClassID INT;
SET @WebFarmServerClassID = (SELECT ClassID FROM CMS_Class WHERE ClassName = 'cms.WebFarmServer');
SET @UpdateAllServerID = (SELECT QueryID FROM CMS_Query WHERE ClassID = @WebFarmServerClassID AND QueryName = 'updateall');

IF (@UpdateAllServerID IS NULL)
BEGIN
INSERT INTO CMS_Query (QueryName,QueryTypeID,QueryText,QueryRequiresTransaction,ClassID,QueryIsLocked,QueryLastModified,QueryGUID,
QueryLoadGeneration,QueryIsCustom) VALUES 
('updateall',0, 'UPDATE CMS_WebFarmServer SET ##COLUMNS## WHERE ##WHERE##',
0,@WebFarmServerClassID,0,getdate(),newid(),0,NULL);
END
 
GO
/********************* END - UPDATE CMS.WEBFARMSERVER OBJECT *********************/


/* ----------------------------------------------------------------------------*/
/* This SQL command must be at the end and must contain current hotfix version */
/* ----------------------------------------------------------------------------*/
UPDATE [CMS_SettingsKey] SET KeyValue = '23' WHERE KeyName = N'CMSHotfixVersion'
GO
