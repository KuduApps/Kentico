CREATE TABLE [CMS_EmailTemplate] (
		[EmailTemplateID]               [int] IDENTITY(1, 1) NOT NULL,
		[EmailTemplateName]             [nvarchar](200) NOT NULL,
		[EmailTemplateDisplayName]      [nvarchar](200) NOT NULL,
		[EmailTemplateText]             [nvarchar](max) NOT NULL,
		[EmailTemplateSiteID]           [int] NULL,
		[EmailTemplateGUID]             [uniqueidentifier] NOT NULL,
		[EmailTemplateLastModified]     [datetime] NOT NULL,
		[EmailTemplatePlainText]        [nvarchar](max) NULL,
		[EmailTemplateSubject]          [nvarchar](250) NULL,
		[EmailTemplateFrom]             [nvarchar](250) NULL,
		[EmailTemplateCc]               [nvarchar](max) NULL,
		[EmailTemplateBcc]              [nvarchar](max) NULL,
		[EmailTemplateType]             [nvarchar](100) NULL
)  
ALTER TABLE [CMS_EmailTemplate]
	ADD
	CONSTRAINT [PK_CMS_EmailTemplate]
	PRIMARY KEY
	NONCLUSTERED
	([EmailTemplateID])
	
	
CREATE CLUSTERED INDEX [IX_CMS_EmailTemplate_EmailTemplateDisplayName]
	ON [CMS_EmailTemplate] ([EmailTemplateDisplayName])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_EmailTemplate_EmailTemplateName_EmailTemplateSiteID]
	ON [CMS_EmailTemplate] ([EmailTemplateName], [EmailTemplateSiteID])
	
	
ALTER TABLE [CMS_EmailTemplate]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_Email_EmailTemplateSiteID_CMS_Site]
	FOREIGN KEY ([EmailTemplateSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [CMS_EmailTemplate]
	CHECK CONSTRAINT [FK_CMS_Email_EmailTemplateSiteID_CMS_Site]
