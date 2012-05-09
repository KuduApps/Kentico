CREATE TABLE [CMS_Site] (
		[SiteID]                          [int] IDENTITY(1, 1) NOT NULL,
		[SiteName]                        [nvarchar](100) NOT NULL,
		[SiteDisplayName]                 [nvarchar](200) NOT NULL,
		[SiteDescription]                 [nvarchar](max) NULL,
		[SiteStatus]                      [nvarchar](20) NOT NULL,
		[SiteDomainName]                  [nvarchar](400) NOT NULL,
		[SiteDefaultStylesheetID]         [int] NULL,
		[SiteDefaultVisitorCulture]       [nvarchar](50) NULL,
		[SiteDefaultEditorStylesheet]     [int] NULL,
		[SiteGUID]                        [uniqueidentifier] NOT NULL,
		[SiteLastModified]                [datetime] NOT NULL,
		[SiteIsOffline]                   [bit] NULL,
		[SiteOfflineRedirectURL]          [nvarchar](400) NULL,
		[SiteOfflineMessage]              [nvarchar](max) NULL
)  
ALTER TABLE [CMS_Site]
	ADD
	CONSTRAINT [PK_CMS_Site]
	PRIMARY KEY
	NONCLUSTERED
	([SiteID])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_Site_SiteDefaultEditorStylesheet]
	ON [CMS_Site] ([SiteDefaultEditorStylesheet])
	
CREATE NONCLUSTERED INDEX [IX_CMS_Site_SiteDefaultStylesheetID]
	ON [CMS_Site] ([SiteDefaultStylesheetID])
	
CREATE CLUSTERED INDEX [IX_CMS_Site_SiteDisplayName]
	ON [CMS_Site] ([SiteDisplayName])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_Site_SiteDomainName_SiteStatus]
	ON [CMS_Site] ([SiteDomainName], [SiteStatus])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_Site_SiteName]
	ON [CMS_Site] ([SiteName])
	
	
ALTER TABLE [CMS_Site]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_Site_SiteDefaultEditorStylesheet_CMS_CssStylesheet]
	FOREIGN KEY ([SiteDefaultEditorStylesheet]) REFERENCES [CMS_CssStylesheet] ([StylesheetID])
ALTER TABLE [CMS_Site]
	CHECK CONSTRAINT [FK_CMS_Site_SiteDefaultEditorStylesheet_CMS_CssStylesheet]
ALTER TABLE [CMS_Site]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_Site_SiteDefaultStylesheetID_CMS_CssStylesheet]
	FOREIGN KEY ([SiteDefaultStylesheetID]) REFERENCES [CMS_CssStylesheet] ([StylesheetID])
ALTER TABLE [CMS_Site]
	CHECK CONSTRAINT [FK_CMS_Site_SiteDefaultStylesheetID_CMS_CssStylesheet]
