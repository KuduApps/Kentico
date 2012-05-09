-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_COM_Order_RemoveDependencies]
	@ID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	BEGIN TRANSACTION;
	-- COM_OrderItem - Deleted by special method
	--DELETE FROM COM_OrderItem WHERE OrderItemOrderID = @ID;
	-- COM_OrderStatusUser
	DELETE FROM COM_OrderStatusUser WHERE OrderID = @ID;
	COMMIT TRANSACTION;
END
