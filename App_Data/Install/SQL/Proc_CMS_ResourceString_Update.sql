CREATE PROCEDURE [Proc_CMS_ResourceString_Update]
    @StringID int,    
    @StringKey nvarchar(200),
    @StringIsCustom bit,
    @StringLoadGeneration int
AS
BEGIN
    SET NOCOUNT ON;   
    UPDATE [CMS_ResourceString]
    SET
        [StringKey] = @StringKey,
        [StringIsCustom] = @StringIsCustom,
        [StringLoadGeneration] = @StringLoadGeneration
    WHERE
        [StringID] = @StringID
END
