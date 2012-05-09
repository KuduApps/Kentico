CREATE TABLE [CMS_EmailAttachment] (
		[AttachmentID]               [int] IDENTITY(1, 1) NOT NULL,
		[AttachmentName]             [nvarchar](255) NOT NULL,
		[AttachmentExtension]        [nvarchar](50) NOT NULL,
		[AttachmentSize]             [int] NOT NULL,
		[AttachmentMimeType]         [nvarchar](100) NOT NULL,
		[AttachmentBinary]           [varbinary](max) NOT NULL,
		[AttachmentGUID]             [uniqueidentifier] NOT NULL,
		[AttachmentLastModified]     [datetime] NOT NULL,
		[AttachmentContentID]        [nvarchar](255) NULL,
		[AttachmentSiteID]           [int] NULL
)  
ALTER TABLE [CMS_EmailAttachment]
	ADD
	CONSTRAINT [PK_CMS_EmailAttachment]
	PRIMARY KEY
	CLUSTERED
	([AttachmentID])
	
	
