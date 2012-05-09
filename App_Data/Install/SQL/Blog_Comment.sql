CREATE TABLE [Blog_Comment] (
		[CommentID]                   [int] IDENTITY(1, 1) NOT NULL,
		[CommentUserName]             [nvarchar](200) NOT NULL,
		[CommentUserID]               [int] NULL,
		[CommentUrl]                  [nvarchar](450) NULL,
		[CommentText]                 [nvarchar](max) NOT NULL,
		[CommentApprovedByUserID]     [int] NULL,
		[CommentPostDocumentID]       [int] NOT NULL,
		[CommentDate]                 [datetime] NOT NULL,
		[CommentIsSpam]               [bit] NULL,
		[CommentApproved]             [bit] NULL,
		[CommentIsTrackBack]          [bit] NULL,
		[CommentEmail]                [nvarchar](250) NULL
)  
ALTER TABLE [Blog_Comment]
	ADD
	CONSTRAINT [PK_Blog_Comment]
	PRIMARY KEY
	NONCLUSTERED
	([CommentID])
	
	
ALTER TABLE [Blog_Comment]
	ADD
	CONSTRAINT [DEFAULT_Blog_Comment_CommentApproved]
	DEFAULT ((0)) FOR [CommentApproved]
ALTER TABLE [Blog_Comment]
	ADD
	CONSTRAINT [DEFAULT_Blog_Comment_CommentIsSpam]
	DEFAULT ((0)) FOR [CommentIsSpam]
ALTER TABLE [Blog_Comment]
	ADD
	CONSTRAINT [DEFAULT_Blog_Comment_CommentIsTrackBack]
	DEFAULT ((0)) FOR [CommentIsTrackBack]
CREATE NONCLUSTERED INDEX [IX_Blog_Comment_CommentApprovedByUserID]
	ON [Blog_Comment] ([CommentApprovedByUserID])
	
CREATE CLUSTERED INDEX [IX_Blog_Comment_CommentDate]
	ON [Blog_Comment] ([CommentDate] DESC)
	
	
CREATE NONCLUSTERED INDEX [IX_Blog_Comment_CommentIsSpam_CommentIsApproved_CommentIsTrackback]
	ON [Blog_Comment] ([CommentIsSpam], [CommentApproved], [CommentIsTrackBack])
	
	
CREATE NONCLUSTERED INDEX [IX_Blog_Comment_CommentPostDocumentID]
	ON [Blog_Comment] ([CommentPostDocumentID])
	
CREATE NONCLUSTERED INDEX [IX_Blog_Comment_CommentUserID]
	ON [Blog_Comment] ([CommentUserID])
	
ALTER TABLE [Blog_Comment]
	WITH CHECK
	ADD CONSTRAINT [FK_Blog_Comment_CommentApprovedByUserID_CMS_User]
	FOREIGN KEY ([CommentApprovedByUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [Blog_Comment]
	CHECK CONSTRAINT [FK_Blog_Comment_CommentApprovedByUserID_CMS_User]
ALTER TABLE [Blog_Comment]
	WITH CHECK
	ADD CONSTRAINT [FK_Blog_Comment_CommentPostDocumentID_CMS_Document]
	FOREIGN KEY ([CommentPostDocumentID]) REFERENCES [CMS_Document] ([DocumentID])
ALTER TABLE [Blog_Comment]
	CHECK CONSTRAINT [FK_Blog_Comment_CommentPostDocumentID_CMS_Document]
ALTER TABLE [Blog_Comment]
	WITH CHECK
	ADD CONSTRAINT [FK_Blog_Comment_CommentUserID_CMS_User]
	FOREIGN KEY ([CommentUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [Blog_Comment]
	CHECK CONSTRAINT [FK_Blog_Comment_CommentUserID_CMS_User]
