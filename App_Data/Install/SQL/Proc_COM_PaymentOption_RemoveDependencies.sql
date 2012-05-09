CREATE PROCEDURE [Proc_COM_PaymentOption_RemoveDependencies] 
	
	@ID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	BEGIN TRANSACTION;
	-- CMS_UserSite
	UPDATE CMS_UserSite SET UserPreferredPaymentOptionID = NULL WHERE UserPreferredPaymentOptionID = @ID;
	
	--COM_PaymentShipping
    DELETE FROM COM_PaymentShipping WHERE PaymentOptionID = @ID;
	COMMIT TRANSACTION;
END
