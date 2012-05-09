CREATE TABLE [CMS_WidgetCategory] (
		[WidgetCategoryID]                   [int] IDENTITY(1, 1) NOT NULL,
		[WidgetCategoryName]                 [nvarchar](100) NOT NULL,
		[WidgetCategoryDisplayName]          [nvarchar](100) NOT NULL,
		[WidgetCategoryParentID]             [int] NULL,
		[WidgetCategoryPath]                 [nvarchar](450) NOT NULL,
		[WidgetCategoryLevel]                [int] NOT NULL,
		[WidgetCategoryOrder]                [int] NULL,
		[WidgetCategoryChildCount]           [int] NULL,
		[WidgetCategoryWidgetChildCount]     [int] NULL,
		[WidgetCategoryImagePath]            [nvarchar](450) NULL,
		[WidgetCategoryGUID]                 [uniqueidentifier] NOT NULL,
		[WidgetCategoryLastModified]         [datetime] NOT NULL
) 
ALTER TABLE [CMS_WidgetCategory]
	ADD
	CONSTRAINT [PK_CMS_WidgetCategory]
	PRIMARY KEY
	NONCLUSTERED
	([WidgetCategoryID])
	
	
CREATE UNIQUE CLUSTERED INDEX [IX_CMS_WidgetCategory_CategoryPath]
	ON [CMS_WidgetCategory] ([WidgetCategoryPath])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_WidgetCategory_WidgetCategoryID_WidgetCategoryOrder]
	ON [CMS_WidgetCategory] ([WidgetCategoryParentID], [WidgetCategoryOrder])
	
	
ALTER TABLE [CMS_WidgetCategory]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_WidgetCategory_WidgetCategoryParentID_CMS_WidgetCategory]
	FOREIGN KEY ([WidgetCategoryParentID]) REFERENCES [CMS_WidgetCategory] ([WidgetCategoryID])
ALTER TABLE [CMS_WidgetCategory]
	CHECK CONSTRAINT [FK_CMS_WidgetCategory_WidgetCategoryParentID_CMS_WidgetCategory]
