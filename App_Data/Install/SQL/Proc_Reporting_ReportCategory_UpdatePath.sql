CREATE PROCEDURE [Proc_Reporting_ReportCategory_UpdatePath] 
	@OldCategoryPath nvarchar(450), 
    @NewCategoryPath nvarchar(450)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
---- Update category paths
UPDATE Reporting_ReportCategory SET 
    CategoryPath = @NewCategoryPath + RIGHT(CategoryPath, LEN(CategoryPath) - LEN(@OldCategoryPath)) 
WHERE
    LEFT(CategoryPath, LEN(@OldCategoryPath) + 1) = @OldCategoryPath + '/' ; 
END
