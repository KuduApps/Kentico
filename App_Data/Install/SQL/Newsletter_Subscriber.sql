CREATE TABLE [Newsletter_Subscriber] (
		[SubscriberID]               [int] IDENTITY(1, 1) NOT NULL,
		[SubscriberEmail]            [nvarchar](400) NULL,
		[SubscriberFirstName]        [nvarchar](200) NULL,
		[SubscriberLastName]         [nvarchar](200) NULL,
		[SubscriberSiteID]           [int] NOT NULL,
		[SubscriberGUID]             [uniqueidentifier] NOT NULL,
		[SubscriberCustomData]       [nvarchar](max) NULL,
		[SubscriberType]             [nvarchar](100) NULL,
		[SubscriberRelatedID]        [int] NULL,
		[SubscriberLastModified]     [datetime] NOT NULL,
		[SubscriberFullName]         [nvarchar](440) NULL,
		[SubscriberBounces]          [int] NULL
)  
ALTER TABLE [Newsletter_Subscriber]
	ADD
	CONSTRAINT [PK_Newsletter_Subscriber]
	PRIMARY KEY
	NONCLUSTERED
	([SubscriberID])
	
	
ALTER TABLE [Newsletter_Subscriber]
	ADD
	CONSTRAINT [DEFAULT_Newsletter_Subscriber_SubscriberGUID]
	DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [SubscriberGUID]
CREATE CLUSTERED INDEX [IX_Newsletter_Subscriber_SubscriberSiteID_SubscriberFullName]
	ON [Newsletter_Subscriber] ([SubscriberSiteID], [SubscriberFullName])
	
	
CREATE NONCLUSTERED INDEX [IX_Newsletter_Subscriber_SubscriberType_SubscriberRelatedID]
	ON [Newsletter_Subscriber] ([SubscriberType], [SubscriberRelatedID])
	
	
ALTER TABLE [Newsletter_Subscriber]
	WITH CHECK
	ADD CONSTRAINT [FK_Newsletter_Subscriber_SubscriberSiteID_CMS_Site]
	FOREIGN KEY ([SubscriberSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [Newsletter_Subscriber]
	CHECK CONSTRAINT [FK_Newsletter_Subscriber_SubscriberSiteID_CMS_Site]
