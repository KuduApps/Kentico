CREATE TABLE [COM_ShippingOptionTaxClass] (
		[ShippingOptionID]     [int] NOT NULL,
		[TaxClassID]           [int] NOT NULL
) 
ALTER TABLE [COM_ShippingOptionTaxClass]
	ADD
	CONSTRAINT [PK_COM_ShippingOptionTaxClass]
	PRIMARY KEY
	CLUSTERED
	([ShippingOptionID], [TaxClassID])
	
ALTER TABLE [COM_ShippingOptionTaxClass]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_ShippingOptionTaxClass_ShippingOptionID_COM_ShippingOption]
	FOREIGN KEY ([ShippingOptionID]) REFERENCES [COM_ShippingOption] ([ShippingOptionID])
ALTER TABLE [COM_ShippingOptionTaxClass]
	CHECK CONSTRAINT [FK_COM_ShippingOptionTaxClass_ShippingOptionID_COM_ShippingOption]
ALTER TABLE [COM_ShippingOptionTaxClass]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_ShippingOptionTaxClass_TaxClassID_COM_TaxClass]
	FOREIGN KEY ([TaxClassID]) REFERENCES [COM_TaxClass] ([TaxClassID])
ALTER TABLE [COM_ShippingOptionTaxClass]
	CHECK CONSTRAINT [FK_COM_ShippingOptionTaxClass_TaxClassID_COM_TaxClass]
