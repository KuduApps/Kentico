CREATE TABLE [Blog_PostSubscription] (
		[SubscriptionID]                 [int] IDENTITY(1, 1) NOT NULL,
		[SubscriptionPostDocumentID]     [int] NOT NULL,
		[SubscriptionUserID]             [int] NULL,
		[SubscriptionEmail]              [nvarchar](250) NULL,
		[SubscriptionLastModified]       [datetime] NOT NULL,
		[SubscriptionGUID]               [uniqueidentifier] NOT NULL
) 
ALTER TABLE [Blog_PostSubscription]
	ADD
	CONSTRAINT [PK_Blog_PostSubscription]
	PRIMARY KEY
	CLUSTERED
	([SubscriptionID])
	
	
CREATE NONCLUSTERED INDEX [IX_Blog_PostSubscription_SubscriptionPostDocumentID]
	ON [Blog_PostSubscription] ([SubscriptionPostDocumentID])
	
CREATE NONCLUSTERED INDEX [IX_Blog_PostSubscription_SubscriptionUserID]
	ON [Blog_PostSubscription] ([SubscriptionUserID])
	
ALTER TABLE [Blog_PostSubscription]
	WITH CHECK
	ADD CONSTRAINT [FK_Blog_PostSubscription_SubscriptionPostDocumentID_CMS_Document]
	FOREIGN KEY ([SubscriptionPostDocumentID]) REFERENCES [CMS_Document] ([DocumentID])
ALTER TABLE [Blog_PostSubscription]
	CHECK CONSTRAINT [FK_Blog_PostSubscription_SubscriptionPostDocumentID_CMS_Document]
ALTER TABLE [Blog_PostSubscription]
	WITH CHECK
	ADD CONSTRAINT [FK_Blog_PostSubscription_SubscriptionUserID_CMS_User]
	FOREIGN KEY ([SubscriptionUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [Blog_PostSubscription]
	CHECK CONSTRAINT [FK_Blog_PostSubscription_SubscriptionUserID_CMS_User]
