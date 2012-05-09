CREATE TABLE [Temp_File] (
		[FileID]               [int] IDENTITY(1, 1) NOT NULL,
		[FileParentGUID]       [uniqueidentifier] NOT NULL,
		[FileNumber]           [int] NOT NULL,
		[FileExtension]        [nvarchar](50) NOT NULL,
		[FileSize]             [bigint] NOT NULL,
		[FileMimeType]         [nvarchar](100) NOT NULL,
		[FileImageWidth]       [int] NULL,
		[FileImageHeight]      [int] NULL,
		[FileBinary]           [varbinary](max) NULL,
		[FileGUID]             [uniqueidentifier] NOT NULL,
		[FileLastModified]     [datetime] NOT NULL,
		[FileDirectory]        [nvarchar](200) NOT NULL,
		[FileName]             [nvarchar](200) NOT NULL,
		[FileTitle]            [nvarchar](250) NULL,
		[FileDescription]      [nvarchar](max) NULL
)  
ALTER TABLE [Temp_File]
	ADD
	CONSTRAINT [PK__Temp_File__7041D2B6]
	PRIMARY KEY
	CLUSTERED
	([FileID])
	
ALTER TABLE [Temp_File]
	ADD
	CONSTRAINT [DEFAULT_Temp_File_FileDirectory]
	DEFAULT ('') FOR [FileDirectory]
ALTER TABLE [Temp_File]
	ADD
	CONSTRAINT [DEFAULT_Temp_File_FileExtension]
	DEFAULT ('') FOR [FileExtension]
ALTER TABLE [Temp_File]
	ADD
	CONSTRAINT [DEFAULT_Temp_File_FileGUID]
	DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [FileGUID]
ALTER TABLE [Temp_File]
	ADD
	CONSTRAINT [DEFAULT_Temp_File_FileLastModified]
	DEFAULT ('6/29/2010 1:57:54 PM') FOR [FileLastModified]
ALTER TABLE [Temp_File]
	ADD
	CONSTRAINT [DEFAULT_Temp_File_FileMimeType]
	DEFAULT ('') FOR [FileMimeType]
ALTER TABLE [Temp_File]
	ADD
	CONSTRAINT [DEFAULT_Temp_File_FileName]
	DEFAULT ('') FOR [FileName]
ALTER TABLE [Temp_File]
	ADD
	CONSTRAINT [DEFAULT_Temp_File_FileNumber]
	DEFAULT ((0)) FOR [FileNumber]
ALTER TABLE [Temp_File]
	ADD
	CONSTRAINT [DEFAULT_Temp_File_FileParentGUID]
	DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [FileParentGUID]
ALTER TABLE [Temp_File]
	ADD
	CONSTRAINT [DEFAULT_Temp_File_FileSize]
	DEFAULT ((0)) FOR [FileSize]
