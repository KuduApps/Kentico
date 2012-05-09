-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_CMS_Class_SelectClassSite] 
	@ClassID int = 0,
	@SiteID int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for procedure here
	SELECT * FROM CMS_ClassSite WHERE ClassID=@ClassID AND SiteID=@SiteID
END
