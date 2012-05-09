CREATE PROCEDURE [Proc_COM_Customer_RemoveDependencies]
	@ID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	BEGIN TRANSACTION;
	-- COM_Address
    DELETE FROM COM_Address WHERE AddressCustomerID = @ID;
	-- COM_CustomerCreditHistory
	DELETE FROM COM_CustomerCreditHistory WHERE EventCustomerID = @ID;
	-- Remove OM_Membership relation
	DELETE FROM OM_Membership WHERE RelatedID = @ID AND MemberType = 1 -- 1 = ecommerce customer
	COMMIT TRANSACTION;
END
