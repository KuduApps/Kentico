CREATE PROCEDURE [Proc_CMS_UIElement_MoveUp]
	@ID int
AS
BEGIN
    /* Move elements only within same branch */
    DECLARE @ParentElementID int;
    SET @ParentElementID = (SELECT TOP 1 ElementParentID FROM CMS_UIElement WHERE ElementID = @ID);
    
	/* Move the previous elements down */
	UPDATE CMS_UIElement SET ElementOrder = ElementOrder + 1 WHERE ElementParentID = @ParentElementID AND ElementOrder = (SELECT ElementOrder FROM CMS_UIElement WHERE ElementID = @ID) - 1
	/* Move the current step up */
	UPDATE CMS_UIElement SET ElementOrder = ElementOrder - 1 WHERE ElementID = @ID AND ElementOrder > 1
END
