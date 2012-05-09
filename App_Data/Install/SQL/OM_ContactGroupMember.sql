CREATE TABLE [OM_ContactGroupMember] (
		[ContactGroupMemberID]                 [int] IDENTITY(1, 1) NOT NULL,
		[ContactGroupMemberContactGroupID]     [int] NOT NULL,
		[ContactGroupMemberType]               [int] NOT NULL,
		[ContactGroupMemberRelatedID]          [int] NOT NULL,
		[ContactGroupMemberFromCondition]      [bit] NULL,
		[ContactGroupMemberFromAccount]        [bit] NULL,
		[ContactGroupMemberFromManual]         [bit] NULL
) 
ALTER TABLE [OM_ContactGroupMember]
	ADD
	CONSTRAINT [PK_OM_ContactGroupMember_1]
	PRIMARY KEY
	CLUSTERED
	([ContactGroupMemberID])
	
ALTER TABLE [OM_ContactGroupMember]
	ADD
	CONSTRAINT [DEFAULT_OM_ContactGroupMember_ContactGroupMemberFromCondition]
	DEFAULT ((0)) FOR [ContactGroupMemberFromCondition]
ALTER TABLE [OM_ContactGroupMember]
	ADD
	CONSTRAINT [DEFAULT_OM_ContactGroupMember_MemberFromManual]
	DEFAULT ((0)) FOR [ContactGroupMemberFromManual]
ALTER TABLE [OM_ContactGroupMember]
	ADD
	CONSTRAINT [DEFAULT_OM_ContactGroupMember_MemberType]
	DEFAULT ((0)) FOR [ContactGroupMemberType]
ALTER TABLE [OM_ContactGroupMember]
	ADD
	CONSTRAINT [DEFAULT_OM_ContactGroupMember_RelatedID]
	DEFAULT ((0)) FOR [ContactGroupMemberRelatedID]
CREATE NONCLUSTERED INDEX [IX_OM_ContactGroupMember_ContactGroupMemberContactGroupID]
	ON [OM_ContactGroupMember] ([ContactGroupMemberContactGroupID])
	
CREATE NONCLUSTERED INDEX [IX_OM_ContactGroupMember_ContactGroupMemberRelatedID]
	ON [OM_ContactGroupMember] ([ContactGroupMemberRelatedID])
	
ALTER TABLE [OM_ContactGroupMember]
	WITH CHECK
	ADD CONSTRAINT [FK_OM_ContactGroupMembers_OM_ContactGroup]
	FOREIGN KEY ([ContactGroupMemberContactGroupID]) REFERENCES [OM_ContactGroup] ([ContactGroupID])
ALTER TABLE [OM_ContactGroupMember]
	CHECK CONSTRAINT [FK_OM_ContactGroupMembers_OM_ContactGroup]
