CREATE TABLE [Board_Message] (
		[MessageID]                   [int] IDENTITY(1, 1) NOT NULL,
		[MessageUserName]             [nvarchar](250) NOT NULL,
		[MessageText]                 [nvarchar](max) NOT NULL,
		[MessageEmail]                [nvarchar](250) NOT NULL,
		[MessageURL]                  [nvarchar](450) NOT NULL,
		[MessageIsSpam]               [bit] NOT NULL,
		[MessageBoardID]              [int] NOT NULL,
		[MessageApproved]             [bit] NOT NULL,
		[MessageApprovedByUserID]     [int] NULL,
		[MessageUserID]               [int] NULL,
		[MessageUserInfo]             [nvarchar](max) NOT NULL,
		[MessageAvatarGUID]           [uniqueidentifier] NULL,
		[MessageInserted]             [datetime] NOT NULL,
		[MessageLastModified]         [datetime] NOT NULL,
		[MessageGUID]                 [uniqueidentifier] NOT NULL,
		[MessageRatingValue]          [float] NULL
)  
ALTER TABLE [Board_Message]
	ADD
	CONSTRAINT [PK_Board_Message]
	PRIMARY KEY
	NONCLUSTERED
	([MessageID])
	
	
ALTER TABLE [Board_Message]
	ADD
	CONSTRAINT [DEFAULT_Board_Message_MessageApproved]
	DEFAULT ((0)) FOR [MessageApproved]
ALTER TABLE [Board_Message]
	ADD
	CONSTRAINT [DEFAULT_Board_Message_MessageBoardID]
	DEFAULT ((0)) FOR [MessageBoardID]
ALTER TABLE [Board_Message]
	ADD
	CONSTRAINT [DEFAULT_Board_Message_MessageEmail]
	DEFAULT ('') FOR [MessageEmail]
ALTER TABLE [Board_Message]
	ADD
	CONSTRAINT [DEFAULT_Board_Message_MessageGUID]
	DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [MessageGUID]
ALTER TABLE [Board_Message]
	ADD
	CONSTRAINT [DEFAULT_Board_Message_MessageInserted]
	DEFAULT ('8/26/2008 12:14:50 PM') FOR [MessageInserted]
ALTER TABLE [Board_Message]
	ADD
	CONSTRAINT [DEFAULT_Board_Message_MessageIsSpam]
	DEFAULT ((0)) FOR [MessageIsSpam]
ALTER TABLE [Board_Message]
	ADD
	CONSTRAINT [DEFAULT_Board_Message_MessageLastModified]
	DEFAULT ('8/26/2008 12:15:04 PM') FOR [MessageLastModified]
ALTER TABLE [Board_Message]
	ADD
	CONSTRAINT [DEFAULT_Board_Message_MessageText]
	DEFAULT ('') FOR [MessageText]
ALTER TABLE [Board_Message]
	ADD
	CONSTRAINT [DEFAULT_Board_Message_MessageURL]
	DEFAULT ('') FOR [MessageURL]
ALTER TABLE [Board_Message]
	ADD
	CONSTRAINT [DEFAULT_Board_Message_MessageUserInfo]
	DEFAULT ('') FOR [MessageUserInfo]
ALTER TABLE [Board_Message]
	ADD
	CONSTRAINT [DEFAULT_Board_Message_MessageUserName]
	DEFAULT ('') FOR [MessageUserName]
CREATE NONCLUSTERED INDEX [IX_Board_Message_MessageApproved_MessageIsSpam]
	ON [Board_Message] ([MessageApproved], [MessageIsSpam])
	
	
CREATE NONCLUSTERED INDEX [IX_Board_Message_MessageApprovedByUserID]
	ON [Board_Message] ([MessageApprovedByUserID])
	
CREATE UNIQUE NONCLUSTERED INDEX [IX_Board_Message_MessageBoardID_MessageGUID]
	ON [Board_Message] ([MessageBoardID], [MessageGUID])
	
	
CREATE CLUSTERED INDEX [IX_Board_Message_MessageInserted]
	ON [Board_Message] ([MessageInserted] DESC)
	
	
CREATE NONCLUSTERED INDEX [IX_Board_Message_MessageUserID]
	ON [Board_Message] ([MessageUserID])
	
ALTER TABLE [Board_Message]
	WITH CHECK
	ADD CONSTRAINT [FK_Board_Message_MessageApprovedByUserID_CMS_User]
	FOREIGN KEY ([MessageApprovedByUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [Board_Message]
	CHECK CONSTRAINT [FK_Board_Message_MessageApprovedByUserID_CMS_User]
ALTER TABLE [Board_Message]
	WITH CHECK
	ADD CONSTRAINT [FK_Board_Message_MessageBoardID_Board_Board]
	FOREIGN KEY ([MessageBoardID]) REFERENCES [Board_Board] ([BoardID])
ALTER TABLE [Board_Message]
	CHECK CONSTRAINT [FK_Board_Message_MessageBoardID_Board_Board]
ALTER TABLE [Board_Message]
	WITH CHECK
	ADD CONSTRAINT [FK_Board_Message_MessageUserID_CMS_User]
	FOREIGN KEY ([MessageUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [Board_Message]
	CHECK CONSTRAINT [FK_Board_Message_MessageUserID_CMS_User]
