CREATE PROCEDURE [Proc_COM_ShippingOption_RemoveDependencies] 
	
	@ID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	BEGIN TRANSACTION;
	UPDATE CMS_UserSite SET UserPreferredShippingOptionID = NULL WHERE UserPreferredShippingOptionID = @ID;
	UPDATE COM_Customer SET CustomerPreferredShippingOptionID = NULL WHERE CustomerPreferredShippingOptionID = @ID;
	UPDATE COM_Customer SET CustomerPrefferedPaymentOptionID = NULL WHERE CustomerPrefferedPaymentOptionID IN (SELECT PaymentOptionID FROM COM_PaymentShipping WHERE ShippingOptionID = @ID);
    DELETE FROM COM_PaymentShipping WHERE ShippingOptionID = @ID;
    DELETE FROM COM_ShippingCost WHERE ShippingCostShippingOptionID = @ID;
    DELETE FROM COM_ShippingOptionTaxClass WHERE ShippingOptionID = @ID;
	COMMIT TRANSACTION;
END
