-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Proc_OM_PersonalizationVariant_MoveUp] 
    @VariantID int
AS
BEGIN
	DECLARE @VariantPageTemplateID int;
	DECLARE @VariantZoneID nvarchar(200);
	DECLARE @VariantInstanceGUID uniqueidentifier;
	
	SET @VariantPageTemplateID = (SELECT TOP 1 VariantPageTemplateID FROM OM_PersonalizationVariant WHERE VariantID = @VariantID);
	SET @VariantZoneID = (SELECT TOP 1 VariantZoneID FROM OM_PersonalizationVariant WHERE VariantID = @VariantID);
	SET @VariantInstanceGUID = (SELECT TOP 1 VariantInstanceGUID FROM OM_PersonalizationVariant WHERE VariantID = @VariantID);
	/* Move the previous step(s) down */
	IF (@VariantInstanceGUID IS NULL)
	BEGIN	
		UPDATE OM_PersonalizationVariant 
		SET VariantPosition = VariantPosition + 1 
		WHERE
			(VariantPageTemplateID = @VariantPageTemplateID) AND
			(VariantZoneID = @VariantZoneID) AND
			(VariantInstanceGUID IS NULL) AND
			(VariantPosition = (SELECT VariantPosition FROM OM_PersonalizationVariant WHERE VariantID = @VariantID) - 1)
	END
	ELSE
	BEGIN
		UPDATE OM_PersonalizationVariant 
		SET VariantPosition = VariantPosition + 1 
		WHERE
			(VariantPageTemplateID = @VariantPageTemplateID) AND
			(VariantZoneID = @VariantZoneID) AND
			(VariantInstanceGUID = @VariantInstanceGUID) AND
			(VariantPosition = (SELECT VariantPosition FROM OM_PersonalizationVariant WHERE VariantID = @VariantID) - 1)
	END			
	/* Move the current step up */
	UPDATE OM_PersonalizationVariant SET VariantPosition = VariantPosition - 1 WHERE VariantID = @VariantID AND VariantPosition > 1
END
