CREATE TABLE [CMS_MembershipUser] (
		[MembershipUserID]     [int] IDENTITY(1, 1) NOT NULL,
		[MembershipID]         [int] NOT NULL,
		[UserID]               [int] NOT NULL,
		[ValidTo]              [datetime] NULL,
		[SendNotification]     [bit] NULL
) 
ALTER TABLE [CMS_MembershipUser]
	ADD
	CONSTRAINT [PK_CMS_MembershipUser]
	PRIMARY KEY
	CLUSTERED
	([MembershipUserID])
	
ALTER TABLE [CMS_MembershipUser]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_MembershipUser_MembershipID_CMS_Membership]
	FOREIGN KEY ([MembershipID]) REFERENCES [CMS_Membership] ([MembershipID])
ALTER TABLE [CMS_MembershipUser]
	CHECK CONSTRAINT [FK_CMS_MembershipUser_MembershipID_CMS_Membership]
ALTER TABLE [CMS_MembershipUser]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_MembershipUser_UserID_CMS_User]
	FOREIGN KEY ([UserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [CMS_MembershipUser]
	CHECK CONSTRAINT [FK_CMS_MembershipUser_UserID_CMS_User]
