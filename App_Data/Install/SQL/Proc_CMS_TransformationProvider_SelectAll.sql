-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_CMS_TransformationProvider_SelectAll]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT CMS_Class.ClassName, CMS_Transformation.* FROM
		CMS_Class INNER JOIN CMS_Transformation ON
		CMS_Class.ClassID=CMS_Transformation.TransformationClassID
END
