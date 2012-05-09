-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_OM_PersonalizationVariant_MoveDown] 
	@VariantID int
AS
BEGIN
	DECLARE @maxVariantOrder int;
	DECLARE @VariantPageTemplateID int;
	DECLARE @VariantZoneID nvarchar(200);
	DECLARE @VariantInstanceGUID uniqueidentifier;
	
	SET @VariantPageTemplateID = (SELECT TOP 1 VariantPageTemplateID FROM OM_PersonalizationVariant WHERE VariantID = @VariantID);
	SET @VariantZoneID = (SELECT TOP 1 VariantZoneID FROM OM_PersonalizationVariant WHERE VariantID = @VariantID);
	SET @VariantInstanceGUID = (SELECT TOP 1 VariantInstanceGUID FROM OM_PersonalizationVariant WHERE VariantID = @VariantID);
	
	IF (@VariantInstanceGUID IS NULL)
	BEGIN
		SET @maxVariantOrder = (SELECT TOP 1 VariantPosition FROM OM_PersonalizationVariant
								WHERE
									(VariantPageTemplateID = @VariantPageTemplateID) AND
									(VariantZoneID = @VariantZoneID) AND
									(VariantInstanceGUID IS NULL)
								ORDER BY VariantPosition DESC);
	END
	ELSE
	BEGIN
		SET @maxVariantOrder = (SELECT TOP 1 VariantPosition FROM OM_PersonalizationVariant
								WHERE
									(VariantPageTemplateID = @VariantPageTemplateID) AND
									(VariantZoneID = @VariantZoneID) AND
									(VariantInstanceGUID = @VariantInstanceGUID)
								ORDER BY VariantPosition DESC);
	END
	/* Move the next step(s) up */
	UPDATE OM_PersonalizationVariant SET VariantPosition = VariantPosition - 1 WHERE VariantPosition = (SELECT VariantPosition FROM OM_PersonalizationVariant WHERE VariantID = @VariantID) + 1 
	/* Move the current step down */
	UPDATE OM_PersonalizationVariant SET VariantPosition = VariantPosition + 1 WHERE VariantID = @VariantID AND VariantPosition < @maxVariantOrder
END
