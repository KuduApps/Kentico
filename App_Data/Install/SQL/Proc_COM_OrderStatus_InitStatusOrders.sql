CREATE PROCEDURE [Proc_COM_OrderStatus_InitStatusOrders]
	@StatusID int
AS
BEGIN
	/* Declare the selection table */
	DECLARE @statusTable TABLE (
		StatusID int NOT NULL,
		StatusOrder int NULL
	);
	
	DECLARE @SiteID int
    SET @SiteID = (SELECT ISNULL(StatusSiteID, 0) FROM COM_OrderStatus WHERE StatusID = @StatusID);
	
	/* Get the steps list */
	INSERT INTO @statusTable SELECT StatusID, StatusOrder FROM COM_OrderStatus WHERE ISNULL(StatusSiteID, 0) = @SiteID;
	
	/* Declare the cursor to loop through the table */
	DECLARE @statusCursor CURSOR;
    SET @statusCursor = CURSOR FOR SELECT StatusID, StatusOrder FROM @statusTable ORDER BY StatusOrder ASC, StatusID ASC;
	/* Assign the numbers to the steps */
	DECLARE @currentIndex int, @currentStatusOrder int;
	SET @currentIndex = 1;
	DECLARE @currentStatusId int;
	
	/* Loop through the table */
	OPEN @statusCursor
	FETCH NEXT FROM @statusCursor INTO @currentStatusId, @currentStatusOrder;
	WHILE @@FETCH_STATUS = 0
	BEGIN
		/* Set the step index if different */
		IF @currentStatusOrder IS NULL OR @currentStatusOrder <> @currentIndex
			UPDATE COM_OrderStatus SET StatusOrder = @currentIndex WHERE StatusID = @currentStatusId;
		/* Get next record */
		SET @currentIndex = @currentIndex + 1;
		FETCH NEXT FROM @statusCursor INTO @currentStatusId, @currentStatusOrder;
	END
	CLOSE @statusCursor;
	DEALLOCATE @statusCursor;
END
