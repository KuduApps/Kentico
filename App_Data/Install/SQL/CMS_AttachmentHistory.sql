CREATE TABLE [CMS_AttachmentHistory] (
		[AttachmentHistoryID]        [int] IDENTITY(1, 1) NOT NULL,
		[AttachmentName]             [nvarchar](255) NOT NULL,
		[AttachmentExtension]        [nvarchar](50) NOT NULL,
		[AttachmentSize]             [int] NOT NULL,
		[AttachmentMimeType]         [nvarchar](100) NOT NULL,
		[AttachmentBinary]           [varbinary](max) NULL,
		[AttachmentImageWidth]       [int] NULL,
		[AttachmentImageHeight]      [int] NULL,
		[AttachmentDocumentID]       [int] NOT NULL,
		[AttachmentGUID]             [uniqueidentifier] NOT NULL,
		[AttachmentIsUnsorted]       [bit] NULL,
		[AttachmentOrder]            [int] NULL,
		[AttachmentGroupGUID]        [uniqueidentifier] NULL,
		[AttachmentHash]             [nvarchar](32) NULL,
		[AttachmentTitle]            [nvarchar](250) NULL,
		[AttachmentDescription]      [nvarchar](max) NULL,
		[AttachmentCustomData]       [nvarchar](max) NULL,
		[AttachmentLastModified]     [datetime] NULL
)  
ALTER TABLE [CMS_AttachmentHistory]
	ADD
	CONSTRAINT [PK_CMS_AttachmentHistory]
	PRIMARY KEY
	NONCLUSTERED
	([AttachmentHistoryID])
	
	
CREATE CLUSTERED INDEX [IX_CMS_AttachmentHistory_AttachmentDocumentID_AttachmentName]
	ON [CMS_AttachmentHistory] ([AttachmentDocumentID], [AttachmentName])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_AttachmentHistory_AttachmentGUID]
	ON [CMS_AttachmentHistory] ([AttachmentGUID])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_AttachmentHistory_AttachmentIsUnsorted_AttachmentGroupGUID_AttachmentOrder]
	ON [CMS_AttachmentHistory] ([AttachmentIsUnsorted], [AttachmentGroupGUID], [AttachmentOrder])
	
	
