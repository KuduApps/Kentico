CREATE TABLE [Forums_Attachment] (
		[AttachmentID]                [int] IDENTITY(1, 1) NOT NULL,
		[AttachmentFileName]          [nvarchar](200) NOT NULL,
		[AttachmentFileExtension]     [nvarchar](10) NOT NULL,
		[AttachmentBinary]            [varbinary](max) NULL,
		[AttachmentGUID]              [uniqueidentifier] NOT NULL,
		[AttachmentLastModified]      [datetime] NOT NULL,
		[AttachmentMimeType]          [nvarchar](100) NOT NULL,
		[AttachmentFileSize]          [int] NOT NULL,
		[AttachmentImageHeight]       [int] NULL,
		[AttachmentImageWidth]        [int] NULL,
		[AttachmentPostID]            [int] NOT NULL,
		[AttachmentSiteID]            [int] NOT NULL
)  
ALTER TABLE [Forums_Attachment]
	ADD
	CONSTRAINT [PK_Forums_Attachment]
	PRIMARY KEY
	CLUSTERED
	([AttachmentID])
	
	
ALTER TABLE [Forums_Attachment]
	ADD
	CONSTRAINT [DEFAULT_Forums_Attachment_AttachmentPostID]
	DEFAULT ((0)) FOR [AttachmentPostID]
CREATE UNIQUE NONCLUSTERED INDEX [IX_Forums_Attachment_AttachmentGUID]
	ON [Forums_Attachment] ([AttachmentSiteID], [AttachmentGUID])
	
	
CREATE NONCLUSTERED INDEX [IX_Forums_Attachment_AttachmentPostID]
	ON [Forums_Attachment] ([AttachmentPostID])
	
ALTER TABLE [Forums_Attachment]
	WITH CHECK
	ADD CONSTRAINT [FK_Forums_Attachment_AttachmentPostID_Forums_ForumPost]
	FOREIGN KEY ([AttachmentPostID]) REFERENCES [Forums_ForumPost] ([PostId])
ALTER TABLE [Forums_Attachment]
	CHECK CONSTRAINT [FK_Forums_Attachment_AttachmentPostID_Forums_ForumPost]
ALTER TABLE [Forums_Attachment]
	WITH CHECK
	ADD CONSTRAINT [FK_Forums_Attachment_AttachmentSiteID_CMS_Site]
	FOREIGN KEY ([AttachmentSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [Forums_Attachment]
	CHECK CONSTRAINT [FK_Forums_Attachment_AttachmentSiteID_CMS_Site]
