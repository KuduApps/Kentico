CREATE TABLE [CMS_SettingsCategory] (
		[CategoryID]              [int] IDENTITY(1, 1) NOT NULL,
		[CategoryDisplayName]     [nvarchar](200) NOT NULL,
		[CategoryOrder]           [int] NULL,
		[CategoryName]            [nvarchar](100) NOT NULL,
		[CategoryParentID]        [int] NULL,
		[CategoryIDPath]          [nvarchar](450) NULL,
		[CategoryLevel]           [int] NULL,
		[CategoryChildCount]      [int] NULL,
		[CategoryIconPath]        [nvarchar](200) NULL,
		[CategoryIsGroup]         [bit] NULL,
		[CategoryIsCustom]        [bit] NULL
) 
ALTER TABLE [CMS_SettingsCategory]
	ADD
	CONSTRAINT [PK_CMS_SettingsCategory]
	PRIMARY KEY
	NONCLUSTERED
	([CategoryID])
	
	
ALTER TABLE [CMS_SettingsCategory]
	ADD
	CONSTRAINT [DEFAULT_CMS_SettingsCategory_CategoryIsCustom]
	DEFAULT ((0)) FOR [CategoryIsCustom]
ALTER TABLE [CMS_SettingsCategory]
	ADD
	CONSTRAINT [DEFAULT_CMS_SettingsCategory_CategoryIsGroup]
	DEFAULT ((0)) FOR [CategoryIsGroup]
CREATE CLUSTERED INDEX [IX_CMS_SettingsCategory_CategoryOrder]
	ON [CMS_SettingsCategory] ([CategoryOrder])
	
	
ALTER TABLE [CMS_SettingsCategory]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_SettingsCategory_CMS_SettingsCategory1]
	FOREIGN KEY ([CategoryParentID]) REFERENCES [CMS_SettingsCategory] ([CategoryID])
ALTER TABLE [CMS_SettingsCategory]
	CHECK CONSTRAINT [FK_CMS_SettingsCategory_CMS_SettingsCategory1]
