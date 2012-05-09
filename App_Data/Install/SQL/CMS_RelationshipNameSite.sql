CREATE TABLE [CMS_RelationshipNameSite] (
		[RelationshipNameID]     [int] NOT NULL,
		[SiteID]                 [int] NOT NULL
) 
ALTER TABLE [CMS_RelationshipNameSite]
	ADD
	CONSTRAINT [PK_CMS_RelationshipNameSite]
	PRIMARY KEY
	CLUSTERED
	([RelationshipNameID], [SiteID])
	
	
ALTER TABLE [CMS_RelationshipNameSite]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_RelationshipNameSite_RelationshipNameID_CMS_RelationshipName]
	FOREIGN KEY ([RelationshipNameID]) REFERENCES [CMS_RelationshipName] ([RelationshipNameID])
ALTER TABLE [CMS_RelationshipNameSite]
	CHECK CONSTRAINT [FK_CMS_RelationshipNameSite_RelationshipNameID_CMS_RelationshipName]
ALTER TABLE [CMS_RelationshipNameSite]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_RelationshipNameSite_SiteID_CMS_Site]
	FOREIGN KEY ([SiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [CMS_RelationshipNameSite]
	CHECK CONSTRAINT [FK_CMS_RelationshipNameSite_SiteID_CMS_Site]
