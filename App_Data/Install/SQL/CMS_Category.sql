CREATE TABLE [CMS_Category] (
		[CategoryID]               [int] IDENTITY(1, 1) NOT NULL,
		[CategoryDisplayName]      [nvarchar](250) NULL,
		[CategoryName]             [nvarchar](250) NULL,
		[CategoryDescription]      [nvarchar](max) NOT NULL,
		[CategoryCount]            [int] NOT NULL,
		[CategoryEnabled]          [bit] NOT NULL,
		[CategoryUserID]           [int] NULL,
		[CategoryGUID]             [uniqueidentifier] NOT NULL,
		[CategoryLastModified]     [datetime] NOT NULL,
		[CategorySiteID]           [int] NULL,
		[CategoryParentID]         [int] NULL,
		[CategoryIDPath]           [nvarchar](450) NULL,
		[CategoryNamePath]         [nvarchar](1500) NULL,
		[CategoryLevel]            [int] NULL,
		[CategoryOrder]            [int] NULL
)  
ALTER TABLE [CMS_Category]
	ADD
	CONSTRAINT [PK_CMS_Category]
	PRIMARY KEY
	NONCLUSTERED
	([CategoryID])
	
	
CREATE CLUSTERED INDEX [IX_CMS_Category_CategoryDisplayName_CategoryEnabled]
	ON [CMS_Category] ([CategoryDisplayName], [CategoryEnabled])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_Category_CategoryUserID]
	ON [CMS_Category] ([CategoryUserID])
	
ALTER TABLE [CMS_Category]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_Category_CategorySiteID_CMS_Site]
	FOREIGN KEY ([CategorySiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [CMS_Category]
	CHECK CONSTRAINT [FK_CMS_Category_CategorySiteID_CMS_Site]
ALTER TABLE [CMS_Category]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_Category_CategoryUserID_CMS_User]
	FOREIGN KEY ([CategoryUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [CMS_Category]
	CHECK CONSTRAINT [FK_CMS_Category_CategoryUserID_CMS_User]
