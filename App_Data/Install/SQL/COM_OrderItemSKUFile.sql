CREATE TABLE [COM_OrderItemSKUFile] (
		[OrderItemSKUFileID]     [int] IDENTITY(1, 1) NOT NULL,
		[Token]                  [uniqueidentifier] NOT NULL,
		[OrderItemID]            [int] NOT NULL,
		[FileID]                 [int] NOT NULL,
		[FileDownloads]          [int] NOT NULL
) 
ALTER TABLE [COM_OrderItemSKUFile]
	ADD
	CONSTRAINT [PK_COM_OrderItemSKUFile]
	PRIMARY KEY
	CLUSTERED
	([OrderItemSKUFileID])
	
ALTER TABLE [COM_OrderItemSKUFile]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_OrderItemSKUFile_COM_OrderItem]
	FOREIGN KEY ([OrderItemID]) REFERENCES [COM_OrderItem] ([OrderItemID])
ALTER TABLE [COM_OrderItemSKUFile]
	CHECK CONSTRAINT [FK_COM_OrderItemSKUFile_COM_OrderItem]
ALTER TABLE [COM_OrderItemSKUFile]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_OrderItemSKUFile_COM_SKUFile]
	FOREIGN KEY ([FileID]) REFERENCES [COM_SKUFile] ([FileID])
ALTER TABLE [COM_OrderItemSKUFile]
	CHECK CONSTRAINT [FK_COM_OrderItemSKUFile_COM_SKUFile]
