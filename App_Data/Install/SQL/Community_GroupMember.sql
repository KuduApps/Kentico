CREATE TABLE [Community_GroupMember] (
		[MemberID]                   [int] IDENTITY(1, 1) NOT NULL,
		[MemberGUID]                 [uniqueidentifier] NOT NULL,
		[MemberUserID]               [int] NOT NULL,
		[MemberGroupID]              [int] NOT NULL,
		[MemberJoined]               [datetime] NOT NULL,
		[MemberApprovedWhen]         [datetime] NULL,
		[MemberRejectedWhen]         [datetime] NULL,
		[MemberApprovedByUserID]     [int] NULL,
		[MemberComment]              [nvarchar](max) NULL,
		[MemberInvitedByUserID]      [int] NULL,
		[MemberStatus]               [int] NULL
)  
ALTER TABLE [Community_GroupMember]
	ADD
	CONSTRAINT [PK_Community_GroupMember]
	PRIMARY KEY
	NONCLUSTERED
	([MemberID])
	
	
ALTER TABLE [Community_GroupMember]
	ADD
	CONSTRAINT [DEFAULT_Community_GroupMember_MemberStatus]
	DEFAULT ((0)) FOR [MemberStatus]
CREATE NONCLUSTERED INDEX [IX_Community_GroupMember_MemberApprovedByUserID]
	ON [Community_GroupMember] ([MemberApprovedByUserID])
	
CREATE NONCLUSTERED INDEX [IX_Community_GroupMember_MemberGroupID]
	ON [Community_GroupMember] ([MemberGroupID])
	
CREATE NONCLUSTERED INDEX [IX_Community_GroupMember_MemberInvitedByUserID]
	ON [Community_GroupMember] ([MemberInvitedByUserID])
	
CREATE CLUSTERED INDEX [IX_Community_GroupMember_MemberJoined]
	ON [Community_GroupMember] ([MemberJoined] DESC)
	
	
CREATE NONCLUSTERED INDEX [IX_Community_GroupMember_MemberStatus]
	ON [Community_GroupMember] ([MemberStatus])
	
	
CREATE NONCLUSTERED INDEX [IX_Community_GroupMember_MemberUserID]
	ON [Community_GroupMember] ([MemberUserID])
	
ALTER TABLE [Community_GroupMember]
	WITH CHECK
	ADD CONSTRAINT [FK_Community_GroupMember_MemberApprovedByUserID_CMS_User]
	FOREIGN KEY ([MemberApprovedByUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [Community_GroupMember]
	CHECK CONSTRAINT [FK_Community_GroupMember_MemberApprovedByUserID_CMS_User]
ALTER TABLE [Community_GroupMember]
	WITH CHECK
	ADD CONSTRAINT [FK_Community_GroupMember_MemberGroupID_Community_Group]
	FOREIGN KEY ([MemberGroupID]) REFERENCES [Community_Group] ([GroupID])
ALTER TABLE [Community_GroupMember]
	CHECK CONSTRAINT [FK_Community_GroupMember_MemberGroupID_Community_Group]
ALTER TABLE [Community_GroupMember]
	WITH CHECK
	ADD CONSTRAINT [FK_Community_GroupMember_MemberInvitedByUserID_CMS_User]
	FOREIGN KEY ([MemberInvitedByUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [Community_GroupMember]
	CHECK CONSTRAINT [FK_Community_GroupMember_MemberInvitedByUserID_CMS_User]
ALTER TABLE [Community_GroupMember]
	WITH CHECK
	ADD CONSTRAINT [FK_Community_GroupMember_MemberUserID_CMS_User]
	FOREIGN KEY ([MemberUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [Community_GroupMember]
	CHECK CONSTRAINT [FK_Community_GroupMember_MemberUserID_CMS_User]
