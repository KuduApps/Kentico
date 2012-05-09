CREATE TABLE [Notification_Gateway] (
		[GatewayID]                    [int] IDENTITY(1, 1) NOT NULL,
		[GatewayName]                  [nvarchar](250) NOT NULL,
		[GatewayDisplayName]           [nvarchar](250) NOT NULL,
		[GatewayAssemblyName]          [nvarchar](250) NOT NULL,
		[GatewayClassName]             [nvarchar](250) NOT NULL,
		[GatewayDescription]           [nvarchar](max) NOT NULL,
		[GatewaySupportsEmail]         [bit] NOT NULL,
		[GatewaySupportsPlainText]     [bit] NOT NULL,
		[GatewaySupportsHTMLText]      [bit] NOT NULL,
		[GatewayLastModified]          [datetime] NOT NULL,
		[GatewayGUID]                  [uniqueidentifier] NOT NULL,
		[GatewayEnabled]               [bit] NULL
)  
ALTER TABLE [Notification_Gateway]
	ADD
	CONSTRAINT [PK_Notification_Gateway]
	PRIMARY KEY
	NONCLUSTERED
	([GatewayID])
	
	
ALTER TABLE [Notification_Gateway]
	ADD
	CONSTRAINT [DEFAULT_Notification_Gateway_GatewayEnabled]
	DEFAULT ((0)) FOR [GatewayEnabled]
CREATE CLUSTERED INDEX [IX_Notification_Gateway_GatewayDisplayName]
	ON [Notification_Gateway] ([GatewayDisplayName])
	
	
