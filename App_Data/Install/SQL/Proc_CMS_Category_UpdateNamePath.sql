CREATE PROCEDURE [Proc_CMS_Category_UpdateNamePath]
	@OldParentID int,
	@NewParentID int
AS
BEGIN
	DECLARE @OldIdPrefix NVARCHAR(450);
    DECLARE @OldPrefix NVARCHAR(1500);
	DECLARE @NewPrefix NVARCHAR(1500);
	DECLARE @Name NVARCHAR(250);
	DECLARE @OldLevel INT;
	DECLARE @NewLevel INT;
	SET @OldIdPrefix = (SELECT TOP 1 CategoryIDPath FROM CMS_Category WHERE CategoryID = @OldParentID);
	SET @OldPrefix = (SELECT TOP 1 CategoryNamePath FROM CMS_Category WHERE CategoryID = @OldParentID);
	SET @Name = (SELECT TOP 1 CategoryDisplayName FROM CMS_Category WHERE CategoryID = @OldParentID);
	SET @NewPrefix = (SELECT TOP 1 CategoryNamePath FROM CMS_Category WHERE CategoryID = @NewParentID);
	SET @OldLevel = (SELECT TOP 1 CategoryLevel FROM CMS_Category WHERE CategoryID = @OldParentID);
	SET @NewLevel = (SELECT TOP 1 CategoryLevel FROM CMS_Category WHERE CategoryID = @NewParentID);
    -- UPDATE NAME PATH
	UPDATE CMS_Category SET CategoryLevel = CategoryLevel - @OldLevel + ISNULL(@NewLevel, -1) + 1, CategoryNamePath = ISNULL(@NewPrefix, '') + '/' + @Name + SUBSTRING(CategoryNamePath, LEN(@OldPrefix)+1, LEN(CategoryNamePath)) WHERE CategoryIDPath LIKE @OldIdPrefix + '%'
END
