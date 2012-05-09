CREATE TABLE [Polls_PollRoles] (
		[PollID]     [int] NOT NULL,
		[RoleID]     [int] NOT NULL
) 
ALTER TABLE [Polls_PollRoles]
	ADD
	CONSTRAINT [PK_Polls_PollRoles]
	PRIMARY KEY
	CLUSTERED
	([PollID], [RoleID])
	
	
ALTER TABLE [Polls_PollRoles]
	WITH CHECK
	ADD CONSTRAINT [FK_Polls_PollRoles_PollID_Polls_Poll]
	FOREIGN KEY ([PollID]) REFERENCES [Polls_Poll] ([PollID])
ALTER TABLE [Polls_PollRoles]
	CHECK CONSTRAINT [FK_Polls_PollRoles_PollID_Polls_Poll]
ALTER TABLE [Polls_PollRoles]
	WITH CHECK
	ADD CONSTRAINT [FK_Polls_PollRoles_RoleID_CMS_Role]
	FOREIGN KEY ([RoleID]) REFERENCES [CMS_Role] ([RoleID])
ALTER TABLE [Polls_PollRoles]
	CHECK CONSTRAINT [FK_Polls_PollRoles_RoleID_CMS_Role]
