CREATE TABLE [Newsletter_Newsletter] (
		[NewsletterID]                           [int] IDENTITY(1, 1) NOT NULL,
		[NewsletterDisplayName]                  [nvarchar](250) NOT NULL,
		[NewsletterName]                         [nvarchar](250) NOT NULL,
		[NewsletterType]                         [nvarchar](5) NOT NULL,
		[NewsletterSubscriptionTemplateID]       [int] NOT NULL,
		[NewsletterUnsubscriptionTemplateID]     [int] NOT NULL,
		[NewsletterSenderName]                   [nvarchar](200) NOT NULL,
		[NewsletterSenderEmail]                  [nvarchar](200) NOT NULL,
		[NewsletterDynamicSubject]               [nvarchar](500) NULL,
		[NewsletterDynamicURL]                   [nvarchar](500) NULL,
		[NewsletterDynamicScheduledTaskID]       [int] NULL,
		[NewsletterTemplateID]                   [int] NULL,
		[NewsletterSiteID]                       [int] NOT NULL,
		[NewsletterGUID]                         [uniqueidentifier] NOT NULL,
		[NewsletterUnsubscribeUrl]               [nvarchar](1000) NULL,
		[NewsletterBaseUrl]                      [nvarchar](500) NULL,
		[NewsletterLastModified]                 [datetime] NOT NULL,
		[NewsletterUseEmailQueue]                [bit] NULL,
		[NewsletterEnableOptIn]                  [bit] NULL,
		[NewsletterOptInTemplateID]              [int] NULL,
		[NewsletterSendOptInConfirmation]        [bit] NULL,
		[NewsletterOptInApprovalURL]             [nvarchar](450) NULL,
		[NewsletterTrackOpenEmails]              [bit] NULL,
		[NewsletterTrackClickedLinks]            [bit] NULL,
		[NewsletterDraftEmails]                  [nvarchar](450) NULL,
		[NewsletterLogActivity]                  [bit] NULL
) 
ALTER TABLE [Newsletter_Newsletter]
	ADD
	CONSTRAINT [PK_Newsletter_Newsletter]
	PRIMARY KEY
	NONCLUSTERED
	([NewsletterID])
	
	
ALTER TABLE [Newsletter_Newsletter]
	ADD
	CONSTRAINT [DEFAULT_Newsletter_Newsletter_NewsletterEnableOptIn]
	DEFAULT ((0)) FOR [NewsletterEnableOptIn]
ALTER TABLE [Newsletter_Newsletter]
	ADD
	CONSTRAINT [DEFAULT_Newsletter_Newsletter_NewsletterLogActivity]
	DEFAULT ((1)) FOR [NewsletterLogActivity]
ALTER TABLE [Newsletter_Newsletter]
	ADD
	CONSTRAINT [DEFAULT_Newsletter_Newsletter_NewsletterSendOptInConfirmation]
	DEFAULT ((0)) FOR [NewsletterSendOptInConfirmation]
ALTER TABLE [Newsletter_Newsletter]
	ADD
	CONSTRAINT [DEFAULT_Newsletter_Newsletter_NewsletterTrackOpenEmails]
	DEFAULT ((0)) FOR [NewsletterTrackOpenEmails]
ALTER TABLE [Newsletter_Newsletter]
	ADD
	CONSTRAINT [DEFAULT_Newsletter_Newsletter_NewsletterUseEmailQueue]
	DEFAULT ((0)) FOR [NewsletterUseEmailQueue]
ALTER TABLE [Newsletter_Newsletter]
	ADD
	CONSTRAINT [DF_Newsletter_Newsletter_NewsletterTrackClickedLinks]
	DEFAULT ((0)) FOR [NewsletterTrackClickedLinks]
CREATE CLUSTERED INDEX [IX_Newsletter_Newsletter_NewsletterSiteID_NewsletterDisplayName]
	ON [Newsletter_Newsletter] ([NewsletterSiteID], [NewsletterDisplayName])
	
	
CREATE UNIQUE NONCLUSTERED INDEX [IX_Newsletter_Newsletter_NewsletterSiteID_NewsletterName]
	ON [Newsletter_Newsletter] ([NewsletterSiteID], [NewsletterName])
	
	
CREATE NONCLUSTERED INDEX [IX_Newsletter_Newsletter_NewsletterSubscriptionTemplateID]
	ON [Newsletter_Newsletter] ([NewsletterSubscriptionTemplateID])
	
CREATE NONCLUSTERED INDEX [IX_Newsletter_Newsletter_NewsletterTemplateID]
	ON [Newsletter_Newsletter] ([NewsletterTemplateID])
	
CREATE NONCLUSTERED INDEX [IX_Newsletter_Newsletter_NewsletterUnsubscriptionTemplateID]
	ON [Newsletter_Newsletter] ([NewsletterUnsubscriptionTemplateID])
	
ALTER TABLE [Newsletter_Newsletter]
	WITH NOCHECK
	ADD CONSTRAINT [FK_Newsletter_Newsletter_NewsletterOptInTemplateID_EmailTemplate]
	FOREIGN KEY ([NewsletterOptInTemplateID]) REFERENCES [Newsletter_EmailTemplate] ([TemplateID])
ALTER TABLE [Newsletter_Newsletter]
	CHECK CONSTRAINT [FK_Newsletter_Newsletter_NewsletterOptInTemplateID_EmailTemplate]
ALTER TABLE [Newsletter_Newsletter]
	WITH CHECK
	ADD CONSTRAINT [FK_Newsletter_Newsletter_NewsletterSiteID_CMS_Site]
	FOREIGN KEY ([NewsletterSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [Newsletter_Newsletter]
	CHECK CONSTRAINT [FK_Newsletter_Newsletter_NewsletterSiteID_CMS_Site]
ALTER TABLE [Newsletter_Newsletter]
	WITH CHECK
	ADD CONSTRAINT [FK_Newsletter_Newsletter_NewsletterSubscriptionTemplateID_Newsletter_EmailTemplate]
	FOREIGN KEY ([NewsletterSubscriptionTemplateID]) REFERENCES [Newsletter_EmailTemplate] ([TemplateID])
ALTER TABLE [Newsletter_Newsletter]
	CHECK CONSTRAINT [FK_Newsletter_Newsletter_NewsletterSubscriptionTemplateID_Newsletter_EmailTemplate]
ALTER TABLE [Newsletter_Newsletter]
	WITH CHECK
	ADD CONSTRAINT [FK_Newsletter_Newsletter_NewsletterTemplateID_Newsletter_EmailTemplate]
	FOREIGN KEY ([NewsletterTemplateID]) REFERENCES [Newsletter_EmailTemplate] ([TemplateID])
ALTER TABLE [Newsletter_Newsletter]
	CHECK CONSTRAINT [FK_Newsletter_Newsletter_NewsletterTemplateID_Newsletter_EmailTemplate]
ALTER TABLE [Newsletter_Newsletter]
	WITH CHECK
	ADD CONSTRAINT [FK_Newsletter_Newsletter_NewsletterUnsubscriptionTemplateID_Newsletter_EmailTemplate]
	FOREIGN KEY ([NewsletterUnsubscriptionTemplateID]) REFERENCES [Newsletter_EmailTemplate] ([TemplateID])
ALTER TABLE [Newsletter_Newsletter]
	CHECK CONSTRAINT [FK_Newsletter_Newsletter_NewsletterUnsubscriptionTemplateID_Newsletter_EmailTemplate]
