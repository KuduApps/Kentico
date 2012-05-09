CREATE TABLE [Newsletter_EmailTemplate] (
		[TemplateID]                 [int] IDENTITY(1, 1) NOT NULL,
		[TemplateDisplayName]        [nvarchar](250) NOT NULL,
		[TemplateName]               [nvarchar](250) NOT NULL,
		[TemplateBody]               [nvarchar](max) NOT NULL,
		[TemplateSiteID]             [int] NOT NULL,
		[TemplateHeader]             [nvarchar](max) NOT NULL,
		[TemplateFooter]             [nvarchar](max) NOT NULL,
		[TemplateType]               [nvarchar](50) NOT NULL,
		[TemplateStylesheetText]     [nvarchar](max) NULL,
		[TemplateGUID]               [uniqueidentifier] NOT NULL,
		[TemplateLastModified]       [datetime] NOT NULL,
		[TemplateSubject]            [nvarchar](450) NULL
)  
ALTER TABLE [Newsletter_EmailTemplate]
	ADD
	CONSTRAINT [PK_Newsletter_EmailTemplate]
	PRIMARY KEY
	NONCLUSTERED
	([TemplateID])
	
	
CREATE CLUSTERED INDEX [IX_Newsletter_EmailTemplate_TemplateSiteID_TemplateDisplayName]
	ON [Newsletter_EmailTemplate] ([TemplateSiteID], [TemplateDisplayName])
	
	
CREATE UNIQUE NONCLUSTERED INDEX [IX_Newsletter_EmailTemplate_TemplateSiteID_TemplateName]
	ON [Newsletter_EmailTemplate] ([TemplateSiteID], [TemplateName])
	
	
ALTER TABLE [Newsletter_EmailTemplate]
	WITH CHECK
	ADD CONSTRAINT [FK_Newsletter_EmailTemplate_TemplateSiteID_CMS_Site]
	FOREIGN KEY ([TemplateSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [Newsletter_EmailTemplate]
	CHECK CONSTRAINT [FK_Newsletter_EmailTemplate_TemplateSiteID_CMS_Site]
