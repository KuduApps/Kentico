CREATE TABLE [COM_VolumeDiscount] (
		[VolumeDiscountID]               [int] IDENTITY(1, 1) NOT NULL,
		[VolumeDiscountSKUID]            [int] NOT NULL,
		[VolumeDiscountMinCount]         [int] NOT NULL,
		[VolumeDiscountValue]            [float] NOT NULL,
		[VolumeDiscountIsFlatValue]      [bit] NOT NULL,
		[VolumeDiscountGUID]             [uniqueidentifier] NOT NULL,
		[VolumeDiscountLastModified]     [datetime] NOT NULL
) 
ALTER TABLE [COM_VolumeDiscount]
	ADD
	CONSTRAINT [PK_COM_VolumeDiscount]
	PRIMARY KEY
	CLUSTERED
	([VolumeDiscountID])
	
	
CREATE NONCLUSTERED INDEX [IX_COM_VolumeDiscount_VolumeDiscountSKUID]
	ON [COM_VolumeDiscount] ([VolumeDiscountSKUID])
	
ALTER TABLE [COM_VolumeDiscount]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_VolumeDiscount_VolumeDiscountSKUID_COM_SKU]
	FOREIGN KEY ([VolumeDiscountSKUID]) REFERENCES [COM_SKU] ([SKUID])
ALTER TABLE [COM_VolumeDiscount]
	CHECK CONSTRAINT [FK_COM_VolumeDiscount_VolumeDiscountSKUID_COM_SKU]
