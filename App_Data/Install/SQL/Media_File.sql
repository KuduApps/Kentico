CREATE TABLE [Media_File] (
		[FileID]                   [int] IDENTITY(1, 1) NOT NULL,
		[FileName]                 [nvarchar](250) NOT NULL,
		[FileTitle]                [nvarchar](250) NOT NULL,
		[FileDescription]          [nvarchar](max) NOT NULL,
		[FileExtension]            [nvarchar](50) NOT NULL,
		[FileMimeType]             [nvarchar](100) NOT NULL,
		[FilePath]                 [nvarchar](450) NOT NULL,
		[FileSize]                 [bigint] NOT NULL,
		[FileImageWidth]           [int] NULL,
		[FileImageHeight]          [int] NULL,
		[FileGUID]                 [uniqueidentifier] NOT NULL,
		[FileLibraryID]            [int] NOT NULL,
		[FileSiteID]               [int] NOT NULL,
		[FileCreatedByUserID]      [int] NULL,
		[FileCreatedWhen]          [datetime] NOT NULL,
		[FileModifiedByUserID]     [int] NULL,
		[FileModifiedWhen]         [datetime] NOT NULL,
		[FileCustomData]           [nvarchar](max) NULL
)  
ALTER TABLE [Media_File]
	ADD
	CONSTRAINT [PK_Media_File]
	PRIMARY KEY
	NONCLUSTERED
	([FileID])
	
	
ALTER TABLE [Media_File]
	ADD
	CONSTRAINT [DEFAULT_Media_File_FileCreatedWhen]
	DEFAULT ('11/11/2008 4:10:00 PM') FOR [FileCreatedWhen]
ALTER TABLE [Media_File]
	ADD
	CONSTRAINT [DEFAULT_Media_File_FileModifiedWhen]
	DEFAULT ('11/11/2008 4:11:15 PM') FOR [FileModifiedWhen]
ALTER TABLE [Media_File]
	ADD
	CONSTRAINT [DEFAULT_Media_File_FileSize]
	DEFAULT ((0)) FOR [FileSize]
ALTER TABLE [Media_File]
	ADD
	CONSTRAINT [DEFAULT_Media_File_FileTitle]
	DEFAULT ('') FOR [FileTitle]
CREATE NONCLUSTERED INDEX [IX_Media_File_FileCreatedByUserID]
	ON [Media_File] ([FileCreatedByUserID])
	
CREATE NONCLUSTERED INDEX [IX_Media_File_FileLibraryID]
	ON [Media_File] ([FileLibraryID])
	
CREATE NONCLUSTERED INDEX [IX_Media_File_FileModifiedByUserID]
	ON [Media_File] ([FileModifiedByUserID])
	
CREATE CLUSTERED INDEX [IX_Media_File_FilePath]
	ON [Media_File] ([FilePath])
	
	
CREATE NONCLUSTERED INDEX [IX_Media_File_FileSiteID_FileGUID]
	ON [Media_File] ([FileSiteID], [FileGUID])
	
	
ALTER TABLE [Media_File]
	WITH CHECK
	ADD CONSTRAINT [FK_Media_File_FileCreatedByUserID_CMS_User]
	FOREIGN KEY ([FileCreatedByUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [Media_File]
	CHECK CONSTRAINT [FK_Media_File_FileCreatedByUserID_CMS_User]
ALTER TABLE [Media_File]
	WITH CHECK
	ADD CONSTRAINT [FK_Media_File_FileLibraryID_Media_Library]
	FOREIGN KEY ([FileLibraryID]) REFERENCES [Media_Library] ([LibraryID])
ALTER TABLE [Media_File]
	CHECK CONSTRAINT [FK_Media_File_FileLibraryID_Media_Library]
ALTER TABLE [Media_File]
	WITH CHECK
	ADD CONSTRAINT [FK_Media_File_FileModifiedByUserID_CMS_User]
	FOREIGN KEY ([FileModifiedByUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [Media_File]
	CHECK CONSTRAINT [FK_Media_File_FileModifiedByUserID_CMS_User]
ALTER TABLE [Media_File]
	WITH CHECK
	ADD CONSTRAINT [FK_Media_File_FileSiteID_CMS_Site]
	FOREIGN KEY ([FileSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [Media_File]
	CHECK CONSTRAINT [FK_Media_File_FileSiteID_CMS_Site]
