CREATE TABLE [CMS_Layout] (
		[LayoutID]                        [int] IDENTITY(1, 1) NOT NULL,
		[LayoutCodeName]                  [nvarchar](100) NOT NULL,
		[LayoutDisplayName]               [nvarchar](200) NOT NULL,
		[LayoutDescription]               [nvarchar](max) NOT NULL,
		[LayoutCode]                      [nvarchar](max) NOT NULL,
		[LayoutCheckedOutFilename]        [nvarchar](450) NULL,
		[LayoutCheckedOutByUserID]        [int] NULL,
		[LayoutCheckedOutMachineName]     [nvarchar](50) NULL,
		[LayoutVersionGUID]               [nvarchar](50) NULL,
		[LayoutGUID]                      [uniqueidentifier] NOT NULL,
		[LayoutLastModified]              [datetime] NOT NULL,
		[LayoutType]                      [nvarchar](50) NULL,
		[LayoutCSS]                       [nvarchar](max) NULL
)  
ALTER TABLE [CMS_Layout]
	ADD
	CONSTRAINT [PK_CMS_Layout]
	PRIMARY KEY
	CLUSTERED
	([LayoutID])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_Layout_LayoutCheckedOutByUserID]
	ON [CMS_Layout] ([LayoutCheckedOutByUserID])
	
CREATE NONCLUSTERED INDEX [IX_CMS_Layout_LayoutDisplayName]
	ON [CMS_Layout] ([LayoutDisplayName])
	
	
ALTER TABLE [CMS_Layout]
	WITH CHECK
	ADD CONSTRAINT [FK_CMS_Layout_LayoutCheckedOutByUserID_CMS_User]
	FOREIGN KEY ([LayoutCheckedOutByUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [CMS_Layout]
	CHECK CONSTRAINT [FK_CMS_Layout_LayoutCheckedOutByUserID_CMS_User]
