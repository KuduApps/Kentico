CREATE TABLE [COM_CustomerCreditHistory] (
		[EventID]                     [int] IDENTITY(1, 1) NOT NULL,
		[EventName]                   [nvarchar](200) NOT NULL,
		[EventCreditChange]           [float] NOT NULL,
		[EventDate]                   [datetime] NOT NULL,
		[EventDescription]            [nvarchar](max) NOT NULL,
		[EventCustomerID]             [int] NOT NULL,
		[EventCreditGUID]             [uniqueidentifier] NULL,
		[EventCreditLastModified]     [datetime] NOT NULL,
		[EventSiteID]                 [int] NULL
)  
ALTER TABLE [COM_CustomerCreditHistory]
	ADD
	CONSTRAINT [PK_COM_CustomerCreditHistory]
	PRIMARY KEY
	NONCLUSTERED
	([EventID])
	
	
CREATE CLUSTERED INDEX [IX_COM_CustomerCreditHistory_EventCustomerID_EventDate]
	ON [COM_CustomerCreditHistory] ([EventCustomerID], [EventDate] DESC)
	
	
ALTER TABLE [COM_CustomerCreditHistory]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_CustomerCreditHistory_EventCustomerID_COM_Customer]
	FOREIGN KEY ([EventCustomerID]) REFERENCES [COM_Customer] ([CustomerID])
ALTER TABLE [COM_CustomerCreditHistory]
	CHECK CONSTRAINT [FK_COM_CustomerCreditHistory_EventCustomerID_COM_Customer]
ALTER TABLE [COM_CustomerCreditHistory]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_CustomerCreditHistory_EventSiteID_CMS_Site]
	FOREIGN KEY ([EventSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [COM_CustomerCreditHistory]
	CHECK CONSTRAINT [FK_COM_CustomerCreditHistory_EventSiteID_CMS_Site]
