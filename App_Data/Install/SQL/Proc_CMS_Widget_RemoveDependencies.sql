CREATE PROCEDURE [Proc_CMS_Widget_RemoveDependencies]
    @ID int
AS
BEGIN    
    DELETE FROM CMS_WidgetRole WHERE WidgetID = @ID
END
