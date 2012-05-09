CREATE PROCEDURE [Proc_COM_ExchangeTable_RemoveDependencies]
	@ID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	BEGIN TRANSACTION;
	-- COM_CurrencyExchangeRate
    DELETE FROM [COM_CurrencyExchangeRate] WHERE ExchangeTableID = @ID;
	COMMIT TRANSACTION;
END
