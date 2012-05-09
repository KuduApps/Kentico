CREATE TABLE [Forums_Forum] (
		[ForumID]                           [int] IDENTITY(1, 1) NOT NULL,
		[ForumGroupID]                      [int] NOT NULL,
		[ForumName]                         [nvarchar](200) NOT NULL,
		[ForumDisplayName]                  [nvarchar](200) NOT NULL,
		[ForumDescription]                  [nvarchar](max) NULL,
		[ForumOrder]                        [int] NULL,
		[ForumDocumentID]                   [int] NULL,
		[ForumOpen]                         [bit] NOT NULL,
		[ForumModerated]                    [bit] NOT NULL,
		[ForumDisplayEmails]                [bit] NULL,
		[ForumRequireEmail]                 [bit] NULL,
		[ForumAccess]                       [int] NOT NULL,
		[ForumThreads]                      [int] NOT NULL,
		[ForumPosts]                        [int] NOT NULL,
		[ForumLastPostTime]                 [datetime] NULL,
		[ForumLastPostUserName]             [nvarchar](200) NULL,
		[ForumBaseUrl]                      [nvarchar](200) NULL,
		[ForumAllowChangeName]              [bit] NULL,
		[ForumHTMLEditor]                   [bit] NULL,
		[ForumUseCAPTCHA]                   [bit] NULL,
		[ForumGUID]                         [uniqueidentifier] NOT NULL,
		[ForumLastModified]                 [datetime] NOT NULL,
		[ForumUnsubscriptionUrl]            [nvarchar](200) NULL,
		[ForumIsLocked]                     [bit] NULL,
		[ForumSettings]                     [nvarchar](max) NULL,
		[ForumAuthorEdit]                   [bit] NULL,
		[ForumAuthorDelete]                 [bit] NULL,
		[ForumType]                         [int] NULL,
		[ForumIsAnswerLimit]                [int] NULL,
		[ForumImageMaxSideSize]             [int] NULL,
		[ForumLastPostTimeAbsolute]         [datetime] NULL,
		[ForumLastPostUserNameAbsolute]     [nvarchar](200) NULL,
		[ForumPostsAbsolute]                [int] NULL,
		[ForumThreadsAbsolute]              [int] NULL,
		[ForumAttachmentMaxFileSize]        [int] NULL,
		[ForumDiscussionActions]            [int] NULL,
		[ForumSiteID]                       [int] NOT NULL,
		[ForumLogActivity]                  [bit] NULL,
		[ForumCommunityGroupID]             [int] NULL
)  
ALTER TABLE [Forums_Forum]
	ADD
	CONSTRAINT [PK_Forums_Forum]
	PRIMARY KEY
	NONCLUSTERED
	([ForumID])
	
	
ALTER TABLE [Forums_Forum]
	ADD
	CONSTRAINT [DEFAULT_Forums_Forum_ForumImageMaxSideSize]
	DEFAULT ((400)) FOR [ForumImageMaxSideSize]
ALTER TABLE [Forums_Forum]
	ADD
	CONSTRAINT [DEFAULT_Forums_Forum_ForumIsAnswerLimit]
	DEFAULT ((5)) FOR [ForumIsAnswerLimit]
ALTER TABLE [Forums_Forum]
	ADD
	CONSTRAINT [DEFAULT_Forums_Forum_ForumIsLocked]
	DEFAULT ((0)) FOR [ForumIsLocked]
ALTER TABLE [Forums_Forum]
	ADD
	CONSTRAINT [DEFAULT_Forums_Forum_ForumSiteID]
	DEFAULT ((0)) FOR [ForumSiteID]
CREATE NONCLUSTERED INDEX [IX_Forums_Forum_ForumDocumentID]
	ON [Forums_Forum] ([ForumDocumentID])
	
CREATE CLUSTERED INDEX [IX_Forums_Forum_ForumGroupID_ForumOrder]
	ON [Forums_Forum] ([ForumGroupID], [ForumOrder])
	
	
CREATE NONCLUSTERED INDEX [IX_Forums_Forum_ForumSiteID_ForumName]
	ON [Forums_Forum] ([ForumSiteID], [ForumName])
	
	
ALTER TABLE [Forums_Forum]
	WITH CHECK
	ADD CONSTRAINT [FK_Forums_Forum_ForumCommunityGroupID_Community_Group]
	FOREIGN KEY ([ForumCommunityGroupID]) REFERENCES [Community_Group] ([GroupID])
ALTER TABLE [Forums_Forum]
	CHECK CONSTRAINT [FK_Forums_Forum_ForumCommunityGroupID_Community_Group]
ALTER TABLE [Forums_Forum]
	WITH CHECK
	ADD CONSTRAINT [FK_Forums_Forum_ForumDocumentID_CMS_Document]
	FOREIGN KEY ([ForumDocumentID]) REFERENCES [CMS_Document] ([DocumentID])
ALTER TABLE [Forums_Forum]
	CHECK CONSTRAINT [FK_Forums_Forum_ForumDocumentID_CMS_Document]
ALTER TABLE [Forums_Forum]
	WITH CHECK
	ADD CONSTRAINT [FK_Forums_Forum_ForumGroupID_Forums_ForumGroup]
	FOREIGN KEY ([ForumGroupID]) REFERENCES [Forums_ForumGroup] ([GroupID])
ALTER TABLE [Forums_Forum]
	CHECK CONSTRAINT [FK_Forums_Forum_ForumGroupID_Forums_ForumGroup]
ALTER TABLE [Forums_Forum]
	WITH CHECK
	ADD CONSTRAINT [FK_Forums_Forum_ForumSiteID_CMS_Site]
	FOREIGN KEY ([ForumSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [Forums_Forum]
	CHECK CONSTRAINT [FK_Forums_Forum_ForumSiteID_CMS_Site]
