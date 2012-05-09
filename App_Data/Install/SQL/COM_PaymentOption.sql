CREATE TABLE [COM_PaymentOption] (
		[PaymentOptionID]                         [int] IDENTITY(1, 1) NOT NULL,
		[PaymentOptionName]                       [nvarchar](200) NOT NULL,
		[PaymentOptionDisplayName]                [nvarchar](200) NOT NULL,
		[PaymentOptionEnabled]                    [bit] NOT NULL,
		[PaymentOptionSiteID]                     [int] NULL,
		[PaymentOptionPaymentGateUrl]             [nvarchar](500) NULL,
		[PaymentOptionAssemblyName]               [nvarchar](200) NULL,
		[PaymentOptionClassName]                  [nvarchar](200) NULL,
		[PaymentOptionSucceededOrderStatusID]     [int] NULL,
		[PaymentOptionFailedOrderStatusID]        [int] NULL,
		[PaymentOptionGUID]                       [uniqueidentifier] NOT NULL,
		[PaymentOptionLastModified]               [datetime] NOT NULL,
		[PaymentOptionAllowIfNoShipping]          [bit] NULL
) 
ALTER TABLE [COM_PaymentOption]
	ADD
	CONSTRAINT [PK_COM_PaymentOption]
	PRIMARY KEY
	NONCLUSTERED
	([PaymentOptionID])
	
	
CREATE NONCLUSTERED INDEX [IX_COM_PaymentOption_PaymentOptionFailedOrderStatusID]
	ON [COM_PaymentOption] ([PaymentOptionFailedOrderStatusID])
	
CREATE CLUSTERED INDEX [IX_COM_PaymentOption_PaymentOptionSiteID_PaymentOptionDisplayName_PaymentOptionEnabled]
	ON [COM_PaymentOption] ([PaymentOptionSiteID], [PaymentOptionDisplayName], [PaymentOptionEnabled])
	
	
CREATE NONCLUSTERED INDEX [IX_COM_PaymentOption_PaymentOptionSucceededOrderStatusID]
	ON [COM_PaymentOption] ([PaymentOptionSucceededOrderStatusID])
	
ALTER TABLE [COM_PaymentOption]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_PaymentOption_PaymentOptionFailedOrderStatusID_COM_OrderStatus]
	FOREIGN KEY ([PaymentOptionFailedOrderStatusID]) REFERENCES [COM_OrderStatus] ([StatusID])
ALTER TABLE [COM_PaymentOption]
	CHECK CONSTRAINT [FK_COM_PaymentOption_PaymentOptionFailedOrderStatusID_COM_OrderStatus]
ALTER TABLE [COM_PaymentOption]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_PaymentOption_PaymentOptionSiteID_CMS_Site]
	FOREIGN KEY ([PaymentOptionSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [COM_PaymentOption]
	CHECK CONSTRAINT [FK_COM_PaymentOption_PaymentOptionSiteID_CMS_Site]
ALTER TABLE [COM_PaymentOption]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_PaymentOption_PaymentOptionSucceededOrderStatusID_COM_OrderStatus]
	FOREIGN KEY ([PaymentOptionSucceededOrderStatusID]) REFERENCES [COM_OrderStatus] ([StatusID])
ALTER TABLE [COM_PaymentOption]
	CHECK CONSTRAINT [FK_COM_PaymentOption_PaymentOptionSucceededOrderStatusID_COM_OrderStatus]
