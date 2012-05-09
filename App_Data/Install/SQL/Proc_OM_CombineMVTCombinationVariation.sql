-- =============================================
-- Author:		Name
-- Create date: 25.7.2007
-- Description:	Stores log records to DB
-- =============================================
CREATE PROCEDURE [Proc_OM_CombineMVTCombinationVariation]
	@OriginalCombinationID int,
	@NewCombinationID int,
	@NewVariantID int
AS
BEGIN
	DECLARE @currentVariantId int;
	DECLARE @cursorVariantIDs CURSOR;
	SET @cursorVariantIDs = CURSOR FOR
		SELECT MVTVariantID FROM OM_MVTCombinationVariation WHERE MVTCombinationID = @OriginalCombinationID;
		
	OPEN @cursorVariantIDs
	FETCH NEXT FROM @cursorVariantIDs INTO @currentVariantId;
	WHILE @@FETCH_STATUS = 0
	BEGIN
		INSERT INTO OM_MVTCombinationVariation (MVTCombinationID, MVTVariantID) VALUES ( @NewCombinationID, @currentVariantId);
		
		FETCH NEXT FROM @cursorVariantIDs INTO @currentVariantId;
	END
	
	INSERT INTO OM_MVTCombinationVariation (MVTCombinationID, MVTVariantID) VALUES ( @NewCombinationID, @NewVariantID);
END
