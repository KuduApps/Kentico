-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_CMS_WebTemplate_MoveUp]
	@ID int
AS
BEGIN
	/* Move the previous step(s) down */
	UPDATE CMS_WebTemplate SET WebTemplateOrder = WebTemplateOrder + 1 WHERE WebTemplateOrder = (SELECT WebTemplateOrder FROM CMS_WebTemplate WHERE WebTemplateID = @ID) - 1
	/* Move the current step up */
	UPDATE CMS_WebTemplate SET WebTemplateOrder = WebTemplateOrder - 1 WHERE WebTemplateID = @ID AND WebTemplateOrder > 1
END
