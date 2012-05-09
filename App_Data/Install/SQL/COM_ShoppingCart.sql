CREATE TABLE [COM_ShoppingCart] (
		[ShoppingCartID]                    [int] IDENTITY(1, 1) NOT NULL,
		[ShoppingCartGUID]                  [uniqueidentifier] NOT NULL,
		[ShoppingCartUserID]                [int] NULL,
		[ShoppingCartSiteID]                [int] NOT NULL,
		[ShoppingCartLastUpdate]            [datetime] NOT NULL,
		[ShoppingCartCurrencyID]            [int] NULL,
		[ShoppingCartPaymentOptionID]       [int] NULL,
		[ShoppingCartShippingOptionID]      [int] NULL,
		[ShoppingCartDiscountCouponID]      [int] NULL,
		[ShoppingCartBillingAddressID]      [int] NULL,
		[ShoppingCartShippingAddressID]     [int] NULL,
		[ShoppingCartCustomerID]            [int] NULL,
		[ShoppingCartNote]                  [nvarchar](max) NULL,
		[ShoppingCartCompanyAddressID]      [int] NULL,
		[ShoppingCartCustomData]            [nvarchar](max) NULL
)  
ALTER TABLE [COM_ShoppingCart]
	ADD
	CONSTRAINT [PK_COM_ShoppingCart]
	PRIMARY KEY
	CLUSTERED
	([ShoppingCartID])
	
	
ALTER TABLE [COM_ShoppingCart]
	ADD
	CONSTRAINT [DEFAULT_COM_ShoppingCart_ShoppingCartGUID]
	DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [ShoppingCartGUID]
CREATE NONCLUSTERED INDEX [IX_COM_ShoppingCart_ShoppingCartBillingAddressID]
	ON [COM_ShoppingCart] ([ShoppingCartBillingAddressID])
	
CREATE NONCLUSTERED INDEX [IX_COM_ShoppingCart_ShoppingCartCompanyAddressID]
	ON [COM_ShoppingCart] ([ShoppingCartCompanyAddressID])
	
CREATE NONCLUSTERED INDEX [IX_COM_ShoppingCart_ShoppingCartCurrencyID]
	ON [COM_ShoppingCart] ([ShoppingCartCurrencyID])
	
CREATE NONCLUSTERED INDEX [IX_COM_ShoppingCart_ShoppingCartCustomerID]
	ON [COM_ShoppingCart] ([ShoppingCartCustomerID])
	
CREATE NONCLUSTERED INDEX [IX_COM_ShoppingCart_ShoppingCartDiscountCouponID]
	ON [COM_ShoppingCart] ([ShoppingCartDiscountCouponID])
	
CREATE NONCLUSTERED INDEX [IX_COM_ShoppingCart_ShoppingCartLastUpdate]
	ON [COM_ShoppingCart] ([ShoppingCartLastUpdate])
	
	
CREATE NONCLUSTERED INDEX [IX_COM_ShoppingCart_ShoppingCartPaymentOptionID]
	ON [COM_ShoppingCart] ([ShoppingCartPaymentOptionID])
	
CREATE NONCLUSTERED INDEX [IX_COM_ShoppingCart_ShoppingCartShippingAddressID]
	ON [COM_ShoppingCart] ([ShoppingCartShippingAddressID])
	
CREATE NONCLUSTERED INDEX [IX_COM_ShoppingCart_ShoppingCartShippingOptionID]
	ON [COM_ShoppingCart] ([ShoppingCartShippingOptionID])
	
CREATE NONCLUSTERED INDEX [IX_COM_ShoppingCart_ShoppingCartSiteID_ShoppingCartGUID]
	ON [COM_ShoppingCart] ([ShoppingCartGUID])
	
CREATE NONCLUSTERED INDEX [IX_COM_ShoppingCart_ShoppingCartUserID]
	ON [COM_ShoppingCart] ([ShoppingCartUserID])
	
ALTER TABLE [COM_ShoppingCart]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_ShoppingCart_ShoppingCartBillingAddressID_COM_Address]
	FOREIGN KEY ([ShoppingCartBillingAddressID]) REFERENCES [COM_Address] ([AddressID])
ALTER TABLE [COM_ShoppingCart]
	CHECK CONSTRAINT [FK_COM_ShoppingCart_ShoppingCartBillingAddressID_COM_Address]
ALTER TABLE [COM_ShoppingCart]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_ShoppingCart_ShoppingCartCompanyAddressID_COM_Address]
	FOREIGN KEY ([ShoppingCartCompanyAddressID]) REFERENCES [COM_Address] ([AddressID])
ALTER TABLE [COM_ShoppingCart]
	CHECK CONSTRAINT [FK_COM_ShoppingCart_ShoppingCartCompanyAddressID_COM_Address]
ALTER TABLE [COM_ShoppingCart]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_ShoppingCart_ShoppingCartCurrencyID_COM_Currency]
	FOREIGN KEY ([ShoppingCartCurrencyID]) REFERENCES [COM_Currency] ([CurrencyID])
ALTER TABLE [COM_ShoppingCart]
	CHECK CONSTRAINT [FK_COM_ShoppingCart_ShoppingCartCurrencyID_COM_Currency]
ALTER TABLE [COM_ShoppingCart]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_ShoppingCart_ShoppingCartCustomerID_COM_Customer]
	FOREIGN KEY ([ShoppingCartCustomerID]) REFERENCES [COM_Customer] ([CustomerID])
ALTER TABLE [COM_ShoppingCart]
	CHECK CONSTRAINT [FK_COM_ShoppingCart_ShoppingCartCustomerID_COM_Customer]
ALTER TABLE [COM_ShoppingCart]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_ShoppingCart_ShoppingCartDiscountCouponID_COM_DiscountCoupon]
	FOREIGN KEY ([ShoppingCartDiscountCouponID]) REFERENCES [COM_DiscountCoupon] ([DiscountCouponID])
ALTER TABLE [COM_ShoppingCart]
	CHECK CONSTRAINT [FK_COM_ShoppingCart_ShoppingCartDiscountCouponID_COM_DiscountCoupon]
ALTER TABLE [COM_ShoppingCart]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_ShoppingCart_ShoppingCartPaymentOptionID_COM_PaymentOption]
	FOREIGN KEY ([ShoppingCartPaymentOptionID]) REFERENCES [COM_PaymentOption] ([PaymentOptionID])
ALTER TABLE [COM_ShoppingCart]
	CHECK CONSTRAINT [FK_COM_ShoppingCart_ShoppingCartPaymentOptionID_COM_PaymentOption]
ALTER TABLE [COM_ShoppingCart]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_ShoppingCart_ShoppingCartShippingAddressID_COM_Address]
	FOREIGN KEY ([ShoppingCartShippingAddressID]) REFERENCES [COM_Address] ([AddressID])
ALTER TABLE [COM_ShoppingCart]
	CHECK CONSTRAINT [FK_COM_ShoppingCart_ShoppingCartShippingAddressID_COM_Address]
ALTER TABLE [COM_ShoppingCart]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_ShoppingCart_ShoppingCartShippingOptionID_COM_ShippingOption]
	FOREIGN KEY ([ShoppingCartShippingOptionID]) REFERENCES [COM_ShippingOption] ([ShippingOptionID])
ALTER TABLE [COM_ShoppingCart]
	CHECK CONSTRAINT [FK_COM_ShoppingCart_ShoppingCartShippingOptionID_COM_ShippingOption]
ALTER TABLE [COM_ShoppingCart]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_ShoppingCart_ShoppingCartSiteID_CMS_Site]
	FOREIGN KEY ([ShoppingCartSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [COM_ShoppingCart]
	CHECK CONSTRAINT [FK_COM_ShoppingCart_ShoppingCartSiteID_CMS_Site]
ALTER TABLE [COM_ShoppingCart]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_ShoppingCart_ShoppingCartUserID_CMS_User]
	FOREIGN KEY ([ShoppingCartUserID]) REFERENCES [CMS_User] ([UserID])
ALTER TABLE [COM_ShoppingCart]
	CHECK CONSTRAINT [FK_COM_ShoppingCart_ShoppingCartUserID_CMS_User]
