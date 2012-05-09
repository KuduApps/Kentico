CREATE TABLE [Forums_ForumSubscription] (
		[SubscriptionID]               [int] IDENTITY(1, 1) NOT NULL,
		[SubscriptionUserID]           [int] NULL,
		[SubscriptionEmail]            [nvarchar](100) NULL,
		[SubscriptionForumID]          [int] NOT NULL,
		[SubscriptionPostID]           [int] NULL,
		[SubscriptionGUID]             [uniqueidentifier] NOT NULL,
		[SubscriptionLastModified]     [datetime] NOT NULL
) 
ALTER TABLE [Forums_ForumSubscription]
	ADD
	CONSTRAINT [PK_Forums_ForumSubscription]
	PRIMARY KEY
	NONCLUSTERED
	([SubscriptionID])
	
	
CREATE CLUSTERED INDEX [IX_Forums_ForumSubscription_SubscriptionForumID_SubscriptionEmail]
	ON [Forums_ForumSubscription] ([SubscriptionEmail], [SubscriptionForumID])
	
	
CREATE NONCLUSTERED INDEX [IX_Forums_ForumSubscription_SubscriptionPostID]
	ON [Forums_ForumSubscription] ([SubscriptionPostID])
	
CREATE NONCLUSTERED INDEX [IX_Forums_ForumSubscription_SubscriptionUserID]
	ON [Forums_ForumSubscription] ([SubscriptionUserID])
	
ALTER TABLE [Forums_ForumSubscription]
	WITH CHECK
	ADD CONSTRAINT [FK_Forums_ForumSubscription_SubscriptionForumID_Forums_Forum]
	FOREIGN KEY ([SubscriptionForumID]) REFERENCES [Forums_Forum] ([ForumID])
ALTER TABLE [Forums_ForumSubscription]
	CHECK CONSTRAINT [FK_Forums_ForumSubscription_SubscriptionForumID_Forums_Forum]
ALTER TABLE [Forums_ForumSubscription]
	WITH CHECK
	ADD CONSTRAINT [FK_Forums_ForumSubscription_SubscriptionPostID_Forums_ForumPost]
	FOREIGN KEY ([SubscriptionPostID]) REFERENCES [Forums_ForumPost] ([PostId])
ALTER TABLE [Forums_ForumSubscription]
	CHECK CONSTRAINT [FK_Forums_ForumSubscription_SubscriptionPostID_Forums_ForumPost]
ALTER TABLE [Forums_ForumSubscription]
	WITH CHECK
	ADD CONSTRAINT [FK_Forums_ForumSubscription_SubscriptionUserID_CMS_User]
	FOREIGN KEY ([SubscriptionUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [Forums_ForumSubscription]
	CHECK CONSTRAINT [FK_Forums_ForumSubscription_SubscriptionUserID_CMS_User]
