CREATE TABLE [COM_PublicStatus] (
		[PublicStatusID]               [int] IDENTITY(1, 1) NOT NULL,
		[PublicStatusName]             [nvarchar](200) NOT NULL,
		[PublicStatusDisplayName]      [nvarchar](200) NOT NULL,
		[PublicStatusEnabled]          [bit] NOT NULL,
		[PublicStatusGUID]             [uniqueidentifier] NOT NULL,
		[PublicStatusLastModified]     [datetime] NOT NULL,
		[PublicStatusSiteID]           [int] NULL
) 
ALTER TABLE [COM_PublicStatus]
	ADD
	CONSTRAINT [PK_COM_PublicStatus]
	PRIMARY KEY
	NONCLUSTERED
	([PublicStatusID])
	
	
CREATE CLUSTERED INDEX [IX_COM_PublicStatus_PublicStatusDisplayName_PublicStatusEnabled]
	ON [COM_PublicStatus] ([PublicStatusDisplayName], [PublicStatusEnabled])
	
	
ALTER TABLE [COM_PublicStatus]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_PublicStatus_PublicStatusSiteID_CMS_Site]
	FOREIGN KEY ([PublicStatusSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [COM_PublicStatus]
	CHECK CONSTRAINT [FK_COM_PublicStatus_PublicStatusSiteID_CMS_Site]
