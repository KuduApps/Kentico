CREATE PROCEDURE [Proc_COM_OrderStatus_MoveStatusDown]
	@StatusID int
AS
BEGIN
    DECLARE @SiteID int
    SET @SiteID = (SELECT ISNULL(StatusSiteID, 0) FROM COM_OrderStatus WHERE StatusID = @StatusID);
    
	DECLARE @MaxStatusOrder int
	SET @MaxStatusOrder = (SELECT TOP 1 StatusOrder FROM COM_OrderStatus WHERE ISNULL(StatusSiteID, 0) = @SiteID ORDER BY StatusOrder DESC);
	/* Move the next step(s) up */
	UPDATE COM_OrderStatus SET StatusOrder = StatusOrder - 1 WHERE StatusOrder = (SELECT StatusOrder FROM COM_OrderStatus WHERE StatusID = @StatusID) + 1 AND
	                                                               ISNULL(StatusSiteID, 0) = @SiteID
	/* Move the current step down */
	UPDATE COM_OrderStatus SET StatusOrder = StatusOrder + 1 WHERE StatusID = @StatusID AND StatusOrder < @MaxStatusOrder
END
