CREATE TABLE [CMS_InlineControlSite] (
		[ControlID]     [int] NOT NULL,
		[SiteID]        [int] NOT NULL
) 
ALTER TABLE [CMS_InlineControlSite]
	ADD
	CONSTRAINT [PK_CMS_InlineControlSite]
	PRIMARY KEY
	CLUSTERED
	([ControlID], [SiteID])
	
	
ALTER TABLE [CMS_InlineControlSite]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_InlineControlSite_ControlD_CMS_InlineControl]
	FOREIGN KEY ([ControlID]) REFERENCES [CMS_InlineControl] ([ControlID])
ALTER TABLE [CMS_InlineControlSite]
	CHECK CONSTRAINT [FK_CMS_InlineControlSite_ControlD_CMS_InlineControl]
ALTER TABLE [CMS_InlineControlSite]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_InlineControlSite_SiteID_CMS_Site]
	FOREIGN KEY ([SiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [CMS_InlineControlSite]
	CHECK CONSTRAINT [FK_CMS_InlineControlSite_SiteID_CMS_Site]
