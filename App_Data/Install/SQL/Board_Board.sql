CREATE TABLE [Board_Board] (
		[BoardID]                      [int] IDENTITY(1, 1) NOT NULL,
		[BoardName]                    [nvarchar](250) NOT NULL,
		[BoardDisplayName]             [nvarchar](250) NOT NULL,
		[BoardDescription]             [nvarchar](max) NOT NULL,
		[BoardOpened]                  [bit] NOT NULL,
		[BoardOpenedFrom]              [datetime] NULL,
		[BoardOpenedTo]                [datetime] NULL,
		[BoardEnabled]                 [bit] NOT NULL,
		[BoardAccess]                  [int] NOT NULL,
		[BoardModerated]               [bit] NOT NULL,
		[BoardUseCaptcha]              [bit] NOT NULL,
		[BoardMessages]                [int] NOT NULL,
		[BoardLastModified]            [datetime] NOT NULL,
		[BoardGUID]                    [uniqueidentifier] NOT NULL,
		[BoardDocumentID]              [int] NOT NULL,
		[BoardUserID]                  [int] NULL,
		[BoardGroupID]                 [int] NULL,
		[BoardLastMessageTime]         [datetime] NULL,
		[BoardLastMessageUserName]     [nvarchar](250) NULL,
		[BoardUnsubscriptionURL]       [nvarchar](450) NULL,
		[BoardRequireEmails]           [bit] NULL,
		[BoardSiteID]                  [int] NOT NULL,
		[BoardEnableSubscriptions]     [bit] NOT NULL,
		[BoardBaseURL]                 [nvarchar](450) NULL,
		[BoardLogActivity]             [bit] NULL
)  
ALTER TABLE [Board_Board]
	ADD
	CONSTRAINT [PK_Board_Board]
	PRIMARY KEY
	CLUSTERED
	([BoardID])
	
	
ALTER TABLE [Board_Board]
	ADD
	CONSTRAINT [DEFAULT_Board_Board_BoardEnableSubscriptions]
	DEFAULT ((0)) FOR [BoardEnableSubscriptions]
ALTER TABLE [Board_Board]
	ADD
	CONSTRAINT [DEFAULT_Board_Board_BoardName]
	DEFAULT ('') FOR [BoardName]
ALTER TABLE [Board_Board]
	ADD
	CONSTRAINT [DEFAULT_Board_Board_BoardRequireEmails]
	DEFAULT ((0)) FOR [BoardRequireEmails]
CREATE UNIQUE NONCLUSTERED INDEX [IX_Board_Board_BoardDocumentID_BoardName]
	ON [Board_Board] ([BoardDocumentID], [BoardName])
	
CREATE NONCLUSTERED INDEX [IX_Board_Board_BoardGroupID_BoardName]
	ON [Board_Board] ([BoardGroupID], [BoardName])
	
CREATE NONCLUSTERED INDEX [IX_Board_Board_BoardSiteID]
	ON [Board_Board] ([BoardSiteID])
	
CREATE NONCLUSTERED INDEX [IX_Board_Board_BoardUserID_BoardName]
	ON [Board_Board] ([BoardUserID], [BoardName])
	
ALTER TABLE [Board_Board]
	WITH CHECK
	ADD CONSTRAINT [FK_Board_Board_BoardDocumentID_CMS_Document]
	FOREIGN KEY ([BoardDocumentID]) REFERENCES [CMS_Document] ([DocumentID])
ALTER TABLE [Board_Board]
	CHECK CONSTRAINT [FK_Board_Board_BoardDocumentID_CMS_Document]
ALTER TABLE [Board_Board]
	WITH CHECK
	ADD CONSTRAINT [FK_Board_Board_BoardGroupID_Community_Group]
	FOREIGN KEY ([BoardGroupID]) REFERENCES [Community_Group] ([GroupID])
ALTER TABLE [Board_Board]
	CHECK CONSTRAINT [FK_Board_Board_BoardGroupID_Community_Group]
ALTER TABLE [Board_Board]
	WITH CHECK
	ADD CONSTRAINT [FK_Board_Board_BoardSiteID_CMS_Site]
	FOREIGN KEY ([BoardSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [Board_Board]
	CHECK CONSTRAINT [FK_Board_Board_BoardSiteID_CMS_Site]
ALTER TABLE [Board_Board]
	WITH CHECK
	ADD CONSTRAINT [FK_Board_Board_BoardUserID_CMS_User]
	FOREIGN KEY ([BoardUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [Board_Board]
	CHECK CONSTRAINT [FK_Board_Board_BoardUserID_CMS_User]
