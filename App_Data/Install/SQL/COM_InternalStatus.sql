CREATE TABLE [COM_InternalStatus] (
		[InternalStatusID]               [int] IDENTITY(1, 1) NOT NULL,
		[InternalStatusName]             [nvarchar](200) NOT NULL,
		[InternalStatusDisplayName]      [nvarchar](200) NOT NULL,
		[InternalStatusEnabled]          [bit] NOT NULL,
		[InternalStatusGUID]             [uniqueidentifier] NOT NULL,
		[InternalStatusLastModified]     [datetime] NOT NULL,
		[InternalStatusSiteID]           [int] NULL
) 
ALTER TABLE [COM_InternalStatus]
	ADD
	CONSTRAINT [PK_COM_InternalStatus]
	PRIMARY KEY
	NONCLUSTERED
	([InternalStatusID])
	
	
CREATE CLUSTERED INDEX [IX_COM_InternalStatus_InternalStatusDisplayName_InternalStatusEnabled]
	ON [COM_InternalStatus] ([InternalStatusDisplayName], [InternalStatusEnabled])
	
	
ALTER TABLE [COM_InternalStatus]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_InternalStatus_InternalStatusSiteID_CMS_Site]
	FOREIGN KEY ([InternalStatusSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [COM_InternalStatus]
	CHECK CONSTRAINT [FK_COM_InternalStatus_InternalStatusSiteID_CMS_Site]
