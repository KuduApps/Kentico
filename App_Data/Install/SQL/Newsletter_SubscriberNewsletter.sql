CREATE TABLE [Newsletter_SubscriberNewsletter] (
		[SubscriberID]                 [int] NOT NULL,
		[NewsletterID]                 [int] NOT NULL,
		[SubscribedWhen]               [datetime] NOT NULL,
		[SubscriptionApproved]         [bit] NULL,
		[SubscriptionApprovedWhen]     [datetime] NULL,
		[SubscriptionApprovalHash]     [nvarchar](100) NULL
) 
ALTER TABLE [Newsletter_SubscriberNewsletter]
	ADD
	CONSTRAINT [PK_Newsletter_SubscriberNewsletter]
	PRIMARY KEY
	CLUSTERED
	([SubscriberID], [NewsletterID])
	
	
ALTER TABLE [Newsletter_SubscriberNewsletter]
	ADD
	CONSTRAINT [DEFAULT_Newsletter_SubscriberNewsletter_SubscriptionApproved]
	DEFAULT ((1)) FOR [SubscriptionApproved]
ALTER TABLE [Newsletter_SubscriberNewsletter]
	WITH CHECK
	ADD CONSTRAINT [FK_Newsletter_SubscriberNewsletter_NewsletterID_Newsletter_Newsletter]
	FOREIGN KEY ([NewsletterID]) REFERENCES [Newsletter_Newsletter] ([NewsletterID])
ALTER TABLE [Newsletter_SubscriberNewsletter]
	CHECK CONSTRAINT [FK_Newsletter_SubscriberNewsletter_NewsletterID_Newsletter_Newsletter]
ALTER TABLE [Newsletter_SubscriberNewsletter]
	WITH CHECK
	ADD CONSTRAINT [FK_Newsletter_SubscriberNewsletter_SubscriberID_Newsletter_Subscriber]
	FOREIGN KEY ([SubscriberID]) REFERENCES [Newsletter_Subscriber] ([SubscriberID])
ALTER TABLE [Newsletter_SubscriberNewsletter]
	CHECK CONSTRAINT [FK_Newsletter_SubscriberNewsletter_SubscriberID_Newsletter_Subscriber]
