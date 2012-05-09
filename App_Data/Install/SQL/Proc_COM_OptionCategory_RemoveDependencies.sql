CREATE PROCEDURE [Proc_COM_OptionCategory_RemoveDependencies]
	@ID int
AS
BEGIN	
	DELETE FROM [COM_SKUOptionCategory] WHERE CategoryID = @ID;
	DELETE FROM [COM_SKU] WHERE SKUOptionCategoryID = @ID;
END
