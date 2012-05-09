CREATE PROCEDURE [Proc_COM_OrderStatus_MoveStatusUp]
	@StatusID int
AS
BEGIN
    DECLARE @SiteID int
    SET @SiteID = (SELECT ISNULL(StatusSiteID, 0) FROM COM_OrderStatus WHERE StatusID = @StatusID);
    /* Move the previous step(s) down */
	UPDATE COM_OrderStatus SET StatusOrder = StatusOrder + 1 WHERE ISNULL(StatusSiteID, 0) = @SiteID AND
	                                                               StatusOrder = (SELECT StatusOrder FROM COM_OrderStatus WHERE StatusID = @StatusID) - 1 
	/* Move the current step up */
	UPDATE COM_OrderStatus SET StatusOrder = StatusOrder - 1 WHERE StatusID = @StatusID AND StatusOrder > 1
END
