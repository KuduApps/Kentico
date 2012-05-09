CREATE TABLE [Notification_Subscription] (
		[SubscriptionID]                   [int] IDENTITY(1, 1) NOT NULL,
		[SubscriptionGatewayID]            [int] NOT NULL,
		[SubscriptionTemplateID]           [int] NOT NULL,
		[SubscriptionEventSource]          [nvarchar](100) NULL,
		[SubscriptionEventCode]            [nvarchar](100) NULL,
		[SubscriptionEventDisplayName]     [nvarchar](250) NOT NULL,
		[SubscriptionEventObjectID]        [int] NULL,
		[SubscriptionTime]                 [datetime] NOT NULL,
		[SubscriptionUserID]               [int] NOT NULL,
		[SubscriptionTarget]               [nvarchar](250) NOT NULL,
		[SubscriptionLastModified]         [datetime] NOT NULL,
		[SubscriptionGUID]                 [uniqueidentifier] NOT NULL,
		[SubscriptionEventData1]           [nvarchar](max) NULL,
		[SubscriptionEventData2]           [nvarchar](max) NULL,
		[SubscriptionUseHTML]              [bit] NULL,
		[SubscriptionSiteID]               [int] NULL
)  
ALTER TABLE [Notification_Subscription]
	ADD
	CONSTRAINT [PK_Notification_Subscription]
	PRIMARY KEY
	CLUSTERED
	([SubscriptionID])
	
	
ALTER TABLE [Notification_Subscription]
	ADD
	CONSTRAINT [DEFAULT_Notification_Subscription_SubscriptionEventDisplayName]
	DEFAULT ('') FOR [SubscriptionEventDisplayName]
ALTER TABLE [Notification_Subscription]
	ADD
	CONSTRAINT [DEFAULT_Notification_Subscription_SubscriptionSiteID]
	DEFAULT ((0)) FOR [SubscriptionSiteID]
ALTER TABLE [Notification_Subscription]
	ADD
	CONSTRAINT [DEFAULT_Notification_Subscription_SubscriptionUseHTML]
	DEFAULT ((0)) FOR [SubscriptionUseHTML]
CREATE NONCLUSTERED INDEX [IX_Notification_Subscription_SubscriptionEventSource_SubscriptionEventCode_SubscriptionEventObjectID]
	ON [Notification_Subscription] ([SubscriptionEventSource], [SubscriptionEventCode], [SubscriptionEventObjectID])
	
	
CREATE NONCLUSTERED INDEX [IX_Notification_Subscription_SubscriptionGatewayID]
	ON [Notification_Subscription] ([SubscriptionGatewayID])
	
CREATE NONCLUSTERED INDEX [IX_Notification_Subscription_SubscriptionSiteID]
	ON [Notification_Subscription] ([SubscriptionSiteID])
	
CREATE NONCLUSTERED INDEX [IX_Notification_Subscription_SubscriptionTemplateID]
	ON [Notification_Subscription] ([SubscriptionTemplateID])
	
CREATE NONCLUSTERED INDEX [IX_Notification_Subscription_SubscriptionUserID]
	ON [Notification_Subscription] ([SubscriptionUserID])
	
ALTER TABLE [Notification_Subscription]
	WITH CHECK
	ADD CONSTRAINT [FK_Notification_Subscription_SubscriptionGatewayID_Notification_Gateway]
	FOREIGN KEY ([SubscriptionGatewayID]) REFERENCES [Notification_Gateway] ([GatewayID])
ALTER TABLE [Notification_Subscription]
	CHECK CONSTRAINT [FK_Notification_Subscription_SubscriptionGatewayID_Notification_Gateway]
ALTER TABLE [Notification_Subscription]
	WITH CHECK
	ADD CONSTRAINT [FK_Notification_Subscription_SubscriptionSiteID_CMS_Site]
	FOREIGN KEY ([SubscriptionSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [Notification_Subscription]
	CHECK CONSTRAINT [FK_Notification_Subscription_SubscriptionSiteID_CMS_Site]
ALTER TABLE [Notification_Subscription]
	WITH CHECK
	ADD CONSTRAINT [FK_Notification_Subscription_SubscriptionTemplateID_Notification_Template]
	FOREIGN KEY ([SubscriptionTemplateID]) REFERENCES [Notification_Template] ([TemplateID])
ALTER TABLE [Notification_Subscription]
	CHECK CONSTRAINT [FK_Notification_Subscription_SubscriptionTemplateID_Notification_Template]
ALTER TABLE [Notification_Subscription]
	WITH CHECK
	ADD CONSTRAINT [FK_Notification_Subscription_SubscriptionUserID_CMS_User]
	FOREIGN KEY ([SubscriptionUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [Notification_Subscription]
	CHECK CONSTRAINT [FK_Notification_Subscription_SubscriptionUserID_CMS_User]
