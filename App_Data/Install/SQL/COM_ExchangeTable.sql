CREATE TABLE [COM_ExchangeTable] (
		[ExchangeTableID]                         [int] IDENTITY(1, 1) NOT NULL,
		[ExchangeTableDisplayName]                [nvarchar](200) NOT NULL,
		[ExchangeTableValidFrom]                  [datetime] NULL,
		[ExchangeTableValidTo]                    [datetime] NULL,
		[ExchangeTableGUID]                       [uniqueidentifier] NOT NULL,
		[ExchangeTableLastModified]               [datetime] NOT NULL,
		[ExchangeTableSiteID]                     [int] NULL,
		[ExchangeTableRateFromGlobalCurrency]     [float] NULL
) 
ALTER TABLE [COM_ExchangeTable]
	ADD
	CONSTRAINT [PK_COM_ExchangeTable]
	PRIMARY KEY
	NONCLUSTERED
	([ExchangeTableID])
	
	
CREATE CLUSTERED INDEX [IX_COM_ExchangeTable_ExchangeTableValidFrom_ExchangeTableValidTo]
	ON [COM_ExchangeTable] ([ExchangeTableValidFrom] DESC, [ExchangeTableValidTo] DESC)
	
	
ALTER TABLE [COM_ExchangeTable]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_ExchangeTable_ExchangeTableSiteID_CMS_Site]
	FOREIGN KEY ([ExchangeTableSiteID]) REFERENCES [CMS_Site] ([SiteID])
ALTER TABLE [COM_ExchangeTable]
	CHECK CONSTRAINT [FK_COM_ExchangeTable_ExchangeTableSiteID_CMS_Site]
