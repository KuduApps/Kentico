CREATE PROCEDURE [Proc_CMS_SettingsKey_Update]
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
    UPDATE CMS_SettingsKey SET 
        [KeyName] = @KeyName,
        [KeyDisplayName] = @KeyDisplayName,
        [KeyDescription] = @KeyDescription,
        [KeyValue] = @KeyValue,
        [KeyType] = @KeyType, 
        [KeyCategoryID] = @KeyCategoryID,
        [SiteID] = @SiteID,
        [KeyLastModified] = @KeyLastModified,
        [KeyGUID] = @KeyGUID,
        [KeyOrder] = @KeyOrder,
        [KeyDefaultValue] = @KeyDefaultValue,
        [KeyValidation] = @KeyValidation,
        [KeyEditingControlPath] = @KeyEditingControlPath,
        [KeyLoadGeneration] = @KeyLoadGeneration,
        [KeyIsGlobal] = @KeyIsGlobal,
        [KeyIsCustom] = @KeyIsCustom,
        [KeyIsHidden] = @KeyIsHidden
    WHERE KeyID = @KeyID
END
