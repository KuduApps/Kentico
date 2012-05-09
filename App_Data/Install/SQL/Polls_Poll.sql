CREATE TABLE [Polls_Poll] (
		[PollID]                       [int] IDENTITY(1, 1) NOT NULL,
		[PollCodeName]                 [nvarchar](200) NOT NULL,
		[PollDisplayName]              [nvarchar](200) NOT NULL,
		[PollTitle]                    [nvarchar](100) NULL,
		[PollOpenFrom]                 [datetime] NULL,
		[PollOpenTo]                   [datetime] NULL,
		[PollAllowMultipleAnswers]     [bit] NOT NULL,
		[PollQuestion]                 [nvarchar](450) NOT NULL,
		[PollAccess]                   [int] NOT NULL,
		[PollResponseMessage]          [nvarchar](450) NULL,
		[PollGUID]                     [uniqueidentifier] NOT NULL,
		[PollLastModified]             [datetime] NOT NULL,
		[PollGroupID]                  [int] NULL,
		[PollSiteID]                   [int] NULL,
		[PollLogActivity]              [bit] NULL
) 
ALTER TABLE [Polls_Poll]
	ADD
	CONSTRAINT [PK_Polls_Poll]
	PRIMARY KEY
	NONCLUSTERED
	([PollID])
	
	
CREATE NONCLUSTERED INDEX [IX_Polls_Poll_PollGroupID]
	ON [Polls_Poll] ([PollGroupID])
	
CREATE NONCLUSTERED INDEX [IX_Polls_Poll_PollSiteID_PollCodeName]
	ON [Polls_Poll] ([PollSiteID], [PollCodeName])
	
	
CREATE CLUSTERED INDEX [IX_Polls_Poll_PollSiteID_PollDisplayName]
	ON [Polls_Poll] ([PollSiteID], [PollDisplayName])
	
	
ALTER TABLE [Polls_Poll]
	WITH CHECK
	ADD CONSTRAINT [FK_Polls_Poll_PollGroupID_Community_Group]
	FOREIGN KEY ([PollGroupID]) REFERENCES [Community_Group] ([GroupID])
ALTER TABLE [Polls_Poll]
	CHECK CONSTRAINT [FK_Polls_Poll_PollGroupID_Community_Group]
ALTER TABLE [Polls_Poll]
	WITH CHECK
	ADD CONSTRAINT [FK_Polls_Poll_PollSiteID_CMS_Site]
	FOREIGN KEY ([PollSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [Polls_Poll]
	CHECK CONSTRAINT [FK_Polls_Poll_PollSiteID_CMS_Site]
