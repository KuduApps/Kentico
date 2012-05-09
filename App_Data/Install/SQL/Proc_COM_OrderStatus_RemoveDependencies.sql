-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_COM_OrderStatus_RemoveDependencies]
	@ID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	BEGIN TRANSACTION;
    -- COM_Order
	UPDATE COM_Order SET [OrderStatusID] = NULL WHERE [OrderStatusID] = @ID;
	
	COMMIT TRANSACTION;
END
