CREATE PROCEDURE [Proc_COM_OrderItem_RemoveDependencies]
	@ID int
AS
BEGIN
	DELETE FROM [COM_OrderItemSKUFile] WHERE OrderItemID = @ID;
END
