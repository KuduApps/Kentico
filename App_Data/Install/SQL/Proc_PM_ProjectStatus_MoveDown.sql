-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_PM_ProjectStatus_MoveDown] 
    @StatusID int
AS
BEGIN
	DECLARE @MaxStatusOrder int
	SET @MaxStatusOrder = (SELECT TOP 1 StatusOrder FROM PM_ProjectStatus ORDER BY StatusOrder DESC);
	/* Move the next step(s) up */
	UPDATE PM_ProjectStatus SET StatusOrder = StatusOrder - 1 WHERE StatusOrder = (SELECT StatusOrder FROM PM_ProjectStatus WHERE StatusID=@StatusID) + 1 
	/* Move the current step down */
	UPDATE PM_ProjectStatus SET StatusOrder = StatusOrder + 1 WHERE StatusID = @StatusID AND StatusOrder < @MaxStatusOrder
END
