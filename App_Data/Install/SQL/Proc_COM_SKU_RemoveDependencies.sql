CREATE PROCEDURE [Proc_COM_SKU_RemoveDependencies]
	@ID int
AS
BEGIN
	UPDATE [CMS_TREE] SET NODESKUID=NULL WHERE NODESKUID = @ID;
	DELETE FROM [COM_ShoppingCartSKU] WHERE SKUID = @ID;
	DELETE FROM [COM_SKUTaxClasses] WHERE SKUID = @ID;
	DELETE FROM [COM_SKUDiscountCoupon] WHERE SKUID = @ID;
	DELETE FROM [COM_Wishlist] WHERE SKUID = @ID;
	DELETE FROM [COM_VolumeDiscount] WHERE VolumeDiscountSKUID = @ID;
	DELETE FROM [COM_OrderItem] WHERE OrderItemSKUID = @ID;
	DELETE FROM [COM_SKUOptionCategory] WHERE SKUID = @ID;
	DELETE FROM [CMS_MetaFile] WHERE MetaFileObjectID = @ID AND MetaFileObjectType = 'ecommerce.sku';
	
	-- Remove SKU product type dependencies
	DELETE FROM [COM_Bundle] WHERE BundleID = @ID OR SKUID = @ID;
	DELETE FROM [COM_SKUFile] WHERE FileSKUID = @ID;
END
