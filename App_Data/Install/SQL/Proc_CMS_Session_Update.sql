-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_CMS_Session_Update] 
	@SessionIdentificator nvarchar(200),
    @SessionUserID int, 
    @SessionLocation nvarchar(450),
	@SessionLastActive datetime, 
	@SessionLastLogon datetime, 
	@SessionExpires datetime, 
	@SessionExpired bit,
	@SessionSiteID int, 
	@SessionUserIsHidden bit
	
AS
BEGIN
	DELETE FROM CMS_Session WHERE SessionIdentificator = @SessionIdentificator
	INSERT INTO CMS_Session ([SessionIdentificator], [SessionUserID], [SessionLocation], [SessionLastActive], [SessionLastLogon], [SessionExpires], [SessionExpired], [SessionSiteID], [SessionUserIsHidden] ) VALUES (@SessionIdentificator,  @SessionUserID, @SessionLocation, @SessionLastActive, @SessionLastLogon, @SessionExpires, @SessionExpired, @SessionSiteID, @SessionUserIsHidden); SELECT SCOPE_IDENTITY() AS [SessionIdentificator] 
END
