CREATE PROCEDURE [Proc_COM_ProductOption_MoveOptionUp]
	@SkuID int,
    @OptionCategoryID int
AS
BEGIN
    /* Move the previous step(s) down */
	UPDATE COM_SKU SET SKUOrder = SKUOrder + 1 WHERE SKUOptionCategoryID = @OptionCategoryID AND SKUOrder = (SELECT SKUOrder FROM COM_SKU WHERE SKUID = @SkuID) - 1 
	/* Move the current step up */
	UPDATE COM_SKU SET SKUOrder = SKUOrder - 1 WHERE SKUOptionCategoryID = @OptionCategoryID AND SKUID = @SkuID AND SKUOrder > 1
END
