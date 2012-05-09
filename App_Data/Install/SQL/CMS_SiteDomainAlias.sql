CREATE TABLE [CMS_SiteDomainAlias] (
		[SiteDomainAliasID]              [int] IDENTITY(1, 1) NOT NULL,
		[SiteDomainAliasName]            [nvarchar](400) NOT NULL,
		[SiteID]                         [int] NOT NULL,
		[SiteDefaultVisitorCulture]      [nvarchar](50) NULL,
		[SiteDomainGUID]                 [uniqueidentifier] NOT NULL,
		[SiteDomainLastModified]         [datetime] NOT NULL,
		[SiteDomainDefaultAliasPath]     [nvarchar](450) NULL,
		[SiteDomainRedirectUrl]          [nvarchar](450) NULL
) 
ALTER TABLE [CMS_SiteDomainAlias]
	ADD
	CONSTRAINT [PK_CMS_SiteDomainAlias]
	PRIMARY KEY
	CLUSTERED
	([SiteDomainAliasID])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_SiteDomainAlias_SiteDomainAliasName]
	ON [CMS_SiteDomainAlias] ([SiteDomainAliasName])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_SiteDomainAlias_SiteID]
	ON [CMS_SiteDomainAlias] ([SiteID])
	
ALTER TABLE [CMS_SiteDomainAlias]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_SiteDomainAlias_SiteID_CMS_Site]
	FOREIGN KEY ([SiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [CMS_SiteDomainAlias]
	CHECK CONSTRAINT [FK_CMS_SiteDomainAlias_SiteID_CMS_Site]
