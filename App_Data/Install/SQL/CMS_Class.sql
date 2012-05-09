CREATE TABLE [CMS_Class] (
		[ClassID]                           [int] IDENTITY(1, 1) NOT NULL,
		[ClassDisplayName]                  [nvarchar](100) NOT NULL,
		[ClassName]                         [nvarchar](100) NOT NULL,
		[ClassUsesVersioning]               [bit] NOT NULL,
		[ClassIsDocumentType]               [bit] NOT NULL,
		[ClassIsCoupledClass]               [bit] NOT NULL,
		[ClassXmlSchema]                    [nvarchar](max) NOT NULL,
		[ClassFormDefinition]               [nvarchar](max) NOT NULL,
		[ClassEditingPageUrl]               [nvarchar](200) NOT NULL,
		[ClassListPageUrl]                  [nvarchar](200) NOT NULL,
		[ClassNodeNameSource]               [nvarchar](100) NOT NULL,
		[ClassTableName]                    [nvarchar](100) NOT NULL,
		[ClassViewPageUrl]                  [nvarchar](200) NULL,
		[ClassPreviewPageUrl]               [nvarchar](200) NULL,
		[ClassFormLayout]                   [nvarchar](max) NULL,
		[ClassNewPageUrl]                   [nvarchar](200) NULL,
		[ClassShowAsSystemTable]            [bit] NOT NULL,
		[ClassUsePublishFromTo]             [bit] NULL,
		[ClassShowTemplateSelection]        [bit] NULL,
		[ClassSKUMappings]                  [nvarchar](max) NULL,
		[ClassIsMenuItemType]               [bit] NULL,
		[ClassNodeAliasSource]              [nvarchar](100) NULL,
		[ClassDefaultPageTemplateID]        [int] NULL,
		[ClassLastModified]                 [datetime] NOT NULL,
		[ClassGUID]                         [uniqueidentifier] NOT NULL,
		[ClassCreateSKU]                    [bit] NULL,
		[ClassIsProduct]                    [bit] NULL,
		[ClassIsCustomTable]                [bit] NOT NULL,
		[ClassShowColumns]                  [nvarchar](1000) NULL,
		[ClassLoadGeneration]               [int] NOT NULL,
		[ClassSearchTitleColumn]            [nvarchar](200) NULL,
		[ClassSearchContentColumn]          [nvarchar](200) NULL,
		[ClassSearchImageColumn]            [nvarchar](200) NULL,
		[ClassSearchCreationDateColumn]     [nvarchar](200) NULL,
		[ClassSearchSettings]               [nvarchar](max) NULL,
		[ClassInheritsFromClassID]          [int] NULL,
		[ClassSearchEnabled]                [bit] NULL,
		[ClassSKUDefaultDepartmentName]     [nvarchar](200) NULL,
		[ClassSKUDefaultDepartmentID]       [int] NULL,
		[ClassContactMapping]               [nvarchar](max) NULL,
		[ClassContactOverwriteEnabled]      [bit] NULL,
		[ClassSKUDefaultProductType]        [nvarchar](50) NULL
)  
ALTER TABLE [CMS_Class]
	ADD
	CONSTRAINT [PK_CMS_Class]
	PRIMARY KEY
	NONCLUSTERED
	([ClassID])
	
	
ALTER TABLE [CMS_Class]
	ADD
	CONSTRAINT [DEFAULT_CMS_Class_ClassSearchEnabled]
	DEFAULT ((1)) FOR [ClassSearchEnabled]
CREATE NONCLUSTERED INDEX [IX_CMS_Class_ClassDefaultPageTemplateID]
	ON [CMS_Class] ([ClassDefaultPageTemplateID])
	
CREATE CLUSTERED INDEX [IX_CMS_Class_ClassID_ClassName_ClassDisplayName]
	ON [CMS_Class] ([ClassID], [ClassName], [ClassDisplayName])
	
CREATE NONCLUSTERED INDEX [IX_CMS_Class_ClassLoadGeneration]
	ON [CMS_Class] ([ClassLoadGeneration])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_Class_ClassName_ClassDisplayName_ClassID]
	ON [CMS_Class] ([ClassName], [ClassDisplayName], [ClassID])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_Class_ClassName_ClassGUID]
	ON [CMS_Class] ([ClassName], [ClassGUID])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_Class_ClassShowAsSystemTable_ClassIsCustomTable_ClassIsCoupledClass_ClassIsDocumentType]
	ON [CMS_Class] ([ClassShowAsSystemTable], [ClassIsCustomTable], [ClassIsCoupledClass], [ClassIsDocumentType])
	
	
ALTER TABLE [CMS_Class]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_Class_ClassDefaultPageTemplateID_CMS_PageTemplate]
	FOREIGN KEY ([ClassDefaultPageTemplateID]) REFERENCES [CMS_PageTemplate] ([PageTemplateID])
ALTER TABLE [CMS_Class]
	CHECK CONSTRAINT [FK_CMS_Class_ClassDefaultPageTemplateID_CMS_PageTemplate]
