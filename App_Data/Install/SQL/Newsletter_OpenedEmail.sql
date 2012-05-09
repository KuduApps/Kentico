CREATE TABLE [Newsletter_OpenedEmail] (
		[SubscriberID]     [int] NOT NULL,
		[IssueID]          [int] NOT NULL,
		[OpenedWhen]       [datetime] NULL
) 
ALTER TABLE [Newsletter_OpenedEmail]
	ADD
	CONSTRAINT [PK__Newsletter_OpenedEmail]
	PRIMARY KEY
	CLUSTERED
	([SubscriberID], [IssueID])
	
ALTER TABLE [Newsletter_OpenedEmail]
	WITH CHECK
	ADD CONSTRAINT [FK_Newsletter_OpenedEmail_IssueID_Newsletter_NewsletterIssue]
	FOREIGN KEY ([IssueID]) REFERENCES [Newsletter_NewsletterIssue] ([IssueID])
ALTER TABLE [Newsletter_OpenedEmail]
	CHECK CONSTRAINT [FK_Newsletter_OpenedEmail_IssueID_Newsletter_NewsletterIssue]
ALTER TABLE [Newsletter_OpenedEmail]
	WITH CHECK
	ADD CONSTRAINT [FK_Newsletter_OpenedEmail_SubscriberID_Newsletter_Subscriber]
	FOREIGN KEY ([SubscriberID]) REFERENCES [Newsletter_Subscriber] ([SubscriberID])
ALTER TABLE [Newsletter_OpenedEmail]
	CHECK CONSTRAINT [FK_Newsletter_OpenedEmail_SubscriberID_Newsletter_Subscriber]
