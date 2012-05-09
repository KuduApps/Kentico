CREATE PROCEDURE [Proc_CMS_UIElement_MoveDown]
	@ID int
AS
BEGIN
	DECLARE @MaxOrder int
	SET @MaxOrder = (SELECT TOP 1 ElementOrder FROM CMS_UIElement ORDER BY ElementOrder DESC);
	
	/* Move elements only within same branch */
    DECLARE @ParentElementID int;
    SET @ParentElementID = (SELECT TOP 1 ElementParentID FROM CMS_UIElement WHERE ElementID = @ID);
	
	/* Move the next step(s) up */
	UPDATE CMS_UIElement SET ElementOrder = ElementOrder - 1 WHERE ElementParentID = @ParentElementID AND ElementOrder = (SELECT ElementOrder FROM CMS_UIElement WHERE ElementID = @ID) + 1
	/* Move the current step down */
	UPDATE CMS_UIElement SET ElementOrder = ElementOrder + 1 WHERE ElementID = @ID AND ElementOrder < @MaxOrder
END
