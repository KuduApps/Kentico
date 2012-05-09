CREATE PROCEDURE [Proc_OM_PersonalizationVariant_InitOrders]
	@VariantPageTemplateID int,
	@VariantZoneID nvarchar(200),
	@VariantInstanceGUID uniqueidentifier = null
AS
BEGIN
	/* Declare the selection table */
	DECLARE @variantTable TABLE (
		VariantID int,
		VariantPosition int
	);
	
	IF (@VariantInstanceGUID IS NULL)
	BEGIN
		INSERT INTO @variantTable
			SELECT VariantID, VariantPosition FROM OM_PersonalizationVariant
			WHERE (VariantPageTemplateID = @VariantPageTemplateID) AND
				(VariantZoneID = @VariantZoneID) AND
				(VariantInstanceGUID IS NULL);
	END
	ELSE
	BEGIN
		INSERT INTO @variantTable
			SELECT VariantID, VariantPosition FROM OM_PersonalizationVariant
			WHERE (VariantPageTemplateID = @VariantPageTemplateID) AND
				(VariantZoneID = @VariantZoneID) AND
				(VariantInstanceGUID = @VariantInstanceGUID);
	END
	
	/* Declare the cursor to loop through the table */
	DECLARE @variantCursor CURSOR;
	
	-- sorting arrows
	SET @variantCursor = CURSOR FOR SELECT VariantID FROM @variantTable ORDER BY VariantPosition, VariantID ASC;
		
	/* Assign the numbers to the nodes */
	DECLARE @currentVariantID int, @currentIndex int;
	SET @currentIndex = 1;
	
	/* Loop through the table */
	OPEN @variantCursor
	FETCH NEXT FROM @variantCursor INTO @currentVariantID;
	WHILE @@FETCH_STATUS = 0
	BEGIN
		UPDATE OM_PersonalizationVariant SET VariantPosition = @currentIndex WHERE VariantID = @currentVariantID;
		/* Get next record */
		SET @currentIndex = @currentIndex + 1;
		FETCH NEXT FROM @variantCursor INTO @currentVariantID;
	END
	CLOSE @variantCursor;
	DEALLOCATE @variantCursor;
END
