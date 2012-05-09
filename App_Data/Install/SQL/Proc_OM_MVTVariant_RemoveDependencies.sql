-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_OM_MVTVariant_RemoveDependencies]
	@ID int
AS
BEGIN
	DELETE FROM [OM_MVTCombinationVariation] WHERE MVTCombinationID IN (SELECT MVTCombinationID FROM [OM_MVTCombinationVariation] WHERE MVTVariantID = @ID);
	DELETE FROM [OM_MVTCombination]
		WHERE (MVTCombinationID NOT IN (SELECT MVTCombinationID FROM [OM_MVTCombinationVariation]))
		AND ((MVTCombinationIsDefault <> 1) OR (MVTCombinationIsDefault IS NULL))
END
