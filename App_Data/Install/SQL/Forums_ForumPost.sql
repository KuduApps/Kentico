CREATE TABLE [Forums_ForumPost] (
		[PostId]                                 [int] IDENTITY(1, 1) NOT NULL,
		[PostForumID]                            [int] NOT NULL,
		[PostParentID]                           [int] NULL,
		[PostIDPath]                             [nvarchar](450) NOT NULL,
		[PostLevel]                              [int] NOT NULL,
		[PostSubject]                            [nvarchar](450) NOT NULL,
		[PostUserID]                             [int] NULL,
		[PostUserName]                           [nvarchar](200) NOT NULL,
		[PostUserMail]                           [nvarchar](100) NULL,
		[PostText]                               [nvarchar](max) NULL,
		[PostTime]                               [datetime] NOT NULL,
		[PostApprovedByUserID]                   [int] NULL,
		[PostThreadPosts]                        [int] NULL,
		[PostThreadLastPostUserName]             [nvarchar](200) NULL,
		[PostThreadLastPostTime]                 [datetime] NULL,
		[PostUserSignature]                      [nvarchar](max) NULL,
		[PostGUID]                               [uniqueidentifier] NOT NULL,
		[PostLastModified]                       [datetime] NOT NULL,
		[PostApproved]                           [bit] NULL,
		[PostIsLocked]                           [bit] NULL,
		[PostIsAnswer]                           [int] NULL,
		[PostStickOrder]                         [int] NOT NULL,
		[PostViews]                              [int] NULL,
		[PostLastEdit]                           [datetime] NULL,
		[PostInfo]                               [nvarchar](max) NULL,
		[PostAttachmentCount]                    [int] NULL,
		[PostType]                               [int] NULL,
		[PostThreadPostsAbsolute]                [int] NULL,
		[PostThreadLastPostUserNameAbsolute]     [nvarchar](200) NULL,
		[PostThreadLastPostTimeAbsolute]         [datetime] NULL,
		[PostQuestionSolved]                     [bit] NULL,
		[PostIsNotAnswer]                        [int] NULL
)  
ALTER TABLE [Forums_ForumPost]
	ADD
	CONSTRAINT [PK_Forums_ForumPost]
	PRIMARY KEY
	NONCLUSTERED
	([PostId])
	
	
ALTER TABLE [Forums_ForumPost]
	ADD
	CONSTRAINT [DEFAULT_Forums_ForumPost_PostAttachmentCount]
	DEFAULT ((0)) FOR [PostAttachmentCount]
ALTER TABLE [Forums_ForumPost]
	ADD
	CONSTRAINT [DEFAULT_Forums_ForumPost_PostIsLocked]
	DEFAULT ((0)) FOR [PostIsLocked]
ALTER TABLE [Forums_ForumPost]
	ADD
	CONSTRAINT [DEFAULT_Forums_ForumPost_PostQuestionSolved]
	DEFAULT ((0)) FOR [PostQuestionSolved]
ALTER TABLE [Forums_ForumPost]
	ADD
	CONSTRAINT [DEFAULT_Forums_ForumPost_PostUserName]
	DEFAULT ('') FOR [PostUserName]
CREATE NONCLUSTERED INDEX [IX_Forums_ForumPost_PostApproved]
	ON [Forums_ForumPost] ([PostApproved])
	
	
CREATE NONCLUSTERED INDEX [IX_Forums_ForumPost_PostApprovedByUserID]
	ON [Forums_ForumPost] ([PostApprovedByUserID])
	
CREATE NONCLUSTERED INDEX [IX_Forums_ForumPost_PostForumID]
	ON [Forums_ForumPost] ([PostForumID])
	
CREATE UNIQUE CLUSTERED INDEX [IX_Forums_ForumPost_PostIDPath]
	ON [Forums_ForumPost] ([PostIDPath])
	
	
CREATE NONCLUSTERED INDEX [IX_Forums_ForumPost_PostLevel]
	ON [Forums_ForumPost] ([PostLevel])
	
	
CREATE NONCLUSTERED INDEX [IX_Forums_ForumPost_PostParentID]
	ON [Forums_ForumPost] ([PostParentID])
	
CREATE NONCLUSTERED INDEX [IX_Forums_ForumPost_PostUserID]
	ON [Forums_ForumPost] ([PostUserID])
	
ALTER TABLE [Forums_ForumPost]
	WITH CHECK
	ADD CONSTRAINT [FK_Forums_ForumPost_PostApprovedByUserID_CMS_User]
	FOREIGN KEY ([PostApprovedByUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [Forums_ForumPost]
	CHECK CONSTRAINT [FK_Forums_ForumPost_PostApprovedByUserID_CMS_User]
ALTER TABLE [Forums_ForumPost]
	WITH CHECK
	ADD CONSTRAINT [FK_Forums_ForumPost_PostForumID_Forums_Forum]
	FOREIGN KEY ([PostForumID]) REFERENCES [Forums_Forum] ([ForumID])
ALTER TABLE [Forums_ForumPost]
	CHECK CONSTRAINT [FK_Forums_ForumPost_PostForumID_Forums_Forum]
ALTER TABLE [Forums_ForumPost]
	WITH CHECK
	ADD CONSTRAINT [FK_Forums_ForumPost_PostParentID_Forums_ForumPost]
	FOREIGN KEY ([PostParentID]) REFERENCES [Forums_ForumPost] ([PostId])
ALTER TABLE [Forums_ForumPost]
	CHECK CONSTRAINT [FK_Forums_ForumPost_PostParentID_Forums_ForumPost]
ALTER TABLE [Forums_ForumPost]
	WITH CHECK
	ADD CONSTRAINT [FK_Forums_ForumPost_PostUserID_CMS_User]
	FOREIGN KEY ([PostUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [Forums_ForumPost]
	CHECK CONSTRAINT [FK_Forums_ForumPost_PostUserID_CMS_User]
