CREATE TABLE [Forums_ForumGroup] (
		[GroupID]                        [int] IDENTITY(1, 1) NOT NULL,
		[GroupSiteID]                    [int] NOT NULL,
		[GroupName]                      [nvarchar](200) NOT NULL,
		[GroupDisplayName]               [nvarchar](200) NOT NULL,
		[GroupOrder]                     [int] NULL,
		[GroupDescription]               [nvarchar](max) NULL,
		[GroupGUID]                      [uniqueidentifier] NOT NULL,
		[GroupLastModified]              [datetime] NOT NULL,
		[GroupBaseUrl]                   [nvarchar](200) NULL,
		[GroupUnsubscriptionUrl]         [nvarchar](200) NULL,
		[GroupGroupID]                   [int] NULL,
		[GroupAuthorEdit]                [bit] NULL,
		[GroupAuthorDelete]              [bit] NULL,
		[GroupType]                      [int] NULL,
		[GroupIsAnswerLimit]             [int] NULL,
		[GroupImageMaxSideSize]          [int] NULL,
		[GroupDisplayEmails]             [bit] NULL,
		[GroupRequireEmail]              [bit] NULL,
		[GroupHTMLEditor]                [bit] NULL,
		[GroupUseCAPTCHA]                [bit] NULL,
		[GroupAttachmentMaxFileSize]     [int] NULL,
		[GroupDiscussionActions]         [int] NULL,
		[GroupLogActivity]               [bit] NULL
)  
ALTER TABLE [Forums_ForumGroup]
	ADD
	CONSTRAINT [PK_Forums_ForumGroup]
	PRIMARY KEY
	NONCLUSTERED
	([GroupID])
	
	
ALTER TABLE [Forums_ForumGroup]
	ADD
	CONSTRAINT [DEFAULT_Forums_ForumGroup_GroupHTMLEditor]
	DEFAULT ((0)) FOR [GroupHTMLEditor]
ALTER TABLE [Forums_ForumGroup]
	ADD
	CONSTRAINT [DEFAULT_Forums_ForumGroup_GroupImageMaxSideSize]
	DEFAULT ((400)) FOR [GroupImageMaxSideSize]
ALTER TABLE [Forums_ForumGroup]
	ADD
	CONSTRAINT [DEFAULT_Forums_ForumGroup_GroupIsAnswerLimit]
	DEFAULT ((5)) FOR [GroupIsAnswerLimit]
ALTER TABLE [Forums_ForumGroup]
	ADD
	CONSTRAINT [DEFAULT_Forums_ForumGroup_GroupUseCAPTCHA]
	DEFAULT ((0)) FOR [GroupUseCAPTCHA]
CREATE NONCLUSTERED INDEX [IX_Forums_ForumGroup_GroupGroupID]
	ON [Forums_ForumGroup] ([GroupGroupID])
	
CREATE NONCLUSTERED INDEX [IX_Forums_ForumGroup_GroupSiteID_GroupName]
	ON [Forums_ForumGroup] ([GroupSiteID], [GroupName])
	
	
CREATE CLUSTERED INDEX [IX_Forums_ForumGroup_GroupSiteID_GroupOrder]
	ON [Forums_ForumGroup] ([GroupSiteID], [GroupOrder])
	
	
ALTER TABLE [Forums_ForumGroup]
	WITH CHECK
	ADD CONSTRAINT [FK_Forums_ForumGroup_GroupGroupID_Community_Group]
	FOREIGN KEY ([GroupGroupID]) REFERENCES [Community_Group] ([GroupID])
ALTER TABLE [Forums_ForumGroup]
	CHECK CONSTRAINT [FK_Forums_ForumGroup_GroupGroupID_Community_Group]
ALTER TABLE [Forums_ForumGroup]
	WITH CHECK
	ADD CONSTRAINT [FK_Forums_ForumGroup_GroupSiteID_CMS_Site]
	FOREIGN KEY ([GroupSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [Forums_ForumGroup]
	CHECK CONSTRAINT [FK_Forums_ForumGroup_GroupSiteID_CMS_Site]
