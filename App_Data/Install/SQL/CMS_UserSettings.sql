CREATE TABLE [CMS_UserSettings] (
		[UserSettingsID]                     [int] IDENTITY(1, 1) NOT NULL,
		[UserNickName]                       [nvarchar](200) NULL,
		[UserPicture]                        [nvarchar](200) NULL,
		[UserSignature]                      [nvarchar](max) NULL,
		[UserURLReferrer]                    [nvarchar](450) NULL,
		[UserCampaign]                       [nvarchar](200) NULL,
		[UserMessagingNotificationEmail]     [nvarchar](200) NULL,
		[UserCustomData]                     [nvarchar](max) NULL,
		[UserRegistrationInfo]               [nvarchar](max) NULL,
		[UserPreferences]                    [nvarchar](max) NULL,
		[UserActivationDate]                 [datetime] NULL,
		[UserActivatedByUserID]              [int] NULL,
		[UserTimeZoneID]                     [int] NULL,
		[UserAvatarID]                       [int] NULL,
		[UserBadgeID]                        [int] NULL,
		[UserShowSplashScreen]               [bit] NULL,
		[UserActivityPoints]                 [int] NULL,
		[UserForumPosts]                     [int] NULL,
		[UserBlogComments]                   [int] NULL,
		[UserGender]                         [int] NULL,
		[UserDateOfBirth]                    [datetime] NULL,
		[UserMessageBoardPosts]              [int] NULL,
		[UserSettingsUserGUID]               [uniqueidentifier] NOT NULL,
		[UserSettingsUserID]                 [int] NOT NULL,
		[WindowsLiveID]                      [nvarchar](50) NULL,
		[UserBlogPosts]                      [int] NULL,
		[UserWaitingForApproval]             [bit] NULL,
		[UserDialogsConfiguration]           [nvarchar](max) NULL,
		[UserDescription]                    [nvarchar](max) NULL,
		[UserUsedWebParts]                   [nvarchar](1000) NULL,
		[UserUsedWidgets]                    [nvarchar](1000) NULL,
		[UserFacebookID]                     [nvarchar](100) NULL,
		[UserAuthenticationGUID]             [uniqueidentifier] NULL,
		[UserSkype]                          [nvarchar](100) NULL,
		[UserIM]                             [nvarchar](100) NULL,
		[UserPhone]                          [nvarchar](26) NULL,
		[UserPosition]                       [nvarchar](200) NULL,
		[UserBounces]                        [int] NULL,
		[UserLinkedInID]                     [nvarchar](100) NULL,
		[UserLogActivities]                  [bit] NULL,
		[UserPasswordRequestHash]            [nvarchar](100) NULL
)  
ALTER TABLE [CMS_UserSettings]
	ADD
	CONSTRAINT [PK_CMS_UserSettings]
	PRIMARY KEY
	NONCLUSTERED
	([UserSettingsID])
	
	
ALTER TABLE [CMS_UserSettings]
	ADD
	CONSTRAINT [DEFAULT_CMS_UserSettings_UserSettingsUserGUID]
	DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [UserSettingsUserGUID]
ALTER TABLE [CMS_UserSettings]
	ADD
	CONSTRAINT [DEFAULT_CMS_UserSettings_UserSettingsUserID]
	DEFAULT ((0)) FOR [UserSettingsUserID]
ALTER TABLE [CMS_UserSettings]
	ADD
	CONSTRAINT [DEFAULT_CMS_UserSettings_UserShowSplashScreen]
	DEFAULT ((0)) FOR [UserShowSplashScreen]
ALTER TABLE [CMS_UserSettings]
	ADD
	CONSTRAINT [DEFAULT_CMS_UserSettings_UserWaitingForApproval]
	DEFAULT ((0)) FOR [UserWaitingForApproval]
CREATE NONCLUSTERED INDEX [IX_CMS_UserSettings_UserActivatedByUserID]
	ON [CMS_UserSettings] ([UserActivatedByUserID])
	
CREATE NONCLUSTERED INDEX [IX_CMS_UserSettings_UserAvatarID]
	ON [CMS_UserSettings] ([UserAvatarID])
	
CREATE NONCLUSTERED INDEX [IX_CMS_UserSettings_UserBadgeID]
	ON [CMS_UserSettings] ([UserBadgeID])
	
CREATE NONCLUSTERED INDEX [IX_CMS_UserSettings_UserFacebookID]
	ON [CMS_UserSettings] ([UserFacebookID])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_UserSettings_UserGender]
	ON [CMS_UserSettings] ([UserGender])
	
	
CREATE CLUSTERED INDEX [IX_CMS_UserSettings_UserNickName]
	ON [CMS_UserSettings] ([UserNickName])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_UserSettings_UserSettingsUserGUID]
	ON [CMS_UserSettings] ([UserSettingsUserGUID])
	
CREATE UNIQUE NONCLUSTERED INDEX [IX_CMS_UserSettings_UserSettingsUserID]
	ON [CMS_UserSettings] ([UserSettingsUserID])
	
CREATE NONCLUSTERED INDEX [IX_CMS_UserSettings_UserTimeZoneID]
	ON [CMS_UserSettings] ([UserTimeZoneID])
	
CREATE NONCLUSTERED INDEX [IX_CMS_UserSettings_UserWaitingForApproval]
	ON [CMS_UserSettings] ([UserWaitingForApproval])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_UserSettings_WindowsLiveID]
	ON [CMS_UserSettings] ([WindowsLiveID])
	
	
ALTER TABLE [CMS_UserSettings]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_UserSettings_UserActivatedByUserID_CMS_User]
	FOREIGN KEY ([UserActivatedByUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [CMS_UserSettings]
	CHECK CONSTRAINT [FK_CMS_UserSettings_UserActivatedByUserID_CMS_User]
ALTER TABLE [CMS_UserSettings]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_UserSettings_UserAvatarID_CMS_Avatar]
	FOREIGN KEY ([UserAvatarID]) REFERENCES [CMS_Avatar] ([AvatarID])
ALTER TABLE [CMS_UserSettings]
	CHECK CONSTRAINT [FK_CMS_UserSettings_UserAvatarID_CMS_Avatar]
ALTER TABLE [CMS_UserSettings]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_UserSettings_UserBadgeID_CMS_Badge]
	FOREIGN KEY ([UserBadgeID]) REFERENCES [CMS_Badge] ([BadgeID])
ALTER TABLE [CMS_UserSettings]
	CHECK CONSTRAINT [FK_CMS_UserSettings_UserBadgeID_CMS_Badge]
ALTER TABLE [CMS_UserSettings]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_UserSettings_UserSettingsUserGUID_CMS_User]
	FOREIGN KEY ([UserSettingsUserGUID]) REFERENCES [CMS_User] ([UserGUID])
ALTER TABLE [CMS_UserSettings]
	CHECK CONSTRAINT [FK_CMS_UserSettings_UserSettingsUserGUID_CMS_User]
ALTER TABLE [CMS_UserSettings]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_UserSettings_UserSettingsUserID_CMS_User]
	FOREIGN KEY ([UserSettingsUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [CMS_UserSettings]
	CHECK CONSTRAINT [FK_CMS_UserSettings_UserSettingsUserID_CMS_User]
ALTER TABLE [CMS_UserSettings]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_UserSettings_UserTimeZoneID_CMS_TimeZone]
	FOREIGN KEY ([UserTimeZoneID]) REFERENCES [CMS_TimeZone] ([TimeZoneID])
ALTER TABLE [CMS_UserSettings]
	CHECK CONSTRAINT [FK_CMS_UserSettings_UserTimeZoneID_CMS_TimeZone]
