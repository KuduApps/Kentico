CREATE TABLE [CMS_WebTemplate] (
		[WebTemplateID]               [int] IDENTITY(1, 1) NOT NULL,
		[WebTemplateDisplayName]      [nvarchar](200) NOT NULL,
		[WebTemplateFileName]         [nvarchar](100) NOT NULL,
		[WebTemplateDescription]      [nvarchar](max) NOT NULL,
		[WebTemplateGUID]             [uniqueidentifier] NOT NULL,
		[WebTemplateLastModified]     [datetime] NOT NULL,
		[WebTemplateName]             [nvarchar](100) NOT NULL,
		[WebTemplateOrder]            [int] NOT NULL,
		[WebTemplateLicenses]         [nvarchar](200) NOT NULL,
		[WebTemplatePackages]         [nvarchar](200) NULL
)  
ALTER TABLE [CMS_WebTemplate]
	ADD
	CONSTRAINT [PK_CMS_WebTemplate]
	PRIMARY KEY
	NONCLUSTERED
	([WebTemplateID])
	
	
CREATE CLUSTERED INDEX [IX_CMS_WebTemplate_WebTemplateOrder]
	ON [CMS_WebTemplate] ([WebTemplateOrder])
	
	
