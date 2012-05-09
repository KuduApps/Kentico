CREATE TABLE [COM_ShippingCost] (
		[ShippingCostID]                   [int] IDENTITY(1, 1) NOT NULL,
		[ShippingCostShippingOptionID]     [int] NOT NULL,
		[ShippingCostMinWeight]            [float] NOT NULL,
		[ShippingCostValue]                [float] NOT NULL,
		[ShippingCostGUID]                 [uniqueidentifier] NOT NULL,
		[ShippingCostLastModified]         [datetime] NOT NULL
) 
ALTER TABLE [COM_ShippingCost]
	ADD
	CONSTRAINT [PK__COM_ShippingCost]
	PRIMARY KEY
	CLUSTERED
	([ShippingCostID])
	
ALTER TABLE [COM_ShippingCost]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_ShippingCost_ShippingCostShippingOptionID_COM_ShippingOption]
	FOREIGN KEY ([ShippingCostShippingOptionID]) REFERENCES [COM_ShippingOption] ([ShippingOptionID])
ALTER TABLE [COM_ShippingCost]
	CHECK CONSTRAINT [FK_COM_ShippingCost_ShippingCostShippingOptionID_COM_ShippingOption]
