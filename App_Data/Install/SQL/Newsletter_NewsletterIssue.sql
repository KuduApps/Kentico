CREATE TABLE [Newsletter_NewsletterIssue] (
		[IssueID]                          [int] IDENTITY(1, 1) NOT NULL,
		[IssueSubject]                     [nvarchar](450) NOT NULL,
		[IssueText]                        [nvarchar](max) NOT NULL,
		[IssueUnsubscribed]                [int] NOT NULL,
		[IssueNewsletterID]                [int] NOT NULL,
		[IssueTemplateID]                  [int] NULL,
		[IssueSentEmails]                  [int] NOT NULL,
		[IssueMailoutTime]                 [datetime] NULL,
		[IssueShowInNewsletterArchive]     [bit] NULL,
		[IssueGUID]                        [uniqueidentifier] NOT NULL,
		[IssueLastModified]                [datetime] NOT NULL,
		[IssueSiteID]                      [int] NOT NULL,
		[IssueOpenedEmails]                [int] NULL,
		[IssueBounces]                     [int] NULL
)  
ALTER TABLE [Newsletter_NewsletterIssue]
	ADD
	CONSTRAINT [PK_Newsletter_NewsletterIssue]
	PRIMARY KEY
	CLUSTERED
	([IssueID])
	
	
ALTER TABLE [Newsletter_NewsletterIssue]
	ADD
	CONSTRAINT [DEFAULT_Newsletter_NewsletterIssue_IssueSiteID]
	DEFAULT ((0)) FOR [IssueSiteID]
ALTER TABLE [Newsletter_NewsletterIssue]
	ADD
	CONSTRAINT [DEFAULT_Newsletter_NewsletterIssue_IssueSubject]
	DEFAULT ('') FOR [IssueSubject]
CREATE NONCLUSTERED INDEX [IX_Newsletter_NewsletterIssue_IssueNewsletterID]
	ON [Newsletter_NewsletterIssue] ([IssueNewsletterID])
	
CREATE NONCLUSTERED INDEX [IX_Newsletter_NewsletterIssue_IssueSiteID]
	ON [Newsletter_NewsletterIssue] ([IssueSiteID])
	
CREATE NONCLUSTERED INDEX [IX_Newsletter_NewsletterIssue_IssueTemplateID]
	ON [Newsletter_NewsletterIssue] ([IssueTemplateID])
	
CREATE NONCLUSTERED INDEX [IX_Newslettes_NewsletterIssue_IssueShowInNewsletterArchive]
	ON [Newsletter_NewsletterIssue] ([IssueShowInNewsletterArchive])
	
	
ALTER TABLE [Newsletter_NewsletterIssue]
	WITH CHECK
	ADD CONSTRAINT [FK_Newsletter_NewsletterIssue_IssueNewsletterID_Newsletter_Newsletter]
	FOREIGN KEY ([IssueNewsletterID]) REFERENCES [Newsletter_Newsletter] ([NewsletterID])
ALTER TABLE [Newsletter_NewsletterIssue]
	CHECK CONSTRAINT [FK_Newsletter_NewsletterIssue_IssueNewsletterID_Newsletter_Newsletter]
ALTER TABLE [Newsletter_NewsletterIssue]
	WITH CHECK
	ADD CONSTRAINT [FK_Newsletter_NewsletterIssue_IssueSiteID_CMS_Site]
	FOREIGN KEY ([IssueSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [Newsletter_NewsletterIssue]
	CHECK CONSTRAINT [FK_Newsletter_NewsletterIssue_IssueSiteID_CMS_Site]
ALTER TABLE [Newsletter_NewsletterIssue]
	WITH CHECK
	ADD CONSTRAINT [FK_Newsletter_NewsletterIssue_IssueTemplateID_Newsletter_EmailTemplate]
	FOREIGN KEY ([IssueTemplateID]) REFERENCES [Newsletter_EmailTemplate] ([TemplateID])
ALTER TABLE [Newsletter_NewsletterIssue]
	CHECK CONSTRAINT [FK_Newsletter_NewsletterIssue_IssueTemplateID_Newsletter_EmailTemplate]
