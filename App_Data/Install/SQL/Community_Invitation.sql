CREATE TABLE [Community_Invitation] (
		[InvitationID]               [int] IDENTITY(1, 1) NOT NULL,
		[InvitedUserID]              [int] NULL,
		[InvitedByUserID]            [int] NOT NULL,
		[InvitationGroupID]          [int] NULL,
		[InvitationCreated]          [datetime] NULL,
		[InvitationValidTo]          [datetime] NULL,
		[InvitationComment]          [nvarchar](max) NULL,
		[InvitationGUID]             [uniqueidentifier] NOT NULL,
		[InvitationLastModified]     [datetime] NOT NULL,
		[InvitationUserEmail]        [nvarchar](200) NULL
)  
ALTER TABLE [Community_Invitation]
	ADD
	CONSTRAINT [PK_Community_GroupInvitation]
	PRIMARY KEY
	CLUSTERED
	([InvitationID])
	
	
CREATE NONCLUSTERED INDEX [IX_Community_Invitation_InvitationGroupID]
	ON [Community_Invitation] ([InvitationGroupID])
	
CREATE NONCLUSTERED INDEX [IX_Community_Invitation_InvitedByUserID]
	ON [Community_Invitation] ([InvitedByUserID])
	
CREATE NONCLUSTERED INDEX [IX_Community_Invitation_InvitedUserID]
	ON [Community_Invitation] ([InvitedUserID])
	
ALTER TABLE [Community_Invitation]
	WITH CHECK
	ADD CONSTRAINT [FK_Community_GroupInvitation_InvitationGroupID_Community_Group]
	FOREIGN KEY ([InvitationGroupID]) REFERENCES [Community_Group] ([GroupID])
ALTER TABLE [Community_Invitation]
	CHECK CONSTRAINT [FK_Community_GroupInvitation_InvitationGroupID_Community_Group]
ALTER TABLE [Community_Invitation]
	WITH CHECK
	ADD CONSTRAINT [FK_Community_GroupInvitation_InvitedByUserID_CMS_User]
	FOREIGN KEY ([InvitedByUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [Community_Invitation]
	CHECK CONSTRAINT [FK_Community_GroupInvitation_InvitedByUserID_CMS_User]
ALTER TABLE [Community_Invitation]
	WITH CHECK
	ADD CONSTRAINT [FK_Community_GroupInvitation_InvitedUserID_CMS_User]
	FOREIGN KEY ([InvitedUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [Community_Invitation]
	CHECK CONSTRAINT [FK_Community_GroupInvitation_InvitedUserID_CMS_User]
