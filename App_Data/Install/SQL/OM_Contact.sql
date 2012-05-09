CREATE TABLE [OM_Contact] (
		[ContactID]                      [int] IDENTITY(1, 1) NOT NULL,
		[ContactFirstName]               [nvarchar](100) NULL,
		[ContactMiddleName]              [nvarchar](100) NULL,
		[ContactLastName]                [nvarchar](100) NOT NULL,
		[ContactSalutation]              [nvarchar](50) NULL,
		[ContactTitleBefore]             [nvarchar](50) NULL,
		[ContactTitleAfter]              [nvarchar](50) NULL,
		[ContactJobTitle]                [nvarchar](50) NULL,
		[ContactAddress1]                [nvarchar](100) NULL,
		[ContactAddress2]                [nvarchar](100) NULL,
		[ContactCity]                    [nvarchar](100) NULL,
		[ContactZIP]                     [nvarchar](20) NULL,
		[ContactStateID]                 [int] NULL,
		[ContactCountryID]               [int] NULL,
		[ContactMobilePhone]             [nvarchar](26) NULL,
		[ContactHomePhone]               [nvarchar](26) NULL,
		[ContactBusinessPhone]           [nvarchar](26) NULL,
		[ContactEmail]                   [nvarchar](100) NULL,
		[ContactWebSite]                 [nvarchar](100) NULL,
		[ContactBirthday]                [datetime] NULL,
		[ContactGender]                  [int] NULL,
		[ContactStatusID]                [int] NULL,
		[ContactNotes]                   [nvarchar](max) NULL,
		[ContactOwnerUserID]             [int] NULL,
		[ContactMonitored]               [bit] NULL,
		[ContactMergedWithContactID]     [int] NULL,
		[ContactIsAnonymous]             [bit] NOT NULL,
		[ContactSiteID]                  [int] NULL,
		[ContactGUID]                    [uniqueidentifier] NOT NULL,
		[ContactLastModified]            [datetime] NOT NULL,
		[ContactCreated]                 [datetime] NOT NULL,
		[ContactMergedWhen]              [datetime] NULL,
		[ContactGlobalContactID]         [int] NULL,
		[ContactBounces]                 [int] NULL,
		[ContactLastLogon]               [datetime] NULL,
		[ContactCampaign]                [nvarchar](200) NULL
)  
ALTER TABLE [OM_Contact]
	ADD
	CONSTRAINT [PK_OM_Contact]
	PRIMARY KEY
	CLUSTERED
	([ContactID])
	
ALTER TABLE [OM_Contact]
	ADD
	CONSTRAINT [DEFAULT_OM_Contact_ContactCreated]
	DEFAULT ('5/3/2011 10:51:13 AM') FOR [ContactCreated]
ALTER TABLE [OM_Contact]
	ADD
	CONSTRAINT [DEFAULT_OM_Contact_ContactGUID]
	DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [ContactGUID]
ALTER TABLE [OM_Contact]
	ADD
	CONSTRAINT [DEFAULT_OM_Contact_ContactIsAnonymous]
	DEFAULT ((1)) FOR [ContactIsAnonymous]
CREATE NONCLUSTERED INDEX [IX_OM_Contact_ContactCountryID]
	ON [OM_Contact] ([ContactCountryID])
	
CREATE NONCLUSTERED INDEX [IX_OM_Contact_ContactGlobalContactID]
	ON [OM_Contact] ([ContactGlobalContactID])
	
CREATE NONCLUSTERED INDEX [IX_OM_Contact_ContactMergedWithContactID]
	ON [OM_Contact] ([ContactMergedWithContactID])
	
CREATE NONCLUSTERED INDEX [IX_OM_Contact_ContactOwnerUserID]
	ON [OM_Contact] ([ContactOwnerUserID])
	
CREATE NONCLUSTERED INDEX [IX_OM_Contact_ContactSiteID]
	ON [OM_Contact] ([ContactSiteID])
	
CREATE NONCLUSTERED INDEX [IX_OM_Contact_ContactStateID]
	ON [OM_Contact] ([ContactStateID])
	
CREATE NONCLUSTERED INDEX [IX_OM_Contact_ContactStatusID]
	ON [OM_Contact] ([ContactStatusID])
	
ALTER TABLE [OM_Contact]
	WITH CHECK
	ADD CONSTRAINT [FK_OM_Contact_CMS_Country]
	FOREIGN KEY ([ContactCountryID]) REFERENCES [CMS_Country] ([CountryID])
ALTER TABLE [OM_Contact]
	CHECK CONSTRAINT [FK_OM_Contact_CMS_Country]
ALTER TABLE [OM_Contact]
	WITH CHECK
	ADD CONSTRAINT [FK_OM_Contact_CMS_Site]
	FOREIGN KEY ([ContactSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [OM_Contact]
	CHECK CONSTRAINT [FK_OM_Contact_CMS_Site]
ALTER TABLE [OM_Contact]
	WITH CHECK
	ADD CONSTRAINT [FK_OM_Contact_CMS_State]
	FOREIGN KEY ([ContactStateID]) REFERENCES [CMS_State] ([StateID])
ALTER TABLE [OM_Contact]
	CHECK CONSTRAINT [FK_OM_Contact_CMS_State]
ALTER TABLE [OM_Contact]
	WITH CHECK
	ADD CONSTRAINT [FK_OM_Contact_CMS_User]
	FOREIGN KEY ([ContactOwnerUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [OM_Contact]
	CHECK CONSTRAINT [FK_OM_Contact_CMS_User]
ALTER TABLE [OM_Contact]
	WITH CHECK
	ADD CONSTRAINT [FK_OM_Contact_OM_Contact_ActiveGlobal]
	FOREIGN KEY ([ContactGlobalContactID]) REFERENCES [OM_Contact] ([ContactID])
ALTER TABLE [OM_Contact]
	CHECK CONSTRAINT [FK_OM_Contact_OM_Contact_ActiveGlobal]
ALTER TABLE [OM_Contact]
	WITH CHECK
	ADD CONSTRAINT [FK_OM_Contact_OM_Contact_Merged]
	FOREIGN KEY ([ContactMergedWithContactID]) REFERENCES [OM_Contact] ([ContactID])
ALTER TABLE [OM_Contact]
	CHECK CONSTRAINT [FK_OM_Contact_OM_Contact_Merged]
ALTER TABLE [OM_Contact]
	WITH CHECK
	ADD CONSTRAINT [FK_OM_Contact_OM_ContactStatus]
	FOREIGN KEY ([ContactStatusID]) REFERENCES [OM_ContactStatus] ([ContactStatusID])
ALTER TABLE [OM_Contact]
	CHECK CONSTRAINT [FK_OM_Contact_OM_ContactStatus]
