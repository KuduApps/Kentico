CREATE PROCEDURE [Proc_CMS_ResourceString_Insert]
    @StringID int,    
    @StringKey nvarchar(200),
    @StringIsCustom bit,
    @StringLoadGeneration int
AS
BEGIN
    SET NOCOUNT ON;   
    INSERT INTO [CMS_ResourceString] (
        [StringKey],
        [StringIsCustom],
        [StringLoadGeneration]
    )
    VALUES (
        @StringKey, 
        @StringIsCustom,
        @StringLoadGeneration
    )
    SELECT SCOPE_IDENTITY()
END
