CREATE TABLE [OM_ContactRole] (
		[ContactRoleID]              [int] IDENTITY(1, 1) NOT NULL,
		[ContactRoleName]            [nvarchar](200) NOT NULL,
		[ContactRoleDisplayName]     [nvarchar](200) NOT NULL,
		[ContactRoleDescription]     [nvarchar](max) NULL,
		[ContactRoleSiteID]          [int] NULL
)  
ALTER TABLE [OM_ContactRole]
	ADD
	CONSTRAINT [PK_OM_ContactRole]
	PRIMARY KEY
	CLUSTERED
	([ContactRoleID])
	
ALTER TABLE [OM_ContactRole]
	ADD
	CONSTRAINT [DEFAULT_OM_ContactRole_ContactRoleDisplayName]
	DEFAULT ('') FOR [ContactRoleDisplayName]
ALTER TABLE [OM_ContactRole]
	ADD
	CONSTRAINT [DEFAULT_OM_ContactRole_ContactRoleName]
	DEFAULT ('') FOR [ContactRoleName]
CREATE NONCLUSTERED INDEX [IX_OM_ContactRole_ContactRoleSiteID]
	ON [OM_ContactRole] ([ContactRoleSiteID])
	
ALTER TABLE [OM_ContactRole]
	WITH CHECK
	ADD CONSTRAINT [FK_OM_ContactRole_CMS_Site]
	FOREIGN KEY ([ContactRoleSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [OM_ContactRole]
	CHECK CONSTRAINT [FK_OM_ContactRole_CMS_Site]
