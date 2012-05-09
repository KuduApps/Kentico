-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_PM_ProjectStatus_MoveUp] 
	@StatusID int
AS
BEGIN
    /* Move the previous step(s) down */
	UPDATE PM_ProjectStatus SET StatusOrder = StatusOrder + 1 WHERE StatusOrder = (SELECT StatusOrder FROM PM_ProjectStatus WHERE StatusID = @StatusID) - 1 
	/* Move the current step up */
	UPDATE PM_ProjectStatus SET StatusOrder = StatusOrder - 1 WHERE StatusID = @StatusID AND StatusOrder > 1
END
