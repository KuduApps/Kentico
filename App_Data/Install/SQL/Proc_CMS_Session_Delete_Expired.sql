-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_CMS_Session_Delete_Expired]
AS
BEGIN
	DELETE FROM CMS_Session WHERE SessionExpired = '1' OR CURRENT_TIMESTAMP > SessionExpires	
END
