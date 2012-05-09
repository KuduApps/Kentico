CREATE TABLE [Messaging_Message] (
		[MessageID]                    [int] IDENTITY(1, 1) NOT NULL,
		[MessageSenderUserID]          [int] NULL,
		[MessageSenderNickName]        [nvarchar](200) NULL,
		[MessageRecipientUserID]       [int] NULL,
		[MessageRecipientNickName]     [nvarchar](200) NULL,
		[MessageSent]                  [datetime] NOT NULL,
		[MessageSubject]               [nvarchar](200) NULL,
		[MessageBody]                  [nvarchar](max) NOT NULL,
		[MessageRead]                  [datetime] NULL,
		[MessageSenderDeleted]         [bit] NULL,
		[MessageRecipientDeleted]      [bit] NULL,
		[MessageGUID]                  [uniqueidentifier] NOT NULL,
		[MessageLastModified]          [datetime] NOT NULL,
		[MessageIsRead]                [bit] NULL
)  
ALTER TABLE [Messaging_Message]
	ADD
	CONSTRAINT [PK_Messaging_Message]
	PRIMARY KEY
	NONCLUSTERED
	([MessageID])
	
	
CREATE CLUSTERED INDEX [IX_Messaging_Message_MessageRecipientUserID_MessageSent_MessageRecipientDeleted]
	ON [Messaging_Message] ([MessageRecipientUserID], [MessageSent] DESC, [MessageRecipientDeleted])
	
	
CREATE NONCLUSTERED INDEX [IX_Messaging_Message_MessageSenderUserID_MessageSent_MessageSenderDeleted]
	ON [Messaging_Message] ([MessageSenderUserID], [MessageSent], [MessageSenderDeleted])
	
	
ALTER TABLE [Messaging_Message]
	WITH CHECK
	ADD CONSTRAINT [FK_Messaging_Message_MessageRecipientUserID_CMS_User]
	FOREIGN KEY ([MessageRecipientUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [Messaging_Message]
	CHECK CONSTRAINT [FK_Messaging_Message_MessageRecipientUserID_CMS_User]
ALTER TABLE [Messaging_Message]
	WITH CHECK
	ADD CONSTRAINT [FK_Messaging_Message_MessageSenderUserID_CMS_User]
	FOREIGN KEY ([MessageSenderUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [Messaging_Message]
	CHECK CONSTRAINT [FK_Messaging_Message_MessageSenderUserID_CMS_User]
