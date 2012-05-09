CREATE TABLE [COM_SKUFile] (
		[FileID]               [int] IDENTITY(1, 1) NOT NULL,
		[FileGUID]             [uniqueidentifier] NOT NULL,
		[FileSKUID]            [int] NOT NULL,
		[FilePath]             [nvarchar](450) NOT NULL,
		[FileType]             [nvarchar](50) NOT NULL,
		[FileLastModified]     [datetime] NOT NULL,
		[FileName]             [nvarchar](250) NOT NULL,
		[FileMetaFileGUID]     [uniqueidentifier] NULL
) 
ALTER TABLE [COM_SKUFile]
	ADD
	CONSTRAINT [PK_COM_SKUFile]
	PRIMARY KEY
	CLUSTERED
	([FileID])
	
ALTER TABLE [COM_SKUFile]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_SKUFile_COM_SKU]
	FOREIGN KEY ([FileSKUID]) REFERENCES [COM_SKU] ([SKUID])
ALTER TABLE [COM_SKUFile]
	CHECK CONSTRAINT [FK_COM_SKUFile_COM_SKU]
