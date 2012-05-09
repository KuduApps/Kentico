CREATE TABLE [CMS_SearchIndexSite] (
		[IndexID]         [int] NOT NULL,
		[IndexSiteID]     [int] NOT NULL
) 
ALTER TABLE [CMS_SearchIndexSite]
	ADD
	CONSTRAINT [PK_CMS_SearchIndexSite]
	PRIMARY KEY
	CLUSTERED
	([IndexID], [IndexSiteID])
	
	
ALTER TABLE [CMS_SearchIndexSite]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_SearchIndexSite_IndexID_CMS_SearchIndex]
	FOREIGN KEY ([IndexID]) REFERENCES [CMS_SearchIndex] ([IndexID])
ALTER TABLE [CMS_SearchIndexSite]
	CHECK CONSTRAINT [FK_CMS_SearchIndexSite_IndexID_CMS_SearchIndex]
ALTER TABLE [CMS_SearchIndexSite]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_SearchIndexSite_IndexSiteID_CMS_Site]
	FOREIGN KEY ([IndexSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [CMS_SearchIndexSite]
	CHECK CONSTRAINT [FK_CMS_SearchIndexSite_IndexSiteID_CMS_Site]
