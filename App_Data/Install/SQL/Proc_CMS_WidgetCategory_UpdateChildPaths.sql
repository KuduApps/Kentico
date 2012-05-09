CREATE PROCEDURE [Proc_CMS_WidgetCategory_UpdateChildPaths] 
	@OldCategoryPath nvarchar(450), 
    @NewCategoryPath nvarchar(450)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
-- Update category paths
UPDATE CMS_WidgetCategory SET 
    WidgetCategoryPath = @NewCategoryPath + RIGHT(WidgetCategoryPath, LEN(WidgetCategoryPath) - LEN(@OldCategoryPath)) 
WHERE
    LEFT(WidgetCategoryPath, LEN(@OldCategoryPath) + 1) = @OldCategoryPath + '/' ; 
END
