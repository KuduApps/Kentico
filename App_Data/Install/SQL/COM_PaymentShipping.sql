CREATE TABLE [COM_PaymentShipping] (
		[PaymentOptionID]      [int] NOT NULL,
		[ShippingOptionID]     [int] NOT NULL
) 
ALTER TABLE [COM_PaymentShipping]
	ADD
	CONSTRAINT [PK_COM_PaymentShipping]
	PRIMARY KEY
	CLUSTERED
	([PaymentOptionID], [ShippingOptionID])
	
	
ALTER TABLE [COM_PaymentShipping]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_PaymentShipping_PaymentOptionID_COM_PaymentOption]
	FOREIGN KEY ([PaymentOptionID]) REFERENCES [COM_PaymentOption] ([PaymentOptionID])
ALTER TABLE [COM_PaymentShipping]
	CHECK CONSTRAINT [FK_COM_PaymentShipping_PaymentOptionID_COM_PaymentOption]
ALTER TABLE [COM_PaymentShipping]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_PaymentShipping_ShippingOptionID_COM_ShippingOption]
	FOREIGN KEY ([ShippingOptionID]) REFERENCES [COM_ShippingOption] ([ShippingOptionID])
ALTER TABLE [COM_PaymentShipping]
	CHECK CONSTRAINT [FK_COM_PaymentShipping_ShippingOptionID_COM_ShippingOption]
