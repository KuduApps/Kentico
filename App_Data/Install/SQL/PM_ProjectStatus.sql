CREATE TABLE [PM_ProjectStatus] (
		[StatusID]               [int] IDENTITY(1, 1) NOT NULL,
		[StatusName]             [nvarchar](200) NOT NULL,
		[StatusDisplayName]      [nvarchar](200) NOT NULL,
		[StatusOrder]            [int] NOT NULL,
		[StatusColor]            [nvarchar](7) NULL,
		[StatusIcon]             [nvarchar](450) NULL,
		[StatusGUID]             [uniqueidentifier] NOT NULL,
		[StatusLastModified]     [datetime] NOT NULL,
		[StatusEnabled]          [bit] NOT NULL,
		[StatusIsFinished]       [bit] NOT NULL,
		[StatusIsNotStarted]     [bit] NOT NULL
) 
ALTER TABLE [PM_ProjectStatus]
	ADD
	CONSTRAINT [PK_PM_ProjectStatus]
	PRIMARY KEY
	CLUSTERED
	([StatusID])
	
