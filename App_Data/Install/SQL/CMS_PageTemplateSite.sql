CREATE TABLE [CMS_PageTemplateSite] (
		[PageTemplateID]     [int] NOT NULL,
		[SiteID]             [int] NOT NULL
) 
ALTER TABLE [CMS_PageTemplateSite]
	ADD
	CONSTRAINT [PK_CMS_PageTemplateSite]
	PRIMARY KEY
	CLUSTERED
	([PageTemplateID], [SiteID])
	
	
ALTER TABLE [CMS_PageTemplateSite]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_PageTemplateSite_PageTemplateID_CMS_PageTemplate]
	FOREIGN KEY ([PageTemplateID]) REFERENCES [CMS_PageTemplate] ([PageTemplateID])
ALTER TABLE [CMS_PageTemplateSite]
	CHECK CONSTRAINT [FK_CMS_PageTemplateSite_PageTemplateID_CMS_PageTemplate]
ALTER TABLE [CMS_PageTemplateSite]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_PageTemplateSite_SiteID_CMS_Site]
	FOREIGN KEY ([SiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [CMS_PageTemplateSite]
	CHECK CONSTRAINT [FK_CMS_PageTemplateSite_SiteID_CMS_Site]
