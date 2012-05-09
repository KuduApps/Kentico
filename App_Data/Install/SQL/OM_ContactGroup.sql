CREATE TABLE [OM_ContactGroup] (
		[ContactGroupID]                   [int] IDENTITY(1, 1) NOT NULL,
		[ContactGroupName]                 [nvarchar](200) NOT NULL,
		[ContactGroupDisplayName]          [nvarchar](200) NOT NULL,
		[ContactGroupDescription]          [nvarchar](max) NULL,
		[ContactGroupSiteID]               [int] NULL,
		[ContactGroupDynamicCondition]     [nvarchar](max) NULL,
		[ContactGroupScheduledTaskID]      [int] NULL,
		[ContactGroupEnabled]              [bit] NULL,
		[ContactGroupLastModified]         [datetime] NULL,
		[ContactGroupGUID]                 [uniqueidentifier] NULL,
		[ContactGroupStatus]               [int] NULL
)  
ALTER TABLE [OM_ContactGroup]
	ADD
	CONSTRAINT [PK_CMS_ContactGroup]
	PRIMARY KEY
	CLUSTERED
	([ContactGroupID])
	
CREATE NONCLUSTERED INDEX [IX_OM_ContactGroup_ContactGroupSiteID]
	ON [OM_ContactGroup] ([ContactGroupSiteID])
	
ALTER TABLE [OM_ContactGroup]
	WITH CHECK
	ADD CONSTRAINT [FK_OM_ContactGroup_CMS_Site]
	FOREIGN KEY ([ContactGroupSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [OM_ContactGroup]
	CHECK CONSTRAINT [FK_OM_ContactGroup_CMS_Site]
