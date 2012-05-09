CREATE PROCEDURE [Proc_CMS_SettingsKey_Insert]
    @KeyID int,
    @KeyName nvarchar(100),
    @KeyDisplayName nvarchar(200),
    @KeyDescription ntext,
    @KeyValue ntext,
    @KeyType nvarchar(50),
    @KeyCategoryID int,
    @SiteID int,
    @KeyLastModified datetime,
    @KeyGUID uniqueidentifier,
    @KeyOrder int,
    @KeyDefaultValue ntext,
    @KeyValidation nvarchar(255),
    @KeyEditingControlPath nvarchar(200),
    @KeyLoadGeneration int,
    @KeyIsGlobal bit,
    @KeyIsCustom bit,
    @KeyIsHidden bit    
AS
BEGIN
    INSERT INTO CMS_SettingsKey (
        [KeyName],
        [KeyDisplayName],
        [KeyDescription],
        [KeyValue],
        [KeyType],
        [KeyCategoryID],
        [SiteID],
        [KeyLastModified],
        [KeyGUID],
        [KeyOrder],
        [KeyDefaultValue],
        [KeyValidation],
        [KeyEditingControlPath],
        [KeyLoadGeneration],
        [KeyIsGlobal],
        [KeyIsCustom],
        [KeyIsHidden]
    )
    VALUES (
        @KeyName,
        @KeyDisplayName,
        @KeyDescription,
        @KeyValue, 
        @KeyType, 
        @KeyCategoryID,
        @SiteID,
        @KeyLastModified,
        @KeyGUID,
        @KeyOrder,
        @KeyDefaultValue,
        @KeyValidation,
        @KeyEditingControlPath,
        @KeyLoadGeneration,
        @KeyIsGlobal,
        @KeyIsCustom,
		@KeyIsHidden
    )
    SELECT SCOPE_IDENTITY()
END
