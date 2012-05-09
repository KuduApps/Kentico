CREATE TABLE [COM_CurrencyExchangeRate] (
		[ExchagneRateID]               [int] IDENTITY(1, 1) NOT NULL,
		[ExchangeRateToCurrencyID]     [int] NOT NULL,
		[ExchangeRateValue]            [float] NOT NULL,
		[ExchangeTableID]              [int] NOT NULL,
		[ExchangeRateGUID]             [uniqueidentifier] NOT NULL,
		[ExchangeRateLastModified]     [datetime] NOT NULL
) 
ALTER TABLE [COM_CurrencyExchangeRate]
	ADD
	CONSTRAINT [PK_COM_CurrencyExchangeRate]
	PRIMARY KEY
	CLUSTERED
	([ExchagneRateID])
	
	
CREATE NONCLUSTERED INDEX [IX_COM_CurrencyExchangeRate_ExchangeRateToCurrencyID]
	ON [COM_CurrencyExchangeRate] ([ExchangeRateToCurrencyID])
	
CREATE NONCLUSTERED INDEX [IX_COM_CurrencyExchangeRate_ExchangeTableID]
	ON [COM_CurrencyExchangeRate] ([ExchangeTableID])
	
ALTER TABLE [COM_CurrencyExchangeRate]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_CurrencyExchangeRate_ExchangeRateToCurrencyID_COM_Currency]
	FOREIGN KEY ([ExchangeRateToCurrencyID]) REFERENCES [COM_Currency] ([CurrencyID])
ALTER TABLE [COM_CurrencyExchangeRate]
	CHECK CONSTRAINT [FK_COM_CurrencyExchangeRate_ExchangeRateToCurrencyID_COM_Currency]
ALTER TABLE [COM_CurrencyExchangeRate]
	WITH CHECK
	ADD CONSTRAINT [FK_COM_CurrencyExchangeRate_ExchangeTableID_COM_ExchangeTable]
	FOREIGN KEY ([ExchangeTableID]) REFERENCES [COM_ExchangeTable] ([ExchangeTableID])
ALTER TABLE [COM_CurrencyExchangeRate]
	CHECK CONSTRAINT [FK_COM_CurrencyExchangeRate_ExchangeTableID_COM_ExchangeTable]
