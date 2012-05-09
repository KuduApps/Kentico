CREATE TABLE [Forums_ForumModerators] (
		[UserID]      [int] NOT NULL,
		[ForumID]     [int] NOT NULL
) 
ALTER TABLE [Forums_ForumModerators]
	ADD
	CONSTRAINT [PK_Forums_ForumModerators]
	PRIMARY KEY
	CLUSTERED
	([UserID], [ForumID])
	
	
ALTER TABLE [Forums_ForumModerators]
	WITH CHECK
	ADD CONSTRAINT [FK_Forums_ForumModerators_ForumID_Forums_Forum]
	FOREIGN KEY ([ForumID]) REFERENCES [Forums_Forum] ([ForumID])
ALTER TABLE [Forums_ForumModerators]
	CHECK CONSTRAINT [FK_Forums_ForumModerators_ForumID_Forums_Forum]
ALTER TABLE [Forums_ForumModerators]
	WITH CHECK
	ADD CONSTRAINT [FK_Forums_ForumModerators_UserID_CMS_User]
	FOREIGN KEY ([UserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [Forums_ForumModerators]
	CHECK CONSTRAINT [FK_Forums_ForumModerators_UserID_CMS_User]
