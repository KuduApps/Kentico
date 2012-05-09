CREATE TABLE [CMS_PageTemplateScope] (
		[PageTemplateScopeID]               [int] IDENTITY(1, 1) NOT NULL,
		[PageTemplateScopePath]             [nvarchar](450) NOT NULL,
		[PageTemplateScopeLevels]           [nvarchar](450) NULL,
		[PageTemplateScopeCultureID]        [int] NULL,
		[PageTemplateScopeClassID]          [int] NULL,
		[PageTemplateScopeTemplateID]       [int] NOT NULL,
		[PageTemplateScopeSiteID]           [int] NULL,
		[PageTemplateScopeLastModified]     [datetime] NOT NULL,
		[PageTemplateScopeGUID]             [uniqueidentifier] NOT NULL
) 
ALTER TABLE [CMS_PageTemplateScope]
	ADD
	CONSTRAINT [PK_CMS_PageTemplateScope]
	PRIMARY KEY
	NONCLUSTERED
	([PageTemplateScopeID])
	
	
ALTER TABLE [CMS_PageTemplateScope]
	ADD
	CONSTRAINT [DEFAULT_CMS_PageTemplateScope_PageTemplateScopeGUID]
	DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [PageTemplateScopeGUID]
ALTER TABLE [CMS_PageTemplateScope]
	ADD
	CONSTRAINT [DEFAULT_CMS_PageTemplateScope_PageTemplateScopeLastModified]
	DEFAULT ('2/22/2010 9:30:07 AM') FOR [PageTemplateScopeLastModified]
ALTER TABLE [CMS_PageTemplateScope]
	ADD
	CONSTRAINT [DEFAULT_CMS_PageTemplateScope_PageTemplateScopePath]
	DEFAULT ('') FOR [PageTemplateScopePath]
ALTER TABLE [CMS_PageTemplateScope]
	ADD
	CONSTRAINT [DEFAULT_CMS_PageTemplateScope_PageTemplateScopeTemplateID]
	DEFAULT ((0)) FOR [PageTemplateScopeTemplateID]
CREATE NONCLUSTERED INDEX [IX_CMS_PageTemplateScope_PageTemplateScopeClassID]
	ON [CMS_PageTemplateScope] ([PageTemplateScopeClassID])
	
CREATE NONCLUSTERED INDEX [IX_CMS_PageTemplateScope_PageTemplateScopeCultureID]
	ON [CMS_PageTemplateScope] ([PageTemplateScopeCultureID])
	
CREATE NONCLUSTERED INDEX [IX_CMS_PageTemplateScope_PageTemplateScopeLevels]
	ON [CMS_PageTemplateScope] ([PageTemplateScopeLevels])
	
	
CREATE CLUSTERED INDEX [IX_CMS_PageTemplateScope_PageTemplateScopePath]
	ON [CMS_PageTemplateScope] ([PageTemplateScopePath])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_PageTemplateScope_PageTemplateScopeSiteID]
	ON [CMS_PageTemplateScope] ([PageTemplateScopeSiteID])
	
CREATE NONCLUSTERED INDEX [IX_CMS_PageTemplateScope_PageTemplateScopeTemplateID]
	ON [CMS_PageTemplateScope] ([PageTemplateScopeTemplateID])
	
ALTER TABLE [CMS_PageTemplateScope]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_PageTemplateScope_PageTemplateScopeClassID_CMS_Class]
	FOREIGN KEY ([PageTemplateScopeClassID]) REFERENCES [CMS_Class] ([ClassID])
ALTER TABLE [CMS_PageTemplateScope]
	CHECK CONSTRAINT [FK_CMS_PageTemplateScope_PageTemplateScopeClassID_CMS_Class]
ALTER TABLE [CMS_PageTemplateScope]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_PageTemplateScope_PageTemplateScopeCultureID_CMS_Culture]
	FOREIGN KEY ([PageTemplateScopeCultureID]) REFERENCES [CMS_Culture] ([CultureID])
ALTER TABLE [CMS_PageTemplateScope]
	CHECK CONSTRAINT [FK_CMS_PageTemplateScope_PageTemplateScopeCultureID_CMS_Culture]
ALTER TABLE [CMS_PageTemplateScope]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_PageTemplateScope_PageTemplateScopeSiteID_CMS_Site]
	FOREIGN KEY ([PageTemplateScopeSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [CMS_PageTemplateScope]
	CHECK CONSTRAINT [FK_CMS_PageTemplateScope_PageTemplateScopeSiteID_CMS_Site]
ALTER TABLE [CMS_PageTemplateScope]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_PageTemplateScope_PageTemplateScopeTemplateID_CMS_PageTemplate]
	FOREIGN KEY ([PageTemplateScopeTemplateID]) REFERENCES [CMS_PageTemplate] ([PageTemplateID])
ALTER TABLE [CMS_PageTemplateScope]
	CHECK CONSTRAINT [FK_CMS_PageTemplateScope_PageTemplateScopeTemplateID_CMS_PageTemplate]
