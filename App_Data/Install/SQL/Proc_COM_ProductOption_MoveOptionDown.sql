CREATE PROCEDURE [Proc_COM_ProductOption_MoveOptionDown]
@SKUID int,
    @OptionCategoryID int
AS
BEGIN
DECLARE @MaxOptionOrder int
	SET @MaxOptionOrder = (SELECT TOP 1 SKUOrder FROM COM_SKU WHERE SKUOptionCategoryID = @OptionCategoryID ORDER BY SKUOrder DESC);
	/* Move the next step(s) up */
	UPDATE COM_SKU SET SKUOrder = SKUOrder - 1 WHERE SKUOptionCategoryID = @OptionCategoryID AND SKUOrder = (SELECT SKUOrder FROM COM_SKU WHERE SKUID = @SKUID) + 1 
	/* Move the current step down */
	UPDATE COM_SKU SET SKUOrder = SKUOrder + 1 WHERE SKUOptionCategoryID = @OptionCategoryID AND SKUID = @SKUID AND SKUOrder < @MaxOptionOrder
END
