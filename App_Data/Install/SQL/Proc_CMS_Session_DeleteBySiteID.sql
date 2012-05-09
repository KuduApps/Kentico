-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_CMS_Session_DeleteBySiteID]
	@SessionSiteID int
AS
BEGIN
	DELETE FROM CMS_Session WHERE SessionSiteID = @SessionSiteID	
END
