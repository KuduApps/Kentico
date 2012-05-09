CREATE TABLE [COM_OrderStatus] (
		[StatusID]                   [int] IDENTITY(1, 1) NOT NULL,
		[StatusName]                 [nvarchar](200) NOT NULL,
		[StatusDisplayName]          [nvarchar](200) NOT NULL,
		[StatusOrder]                [int] NULL,
		[StatusEnabled]              [bit] NOT NULL,
		[StatusColor]                [nvarchar](7) NULL,
		[StatusGUID]                 [uniqueidentifier] NOT NULL,
		[StatusLastModified]         [datetime] NOT NULL,
		[StatusSendNotification]     [bit] NULL,
		[StatusSiteID]               [int] NULL,
		[StatusOrderIsPaid]          [bit] NULL
) 
ALTER TABLE [COM_OrderStatus]
	ADD
	CONSTRAINT [PK_COM_OrderStatus]
	PRIMARY KEY
	NONCLUSTERED
	([StatusID])
	
	
CREATE CLUSTERED INDEX [IX_COM_OrderStatus_StatusOrder_StatusDisplayName_StatusEnabled]
	ON [COM_OrderStatus] ([StatusOrder], [StatusDisplayName], [StatusEnabled])
	
	
ALTER TABLE [COM_OrderStatus]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_OrderStatus_StatusSiteID_CMS_Site]
	FOREIGN KEY ([StatusSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [COM_OrderStatus]
	CHECK CONSTRAINT [FK_COM_OrderStatus_StatusSiteID_CMS_Site]
