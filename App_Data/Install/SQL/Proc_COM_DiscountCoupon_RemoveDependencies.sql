-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_COM_DiscountCoupon_RemoveDependencies]
	@ID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	BEGIN TRANSACTION;
    -- COM_SKUDiscountCoupon
	DELETE FROM COM_SKUDiscountCoupon WHERE DiscountCouponID = @ID;
	COMMIT TRANSACTION;
END
