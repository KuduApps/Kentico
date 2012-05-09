-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_COM_Currency_RemoveDependencies]
	@ID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	BEGIN TRANSACTION;
	-- CMS_UserSite
	UPDATE CMS_UserSite SET UserPreferredCurrencyID = NULL WHERE UserPreferredCurrencyID=@ID;
	
	-- COM_CurrencyExchangeRate
    DELETE FROM COM_CurrencyExchangeRate WHERE ExchangeRateToCurrencyID=@ID;
	COMMIT TRANSACTION;
END
