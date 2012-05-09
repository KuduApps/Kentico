CREATE TABLE [COM_Order] (
		[OrderID]                           [int] IDENTITY(1, 1) NOT NULL,
		[OrderBillingAddressID]             [int] NOT NULL,
		[OrderShippingAddressID]            [int] NULL,
		[OrderShippingOptionID]             [int] NULL,
		[OrderTotalShipping]                [float] NULL,
		[OrderTotalPrice]                   [float] NOT NULL,
		[OrderTotalTax]                     [float] NOT NULL,
		[OrderDate]                         [datetime] NOT NULL,
		[OrderStatusID]                     [int] NULL,
		[OrderCurrencyID]                   [int] NULL,
		[OrderCustomerID]                   [int] NOT NULL,
		[OrderCreatedByUserID]              [int] NULL,
		[OrderNote]                         [nvarchar](max) NULL,
		[OrderSiteID]                       [int] NOT NULL,
		[OrderPaymentOptionID]              [int] NULL,
		[OrderInvoice]                      [nvarchar](max) NULL,
		[OrderInvoiceNumber]                [nvarchar](200) NULL,
		[OrderDiscountCouponID]             [int] NULL,
		[OrderCompanyAddressID]             [int] NULL,
		[OrderTrackingNumber]               [nvarchar](100) NULL,
		[OrderCustomData]                   [nvarchar](max) NULL,
		[OrderPaymentResult]                [nvarchar](max) NULL,
		[OrderGUID]                         [uniqueidentifier] NOT NULL,
		[OrderLastModified]                 [datetime] NOT NULL,
		[OrderTotalPriceInMainCurrency]     [float] NULL,
		[OrderIsPaid]                       [bit] NULL,
		[OrderCulture]                      [nvarchar](10) NULL
)  
ALTER TABLE [COM_Order]
	ADD
	CONSTRAINT [PK_COM_Order]
	PRIMARY KEY
	NONCLUSTERED
	([OrderID])
	
	
ALTER TABLE [COM_Order]
	ADD
	CONSTRAINT [DEFAULT_COM_Order_OrderGUID]
	DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [OrderGUID]
CREATE NONCLUSTERED INDEX [IX_COM_Order_OrderBillingAddressID]
	ON [COM_Order] ([OrderBillingAddressID])
	
CREATE NONCLUSTERED INDEX [IX_COM_Order_OrderCompanyAddressID]
	ON [COM_Order] ([OrderCompanyAddressID])
	
CREATE NONCLUSTERED INDEX [IX_COM_Order_OrderCurrencyID]
	ON [COM_Order] ([OrderCurrencyID])
	
CREATE NONCLUSTERED INDEX [IX_COM_Order_OrderCustomerID]
	ON [COM_Order] ([OrderCustomerID])
	
CREATE NONCLUSTERED INDEX [IX_COM_Order_OrderDiscountCouponID]
	ON [COM_Order] ([OrderDiscountCouponID])
	
CREATE NONCLUSTERED INDEX [IX_COM_Order_OrderPaymentOptionID]
	ON [COM_Order] ([OrderPaymentOptionID])
	
CREATE NONCLUSTERED INDEX [IX_COM_Order_OrderShippingAddressID]
	ON [COM_Order] ([OrderShippingAddressID])
	
CREATE NONCLUSTERED INDEX [IX_COM_Order_OrderShippingOptionID]
	ON [COM_Order] ([OrderShippingOptionID])
	
CREATE CLUSTERED INDEX [IX_COM_Order_OrderSiteID_OrderDate]
	ON [COM_Order] ([OrderSiteID], [OrderDate] DESC)
	
	
CREATE NONCLUSTERED INDEX [IX_COM_Order_OrderStatusID]
	ON [COM_Order] ([OrderStatusID])
	
ALTER TABLE [COM_Order]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_Order_OrderBillingAddressID_COM_Adress]
	FOREIGN KEY ([OrderBillingAddressID]) REFERENCES [COM_Address] ([AddressID])
ALTER TABLE [COM_Order]
	CHECK CONSTRAINT [FK_COM_Order_OrderBillingAddressID_COM_Adress]
ALTER TABLE [COM_Order]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_Order_OrderCompanyAddressID_COM_Address]
	FOREIGN KEY ([OrderCompanyAddressID]) REFERENCES [COM_Address] ([AddressID])
ALTER TABLE [COM_Order]
	CHECK CONSTRAINT [FK_COM_Order_OrderCompanyAddressID_COM_Address]
ALTER TABLE [COM_Order]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_Order_OrderCurrencyID_COM_Currency]
	FOREIGN KEY ([OrderCurrencyID]) REFERENCES [COM_Currency] ([CurrencyID])
ALTER TABLE [COM_Order]
	CHECK CONSTRAINT [FK_COM_Order_OrderCurrencyID_COM_Currency]
ALTER TABLE [COM_Order]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_Order_OrderCustomerID_COM_Customer]
	FOREIGN KEY ([OrderCustomerID]) REFERENCES [COM_Customer] ([CustomerID])
ALTER TABLE [COM_Order]
	CHECK CONSTRAINT [FK_COM_Order_OrderCustomerID_COM_Customer]
ALTER TABLE [COM_Order]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_Order_OrderDiscountCouponID_COM_DiscountCoupon]
	FOREIGN KEY ([OrderDiscountCouponID]) REFERENCES [COM_DiscountCoupon] ([DiscountCouponID])
ALTER TABLE [COM_Order]
	CHECK CONSTRAINT [FK_COM_Order_OrderDiscountCouponID_COM_DiscountCoupon]
ALTER TABLE [COM_Order]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_Order_OrderPaymentOptionID_COM_PaymentOption]
	FOREIGN KEY ([OrderPaymentOptionID]) REFERENCES [COM_PaymentOption] ([PaymentOptionID])
ALTER TABLE [COM_Order]
	CHECK CONSTRAINT [FK_COM_Order_OrderPaymentOptionID_COM_PaymentOption]
ALTER TABLE [COM_Order]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_Order_OrderShippingAddressID_COM_Address]
	FOREIGN KEY ([OrderShippingAddressID]) REFERENCES [COM_Address] ([AddressID])
ALTER TABLE [COM_Order]
	CHECK CONSTRAINT [FK_COM_Order_OrderShippingAddressID_COM_Address]
ALTER TABLE [COM_Order]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_Order_OrderShippingOptionID_COM_ShippingOption]
	FOREIGN KEY ([OrderShippingOptionID]) REFERENCES [COM_ShippingOption] ([ShippingOptionID])
ALTER TABLE [COM_Order]
	CHECK CONSTRAINT [FK_COM_Order_OrderShippingOptionID_COM_ShippingOption]
ALTER TABLE [COM_Order]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_Order_OrderSiteID_CMS_Site]
	FOREIGN KEY ([OrderSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [COM_Order]
	CHECK CONSTRAINT [FK_COM_Order_OrderSiteID_CMS_Site]
ALTER TABLE [COM_Order]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_Order_OrderStatusID_COM_Status]
	FOREIGN KEY ([OrderStatusID]) REFERENCES [COM_OrderStatus] ([StatusID])
ALTER TABLE [COM_Order]
	CHECK CONSTRAINT [FK_COM_Order_OrderStatusID_COM_Status]
