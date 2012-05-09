CREATE TABLE [Community_Group] (
		[GroupID]                                     [int] IDENTITY(1, 1) NOT NULL,
		[GroupGUID]                                   [uniqueidentifier] NOT NULL,
		[GroupLastModified]                           [datetime] NOT NULL,
		[GroupSiteID]                                 [int] NOT NULL,
		[GroupDisplayName]                            [nvarchar](200) NOT NULL,
		[GroupName]                                   [nvarchar](100) NOT NULL,
		[GroupDescription]                            [nvarchar](max) NOT NULL,
		[GroupNodeGUID]                               [uniqueidentifier] NULL,
		[GroupApproveMembers]                         [int] NOT NULL,
		[GroupAccess]                                 [int] NOT NULL,
		[GroupCreatedByUserID]                        [int] NULL,
		[GroupApprovedByUserID]                       [int] NULL,
		[GroupAvatarID]                               [int] NULL,
		[GroupApproved]                               [bit] NULL,
		[GroupCreatedWhen]                            [datetime] NOT NULL,
		[GroupSendJoinLeaveNotification]              [bit] NULL,
		[GroupSendWaitingForApprovalNotification]     [bit] NULL,
		[GroupSecurity]                               [int] NULL,
		[GroupLogActivity]                            [bit] NULL
)  
ALTER TABLE [Community_Group]
	ADD
	CONSTRAINT [PK_Community_Group]
	PRIMARY KEY
	NONCLUSTERED
	([GroupID])
	
	
ALTER TABLE [Community_Group]
	ADD
	CONSTRAINT [DEFAULT_Community_Group_GroupApproved]
	DEFAULT ((0)) FOR [GroupApproved]
ALTER TABLE [Community_Group]
	ADD
	CONSTRAINT [DEFAULT_Community_Group_GroupCreatedWhen]
	DEFAULT ('10/21/2008 10:17:56 AM') FOR [GroupCreatedWhen]
ALTER TABLE [Community_Group]
	ADD
	CONSTRAINT [DEFAULT_Community_Group_GroupSecurity]
	DEFAULT ((444)) FOR [GroupSecurity]
ALTER TABLE [Community_Group]
	ADD
	CONSTRAINT [DEFAULT_Community_Group_GroupSendJoinLeaveNotification]
	DEFAULT ((1)) FOR [GroupSendJoinLeaveNotification]
ALTER TABLE [Community_Group]
	ADD
	CONSTRAINT [DEFAULT_Community_Group_GroupSendWaitingForApprovalNotification]
	DEFAULT ((1)) FOR [GroupSendWaitingForApprovalNotification]
CREATE NONCLUSTERED INDEX [IX_Community_Group_GroupApproved]
	ON [Community_Group] ([GroupApproved])
	
	
CREATE NONCLUSTERED INDEX [IX_Community_Group_GroupApprovedByUserID]
	ON [Community_Group] ([GroupApprovedByUserID])
	
CREATE NONCLUSTERED INDEX [IX_Community_Group_GroupAvatarID]
	ON [Community_Group] ([GroupAvatarID])
	
CREATE NONCLUSTERED INDEX [IX_Community_Group_GroupCreatedByUserID]
	ON [Community_Group] ([GroupCreatedByUserID])
	
CREATE CLUSTERED INDEX [IX_Community_Group_GroupDisplayName]
	ON [Community_Group] ([GroupSiteID], [GroupDisplayName])
	
	
CREATE NONCLUSTERED INDEX [IX_Community_Group_GroupSiteID_GroupName]
	ON [Community_Group] ([GroupSiteID], [GroupName])
	
	
ALTER TABLE [Community_Group]
	WITH CHECK
	ADD CONSTRAINT [FK_Community_Group_GroupApprovedByUserID_CMS_User]
	FOREIGN KEY ([GroupApprovedByUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [Community_Group]
	CHECK CONSTRAINT [FK_Community_Group_GroupApprovedByUserID_CMS_User]
ALTER TABLE [Community_Group]
	WITH CHECK
	ADD CONSTRAINT [FK_Community_Group_GroupAvatarID_CMS_Avatar]
	FOREIGN KEY ([GroupAvatarID]) REFERENCES [CMS_Avatar] ([AvatarID])
ALTER TABLE [Community_Group]
	CHECK CONSTRAINT [FK_Community_Group_GroupAvatarID_CMS_Avatar]
ALTER TABLE [Community_Group]
	WITH CHECK
	ADD CONSTRAINT [FK_Community_Group_GroupCreatedByUserID_CMS_User]
	FOREIGN KEY ([GroupCreatedByUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [Community_Group]
	CHECK CONSTRAINT [FK_Community_Group_GroupCreatedByUserID_CMS_User]
ALTER TABLE [Community_Group]
	WITH CHECK
	ADD CONSTRAINT [FK_Community_Group_GroupSiteID_CMS_Site]
	FOREIGN KEY ([GroupSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [Community_Group]
	CHECK CONSTRAINT [FK_Community_Group_GroupSiteID_CMS_Site]
