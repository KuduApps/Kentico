CREATE TABLE [Newsletter_SubscriberLink] (
		[SubscriberID]     [int] NOT NULL,
		[LinkID]           [int] NOT NULL,
		[Clicks]           [int] NULL
) 
ALTER TABLE [Newsletter_SubscriberLink]
	ADD
	CONSTRAINT [PK_Newsletter_SubscriberLink]
	PRIMARY KEY
	CLUSTERED
	([SubscriberID], [LinkID])
	
ALTER TABLE [Newsletter_SubscriberLink]
	WITH CHECK
	ADD CONSTRAINT [FK_Newsletter_SubscriberLink_Newsletter_Link]
	FOREIGN KEY ([LinkID]) REFERENCES [Newsletter_Link] ([LinkID])
ALTER TABLE [Newsletter_SubscriberLink]
	CHECK CONSTRAINT [FK_Newsletter_SubscriberLink_Newsletter_Link]
ALTER TABLE [Newsletter_SubscriberLink]
	WITH CHECK
	ADD CONSTRAINT [FK_Newsletter_SubscriberLink_Newsletter_Subscriber]
	FOREIGN KEY ([SubscriberID]) REFERENCES [Newsletter_Subscriber] ([SubscriberID])
ALTER TABLE [Newsletter_SubscriberLink]
	CHECK CONSTRAINT [FK_Newsletter_SubscriberLink_Newsletter_Subscriber]
