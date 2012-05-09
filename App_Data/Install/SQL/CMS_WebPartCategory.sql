CREATE TABLE [CMS_WebPartCategory] (
		[CategoryID]                    [int] IDENTITY(1, 1) NOT NULL,
		[CategoryDisplayName]           [nvarchar](100) NOT NULL,
		[CategoryParentID]              [int] NULL,
		[CategoryName]                  [nvarchar](100) NOT NULL,
		[CategoryGUID]                  [uniqueidentifier] NOT NULL,
		[CategoryLastModified]          [datetime] NOT NULL,
		[CategoryImagePath]             [nvarchar](450) NULL,
		[CategoryPath]                  [nvarchar](450) NOT NULL,
		[CategoryOrder]                 [int] NULL,
		[CategoryLevel]                 [int] NULL,
		[CategoryChildCount]            [int] NULL,
		[CategoryWebPartChildCount]     [int] NULL
) 
ALTER TABLE [CMS_WebPartCategory]
	ADD
	CONSTRAINT [PK_CMS_WebPartCategory]
	PRIMARY KEY
	NONCLUSTERED
	([CategoryID])
	
	
ALTER TABLE [CMS_WebPartCategory]
	ADD
	CONSTRAINT [DEFAULT_CMS_WebPartCategory_CategoryPath]
	DEFAULT ('') FOR [CategoryPath]
CREATE NONCLUSTERED INDEX [IX_CMS_WebPartCategory_CategoryParentID_CategoryOrder]
	ON [CMS_WebPartCategory] ([CategoryParentID], [CategoryOrder])
	
	
CREATE UNIQUE CLUSTERED INDEX [IX_CMS_WebPartCategory_CategoryPath]
	ON [CMS_WebPartCategory] ([CategoryPath])
	
	
ALTER TABLE [CMS_WebPartCategory]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_WebPartCategory_CategoryParentID_CMS_WebPartCategory]
	FOREIGN KEY ([CategoryParentID]) REFERENCES [CMS_WebPartCategory] ([CategoryID])
ALTER TABLE [CMS_WebPartCategory]
	CHECK CONSTRAINT [FK_CMS_WebPartCategory_CategoryParentID_CMS_WebPartCategory]
