-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_CMS_Session_Delete]
	@SessionIdentificator nvarchar(200)
AS
BEGIN
	DELETE FROM CMS_Session WHERE SessionIdentificator = @SessionIdentificator	
END
