CREATE TABLE [CMS_EmailUser] (
		[EmailID]             [int] NOT NULL,
		[UserID]              [int] NOT NULL,
		[LastSendResult]      [nvarchar](max) NULL,
		[LastSendAttempt]     [datetime] NULL,
		[Status]              [int] NULL
)  
ALTER TABLE [CMS_EmailUser]
	ADD
	CONSTRAINT [PK_CMS_EmailUser]
	PRIMARY KEY
	CLUSTERED
	([EmailID], [UserID])
	
	
ALTER TABLE [CMS_EmailUser]
	ADD
	CONSTRAINT [DEFAULT_CMS_EmailRole_UserID]
	DEFAULT ((0)) FOR [UserID]
CREATE NONCLUSTERED INDEX [IX_CMS_EmailUser_Status]
	ON [CMS_EmailUser] ([Status])
	
	
ALTER TABLE [CMS_EmailUser]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_EmailUser_EmailID_CMS_Email]
	FOREIGN KEY ([EmailID]) REFERENCES [CMS_Email] ([EmailID])
ALTER TABLE [CMS_EmailUser]
	CHECK CONSTRAINT [FK_CMS_EmailUser_EmailID_CMS_Email]
ALTER TABLE [CMS_EmailUser]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_EmailUser_UserID_CMS_User]
	FOREIGN KEY ([UserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [CMS_EmailUser]
	CHECK CONSTRAINT [FK_CMS_EmailUser_UserID_CMS_User]
