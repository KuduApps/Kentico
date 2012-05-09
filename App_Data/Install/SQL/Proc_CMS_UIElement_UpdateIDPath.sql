CREATE PROCEDURE [Proc_CMS_UIElement_UpdateIDPath]
	@OldParentID int,
	@NewParentID int
AS
BEGIN
    DECLARE @OldPrefix VARCHAR(450);
	DECLARE @NewPrefix VARCHAR(450);
	DECLARE @OldLevel INT;
	DECLARE @NewLevel INT;
	SET @OldPrefix = (SELECT TOP 1 ElementIDPath FROM CMS_UIElement WHERE ElementID = @OldParentID);
	SET @NewPrefix = (SELECT TOP 1 ElementIDPath FROM CMS_UIElement WHERE ElementID = @NewParentID);
	SET @OldLevel = (SELECT TOP 1 ElementLevel FROM CMS_UIElement WHERE ElementID = @OldParentID);
	SET @NewLevel = (SELECT TOP 1 ElementLevel FROM CMS_UIElement WHERE ElementID = @NewParentID);
    -- UPDATE ID PATH
	UPDATE CMS_UIElement SET ElementLevel = ElementLevel - @OldLevel + @NewLevel + 1, ElementIDPath = @NewPrefix + SUBSTRING(ElementIDPath, LEN(@OldPrefix) - 8, LEN(ElementIDPath)) WHERE ElementIDPath LIKE @OldPrefix + '%'
END
