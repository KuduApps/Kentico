-- =============================================
-- Author:		Name
-- Create date: 25.7.2007
-- Description:	Stores log records to DB
-- =============================================
CREATE PROCEDURE [Proc_OM_EnableMVTCombinations]
	@MVTVariantID int,
	@enabled bit
AS
BEGIN
IF (@enabled = 1)
BEGIN
	UPDATE OM_MVTCombination
	SET MVTCombinationEnabled = 1
	WHERE
		MVTCombinationID IN
		(
			SELECT MVTCombinationID
			FROM OM_MVTCombinationVariation
			WHERE
				(MVTVariantID = @MVTVariantID) AND
				(MVTCombinationID NOT IN
					(
						SELECT MVTCombinationID
						FROM OM_MVTCombinationVariation
						WHERE 
							MVTVariantID IN
							(
								SELECT MVTVariantID
								FROM OM_MVTVariant
								WHERE MVTVariantEnabled = 0
							)
					)
				)
		)
END
ELSE
BEGIN
	UPDATE OM_MVTCombination
	SET MVTCombinationEnabled = 0
	WHERE
		MVTCombinationID IN (
			SELECT MVTCombinationID FROM OM_MVTCombinationVariation WHERE MVTVariantID = @MVTVariantID
		)
END
END
