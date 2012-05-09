-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_CMS_Class_InsertClassSite]
	@ClassID int = NULL,
	@SiteID int = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for procedure here
	INSERT INTO CMS_ClassSite (
		ClassID,
		SiteID
	)
	VALUES (
		@ClassID,
		@SiteID
	)
END
