CREATE TABLE [COM_ShippingOption] (
		[ShippingOptionID]               [int] IDENTITY(1, 1) NOT NULL,
		[ShippingOptionName]             [nvarchar](200) NOT NULL,
		[ShippingOptionDisplayName]      [nvarchar](200) NOT NULL,
		[ShippingOptionCharge]           [float] NOT NULL,
		[ShippingOptionEnabled]          [bit] NOT NULL,
		[ShippingOptionSiteID]           [int] NULL,
		[ShippingOptionGUID]             [uniqueidentifier] NOT NULL,
		[ShippingOptionLastModified]     [datetime] NOT NULL
) 
ALTER TABLE [COM_ShippingOption]
	ADD
	CONSTRAINT [PK_COM_ShippingOption]
	PRIMARY KEY
	NONCLUSTERED
	([ShippingOptionID])
	
	
CREATE NONCLUSTERED INDEX [IX_COM_ShippingOption_ShippingOptionSiteID_ShippingOptionDisplayName_ShippingOptionEnabled]
	ON [COM_ShippingOption] ([ShippingOptionSiteID])
	
CREATE CLUSTERED INDEX [IX_COM_ShippingOptionDisplayName]
	ON [COM_ShippingOption] ([ShippingOptionDisplayName])
	
ALTER TABLE [COM_ShippingOption]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_ShippingOption_ShippingOptionSiteID_CMS_Site]
	FOREIGN KEY ([ShippingOptionSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [COM_ShippingOption]
	CHECK CONSTRAINT [FK_COM_ShippingOption_ShippingOptionSiteID_CMS_Site]
