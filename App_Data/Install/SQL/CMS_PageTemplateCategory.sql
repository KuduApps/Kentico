CREATE TABLE [CMS_PageTemplateCategory] (
		[CategoryID]                     [int] IDENTITY(1, 1) NOT NULL,
		[CategoryDisplayName]            [nvarchar](100) NOT NULL,
		[CategoryParentID]               [int] NULL,
		[CategoryName]                   [nvarchar](100) NOT NULL,
		[CategoryGUID]                   [uniqueidentifier] NOT NULL,
		[CategoryLastModified]           [datetime] NOT NULL,
		[CategoryImagePath]              [nvarchar](450) NULL,
		[CategoryChildCount]             [int] NULL,
		[CategoryTemplateChildCount]     [int] NULL,
		[CategoryPath]                   [nvarchar](450) NULL,
		[CategoryOrder]                  [int] NULL,
		[CategoryLevel]                  [int] NULL
) 
ALTER TABLE [CMS_PageTemplateCategory]
	ADD
	CONSTRAINT [PK_CMS_PageTemplateCategory]
	PRIMARY KEY
	NONCLUSTERED
	([CategoryID])
	
	
ALTER TABLE [CMS_PageTemplateCategory]
	ADD
	CONSTRAINT [DF_CMS_PageTemplateCategory_CategoryChildCount]
	DEFAULT ((0)) FOR [CategoryChildCount]
ALTER TABLE [CMS_PageTemplateCategory]
	ADD
	CONSTRAINT [DF_CMS_PageTemplateCategory_CategoryTemplateChildCount]
	DEFAULT ((0)) FOR [CategoryTemplateChildCount]
CREATE NONCLUSTERED INDEX [IX_CMS_PageTemplateCategory_CategoryLevel]
	ON [CMS_PageTemplateCategory] ([CategoryLevel])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_PageTemplateCategory_CategoryParentID]
	ON [CMS_PageTemplateCategory] ([CategoryParentID])
	
CREATE UNIQUE CLUSTERED INDEX [IX_CMS_PageTemplateCategory_CategoryPath]
	ON [CMS_PageTemplateCategory] ([CategoryPath])
	
	
ALTER TABLE [CMS_PageTemplateCategory]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_PageTemplateCategory_CategoryParentID_CMS_PageTemplateCategory]
	FOREIGN KEY ([CategoryParentID]) REFERENCES [CMS_PageTemplateCategory] ([CategoryID])
ALTER TABLE [CMS_PageTemplateCategory]
	CHECK CONSTRAINT [FK_CMS_PageTemplateCategory_CategoryParentID_CMS_PageTemplateCategory]
