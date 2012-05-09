CREATE TABLE [Board_Moderator] (
		[BoardID]     [int] NOT NULL,
		[UserID]      [int] NOT NULL
) 
ALTER TABLE [Board_Moderator]
	ADD
	CONSTRAINT [PK_Board_Moderator]
	PRIMARY KEY
	CLUSTERED
	([BoardID], [UserID])
	
	
ALTER TABLE [Board_Moderator]
	WITH CHECK
	ADD CONSTRAINT [FK_Board_Moderator_BoardID_Board_Board]
	FOREIGN KEY ([BoardID]) REFERENCES [Board_Board] ([BoardID])
ALTER TABLE [Board_Moderator]
	CHECK CONSTRAINT [FK_Board_Moderator_BoardID_Board_Board]
ALTER TABLE [Board_Moderator]
	WITH CHECK
	ADD CONSTRAINT [FK_Board_Moderator_UserID_CMS_User]
	FOREIGN KEY ([UserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [Board_Moderator]
	CHECK CONSTRAINT [FK_Board_Moderator_UserID_CMS_User]
