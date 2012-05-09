CREATE TABLE [Newsletter_Link] (
		[LinkID]              [int] IDENTITY(1, 1) NOT NULL,
		[LinkIssueID]         [int] NOT NULL,
		[LinkTarget]          [nvarchar](max) NOT NULL,
		[LinkDescription]     [nvarchar](450) NOT NULL,
		[LinkOutdated]        [bit] NOT NULL,
		[LinkTotalClicks]     [int] NULL,
		[LinkGUID]            [uniqueidentifier] NOT NULL
)  
ALTER TABLE [Newsletter_Link]
	ADD
	CONSTRAINT [PK_Newsletter_Link]
	PRIMARY KEY
	CLUSTERED
	([LinkID])
	
ALTER TABLE [Newsletter_Link]
	ADD
	CONSTRAINT [DF_Newsletter_Link_LinkOutdated]
	DEFAULT ((0)) FOR [LinkOutdated]
ALTER TABLE [Newsletter_Link]
	WITH CHECK
	ADD CONSTRAINT [FK_Newsletter_Link_Newsletter_NewsletterIssue]
	FOREIGN KEY ([LinkIssueID]) REFERENCES [Newsletter_NewsletterIssue] ([IssueID])
ALTER TABLE [Newsletter_Link]
	CHECK CONSTRAINT [FK_Newsletter_Link_Newsletter_NewsletterIssue]
