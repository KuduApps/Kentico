CREATE TABLE [COM_OrderItem] (
		[OrderItemID]                           [int] IDENTITY(1, 1) NOT NULL,
		[OrderItemOrderID]                      [int] NOT NULL,
		[OrderItemSKUID]                        [int] NOT NULL,
		[OrderItemSKUName]                      [nvarchar](450) NOT NULL,
		[OrderItemUnitPrice]                    [float] NOT NULL,
		[OrderItemUnitCount]                    [int] NOT NULL,
		[OrderItemCustomData]                   [nvarchar](max) NULL,
		[OrderItemGuid]                         [uniqueidentifier] NOT NULL,
		[OrderItemParentGuid]                   [uniqueidentifier] NULL,
		[OrderItemLastModified]                 [datetime] NOT NULL,
		[OrderItemIsPrivate]                    [bit] NULL,
		[OrderItemSKU]                          [nvarchar](max) NULL,
		[OrderItemValidTo]                      [datetime] NULL,
		[OrderItemBundleGUID]                   [uniqueidentifier] NULL,
		[OrderItemTotalPriceInMainCurrency]     [float] NULL,
		[OrderItemSendNotification]             [bit] NULL,
		[OrderItemText]                         [nvarchar](max) NULL,
		[OrderItemPrice]                        [float] NULL
)  
ALTER TABLE [COM_OrderItem]
	ADD
	CONSTRAINT [PK_COM_OrderItem]
	PRIMARY KEY
	CLUSTERED
	([OrderItemID])
	
	
ALTER TABLE [COM_OrderItem]
	ADD
	CONSTRAINT [DEFAULT_COM_OrderItem_OrderItemGuid]
	DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [OrderItemGuid]
CREATE NONCLUSTERED INDEX [IX_COM_OrderItem_OrderItemOrderID]
	ON [COM_OrderItem] ([OrderItemOrderID])
	
CREATE NONCLUSTERED INDEX [IX_COM_OrderItem_OrderItemSKUID]
	ON [COM_OrderItem] ([OrderItemSKUID])
	
ALTER TABLE [COM_OrderItem]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_OrderItem_OrderItemOrderID_COM_Order]
	FOREIGN KEY ([OrderItemOrderID]) REFERENCES [COM_Order] ([OrderID])
ALTER TABLE [COM_OrderItem]
	CHECK CONSTRAINT [FK_COM_OrderItem_OrderItemOrderID_COM_Order]
ALTER TABLE [COM_OrderItem]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_OrderItem_OrderItemSKUID_COM_SKU]
	FOREIGN KEY ([OrderItemSKUID]) REFERENCES [COM_SKU] ([SKUID])
ALTER TABLE [COM_OrderItem]
	CHECK CONSTRAINT [FK_COM_OrderItem_OrderItemSKUID_COM_SKU]
