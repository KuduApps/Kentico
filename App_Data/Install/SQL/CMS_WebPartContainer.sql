CREATE TABLE [CMS_WebPartContainer] (
		[ContainerID]               [int] IDENTITY(1, 1) NOT NULL,
		[ContainerDisplayName]      [nvarchar](200) NOT NULL,
		[ContainerName]             [nvarchar](200) NOT NULL,
		[ContainerTextBefore]       [nvarchar](max) NULL,
		[ContainerTextAfter]        [nvarchar](max) NULL,
		[ContainerGUID]             [uniqueidentifier] NOT NULL,
		[ContainerLastModified]     [datetime] NOT NULL,
		[ContainerCSS]              [nvarchar](max) NULL
)  
ALTER TABLE [CMS_WebPartContainer]
	ADD
	CONSTRAINT [PK_CMS_WebPartContainer]
	PRIMARY KEY
	NONCLUSTERED
	([ContainerID])
	
	
CREATE CLUSTERED INDEX [IX_CMS_WebPartContainer_ContainerDisplayName]
	ON [CMS_WebPartContainer] ([ContainerDisplayName])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_WebPartContainer_ContainerName]
	ON [CMS_WebPartContainer] ([ContainerName])
	
	
