CREATE TABLE [Polls_PollSite] (
		[PollID]     [int] NOT NULL,
		[SiteID]     [int] NOT NULL
) 
ALTER TABLE [Polls_PollSite]
	ADD
	CONSTRAINT [PK_Polls_PollSite]
	PRIMARY KEY
	CLUSTERED
	([PollID], [SiteID])
	
	
ALTER TABLE [Polls_PollSite]
	WITH CHECK
	ADD CONSTRAINT [FK_Polls_PollSite_PollID_Polls_Poll]
	FOREIGN KEY ([PollID]) REFERENCES [Polls_Poll] ([PollID])
ALTER TABLE [Polls_PollSite]
	CHECK CONSTRAINT [FK_Polls_PollSite_PollID_Polls_Poll]
ALTER TABLE [Polls_PollSite]
	WITH CHECK
	ADD CONSTRAINT [FK_Polls_PollSite_SiteID_CMS_Site]
	FOREIGN KEY ([SiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [Polls_PollSite]
	CHECK CONSTRAINT [FK_Polls_PollSite_SiteID_CMS_Site]
