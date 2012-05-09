CREATE TABLE [OM_Score] (
		[ScoreID]                    [int] IDENTITY(1, 1) NOT NULL,
		[ScoreName]                  [nvarchar](200) NOT NULL,
		[ScoreDisplayName]           [nvarchar](200) NOT NULL,
		[ScoreDescription]           [nvarchar](max) NULL,
		[ScoreSiteID]                [int] NOT NULL,
		[ScoreEnabled]               [bit] NOT NULL,
		[ScoreEmailAtScore]          [int] NULL,
		[ScoreNotificationEmail]     [nvarchar](250) NULL,
		[ScoreLastModified]          [datetime] NOT NULL,
		[ScoreGUID]                  [uniqueidentifier] NOT NULL,
		[ScoreStatus]                [int] NULL,
		[ScoreScheduledTaskID]       [int] NULL
)  
ALTER TABLE [OM_Score]
	ADD
	CONSTRAINT [PK_OM_Score]
	PRIMARY KEY
	CLUSTERED
	([ScoreID])
	
CREATE NONCLUSTERED INDEX [IX_OM_Score_ScoreSiteID]
	ON [OM_Score] ([ScoreSiteID])
	
ALTER TABLE [OM_Score]
	WITH CHECK
	ADD CONSTRAINT [FK_OM_Score_CMS_Site]
	FOREIGN KEY ([ScoreSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [OM_Score]
	CHECK CONSTRAINT [FK_OM_Score_CMS_Site]
