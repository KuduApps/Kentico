CREATE TABLE [CMS_Email] (
		[EmailID]                  [int] IDENTITY(1, 1) NOT NULL,
		[EmailFrom]                [nvarchar](250) NOT NULL,
		[EmailTo]                  [nvarchar](max) NULL,
		[EmailCc]                  [nvarchar](max) NULL,
		[EmailBcc]                 [nvarchar](max) NULL,
		[EmailSubject]             [nvarchar](450) NOT NULL,
		[EmailBody]                [nvarchar](max) NULL,
		[EmailPlainTextBody]       [nvarchar](max) NULL,
		[EmailFormat]              [int] NOT NULL,
		[EmailPriority]            [int] NOT NULL,
		[EmailSiteID]              [int] NULL,
		[EmailLastSendResult]      [nvarchar](max) NULL,
		[EmailLastSendAttempt]     [datetime] NULL,
		[EmailGUID]                [uniqueidentifier] NOT NULL,
		[EmailLastModified]        [datetime] NOT NULL,
		[EmailStatus]              [int] NULL,
		[EmailIsMass]              [bit] NULL,
		[EmailSetName]             [nvarchar](250) NULL,
		[EmailSetRelatedID]        [int] NULL,
		[EmailReplyTo]             [nvarchar](250) NULL,
		[EmailHeaders]             [nvarchar](max) NULL
)  
ALTER TABLE [CMS_Email]
	ADD
	CONSTRAINT [PK_CMS_Email]
	PRIMARY KEY
	NONCLUSTERED
	([EmailID])
	
	
ALTER TABLE [CMS_Email]
	ADD
	CONSTRAINT [DEFAULT_CMS_Email_EmailIsMass]
	DEFAULT ((1)) FOR [EmailIsMass]
ALTER TABLE [CMS_Email]
	ADD
	CONSTRAINT [DEFAULT_CMS_Email_EmailSubject]
	DEFAULT ('') FOR [EmailSubject]
CREATE CLUSTERED INDEX [IX_CMS_Email_EmailPriority_EmailStatus]
	ON [CMS_Email] ([EmailPriority], [EmailStatus])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_Email_EmailSiteID_EmailStatus_EmailLastSendAttempt]
	ON [CMS_Email] ([EmailSiteID], [EmailStatus], [EmailLastSendAttempt])
	INCLUDE ([EmailID])
	
CREATE NONCLUSTERED INDEX [IX_CMS_Email_EmailStatus_EmailID_EmailPriority_EmailLastModified]
	ON [CMS_Email] ([EmailStatus], [EmailID])
	INCLUDE ([EmailPriority], [EmailLastModified])
	
