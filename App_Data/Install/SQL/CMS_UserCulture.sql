CREATE TABLE [CMS_UserCulture] (
		[UserID]        [int] NOT NULL,
		[CultureID]     [int] NOT NULL,
		[SiteID]        [int] NOT NULL
) 
ALTER TABLE [CMS_UserCulture]
	ADD
	CONSTRAINT [PK_CMS_UserCulture]
	PRIMARY KEY
	CLUSTERED
	([UserID], [CultureID], [SiteID])
	
	
ALTER TABLE [CMS_UserCulture]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_UserCulture_CultureID_CMS_Culture]
	FOREIGN KEY ([CultureID]) REFERENCES [CMS_Culture] ([CultureID])
ALTER TABLE [CMS_UserCulture]
	CHECK CONSTRAINT [FK_CMS_UserCulture_CultureID_CMS_Culture]
ALTER TABLE [CMS_UserCulture]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_UserCulture_SiteID_CMS_Site]
	FOREIGN KEY ([SiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [CMS_UserCulture]
	CHECK CONSTRAINT [FK_CMS_UserCulture_SiteID_CMS_Site]
ALTER TABLE [CMS_UserCulture]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_UserCulture_UserID_CMS_User]
	FOREIGN KEY ([UserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [CMS_UserCulture]
	CHECK CONSTRAINT [FK_CMS_UserCulture_UserID_CMS_User]
