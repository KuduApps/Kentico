CREATE TABLE [OM_Account] (
		[AccountID]                      [int] IDENTITY(1, 1) NOT NULL,
		[AccountName]                    [nvarchar](200) NOT NULL,
		[AccountAddress1]                [nvarchar](100) NULL,
		[AccountAddress2]                [nvarchar](100) NULL,
		[AccountCity]                    [nvarchar](100) NULL,
		[AccountZIP]                     [nvarchar](20) NULL,
		[AccountStateID]                 [int] NULL,
		[AccountCountryID]               [int] NULL,
		[AccountWebSite]                 [nvarchar](200) NULL,
		[AccountPhone]                   [nvarchar](26) NULL,
		[AccountEmail]                   [nvarchar](100) NULL,
		[AccountFax]                     [nvarchar](26) NULL,
		[AccountPrimaryContactID]        [int] NULL,
		[AccountSecondaryContactID]      [int] NULL,
		[AccountStatusID]                [int] NULL,
		[AccountNotes]                   [nvarchar](max) NULL,
		[AccountOwnerUserID]             [int] NULL,
		[AccountSubsidiaryOfID]          [int] NULL,
		[AccountMergedWithAccountID]     [int] NULL,
		[AccountSiteID]                  [int] NULL,
		[AccountGUID]                    [uniqueidentifier] NOT NULL,
		[AccountLastModified]            [datetime] NOT NULL,
		[AccountCreated]                 [datetime] NOT NULL,
		[AccountGlobalAccountID]         [int] NULL
)  
ALTER TABLE [OM_Account]
	ADD
	CONSTRAINT [PK_OM_Account]
	PRIMARY KEY
	CLUSTERED
	([AccountID])
	
CREATE NONCLUSTERED INDEX [IX_OM_Account_AccountCountryID]
	ON [OM_Account] ([AccountCountryID])
	
CREATE NONCLUSTERED INDEX [IX_OM_Account_AccountGlobalAccountID]
	ON [OM_Account] ([AccountGlobalAccountID])
	
CREATE NONCLUSTERED INDEX [IX_OM_Account_AccountMergedWithAccountID]
	ON [OM_Account] ([AccountMergedWithAccountID])
	
CREATE NONCLUSTERED INDEX [IX_OM_Account_AccountOwnerUserID]
	ON [OM_Account] ([AccountOwnerUserID])
	
CREATE NONCLUSTERED INDEX [IX_OM_Account_AccountPrimaryContactID]
	ON [OM_Account] ([AccountPrimaryContactID])
	
CREATE NONCLUSTERED INDEX [IX_OM_Account_AccountSecondaryContactID]
	ON [OM_Account] ([AccountSecondaryContactID])
	
CREATE NONCLUSTERED INDEX [IX_OM_Account_AccountSiteID]
	ON [OM_Account] ([AccountSiteID])
	
CREATE NONCLUSTERED INDEX [IX_OM_Account_AccountStateID]
	ON [OM_Account] ([AccountStateID])
	
CREATE NONCLUSTERED INDEX [IX_OM_Account_AccountStatusID]
	ON [OM_Account] ([AccountStatusID])
	
CREATE NONCLUSTERED INDEX [IX_OM_Account_AccountSubsidiaryOfID]
	ON [OM_Account] ([AccountSubsidiaryOfID])
	
ALTER TABLE [OM_Account]
	WITH CHECK
	ADD CONSTRAINT [FK_OM_Account_CMS_Country]
	FOREIGN KEY ([AccountCountryID]) REFERENCES [CMS_Country] ([CountryID])
ALTER TABLE [OM_Account]
	CHECK CONSTRAINT [FK_OM_Account_CMS_Country]
ALTER TABLE [OM_Account]
	WITH CHECK
	ADD CONSTRAINT [FK_OM_Account_CMS_Site]
	FOREIGN KEY ([AccountSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [OM_Account]
	CHECK CONSTRAINT [FK_OM_Account_CMS_Site]
ALTER TABLE [OM_Account]
	WITH CHECK
	ADD CONSTRAINT [FK_OM_Account_CMS_State]
	FOREIGN KEY ([AccountStateID]) REFERENCES [CMS_State] ([StateID])
ALTER TABLE [OM_Account]
	CHECK CONSTRAINT [FK_OM_Account_CMS_State]
ALTER TABLE [OM_Account]
	WITH CHECK
	ADD CONSTRAINT [FK_OM_Account_CMS_User]
	FOREIGN KEY ([AccountOwnerUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [OM_Account]
	CHECK CONSTRAINT [FK_OM_Account_CMS_User]
ALTER TABLE [OM_Account]
	WITH CHECK
	ADD CONSTRAINT [FK_OM_Account_OM_Account_MergedWith]
	FOREIGN KEY ([AccountMergedWithAccountID]) REFERENCES [OM_Account] ([AccountID])
ALTER TABLE [OM_Account]
	CHECK CONSTRAINT [FK_OM_Account_OM_Account_MergedWith]
ALTER TABLE [OM_Account]
	WITH CHECK
	ADD CONSTRAINT [FK_OM_Account_OM_Account_OriginalGlobal]
	FOREIGN KEY ([AccountGlobalAccountID]) REFERENCES [OM_Account] ([AccountID])
ALTER TABLE [OM_Account]
	CHECK CONSTRAINT [FK_OM_Account_OM_Account_OriginalGlobal]
ALTER TABLE [OM_Account]
	WITH CHECK
	ADD CONSTRAINT [FK_OM_Account_OM_Account_SubsidiaryOf]
	FOREIGN KEY ([AccountSubsidiaryOfID]) REFERENCES [OM_Account] ([AccountID])
ALTER TABLE [OM_Account]
	CHECK CONSTRAINT [FK_OM_Account_OM_Account_SubsidiaryOf]
ALTER TABLE [OM_Account]
	WITH CHECK
	ADD CONSTRAINT [FK_OM_Account_OM_AccountStatus]
	FOREIGN KEY ([AccountStatusID]) REFERENCES [OM_AccountStatus] ([AccountStatusID])
ALTER TABLE [OM_Account]
	CHECK CONSTRAINT [FK_OM_Account_OM_AccountStatus]
ALTER TABLE [OM_Account]
	WITH CHECK
	ADD CONSTRAINT [FK_OM_Account_OM_Contact_PrimaryContact]
	FOREIGN KEY ([AccountPrimaryContactID]) REFERENCES [OM_Contact] ([ContactID])
ALTER TABLE [OM_Account]
	CHECK CONSTRAINT [FK_OM_Account_OM_Contact_PrimaryContact]
ALTER TABLE [OM_Account]
	WITH CHECK
	ADD CONSTRAINT [FK_OM_Account_OM_Contact_SecondaryContact]
	FOREIGN KEY ([AccountSecondaryContactID]) REFERENCES [OM_Contact] ([ContactID])
ALTER TABLE [OM_Account]
	CHECK CONSTRAINT [FK_OM_Account_OM_Contact_SecondaryContact]
