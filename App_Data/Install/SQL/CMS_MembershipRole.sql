CREATE TABLE [CMS_MembershipRole] (
		[MembershipID]     [int] NOT NULL,
		[RoleID]           [int] NOT NULL
) 
ALTER TABLE [CMS_MembershipRole]
	ADD
	CONSTRAINT [PK_CMS_MembershipRole]
	PRIMARY KEY
	CLUSTERED
	([MembershipID], [RoleID])
	
ALTER TABLE [CMS_MembershipRole]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_MembershipRole_MembershipID_CMS_Membership]
	FOREIGN KEY ([MembershipID]) REFERENCES [CMS_Membership] ([MembershipID])
ALTER TABLE [CMS_MembershipRole]
	CHECK CONSTRAINT [FK_CMS_MembershipRole_MembershipID_CMS_Membership]
ALTER TABLE [CMS_MembershipRole]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_MembershipRole_RoleID_CMS_Role]
	FOREIGN KEY ([RoleID]) REFERENCES [CMS_Role] ([RoleID])
ALTER TABLE [CMS_MembershipRole]
	CHECK CONSTRAINT [FK_CMS_MembershipRole_RoleID_CMS_Role]
