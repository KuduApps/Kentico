CREATE TABLE [CMS_Resource] (
		[ResourceID]               [int] IDENTITY(1, 1) NOT NULL,
		[ResourceDisplayName]      [nvarchar](100) NOT NULL,
		[ResourceName]             [nvarchar](100) NOT NULL,
		[ResourceDescription]      [nvarchar](max) NULL,
		[ShowInDevelopment]        [bit] NULL,
		[ResourceURL]              [nvarchar](1000) NULL,
		[ResourceGUID]             [uniqueidentifier] NOT NULL,
		[ResourceLastModified]     [datetime] NOT NULL
)  
ALTER TABLE [CMS_Resource]
	ADD
	CONSTRAINT [PK_CMS_Resource]
	PRIMARY KEY
	NONCLUSTERED
	([ResourceID])
	
	
CREATE CLUSTERED INDEX [IX_CMS_Resource_ResourceDisplayName]
	ON [CMS_Resource] ([ResourceDisplayName])
	
	
CREATE NONCLUSTERED INDEX [IX_CMS_Resource_ResourceName]
	ON [CMS_Resource] ([ResourceName])
	
	
