CREATE TABLE [CMS_AbuseReport] (
		[ReportID]             [int] IDENTITY(1, 1) NOT NULL,
		[ReportGUID]           [uniqueidentifier] NOT NULL,
		[ReportTitle]          [nvarchar](100) NULL,
		[ReportURL]            [nvarchar](1000) NOT NULL,
		[ReportCulture]        [nvarchar](50) NOT NULL,
		[ReportObjectID]       [int] NULL,
		[ReportObjectType]     [nvarchar](100) NULL,
		[ReportComment]        [nvarchar](max) NOT NULL,
		[ReportUserID]         [int] NULL,
		[ReportWhen]           [datetime] NOT NULL,
		[ReportStatus]         [int] NOT NULL,
		[ReportSiteID]         [int] NOT NULL
)  
ALTER TABLE [CMS_AbuseReport]
	ADD
	CONSTRAINT [PK_CMS_AbuseReport]
	PRIMARY KEY
	NONCLUSTERED
	([ReportID])
	
	
ALTER TABLE [CMS_AbuseReport]
	ADD
	CONSTRAINT [DEFAULT_cms_abusereport_ReportComment]
	DEFAULT ('') FOR [ReportComment]
ALTER TABLE [CMS_AbuseReport]
	ADD
	CONSTRAINT [DEFAULT_cms_abusereport_ReportCulture]
	DEFAULT ('') FOR [ReportCulture]
ALTER TABLE [CMS_AbuseReport]
	ADD
	CONSTRAINT [DEFAULT_cms_abusereport_ReportGUID]
	DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [ReportGUID]
ALTER TABLE [CMS_AbuseReport]
	ADD
	CONSTRAINT [DEFAULT_cms_abusereport_ReportSiteID]
	DEFAULT ((0)) FOR [ReportSiteID]
ALTER TABLE [CMS_AbuseReport]
	ADD
	CONSTRAINT [DEFAULT_cms_abusereport_ReportStatus]
	DEFAULT ((0)) FOR [ReportStatus]
ALTER TABLE [CMS_AbuseReport]
	ADD
	CONSTRAINT [DEFAULT_CMS_AbuseReport_ReportTitle]
	DEFAULT ('') FOR [ReportTitle]
ALTER TABLE [CMS_AbuseReport]
	ADD
	CONSTRAINT [DEFAULT_cms_abusereport_ReportURL]
	DEFAULT ('') FOR [ReportURL]
ALTER TABLE [CMS_AbuseReport]
	ADD
	CONSTRAINT [DEFAULT_cms_abusereport_ReportWhen]
	DEFAULT ('9/11/2008 4:32:15 PM') FOR [ReportWhen]
CREATE NONCLUSTERED INDEX [IX_CMS_AbuseReport_ReportSiteID]
	ON [CMS_AbuseReport] ([ReportSiteID])
	
CREATE NONCLUSTERED INDEX [IX_CMS_AbuseReport_ReportStatus]
	ON [CMS_AbuseReport] ([ReportStatus])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_AbuseReport_ReportUserID]
	ON [CMS_AbuseReport] ([ReportUserID])
	
CREATE CLUSTERED INDEX [IX_CMS_AbuseReport_ReportWhen]
	ON [CMS_AbuseReport] ([ReportWhen] DESC)
	
	
ALTER TABLE [CMS_AbuseReport]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_AbuseReport_ReportSiteID_CMS_Site]
	FOREIGN KEY ([ReportSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [CMS_AbuseReport]
	CHECK CONSTRAINT [FK_CMS_AbuseReport_ReportSiteID_CMS_Site]
ALTER TABLE [CMS_AbuseReport]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_AbuseReport_ReportUserID_CMS_User]
	FOREIGN KEY ([ReportUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [CMS_AbuseReport]
	CHECK CONSTRAINT [FK_CMS_AbuseReport_ReportUserID_CMS_User]
