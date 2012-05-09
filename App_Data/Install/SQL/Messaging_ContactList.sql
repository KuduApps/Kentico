CREATE TABLE [Messaging_ContactList] (
		[ContactListUserID]            [int] NOT NULL,
		[ContactListContactUserID]     [int] NOT NULL
) 
ALTER TABLE [Messaging_ContactList]
	ADD
	CONSTRAINT [PK_Messaging_ContactList]
	PRIMARY KEY
	CLUSTERED
	([ContactListUserID], [ContactListContactUserID])
	
	
ALTER TABLE [Messaging_ContactList]
	WITH CHECK
	ADD CONSTRAINT [FK_Messaging_ContactList_ContactListContactUserID_CMS_User]
	FOREIGN KEY ([ContactListContactUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [Messaging_ContactList]
	CHECK CONSTRAINT [FK_Messaging_ContactList_ContactListContactUserID_CMS_User]
ALTER TABLE [Messaging_ContactList]
	WITH CHECK
	ADD CONSTRAINT [FK_Messaging_ContactList_ContactListUserID_CMS_User]
	FOREIGN KEY ([ContactListUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [Messaging_ContactList]
	CHECK CONSTRAINT [FK_Messaging_ContactList_ContactListUserID_CMS_User]
