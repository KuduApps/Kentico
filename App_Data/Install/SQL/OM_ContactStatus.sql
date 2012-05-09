CREATE TABLE [OM_ContactStatus] (
		[ContactStatusID]              [int] IDENTITY(1, 1) NOT NULL,
		[ContactStatusName]            [nvarchar](200) NOT NULL,
		[ContactStatusDisplayName]     [nvarchar](200) NOT NULL,
		[ContactStatusDescription]     [nvarchar](max) NULL,
		[ContactStatusSiteID]          [int] NULL
)  
ALTER TABLE [OM_ContactStatus]
	ADD
	CONSTRAINT [PK_OM_ContactStatus]
	PRIMARY KEY
	CLUSTERED
	([ContactStatusID])
	
ALTER TABLE [OM_ContactStatus]
	ADD
	CONSTRAINT [DEFAULT_OM_ContactStatus_ContactStatusDisplayName]
	DEFAULT ('') FOR [ContactStatusDisplayName]
ALTER TABLE [OM_ContactStatus]
	ADD
	CONSTRAINT [DEFAULT_OM_ContactStatus_ContactStatusName]
	DEFAULT ('') FOR [ContactStatusName]
CREATE NONCLUSTERED INDEX [IX_OM_ContactStatus_ContactStatusSiteID]
	ON [OM_ContactStatus] ([ContactStatusSiteID])
	
ALTER TABLE [OM_ContactStatus]
	WITH CHECK
	ADD CONSTRAINT [FK_OM_ContactStatus_CMS_Site]
	FOREIGN KEY ([ContactStatusSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [OM_ContactStatus]
	CHECK CONSTRAINT [FK_OM_ContactStatus_CMS_Site]
