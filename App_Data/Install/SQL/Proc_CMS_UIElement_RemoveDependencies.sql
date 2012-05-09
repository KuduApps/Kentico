CREATE PROCEDURE [Proc_CMS_UIElement_RemoveDependencies] 
	@IDPath varchar(450)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Remove RoleUIElement dependencies
    DELETE FROM [CMS_RoleUIElement] 
        WHERE ElementID IN (
            SELECT ElementID FROM CMS_UIElement 
            WHERE ((ElementIDPath LIKE N'' + @IDPath + '/%') OR (ElementIDPath LIKE @IDPath))
        )
	-- Remove references within UIElment table
	UPDATE [CMS_UIElement] SET ElementParentID = NULL WHERE ((ElementIDPath LIKE N'' + @IDPath + '/%') OR (ElementIDPath LIKE @IDPath))
	
	-- Remove UIElement dependencies
	DELETE FROM [CMS_UIElement] 
        WHERE ElementID IN (
            SELECT ElementID FROM CMS_UIElement 
            WHERE ((ElementIDPath LIKE N'' + @IDPath + '/%') OR (ElementIDPath LIKE @IDPath))
        )
END
