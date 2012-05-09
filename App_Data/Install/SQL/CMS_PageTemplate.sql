CREATE TABLE [CMS_PageTemplate] (
		[PageTemplateID]                              [int] IDENTITY(1, 1) NOT NULL,
		[PageTemplateDisplayName]                     [nvarchar](200) NOT NULL,
		[PageTemplateCodeName]                        [nvarchar](100) NULL,
		[PageTemplateDescription]                     [nvarchar](max) NULL,
		[PageTemplateFile]                            [nvarchar](400) NOT NULL,
		[PageTemplateIsPortal]                        [bit] NULL,
		[PageTemplateCategoryID]                      [int] NULL,
		[PageTemplateLayoutID]                        [int] NULL,
		[PageTemplateWebParts]                        [nvarchar](max) NULL,
		[PageTemplateIsReusable]                      [bit] NULL,
		[PageTemplateShowAsMasterTemplate]            [bit] NULL,
		[PageTemplateInheritPageLevels]               [nvarchar](200) NULL,
		[PageTemplateLayout]                          [nvarchar](max) NULL,
		[PageTemplateLayoutCheckedOutFileName]        [nvarchar](450) NULL,
		[PageTemplateLayoutCheckedOutByUserID]        [int] NULL,
		[PageTemplateLayoutCheckedOutMachineName]     [nvarchar](50) NULL,
		[PageTemplateVersionGUID]                     [nvarchar](50) NULL,
		[PageTemplateHeader]                          [nvarchar](max) NULL,
		[PageTemplateGUID]                            [uniqueidentifier] NOT NULL,
		[PageTemplateLastModified]                    [datetime] NOT NULL,
		[PageTemplateSiteID]                          [int] NULL,
		[PageTemplateForAllPages]                     [bit] NULL,
		[PageTemplateType]                            [nvarchar](10) NULL,
		[PageTemplateLayoutType]                      [nvarchar](50) NULL,
		[PageTemplateCSS]                             [nvarchar](max) NULL
)  
ALTER TABLE [CMS_PageTemplate]
	ADD
	CONSTRAINT [PK_CMS_PageTemplate]
	PRIMARY KEY
	NONCLUSTERED
	([PageTemplateID])
	
	
ALTER TABLE [CMS_PageTemplate]
	ADD
	CONSTRAINT [DEFAULT_CMS_PageTemplate_PageTemplateFile]
	DEFAULT ('a') FOR [PageTemplateFile]
ALTER TABLE [CMS_PageTemplate]
	ADD
	CONSTRAINT [DEFAULT_CMS_PageTemplate_PageTemplateForAllPages]
	DEFAULT ((1)) FOR [PageTemplateForAllPages]
CREATE CLUSTERED INDEX [IX_CMS_PageTemplate_PageTemplateCategoryID]
	ON [CMS_PageTemplate] ([PageTemplateCategoryID])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_PageTemplate_PageTemplateCodeName_PageTemplateDisplayName]
	ON [CMS_PageTemplate] ([PageTemplateCodeName], [PageTemplateDisplayName])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_PageTemplate_PageTemplateIsReusable_PageTemplateForAllPages_PageTemplateShowAsMasterTemplate]
	ON [CMS_PageTemplate] ([PageTemplateIsReusable], [PageTemplateForAllPages], [PageTemplateShowAsMasterTemplate])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_PageTemplate_PageTemplateLayoutCheckedOutByUserID]
	ON [CMS_PageTemplate] ([PageTemplateLayoutCheckedOutByUserID])
	
CREATE NONCLUSTERED INDEX [IX_CMS_PageTemplate_PageTemplateLayoutID]
	ON [CMS_PageTemplate] ([PageTemplateLayoutID])
	
CREATE NONCLUSTERED INDEX [IX_CMS_PageTemplate_PageTemplateSiteID_PageTemplateCodeName_PageTemplateGUID]
	ON [CMS_PageTemplate] ([PageTemplateSiteID], [PageTemplateCodeName], [PageTemplateGUID])
	
	
ALTER TABLE [CMS_PageTemplate]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_PageTemplate_PageTemplateCategoryID_CMS_PageTemplateCategory]
	FOREIGN KEY ([PageTemplateCategoryID]) REFERENCES [CMS_PageTemplateCategory] ([CategoryID])
ALTER TABLE [CMS_PageTemplate]
	CHECK CONSTRAINT [FK_CMS_PageTemplate_PageTemplateCategoryID_CMS_PageTemplateCategory]
ALTER TABLE [CMS_PageTemplate]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_PageTemplate_PageTemplateLayoutCheckedOutByUserID_CMS_User]
	FOREIGN KEY ([PageTemplateLayoutCheckedOutByUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [CMS_PageTemplate]
	CHECK CONSTRAINT [FK_CMS_PageTemplate_PageTemplateLayoutCheckedOutByUserID_CMS_User]
ALTER TABLE [CMS_PageTemplate]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_PageTemplate_PageTemplateLayoutID_CMS_Layout]
	FOREIGN KEY ([PageTemplateLayoutID]) REFERENCES [CMS_Layout] ([LayoutID])
ALTER TABLE [CMS_PageTemplate]
	CHECK CONSTRAINT [FK_CMS_PageTemplate_PageTemplateLayoutID_CMS_Layout]
ALTER TABLE [CMS_PageTemplate]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_PageTemplate_PageTemplateSiteID_CMS_Site]
	FOREIGN KEY ([PageTemplateSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [CMS_PageTemplate]
	CHECK CONSTRAINT [FK_CMS_PageTemplate_PageTemplateSiteID_CMS_Site]
