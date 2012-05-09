CREATE TABLE [COM_DiscountCoupon] (
		[DiscountCouponID]               [int] IDENTITY(1, 1) NOT NULL,
		[DiscountCouponDisplayName]      [nvarchar](200) NOT NULL,
		[DiscountCouponIsExcluded]       [bit] NOT NULL,
		[DiscountCouponValidFrom]        [datetime] NULL,
		[DiscountCouponValidTo]          [datetime] NULL,
		[DiscountCouponValue]            [float] NULL,
		[DiscountCouponIsFlatValue]      [bit] NOT NULL,
		[DiscountCouponCode]             [nvarchar](200) NOT NULL,
		[DiscountCouponGUID]             [uniqueidentifier] NOT NULL,
		[DiscountCouponLastModified]     [datetime] NOT NULL,
		[DiscountCouponSiteID]           [int] NULL
) 
ALTER TABLE [COM_DiscountCoupon]
	ADD
	CONSTRAINT [PK_COM_DiscountCoupon]
	PRIMARY KEY
	NONCLUSTERED
	([DiscountCouponID])
	
	
CREATE NONCLUSTERED INDEX [IX_COM_DiscountCoupon_DiscountCouponCode]
	ON [COM_DiscountCoupon] ([DiscountCouponCode])
	
	
CREATE CLUSTERED INDEX [IX_COM_DiscountCoupon_DiscoutCouponDisplayName]
	ON [COM_DiscountCoupon] ([DiscountCouponDisplayName])
	
	
ALTER TABLE [COM_DiscountCoupon]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_DiscountCoupon_DiscountCouponSiteID_CMS_Site]
	FOREIGN KEY ([DiscountCouponSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [COM_DiscountCoupon]
	CHECK CONSTRAINT [FK_COM_DiscountCoupon_DiscountCouponSiteID_CMS_Site]
