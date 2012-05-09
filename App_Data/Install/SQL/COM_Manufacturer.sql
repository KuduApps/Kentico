CREATE TABLE [COM_Manufacturer] (
		[ManufacturerID]               [int] IDENTITY(1, 1) NOT NULL,
		[ManufacturerDisplayName]      [nvarchar](200) NOT NULL,
		[ManufactureHomepage]          [nvarchar](400) NULL,
		[ManufacturerEnabled]          [bit] NOT NULL,
		[ManufacturerGUID]             [uniqueidentifier] NOT NULL,
		[ManufacturerLastModified]     [datetime] NOT NULL,
		[ManufacturerSiteID]           [int] NULL
) 
ALTER TABLE [COM_Manufacturer]
	ADD
	CONSTRAINT [PK_COM_Manufacturer]
	PRIMARY KEY
	NONCLUSTERED
	([ManufacturerID])
	
	
CREATE CLUSTERED INDEX [IX_COM_Manufacturer_ManufacturerDisplayName_ManufacturerEnabled]
	ON [COM_Manufacturer] ([ManufacturerDisplayName], [ManufacturerEnabled])
	
	
ALTER TABLE [COM_Manufacturer]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_Manufacturer_ManufacturerSiteID_CMS_Site]
	FOREIGN KEY ([ManufacturerSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [COM_Manufacturer]
	CHECK CONSTRAINT [FK_COM_Manufacturer_ManufacturerSiteID_CMS_Site]
