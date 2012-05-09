CREATE TABLE [COM_SKUDiscountCoupon] (
		[SKUID]                [int] NOT NULL,
		[DiscountCouponID]     [int] NOT NULL
) 
ALTER TABLE [COM_SKUDiscountCoupon]
	ADD
	CONSTRAINT [PK_COM_SKUDiscountCoupon]
	PRIMARY KEY
	CLUSTERED
	([SKUID], [DiscountCouponID])
	
	
ALTER TABLE [COM_SKUDiscountCoupon]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_SKUDiscountCoupon_DiscountCouponID_COM_DiscountCoupon]
	FOREIGN KEY ([DiscountCouponID]) REFERENCES [COM_DiscountCoupon] ([DiscountCouponID])
ALTER TABLE [COM_SKUDiscountCoupon]
	CHECK CONSTRAINT [FK_COM_SKUDiscountCoupon_DiscountCouponID_COM_DiscountCoupon]
ALTER TABLE [COM_SKUDiscountCoupon]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_SKUDiscountCoupon_SKUID_COM_SKU]
	FOREIGN KEY ([SKUID]) REFERENCES [COM_SKU] ([SKUID])
ALTER TABLE [COM_SKUDiscountCoupon]
	CHECK CONSTRAINT [FK_COM_SKUDiscountCoupon_SKUID_COM_SKU]
