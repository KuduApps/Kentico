-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_CMS_WebTemplate_MoveDown]
	@ID int
AS
BEGIN
	DECLARE @MaxAnswerOrder int
	SET @MaxAnswerOrder = (SELECT TOP 1 WebTemplateOrder FROM CMS_WebTemplate ORDER BY WebTemplateOrder DESC);
	/* Move the next step(s) up */
	UPDATE CMS_WebTemplate SET WebTemplateOrder = WebTemplateOrder - 1 WHERE WebTemplateOrder = (SELECT WebTemplateOrder FROM CMS_WebTemplate WHERE WebTemplateID = @ID) + 1
	/* Move the current step down */
	UPDATE CMS_WebTemplate SET WebTemplateOrder = WebTemplateOrder + 1 WHERE WebTemplateID = @ID AND WebTemplateOrder < @MaxAnswerOrder
END
