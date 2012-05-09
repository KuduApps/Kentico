CREATE PROCEDURE [Proc_CMS_Category_UpdateIDPath]
	@OldParentID int,
	@NewParentID int
AS
BEGIN
    DECLARE @OldPrefix VARCHAR(450);
	DECLARE @NewPrefix VARCHAR(450);
	DECLARE @OldLevel INT;
	DECLARE @NewLevel INT;
	SET @OldPrefix = (SELECT TOP 1 CategoryIDPath FROM CMS_Category WHERE CategoryID = @OldParentID);
	SET @NewPrefix = (SELECT TOP 1 CategoryIDPath FROM CMS_Category WHERE CategoryID = @NewParentID);
	SET @OldLevel = (SELECT TOP 1 CategoryLevel FROM CMS_Category WHERE CategoryID = @OldParentID);
	SET @NewLevel = (SELECT TOP 1 CategoryLevel FROM CMS_Category WHERE CategoryID = @NewParentID);
    -- UPDATE ID PATH
	UPDATE CMS_Category SET CategoryLevel = CategoryLevel - @OldLevel + ISNULL(@NewLevel, -1) + 1, CategoryIDPath = ISNULL(@NewPrefix, '') + SUBSTRING(CategoryIDPath, LEN(@OldPrefix) - 8, LEN(CategoryIDPath)) WHERE CategoryIDPath LIKE @OldPrefix + '%'
END
