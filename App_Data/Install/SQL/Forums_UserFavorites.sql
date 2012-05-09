CREATE TABLE [Forums_UserFavorites] (
		[FavoriteID]               [int] IDENTITY(1, 1) NOT NULL,
		[UserID]                   [int] NOT NULL,
		[PostID]                   [int] NULL,
		[ForumID]                  [int] NULL,
		[FavoriteName]             [nvarchar](100) NULL,
		[SiteID]                   [int] NOT NULL,
		[FavoriteGUID]             [uniqueidentifier] NOT NULL,
		[FavoriteLastModified]     [datetime] NOT NULL
) 
ALTER TABLE [Forums_UserFavorites]
	ADD
	CONSTRAINT [PK_Forums_UserFavorites]
	PRIMARY KEY
	CLUSTERED
	([FavoriteID])
	
	
ALTER TABLE [Forums_UserFavorites]
	ADD
	CONSTRAINT [DEFAULT_Forums_UserFavorites_FavoriteGUID]
	DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [FavoriteGUID]
ALTER TABLE [Forums_UserFavorites]
	ADD
	CONSTRAINT [DEFAULT_Forums_UserFavorites_FavoriteLastModified]
	DEFAULT ('12/4/2008 3:23:57 PM') FOR [FavoriteLastModified]
ALTER TABLE [Forums_UserFavorites]
	ADD
	CONSTRAINT [DEFAULT_Forums_UserFavorites_SiteID]
	DEFAULT ((0)) FOR [SiteID]
CREATE NONCLUSTERED INDEX [IX_Forums_UserFavorites_ForumID]
	ON [Forums_UserFavorites] ([ForumID])
	
CREATE NONCLUSTERED INDEX [IX_Forums_UserFavorites_PostID]
	ON [Forums_UserFavorites] ([PostID])
	
CREATE NONCLUSTERED INDEX [IX_Forums_UserFavorites_SiteID]
	ON [Forums_UserFavorites] ([SiteID])
	
CREATE NONCLUSTERED INDEX [IX_Forums_UserFavorites_UserID]
	ON [Forums_UserFavorites] ([UserID])
	
ALTER TABLE [Forums_UserFavorites]
	WITH CHECK
	ADD CONSTRAINT [FK_Forums_UserFavorites_ForumID_Forums_Forum]
	FOREIGN KEY ([ForumID]) REFERENCES [Forums_Forum] ([ForumID])
ALTER TABLE [Forums_UserFavorites]
	CHECK CONSTRAINT [FK_Forums_UserFavorites_ForumID_Forums_Forum]
ALTER TABLE [Forums_UserFavorites]
	WITH CHECK
	ADD CONSTRAINT [FK_Forums_UserFavorites_PostID_Forums_ForumPost]
	FOREIGN KEY ([PostID]) REFERENCES [Forums_ForumPost] ([PostId])
ALTER TABLE [Forums_UserFavorites]
	CHECK CONSTRAINT [FK_Forums_UserFavorites_PostID_Forums_ForumPost]
ALTER TABLE [Forums_UserFavorites]
	WITH CHECK
	ADD CONSTRAINT [FK_Forums_UserFavorites_SiteID_CMS_Site]
	FOREIGN KEY ([SiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [Forums_UserFavorites]
	CHECK CONSTRAINT [FK_Forums_UserFavorites_SiteID_CMS_Site]
ALTER TABLE [Forums_UserFavorites]
	WITH CHECK
	ADD CONSTRAINT [FK_Forums_UserFavorites_UserID_CMS_User]
	FOREIGN KEY ([UserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [Forums_UserFavorites]
	CHECK CONSTRAINT [FK_Forums_UserFavorites_UserID_CMS_User]
