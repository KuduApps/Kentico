-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_CMS_Class_DeleteClassSite]
	@ClassID int,
	@SiteID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    DELETE FROM CMS_ClassSite WHERE ClassID=@ClassID AND SiteID=@SiteID
END
