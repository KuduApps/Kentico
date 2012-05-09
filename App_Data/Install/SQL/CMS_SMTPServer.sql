CREATE TABLE [CMS_SMTPServer] (
		[ServerID]               [int] IDENTITY(1, 1) NOT NULL,
		[ServerName]             [nvarchar](200) NOT NULL,
		[ServerUserName]         [nvarchar](50) NULL,
		[ServerPassword]         [nvarchar](200) NULL,
		[ServerUseSSL]           [bit] NOT NULL,
		[ServerEnabled]          [bit] NOT NULL,
		[ServerIsGlobal]         [bit] NOT NULL,
		[ServerGUID]             [uniqueidentifier] NOT NULL,
		[ServerLastModified]     [datetime] NOT NULL,
		[ServerPriority]         [int] NULL
) 
ALTER TABLE [CMS_SMTPServer]
	ADD
	CONSTRAINT [PK_CMS_SMTPServer]
	PRIMARY KEY
	CLUSTERED
	([ServerID])
	
