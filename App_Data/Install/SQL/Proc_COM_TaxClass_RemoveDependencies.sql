CREATE PROCEDURE [Proc_COM_TaxClass_RemoveDependencies]
    @ID int
AS
BEGIN
    DELETE FROM COM_TaxClassCountry WHERE [TaxClassID] = @ID;
    DELETE FROM COM_TaxClassState   WHERE [TaxClassID] = @ID;
    DELETE FROM [COM_SKUTaxClasses] WHERE [TaxClassID] = @ID;
    UPDATE [COM_Department] SET [DepartmentDefaultTaxClassID] = NULL WHERE [DepartmentDefaultTaxClassID] = @ID;
    DELETE FROM COM_DepartmentTaxClass WHERE TaxClassID = @ID;
    DELETE FROM COM_ShippingOptionTaxClass WHERE TaxClassID = @ID;
END
