CREATE TABLE [CMS_Badge] (
		[BadgeID]               [int] IDENTITY(1, 1) NOT NULL,
		[BadgeName]             [nvarchar](100) NOT NULL,
		[BadgeDisplayName]      [nvarchar](200) NOT NULL,
		[BadgeImageURL]         [nvarchar](200) NULL,
		[BadgeIsAutomatic]      [bit] NOT NULL,
		[BadgeTopLimit]         [int] NULL,
		[BadgeGUID]             [uniqueidentifier] NOT NULL,
		[BadgeLastModified]     [datetime] NOT NULL
) 
ALTER TABLE [CMS_Badge]
	ADD
	CONSTRAINT [PK_CMS_Badge]
	PRIMARY KEY
	NONCLUSTERED
	([BadgeID])
	
	
ALTER TABLE [CMS_Badge]
	ADD
	CONSTRAINT [DEFAULT_Community_Badge_BadgeDisplayName]
	DEFAULT ('') FOR [BadgeDisplayName]
ALTER TABLE [CMS_Badge]
	ADD
	CONSTRAINT [DEFAULT_Community_Badge_BadgeGUID]
	DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [BadgeGUID]
ALTER TABLE [CMS_Badge]
	ADD
	CONSTRAINT [DEFAULT_Community_Badge_BadgeIsAutomatic]
	DEFAULT ((0)) FOR [BadgeIsAutomatic]
ALTER TABLE [CMS_Badge]
	ADD
	CONSTRAINT [DEFAULT_Community_Badge_BadgeLastModified]
	DEFAULT ('9/25/2008 5:07:55 PM') FOR [BadgeLastModified]
ALTER TABLE [CMS_Badge]
	ADD
	CONSTRAINT [DEFAULT_Community_Badge_BadgeName]
	DEFAULT ('') FOR [BadgeName]
CREATE CLUSTERED INDEX [IX_CMS_Badge_BadgeTopLimit]
	ON [CMS_Badge] ([BadgeTopLimit] DESC)
	
	
