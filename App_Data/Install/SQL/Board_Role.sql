CREATE TABLE [Board_Role] (
		[BoardID]     [int] NOT NULL,
		[RoleID]      [int] NOT NULL
) 
ALTER TABLE [Board_Role]
	ADD
	CONSTRAINT [PK_Board_Role]
	PRIMARY KEY
	CLUSTERED
	([BoardID], [RoleID])
	
	
ALTER TABLE [Board_Role]
	WITH CHECK
	ADD CONSTRAINT [FK_Board_Role_BoardID_Board_Board]
	FOREIGN KEY ([BoardID]) REFERENCES [Board_Board] ([BoardID])
ALTER TABLE [Board_Role]
	CHECK CONSTRAINT [FK_Board_Role_BoardID_Board_Board]
ALTER TABLE [Board_Role]
	WITH CHECK
	ADD CONSTRAINT [FK_Board_Role_RoleID_CMS_Role]
	FOREIGN KEY ([RoleID]) REFERENCES [CMS_Role] ([RoleID])
ALTER TABLE [Board_Role]
	CHECK CONSTRAINT [FK_Board_Role_RoleID_CMS_Role]
