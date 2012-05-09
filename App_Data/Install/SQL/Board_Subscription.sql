CREATE TABLE [Board_Subscription] (
		[SubscriptionID]               [int] IDENTITY(1, 1) NOT NULL,
		[SubscriptionBoardID]          [int] NOT NULL,
		[SubscriptionUserID]           [int] NULL,
		[SubscriptionEmail]            [nvarchar](250) NOT NULL,
		[SubscriptionLastModified]     [datetime] NOT NULL,
		[SubscriptionGUID]             [uniqueidentifier] NOT NULL
) 
ALTER TABLE [Board_Subscription]
	ADD
	CONSTRAINT [PK_Board_Subscription]
	PRIMARY KEY
	CLUSTERED
	([SubscriptionID])
	
	
CREATE NONCLUSTERED INDEX [IX_Board_Subscription_SubscriptionBoardID]
	ON [Board_Subscription] ([SubscriptionBoardID])
	
CREATE NONCLUSTERED INDEX [IX_Board_Subscription_SubscriptionUserID]
	ON [Board_Subscription] ([SubscriptionUserID])
	
ALTER TABLE [Board_Subscription]
	WITH CHECK
	ADD CONSTRAINT [FK_Board_Subscription_SubscriptionBoardID_Board_Board]
	FOREIGN KEY ([SubscriptionBoardID]) REFERENCES [Board_Board] ([BoardID])
ALTER TABLE [Board_Subscription]
	CHECK CONSTRAINT [FK_Board_Subscription_SubscriptionBoardID_Board_Board]
ALTER TABLE [Board_Subscription]
	WITH CHECK
	ADD CONSTRAINT [FK_Board_Subscription_SubscriptionUserID_CMS_User]
	FOREIGN KEY ([SubscriptionUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [Board_Subscription]
	CHECK CONSTRAINT [FK_Board_Subscription_SubscriptionUserID_CMS_User]
