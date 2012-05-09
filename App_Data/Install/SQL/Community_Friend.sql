CREATE TABLE [Community_Friend] (
		[FriendID]                  [int] IDENTITY(1, 1) NOT NULL,
		[FriendRequestedUserID]     [int] NOT NULL,
		[FriendUserID]              [int] NOT NULL,
		[FriendRequestedWhen]       [datetime] NOT NULL,
		[FriendComment]             [nvarchar](max) NULL,
		[FriendApprovedBy]          [int] NULL,
		[FriendApprovedWhen]        [datetime] NULL,
		[FriendRejectedBy]          [int] NULL,
		[FriendRejectedWhen]        [datetime] NULL,
		[FriendGUID]                [uniqueidentifier] NOT NULL,
		[FriendStatus]              [int] NOT NULL
)  
ALTER TABLE [Community_Friend]
	ADD
	CONSTRAINT [PK_Community_Friend]
	PRIMARY KEY
	CLUSTERED
	([FriendID])
	
	
ALTER TABLE [Community_Friend]
	ADD
	CONSTRAINT [DEFAULT_Community_Friend_FriendStatus]
	DEFAULT ((0)) FOR [FriendStatus]
CREATE NONCLUSTERED INDEX [IX_Community_Friend_FriendApprovedBy]
	ON [Community_Friend] ([FriendApprovedBy])
	
CREATE NONCLUSTERED INDEX [IX_Community_Friend_FriendRejectedBy]
	ON [Community_Friend] ([FriendRejectedBy])
	
CREATE NONCLUSTERED INDEX [IX_Community_Friend_FriendRequestedUserID_FriendStatus]
	ON [Community_Friend] ([FriendRequestedUserID], [FriendStatus])
	
	
CREATE NONCLUSTERED INDEX [IX_Community_Friend_FriendUserID_FriendStatus]
	ON [Community_Friend] ([FriendUserID], [FriendStatus])
	
	
ALTER TABLE [Community_Friend]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_Friend_FriendApprovedBy_CMS_User]
	FOREIGN KEY ([FriendApprovedBy]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [Community_Friend]
	CHECK CONSTRAINT [FK_CMS_Friend_FriendApprovedBy_CMS_User]
ALTER TABLE [Community_Friend]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_Friend_FriendRejectedBy_CMS_User]
	FOREIGN KEY ([FriendRejectedBy]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [Community_Friend]
	CHECK CONSTRAINT [FK_CMS_Friend_FriendRejectedBy_CMS_User]
ALTER TABLE [Community_Friend]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_Friend_FriendRequestedUserID_CMS_User]
	FOREIGN KEY ([FriendRequestedUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [Community_Friend]
	CHECK CONSTRAINT [FK_CMS_Friend_FriendRequestedUserID_CMS_User]
ALTER TABLE [Community_Friend]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_Friend_FriendUserID_CMS_User]
	FOREIGN KEY ([FriendUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [Community_Friend]
	CHECK CONSTRAINT [FK_CMS_Friend_FriendUserID_CMS_User]
