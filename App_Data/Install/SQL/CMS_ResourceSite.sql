CREATE TABLE [CMS_ResourceSite] (
		[ResourceID]     [int] NOT NULL,
		[SiteID]         [int] NOT NULL
) 
ALTER TABLE [CMS_ResourceSite]
	ADD
	CONSTRAINT [PK_CMS_ResourceSite]
	PRIMARY KEY
	CLUSTERED
	([ResourceID], [SiteID])
	
	
ALTER TABLE [CMS_ResourceSite]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_ResourceSite_ResourceID_CMS_Resource]
	FOREIGN KEY ([ResourceID]) REFERENCES [CMS_Resource] ([ResourceID])
ALTER TABLE [CMS_ResourceSite]
	CHECK CONSTRAINT [FK_CMS_ResourceSite_ResourceID_CMS_Resource]
ALTER TABLE [CMS_ResourceSite]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_ResourceSite_SiteID_CMS_Site]
	FOREIGN KEY ([SiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [CMS_ResourceSite]
	CHECK CONSTRAINT [FK_CMS_ResourceSite_SiteID_CMS_Site]
