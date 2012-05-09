CREATE TABLE [COM_DiscountLevel] (
		[DiscountLevelID]               [int] IDENTITY(1, 1) NOT NULL,
		[DiscountLevelDisplayName]      [nvarchar](200) NOT NULL,
		[DiscountLevelName]             [nvarchar](100) NOT NULL,
		[DiscountLevelValue]            [float] NOT NULL,
		[DiscountLevelEnabled]          [bit] NOT NULL,
		[DiscountLevelValidFrom]        [datetime] NULL,
		[DiscountLevelValidTo]          [datetime] NULL,
		[DiscountLevelGUID]             [uniqueidentifier] NOT NULL,
		[DiscountLevelLastModified]     [datetime] NOT NULL,
		[DiscountLevelSiteID]           [int] NULL
) 
ALTER TABLE [COM_DiscountLevel]
	ADD
	CONSTRAINT [PK_COM_DiscountLevel]
	PRIMARY KEY
	NONCLUSTERED
	([DiscountLevelID])
	
	
CREATE CLUSTERED INDEX [IX_COM_DiscountLevel_DiscountLevelDisplayName_DiscountLevelEnabled]
	ON [COM_DiscountLevel] ([DiscountLevelDisplayName], [DiscountLevelEnabled])
	
	
ALTER TABLE [COM_DiscountLevel]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_DiscountLevel_DiscountLevelSiteID_CMS_Site]
	FOREIGN KEY ([DiscountLevelSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [COM_DiscountLevel]
	CHECK CONSTRAINT [FK_COM_DiscountLevel_DiscountLevelSiteID_CMS_Site]
