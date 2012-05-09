-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_CMS_WebTemplate_InitOrders]
AS
BEGIN
	/* Declare the selection table */
	DECLARE @webtemplateTable TABLE (
		WebTemplateID int NOT NULL,
		WebTemplateOrder int NOT NULL
	);
	
	/* Get the steps list */
	INSERT INTO @webtemplateTable SELECT WebTemplateID, WebTemplateOrder FROM CMS_WebTemplate
	
	/* Declare the cursor to loop through the table */
	DECLARE @cursor CURSOR;
    SET @cursor = CURSOR FOR SELECT WebTemplateID, WebTemplateOrder FROM @webtemplateTable ORDER BY WebTemplateOrder ASC, WebTemplateID ASC;
	/* Assign the numbers to the steps */
	DECLARE @currentIndex int, @currentOrder int;
	SET @currentIndex = 1;
	DECLARE @currentId int;
	
	/* Loop through the table */
	OPEN @cursor
	FETCH NEXT FROM @cursor INTO @currentId, @currentOrder;
	WHILE @@FETCH_STATUS = 0
	BEGIN
		/* Set the step index if different */
		IF @currentOrder IS NULL OR @currentOrder <> @currentIndex
			UPDATE CMS_WebTemplate SET WebTemplateOrder = @currentIndex WHERE WebTemplateID = @currentId;
		/* Get next record */
		SET @currentIndex = @currentIndex + 1;
		FETCH NEXT FROM @cursor INTO @currentId, @currentOrder;
	END
	CLOSE @cursor;
	DEALLOCATE @cursor;
END
