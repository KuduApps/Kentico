CREATE TABLE [CMS_WebPartContainerSite] (
		[ContainerID]     [int] NOT NULL,
		[SiteID]          [int] NOT NULL
) 
ALTER TABLE [CMS_WebPartContainerSite]
	ADD
	CONSTRAINT [PK_CMS_WebPartContainerSite]
	PRIMARY KEY
	CLUSTERED
	([ContainerID], [SiteID])
	
	
ALTER TABLE [CMS_WebPartContainerSite]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_WebPartContainerSite_ContainerID_CMS_WebPartContainer]
	FOREIGN KEY ([ContainerID]) REFERENCES [CMS_WebPartContainer] ([ContainerID])
ALTER TABLE [CMS_WebPartContainerSite]
	CHECK CONSTRAINT [FK_CMS_WebPartContainerSite_ContainerID_CMS_WebPartContainer]
ALTER TABLE [CMS_WebPartContainerSite]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_WebPartContainerSite_SiteID_CMS_Site]
	FOREIGN KEY ([SiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [CMS_WebPartContainerSite]
	CHECK CONSTRAINT [FK_CMS_WebPartContainerSite_SiteID_CMS_Site]
