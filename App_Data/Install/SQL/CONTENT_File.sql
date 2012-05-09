CREATE TABLE [CONTENT_File] (
		[FileID]              [int] IDENTITY(1, 1) NOT NULL,
		[FileDescription]     [nvarchar](500) NULL,
		[FileName]            [nvarchar](100) NOT NULL,
		[FileAttachment]      [uniqueidentifier] NULL
) 
ALTER TABLE [CONTENT_File]
	ADD
	CONSTRAINT [PK__CONTENT_File__46D27B73]
	PRIMARY KEY
	CLUSTERED
	([FileID])
	
	
ALTER TABLE [CONTENT_File]
	ADD
	CONSTRAINT [DEFAULT_CONTENT_File_FileNames]
	DEFAULT ('') FOR [FileName]
